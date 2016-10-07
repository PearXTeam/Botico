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
				sb.AppendLine(args.Botico.GetCommandName(args.Botico.Commands[i]) + " - " + cmd.Description(args.Botico));
			}
			sb.Append(string.Format(args.Botico.Loc.GetString("command.help.page"), page, args.Botico.Commands.Count / onPage, args.Botico.GetCommandName(this)));
			return sb.ToString();
		}
	}
}
