using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace FileSearch
{
    public partial class MainForm : Form
    {
        private int counter;//Счетчик числа рассмотренных файлов
        private Timer timer;//Секундомер
        CancellationTokenSource cts;//Токен для прерывания поиска
        public MainForm()
        {
            InitializeComponent();
            DirectoryPathText.Text = Properties.Settings.Default.DirectoryPath;
            NameTemplateText.Text = Properties.Settings.Default.TemplateText;
            FileContentText.Text = Properties.Settings.Default.FileContent;
            CounterText.Text = "0";
            treeView1.ContextMenuStrip = ContextForTree;
        }
        //Выбор папки поиска
        private void SelectDirectoryButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = DirectoryPathText.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                DirectoryPathText.Text = folderBrowserDialog1.SelectedPath;
        }
        //Сохранение настроек формы
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.DirectoryPath = DirectoryPathText.Text;
            Properties.Settings.Default.TemplateText = NameTemplateText.Text;
            Properties.Settings.Default.FileContent = FileContentText.Text;
            Properties.Settings.Default.Save();
        }
        //Выполнение поиска
        private async void StartButton_Click(object sender, EventArgs e)
        {
            counter = 0;
            if (DirectoryPathText.Text == "")
            {
                MessageBox.Show("Укажите папку для начала поиска");
                return;
            }
            if (NameTemplateText.Text == "")
            {
                MessageBox.Show("Укажите шаблон поиска");
                return;
            }
            try
            {
                timer = new Timer();
                cts = new CancellationTokenSource();
                await Task.Factory.StartNew(() =>
                    {
                        ListDirectory(true);
                    }, cts.Token);
                ListDirectory(false);
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Поиск был прерван");
            }
            catch (NullReferenceException)
            {
                FileNameText.Text = "Поиск не дал результатов";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Создание дерева результатов
        private void ListDirectory(bool _async)
        {
            if (_async)
            {
                treeView1.BeginInvoke(new Action(() => treeView1.Nodes.Clear()));
                treeView1.BeginInvoke(new Action<TreeNode>((n) => treeView1.Nodes.Add(n)), CreateDirectoryNode(new DirectoryInfo(DirectoryPathText.Text), 100, cts.Token));
            }
            else
            {
                counter = 0;
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(CreateDirectoryNode(new DirectoryInfo(DirectoryPathText.Text), 0, cts.Token));
                treeView1.Nodes[0].Text = DirectoryPathText.Text;
                NodeCheck(treeView1.Nodes[0]);
            }
        }
        //Добавление новых узлов в дерево рекурсивно
        private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo, int delay, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            TreeNode directoryNode = new TreeNode(directoryInfo.Name);
            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory, delay, token));
            foreach (FileInfo file in directoryInfo.GetFiles(NameTemplateText.Text))
            {
                FileNameText.BeginInvoke(new Action<string>((s) => FileNameText.Text = s), file.FullName);
                CounterText.BeginInvoke(new Action<string>((s) => CounterText.Text = s), (++counter).ToString());
                TimerText.BeginInvoke(new Action<string>((s) => TimerText.Text = s), timer.TimerStop());
                Thread.Sleep(delay);
                if (File.ReadAllText(file.FullName).Contains(FileContentText.Text))
                {
                    directoryNode.Nodes.Add(new TreeNode(file.Name));
                    treeView1.BeginInvoke(new Action(() => treeView1.Refresh()));
                }
            }
            return directoryNode;
        }
        //Проверка узлов в дереве вложенности на лишние (избыточные) папки и их удаление
        private void NodeCheck(TreeNode node)
        {
            if (node.GetNodeCount(false) == 0)
            {
                if (!File.Exists(node.FullPath))
                {
                    TreeNode parent;
                    parent = node.Parent;
                    treeView1.Nodes.Remove(node);
                    NodeCheck(parent);
                }
            }
            else
                foreach (TreeNode trNode in node.Nodes)
                    if (trNode != null)
                        NodeCheck(trNode);
        }
        //Запуск файлов из дерева результатов
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (!IsDirectory(treeView1.SelectedNode.FullPath))
                    Process.Start(treeView1.SelectedNode.FullPath);
            }
            catch (NullReferenceException) { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Открытие файла/папки из контекстного меню
        private void Option_Open_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(treeView1.SelectedNode.FullPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Удаление файла из контекстного меню
        private void Option_Delete_Click(object sender, EventArgs e)
        {
            if (IsDirectory(treeView1.SelectedNode.FullPath))
                MessageBox.Show("Невозможно удалить папку");
            else
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить " + treeView1.SelectedNode.FullPath, "Удаление", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    File.Delete(treeView1.SelectedNode.FullPath);
                NodeCheck(treeView1.Nodes[0]);
            }
        }
        //Обработчик клика правой кнопкой по дереву
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;
                ContextForTree.Show();
            }
        }
        //Проверка, является ли выбранный путь папкой
        private bool IsDirectory(string name)
        {
            FileAttributes fa = File.GetAttributes(name);
            return (fa & FileAttributes.Directory) == FileAttributes.Directory;
        }
        private void StopButton_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }
    }
    //Класс для запуска секундомера
    class Timer
    {
        public DateTime Time;
        public Timer()
        {
            Time = DateTime.Now;
        }
        public string TimerStop()
        {
            TimeSpan Dif = DateTime.Now - this.Time;
            return (Dif.Hours < 10 ? "0" + Dif.Hours.ToString() : Dif.Hours.ToString()) + ":" +
                            (Dif.Minutes < 10 ? "0" + Dif.Minutes.ToString() : Dif.Minutes.ToString()) + ":" +
                            (Dif.Seconds < 10 ? "0" + Dif.Seconds.ToString() : Dif.Seconds.ToString());
        }
    }
}
