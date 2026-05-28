namespace FileFusion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            groupBox2 = new GroupBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            allFiles = new CheckBox();
            files = new CheckedListBox();
            groupBox1 = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            allExtensions = new CheckBox();
            extensions = new CheckedListBox();
            selectFolder = new Button();
            saveToFile = new Button();
            content = new RichTextBox();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            groupBox2.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(content, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(groupBox2, 0, 2);
            tableLayoutPanel2.Controls.Add(groupBox1, 0, 1);
            tableLayoutPanel2.Controls.Add(selectFolder, 0, 0);
            tableLayoutPanel2.Controls.Add(saveToFile, 0, 3);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 39.9976F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 60.0024F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(300, 450);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(tableLayoutPanel4);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(3, 183);
            groupBox2.Margin = new Padding(3, 0, 3, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(294, 228);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Файлы";
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(allFiles, 0, 1);
            tableLayoutPanel4.Controls.Add(files, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 19);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.Size = new Size(288, 206);
            tableLayoutPanel4.TabIndex = 1;
            // 
            // allFiles
            // 
            allFiles.AutoSize = true;
            allFiles.Dock = DockStyle.Fill;
            allFiles.Location = new Point(3, 184);
            allFiles.Name = "allFiles";
            allFiles.Size = new Size(282, 19);
            allFiles.TabIndex = 0;
            allFiles.Text = "Все";
            allFiles.UseVisualStyleBackColor = true;
            allFiles.CheckedChanged += allFiles_CheckedChanged;
            // 
            // files
            // 
            files.Dock = DockStyle.Fill;
            files.FormattingEnabled = true;
            files.HorizontalScrollbar = true;
            files.Location = new Point(0, 0);
            files.Margin = new Padding(0);
            files.Name = "files";
            files.Size = new Size(288, 181);
            files.TabIndex = 1;
            files.ItemCheck += files_ItemCheck;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tableLayoutPanel3);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 31);
            groupBox1.Margin = new Padding(3, 0, 3, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(294, 152);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Расширения";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(allExtensions, 0, 1);
            tableLayoutPanel3.Controls.Add(extensions, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 19);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.Size = new Size(288, 130);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // allExtensions
            // 
            allExtensions.AutoSize = true;
            allExtensions.Dock = DockStyle.Fill;
            allExtensions.Location = new Point(3, 108);
            allExtensions.Name = "allExtensions";
            allExtensions.Size = new Size(282, 19);
            allExtensions.TabIndex = 0;
            allExtensions.Text = "Все";
            allExtensions.UseVisualStyleBackColor = true;
            allExtensions.CheckedChanged += allExtensions_CheckedChanged;
            // 
            // extensions
            // 
            extensions.Dock = DockStyle.Fill;
            extensions.FormattingEnabled = true;
            extensions.Location = new Point(0, 0);
            extensions.Margin = new Padding(0);
            extensions.Name = "extensions";
            extensions.Size = new Size(288, 105);
            extensions.TabIndex = 1;
            extensions.ItemCheck += extensions_ItemCheck;
            // 
            // selectFolder
            // 
            selectFolder.AutoSize = true;
            selectFolder.Dock = DockStyle.Fill;
            selectFolder.Location = new Point(3, 3);
            selectFolder.Name = "selectFolder";
            selectFolder.Size = new Size(294, 25);
            selectFolder.TabIndex = 4;
            selectFolder.Text = "Выбрать папку";
            selectFolder.UseVisualStyleBackColor = true;
            selectFolder.Click += selectFolder_Click;
            // 
            // saveToFile
            // 
            saveToFile.AutoSize = true;
            saveToFile.Dock = DockStyle.Fill;
            saveToFile.Location = new Point(3, 414);
            saveToFile.Name = "saveToFile";
            saveToFile.Size = new Size(294, 33);
            saveToFile.TabIndex = 5;
            saveToFile.Text = "Сохранить в фаил";
            saveToFile.UseVisualStyleBackColor = true;
            saveToFile.Click += saveToFile_Click;
            // 
            // content
            // 
            content.Dock = DockStyle.Fill;
            content.Location = new Point(300, 3);
            content.Margin = new Padding(0, 3, 3, 3);
            content.Name = "content";
            content.Size = new Size(497, 444);
            content.TabIndex = 1;
            content.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "FileFusion";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            groupBox2.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            groupBox1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel3;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private CheckBox allFiles;
        private CheckedListBox files;
        private CheckBox allExtensions;
        private CheckedListBox extensions;
        private Button selectFolder;
        private Button saveToFile;
        private RichTextBox content;
    }
}
