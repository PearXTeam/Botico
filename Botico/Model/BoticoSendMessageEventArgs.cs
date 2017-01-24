namespace Botico.Model
{
	public class BoticoSendMessageEventArgs
	{
		public bool ToGroupChat { get; set; }
		public string To { get; set; }
		public BoticoResponse Response { get; set; }
	}
}
