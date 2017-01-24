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

		public static bool IsOwner(string sender, BoticoConfig config)
		{
			return config.Owners.ToList().Contains(sender);
		}
	}
}
