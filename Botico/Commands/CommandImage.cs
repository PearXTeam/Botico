using System;
using System.Collections.Generic;
using System.Net;
using Botico.Model;
using PearXLib.WebServices.GoogleApis;

namespace Botico
{
	public class CommandImage : BCommand
	{
		public CommandImage()
		{
			DescInHelp = true;
		}
		
		public override string Description(BoticoClient b)
		{
			return b.Loc["command.image.desc"].Replace("%cmd", b.GetCommandName(this));
		}

		public override string[] Names(BoticoClient b)
		{
			return b.Loc["command.image.names"].Split(',');
		}

		public override BoticoResponse OnUse(CommandArgs args)
		{
			switch (args.Args.Length)
			{
				case 0:
					return Description(args.Botico);
				default:
					try
					{
						var v = GoogleUtils.SearchImages(args.JoinedArgs, args.Botico.Config.GoogleApiKey, "001650684090692243479:q5zk7hv6vqg");
						var item = v.items[args.Random.Next(0, 10)];
						if (!args.Botico.Config.LinksInsteadOfImages)
						{
							List<BoticoImage> lst = new List<BoticoImage>();
							using (var client = new WebClient())
							{
								lst.Add(new BoticoImage
								{
									Animated = item.link.EndsWith(".gif", StringComparison.Ordinal),
									Data = client.DownloadData(item.link)
								});
							}
							return new BoticoResponse { Images = lst };
						}
						return item.link + " - " + item.title;
					}
					catch (Exception ex)
					{
						return ex.Message;
					}
			}
		}
	}
}
