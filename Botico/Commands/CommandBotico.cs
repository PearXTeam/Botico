using System;
using System.Text;
using Botico.Model;

namespace Botico.Commands
{
	public class CommandBotico : ICommand
	{
		public string Description(BoticoClient b)
		{
			return b.Loc.GetString("command.botico.desc");
		}

		public string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.botico.names").Split(',');
		}

		public BoticoResponse OnUse(CommandArgs args)
		{
			StringBuilder sb = new StringBuilder();
			string post = args.Botico.ShorterMessages ? ", " : "\n";
			sb.Append("Botico " + BoticoClient.Version + post);
			sb.Append(args.Botico.Loc.GetString("command.botico.runningOn") + Environment.OSVersion.Platform + post);
			sb.Append(args.Botico.Loc.GetString("command.botico.client") + args.Botico.ClientName + post);
			sb.Append(args.Botico.Loc.GetString("command.botico.ramUsed") + GC.GetTotalMemory(true) / 1024 + " kB");
			return sb.ToString();
		}
	}
}
