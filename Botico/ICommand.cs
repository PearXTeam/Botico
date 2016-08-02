using Botico.Model;

namespace Botico
{
	public interface ICommand
	{
		string[] Names(BoticoClient b);
		BoticoResponse OnUse(CommandArgs args);
		string Description(BoticoClient b);
	}
}
