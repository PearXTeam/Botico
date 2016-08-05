namespace Botico.Model
{
	public class BoticoDictionary
	{
		/// <summary>
		/// Name of the dictionary.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Type of the dictionary. Can be "local" and "web".
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// Path to the dictionary file. Can be "https://github.com/mrAppleXZ/TextDicts/raw/master/russian.txt" if web, and "/home/user/dict.txt" or "C:\Dicts\dictionary.txt" if local.
		/// </summary>
		public string Path { get; set; }
	}
}
