using System.Drawing;

namespace Botico.Model
{
	public class BoticoResponse
	{
		public string Text { get; set; }
		public Image Image { get; set; }

		public static implicit operator BoticoResponse(string s)
		{
			return new BoticoResponse { Text = s };
		}
	}
}
