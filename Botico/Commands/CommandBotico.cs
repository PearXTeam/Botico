using System;
using System.Text;
using Botico.Model;

namespace Botico.Commands
{
	public class CommandBotico : BCommand
	{
		public override string Description(BoticoClient b)
		{
			return b.Loc.GetString("command.botico.desc");
		}

		public override string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.botico.names").Split(',');
		}

		public override BoticoResponse OnUse(CommandArgs args)
		{
			StringBuilder sb = new StringBuilder();
			if (args.Botico.Config.NewLines)
			{
				sb.AppendLine("PearX Team's Botico");
				sb.AppendLine(args.Botico.Loc.GetString("command.botico.runningOn") + Environment.OSVersion);
				sb.AppendLine(args.Botico.Loc.GetString("command.botico.client") + args.Botico.ClientName);
				sb.Append(args.Botico.Loc.GetString("command.botico.ramUsed") + GC.GetTotalMemory(true) / 1024 + " kB");
			}
			else
			{
				sb.Append("PearX Team's Botico (");
				sb.Append(args.Botico.ClientName);
				sb.Append("), запущен на ");
				sb.Append(Environment.OSVersion);
				sb.Append(", использовано ");
				sb.Append(GC.GetTotalMemory(true) / 1024 + " kB");
				sb.Append(".");
			}
			return sb.ToString();
		}
	}
}
