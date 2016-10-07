using System.Collections.Generic;
using System.Drawing;

namespace Botico.Model
{
	public class BoticoResponse
	{
		public string Text { get; set; }
		public List<Image> Images { get; set; }

		public static implicit operator BoticoResponse(string s)
		{
			return new BoticoResponse { Text = s };
		}
	}
}
