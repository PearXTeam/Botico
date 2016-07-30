using System;
namespace Botico
{
	public static class EmbeddedLangs
	{
		public static string ru_RU =
@"command.help.names=помощь,команды,help,commands
command.help=Доступны следующие команды: 

command.botico.names=botico,info
command.botico.runningOn=Запущен на: 
command.botico.client=Клиент: 
command.botico.ramUsed=Использовано памяти: 

command.random.names=рандом,random
command.random.usage=Использование команды: %cmd <макс. значение> или %cmd <мин. значение> <макс. значение>
command.random.nan.max=Максимальное значение не является числом.
command.random.nan.min=Минимальное значение не является числом.
command.random.minBiggerMax=Минимальное значение больше максимального.

command.things.names=вещи,stuff,things
command.things=У меня есть следующие вещи: 

command.addThing.names=вещь,thing,addthing,add_thing,добавитьвещь,добавить_вещь
command.addThing=Вещь '%thing' успешно добавлена.
command.addThing.usage=Использование команды: %cmd <вещь>.
command.addThing.tooLong=Попробуй добавить что-нибудь поменьше.";
		public static string ru_RU_info = "Russian (Русский)";
	}
}
