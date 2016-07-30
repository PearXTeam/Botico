using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Botico.Commands;
using Botico.Model;
using Newtonsoft.Json;
using PearXLib;

namespace Botico
{
	public class BoticoClient
	{
		public static string Path => AppDomain.CurrentDomain.BaseDirectory + "/Botico/";
		public static string PathLangs => Path + "langs/";
		public static string PathConfig => Path + "config.json";
		public static string Version => "1.0.0";

		public char? CommandSymbol { get; set; }
		public string ClientName { get; set; }
		public bool UseMarkdown { get; set; }
		public Localization Loc;
		public BoticoConfig Config;
		public List<ICommand> Commands = new List<ICommand>();
		public Random Rand = new Random();

		public CommandThings CommandThings = new CommandThings();
		public CommandQuestion CommandQuestion = new CommandQuestion();

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Botico.BoticoClient"/> class.
		/// </summary>
		/// <param name="cmdSymbol">Your command start symbol. For example '/'.</param>
		/// <param name="clientName">Botico client name. For example "BotConsole for Windows".</param>
		/// <param name="useMarkdown">Use markdown?</param>
		public BoticoClient(char? cmdSymbol, string clientName, bool useMarkdown)
		{
			CommandSymbol = cmdSymbol;
			ClientName = clientName;
			UseMarkdown = useMarkdown;

			Commands.Add(new CommandHelp());
			Commands.Add(new CommandBotico());
			Commands.Add(new CommandRandom());
			Commands.Add(CommandThings);
			Commands.Add(new CommandAddThing());
			Commands.Add(new CommandWiki());
			Commands.Add(new CommandRussianRoulette());
			Commands.Add(CommandQuestion);
			Commands.Add(new CommandAnswer());
		}

		/// <summary>
		/// Send command to the Botico.
		/// </summary>
		/// <returns>The commmand return message.</returns>
		/// <param name="command">Command.</param>
		/// <param name="sender">Command sender.</param>
		/// <param name="inGroupChat">Is command sent in group chat?</param>
		public string UseCommand(string command, string sender, bool inGroupChat)
		{
			if (string.IsNullOrEmpty(command)) return "";

			if (CommandSymbol != null)
			{
				if (command[0] != CommandSymbol) return "";
				command = command.Remove(0, 1);
				if (string.IsNullOrEmpty(command)) return "";
				if (command[0] == ' ')
					command = command.Remove(0, 1);
			}

			foreach (ICommand cmd in Commands)
			{
				foreach (string cmdName in cmd.Names(this))
				{
					bool useArgs = false;
					if (!command.ToLower().StartsWith(cmdName, StringComparison.Ordinal)) continue;
					if (command.Length != cmdName.Length)
					{
						if (command[cmdName.Length] != ' ') continue;
						useArgs = true;
					}
					string args = useArgs ? command.Substring(cmdName.Length + 1) : "";
					return cmd.OnUse(new CommandArgs {
						FullCommand = command,
						InGroupChat = inGroupChat,
						Sender = sender,
						Command = cmdName,
						JoinedArgs = args,
						Args = useArgs ? args.Split(' ') : new string[] { },
						IsOwner = BoticoUtils.IsOwner(sender, Config), 
						Botico = this,
						Random = Rand
					});
				}
			}
			return "";
		}

		/// <summary>
		/// Initialize Botico.
		/// </summary>
		public void Init()
		{
			Directory.CreateDirectory(PathLangs);
			File.WriteAllText(PathLangs + "ru_RU.lang", EmbeddedLangs.ru_RU);
			File.WriteAllText(PathLangs + "ru_RU.langinfo", EmbeddedLangs.ru_RU_info);

			if (File.Exists(PathConfig))
				Config = JsonConvert.DeserializeObject<BoticoConfig>(File.ReadAllText(PathConfig));
			else
			{
				Config = new BoticoConfig
				{
					Language = "ru_RU",
					Owners = new string[] { "183388312" },
					WikiSources = new WikiSource[] { new WikiSource
						{
							Name = "wikipedia",
							RandomURL = "https://en.wikipedia.org/wiki/Special:Random",
							URL = "https://en.wikipedia.org/wiki/" ,
							FriendlyName = "WikiPedia"
						} },
					GoogleURLShortenerKey = "put_your_key_here"
				};
			}

			if (File.Exists(CommandThings.PathThings))
				CommandThings.Things = JsonConvert.DeserializeObject<List<BoticoElement>>(File.ReadAllText(CommandThings.PathThings));

			if (File.Exists(CommandQuestion.PathQuestions))
				CommandQuestion.Questions = JsonConvert.DeserializeObject<Dictionary<string, BoticoElement>>(File.ReadAllText(CommandQuestion.PathQuestions));

			Loc = new Localization(PathLangs, Config.Language, "ru_RU");
		}

		/// <summary>
		/// End Botico.
		/// </summary>
		public void End()
		{
			File.WriteAllText(PathConfig, JsonConvert.SerializeObject(Config, Formatting.Indented));
		}
	}
}