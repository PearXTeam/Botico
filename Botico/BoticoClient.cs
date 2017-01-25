using System;
using System.Collections.Generic;
using System.IO;
using Botico.Commands;
using Botico.Model;
using Botico.Model.CustomCommands;
using Newtonsoft.Json;
using PearXLib;

namespace Botico
{
	/// <summary>
	/// Botico client.
	/// </summary>
	public class BoticoClient
	{
		public static string Path = AppDomain.CurrentDomain.BaseDirectory + "/Botico/";
		public static string PathLangs = Path + "langs/";
		public static string PathConfig = Path + "config.json";
		public static string PathCustomCommandsConfig = Path + "customcommands.json";

		public BoticoClientProvider Provider { get; set; }
		public string CmdSymbol => Config.CommandSymbol == null ? "" : Config.CommandSymbol.Value.ToString();

		public Localization Loc;
		public BoticoConfig Config;
		public List<BCommand> Commands = new List<BCommand>();
		public Random Rand = new Random();
		public Logging Log;

		public CommandThings CommandThings = new CommandThings();
		public CommandQuestion CommandQuestion = new CommandQuestion();
		public CommandDictionary CommandDict = new CommandDictionary();

	    /// <summary>
	    /// Initializes a new instance of the <see cref="T:Botico.BoticoClient"/> class.
	    /// </summary>
		/// <param name="prov">Botico client provider.</param>
		public BoticoClient(BoticoClientProvider prov)
		{
			Provider = prov;
			Log = new Logging(Path + "logs/" + PXL.GetDateTimeNow() + ".log", true);

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
			Commands.Add(new CommandWolfram());
			Commands.Add(new CommandWho());
		}

		/// <summary>
		/// Send command to the Botico.
		/// </summary>
		/// <returns>The commmand return message.</returns>
		/// <param name="command">Command.</param>
		/// <param name="sender">Command sender.</param>
		/// <param name="inGroupChat">Is command sent in group chat?</param>
		/// <param name="groupChatMembers">Group chat members. Can be null.</param>
		public BoticoResponse UseCommand(string command, CommandSender sender, bool inGroupChat, CommandSender[] groupChatMembers)
		{
			if (string.IsNullOrEmpty(command)) return EmptyResponse;

			if (Config.CommandSymbol != null)
			{
				if (command[0] != Config.CommandSymbol) return EmptyResponse;
				command = command.Remove(0, 1);
				if (string.IsNullOrEmpty(command)) return EmptyResponse;
				if (command[0] == ' ')
					command = command.Remove(0, 1);
			}
			foreach (BCommand cmd in Commands)
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
					var resp =  cmd.OnUse(new CommandArgs {
						FullCommand = command,
						InGroupChat = inGroupChat,
						Sender = sender,
						Command = cmdName,
						JoinedArgs = args,
						Args = useArgs ? args.Split(' ') : new string[] { },
						IsOwner = BoticoUtils.IsOwner(sender, Config), 
						Botico = this,
						Random = Rand,
						GroupChatMembers = groupChatMembers
					});
					if (!string.IsNullOrEmpty(resp.Text))
					{
						if (resp.Text.Length > Config.MessageTextLimit)
							resp.Text = resp.Text.Substring(0, resp.Text.Length - 3) + "...";
					}
					return resp;
				}
			}
			return EmptyResponse;
		}

		/// <summary>
		/// Initialize Botico.
		/// </summary>
		public void Init()
		{
			Log.Add("Extracting default languages...", "Botico", LogType.Info);
			Directory.CreateDirectory(PathLangs);
			File.WriteAllBytes(PathLangs + "ru_RU.lang", ResourceUtils.GetFromResources("Botico.EmbeddedLangs.ru_RU.lang"));
			File.WriteAllBytes(PathLangs + "ru_RU.langinfo", ResourceUtils.GetFromResources("Botico.EmbeddedLangs.ru_RU.langinfo"));
			File.WriteAllBytes(PathLangs + "en_US.lang", ResourceUtils.GetFromResources("Botico.EmbeddedLangs.en_US.lang"));
			File.WriteAllBytes(PathLangs + "en_US.langinfo", ResourceUtils.GetFromResources("Botico.EmbeddedLangs.en_US.langinfo"));

			Log.Add("Loading config files...", "Botico", LogType.Info);
			if (File.Exists(PathConfig))
				Config = JsonConvert.DeserializeObject<BoticoConfig>(File.ReadAllText(PathConfig));
			else
			{
				Config = new BoticoConfig
				{
					Language = "ru_RU",
					Owners = new string[] { "your_name", "your_friend_name" },
					GoogleApiKey = "put_your_key_here",
					WolframAppID = "put_your_appid_here",
					WikiSources = new WikiSource[]
					{
						new WikiSource {
							Name = "wikipedia",
							RandomURL = "https://en.wikipedia.org/wiki/Special:Random",
							URL = "https://en.wikipedia.org/wiki/" ,
							ApiPhp = "https://en.wikipedia.org/w/api.php",
							FriendlyName = "WikiPedia"
						}
					},
					Dictionaries = new BoticoDictionary[]
					{
						new BoticoDictionary {
							Name = "Russian",
							Type = "web",
							Path = "https://github.com/mrAppleXZ/TextDicts/raw/master/russian.txt"
						}
					},
					CommandSymbol = '!',
					MessageTextLimit = 4096,
					UseMarkdown = false,
					LinksInsteadOfImages = false,
					NewLines = true
				};
				File.WriteAllText(PathConfig, JsonConvert.SerializeObject(Config, Formatting.Indented));
			}

			if (File.Exists(CommandThings.PathThings))
				CommandThings.Things = JsonConvert.DeserializeObject<List<BoticoElement>>(File.ReadAllText(CommandThings.PathThings));

			if (File.Exists(CommandQuestion.PathQuestions))
				CommandQuestion.Questions = JsonConvert.DeserializeObject<Dictionary<string, BoticoElement>>(File.ReadAllText(CommandQuestion.PathQuestions));
			
			Log.Add("Loading dictionaries...", "Botico", LogType.Info);
			CommandDict.Init(this);

			Log.Add("Initializing custom commands...", "Botico", LogType.Info);
			if (File.Exists(PathCustomCommandsConfig))
			{
				CustomCommandsConfig cfgs = JsonConvert.DeserializeObject<CustomCommandsConfig>(File.ReadAllText(PathCustomCommandsConfig));
				foreach (var cfg in cfgs.CustomCommands)
				{
					Log.Add($"Adding custom command \"{cfg.Names[0]}\"...", "Botico", LogType.Info);
					Commands.Add(new CustomCommand(cfg.Names, cfg.Description, cfg.Text, cfg.ImagesPath, cfg.Hidden));
				}
			}
			else
			{
				Log.Add($"Custom commands config does not exists! Creating it.", "Botico", LogType.Info);
				File.WriteAllText(PathCustomCommandsConfig, JsonConvert.SerializeObject(new CustomCommandsConfig
				{
					CustomCommands = new CustomCommandCfg[]
					{
						new CustomCommandCfg
						{
							Names = new string[]{"customcommand", "cc"},
							Text = "Hello! I'm your custom command ;).",
							Description = "Sends an example custom command's text.",
							ImagesPath = null,
							Hidden = false
						},
						new CustomCommandCfg
						{
							Names = new string[]{"hiddencc"},
							Text = "",
							Description = "I'm a hidden custom command. Tsss...!",
							ImagesPath = new string[]{"image1.png", "image2.png"},
							Hidden = true
						}
					}
				}, Formatting.Indented));
			}

			Log.Add("Done loading Botico!", "Botico", LogType.Info);
			Loc = new Localization(PathLangs, Config.Language, "ru_RU");
		}

		/// <summary>
		/// Gets a command from a string.
		/// </summary>
		/// <returns>A command.</returns>
		/// <param name="s">Input string.</param>
		public BCommand GetCommandFromString(string s)
		{
			foreach (BCommand cmd in Commands)
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

		public string GetCommandName(BCommand cmd)
		{
			return CmdSymbol + cmd.Names(this)[0];
		}

		/// <summary>
		/// An empty Botico response.
		/// </summary>
		public static BoticoResponse EmptyResponse => new BoticoResponse { Images = null, Text = "" };
	}
}