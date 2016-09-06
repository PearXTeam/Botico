using System;
using System.Collections.Generic;
using System.IO;
using Botico.Commands;
using Botico.Model;
using Newtonsoft.Json;
using PearXLib;

namespace Botico
{
	public class BoticoClient
	{
		public static string Path = AppDomain.CurrentDomain.BaseDirectory + "/Botico/";
		public static string PathLangs = Path + "langs/";
		public static string PathConfig = Path + "config.json";
		public const string Version = "1.6.0";

		public char? CommandSymbol { get; set; }
		public string ClientName { get; set; }
		public bool UseMarkdown { get; set; }
		public bool ShorterMessages { get; set; }
		public bool LinksInsteadImages { get; set; }

		public Localization Loc;
		public BoticoConfig Config;
		public List<ICommand> Commands = new List<ICommand>();
		public Random Rand = new Random();
		public Logging Log;

		public CommandThings CommandThings = new CommandThings();
		public CommandQuestion CommandQuestion = new CommandQuestion();
		public CommandDictionary CommandDict = new CommandDictionary();
		public CommandBoobs CommandBoobs = new CommandBoobs();

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Botico.BoticoClient"/> class.
		/// </summary>
		/// <param name="cmdSymbol">Your command start symbol. For example '/'.</param>
		/// <param name="clientName">Botico client name. For example "BotConsole for Windows".</param>
		/// <param name="useMarkdown">Use markdown?</param>
		/// <param name="outLogToConsole">Out log to console?</param>
		/// <param name="shorterMessages"></param>Use shorter messages? Recommended for IRC.</param>
		public BoticoClient(char? cmdSymbol, string clientName, bool useMarkdown, bool outLogToConsole, bool shorterMessages, bool linksInsteadImages)
		{
			CommandSymbol = cmdSymbol;
			ClientName = clientName;
			UseMarkdown = useMarkdown;
			ShorterMessages = shorterMessages;
			LinksInsteadImages = linksInsteadImages;
			Log = new Logging(Path + "logs/" + PXL.GetDateTimeNow() + ".log", outLogToConsole);

			Commands.Add(new CommandHelp());
			Commands.Add(new CommandBotico());
			Commands.Add(new CommandRandom());
			Commands.Add(CommandThings);
			Commands.Add(new CommandAddThing());
			Commands.Add(new CommandWiki());
			Commands.Add(new CommandRussianRoulette());
			Commands.Add(CommandQuestion);
			Commands.Add(new CommandAnswer());
			Commands.Add(new CommandImage());
			Commands.Add(CommandDict);
			Commands.Add(new CommandAbout());
			Commands.Add(new CommandTurn());
			Commands.Add(CommandBoobs);
		}

		/// <summary>
		/// Send command to the Botico.
		/// </summary>
		/// <returns>The commmand return message.</returns>
		/// <param name="command">Command.</param>
		/// <param name="sender">Command sender.</param>
		/// <param name="inGroupChat">Is command sent in group chat?</param>
		public BoticoResponse UseCommand(string command, string sender, bool inGroupChat)
		{
			if (string.IsNullOrEmpty(command)) return EmptyResponse;

			if (CommandSymbol != null)
			{
				if (command[0] != CommandSymbol) return EmptyResponse;
				command = command.Remove(0, 1);
				if (string.IsNullOrEmpty(command)) return EmptyResponse;
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
			return EmptyResponse;
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
					Owners = new string[] { "your_name", "your_friend_name" },
					WikiSources = new WikiSource[]
					{
						new WikiSource {
							Name = "wikipedia",
							RandomURL = "https://en.wikipedia.org/wiki/Special:Random",
							URL = "https://en.wikipedia.org/wiki/" ,
							ApiPhp = "https://en.wikipedia.org/w/api.php",
							FriendlyName = "WikiPedia"
						} },
					GoogleApiKey = "put_your_key_here",
					Dictionaries = new BoticoDictionary[]
					{
						new BoticoDictionary {
							Name = "Russian",
							Type = "web",
							Path = "https://github.com/mrAppleXZ/TextDicts/raw/master/russian.txt"
						}
					}
				};
				File.WriteAllText(PathConfig, JsonConvert.SerializeObject(Config, Formatting.Indented));
			}

			if (File.Exists(CommandThings.PathThings))
				CommandThings.Things = JsonConvert.DeserializeObject<List<BoticoElement>>(File.ReadAllText(CommandThings.PathThings));

			if (File.Exists(CommandQuestion.PathQuestions))
				CommandQuestion.Questions = JsonConvert.DeserializeObject<Dictionary<string, BoticoElement>>(File.ReadAllText(CommandQuestion.PathQuestions));

			if (File.Exists(CommandBoobs.Path))
				CommandBoobs.Index = Convert.ToInt32(File.ReadAllText(CommandBoobs.Path));

			CommandDict.Init(this);

			Loc = new Localization(PathLangs, Config.Language, "ru_RU");
		}

		public string GetCommandSymbol()
		{
			return CommandSymbol == null ? "" : CommandSymbol.Value.ToString();
		}

		public ICommand GetCommandFromString(string s)
		{
			foreach (ICommand cmd in Commands)
			{
				foreach (string cmdName in cmd.Names(this))
				{
					if (!s.ToLower().StartsWith(cmdName, StringComparison.Ordinal)) continue;
					if (s.Length != cmdName.Length)
					{
						if (s[cmdName.Length] != ' ') continue;
					}
					return cmd;
				}
			}
			return null;
		}

		public static BoticoResponse EmptyResponse => new BoticoResponse { Image = null, Text = "" };

		public event BoticoSendMessageHandler Send;

		public void PerformSend(BoticoSendMessageEventArgs e)
		{
			if (Send != null)
				Send(e);
			else
				Log.Add("[Botico] Message sending not implemented!", LogType.Warning);
				
		}
	}

	public delegate void BoticoSendMessageHandler(BoticoSendMessageEventArgs e);
}