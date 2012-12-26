namespace Jumper
{
    partial class JumpForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JumpForm));
            this.button1 = new System.Windows.Forms.Button();
            this._label = new System.Windows.Forms.Label();
            this.textFolder = new System.Windows.Forms.TextBox();
            this.listJumpItems = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dropDefault = new System.Windows.Forms.ComboBox();
            this.browseDefault = new System.Windows.Forms.Button();
            this.browseFolder = new System.Windows.Forms.Button();
            this.command = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(435, 311);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // _label
            // 
            this._label.AutoSize = true;
            this._label.Location = new System.Drawing.Point(12, 9);
            this._label.Name = "_label";
            this._label.Size = new System.Drawing.Size(316, 13);
            this._label.TabIndex = 2;
            this._label.Text = "Choose a folder with shortcuts you want to access from a jump list";
            // 
            // textFolder
            // 
            this.textFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textFolder.Location = new System.Drawing.Point(12, 25);
            this.textFolder.Name = "textFolder";
            this.textFolder.ReadOnly = true;
            this.textFolder.Size = new System.Drawing.Size(468, 20);
            this.textFolder.TabIndex = 3;
            // 
            // listJumpItems
            // 
            this.listJumpItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listJumpItems.Location = new System.Drawing.Point(12, 64);
            this.listJumpItems.Name = "listJumpItems";
            this.listJumpItems.Size = new System.Drawing.Size(495, 142);
            this.listJumpItems.TabIndex = 5;
            this.listJumpItems.UseCompatibleStateImageBehavior = false;
            this.listJumpItems.View = System.Windows.Forms.View.List;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Jump list items";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Default application to run";
            // 
            // dropDefault
            // 
            this.dropDefault.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dropDefault.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropDefault.FormattingEnabled = true;
            this.dropDefault.Location = new System.Drawing.Point(12, 225);
            this.dropDefault.Name = "dropDefault";
            this.dropDefault.Size = new System.Drawing.Size(468, 21);
            this.dropDefault.TabIndex = 8;
            this.dropDefault.SelectedIndexChanged += new System.EventHandler(this.dropDefault_SelectedIndexChanged);
            // 
            // browseDefault
            // 
            this.browseDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.browseDefault.Location = new System.Drawing.Point(486, 224);
            this.browseDefault.Name = "browseDefault";
            this.browseDefault.Size = new System.Drawing.Size(24, 23);
            this.browseDefault.TabIndex = 4;
            this.browseDefault.Text = "...";
            this.browseDefault.UseVisualStyleBackColor = true;
            // 
            // browseFolder
            // 
            this.browseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseFolder.Location = new System.Drawing.Point(486, 23);
            this.browseFolder.Name = "browseFolder";
            this.browseFolder.Size = new System.Drawing.Size(24, 23);
            this.browseFolder.TabIndex = 4;
            this.browseFolder.Text = "...";
            this.browseFolder.UseVisualStyleBackColor = true;
            this.browseFolder.Click += new System.EventHandler(this.browseFolder_Click);
            // 
            // command
            // 
            this.command.Location = new System.Drawing.Point(12, 252);
            this.command.Multiline = true;
            this.command.Name = "command";
            this.command.ReadOnly = true;
            this.command.Size = new System.Drawing.Size(498, 53);
            this.command.TabIndex = 9;
            // 
            // JumpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 346);
            this.Controls.Add(this.command);
            this.Controls.Add(this.dropDefault);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listJumpItems);
            this.Controls.Add(this.browseDefault);
            this.Controls.Add(this.browseFolder);
            this.Controls.Add(this.textFolder);
            this.Controls.Add(this._label);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "JumpForm";
            this.Text = "Jumper";
            this.Load += new System.EventHandler(this.JumpForm_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label _label;
        private System.Windows.Forms.TextBox textFolder;
        private System.Windows.Forms.ListView listJumpItems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox dropDefault;
        private System.Windows.Forms.Button browseDefault;
        private System.Windows.Forms.Button browseFolder;
        private System.Windows.Forms.TextBox command;
    }
}

