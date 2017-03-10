using System.Collections.Generic;

namespace Botico.Model
{
	/// <summary>
	/// Configuration file for Botico.
	/// </summary>
	public class BoticoConfig
	{
		public string Language { get; set; }
		public string[] Owners { get; set; }
		public string GoogleApiKey { get; set; }
		public string WolframAppID { get; set; }
		public WikiSource[] WikiSources { get; set; }
		public BoticoDictionary[] Dictionaries { get; set; }
		public char? CommandSymbol { get; set; }
		public bool UseMarkdown { get; set; }
		public long MessageTextLimit { get; set; }
		public bool LinksInsteadOfImages { get; set; }
		public bool NewLines { get; set; }
		public Dictionary<string, string> Aliases { get; set; }
	}
}
