using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChangeVersionNumberTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tbFolderPath.Text = Properties.Settings.Default.FolderPath;
            //tbFolderPath.Text = "C:\\Users\\user\\Desktop\\20170623_EP16.1update";

        }

        //瀏覽按鈕
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tbFolderPath.Text = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.FolderPath = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        //開始按鈕
        private void btnStart_Click(object sender, EventArgs e)
        {
            //取得此資料夾內 所有副檔名為html的檔案
            string[] files = Directory.GetFiles(tbFolderPath.Text, "*.html", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                //讀取檔案的內容
                string html = File.ReadAllText(file);
                //將version number取代掉
                int unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                string newHtml = Regex.Replace(html, @"\?version=(\d*)", "?version="+ unixTimestamp.ToString());
                //寫回檔案
                File.WriteAllText(file, newHtml);
            }

            lbStatus.Text = "成功";

        }
    }
}
