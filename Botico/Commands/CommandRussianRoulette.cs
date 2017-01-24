using System;
using Botico.Model;

namespace Botico.Commands
{
	public class CommandRussianRoulette : BCommand
	{
		public override string Description(BoticoClient b)
		{
			return b.Loc.GetString("command.roulette.desc");
		}

		public override string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.roulette.names").Split(',');
		}

		public override BoticoResponse OnUse(CommandArgs args)
		{
			if (args.Random.Next(0, 6) == 0)
				return args.Botico.Loc.GetString("command.roulette.fail");
			return args.Botico.Loc.GetString("command.roulette.win");
		}
	}
}
