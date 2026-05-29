using System.Text;

namespace FileFusion
{
    public partial class MainForm : Form
    {
        private Dictionary<string, List<string>> _contextFiles;
        private Dictionary<string, TreeNode> _extensionNodes = new Dictionary<string, TreeNode>();
        private bool _updatingTree = false;

        public MainForm()
        {
            InitializeComponent();
            ConfigureUi();
        }

        private void ConfigureUi()
        {
            BackColor = Color.FromArgb(30, 30, 30);
            ForeColor = Color.White;

            content.BackColor = Color.FromArgb(37, 37, 38);
            content.ForeColor = Color.White;
            content.Font = new Font("Consolas", 10);

            extensions.BackColor = Color.FromArgb(45, 45, 48);
            extensions.ForeColor = Color.White;

            filesTreeView.BackColor = Color.FromArgb(45, 45, 48);
            filesTreeView.ForeColor = Color.White;

            foreach (Control control in Controls)
            {
                ApplyDarkTheme(control);
            }
        }

        private void ApplyDarkTheme(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = Color.FromArgb(0, 122, 204);
                    button.ForeColor = Color.White;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    button.Cursor = Cursors.Hand;
                }

                if (control is GroupBox groupBox)
                {
                    groupBox.ForeColor = Color.White;
                }

                ApplyDarkTheme(control);
            }
        }

        private void selectFolder_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Выберите папку с файлами";
                folderDialog.ShowNewFolderButton = true;
                folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    FillExtensions(folderDialog.SelectedPath);
                }
            }
        }

        private void saveToFile_Click(object sender, EventArgs e)
        {
            SaveDataToFile(content.Text);
        }

        private void allExtensions_CheckedChanged(object sender, EventArgs e)
        {
            content.Text = "";
            extensions.ItemCheck -= extensions_ItemCheck;

            var isCheck = allExtensions.Checked;

            if (!isCheck)
            {
                allFiles.Checked = isCheck;
            }

            for (int i = 0; i < extensions.Items.Count; i++)
            {
                if (i == extensions.Items.Count - 1)
                {
                    extensions.ItemCheck += extensions_ItemCheck;
                }

                extensions.SetItemChecked(i, isCheck);
            }
        }

        private void extensions_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                UpdateTreeView();
                allFiles.Checked = false;
                content.Text = "";
            }));
        }

        private void UpdateTreeView()
        {
            _updatingTree = true;
            filesTreeView.Nodes.Clear();
            _extensionNodes.Clear();

            foreach (string ex in extensions.CheckedItems)
            {
                if (_contextFiles.ContainsKey(ex))
                {
                    var extensionNode = new TreeNode(ex.TrimStart('.'));
                    extensionNode.Tag = ex;
                    _extensionNodes[ex] = extensionNode;

                    // Группируем файлы по папкам
                    var filesByFolder = _contextFiles[ex]
                        .GroupBy(f => Path.GetDirectoryName(f))
                        .OrderBy(g => g.Key);

                    foreach (var folderGroup in filesByFolder)
                    {
                        var folderName = folderGroup.Key;
                        var folderNode = new TreeNode(folderName ?? "Root");
                        folderNode.Tag = folderName;

                        foreach (string file in folderGroup.OrderBy(f => f))
                        {
                            var fileNode = new TreeNode(Path.GetFileName(file));
                            fileNode.Tag = file; // Полный путь к файлу
                            folderNode.Nodes.Add(fileNode);
                        }

                        extensionNode.Nodes.Add(folderNode);
                    }

                    filesTreeView.Nodes.Add(extensionNode);
                }
            }

            _updatingTree = false;
        }

        private void filesTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (_updatingTree) return;

            _updatingTree = true;

            // Обновляем дочерние узлы
            CheckAllChildNodes(e.Node, e.Node.Checked);

            // Обновляем родительские узлы
            UpdateParentNodes(e.Node);

            _updatingTree = false;

            // Обновляем контент
            UpdateContent();
        }

        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    CheckAllChildNodes(node, nodeChecked);
                }
            }
        }

        private void UpdateParentNodes(TreeNode treeNode)
        {
            if (treeNode.Parent != null)
            {
                bool allChecked = true;
                bool anyChecked = false;

                foreach (TreeNode node in treeNode.Parent.Nodes)
                {
                    if (node.Checked)
                        anyChecked = true;
                    else
                        allChecked = false;
                }

                if (allChecked)
                    treeNode.Parent.Checked = true;
                else if (!anyChecked)
                    treeNode.Parent.Checked = false;
                else
                    treeNode.Parent.Checked = true; // Промежуточное состояние

                UpdateParentNodes(treeNode.Parent);
            }
        }

        private void allFiles_CheckedChanged(object sender, EventArgs e)
        {
            _updatingTree = true;

            foreach (TreeNode node in filesTreeView.Nodes)
            {
                node.Checked = allFiles.Checked;
                CheckAllChildNodes(node, allFiles.Checked);
            }

            _updatingTree = false;
            UpdateContent();
        }

        private void UpdateContent()
        {
            content.Text = "";
            var errorList = new List<string>();
            var builder = new StringBuilder();
            var selectedFiles = GetCheckedFiles();

            foreach (string file in selectedFiles)
            {
                var fileContent = GetFileContent(file);

                if (builder.Length + fileContent.Length > builder.MaxCapacity)
                {
                    MessageBox.Show("Достигнут максимальный размер контента");
                    return;
                }

                if (fileContent is null)
                {
                    errorList.Add(file);
                    continue;
                }

                if (fileContent.Length > 0)
                {
                    builder.AppendLine($"// ========================================");
                    builder.AppendLine($"// FILE: {file}");
                    builder.AppendLine($"// ========================================");
                    builder.AppendLine(fileContent);
                    builder.AppendLine($"\n// Конец файла: {file}\n");
                }
            }

            if (errorList.Count > 0)
            {
                MessageBox.Show($"Не удалось прочитать следующие файлы:\n{string.Join("\n", errorList)}");
            }

            if (builder.Length > content.MaxLength)
            {
                if (MessageBox.Show("Контент превышает максимальный размер, сохранить в файл?",
                    "Предупреждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveDataToFile(builder.ToString());
                    return;
                }
            }
            content.Text = builder.ToString();
        }

        private List<string> GetCheckedFiles()
        {
            var checkedFiles = new List<string>();
            GetCheckedFilesRecursive(filesTreeView.Nodes, checkedFiles);
            return checkedFiles;
        }

        private void GetCheckedFilesRecursive(TreeNodeCollection nodes, List<string> checkedFiles)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked && node.Tag != null)
                {
                    // Если это файл (нет дочерних узлов)
                    if (node.Nodes.Count == 0 && File.Exists(node.Tag.ToString()))
                    {
                        checkedFiles.Add(node.Tag.ToString());
                    }
                }

                if (node.Nodes.Count > 0)
                {
                    GetCheckedFilesRecursive(node.Nodes, checkedFiles);
                }
            }
        }

        private void FillExtensions(string folder)
        {
            try
            {
                var allFiles = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories).ToList();
                _contextFiles = GroupFilesByExtension(allFiles);

                extensions.Items.Clear();
                foreach (var ex in _contextFiles.Keys)
                {
                    extensions.Items.Add(ex);
                }

                filesTreeView.Nodes.Clear();
                _extensionNodes.Clear();
                content.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сканировании: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Dictionary<string, List<string>> GroupFilesByExtension(List<string> filePaths)
        {
            return filePaths
                .GroupBy(file => Path.GetExtension(file).ToLower())
                .OrderBy(ex => ex.Key)
                .ToDictionary(
                    group => group.Key,
                    group => group.ToList()
                );
        }

        private string? GetFileContent(string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath))
                    return null;

                if (!File.Exists(filePath))
                {
                    return null;
                }

                return File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void SaveDataToFile(string saveText)
        {
            if (string.IsNullOrEmpty(saveText))
            {
                MessageBox.Show("Нет данных для сохранения", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                saveFileDialog.Title = "Сохранить файл";
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.FileName = $"FileFusion_{DateTime.Now:yyyyMMddHHmmss}";
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveFileDialog.FileName, saveText, Encoding.UTF8);
                        MessageBox.Show("Файл успешно сохранен!", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}