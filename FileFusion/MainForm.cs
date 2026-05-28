using System.Text;

namespace FileFusion
{
    public partial class MainForm : Form
    {
        private Dictionary<string, List<string>> _contextFiles;

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

            files.BackColor = Color.FromArgb(45, 45, 48);
            files.ForeColor = Color.White;

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
                folderDialog.Description = "Выберите папку";
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
                files.Items.Clear();

                foreach (string ex in extensions.CheckedItems)
                {
                    if (_contextFiles.ContainsKey(ex))
                    {
                        foreach (string el in _contextFiles[ex])
                        {
                            files.Items.Add(el);
                        }
                    }
                }

                allFiles.Checked = false;
            }));
        }

        private void allFiles_CheckedChanged(object sender, EventArgs e)
        {
            files.ItemCheck -= files_ItemCheck;

            for (int i = 0; i < files.Items.Count; i++)
            {
                if (i == files.Items.Count - 1)
                {
                    files.ItemCheck += files_ItemCheck;
                }

                files.SetItemChecked(i, allFiles.Checked);
            }
        }

        private void files_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                content.Text = "";
                var errorList = new List<string>();
                var builder = new StringBuilder();

                foreach (string file in files.CheckedItems)
                {
                    var fileContent = GetFileContent(file);

                    if (builder.Length + fileContent.Length > builder.MaxCapacity)
                    {
                        MessageBox.Show($"Место закончилось");
                        return;
                    }

                    if (fileContent is null)
                    {
                        errorList.Add(file);
                    }

                    if (fileContent.Length > 0)
                    {
                        builder.AppendLine($"// ========================================");
                        builder.AppendLine($"// FILE: {file}");
                        builder.AppendLine($"// ========================================");
                        builder.AppendLine(fileContent);
                        builder.AppendLine($"\n//Конец файла: {file}\n");
                    }
                }

                if (errorList.Count > 0)
                {
                    MessageBox.Show($"Не удалось получить содержимое следующих файлов:\n{string.Join("\n", errorList)}");
                }

                if (builder.Length > content.MaxLength)
                {
                    if (MessageBox.Show("Данных слишком много, сохранить их сразу в фаил?",
                        "",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SaveDataToFile(builder.ToString());
                        return;
                    }
                }
                content.Text = builder.ToString();
            }));
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

                files.Items.Clear();

                content.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($"Нет данных для сохранения", "Ошибка",
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
                        MessageBox.Show("Файл успешно сохранен!", "",
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
