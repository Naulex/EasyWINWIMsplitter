using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Drawing;

namespace EasyWINWIMsplitter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            string[] arguments = Environment.GetCommandLineArgs();
            if (arguments.Length > 1)
            {
                try
                {
                    string path = arguments[1];
                    int splitCount = Convert.ToInt32(arguments[2]);
                    BeginSplitMethod(path, splitCount);
                }
                catch
                {
                    MessageBox.Show("Ошибка обработки аргументов командной строки!", "Ошибка | EasyWIN-WIMsplitter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
            }
        }

        private void BeginSplitMethod(string path, int splitCount)
        {
            if (path.Length == 0)
            {
                MessageBox.Show("Не выбран WIM файл!", "Ошибка | EasyWIN-WIMsplitter", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                InfoTextBox.Text += "Деление начато!";
                Process split = new Process();
                split.StartInfo.FileName = @"dism.exe";
                split.StartInfo.Arguments = " /split-image /imagefile:\"" + path + "\" /swmfile:\"" + Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ".swm\" /filesize:" + splitCount;
                split.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                split.Start();
                split.WaitForExit();
                split.Dispose();
                InfoTextBox.Text += "Деление завершено!";
                Console.Beep(200, 100);
                Console.Beep(300, 100);
                Console.Beep(400, 100);
            }
        }

        private void ChooseFileBtn_Click(object sender, EventArgs e)
        {
            if (openWim.ShowDialog() == DialogResult.Cancel)
                return;
            InfoTextBox.Text += "Выбран образ: " + openWim.FileName;
                  }

        private void BeginSplit_Click(object sender, EventArgs e)
        {
            BeginSplitMethod(openWim.FileName, Convert.ToInt32(splitSize.Value));
        }

        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("EasyWIN-WIMsplitter - программа, позволяющая делить WIM образы на фрагменты нужного размера, что необходимо при создании загрузочных устройств UEFI и не только.\r\n\r\n\r\nПоддерживаются аргументы командной строки в виде EasyWINWIMsplitter.exe \"С:\\boot.wim\" 4000\r\n\r\n\r\nДля создания загрузочной флешки в UEFI поделите файл \"install.wim\" на части при помощи этой программы, затем переместите всё содержимое ISO образа ОС на носитель, отформатированный в FAT32.\r\n\r\n\r\nEasyWIN-WIMsplitter v.1.1\r\n(c)Naulex, 2023\r\n073797@gmail.com", "Справка | EasyWIN-WIMsplitter", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
