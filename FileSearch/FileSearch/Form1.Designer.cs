namespace FileSearch
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.DirectoryPathText = new System.Windows.Forms.TextBox();
            this.DirectoryLabel = new System.Windows.Forms.Label();
            this.NameTemplateText = new System.Windows.Forms.TextBox();
            this.TemplateLabel = new System.Windows.Forms.Label();
            this.FileContentText = new System.Windows.Forms.TextBox();
            this.FileTextLabel = new System.Windows.Forms.Label();
            this.SelectDirectoryButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.StartButton = new System.Windows.Forms.Button();
            this.FileNameText = new System.Windows.Forms.TextBox();
            this.FileNameLabel = new System.Windows.Forms.Label();
            this.CountLabel = new System.Windows.Forms.Label();
            this.CounterText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TimerText = new System.Windows.Forms.TextBox();
            this.ContextForTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Option_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.Option_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextForTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // DirectoryPathText
            // 
            this.DirectoryPathText.Enabled = false;
            this.DirectoryPathText.Location = new System.Drawing.Point(15, 31);
            this.DirectoryPathText.Name = "DirectoryPathText";
            this.DirectoryPathText.Size = new System.Drawing.Size(347, 20);
            this.DirectoryPathText.TabIndex = 0;
            this.DirectoryPathText.TabStop = false;
            // 
            // DirectoryLabel
            // 
            this.DirectoryLabel.AutoSize = true;
            this.DirectoryLabel.Location = new System.Drawing.Point(15, 9);
            this.DirectoryLabel.Name = "DirectoryLabel";
            this.DirectoryLabel.Size = new System.Drawing.Size(78, 13);
            this.DirectoryLabel.TabIndex = 1;
            this.DirectoryLabel.Text = "Папка поиска";
            // 
            // NameTemplateText
            // 
            this.NameTemplateText.Location = new System.Drawing.Point(15, 70);
            this.NameTemplateText.Name = "NameTemplateText";
            this.NameTemplateText.Size = new System.Drawing.Size(347, 20);
            this.NameTemplateText.TabIndex = 1;
            // 
            // TemplateLabel
            // 
            this.TemplateLabel.AutoSize = true;
            this.TemplateLabel.Location = new System.Drawing.Point(15, 54);
            this.TemplateLabel.Name = "TemplateLabel";
            this.TemplateLabel.Size = new System.Drawing.Size(81, 13);
            this.TemplateLabel.TabIndex = 1;
            this.TemplateLabel.Text = "Шаблон имени";
            // 
            // FileContentText
            // 
            this.FileContentText.Location = new System.Drawing.Point(15, 109);
            this.FileContentText.Name = "FileContentText";
            this.FileContentText.Size = new System.Drawing.Size(347, 20);
            this.FileContentText.TabIndex = 2;
            // 
            // FileTextLabel
            // 
            this.FileTextLabel.AutoSize = true;
            this.FileTextLabel.Location = new System.Drawing.Point(15, 93);
            this.FileTextLabel.Name = "FileTextLabel";
            this.FileTextLabel.Size = new System.Drawing.Size(107, 13);
            this.FileTextLabel.TabIndex = 1;
            this.FileTextLabel.Text = "Содержимое файла";
            // 
            // SelectDirectoryButton
            // 
            this.SelectDirectoryButton.Location = new System.Drawing.Point(99, 4);
            this.SelectDirectoryButton.Name = "SelectDirectoryButton";
            this.SelectDirectoryButton.Size = new System.Drawing.Size(100, 23);
            this.SelectDirectoryButton.TabIndex = 0;
            this.SelectDirectoryButton.Text = "Выбрать папку";
            this.SelectDirectoryButton.UseVisualStyleBackColor = true;
            this.SelectDirectoryButton.Click += new System.EventHandler(this.SelectDirectoryButton_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(368, 4);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(275, 273);
            this.treeView1.TabIndex = 3;
            this.treeView1.TabStop = false;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(18, 190);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(129, 23);
            this.StartButton.TabIndex = 3;
            this.StartButton.Text = "Начать поиск";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // FileNameText
            // 
            this.FileNameText.Enabled = false;
            this.FileNameText.Location = new System.Drawing.Point(15, 164);
            this.FileNameText.Name = "FileNameText";
            this.FileNameText.Size = new System.Drawing.Size(347, 20);
            this.FileNameText.TabIndex = 0;
            this.FileNameText.TabStop = false;
            // 
            // FileNameLabel
            // 
            this.FileNameLabel.AutoSize = true;
            this.FileNameLabel.Location = new System.Drawing.Point(15, 148);
            this.FileNameLabel.Name = "FileNameLabel";
            this.FileNameLabel.Size = new System.Drawing.Size(176, 13);
            this.FileNameLabel.TabIndex = 1;
            this.FileNameLabel.Text = "Последний рассмотренный файл";
            // 
            // CountLabel
            // 
            this.CountLabel.AutoSize = true;
            this.CountLabel.Location = new System.Drawing.Point(15, 231);
            this.CountLabel.Name = "CountLabel";
            this.CountLabel.Size = new System.Drawing.Size(163, 13);
            this.CountLabel.TabIndex = 5;
            this.CountLabel.Text = "Число рассмотренных файлов";
            // 
            // CounterText
            // 
            this.CounterText.Enabled = false;
            this.CounterText.Location = new System.Drawing.Point(184, 228);
            this.CounterText.Name = "CounterText";
            this.CounterText.Size = new System.Drawing.Size(54, 20);
            this.CounterText.TabIndex = 6;
            this.CounterText.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Время выполнения";
            // 
            // TimerText
            // 
            this.TimerText.Enabled = false;
            this.TimerText.Location = new System.Drawing.Point(156, 257);
            this.TimerText.Name = "TimerText";
            this.TimerText.Size = new System.Drawing.Size(111, 20);
            this.TimerText.TabIndex = 6;
            this.TimerText.TabStop = false;
            // 
            // ContextForTree
            // 
            this.ContextForTree.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.ContextForTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Option_Open,
            this.Option_Delete});
            this.ContextForTree.Name = "ContextForTree";
            this.ContextForTree.Size = new System.Drawing.Size(122, 48);
            this.ContextForTree.Text = "Действия";
            // 
            // Option_Open
            // 
            this.Option_Open.Name = "Option_Open";
            this.Option_Open.Size = new System.Drawing.Size(121, 22);
            this.Option_Open.Text = "Открыть";
            this.Option_Open.Click += new System.EventHandler(this.Option_Open_Click);
            // 
            // Option_Delete
            // 
            this.Option_Delete.Name = "Option_Delete";
            this.Option_Delete.Size = new System.Drawing.Size(121, 22);
            this.Option_Delete.Text = "Удалить";
            this.Option_Delete.Click += new System.EventHandler(this.Option_Delete_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 288);
            this.Controls.Add(this.TimerText);
            this.Controls.Add(this.CounterText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CountLabel);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.SelectDirectoryButton);
            this.Controls.Add(this.FileNameLabel);
            this.Controls.Add(this.FileTextLabel);
            this.Controls.Add(this.FileNameText);
            this.Controls.Add(this.FileContentText);
            this.Controls.Add(this.TemplateLabel);
            this.Controls.Add(this.NameTemplateText);
            this.Controls.Add(this.DirectoryLabel);
            this.Controls.Add(this.DirectoryPathText);
            this.Name = "MainForm";
            this.Text = "File Search";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ContextForTree.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DirectoryPathText;
        private System.Windows.Forms.Label DirectoryLabel;
        private System.Windows.Forms.TextBox NameTemplateText;
        private System.Windows.Forms.Label TemplateLabel;
        private System.Windows.Forms.TextBox FileContentText;
        private System.Windows.Forms.Label FileTextLabel;
        private System.Windows.Forms.Button SelectDirectoryButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox FileNameText;
        private System.Windows.Forms.Label FileNameLabel;
        private System.Windows.Forms.Label CountLabel;
        private System.Windows.Forms.TextBox CounterText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TimerText;
        private System.Windows.Forms.ContextMenuStrip ContextForTree;
        private System.Windows.Forms.ToolStripMenuItem Option_Open;
        private System.Windows.Forms.ToolStripMenuItem Option_Delete;
    }
}

