using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Jumper
{
    public partial class JumpForm : Form
    {
        public JumpForm()
        {
            InitializeComponent();
            RefreshOnTop = true;
            ShortcutsFolder = null;
        }

        private void JumpForm_Load(object sender, EventArgs e)
        {
            if (ShortcutsFolder == "")
                return;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            WindowProperties.SetWindowProperty(GetHandle(), SystemProperties.System.AppUserModel.PreventPinning, true);
            RefreshDialog();
        }

        public string ShortcutsFolder
        {
            get;
            set;
        }

        public bool RefreshOnTop
        {
            get;
            set;
        }

        private void browseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = ShortcutsFolder;
            DialogResult res = dlg.ShowDialog();

            if (res == DialogResult.OK)
            {
                ShortcutsFolder = dlg.SelectedPath;
                RefreshDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EnableControls(bool enable)
        {
            listJumpItems.Enabled = enable;
            dropDefault.Enabled = enable;
            browseDefault.Enabled = enable;
        }

        private void RefreshDialog()
        {
            bool success = RefreshJumpItems();
            EnableControls(success);

            textFolder.Text = ShortcutsFolder;

            string format = success ? "Jumper - {0}" : "Jumper";
            Text = String.Format(format, ShortcutsFolder);

            if (success)
            {
                WindowProperties.SetWindowProperty(GetHandle(), SystemProperties.System.AppUserModel.PreventPinning, false);
                TaskbarManager.Instance.ApplicationId = "Jumper@" + ShortcutsFolder;

                RefreshJumpList(GetHandle(), ShortcutsFolder, RefreshOnTop);
            }

            RefreshCommand();
        }

        private void RefreshCommand()
        {
            if (!listJumpItems.Enabled)
            {
                command.Text = "";
                return;
            }

            if (dropDefault.SelectedItem != null)
            {
                command.Text = BuildCommand(Application.ExecutablePath, "--dir", ShortcutsFolder, RefreshOnTop ? "" : "--bottom", "--launch", dropDefault.SelectedItem.ToString());
            }
            else
            {
                command.Text = BuildCommand(Application.ExecutablePath, "--dir", ShortcutsFolder, RefreshOnTop ? "" : "--bottom");
            }
        }

        private bool RefreshJumpItems()
        {
            listJumpItems.Clear();
            dropDefault.Items.Clear();

            if (!Directory.Exists(ShortcutsFolder))
                return false;

            ListViewItem[] items = ShortcutsToLinks(ShortcutsFolder).Select(jumpItem => new ListViewItem(jumpItem.Title)).ToArray();
            listJumpItems.Items.AddRange(items);
            dropDefault.Items.AddRange(
                Directory.GetFiles(ShortcutsFolder, "*.lnk", SearchOption.TopDirectoryOnly).OrderBy(x => x, new StringLogicalComparer()).ToArray());

            return true;
        }

        private IntPtr GetHandle()
        {
            return this.Handle;
        }

        private static string BuildCommand(params string[] args)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string a in args)
            {
                if (string.IsNullOrEmpty(a))
                    continue;

                if (builder.Length > 0)
                    builder.Append(" ");
                builder.Append(ArgumentEscape(a));
            }
            return builder.ToString();
        }

        private static string ArgumentEscape(string arg)
        {
            if (arg.Contains(@" "))
                return @"""" + arg + @"""";
            return arg;
        }

        private static string GetDisplayName(string filename)
        {
            return System.Text.RegularExpressions.Regex.Replace(Path.GetFileNameWithoutExtension(filename), @"^_.*?_", "").Trim();
        }

        private static IEnumerable<JumpListLink> ShortcutsToLinks(string folderName)
        {
            Shell32.Shell shell = new Shell32.ShellClass();
            Shell32.Folder folder = shell.NameSpace(folderName);

            foreach (string filename in Directory.GetFiles(folderName, "*.lnk", SearchOption.TopDirectoryOnly).OrderBy(x => x, new StringLogicalComparer()))
            {
                string filenameOnly = System.IO.Path.GetFileName(filename);

                if (filenameOnly.StartsWith("-"))
                    continue;

                bool runAs = Program.IsMarkedRunAs(filename);

                Shell32.FolderItem folderItem = folder.ParseName(filenameOnly);
                if (folderItem != null)
                {
                    Shell32.ShellLinkObject link = (Shell32.ShellLinkObject)folderItem.GetLink;

                    string iconPath;
                    int iconId = link.GetIconLocation(out iconPath);

                    if (String.IsNullOrEmpty(iconPath))
                    {
                        iconPath = link.Path;
                        iconId = 0;
                    }

                    JumpListLink jumpLink;
                    if (runAs)
                    {
                        jumpLink = new JumpListLink(Application.ExecutablePath, GetDisplayName(filenameOnly))
                            {
                                Arguments = BuildCommand("--launch", filename),
                                WorkingDirectory = Directory.GetCurrentDirectory(),
                            };
                    }
                    else
                    {
                        jumpLink = new JumpListLink(link.Path, GetDisplayName(filenameOnly))
                            {
                                Arguments = link.Arguments,
                                WorkingDirectory = link.WorkingDirectory,
                                ShowCommand = Enum.IsDefined(typeof(WindowShowCommand), link.ShowCommand) ?
                                                (WindowShowCommand)link.ShowCommand : WindowShowCommand.Default,
                            };
                    }

                    jumpLink.IconReference = new IconReference(iconPath, iconId);
                    yield return jumpLink;
                }
            }
        }

        internal static void RefreshJumpList(IntPtr handle, string shortcutsFolder, bool refreshOnTop)
        {
            JumpList jumpList = JumpList.CreateJumpListForIndividualWindow(TaskbarManager.Instance.ApplicationId, handle);

            if (refreshOnTop)
            {
                jumpList.AddUserTasks(new JumpListLink(Application.ExecutablePath, "Refresh jump list")
                    {
                        Arguments = BuildCommand("--dir", shortcutsFolder, "--refresh", "--pinned"),
                        WorkingDirectory = Directory.GetCurrentDirectory(),
                    });
                jumpList.AddUserTasks(new JumpListSeparator());
            }

            jumpList.AddUserTasks(ShortcutsToLinks(shortcutsFolder).ToArray());

            if (!refreshOnTop)
            {
                jumpList.AddUserTasks(new JumpListSeparator());
                jumpList.AddUserTasks(new JumpListLink(Application.ExecutablePath, "Refresh jump list")
                    {
                        Arguments = BuildCommand("--dir", shortcutsFolder, "--refresh", "--pinned", "--bottom"),
                        WorkingDirectory = Directory.GetCurrentDirectory(),
                    });
            }

            jumpList.Refresh();
        }
        private void dropDefault_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshCommand();
        }

    }
}
