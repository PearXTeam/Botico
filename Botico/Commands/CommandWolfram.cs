using System.Collections.Generic;
using System.Drawing;
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
			return b.Loc.GetString("command.wolfram.desc").Replace("%cmd", b.GetCommandName(this));
		}

		public override string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.wolfram.names").Split(',');
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
						List<Image> lst = new List<Image>();

						int num = 0;
						foreach (WolframPod pod in v)
						{
							for (int i = 0; i < pod.Images.Length; i++)
							{
								num++;
								sb.AppendLine(num + " - " + pod.Title);
								lst.Add(pod.Images[i].Image);
							}
						}
						return new BoticoResponse { Images = lst, Text = sb.ToString() };
					}
					return null;
			}
		}
	}
}
