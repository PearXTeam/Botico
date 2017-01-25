using System.Linq;
using Botico.Model;

namespace Botico
{
	public static class BoticoUtils
	{
		public static string GenAnswer(BoticoClient botico)
		{
			string[] s = botico.Loc.GetString("answers").Split(';');
			return s[botico.Rand.Next(0, s.Length)];
		}

		public static string GetShortQuestion(string full)
		{
			return full.ToLower().Replace(" ", "").Replace("?", "").Replace("!", "").Replace(",", "").Replace(".", "");
		}

		public static bool IsOwner(CommandSender sender, BoticoConfig config)
		{
			return IsOwner(sender.ID, config);
		}

		public static bool IsOwner(string id, BoticoConfig cfg)
		{
			return cfg.Owners.ToList().Contains(id);
		}
	}
}
