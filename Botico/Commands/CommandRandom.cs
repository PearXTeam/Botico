using Botico.Model;
using PearXLib;

namespace Botico.Commands
{
	public class CommandRandom : ICommand
	{
		public string Description(BoticoClient b)
		{
			return b.Loc.GetString("command.random.desc").Replace("%cmd", b.GetCommandSymbol() + Names(b)[0]);
		}

		public string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.random.names").Split(',');
		}

		public BoticoResponse OnUse(CommandArgs args)
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
					return args.Botico.Loc.GetString("command.random.nan.max").Replace("%longMax", long.MaxValue.ToString());
				case 2:
					if (long.TryParse(args.Args[0], out min))
					{
						if (long.TryParse(args.Args[1], out max))
						{
							if(min <= max)
								return args.Random.NextLong(max, min).ToString();
							return args.Botico.Loc.GetString("command.random.minBiggerMax");
						}
						return args.Botico.Loc.GetString("command.random.nan.max").Replace("%longMax", long.MaxValue.ToString());
					}
					return args.Botico.Loc.GetString("command.random.nan.min").Replace("%longMax", long.MaxValue.ToString());

			}
		}
	}
}
