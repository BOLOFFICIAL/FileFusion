namespace FileFusion
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            splitContainer = new SplitContainer();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            allFiles = new CheckBox();
            filesTreeView = new TreeView();
            selectFolder = new Button();
            saveToFile = new Button();
            progressBar = new ProgressBar();
            content = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(5, 5);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(tableLayoutPanel1);
            splitContainer.Panel1MinSize = 280;
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(content);
            splitContainer.Panel2MinSize = 320;
            splitContainer.Size = new Size(970, 630);
            splitContainer.SplitterDistance = 375;
            splitContainer.SplitterWidth = 5;
            splitContainer.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(selectFolder, 0, 0);
            tableLayoutPanel1.Controls.Add(saveToFile, 0, 2);
            tableLayoutPanel1.Controls.Add(progressBar, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(375, 630);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(allFiles, 0, 1);
            tableLayoutPanel2.Controls.Add(filesTreeView, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 37);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(375, 554);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // allFiles
            // 
            allFiles.AutoSize = true;
            allFiles.Dock = DockStyle.Fill;
            allFiles.Location = new Point(0, 530);
            allFiles.Margin = new Padding(0);
            allFiles.Name = "allFiles";
            allFiles.Size = new Size(375, 24);
            allFiles.TabIndex = 0;
            allFiles.Text = "Выбрать все файлы";
            allFiles.UseVisualStyleBackColor = true;
            allFiles.CheckedChanged += allFiles_CheckedChanged;
            // 
            // filesTreeView
            // 
            filesTreeView.BorderStyle = BorderStyle.FixedSingle;
            filesTreeView.CheckBoxes = true;
            filesTreeView.Dock = DockStyle.Fill;
            filesTreeView.Location = new Point(0, 0);
            filesTreeView.Margin = new Padding(0);
            filesTreeView.Name = "filesTreeView";
            filesTreeView.Size = new Size(375, 530);
            filesTreeView.TabIndex = 1;
            filesTreeView.AfterCheck += filesTreeView_AfterCheck;
            // 
            // selectFolder
            // 
            selectFolder.AutoSize = true;
            selectFolder.Dock = DockStyle.Fill;
            selectFolder.FlatStyle = FlatStyle.Flat;
            selectFolder.Location = new Point(0, 0);
            selectFolder.Margin = new Padding(0, 0, 0, 5);
            selectFolder.Name = "selectFolder";
            selectFolder.Size = new Size(375, 32);
            selectFolder.TabIndex = 4;
            selectFolder.Text = "Выбрать папку";
            selectFolder.UseVisualStyleBackColor = false;
            selectFolder.Click += selectFolder_Click;
            // 
            // saveToFile
            // 
            saveToFile.Dock = DockStyle.Fill;
            saveToFile.FlatStyle = FlatStyle.Flat;
            saveToFile.Location = new Point(0, 591);
            saveToFile.Margin = new Padding(0);
            saveToFile.Name = "saveToFile";
            saveToFile.Size = new Size(375, 30);
            saveToFile.TabIndex = 5;
            saveToFile.Text = "Сохранить в файл";
            saveToFile.UseVisualStyleBackColor = false;
            saveToFile.Click += saveToFile_Click;
            // 
            // progressBar
            // 
            progressBar.Dock = DockStyle.Fill;
            progressBar.ForeColor = Color.FromArgb(0, 192, 0);
            progressBar.Location = new Point(0, 626);
            progressBar.Margin = new Padding(0, 5, 0, 0);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(375, 4);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 6;
            progressBar.Visible = false;
            // 
            // content
            // 
            content.BorderStyle = BorderStyle.FixedSingle;
            content.Dock = DockStyle.Fill;
            content.Location = new Point(0, 0);
            content.Name = "content";
            content.Size = new Size(590, 630);
            content.TabIndex = 1;
            content.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(980, 640);
            Controls.Add(splitContainer);
            Font = new Font("Segoe UI", 11F);
            MinimumSize = new Size(760, 540);
            Name = "MainForm";
            Padding = new Padding(5);
            Text = "FileFusion";
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        private SplitContainer splitContainer;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private CheckBox allFiles;
        private TreeView filesTreeView;
        private Button selectFolder;
        private Button saveToFile;
        private RichTextBox content;
        private ProgressBar progressBar;
    }
}