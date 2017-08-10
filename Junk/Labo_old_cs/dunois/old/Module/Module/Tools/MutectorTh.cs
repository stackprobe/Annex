using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	public class MutectorTh
	{
		private const int BUFF_SIZE_MAX = 20000000; // 20 MB
		private const int M_SIZE_CONST = 100;

		public class Sender : IDisposable
		{
			private NamedEventPair EvSend;
			private Mutector.Sender MtSender;
			private Thread Th;
			private bool Death = false;
			private object SYNCROOT = new object();
			private Queue<byte[]> Buff = new Queue<byte[]>();
			private int BuffSize = 0;

			public Sender(string name)
			{
				if (name == null)
					throw new ArgumentNullException();

				this.EvSend = new NamedEventPair();
				this.MtSender = new Mutector.Sender(name);
				this.Th = new Thread((ThreadStart)delegate
				{
					for (; ; )
					{
						this.EvSend.WaitForMillis(2000);

						if (this.Death)
							break;

						for (; ; )
						{
							byte[] message;

							lock (SYNCROOT)
							{
								if (this.Buff.Count == 0)
									break;

								message = this.Buff.Dequeue();
								this.BuffSize -= message.Length + M_SIZE_CONST;
							}
							this.MtSender.Send(message);
						}
					}
				});
				this.Th.Start();
			}

			public void Dispose()
			{
				if (this.Th != null)
				{
					this.Death = true;
					this.EvSend.Set();

					this.Th.Join();
					this.Th = null;

					this.EvSend.Dispose();
					this.EvSend = null;
					this.MtSender.Dispose();
					this.MtSender = null;
				}
			}

			public bool Send(byte[] message) // ret: ? 成功
			{
				if (message == null)
					throw new ArgumentNullException();

				lock (SYNCROOT)
				{
					if (BUFF_SIZE_MAX < this.BuffSize) // ? Overflow
						return false;

					this.Buff.Enqueue(message);
					this.BuffSize += message.Length + M_SIZE_CONST;
					this.EvSend.Set();
				}
				return true;
			}
		}

		public class Recver : IDisposable, Mutector.IRecver
		{
			private Mutector.Recver MtRecver;
			private Thread Th;
			private bool Death = false;
			private object SYNCROOT = new object();
			private Queue<byte[]> Buff = new Queue<byte[]>();
			private int BuffSize = 0;

			public Recver(string name)
			{
				if (name == null)
					throw new ArgumentNullException();

				this.MtRecver = new Mutector.Recver(name);
				this.MtRecver.SetRecver(this);
				this.Th = new Thread((ThreadStart)delegate
				{
					this.MtRecver.Perform();
				});
				this.Th.Start();
			}

			public void Dispose()
			{
				if (this.Th != null)
				{
					this.Death = true;

					this.Th.Join();
					this.Th = null;

					this.MtRecver.Dispose();
					this.MtRecver = null;
				}
			}

			public bool Interlude()
			{
				return this.Death == false;
			}

			public void Recved(byte[] message)
			{
				lock (SYNCROOT)
				{
					if (BUFF_SIZE_MAX < this.BuffSize) // ? Overflow
						return;

					this.Buff.Enqueue(message);
					this.BuffSize += message.Length + M_SIZE_CONST;
				}
			}

			public byte[] Recv()
			{
				byte[] message = null;

				lock (SYNCROOT)
				{
					if (1 <= this.Buff.Count)
					{
						message = this.Buff.Dequeue();
						this.BuffSize -= message.Length + M_SIZE_CONST;
					}
				}
				return message;
			}
		}
	}
}
