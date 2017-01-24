using System.IO;
using System.Net;
using System.Text;
using Botico.Model;
using PearXLib;

namespace Botico.Commands
{
	public class CommandDictionary : BCommand
	{
		public string[] Words;

		public CommandDictionary()
		{
			DescInHelp = true;
		}

		public override string Description(BoticoClient b)
		{
			return b.Loc.GetString("command.dictionary.desc").Replace("%cmd", b.GetCommandName(this));
		}

		public override string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.dictionary.names").Split(',');
		}

		public override BoticoResponse OnUse(CommandArgs args)
		{
			switch (args.Args.Length)
			{
				case 0:
					return Description(args.Botico);
				default:
					if (args.Args[0] == args.Botico.Loc.GetString("command.dictionary.list"))
					{
						StringBuilder sb = new StringBuilder();
						sb.Append(args.Botico.Loc.GetString("command.dictionary.dicts"));
						foreach (var v in args.Botico.Config.Dictionaries)
						{
							sb.Append(v.Name);
							sb.Append(", ");
						}
						sb.Remove(sb.Length - 2, 2);
						sb.Append(".");
						return sb.ToString();
					}
					if (args.Args[0] == args.Botico.Loc.GetString("command.dictionary.random"))
					{
						return Words[args.Random.Next(0, Words.Length)];
					}
					return Description(args.Botico);
			}
		}

		public void Init(BoticoClient b)
		{
			foreach (var dict in b.Config.Dictionaries)
			{
				switch (dict.Type)
				{
					case "web":
						using (WebClient c = new WebClient())
						{
							Words = PXL.GetArrayFromString(c.DownloadString(dict.Path));
						}
						break;
					case "local":
						Words = File.ReadAllLines(dict.Path);
						break;
				}
			}
		}
	}
}
