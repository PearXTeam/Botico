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
command.addThing.tooLong=Попробуй добавить что-нибудь поменьше.
command.addThing.exists=У меня уже есть это.

command.wiki.names=вики,википедия,wiki,wikipedia
command.wiki.usage=Использование команды: %cmd <рандом|список>
command.wiki.random=рандом
command.wiki.list=список
command.wiki.wikis=Подключены следующие вики: 

command.roulette.names=русскаярулетка,рулетка,russianroulette,roulette,русская рулетка,russian roulette
command.roulette.fail=Выстрел! Вы падаете замертво.
command.roulette.win=Выстрел! Вы живы...

command.question.names=вопрос,question

command.answer.names=ответ,нбо,answer
command.answer=Буду знать.
command.answer.notAsked=Ты не задал мне вопрос.
command.answer.incorrectUsage=Ты не написал ответ.
command.answer.notPermitted=Ты не Мой Господин.

answers=Да;Нет;Наверное;Возможно;Скорее да, чем нет;Скорее нет, чем да;А ты как думаешь?;Ну я даже не знаю...";
		public static string ru_RU_info = "Russian (Русский)";
	}
}
