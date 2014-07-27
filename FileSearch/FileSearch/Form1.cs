﻿//В итоге реализовать удалось большую часть задачи, однако из-за отсутствия работы в реальном времени
//динамическая часть программы была утеряна (обработка текущего файла и возможность остановки работы).
//Были попытки реализовать через async-методы, но возникали проблемы доступа к элементам окна, поскольку
//обращение к ним происходило из другого потока. Invoke не помог, поскольку он блокирует поток. 
//BackgroundWorker тоже не помог, потому что изменение прогресса все время работало с нулевым значением
//самого прогресс-значения, и динамичность при этом терялась.
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

namespace FileSearch
{
    public partial class MainForm : Form
    {
        private int counter;//Счетчик числа рассмотренных файлов
        private Timer timer;//Секундомер
        public MainForm()
        {
            InitializeComponent();
            DirectoryPathText.Text = Properties.Settings.Default.DirectoryPath;
            NameTemplateText.Text = Properties.Settings.Default.TemplateText;
            FileContentText.Text = Properties.Settings.Default.FileContent;
            CounterText.Text = "0";
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
        private void StartButton_Click(object sender, EventArgs e)
        {
            StopButton.Enabled = true;
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
                ListDirectory();
                TimerText.Text = timer.TimerStop();
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
        //Остановка поиска. Из-за отсутствия работы в реальном времени смысла в данной кнопке нет
        private void StopButton_Click(object sender, EventArgs e)
        {
            treeView1.Refresh();
        }
        //Создание дерева результатов
        private void ListDirectory()
        {
            treeView1.Nodes.Clear();
            DirectoryInfo rootDirectoryInfo = new DirectoryInfo(DirectoryPathText.Text);
            treeView1.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
            treeView1.Nodes[0].Text = DirectoryPathText.Text;
            NodeCheck(treeView1.Nodes[0]);
        }
        //Добавление новых узлов в дерево рекурсивно
        private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            TreeNode directoryNode = new TreeNode(directoryInfo.Name);
            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            foreach (FileInfo file in directoryInfo.GetFiles(NameTemplateText.Text))
            {
                FileNameText.Text = file.FullName;
                CounterText.Text = (++counter).ToString();
                if (File.ReadAllText(file.FullName).Contains(FileContentText.Text))
                    directoryNode.Nodes.Add(new TreeNode(file.Name));
            }
            return directoryNode;
        }
        //Проверка узлов в дереве вложенности на лишние (избыточные) папки и их удаление
        private void NodeCheck(TreeNode node)
        {
            if (node.GetNodeCount(false) == 0)
            {
                FileAttributes fa = File.GetAttributes(node.FullPath);
                if ((fa & FileAttributes.Directory) == FileAttributes.Directory)
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