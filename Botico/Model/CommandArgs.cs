using System;

namespace Botico.Model
{
	public class CommandArgs
	{
		/// <summary>
		/// Entered command. For example: if full command is "/set 24", Command must be "set".
		/// </summary>
		public string Command { get; set; }

		/// <summary>
		/// Full command.
		/// </summary>
		public string FullCommand { get; set; }

		/// <summary>
		/// Command sender.
		/// </summary>
		public string Sender { get; set; }

		/// <summary>
		/// Joined arguments. For example: if full command is "/question How are you?", JoinedArgs must be "How are you?".
		/// </summary>
		/// <value>The joined arguments.</value>
		public string JoinedArgs { get; set; }

		/// <summary>
		/// Arguments. For example: if full command is "/random 45 50", Args must be [0] - 45, [1] - 50.
		/// </summary>
		/// <value>The arguments.</value>
		public string[] Args { get; set; }

		/// <summary>
		/// Is command sent in group chat?
		/// </summary>
		public bool InGroupChat { get; set; }

		/// <summary>
		/// Is command sender - owner?
		/// </summary>
		public bool IsOwner { get; set; }

		/// <summary>
		/// <see cref="T:Botico.BoticoClient"/> instance.
		/// </summary>
		public BoticoClient Botico { get; set; }

		/// <summary>
		/// <see cref="T:System.Random"/> instance.
		/// </summary>
		public Random Random { get; set; }

		/// <summary>
		/// Other object.
		/// </summary>
		public object Other { get; set; }
	}
}
