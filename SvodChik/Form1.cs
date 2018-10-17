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

        Size defaultSize = new Size(1920, 1080);
        String savePath = "C:\\Screenshots\\";
        String processName = "EliteDangerous64";
        Point start = new Point(13, 450);
        Point end = new Point(415, 680);

        public MainWindow()
        {
            InitializeComponent();
            button1.Text = "Test Save";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            foreach (var process in Process.GetProcesses())
                richTextBox1.Text += process.ProcessName + "\n";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            CaptureApp(processName);
        }

        private void TakeFullScreenShot(string savePath, string shotName)
        {
            Bitmap bmp = new Bitmap(defaultSize.Width, defaultSize.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.CopyFromScreen(0, 0, 0, 0, new Size(defaultSize.Width, defaultSize.Height), CopyPixelOperation.SourceCopy);

            bmp.Save(savePath + shotName, ImageFormat.Png);
        }

        public Bitmap CutImg(Bitmap source, Rectangle section)
        {
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }

        private void CaptureApp(string processName)
        {
            try
            {
                var process = Process.GetProcessesByName(processName)[0];
                MessageBox.Show(process.ToString());
                var rect = new User32.Rect();
                User32.GetWindowRect(process.MainWindowHandle, ref rect);

                int width = rect.right - rect.left;
                int height = rect.bottom - rect.top;

                var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                Graphics graphics = Graphics.FromImage(bmp);
                graphics.CopyFromScreen(rect.left, rect.top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);

                bmp.Save(savePath + processName + ".png", ImageFormat.Png);

                Rectangle statSection = new Rectangle(start, new Size(415 - 13, 680 - 450));
                Bitmap statBmp = CutImg(bmp, statSection);
                statBmp.Save(savePath + "Tembala stat.png", ImageFormat.Png);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //some comment


        private class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct Rect
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);
        }
    }
}
