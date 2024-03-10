using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestBunifu
{
    public partial class SettingsForm : Form
    {
        private bool isDarkMode = true; // Default to dark mode
        public SettingsForm()
        {
            InitializeComponent();
            pnlHeader2 = new Panel(); // Initialize pnlHeader2
            panel1 = new Panel(); // Initialize panel1
            InitializeComponent();
        }

        private Panel pnlHeader2;
        private Panel panel1;
        

        // Constructor that takes required controls as parameters
        public SettingsForm(Panel pnlHeader2, Panel panel1)
        {
            InitializeComponent();
            // Assign the passed controls to local variables
            this.pnlHeader2 = pnlHeader2;
            this.panel1 = panel1;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnToggleMode_Click(object sender, EventArgs e)
        {
            ToggleMode();
        }


        private void ToggleMode()
        {
            if (isDarkMode)
            {
                // Switch to light mode
                this.BackColor = SystemColors.Control;
                pnlHeader2.BackColor = SystemColors.ControlLight;
                panel1.BackColor = SystemColors.ControlLight;
                
                // Iterate through controls and set their color
                foreach (Control control in this.Controls)
                {
                    if (control is Button || control is Panel)
                    {
                        control.BackColor = SystemColors.ControlLight;
                        control.ForeColor = SystemColors.ControlText;
                    }
                }
            }
            else
            {
                // Switch to dark mode
                this.BackColor = Color.FromArgb(45, 45, 48);
                if (pnlHeader2 != null) pnlHeader2.BackColor = Color.FromArgb(28, 28, 28);
                if (panel1 != null) panel1.BackColor = Color.FromArgb(28, 28, 28);

                // Iterate through controls and set their color
                foreach (Control control in this.Controls)
                {
                    if (control is Button || control is Panel)
                    {
                        control.BackColor = Color.FromArgb(28, 28, 28);
                        control.ForeColor = Color.White;
                    }
                }
            }

            // Toggle the mode
            isDarkMode = !isDarkMode;
        }

    }
}
