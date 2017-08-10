using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Satellizer
{
	public class Serializer
	{
		private byte[] B;
		private byte[] L;
		private byte[] M;
		private byte[] S;

		private Serializer()
		{
			this.B = Encoding.ASCII.GetBytes("B");
			this.L = Encoding.ASCII.GetBytes("L");
			this.M = Encoding.ASCII.GetBytes("M");
			this.S = Encoding.ASCII.GetBytes("S");
		}

		public static Serializer I = new Serializer();

		public List<byte[]> GetBlockList(object obj)
		{
			ToBlockList i = new ToBlockList();
			i.AddBlock(obj);
			return i._buff;
		}

		private class ToBlockList
		{
			public List<byte[]> _buff = new List<byte[]>();

			public void AddBlock(object obj)
			{
				if (obj == null)
				{
					this.AddString("<null>");
				}
				else if (obj is byte[])
				{
					this.AddBytes((byte[])obj);
				}
				else if (obj is List<object>)
				{
					this.AddList((List<object>)obj);
				}
				else if (obj is Dictionary<string, object>)
				{
					this.AddMap((Dictionary<string, object>)obj);
				}
				else
				{
					this.AddString("" + obj);
				}
			}

			private void AddBytes(byte[] bytes)
			{
				this.AddBlock(I.B, bytes);
			}

			private void AddList(List<object> list)
			{
				List<byte[]> buff = new List<byte[]>();

				foreach (object obj in list)
				{
					buff.AddRange(I.GetBlockList(obj));
				}
				this.AddBlock(I.L, Join(buff));
			}

			private void AddMap(Dictionary<string, object> map)
			{
				List<byte[]> buff = new List<byte[]>();

				foreach (string key in map.Keys)
				{
					buff.AddRange(I.GetBlockList(key));
					buff.AddRange(I.GetBlockList(map[key]));
				}
				this.AddBlock(I.M, Join(buff));
			}

			private void AddString(string obj)
			{
				this.AddBlock(I.S, Encoding.UTF8.GetBytes(obj));
			}

			private void AddBlock(byte[] kind, byte[] data)
			{
				_buff.Add(kind);
				_buff.Add(IntToBlock(data.Length));
				_buff.Add(data);
			}
		}

		public object GetObject(byte[] block)
		{
			return new ToObject(block).NextObject();
		}

		private class ToObject
		{
			private byte[] _block;
			private int _rPos;

			public ToObject(byte[] block)
			{
				_block = block;
				_rPos = 0;
			}

			public object NextObject()
			{
				if (_rPos < _block.Length)
				{
					this.ParseData();
					return this.GetObject();
				}
				return null;
			}

			private byte _kind;
			private byte[] _data;

			private void ParseData()
			{
				_kind = _block[_rPos];
				_rPos++;

				int size = BlockToInt(_block, _rPos);
				_data = new byte[size];
				_rPos += 4;

				Array.Copy(_block, _rPos, _data, 0, size);
				_rPos += size;
			}

			private object GetObject()
			{
				if (_kind == I.B[0])
				{
					return _data;
				}
				if (_kind == I.L[0])
				{
					return new ToObject(_data).GetObjectList();
				}
				if (_kind == I.M[0])
				{
					return new ToObject(_data).GetObjectMap();
				}
				if (_kind == I.S[0])
				{
					return Encoding.UTF8.GetString(_data);
				}
				throw new Exception("不正なkind");
			}

			private object GetObjectList()
			{
				List<object> list = new List<object>();

				for (; ; )
				{
					object obj = this.NextObject();

					if (obj == null)
						break;

					list.Add(obj);
				}
				return list;
			}

			private object GetObjectMap()
			{
				Dictionary<string, object> map = new Dictionary<string, object>();

				for (; ; )
				{
					object key = this.NextObject();

					if (key == null)
						break;

					map.Add("" + key, this.NextObject());
				}
				return map;
			}
		}

		private static byte[] IntToBlock(int value)
		{
			int v1 = value % 0x100;
			value /= 0x100;
			int v2 = value % 0x100;
			value /= 0x100;
			int v3 = value % 0x100;
			int v4 = value / 0x100;

			return new byte[]
			{
				(byte)v1,
				(byte)v2,
				(byte)v3,
				(byte)v4,
			};
		}

		private static int BlockToInt(byte[] block, int rPos)
		{
			int v1 = block[rPos++];
			int v2 = block[rPos++];
			int v3 = block[rPos++];
			int v4 = block[rPos];

			int value = v4;
			value *= 0x100;
			value += v3;
			value *= 0x100;
			value += v2;
			value *= 0x100;
			value += v1;

			return value;
		}

		private static byte[] Join(List<byte[]> blockList)
		{
			int count = 0;

			foreach (byte[] block in blockList)
				count += block.Length;

			byte[] buff = new byte[count];
			count = 0;

			foreach (byte[] block in blockList)
			{
				Array.Copy(block, 0, buff, count, block.Length);
				count += block.Length;
			}
			return buff;
		}
	}
}
