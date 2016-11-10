using System;
using System.Text;
using Botico.Model;

namespace Botico.Commands
{
	public class CommandHelp : BCommand
	{
		public override string Description(BoticoClient b)
		{
			return b.Loc.GetString("command.help.desc");
		}

		public override string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.help.names").Split(',');
		}

		public override BoticoResponse OnUse(CommandArgs args)
		{
			StringBuilder sb = new StringBuilder();
			if (args.Botico.Config.NewLines)
			{
				int onPage = 5;
				int page = 1;
				if (args.Args.Length >= 1)
					int.TryParse(args.Args[0], out page);
				if (page == 0)
					page = 1;

				for (int i = (page - 1) * onPage; i < page * onPage; i++)
				{
					if (args.Botico.Commands.Count <= i)
						break;
					var cmd = args.Botico.Commands[i];
					if (cmd.Hidden) continue;
					if (!cmd.DescInHelp)
						sb.AppendLine(args.Botico.GetCommandName(args.Botico.Commands[i]) + " - " + cmd.Description(args.Botico));
					else
						sb.AppendLine(cmd.Description(args.Botico));
				}
				sb.Append(string.Format(args.Botico.Loc.GetString("command.help.page"), page, Math.Round((float)args.Botico.Commands.Count / onPage), args.Botico.GetCommandName(this)));
			}
			else
			{
				foreach (BCommand cmd in args.Botico.Commands)
				{
					if (cmd.Hidden) continue;
					sb.Append(args.Botico.CmdSymbol + cmd.Names(args.Botico)[0]);
					sb.Append(", ");
				}
				sb.Remove(sb.Length - 2, 2);
				sb.Append(".");
			}
			return sb.ToString();
		}
	}
}
