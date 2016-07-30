using System;
using System.Net;
using System.Text;
using Botico.Model;
using Newtonsoft.Json;
using PearXLib;

namespace Botico.Commands
{
	public class CommandWiki : ICommand
	{
		public string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.wiki.names").Split(',');
		}

		public string OnUse(CommandArgs args)
		{
			string cmdSymbol = args.Botico.CommandSymbol == null ? "" : args.Botico.CommandSymbol.ToString();
			switch (args.Args.Length)
			{
				case 1:
					if (args.Args[0] == args.Botico.Loc.GetString("command.wiki.random"))
					{
						var wikiSource = args.Botico.Config.WikiSources[args.Random.Next(0, args.Botico.Config.WikiSources.Length)];
						HttpWebRequest req = (HttpWebRequest)WebRequest.Create(wikiSource.RandomURL);
						req.AllowAutoRedirect = false;
						req.Method = "HEAD";
						using (var resp = req.GetResponse())
						{
							string s = resp.Headers["Location"];
							string shorten = JsonConvert.DeserializeObject<GoogleShortener>(WebUtils.ShortURL(s, args.Botico.Config.GoogleURLShortenerKey)).id;
							string name = Uri.UnescapeDataString(s).Substring(wikiSource.URL.Length) + " - " + wikiSource.FriendlyName;
							if (args.Botico.UseMarkdown)
								return shorten + " - " + "```\n" + name + "\n```";
							return shorten + " - " + name;
						}
					}
					else if (args.Args[0] == args.Botico.Loc.GetString("command.wiki.list"))
					{
						StringBuilder sb = new StringBuilder();
						sb.Append(args.Botico.Loc.GetString("command.wiki.wikis"));
						foreach (WikiSource src in args.Botico.Config.WikiSources)
						{
							sb.Append(src.FriendlyName);
							sb.Append(", ");
						}
						sb.Remove(sb.Length - 2, 2);
						sb.Append(".");
						return sb.ToString();
					}
					return args.Botico.Loc.GetString("command.wiki.usage").Replace("%cmd", cmdSymbol + args.Command);
				default:
					return args.Botico.Loc.GetString("command.wiki.usage").Replace("%cmd", cmdSymbol + args.Command);
			}
		}
	}
}
