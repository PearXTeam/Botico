using System.Collections.Generic;
using System.Text;
using Botico.Model;
using PearXLib.WebServices.Wolfram;

namespace Botico.Commands
{
	public class CommandWolfram : BCommand
	{
		public CommandWolfram()
		{
			DescInHelp = true;
		}

		public override string Description(BoticoClient b)
		{
			return b.Loc["command.wolfram.desc"].Replace("%cmd", b.GetCommandName(this));
		}

		public override string[] Names(BoticoClient b)
		{
			return b.Loc["command.wolfram.names"].Split(',');
		}

		public override BoticoResponse OnUse(CommandArgs args)
		{
			switch (args.Args.Length)
			{
				case 0:
					return Description(args.Botico);
				default:
					if (!args.Botico.Config.LinksInsteadOfImages)
					{
						var v = Wolfram.Process(args.JoinedArgs, args.Botico.Config.WolframAppID);
						StringBuilder sb = new StringBuilder();
						List<BoticoImage> lst = new List<BoticoImage>();

						int num = 0;
						foreach (WolframPod pod in v)
						{
							for (int i = 0; i < pod.Images.Length; i++)
							{
								num++;
								sb.AppendLine(num + " - " + pod.Title);
								lst.Add(new BoticoImage
								{
									Data = pod.Images[i].Image,
									Animated = false
								});
							}
						}
						return new BoticoResponse { Images = lst, Text = sb.ToString() };
					}
					return null;
			}
		}
	}
}
