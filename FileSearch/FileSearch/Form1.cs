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
                        TreePopulate();
                    }, cts.Token);
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Поиск был прерван");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (treeView1.Nodes[0].Nodes.Count == 0)
                MessageBox.Show("Поиск не дал результатов");
        }
        //Обработка дерева. Поиск необходимых узлов и добавление новых в требуемых местах
        private void TreePopulate()
        {
            treeView1.BeginInvoke(new Action(() => treeView1.Nodes.Clear()));
            treeView1.BeginInvoke(new Action<string>((s) => treeView1.Nodes.Add(s)), DirectoryPathText.Text);
            DirectoryInfo DI = new DirectoryInfo(DirectoryPathText.Text);
            foreach (FileInfo file in DI.GetFiles(NameTemplateText.Text, SearchOption.AllDirectories))
            {
                cts.Token.ThrowIfCancellationRequested();
                FileNameText.BeginInvoke(new Action<string>((s) => FileNameText.Text = s), file.FullName);
                CounterText.BeginInvoke(new Action<string>((s) => CounterText.Text = s), (++counter).ToString());
                TimerText.BeginInvoke(new Action<string>((s) => TimerText.Text = s), timer.TimerStop());
                //Определение соответствия содержимого файла установленному требованию
                if (File.ReadAllText(file.FullName).Contains(FileContentText.Text))
                {
                    //Массив строк, содержаший элементы пути файла. Необходим для постепенного добавления узлов в дерево
                    //исключая повторения
                    string[] PathArray = file.FullName.Replace(DirectoryPathText.Text + "\\", "").Split(Path.DirectorySeparatorChar);
                    TreeNode node = treeView1.Nodes[0];
                    //Проверка наличия соответствующего пути узла в дереве
                    bool contains = true;
                    int index = 0;
                    //Если у узла нет потомков мы сразу добавляем весь путь
                    if (node.Nodes.Count == 0)
                        treeView1.BeginInvoke(new Action<TreeNode>((n) => n.Nodes.Add(NodeAdd(index, PathArray, n))), node);
                    else
                        //Рассматриваем поэтапно все узлы для выявления узла, соответствующего пути, но не имеющего потомков
                        while (contains)
                        {
                            //Логическая переменная нужна, чтобы определить, был ли найден узел, соответствующий пути
                            bool checker = false;
                            foreach (TreeNode nodes in node.Nodes)
                                //Проверка, соответствует ли какой-либо потомок пути у рассматриваемого узла.
                                //Если это так, то фокус переходит на этого потомка, а массив строк пути переходит на следующий элемент
                                if (nodes.Text == PathArray[index])
                                {
                                    index++;
                                    node = nodes;
                                    checker = true;
                                    break;
                                }
                            //Когда мы наткнемся на узел без потомков, мы добавляем оставшийся путь в дерево
                            if (!checker)
                            {
                                contains = false;
                                treeView1.BeginInvoke(new Action<TreeNode>((n) => n.Nodes.Add(NodeAdd(index, PathArray, n))), node);
                            }
                        }
                }
                Thread.Sleep(100);
            }
        }
        //Добавление узлов в дерево по пути
        private TreeNode NodeAdd (int index, string[] pathArray, TreeNode node)
        {
            TreeNode TN = new TreeNode(pathArray[index]);
            if (index != pathArray.Length - 1)
                TN.Nodes.Add(NodeAdd(++index, pathArray, TN));
            return TN;
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
        //Остановка поиска
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
