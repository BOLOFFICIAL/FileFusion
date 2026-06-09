using System.Text;

namespace FileFusion
{
    public partial class MainForm : Form
    {
        private bool _updatingTree = false;
        private CancellationTokenSource _cancellationTokenSource;
        private string _currentFolder;

        public MainForm()
        {
            InitializeComponent();
        }

        #region File Tree Building

        private void selectFolder_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Выберите папку с файлами";
                folderDialog.ShowNewFolderButton = true;
                folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    _currentFolder = folderDialog.SelectedPath;
                    BuildFileTree(_currentFolder);
                    this.Text = $"📁 FileFusion — {_currentFolder}";
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

        #endregion

        #region Tree Checkbox Logic

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

        private void Button_MouseEnter(object sender, EventArgs e) =>
            ((Button)sender).BackColor = Color.FromArgb(16, 110, 190);

        private void Button_MouseLeave(object sender, EventArgs e) =>
            ((Button)sender).BackColor = Color.FromArgb(0, 120, 215);

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

        #endregion

        #region Content Processing

        private async void UpdateContent()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            selectFolder.Enabled = false;
            selectFolder.Visible = false;
            saveToFile.Enabled = false;
            saveToFile.Visible = false;
            allFiles.Enabled = false;
            allFiles.Visible = false;
            filesTreeView.Enabled = false;

            progressBar.Visible = true;
            progressBar.Value = 0;

            content.Text = "";

            var selectedFiles = GetCheckedFiles();

            try
            {
                var progress = new Progress<(int current, int total, string fileName)>(
                    update =>
                    {
                        if (!token.IsCancellationRequested)
                        {
                            int percent = (update.current * 100) / update.total;
                            progressBar.Value = Math.Min(percent, 100);
                            this.Text = $"📄 FileFusion — {update.current}/{update.total} — {Path.GetFileName(update.fileName)}";
                        }
                    });

                var contentProgress = new Progress<string>(
                    contentChunk =>
                    {
                        if (!token.IsCancellationRequested)
                        {
                            content.AppendText(contentChunk);
                            content.SelectionStart = content.Text.Length;
                            content.ScrollToCaret();
                        }
                    });

                var result = await Task.Run(() => ProcessFilesAsync(selectedFiles, progress, contentProgress, token), token);

                if (result.Cancelled)
                {
                    MessageBox.Show("Операция была отменена пользователем.", "Отмена",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (result.Errors.Count > 0)
                {
                    MessageBox.Show($"Не удалось прочитать файлы:\n{string.Join("\n", result.Errors)}",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (result.MaxCapacityExceeded)
                {
                    MessageBox.Show("Достигнут максимальный размер содержимого. Операция прервана.",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Операция была отменена.", "Отмена",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обработке файлов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                FinishUpdate();
            }
        }

        private async Task<(List<string> Errors, bool MaxCapacityExceeded, bool Cancelled)> ProcessFilesAsync(
            List<string> files,
            IProgress<(int current, int total, string fileName)> progress,
            IProgress<string> contentProgress,
            CancellationToken token)
        {
            var errorList = new List<string>();
            bool maxCapacityExceeded = false;
            bool cancelled = false;
            int processed = 0;
            int total = files.Count;

            foreach (string file in files)
            {
                if (token.IsCancellationRequested)
                {
                    cancelled = true;
                    break;
                }

                progress.Report((processed + 1, total, file));

                var fileContent = GetFileContent(file);

                if (fileContent is null)
                {
                    errorList.Add(file);
                    processed++;
                    continue;
                }

                if (fileContent.Length > 0)
                {
                    var builder = new StringBuilder();

                    builder.AppendLine($"// ========================================");
                    builder.AppendLine($"// FILE: {file}");
                    builder.AppendLine($"// ========================================");
                    builder.AppendLine();
                    builder.AppendLine(fileContent);
                    builder.AppendLine($"\n// Конец файла: {file}\n");

                    contentProgress.Report(builder.ToString());
                }

                processed++;
            }

            return (errorList, maxCapacityExceeded, cancelled);
        }

        private string? GetFileContent(string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                    return null;

                return File.ReadAllText(filePath);
            }
            catch
            {
                return null;
            }
        }

        private void FinishUpdate()
        {
            selectFolder.Enabled = true;
            selectFolder.Visible = true;
            saveToFile.Enabled = true;
            saveToFile.Visible = true;
            allFiles.Enabled = true;
            allFiles.Visible = true;
            filesTreeView.Enabled = true;

            progressBar.Visible = false;

            this.Text = "📄 FileFusion — Объединение файлов";
        }

        #endregion

        #region Save to File

        private void saveToFile_Click(object sender, EventArgs e)
        {
            SaveDataToFile(content.Text);
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
                saveFileDialog.FileName = $"FileFusion_{Path.GetFileName(_currentFolder)}";
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

        #endregion
    }
}