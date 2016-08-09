using System;
using System.Drawing;
using System.Net;
using Botico.Model;
using PearXLib.GoogleApis;

namespace Botico
{
	public class CommandImage : ICommand
	{
		public string Description(BoticoClient b)
		{
			return b.Loc.GetString("command.image.desc");
		}

		public string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.image.names").Split(',');
		}

		public BoticoResponse OnUse(CommandArgs args)
		{
			switch (args.Args.Length)
			{
				case 0:
					return Description(args.Botico);
				default:
					try
					{
						var v = GoogleUtils.SearchImages(args.JoinedArgs, args.Botico.Config.GoogleApiKey, "001650684090692243479:q5zk7hv6vqg");
						string imgUrl = v.items[args.Random.Next(0, 10)].link;
						if (!args.Botico.LinksInsteadImages)
						{
							using (WebResponse resp = WebRequest.Create(imgUrl).GetResponse())
							{
								using (var stream = resp.GetResponseStream())
								{
									return new BoticoResponse { Image = Image.FromStream(stream) };
								}
							}
						}
						return imgUrl;
					}
					catch (Exception ex)
					{
						return ex.Message;
					}
			}
		}
	}
}
