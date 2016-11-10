using System;
using Botico.Model;

namespace Botico
{
	public class BCommand
	{
		public virtual bool Hidden { get; set; } = false;
		public virtual bool DescInHelp { get; set; } = false;

		public virtual string[] Names(BoticoClient b)
		{
			throw new NotImplementedException();
		}
		public virtual BoticoResponse OnUse(CommandArgs args)
		{
			throw new NotImplementedException();
		}
		public virtual string Description(BoticoClient b)
		{
			throw new NotImplementedException();
		}
	}
}
