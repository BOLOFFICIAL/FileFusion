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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            splitContainer = new SplitContainer();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            allFiles = new CheckBox();
            filesTreeView = new TreeView();
            selectFolder = new Button();
            saveToFile = new Button();
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
            splitContainer.Location = new Point(0, 0);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(tableLayoutPanel1);
            splitContainer.Panel1MinSize = 300;
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(content);
            splitContainer.Panel2MinSize = 300;
            splitContainer.Size = new Size(900, 500);
            splitContainer.SplitterDistance = 400;
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
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(5, 3, 0, 3);
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(400, 500);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(allFiles, 0, 1);
            tableLayoutPanel2.Controls.Add(filesTreeView, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(8, 37);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(389, 421);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // allFiles
            // 
            allFiles.AutoSize = true;
            allFiles.Dock = DockStyle.Fill;
            allFiles.Location = new Point(3, 399);
            allFiles.Name = "allFiles";
            allFiles.Size = new Size(383, 19);
            allFiles.TabIndex = 0;
            allFiles.Text = "Выбрать все файлы";
            allFiles.UseVisualStyleBackColor = true;
            allFiles.CheckedChanged += allFiles_CheckedChanged;
            // 
            // filesTreeView
            // 
            filesTreeView.CheckBoxes = true;
            filesTreeView.Dock = DockStyle.Fill;
            filesTreeView.Location = new Point(0, 0);
            filesTreeView.Margin = new Padding(0);
            filesTreeView.Name = "filesTreeView";
            filesTreeView.Size = new Size(389, 396);
            filesTreeView.TabIndex = 1;
            filesTreeView.AfterCheck += filesTreeView_AfterCheck;
            // 
            // selectFolder
            // 
            selectFolder.AutoSize = true;
            selectFolder.Dock = DockStyle.Fill;
            selectFolder.Location = new Point(8, 6);
            selectFolder.Name = "selectFolder";
            selectFolder.Size = new Size(389, 25);
            selectFolder.TabIndex = 4;
            selectFolder.Text = "Выбрать папку";
            selectFolder.UseVisualStyleBackColor = true;
            selectFolder.Click += selectFolder_Click;
            // 
            // saveToFile
            // 
            saveToFile.AutoSize = true;
            saveToFile.Dock = DockStyle.Fill;
            saveToFile.Location = new Point(8, 464);
            saveToFile.MaximumSize = new Size(0, 30);
            saveToFile.MinimumSize = new Size(0, 30);
            saveToFile.Name = "saveToFile";
            saveToFile.Size = new Size(389, 30);
            saveToFile.TabIndex = 5;
            saveToFile.Text = "Сохранить в файл";
            saveToFile.UseVisualStyleBackColor = true;
            saveToFile.Click += saveToFile_Click;
            // 
            // content
            // 
            content.Dock = DockStyle.Fill;
            content.Location = new Point(0, 0);
            content.Margin = new Padding(0);
            content.Name = "content";
            content.Size = new Size(495, 500);
            content.TabIndex = 1;
            content.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 500);
            Controls.Add(splitContainer);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "FileFusion - Объединение файлов";
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

        #endregion

        private SplitContainer splitContainer;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private CheckBox allFiles;
        private TreeView filesTreeView;
        private Button selectFolder;
        private Button saveToFile;
        private RichTextBox content;
    }
}