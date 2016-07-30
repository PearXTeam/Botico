using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Botico.Commands;
using Newtonsoft.Json;
using PearXLib;

namespace Botico
{
	public class BoticoClient
	{
		public static string Path => AppDomain.CurrentDomain.BaseDirectory + "/Botico/";
		public static string PathLangs => Path + "langs/";
		public static string PathConfig => Path + "config.json";

		public char? CommandSymbol { get; set; }
		public string ClientName { get; set; }
		public bool UseMarkdown { get; set; }
		public Localization Loc;
		public BoticoConfig Config;
		public List<ICommand> Commands = new List<ICommand>();

		public BoticoClient(char? cmdSymbol, string clientName, bool useMarkdown)
		{
			CommandSymbol = cmdSymbol;
			ClientName = clientName;
			UseMarkdown = useMarkdown;

			Commands.Add(new CommandHelp());
		}

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
					return cmd.OnUse(new CommandArgs { FullCommand = command, InGroupChat = inGroupChat, Sender = sender, Command = cmdName, JoinedArgs = args, Args = args.Split(' '), IsOwner = Config.Owners.ToList().Contains(sender), Botico = this });
				}
			}
			return "";
		}

		public void Init()
		{
			Directory.CreateDirectory(PathLangs);
			File.WriteAllText(PathLangs + "ru_RU.lang", EmbeddedLangs.ru_RU);
			File.WriteAllText(PathLangs + "ru_RU.langinfo", EmbeddedLangs.ru_RU_info);
			if (File.Exists(PathConfig))
			{
				Config = JsonConvert.DeserializeObject<BoticoConfig>(File.ReadAllText(PathConfig));
			}
			else
			{
				Config = new BoticoConfig { Language = "ru_RU", Owners = new string[] { "183388312" } };
			}
			Loc = new Localization(PathLangs, Config.Language, "ru_RU");
		}

		public void End()
		{
			File.WriteAllText(PathConfig, JsonConvert.SerializeObject(Config, Formatting.Indented));
		}
	}
}