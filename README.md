# <p align="center"> Соревнование по программированию ботов "Bot Fight 1.0"</p>
## Вступление
Всех рад приветствовать на открытом соревновании по программированию ботов.</br>
<b>Целью</b> соревнования является продвижении программирования среди студентов ВУЗов и развитие навыков программирования поисковых алгоритмов. Надеюсь, что участники найдут здесь друзей, единомышленников, обменяются между собой опытом, получат новые знания в программировании и просто хорошо проведут время.
## О месте проведения
Проведение соревнования планируется в рамках практики по предмету "Информационные технологии и программирование", проводимого на кафедре "Прикладная математика и информационные технологии" (г. Ижевск, ИжГТУ им. М.Т. Калашникова), также к участию приглашаются все желающие студенты других кафедр. Соревнование планируется проводить по мере готовности студентов, примерная дата первого проведения: <b>январь-февраль 2024 года</b>, кафедра "ПМиИТ", 6 корпус, кабинеты 309 и 310.
## О соревновании (Об игре)
Соревнование представляет собой мини-хакатон. Примерно 10-20 команд из 1-2 человек должны разработать алгоритм, который управляет ботом (говорит куда двигаться и что делать), чтобы победить необходимо набрать больше очков, чем другие игроки. Боты передвигаются по общему игровому полю из 40x40 клеток (могут быть и другие поля).
## Правила игры
За основу взята игра "Бомбермен" с некоторыми изменениями.</br>
Далее представлено изображение игрового поля.
![image](https://github.com/KaBaN4iK357/BotFight/assets/32903150/3983c038-85f1-4d98-b45c-da4f340b33f9) </br>
Поле имеет прямоугольный вид, на поле размещены игровые объекты (стены, бонусы, ловушки) и игроки, имена ботов противников подписаны красным шрифтом, имя Вашего бота отмечается зеленым шрифтом, справа отображается таблица игроков.
### Игровые объекты
#### Крепкая стена
![HeavyWall](https://github.com/KaBaN4iK357/BotFight/assets/32903150/bcdef53b-eb23-4dbb-9925-12f4b7c58a4b)  </br>
<b>Свойства объекта:</b></br>
* Препятствует передвижению </br>
* Нельзя уничтожить </br>
* Защищает от взрывов (см. подробности в п. "Мина", "Бомба" и "Суперсила"). </br>
![image](https://github.com/KaBaN4iK357/BotFight/assets/32903150/24a8d926-8912-4c72-8941-d51c96277850) </br>

#### Обычная стена
![Wall](https://github.com/KaBaN4iK357/BotFight/assets/32903150/8ffa6779-cb72-41d3-b4f7-3f3cb29dcedf) </br>
<b>Свойства объекта:</b></br>
* Препятствует передвижению </br>
* Можно уничтожить </br>
* Защищает от взрыва, после чего разрушается (см. подробности в п. "Мина", "Бомба" и "Суперсила"). </br>
![image](https://github.com/KaBaN4iK357/BotFight/assets/32903150/cb543bec-81e9-4004-a3cd-960eba0ce876) </br>

#### Порох
![Powder](https://github.com/KaBaN4iK357/BotFight/assets/32903150/46363242-0258-4abe-88dd-f1899e653af1) </br>
<b>Свойства объекта:</b></br>
* Может быть подобран игроком </br>
* При подборе дает 1 очко и увеличивает дальность взрыва бомбы на 1 клетку
* Можно уничтожить взрывом. </br>

#### Мина
![mine](https://github.com/KaBaN4iK357/BotFight/assets/32903150/8390e934-c646-49c9-82fb-2d9514def9ab) </br>
![image](https://github.com/KaBaN4iK357/BotFight/assets/32903150/40287c6a-cfc6-42b8-9fba-00d9c8d3dc4c) </br>
<b>Свойства объекта:</b></br>
* Взрывается если игрок наступил </br>
* Радиус взрыва равен 2 клетки
* Можно подорвать взрывом </br>
* Возможны цепные реакции взрывов </br>
* Игрок, который инициировал взрыв мины (наступив или взорвав) считается инициатором взрыва </br>
* Если мина подорвала игрока, то очки начисляются инициатору взрыва </br>
* Нельзя получить очки за подрыв самого себя. </br>
![image](https://github.com/KaBaN4iK357/BotFight/assets/32903150/06e4eeb3-11c1-4b80-ac1d-7579b56280cb)
![image](https://github.com/KaBaN4iK357/BotFight/assets/32903150/04be7905-8823-4826-a436-794367e22225)

#### Алмаз
![Diamond](https://github.com/KaBaN4iK357/BotFight/assets/32903150/006d05da-f673-4560-82fd-64c95b7d7410) </br>
<b>Свойства объекта:</b></br>
* Может быть подобран игроком (дает 10 очков) </br>
* Нельзя уничтожить взрывом </br>
* Не защищает от взрыва. </br>

#### Бонус "Постройка"
![build](https://github.com/KaBaN4iK357/BotFight/assets/32903150/8bf2df35-2a86-44f6-a4d4-1732edb0d196) </br>
<b>Свойства объекта:</b></br>
* Может быть подобран игроком </br>
* При подборе возводит стены вокруг игрока </br>
* Стены возводятся только в пустых ячейках. </br>
![image](https://github.com/KaBaN4iK357/BotFight/assets/32903150/5912db2d-df94-4914-a10f-21527f519743)
![image](https://github.com/KaBaN4iK357/BotFight/assets/32903150/74063a42-946a-44f3-9408-ab090bcdf13b)

#### Суперсила
![SuperPower](https://github.com/KaBaN4iK357/BotFight/assets/32903150/013bbf55-dddb-4263-be72-9135ef3c86f1) </br>
<b>Свойства объекта:</b></br>
* Может быть подобран игроком </br>
* При подборе у игрока меняется модель </br>
![image](https://github.com/KaBaN4iK357/BotFight/assets/32903150/271f6beb-54c9-4299-b8ee-35365497c851) </br>
* При подборе дает игроку суперсилы на следующие 10 ходов </br>
* Во время действия суперсилы игрок неуязвим </br>
![image](https://github.com/KaBaN4iK357/BotFight/assets/32903150/e52ca05a-15be-49e8-a90d-97b8405642bd) </br>
* Установка бомбы заменяется на выстрел в заданном направлении </br>
* Выстрел пробивает насквозь 1 простую стену </br>
![image](https://github.com/KaBaN4iK357/BotFight/assets/32903150/e45e750d-1f62-48aa-9a40-c74b87416340)
![image](https://github.com/KaBaN4iK357/BotFight/assets/32903150/9db88da7-71ce-4629-b935-48d6b7c19f2e) </br>
* Дальность выстрела равна удвоенной дальности взрыва бомбы. </br>

#### Бомба
![bomb](https://github.com/KaBaN4iK357/BotFight/assets/32903150/952e6218-2d99-4820-b00c-c70161a7b844) </br>
<b>Свойства объекта:</b></br>
* Устанавливается игроком </br>
* Взрывается на 5-й ход после размещения </br>
* Радиус взрыва по умолчанию равен 2 клетки </br>
![image](https://github.com/KaBaN4iK357/BotFight/assets/32903150/448b7211-7c61-429a-8bd9-d07a510e3533)

#### Игрок
![player](https://github.com/KaBaN4iK357/BotFight/assets/32903150/7d5a8e9b-bf36-4b01-be9d-2fbd723a4183) </br>
<b>Свойства объекта:</b></br>
* Воскрешается на 3-й ход после смерти </br>

### Возможные действия
Возможные действия представлены в программе в виде enum [PlayerAction](https://github.com/KaBaN4iK357/BotFight/blob/main/BomberMans/BomberMansTCPFormsLibrary/PlayerAction.cs). За 1 ход игрок может сделать 1 из действией:
* Ничего (None)
* Шаг влево (Left)
* Шаг вправо (Right)
* Шаг вверх (Top)
* Шаг вниз (Bottom)
* Установить бомбу слева от игрока (BombLeft)
* Установить бомбу справа от игрока (BombRight)
* Установить бомбу сверху от игрока (BombTop)
* Установить бомбу снизу от игрока (BombBottom)

```
public enum PlayerAction
{
  None, Left, Right, Top, Bottom, BombLeft, BombRight, BombTop, BombBottom
}
```

### Как устроена игра
Проект представляет собой клиент-серверное приложение. Все операции обрабатываются на сервере, доступ к серверу обеспечивается по локальной сети. В начале каждого хода на компьютеры игроков по TCP в виде строки формата json отправляется текущее игровое поле, далее через 2 секунды происходит обработка ответов игроков и обновление поля. <br>

В репозитории расположена серверная часть проекта в папке BomberMans и пример клиентской части BomberManClient, которую должны запускать игроки для общения с сервером и отправки действий ботов. Серверная часть дана для ознакомления с программой и её логикой. Проект целиком написан на языке C#, однако участники при желании могут написть свою клиентскую часть на любом языке программирования. <br>
#### Последовательность обработки ответов игроков:
Запросы обрабатываются в порядке их получения сервером. Это значит, что в случае спорной ситуации, например, когда 2 игрока хотят занять одну и ту же клетку, предпочтение будет отдаваться тому, кто быстрее отправил ответ на сервер.
#### Последовательность операций при обновлении поля:
1. Удаление взрывов
2. Уменьшение таймера действия суперсилы и её отключение в случае, если истек таймер
3. Уменьшение таймера бомб
4. Подрыв бомб, у которых истек таймер
5. Выполнение действий игроков
6. Уменьшение таймера воскрешения
7. Воскрешение игроков, у которых таймер воскрешения истек

[comment]: <> (TODO: Игровые ситуации, взаимодействие объектов, цепочка действий, цепной взрыв, за что начисляются очки, картинки с ситуациями)

## Как начать программировать своего бота

## Как участвовать
Сообщить о намерении участвовать организатору (см. "Контактная информация"), если набирается минимальное количество команд, то Вам будет предоставлена информация о месте и времени проведения соревнования.
## Контактная информация
Разработчиком соревнования является инженер-программист 1-й кат., преподаватель курса "ИТиП" Клюкин Даниил Анатольевич (https://vk.com/id194921992).
