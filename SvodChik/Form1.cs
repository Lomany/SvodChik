using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SvodChik
{
    public partial class MainWindow : Form
    {

        Size windowSize = new Size(1920, 1080);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            foreach (var process in Process.GetProcesses())
                richTextBox1.Text += process.ProcessName + "\n";
        }       

        private void button1_Click_1(object sender, EventArgs e)
        {
            TakeScreenShot("C:\\Screenshots\\", "123.png");
        }

        private void TakeScreenShot (string savePath, string shotName)
        {
            Bitmap bmp = new Bitmap(windowSize.Width, windowSize.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.CopyFromScreen(0, 0, 500, 500, new Size(windowSize.Width, windowSize.Height), CopyPixelOperation.SourceCopy);

            bmp.Save(savePath + shotName, ImageFormat.Png);
        }
    }
}
