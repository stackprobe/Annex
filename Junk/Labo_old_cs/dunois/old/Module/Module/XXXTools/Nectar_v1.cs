using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Charlotte.Tools;

namespace Charlotte.XXXTools
{
	/// <summary>
	/// 送受信どちらかのプロセスが強制終了した場合、内部状態が壊れることがある。-- XXX 回避できないのか？
	/// -- 送受信両オブジェクトを再作成すること。
	/// -- 送受信両オブジェクトが存在しないタイミングを確保すること。== イベントのセット状態をクリアするため。
	/// </summary>
	public class Nectar_v1 : IDisposable
	{
		private const string COMMON_ID = "{4498e8a7-3511-4512-8ac3-f58c27da720c}";

		private MutexData _mtx;
		private NamedEventData _evReady;
		private NamedEventData _evData;
		private NamedEventData _evCtrl;
		private NamedEventData _evSync;
		private int _timeoutMillis;

		public Nectar_v1(string name, int timeoutMillis = 5000)
		{
			string ident = COMMON_ID + "_" + SecurityTools.GetSHA512_128String(StringTools.ENCODING_SJIS.GetBytes(name));

			_mtx = new MutexData(ident + "_Mutex");

			using (new MutexData.Section(_mtx))
			{
				_evReady = new NamedEventData(ident + "_Ready");
				_evData = new NamedEventData(ident + "_Data");
				_evCtrl = new NamedEventData(ident + "_Ctrl");
				_evSync = new NamedEventData(ident + "_Sync");
			}
			_timeoutMillis = timeoutMillis;
		}

		/// <summary>
		/// タイムアウトすると例外を投げる。
		/// </summary>
		/// <param name="message"></param>
		public void Send(byte[] message)
		{
			this.SendBit(false, true);

			for (int index = 0; index < message.Length; index++)
			{
				for (int bit = 1 << 7; bit != 0; bit >>= 1)
				{
					this.SendBit((message[index] & bit) != 0, false);
				}
			}
			this.SendBit(true, true);
		}

		private void SendBit(bool data, bool ctrl)
		{
			if (_evReady.WaitForMillis(_timeoutMillis) == false)
			{
				throw new Exception("送信タイムアウト");
			}
			if (data)
			{
				_evData.Set();
			}
			if (ctrl)
			{
				_evCtrl.Set();
			}
			_evSync.Set();
		}

		private int _chr = 0;
		private int _bIndex = 0;
		private List<byte> _buff = new List<byte>();

		/// <summary>
		/// タイムアウトすると例外を投げる。
		/// </summary>
		/// <returns></returns>
		public byte[] Recv()
		{
			for (; ; )
			{
				int bit = this.RecvBit();

				if (bit == 2)
				{
					_chr = 0;
					_bIndex = 0;
					_buff.Clear();
				}
				else if (bit == 3)
				{
					return _buff.ToArray();
				}
				else
				{
					_chr <<= 1;
					_chr |= bit;
					_bIndex++;

					if (_bIndex == 8)
					{
						_buff.Add((byte)_chr);
						_chr = 0;
						_bIndex = 0;
					}
				}
			}
		}

		private bool _ready;

		private int RecvBit()
		{
			if (_ready == false)
			{
				_evReady.Set();
				_ready = true;
			}
			if (_evSync.WaitForMillis(_timeoutMillis) == false)
			{
				throw new Exception("受信タイムアウト");
			}
			_ready = false;

			int bit = 0;

			bit |= _evData.WaitForMillis(0) ? 1 : 0;
			bit |= _evCtrl.WaitForMillis(0) ? 2 : 0;

			return bit;
		}

		public void Dispose()
		{
			if (_mtx != null)
			{
				using (new MutexData.Section(_mtx))
				{
					_evReady.Dispose();
					_evReady = null;
					_evData.Dispose();
					_evData = null;
					_evCtrl.Dispose();
					_evCtrl = null;
					_evSync.Dispose();
					_evSync = null;
				}
				_mtx.Dispose();
				_mtx = null;
			}
		}

		public class Sender : IDisposable
		{
			private Nectar_v1 _n;

			public Sender(string name)
			{
				_n = new Nectar_v1(name, 30000); // 30 秒 -- 受信側が存在すること前提なので、長め。
			}

			public bool Send(byte[] message)
			{
				try
				{
					_n.Send(message);
					return true;
				}
				catch
				{ }

				return false;
			}

			public void Dispose()
			{
				if (_n != null)
				{
					_n.Dispose();
					_n = null;
				}
			}
		}

		public class Recver : IDisposable
		{
			private Nectar_v1 _n;

			public Recver(string name)
			{
				_n = new Nectar_v1(name, 2000); // 2 秒 -- タイムアウトしても送信中のメッセージは維持される。interrupt 確保のため、短め。
			}

			/// <summary>
			/// クライアント応答用？
			/// </summary>
			/// <param name="count"></param>
			/// <returns></returns>
			public byte[] Recv(int count = 30) // def 30 -> 60 秒 -- 相手側の処理時間 + 応答が必ずあることが前提なので、長め。
			{
				for (int c = 0; c < count; c++)
				{
					byte[] message = this.Receipt();

					if (message != null)
					{
						return message;
					}
				}
				return null;
			}

			/// <summary>
			/// サーバー用？
			/// 受信を待機する場合、時間を空けずに繰り返し呼び出すこと。
			/// </summary>
			/// <returns></returns>
			public byte[] Receipt()
			{
				try
				{
					return _n.Recv();
				}
				catch
				{ }

				return null;
			}

			public void Dispose()
			{
				if (_n != null)
				{
					_n.Dispose();
					_n = null;
				}
			}
		}
	}
}
