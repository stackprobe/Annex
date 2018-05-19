using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class Key
	{
		private string _name;
		private string _ident; // 128 bit
		private string _rawKey; // 512 bit
		private string _hash; // 128 bit

		public Key(string name, string ident, string rawKey)
		{
			_name = name;
			_ident = ident;
			_rawKey = rawKey;
			resetHash();
		}

		public Key(string name, string ident, string rawKey, string hash)
		{
			if (hash != getHash(name, ident, rawKey))
				throw new Exception("指定された鍵は破損しています。(ハッシュ値が一致しません)");

			_name = name;
			_ident = ident;
			_rawKey = rawKey;
			_hash = hash;
		}

		private void resetHash()
		{
			_hash = getHash(_name, _ident, _rawKey);
		}

		public static string getHash(string name, string ident, string rawKey)
		{
			return Utils.getHash(BinaryTools.join(
				StringTools.ENCODING_SJIS.GetBytes(name),
				StringTools.hex(ident),
				StringTools.hex(rawKey)
				));
		}

		public static Key create()
		{
			return create("名無しの鍵さん" + Utils.getUniquePosix());
		}

		public static Key create(string name)
		{
			return new Key(
				name,
				StringTools.toHex(SecurityTools.getCRand(16)),
				StringTools.toHex(SecurityTools.getCRand(64))
				);
		}

		public static Key getDummyKey()
		{
			return new Key(
				Consts.DUMMY_KEY_NAME,
				Consts.DUMMY_IDENT,
				Consts.DUMMY_RAW_KEY
				);
		}

		public static Key load(XNode xRoot)
		{
			return new Key(
				xRoot.get("name").value,
				xRoot.get("ident").value,
				xRoot.get("raw-key").value,
				xRoot.get("hash").value
				);
		}

		public XNode save()
		{
			XNode xRoot = new XNode("key");

			xRoot.children.Add(new XNode("name", _name));
			xRoot.children.Add(new XNode("ident", _ident));
			xRoot.children.Add(new XNode("raw-key", _rawKey));
			xRoot.children.Add(new XNode("hash", _hash));

			return xRoot;
		}

		public string getName()
		{
			return _name;
		}

		public void setName(string name)
		{
			_name = name;
			resetHash();
		}

		public string getIdent()
		{
			return _ident;
		}

		public string getRawKey()
		{
			return _rawKey;
		}

		public string getHash()
		{
			return _hash;
		}
	}
}
