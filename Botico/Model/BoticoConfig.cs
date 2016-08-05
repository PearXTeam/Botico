namespace Botico.Model
{
	public class BoticoConfig
	{
		public string Language { get; set; }
		public string[] Owners { get; set; }
		public WikiSource[] WikiSources { get; set; }
		public string GoogleApiKey { get; set; }
		public BoticoDictionary[] Dictionaries { get; set; }
	}
}
