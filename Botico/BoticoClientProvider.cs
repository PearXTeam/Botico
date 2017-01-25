using System;
using Botico.Model;
namespace Botico
{
	public class BoticoClientProvider
	{
		/// <summary>
		/// Client's name.
		/// </summary>
		public virtual string Name { get; }

		/// <summary>
		/// Creates the mention by CommandSender object.
		/// </summary>
		/// <returns>The mention.</returns>
		/// <param name="sndr">Command sender.</param>
		public virtual string CreateMention(CommandSender sndr)
		{
			throw new NotImplementedException();
		}
	}
}
