using System.Collections.Generic;

namespace Botico.Model
{
	public class BoticoResponse
	{
		public string Text { get; set; }
		public List<BoticoImage> Images { get; set; }
		public List<BoticoFile> Files { get; set; }

		public static implicit operator BoticoResponse(string s)
		{
			return new BoticoResponse { Text = s };
		}
	}
}
