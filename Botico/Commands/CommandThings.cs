using System.Text;
using System.Collections.Generic;
using Botico.Model;

namespace Botico.Commands
{
	public class CommandThings : ICommand
	{
		public List<BoticoElement> Things = new List<BoticoElement>();
		public static string PathThings => BoticoClient.Path + "things.json";

		public string Description(BoticoClient b)
		{
			return b.Loc.GetString("command.things.desc");
		}

		public string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.things.names").Split(',');
		}

		public BoticoResponse OnUse(CommandArgs args)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(args.Botico.Loc.GetString("command.things"));
			foreach (var t in Things)
			{
				sb.Append(t.Content);
				sb.Append(", ");
			}
			sb.Remove(sb.Length - 2, 2);
			sb.Append(".");
			return sb.ToString();
		}
	}
}
