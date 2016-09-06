using System;
using System.Drawing;
using System.Net;
using Botico.Model;
using PearXLib.GoogleApis;

namespace Botico.Commands
{
	public class CommandBoobs : ICommand, IHidden
	{
		public int Index = 1;
		public static string Path => BoticoClient.Path + "boobsIndex.txt";

		public string Description(BoticoClient b)
		{
			return "( ͡° ͜ʖ ͡°)";
		}

		public string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.boobs.names").Split(',');
		}

		public BoticoResponse OnUse(CommandArgs args)
		{
			try
			{
				var v = GoogleUtils.SearchImages("сиськи", args.Botico.Config.GoogleApiKey, "001650684090692243479:q5zk7hv6vqg", Index);
				string imgUrl = v.items[0].link;
				Index++;
				System.IO.File.WriteAllText(Path, Index.ToString());

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
