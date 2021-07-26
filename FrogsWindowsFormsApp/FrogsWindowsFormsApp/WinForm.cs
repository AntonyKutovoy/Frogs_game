using System;
using System.Windows.Forms;

namespace FrogsWindowsFormsApp
{
    public partial class WinForm : Form
    {
        private int stepCount;
        public WinForm(int stepCount)
        {
            InitializeComponent();
            this.stepCount = stepCount;
        }

        private void WinForm_Load(object sender, EventArgs e)
        {
            infoLabel.Visible = false;
            if (stepCount - 24 > 0)
            {
                infoLabel.Visible = true;
            }
        }
    }
}
