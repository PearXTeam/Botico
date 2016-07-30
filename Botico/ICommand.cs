using System;
using Botico.Model;

namespace Botico
{
	public interface ICommand
	{
		string[] Names(BoticoClient b);
		string OnUse(CommandArgs args);
	}
}
