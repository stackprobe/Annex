using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test01
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			this.listView1.Items.Add("ABC");
			this.listView1.Items.Add("DEF");
			this.listView1.Items.Add("GHI");

			this.checkedListBox1.Items.Add("ABC");
			this.checkedListBox1.Items.Add("ABC2");
			this.checkedListBox1.Items.Add("ABC3");
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			// noop
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void button1_Click(object sender, EventArgs e)
		{
			new System.Threading.Thread(B1Th).Start();
			new Form2().ShowDialog();
		}
		private void B1Th()
		{
			System.Threading.Thread.Sleep(3000);
			Form2.Death = true;
		}

		private void button2_Click(object sender, EventArgs e) // 別スレッドだからか、モーダルが弱い。
		{
			new System.Threading.Thread(B2Th).Start();
			System.Threading.Thread.Sleep(3000);
			Form2.Death = true;
		}
		private void B2Th()
		{
			new Form2().ShowDialog();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			NetUse.ConnectDisk("\\\\HISCL003\\Ndw", "", "");
		}
	}
}
