using System.IO;
using System.Net;
using System.Text;
using Botico.Model;
using PearXLib;

namespace Botico.Commands
{
	public class CommandDictionary : ICommand
	{
		public string[] Words;

		public string Description(BoticoClient b)
		{
			return b.Loc.GetString("command.dictionary.desc");
		}

		public string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.dictionary.names").Split(',');
		}

		public BoticoResponse OnUse(CommandArgs args)
		{
			if (args.Args.Length > 0 && args.Args[0] == args.Botico.Loc.GetString("command.dictionary.list"))
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
			if (args.Args.Length > 0 && args.Args[0] == args.Botico.Loc.GetString("command.dictionary.random"))
			{
				return Words[args.Random.Next(0, Words.Length)];
			}

			string cmdSymbol = args.Botico.CommandSymbol == null ? "" : args.Botico.CommandSymbol.ToString();
			return args.Botico.Loc.GetString("command.dictionary.usage").Replace("%cmd", cmdSymbol + args.Command);
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
