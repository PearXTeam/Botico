using System.Collections.Generic;
using Botico.Model;

namespace Botico
{
	public class CommandQuestion : BCommand
	{
		public static string PathQuestions = BoticoClient.Path + "questions.json";

		public Dictionary<string, BoticoElement> Questions = new Dictionary<string, BoticoElement>();
		public Dictionary<string, string> QuestionCache = new Dictionary<string, string>();

		public override string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.question.names").Split(',');
		}

		public override BoticoResponse OnUse(CommandArgs args)
		{
			string q = BoticoUtils.GetShortQuestion(args.JoinedArgs);
			QuestionCache[args.Sender.ID] = q;
			if (Questions.ContainsKey(q))
			{
				return Questions[q].Content;
			}
			return BoticoUtils.GenAnswer(args.Botico);
		}

		public override string Description(BoticoClient b)
		{
			return b.Loc.GetString("command.question.desc");
		}
	}
}
