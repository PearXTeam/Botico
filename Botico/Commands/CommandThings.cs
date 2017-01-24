using System.Text;
using System.Collections.Generic;
using Botico.Model;

namespace Botico.Commands
{
	public class CommandThings : BCommand
	{
		public List<BoticoElement> Things = new List<BoticoElement>();
		public static string PathThings => BoticoClient.Path + "things.json";

		public override string Description(BoticoClient b)
		{
			return b.Loc.GetString("command.things.desc").Replace("%cmd", b.GetCommandName(this));
		}

		public override string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.things.names").Split(',');
		}

		public override BoticoResponse OnUse(CommandArgs args)
		{
			if (args.Args.Length >= 1)
			{
				var cmdRemove = args.Botico.Loc.GetString("command.things.remove");
				if (args.Args[0] == args.Botico.Loc.GetString("command.things.random"))
				{
					return Things[args.Random.Next(0, Things.Count)].Content;
				}
				else if (args.Args[0] == cmdRemove)
				{
					if (args.Args.Length >= 2)
					{
						foreach (var t in Things)
						{
							string toRem = args.JoinedArgs.Remove(0, cmdRemove.Length + 1);
							if (t.Content.ToLower() == toRem.ToLower())
							{
								Things.Remove(t);
								return args.Botico.Loc.GetString("command.things.remove.ok");
							}
						}
						return args.Botico.Loc.GetString("command.things.remove.notFound");
					}
					return args.Botico.Loc.GetString("command.things.remove.noThing");
				}
			}
			StringBuilder sb = new StringBuilder();
			sb.Append(args.Botico.Loc.GetString("command.things"));
			foreach (var t in Things)
			{
				sb.Append(t.Content);
				sb.Append(", ");
			}
			sb.Remove(sb.Length - 2, 2);
			sb.Append(".");
			return sb.ToString();
		}
	}
}
