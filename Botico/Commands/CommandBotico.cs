﻿using System;
using System.Text;

namespace Botico.Commands
{
	public class CommandBotico : ICommand
	{
		public string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.botico.names").Split(',');
		}

		public string OnUse(CommandArgs args)
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
