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
using HDDComparison.LogSharp;

namespace HDDComparison
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AppendLog("Welcome to HDD Comparison!");
        }

        private void DriveOneSelect_Click(object sender, EventArgs e)
        {
            AppendLog("\nOpening File Dialog Box for Drive One...");
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    DriveOneText.Text = $"Selected Drive: " + dialog.SelectedPath;
                    AppendLog("Drive " + dialog.SelectedPath + " has been selected");
                }
            }
        }


        private void AppendLog(string content) => consoleOutput.Text += $"{content}\n";

        private Log log;
        private string reportName;

        private void button1_Click(object sender, EventArgs e)
        {
            string driveOne = DriveOneText.Text.Replace("Selected Drive: ", "");
            DriveInfo onedrive = new DriveInfo(driveOne);

            if (string.IsNullOrEmpty(reportName))
                reportName = $"drivereport({Directory.GetFiles(Directory.GetCurrentDirectory()).Length - 2}).txt";

            log = new Log(LogFormat.DMY, reportName);
            log.AppendLog($"{driveOne} has been selected for Drive 1");

            AppendLog("Drive Type: " + onedrive.DriveType);
            AppendLog("Drive Format: " + onedrive.DriveFormat);
            AppendLog("Drive Size Total: " + onedrive.TotalSize / 1073741824 + " GB");
            AppendLog("Used Space: " + (onedrive.TotalSize - onedrive.AvailableFreeSpace) / 1073741824 + " GB");

            log.AppendLog("Drive Type: " + onedrive.DriveType);
            log.AppendLog("Drive Format: " + onedrive.DriveFormat);
            log.AppendLog("Drive Size Total: " + onedrive.TotalSize / 1073741824 + " GB");
            log.AppendLog("Used Space: " + (onedrive.TotalSize - onedrive.AvailableFreeSpace) / 1073741824 + " GB");

            AppendLog("Done. You can view the full report with: " + reportName);

            long usedSpace = onedrive.TotalSize - onedrive.AvailableFreeSpace;

            log.AppendLog("Used Data Space\n" +
                $"\t\t\t\t{Format(usedSpace)} Bytes\n" +
                $"\t\t\t\t{Format(usedSpace / 1024)} KB\n" +
                $"\t\t\t\t{Format(usedSpace / 1048576)} MB\n" +
                $"\t\t\t\t{Format(usedSpace / 1073741824)} GB");

        }

        

        private string Format(long num) {
            return string.Format("{0:N}", num);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DriveOneText.Text = "Selected drive: ";
            consoleOutput.Text += "\n\nApplication Reset. Ready for Drive Selection";
            log.AppendLog("\n\n----------------------------\n\n");
        }
    }
}
