using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jig_Replicator
{
	public partial class Form4 : Form
	{
		public Form4()
		{
			InitializeComponent();
			CheckForIllegalCrossThreadCalls = false;

		}

		private void button1_Click(object sender, EventArgs e)
		{
			Program.frm3.SDAdapterID = 1;
			Program.frm3.changeSD("Generic (Photofast)");
			this.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Program.frm3.SDAdapterID = 2;
			Program.frm3.changeSD("Smart Dual Reader Gold");
			this.Close();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			Program.frm3.SDAdapterID = 3;
			Program.frm3.changeSD("Smart Dual Reader Black");
			this.Close();
		}
	}
}
