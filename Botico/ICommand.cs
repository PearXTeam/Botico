using System;
namespace Botico
{
	public interface ICommand
	{
		string[] Names(BoticoClient b);
		string OnUse(CommandArgs args);
	}
}
