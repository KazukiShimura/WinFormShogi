using System;
using System.Windows.Forms;

namespace WinFormShogi
{
    public partial class ReverseWindow : Form
    {
        public bool NoButtonClick { get; set; } = false;
        public bool YesButtonClick { get; set; } = false;

        public ReverseWindow()
        {
            InitializeComponent();
        }

        private void noButton_Click(object sender, EventArgs e)
        {
            NoButtonClick = true;
        }

        private void yesButton_Click(object sender, EventArgs e)
        {
            YesButtonClick = true;
        }
    }
}
