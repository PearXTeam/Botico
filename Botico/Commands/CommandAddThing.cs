using System.IO;
using Botico.Model;
using Newtonsoft.Json;

namespace Botico.Commands
{
	public class CommandAddThing : ICommand
	{
		public string Description(BoticoClient b)
		{
			return b.Loc.GetString("command.addThing.desc").Replace("%cmd", b.CmdSymbol + Names(b)[0]);
		}

		public string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.addThing.names").Split(',');
		}

		public BoticoResponse OnUse(CommandArgs args)
		{
			switch (args.Args.Length)
			{
				case 0:
					return Description(args.Botico);
				default:
					if (args.JoinedArgs.Length <= 24)
					{
						foreach (var v in args.Botico.CommandThings.Things)
						{
							if (v.Content == args.JoinedArgs)
								return args.Botico.Loc.GetString("command.addThing.exists");
						}
						args.Botico.CommandThings.Things.Add(new BoticoElement { Content = args.JoinedArgs, From = args.Sender });
						File.WriteAllText(CommandThings.PathThings, JsonConvert.SerializeObject(args.Botico.CommandThings.Things, Formatting.Indented));
						return args.Botico.Loc.GetString("command.addThing").Replace("%thing", args.JoinedArgs);
					}
					return args.Botico.Loc.GetString("command.addThing.tooLong");
			}
		}
	}
}
