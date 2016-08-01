using System;
using System.Drawing;
using System.Net;
using Botico.Model;
using Newtonsoft.Json;
using PearXLib.GoogleApis;

namespace Botico
{
	public class CommandImage : ICommand
	{
		public string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.image.names").Split(',');
		}

		public BoticoResponse OnUse(CommandArgs args)
		{
			if (args.Args.Length == 0)
			{
				string sym = args.Botico.CommandSymbol == null ? "" : args.Botico.CommandSymbol.Value.ToString();
				return args.Botico.Loc.GetString("command.image.usage").Replace("%cmd", sym + args.Command);
			}

			var v = JsonConvert.DeserializeObject<GoogleImageSearch.RootObject>(GoogleUtils.SearchImages(args.JoinedArgs, args.Botico.Config.GoogleURLShortenerKey, "001650684090692243479:q5zk7hv6vqg"));
			string imgUrl = v.items[args.Random.Next(0, 10)].link;
			using (WebResponse resp = WebRequest.Create(imgUrl).GetResponse())
			{
				using (var stream = resp.GetResponseStream())
				{
					return new BoticoResponse { Image = Image.FromStream(stream) };
				}
			}
		}
	}
}
