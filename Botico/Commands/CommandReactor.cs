using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using Botico.Model;

namespace Botico
{
	public class CommandReactor : BCommand
	{
		public CommandReactor()
		{
			DescInHelp = true;
		}

		public override string[] Names(BoticoClient b)
		{
			return b.Loc["command.reactor.names"].Split(',');
		}

		public override string Description(BoticoClient b)
		{
			return b.Loc["command.reactor.desc"].Replace("%cmd", b.GetCommandName(this));
		}

		public override BoticoResponse OnUse(CommandArgs args)
		{
			using (WebClient cl = new WebClient())
			{
				try
				{
					string url = "http://joyreactor.cc/tag/" + HttpUtility.UrlEncode(args.JoinedArgs);
					int count = Convert.ToInt32(Regex.Match(cl.DownloadString(url), @"<a href='\/tag\/[\%a-zA-Z0-9]*\/([0-9]+)' class='next'>", RegexOptions.Singleline).Groups[1].Value) + 1;
					Regex rgx = new Regex(@"<div class=""image"">.{0,500}?<img src=""([^<>""""]*)""", RegexOptions.Singleline);
					string jrUrl = url + "/" + args.Random.Next(0, count);
					var matches = rgx.Matches(cl.DownloadString(jrUrl));
					string imgUrl = matches[args.Random.Next(0, matches.Count)].Groups[1].Value;
					return new BoticoResponse
					{
						Images = new List<BoticoImage>
					{
						new BoticoImage
						{
							Animated = imgUrl.EndsWith(".gif", StringComparison.Ordinal),
							Data = cl.DownloadData(imgUrl)
						}
					}
					};
				}
				catch(Exception e)
				{
					return e.Message;
				}
			}
		}
	}
}
