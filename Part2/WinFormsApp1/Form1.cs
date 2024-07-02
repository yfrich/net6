using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = @"F:\于富\git管理代码\aspNetCore\Part2\demoFile\2.txt";
            string s = File.ReadAllText(fileName);
            MessageBox.Show(s.Substring(0, 20));
        }

        //事件委托定义了返回值必须为void 所以返回值不能为Task
        private async void button2_Click(object sender, EventArgs e)
        {
            string fileName = @"F:\于富\git管理代码\aspNetCore\Part2\demoFile\2.txt";
            string s = await File.ReadAllTextAsync(fileName);
            MessageBox.Show(s.Substring(0, 20));
        }
    }
}
