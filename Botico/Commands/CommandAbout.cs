using System;
using Botico.Model;

namespace Botico
{
	public class CommandAbout : ICommand
	{
		public string Description(BoticoClient b)
		{
			return b.Loc.GetString("command.about.desc").Replace("%cmd", b.GetCommandSymbol() + Names(b)[0]);
		}

		public string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.about.names").Split(',');
		}

		public BoticoResponse OnUse(CommandArgs args)
		{
			var v = args.Botico.GetCommandFromString(args.JoinedArgs);
			if (v == null)
				return args.Botico.Loc.GetString("command.about.notFound");
			return v.Description(args.Botico);
		}
	}
}
