using Botico.Model;
using PearXLib;

namespace Botico.Commands
{
	public class CommandRandom : BCommand
	{
		public CommandRandom()
		{
			DescInHelp = true;
		}

		public override string Description(BoticoClient b)
		{
			return b.Loc["command.random.desc"].Replace("%cmd", b.GetCommandName(this));
		}

		public override string[] Names(BoticoClient b)
		{
			return b.Loc["command.random.names"].Split(',');
		}

		public override BoticoResponse OnUse(CommandArgs args)
		{
			long max, min;
			switch (args.Args.Length)
			{
				default:
					return Description(args.Botico);
				case 1:
					if (long.TryParse(args.Args[0], out max))
					{
						return args.Random.NextLong(max).ToString();
					}
					return args.Botico.Loc["command.random.nan.max"].Replace("%longMax", long.MaxValue.ToString());
				case 2:
					if (long.TryParse(args.Args[0], out min))
					{
						if (long.TryParse(args.Args[1], out max))
						{
							if(min <= max)
								return args.Random.NextLong(max, min).ToString();
							return args.Botico.Loc["command.random.minBiggerMax"];
						}
						return args.Botico.Loc["command.random.nan.max"].Replace("%longMax", long.MaxValue.ToString());
					}
					return args.Botico.Loc["command.random.nan.min"].Replace("%longMax", long.MaxValue.ToString());

			}
		}
	}
}
