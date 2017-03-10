using System.Collections.Generic;
using System.IO;
using Botico.Model;

namespace Botico.Commands
{
	public class CustomCommand : BCommand
	{
		public string[] Nms { get; set; }
		public string Text { get; set; } 
		public string[] ImagesPath { get; set; }
		public string Desc { get; set; }

		public CustomCommand(string[] names, string desc, string txt, string[] images, bool hidden)
		{
			Nms = names;
			Text = txt;
			ImagesPath = images;
			Hidden = hidden;
			Desc = desc;
		}

		public override string[] Names(BoticoClient b)
		{
			return Nms;
		}

		public override string Description(BoticoClient b)
		{
			return Desc;
		}

		public override BoticoResponse OnUse(CommandArgs args)
		{
			List<BoticoImage> imgs = new List<BoticoImage>();
			if (ImagesPath != null)
			{
				if (ImagesPath.Length > 0)
				{
					foreach(string path in ImagesPath)
					{
						string absPath = BoticoClient.Path + path;
						imgs.Add(new BoticoImage
						{
							Animated = absPath.EndsWith(".gif", System.StringComparison.Ordinal),
							Data = File.ReadAllBytes(absPath)
						});
					}
				}
			}
			return new BoticoResponse { Images = imgs, Text = Text };
		}
	}
}
