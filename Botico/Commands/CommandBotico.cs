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
			sb.AppendLine("Botico " + BoticoClient.Version);
			sb.AppendLine(args.Botico.Loc.GetString("command.botico.runningOn") + Environment.OSVersion.Platform);
			sb.AppendLine(args.Botico.Loc.GetString("command.botico.client") + args.Botico.ClientName);
			sb.Append(args.Botico.Loc.GetString("command.botico.ramUsed") + GC.GetTotalMemory(true) / 1024 + " kB");
			return sb.ToString();
		}
	}
}
