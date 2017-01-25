using Botico.Model;

namespace Botico
{
	public class CommandWho : BCommand
	{
		public CommandWho()
		{
			DescInHelp = true;
		}

		public override string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.who.names").Split(',');
		}

		public override string Description(BoticoClient b)
		{
			return b.Loc.GetString("command.who.desc").Replace("%cmd", b.GetCommandName(this));
		}

		public override BoticoResponse OnUse(CommandArgs args)
		{
			if (args.GroupChatMembers != null)
			{
				var usr = args.GroupChatMembers[args.Random.Next(0, args.GroupChatMembers.Length)];
				return args.Botico.Provider.CreateMention(usr) + " - " + args.JoinedArgs.Replace("?", "") + ".";
			}
			return args.Botico.Loc.GetString("command.who.notgroupchat");
		}
	}
}
