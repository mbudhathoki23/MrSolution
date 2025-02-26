using System;
using System.Windows.Forms;

namespace MrSync
{
	public partial class OnlineSync : Form
	{
		public OnlineSync()
		{
			InitializeComponent();
		}

		private void BtnPull_Click(object sender, EventArgs e)
		{
			MessageBox.Show("I am here");
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			BtnPull.PerformClick();
		}

		private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			Show();
			this.WindowState = FormWindowState.Normal;
			this.StartPosition = FormStartPosition.CenterParent;
		}
	}
}
