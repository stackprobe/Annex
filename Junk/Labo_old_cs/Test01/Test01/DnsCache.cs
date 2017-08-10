using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Charlotte
{
	public class DnsCache
	{
		public static readonly DnsCache I = new DnsCache();

		private DnsCache()
		{ }

		private object SYNCROOT = new object();
		private Dictionary<string, string> Cache = new Dictionary<string, string>();

		public string GetIP(string domain)
		{
			string ip;

			lock (this.SYNCROOT)
			{
				try
				{
					ip = this.Cache[domain];
				}
				catch
				{
					ip = this.ToIP(domain);
					this.Cache.Add(domain, ip);
				}
			}
			return ip;
		}

		private string ToIP(string domain)
		{
			string ip = Dns.GetHostEntry(domain).AddressList[0].ToString();

			if (ip == "::1")
			{
				ip = "127.0.0.1";
			}
			return ip;
		}
	}
}
