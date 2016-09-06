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
			if (!args.Botico.ShorterMessages)
			{
				sb.AppendLine(args.Botico.Loc.GetString("command.help"));
				foreach (ICommand cmd in args.Botico.Commands)
				{
					if(!(cmd is IHidden))
						sb.Append(args.Botico.GetCommandSymbol() + cmd.Names(args.Botico)[0] + " - " + cmd.Description(args.Botico) + "\n");
				}
				sb.Remove(sb.Length - 1, 1);
			}
			else
			{
				sb.Append(args.Botico.Loc.GetString("command.help"));
				foreach (ICommand cmd in args.Botico.Commands)
				{
					sb.Append(args.Botico.GetCommandSymbol() + cmd.Names(args.Botico)[0] + ", ");
				}
				sb.Remove(sb.Length - 2, 2);
				sb.Append(".");
			}
			return sb.ToString();
		}
	}
}
