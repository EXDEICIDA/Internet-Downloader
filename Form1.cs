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
using System.Data.SQLite;

namespace TestBunifu
{
    public partial class frmMain : Form
    {

        private Download downloadManager;
        private HttpHandler httpHandler;
        private Form currentSettingsForm = null;
        //Sorting
        private string currentFilter;

        public frmMain()
        {
            InitializeComponent();
            bunifuFormDock1.SubscribeControlsToDragEvents(new Control[]
            {//This code piece is  the dragging the app in the screen 
                pnlHeader2,
                lblTitle

            });
            downloadManager = new Download();
            httpHandler = new HttpHandler();

            //Sorting used
            currentFilter = null;

            BindDataToGrid();

           
        }

        // Method to bind data to DataGridView
        private void BindDataToGrid()
        {
            try
            {
                // Get downloads from the database
                List<DownloadInfo> downloads = currentFilter != null? downloadManager.GetDownloadsByType(currentFilter) : downloadManager.GetAllDownloads();

                if (downloads.Count == 0)
                {
                    MessageBox.Show("No data retrieved from the database.");
                    return;
                }

                // Clear existing columns and rows
                grid.Columns.Clear();
                grid.Rows.Clear();

                // Create columns dynamically based on properties of DownloadInfo class
                foreach (var property in typeof(DownloadInfo).GetProperties())
                {
                    grid.Columns.Add(property.Name, property.Name);
                }

                // Add rows to DataGridView
                foreach (DownloadInfo download in downloads)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    List<object> values = new List<object>();
                    foreach (var property in typeof(DownloadInfo).GetProperties())
                    {
                        values.Add(property.GetValue(download));
                    }
                    grid.Rows.Add(values.ToArray());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while binding data to grid: " + ex.Message);
            }
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
            
        }

        private void frmMain_shown(object sender, EventArgs e)
        {
           // btnCompressed.Focus();
            //btnDocuments.Focus();
            
        }

       
        //Gui Conponents down here listed

        private void btnCompressed_Click(object sender, EventArgs e)
        {
            // Set the current filter to Compressed Files
            currentFilter = "Compressed";
            BindDataToGrid();

        }

        private void btnDocuments_Click(object sender, EventArgs e)
        {
            // Set the current filter to Documents
            currentFilter = "Document";
            BindDataToGrid();
        }

        private void btnMusic_Click(object sender, EventArgs e)
        {
            // Set the current filter to Music
            currentFilter = "Music";
            BindDataToGrid();

        }

        private void btnSoftware_Click(object sender, EventArgs e)
        {
            // Set the current filter to Software
            currentFilter = "Software";
            BindDataToGrid();
        }

        private void btnVideos_Click(object sender, EventArgs e)
        {
            // Set the current filter to Videos
            currentFilter = "Video";
            BindDataToGrid();
        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {

            ToggleMainControlsVisibility(false); // Hide main controls
            pnlHeader.Visible = false; // Specifically hide pnlHeader when opening settings

            if (currentSettingsForm == null || currentSettingsForm.IsDisposed)
            {
                currentSettingsForm = new SettingsForm(); 
                currentSettingsForm.TopLevel = false;
                currentSettingsForm.Dock = DockStyle.Fill;
                panel4.Controls.Add(currentSettingsForm); 
            }

            currentSettingsForm.Show(); 
            currentSettingsForm.BringToFront(); 
        }
       

       

        private void btnHelp_Click(object sender, EventArgs e)
        {

        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            currentFilter = null; // Show all records
            BindDataToGrid();
        }

        private void btnCancelled_Click(object sender, EventArgs e)
        {
            currentFilter = "Incomplete"; // Filter to show only cancelled
            BindDataToGrid();
        }

        private void btnFinished_Click(object sender, EventArgs e)
        {
            currentFilter = "Downloaded"; // Filter to show only finished
            BindDataToGrid();
        }

        private void btnInLine_Click(object sender, EventArgs e)
        {
            currentFilter = "Pending"; // Filter to show only in line (pending)
            BindDataToGrid();
        }

        private void textBoxUrl_TextChanged(object sender, EventArgs e)
        {

        }

       
       
        private async void btnDownload_Click(object sender, EventArgs e)
        {
            string url = textBoxUrl.Text;

            try
            {
                // Initialize the Progress object for reporting download progress
                var progress = new Progress<Tuple<int, string>>(tuple => UpdateProgressBar(tuple.Item1, tuple.Item2));

                // Perform the file download asynchronously, passing the progress object
                bool downloadSuccess = await downloadManager.DownloadFileAsync(url, progress);

                if (downloadSuccess)
                {
                    // Refresh grid data after successful download
                    BindDataToGrid();
                    MessageBox.Show("File downloaded successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to download file from the provided URL.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during download: {ex.Message}");
            }
        }


        private void downloadProgress_progressChanged(object sender, EventArgs e)
        {
           
        }


      
        // Method to update the progress bar and label
        private void UpdateProgressBar(int percentage, string status)
        {
           
            Console.WriteLine($"UpdateProgressBar method called with percentage: {percentage}");

            
            if (downloadProgress.InvokeRequired)
            {
                Console.WriteLine("InvokeRequired: True");
                downloadProgress.Invoke((MethodInvoker)(() => downloadProgress.Value = percentage));
            }
            else
            {
                Console.WriteLine("InvokeRequired: False");
                downloadProgress.Value = percentage;
            }

           
            if (statsLbl.InvokeRequired)
            {
                Console.WriteLine("statsLbl InvokeRequired: True");
                statsLbl.Invoke((MethodInvoker)(() => statsLbl.Text = $"{percentage}%"));
            }
            else
            {
                Console.WriteLine("statsLbl InvokeRequired: False");
                statsLbl.Text = $"{percentage}%"; 
            }
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            // Check if the window is already maximized
            if (this.WindowState == FormWindowState.Maximized)
            {
                // If the window is maximized, restore it to its normal state
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                // Otherwise, maximize the window
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void statsLbl_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlHeader2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (currentSettingsForm != null && !currentSettingsForm.IsDisposed)
            {
                currentSettingsForm.Hide(); // Hide the settings form
            }

            ToggleMainControlsVisibility(true); // Show main controls again
            pnlHeader.Visible = true; // Specifically show pnlHeader when returning to the main menu

        }

       
        private void ToggleMainControlsVisibility(bool visible)
        {
            // Adjust these controls as per your actual UI elements that should be toggled
            grid.Visible = visible;
            btnDownload.Visible = visible;
            downloadProgress.Visible = visible;
            textBoxUrl.Visible = visible;
            statsLbl.Visible = visible;
            // pnlHeader.Visible = visible; // Toggle this if necessary
            // pnlHeader2 should stay as it is since you always want it visible
        }


    }
}
