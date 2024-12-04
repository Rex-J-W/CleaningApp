namespace CleaningApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timer = new System.Windows.Forms.Timer(components);
            folderView = new ListView();
            directory = new ColumnHeader();
            size = new ColumnHeader();
            files = new ColumnHeader();
            upDirButton = new Button();
            drivesBox = new ComboBox();
            openButton = new Button();
            dirLabel = new Label();
            SuspendLayout();
            // 
            // timer
            // 
            timer.Enabled = true;
            timer.Interval = 3000;
            timer.Tick += timer_Tick;
            // 
            // folderView
            // 
            folderView.Columns.AddRange(new ColumnHeader[] { directory, size, files });
            folderView.FullRowSelect = true;
            folderView.GridLines = true;
            folderView.Location = new Point(10, 40);
            folderView.Margin = new Padding(1);
            folderView.MultiSelect = false;
            folderView.Name = "folderView";
            folderView.ShowGroups = false;
            folderView.Size = new Size(780, 452);
            folderView.TabIndex = 0;
            folderView.UseCompatibleStateImageBehavior = false;
            folderView.View = View.Details;
            folderView.SelectedIndexChanged += folderView_SelectedIndexChanged;
            // 
            // directory
            // 
            directory.Text = "Directory";
            directory.Width = 400;
            // 
            // size
            // 
            size.Text = "Size";
            size.Width = 150;
            // 
            // files
            // 
            files.Text = "Files";
            files.Width = 150;
            // 
            // upDirButton
            // 
            upDirButton.Location = new Point(10, 12);
            upDirButton.Name = "upDirButton";
            upDirButton.Size = new Size(21, 23);
            upDirButton.TabIndex = 1;
            upDirButton.Text = "^";
            upDirButton.UseVisualStyleBackColor = true;
            upDirButton.Click += upDirButton_Click;
            // 
            // drivesBox
            // 
            drivesBox.FormattingEnabled = true;
            drivesBox.Location = new Point(95, 13);
            drivesBox.Name = "drivesBox";
            drivesBox.Size = new Size(67, 23);
            drivesBox.TabIndex = 2;
            drivesBox.SelectedIndexChanged += drivesBox_SelectedIndexChanged;
            // 
            // openButton
            // 
            openButton.Location = new Point(37, 12);
            openButton.Name = "openButton";
            openButton.Size = new Size(52, 23);
            openButton.TabIndex = 3;
            openButton.Text = "Open";
            openButton.UseVisualStyleBackColor = true;
            openButton.Click += openButton_Click;
            // 
            // dirLabel
            // 
            dirLabel.AutoSize = true;
            dirLabel.Location = new Point(168, 16);
            dirLabel.Name = "dirLabel";
            dirLabel.Size = new Size(55, 15);
            dirLabel.TabIndex = 4;
            dirLabel.Text = "Directory";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 502);
            Controls.Add(dirLabel);
            Controls.Add(openButton);
            Controls.Add(drivesBox);
            Controls.Add(upDirButton);
            Controls.Add(folderView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Cleaner";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private ListView folderView;
        private ColumnHeader directory;
        private ColumnHeader size;
        private ColumnHeader files;
        private Button upDirButton;
        private ComboBox drivesBox;
        private Button openButton;
        private Label dirLabel;
    }
}
