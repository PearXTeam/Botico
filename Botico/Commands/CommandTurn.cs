using System;
using Botico.Model;
using PearXLib;

namespace Botico
{
	public class CommandTurn : ICommand
	{
		public string Description(BoticoClient b)
		{
			return b.Loc.GetString("command.turn.desc").Replace("%cmd", b.CmdSymbol + Names(b)[0]);
		}

		public string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.turn.names").Split(',');
		}

		public BoticoResponse OnUse(CommandArgs args)
		{
			switch (args.Args.Length)
			{
				case 0:
					return Description(args.Botico);
				default:
					return TextUtils.Turn(args.JoinedArgs);
			}
		}
	}
}
