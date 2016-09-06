namespace Botico
{
	public static class EmbeddedLangs
	{
		public static string ru_RU =
@"command.help.names=помощь,команды,help,commands
command.help.desc=Список команд.
command.help=Доступны следующие команды: 

command.botico.names=botico,info
command.botico.desc=Информация обо мне.
command.botico.runningOn=Запущен на: 
command.botico.client=Клиент: 
command.botico.ramUsed=Использовано памяти: 

command.random.names=рандом,random
command.random.desc=Получить случайное число в указанном диапазоне. Использование команды: %cmd <макс значение> или %cmd <мин значение> <макс значение>
command.random.nan.max=Максимальное значение должно являться числом и быть меньше %longMax.
command.random.nan.min=Минимальное значение должно являться числом и быть больше %longMax.
command.random.minBiggerMax=Минимальное значение больше максимального.

command.things.names=вещи,stuff,things
command.things.desc=Список моих вещей.
command.things=У меня есть следующие вещи: 

command.addThing.names=вещь,thing,addthing,add_thing,добавитьвещь,добавить_вещь
command.addThing.desc=Добавляет мне указанную вещь. Использование команды: %cmd <вещь>
command.addThing=Вещь '%thing' успешно добавлена.
command.addThing.tooLong=Попробуй добавить что-нибудь поменьше.
command.addThing.exists=У меня уже есть это.

command.wiki.names=вики,википедия,wiki,wikipedia
command.wiki.desc=Плюшки, связанные с вики. %cmd рандом - ссылка на случайную статью, %cmd список - список подключенных вики, %cmd инфо <о чем> - получить информацию о чем-то.
command.wiki.random=рандом
command.wiki.list=список
command.wiki.reference=справка
command.wiki.wikis=Подключены следующие вики: 
command.wiki.reference.error=Введите то, о чем найти справку.
command.wiki.reference.notFound=Того, чего вы ищете нет ни в одной вики.

command.roulette.names=русскаярулетка,рулетка,russianroulette,roulette,русская рулетка,russian roulette
command.roulette.desc=Русская рулетка.
command.roulette.fail=Выстрел! Вы падаете замертво.
command.roulette.win=Выстрел! Вы живы...

command.question.names=вопрос,question
command.question.desc=Задать мне вопрос.

command.answer.names=ответ,нбо,answer
command.answer.desc=Указать ответ на прошлый вопрос.
command.answer=Буду знать.
command.answer.notAsked=Ты не задал мне вопрос.
command.answer.incorrectUsage=Ты не написал ответ.
command.answer.notPermitted=Ты не Мой Господин.

command.image.names=картинку,картинка,картинки,image,images,picture,pictures
command.image.desc=Поиск указанной картинки. Использование команды: %cmd <название картинки>

command.dictionary.names=словарь,словари,dict,dictionary
command.dictionary.desc=Плюшки, связанные со словарями. %cmd рандом - получить случайное слово, %cmd список - получить список подключенных словарей.
command.dictionary.list=список
command.dictionary.random=рандом
command.dictionary.dicts=Доступны следующие словари: 

command.about.names=окоманде,о_команде,о команде,aboutcommand,aboutcmd,about_command,about_cmd
command.about.desc=Получить инфу о команде. Использование: %cmd <команда>.
command.about.notFound=Команда не найдена!

command.turn.names=повернуть,вертеть,будемвертеть,будем вертеть,turn
command.turn.desc=Повернуть слово задом наперед. Использование %cmd <слово>

# ( ͡° ͜ʖ ͡°)
command.boobs.names=сиськи,boobs,сисечки

answers=Да;Нет;Наверное;Возможно;Скорее да, чем нет;Скорее нет, чем да;А ты как думаешь?;Ну я даже не знаю...";
		public static string ru_RU_info = "Russian (Русский)";
	}
}
