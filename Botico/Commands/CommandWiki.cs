using System.Text;
using Botico.Model;
using PearXLib;
using PearXLib.Wiki;
using System.Linq;
using System;
using PearXLib.WebServices.GoogleApis;

namespace Botico.Commands
{
	public class CommandWiki : BCommand
	{
		public CommandWiki()
		{
			DescInHelp = true;
		}

		public override string Description(BoticoClient b)
		{
			return b.Loc["command.wiki.desc"].Replace("%cmd", b.GetCommandName(this));
		}

		public override string[] Names(BoticoClient b)
		{
			return b.Loc["command.wiki.names"].Split(',');
		}

		public override BoticoResponse OnUse(CommandArgs args)
		{
			string reference = args.Botico.Loc["command.wiki.reference"];
			switch (args.Args.Length)
			{
				case 0:
					return Description(args.Botico);
				default:
					if (args.Args[0] == args.Botico.Loc["command.wiki.random"])
					{
						var wikiSource = args.Botico.Config.WikiSources[args.Random.Next(0, args.Botico.Config.WikiSources.Length)];
						string s = WebUtils.GetRedirectUrl(wikiSource.RandomURL);

						string shorten = GoogleUtils.ShortURL(s, args.Botico.Config.GoogleApiKey).id;
						string name = Uri.UnescapeDataString(s).Substring(wikiSource.URL.Length) + " - " + wikiSource.FriendlyName;
						if (args.Botico.Config.UseMarkdown)
							return shorten + " - " + "```" + name + "```";
						return shorten + " - " + name;
					}
					if (args.Args[0] == args.Botico.Loc["command.wiki.list"])
					{
						StringBuilder sb = new StringBuilder();
						sb.Append(args.Botico.Loc["command.wiki.wikis"]);
						foreach (WikiSource src in args.Botico.Config.WikiSources)
						{
							sb.Append(src.FriendlyName);
							sb.Append(", ");
						}
						sb.Remove(sb.Length - 2, 2);
						sb.Append(".");
						return sb.ToString();
					}
					if (args.Args[0] == reference)
					{
						if (args.Args.Length >= 2)
						{
							foreach (WikiSource s in args.Botico.Config.WikiSources)
							{
								var v = WikiUtils.GetSummary(s.ApiPhp, args.JoinedArgs.Substring((reference + " ").Length));
								if (v.query.pages.FirstOrDefault().Value.extract != null)
								{
									return v.query.pages.FirstOrDefault().Value.extract;
								}
							}
							return args.Botico.Loc["command.wiki.reference.notFound"];
						}
						return args.Botico.Loc["command.wiki.reference.error"];
					}
					return Description(args.Botico);
			}
		}
	}
}
