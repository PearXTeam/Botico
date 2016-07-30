using System.Text;
using Botico.Model;

namespace Botico.Commands
{
	public class CommandHelp : ICommand
	{
		public string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.help.names").Split(',');
		}

		public string OnUse(CommandArgs args)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(args.Botico.Loc.GetString("command.help"));
			foreach (ICommand cmd in args.Botico.Commands)
			{
				sb.Append(cmd.Names(args.Botico)[0]);
				sb.Append(", ");
			}
			sb.Remove(sb.Length - 2, 2);
			sb.Append(".");
			return sb.ToString();
		}
	}
}
