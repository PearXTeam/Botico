using Botico.Model;
using PearXLib;

namespace Botico
{
	public class CommandTurn : BCommand
	{
		public CommandTurn()
		{
			DescInHelp = true;
		}

		public override string Description(BoticoClient b)
		{
			return b.Loc.GetString("command.turn.desc").Replace("%cmd", b.GetCommandName(this));
		}

		public override string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.turn.names").Split(',');
		}

		public override BoticoResponse OnUse(CommandArgs args)
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
