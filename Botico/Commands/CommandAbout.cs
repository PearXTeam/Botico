using System;
using Botico.Model;

namespace Botico
{
	public class CommandAbout : BCommand
	{
		public CommandAbout()
		{
			DescInHelp = true;
		}

		public override string Description(BoticoClient b)
		{
			return b.Loc["command.about.desc"].Replace("%cmd", b.GetCommandName(this));
		}

		public override string[] Names(BoticoClient b)
		{
			return b.Loc["command.about.names"].Split(',');
		}

		public override BoticoResponse OnUse(CommandArgs args)
		{
			var v = args.Botico.GetCommandFromString(args.JoinedArgs);
			if (v == null)
				return args.Botico.Loc["command.about.notFound"];
			return v.Description(args.Botico);
		}
	}
}
