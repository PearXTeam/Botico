using System.Collections.Generic;
using System.IO;
using System.Text;
using Botico.Model;
using PearXLib;

namespace Botico
{
	public class CommandWall : BCommand
	{
		public List<BoticoElement> Posts = new List<BoticoElement>();
		public static string PathPosts => Path.Combine(BoticoClient.Path, "wall.json");

		public CommandWall()
		{
			DescInHelp = true;
		}
		public override string[] Names(BoticoClient b)
		{
			return b.Loc["command.wall.names"].Split(',');
		}

		public override string Description(BoticoClient b)
		{
			return b.Loc["command.wall.desc"].Replace("%cmd", Names(b)[0]);
		}

		public override BoticoResponse OnUse(CommandArgs args)
		{
			if (args.Args.Length > 0)
			{
				string postCmd = args.Botico.Loc["command.wall.post"];
				if (args.Args[0] == postCmd)
				{
					string msg = args.JoinedArgs.Substring(postCmd.Length + 1);
					Posts.Add(new BoticoElement(msg, args.Sender.ID));
					File.WriteAllText(PathPosts, Posts.ToJson());
					return args.Botico.Loc["command.wall.sent"];
				}
			}
			StringBuilder sb = new StringBuilder();
			foreach (var post in Posts)
			{
				sb.AppendLine(post.Time.ToString("G") + " " + post.Content);
			}
			return sb.ToString();
		}
	}
}
