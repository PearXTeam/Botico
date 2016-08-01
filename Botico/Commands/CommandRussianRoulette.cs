using Botico.Model;

namespace Botico.Commands
{
	public class CommandRussianRoulette : ICommand
	{
		public string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.roulette.names").Split(',');
		}

		public BoticoResponse OnUse(CommandArgs args)
		{
			if (args.Random.Next(0, 6) == 0)
				return args.Botico.Loc.GetString("command.roulette.fail");
			return args.Botico.Loc.GetString("command.roulette.win");
		}
	}
}
