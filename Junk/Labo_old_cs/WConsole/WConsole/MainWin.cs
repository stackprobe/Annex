using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace WConsole
{
	public partial class MainWin : Form
	{
		#region ALT_F4 抑止

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
			{
				_xPressed = true;
				return;
			}
			base.WndProc(ref m);
		}

		#endregion

		private bool _xPressed;

		private const int OUTPUT_TEXT_LEN_MAX = 100000;

		private Process _cmdProc;
		private StreamReader _cmdStderr;
		private StreamReader _cmdStdout;
		private StreamWriter _cmdStdin;

		private Thread _stderrReadTh;
		private Thread _stdoutReadTh;

		private object _stdoutBuff_SYNCROOT = new object();
		private StringBuilder _stdoutBuff = new StringBuilder();

		public MainWin()
		{
			ProcessStartInfo psi = new ProcessStartInfo();

			psi.FileName = "cmd.exe";
			psi.Arguments = "";
			psi.CreateNoWindow = true;
			psi.UseShellExecute = false;
			psi.RedirectStandardError = true;
			psi.RedirectStandardOutput = true;
			psi.RedirectStandardInput = true;

			_cmdProc = Process.Start(psi);
			_cmdStderr = _cmdProc.StandardError;
			_cmdStdout = _cmdProc.StandardOutput;
			_cmdStdin = _cmdProc.StandardInput;

			_stderrReadTh = new Thread((ThreadStart)delegate
			{
				this.ReadTh(_cmdStderr);
			});
			_stdoutReadTh = new Thread((ThreadStart)delegate
			{
				this.ReadTh(_cmdStdout);
			});
			_stderrReadTh.Start();
			_stdoutReadTh.Start();

			InitializeComponent();
		}

		private void ReadTh(StreamReader sr)
		{
			for (; ; )
			{
				int chr = sr.Read(); // 次の文字を読み込むまでブロックする。プロセス終了後 -> 直ちに -1 を返す。

				if (chr == -1)
					break;

				if (chr == '\r')
					continue;

				lock (_stdoutBuff_SYNCROOT)
				{
					if (chr == '\n')
					{
						_stdoutBuff.Append('\r');
						_stdoutBuff.Append('\n');
					}
					else
						_stdoutBuff.Append((char)chr);
				}
			}
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.OutputText.BackColor = this.InputText.BackColor;
			this.InputText.Focus();
			this.MT_Enabled = true;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MT_Enabled = false;

			_stderrReadTh.Join();
			_stderrReadTh = null;

			_stdoutReadTh.Join();
			_stdoutReadTh = null;
		}

		private bool MT_Enabled;
		private bool MT_Busy;
		private long MT_Count;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MT_Enabled == false || this.MT_Busy)
				return;

			this.MT_Busy = true;

			try
			{
				if (_cmdProc.HasExited)
				{
					this.Close();
					return;
				}
				string buff = null;

				if (_xPressed)
				{
					_cmdStdin.Write("exit\n");
					_cmdStdin.Flush();

					_xPressed = false;
				}
				lock (_stdoutBuff_SYNCROOT)
				{
					if (1 <= _stdoutBuff.Length)
					{
						buff = _stdoutBuff.ToString();
						_stdoutBuff = new StringBuilder();
					}
				}
				if (buff != null)
				{
					string text = this.OutputText.Text + buff;

					// '\f' == clear screen らしい..
					{
						int index = buff.IndexOf('\f');

						if (index != -1)
							text = buff.Substring(index + 1);
					}

					if (OUTPUT_TEXT_LEN_MAX < text.Length)
						text = text.Substring(text.Length - OUTPUT_TEXT_LEN_MAX);

					this.OutputText.Text = text;
					this.OutputText.SelectionStart = text.Length;
					this.OutputText.ScrollToCaret();
				}
			}
			finally
			{
				this.MT_Busy = false;
				this.MT_Count++;
			}
		}

		private void OutputText_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void OutputText_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.InputText.Focus();
		}

		private void OutputText_KeyDown(object sender, KeyEventArgs e)
		{
			//this.InputText.Focus(); // 効かない。
		}

		private void InputText_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private const int HISTORY_MAX = 100;
		private List<string> _history = new List<string>();
		private int _historyCurrPos = 0;

		private void InputText_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13) // enter
			{
				e.Handled = true;

				if (this.InputText.Text != "")
				{
					if (HISTORY_MAX <= _history.Count)
						_history.RemoveAt(0);

					_history.Add(this.InputText.Text);
					_historyCurrPos = _history.Count;
				}
				_cmdStdin.Write(this.InputText.Text + "\n");
				_cmdStdin.Flush();

				this.InputText.Text = "";
			}
		}

		private void InputText_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
			{
				e.Handled = true;

				if (1 <= _history.Count)
				{
					if (e.KeyCode == Keys.Up)
					{
						if (1 <= _historyCurrPos)
							_historyCurrPos--;
					}
					else
					{
						if (_historyCurrPos < _history.Count - 1)
							_historyCurrPos++;
					}
					this.InputText.Text = _history[_historyCurrPos];
					this.InputText.SelectionStart = this.InputText.Text.Length;
					this.InputText.ScrollToCaret();
				}
			}
		}
	}
}
