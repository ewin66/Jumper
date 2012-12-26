using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Jumper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            TaskbarManager.Instance.ApplicationId = "JumperDebug";

            bool refreshOnTop = true;
            string shortcutDir = null;

            if (args.Contains("--bottom"))
            {
                refreshOnTop = false;
            }

            string launchPath = GetArgValue(args, "--launch");
            shortcutDir = GetArgValue(args, "--dir");

            if (args.Contains("--refresh"))
            {
                TaskbarManager.Instance.ApplicationId = "Jumper@" + shortcutDir;
                JumpForm.RefreshJumpList(IntPtr.Zero, shortcutDir, refreshOnTop);
                return;
            }

            if (!String.IsNullOrEmpty(launchPath))
            {
                Launch(launchPath);
            }
            else
            {
                JumpForm mainForm = new JumpForm();
                mainForm.RefreshOnTop = refreshOnTop;
                if (shortcutDir == null && args.Length == 2)
                    shortcutDir = args[1];
                mainForm.ShortcutsFolder = Directory.Exists(shortcutDir) ? shortcutDir : null;

                Application.Run(mainForm);
            }
        }

        private static string GetArgValue(string[] args, string key)
        {
            return args.Where((a, index) => index != 0 && args[index - 1] == key).FirstOrDefault();
        }

        private static void Launch(string launchPath)
        {
            ProcessStartInfo startInfo = null;

            if (launchPath.EndsWith(".lnk"))
            {
                startInfo = LaunchShortcut(launchPath);
            }
            else
            {
                startInfo = new ProcessStartInfo(launchPath);
            }

            if (startInfo != null)
            {
                Process.Start(startInfo);
            }
        }

        private static ProcessStartInfo LaunchShortcut(string shortcutPath)
        {
            Shell32.Shell shell = new Shell32.ShellClass();
            Shell32.Folder folder = shell.NameSpace(Path.GetDirectoryName(shortcutPath));

            bool runAs = IsMarkedRunAs(shortcutPath);

            string filenameOnly = System.IO.Path.GetFileName(shortcutPath);
            Shell32.FolderItem folderItem = folder.ParseName(filenameOnly);

            if (folderItem != null)
            {
                Shell32.ShellLinkObject link = (Shell32.ShellLinkObject)folderItem.GetLink;

                ProcessStartInfo info = new ProcessStartInfo(link.Path, link.Arguments)
                    {
                        WorkingDirectory = link.WorkingDirectory,
                    };

                if (runAs)
                    info.Verb = "runas";

                return info;
            }

            return null;
        }

        internal static bool IsMarkedRunAs(string shortcutFile)
        {
            Type shellLinkType = Type.GetTypeFromCLSID(new Guid(ShellIIDGuid.CShellLink), true);
            IPersistFile shellFile = Activator.CreateInstance(shellLinkType) as IPersistFile;

            if (shellFile != null)
            {
                shellFile.Load(shortcutFile, (int)AccessModes.Read);
                IShellLinkDataList shellLink = (IShellLinkDataList)shellFile;
                int flags;
                shellLink.GetFlags(out flags);

                return (flags & Convert.ToInt32(ShellLinkDataFlags.RunAs)) != 0;
            }

            return false;
        }

    }
}
