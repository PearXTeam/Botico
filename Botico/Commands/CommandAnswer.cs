using System;
using System.IO;
using Botico.Model;
using Newtonsoft.Json;

namespace Botico
{
	public class CommandAnswer : BCommand
	{
		public override string Description(BoticoClient b)
		{
			return b.Loc.GetString("command.answer.desc");
		}

		public override string[] Names(BoticoClient b)
		{
			return b.Loc.GetString("command.answer.names").Split(',');
		}

		public override BoticoResponse OnUse(CommandArgs args)
		{
			if (args.Args.Length == 0)
				return args.Botico.Loc.GetString("command.answer.incorrectUsage");
			if (args.Botico.CommandQuestion.QuestionCache.ContainsKey(args.Sender))
			{
				string q = args.Botico.CommandQuestion.QuestionCache[args.Sender];
				if (args.Botico.CommandQuestion.Questions.ContainsKey(q))
				{
					if (BoticoUtils.IsOwner(args.Botico.CommandQuestion.Questions[q].From, args.Botico.Config))
					{
						if (!BoticoUtils.IsOwner(args.Sender, args.Botico.Config))
							return args.Botico.Loc.GetString("command.answer.notPermitted");
					}
				}
				args.Botico.CommandQuestion.Questions[q] = new BoticoElement { Content = args.JoinedArgs, From = args.Sender };
				File.WriteAllText(CommandQuestion.PathQuestions, JsonConvert.SerializeObject(args.Botico.CommandQuestion.Questions, Formatting.Indented));
				return args.Botico.Loc.GetString("command.answer");
			}
			return args.Botico.Loc.GetString("command.answer.notAsked");
		}
	}
}
