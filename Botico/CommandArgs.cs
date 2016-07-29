using System;
namespace Botico
{
	public class CommandArgs
	{
		public string Command { get; set; }
		public string FullCommand { get; set; }
		public string Sender { get; set; }
		public string JoinedArgs { get; set; }
		public string[] Args { get; set; }
		public bool InGroupChat { get; set; }
		public bool IsOwner { get; set; }
		public BoticoClient Botico { get; set; }
	}
}
