using System;
using System.Text;
using Botico.Model;

namespace Botico.Commands
{
	public class CommandHelp : ICommand
	{
		public string Description(BoticoClient b)
		{
			return b.Loc.GetString("command.help.desc");
		}

		public string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.help.names").Split(',');
		}

		public BoticoResponse OnUse(CommandArgs args)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(args.Botico.Loc.GetString("command.help"));
			string cmdSym = args.Botico.CommandSymbol == null ? "" : args.Botico.CommandSymbol.ToString();
			foreach (ICommand cmd in args.Botico.Commands)
			{
				sb.Append(cmdSym + cmd.Names(args.Botico)[0] + " - " + cmd.Description(args.Botico) + "\n");
			}
			sb.Remove(sb.Length - 1, 1);
			return sb.ToString();
		}
	}
}
