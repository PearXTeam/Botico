using System;

namespace Botico.Model
{
	public class BoticoElement
	{
		public BoticoElement(string context, string from)
		{
			Content = context;
			From = from;
			Time = DateTime.Now;
		}
		public string Content { get; set; }
		public string From { get; set; }
		public DateTime Time { get; set; }
	}
}
