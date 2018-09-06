using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;

namespace NmEvTest
{
	class Program
	{
		const string IDENT = "{ea8854ae-c349-4e77-9b8b-d2ded4e8040f}";

		static void Main(string[] args)
		{
			try
			{
				string arg0 = args[0];

				arg0 = arg0.ToUpper();

				// G ... Global
				// L ... Local

				// S ... Security

				// S ... Set
				// W ... Wait

				if (arg0 == "/GSS")
				{
					EventWaitHandleSecurity security = new EventWaitHandleSecurity();

					security.AddAccessRule(
						new EventWaitHandleAccessRule(
							new SecurityIdentifier(
								WellKnownSidType.WorldSid,
								null
								),
							EventWaitHandleRights.FullControl,
							AccessControlType.Allow
							)
						);

					bool createdNew;
					EventWaitHandle ev = new EventWaitHandle(false, EventResetMode.AutoReset, @"Global\Global_" + IDENT, out createdNew, security);

					ev.Set();
					ev.Dispose();
				}
				else if (arg0 == "/GSW")
				{
					EventWaitHandleSecurity security = new EventWaitHandleSecurity();

					security.AddAccessRule(
						new EventWaitHandleAccessRule(
							new SecurityIdentifier(
								WellKnownSidType.WorldSid,
								null
								),
							EventWaitHandleRights.FullControl,
							AccessControlType.Allow
							)
						);

					bool createdNew;
					EventWaitHandle ev = new EventWaitHandle(false, EventResetMode.AutoReset, @"Global\Global_" + IDENT, out createdNew, security);

					ev.WaitOne();
					ev.Dispose();
				}
				else if (arg0 == "/GS")
				{
					EventWaitHandle ev = new EventWaitHandle(false, EventResetMode.AutoReset, @"Global\Global_" + IDENT);

					ev.Set();
					ev.Dispose();
				}
				else if (arg0 == "/GW")
				{
					EventWaitHandle ev = new EventWaitHandle(false, EventResetMode.AutoReset, @"Global\Global_" + IDENT);

					ev.WaitOne();
					ev.Dispose();
				}
				else if (arg0 == "/LS")
				{
					EventWaitHandle ev = new EventWaitHandle(false, EventResetMode.AutoReset, IDENT);

					ev.Set();
					ev.Dispose();
				}
				else if (arg0 == "/LW")
				{
					EventWaitHandle ev = new EventWaitHandle(false, EventResetMode.AutoReset, IDENT);

					ev.WaitOne();
					ev.Dispose();
				}
				else
				{
					throw new Exception("不明なコマンド引数");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("" + e);
			}
		}
	}
}
