using System.Text;

namespace FileFusion
{
    public partial class MainForm : Form
    {
        private Dictionary<string, List<string>> _contextFiles;
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
                    var folder = folderDialog.SelectedPath;
                    BuildFileTree(folder);
                    this.Text = $"FileFusion - {folder}";
                }
            }
        }

        private void BuildFileTree(string rootPath)
        {
            try
            {
                _updatingTree = true;
                filesTreeView.Nodes.Clear();
                content.Text = "";
                var rootNode = new TreeNode(Path.GetFileName(rootPath));
                rootNode.Tag = rootPath;

                AddDirectoryNodes(rootNode, rootPath);

                filesTreeView.Nodes.Add(rootNode);
                rootNode.Expand(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при построении дерева: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _updatingTree = false;
            }
        }

        private void AddDirectoryNodes(TreeNode parentNode, string path)
        {
            try
            {
                var directories = Directory.GetDirectories(path);
                foreach (var dir in directories)
                {
                    var dirName = Path.GetFileName(dir);
                    var dirNode = new TreeNode(dirName);
                    dirNode.Tag = dir;

                    AddDirectoryNodes(dirNode, dir);

                    parentNode.Nodes.Add(dirNode);
                }

                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    var fileNode = new TreeNode(fileName);
                    fileNode.Tag = file;
                    parentNode.Nodes.Add(fileNode);
                }
            }
            catch (UnauthorizedAccessException)
            {
                var noAccessNode = new TreeNode("[Нет доступа]");
                parentNode.Nodes.Add(noAccessNode);
            }
            catch (Exception ex)
            {
                var errorNode = new TreeNode($"[Ошибка: {ex.Message}]");
                parentNode.Nodes.Add(errorNode);
            }
        }

        private void saveToFile_Click(object sender, EventArgs e)
        {
            SaveDataToFile(content.Text);
        }

        private void filesTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (_updatingTree) return;

            _updatingTree = true;
            CheckAllChildNodes(e.Node, e.Node.Checked);
            UpdateParentNodes(e.Node);

            _updatingTree = false;
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
                    treeNode.Parent.Checked = true;

                UpdateParentNodes(treeNode.Parent);
            }
        }

        private void allFiles_CheckedChanged(object sender, EventArgs e)
        {
            if (_updatingTree) return;

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

                if (fileContent is null)
                {
                    errorList.Add(file);
                    continue;
                }

                if (builder.Length + fileContent.Length > builder.MaxCapacity)
                {
                    MessageBox.Show("Достигнут максимальный размер содержимого");
                    return;
                }

                if (fileContent.Length > 0)
                {
                    builder.AppendLine($"// ========================================");
                    builder.AppendLine($"// FILE: {file}");
                    builder.AppendLine($"// ========================================");
                    builder.AppendLine();
                    builder.AppendLine(fileContent);
                    builder.AppendLine($"\n// Конец файла: {file}\n");
                }
            }

            if (errorList.Count > 0)
            {
                MessageBox.Show($"Не удалось прочитать файлы:\n{string.Join("\n", errorList)}");
            }

            if (builder.Length > content.MaxLength)
            {
                if (MessageBox.Show("Содержимое слишком большое, сохранить в файл?",
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
                saveFileDialog.Title = "Сохранить содержимое";
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