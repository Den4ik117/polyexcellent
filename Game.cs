using System;
using System.Collections.Generic;
using System.Linq;
using static System.Int32;

namespace Polyexcellent
{
    /// <summary>
    /// Класс Game
    /// Основной класс программы
    /// Большая часть логики игры Polyexcellent заключена здесь
    /// </summary>
    class Game
    {
        private readonly List<Player> _players;
        private readonly Board _boardGame;
        private int _rounds;
        private Player _winner;

        public Game()
        {
            _players = new List<Player>();
            _boardGame = new Board();
        }

        /// <summary>
        /// Метод Create() является
        /// стартовым для инициализации
        /// количества игроков и их имён
        /// </summary>
        public void Create()
        {
            Console.WriteLine("Добро пожаловать в игру «Polyexcellent»!");
            
            int numberOfPlayers;
            do
            {
                Console.WriteLine("Введите количество игроков (от 2-х до 6-ти):");
                TryParse(Console.ReadLine(), out numberOfPlayers);
            } while (numberOfPlayers is < 2 or > 6);
            
            for (var i = 0; i < numberOfPlayers; i++)
            {
                Console.WriteLine("Игрок " + (i + 1) + ":");
                Console.Write("Введите никнейм игрока: ");
                var name = Console.ReadLine() ?? "Игрок #" + (i + 1);
                _players.Add(new Player { Name = name });
                Console.WriteLine("\nИгрок был успешно добавлен!\n");
            }
            
            Console.WriteLine("\nСписок игроков:");
            foreach (var player in _players)
                Console.WriteLine("\n" + player);
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nЧтобы начать игру, нажмите любую клавишу!\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey(true);
        }

        /// <summary>
        /// Метод Start() стоит на втором месте
        /// после метода Create(),
        /// вызывается после того, как игроки были проинициализированы
        /// </summary>
        public void Start()
        {
            var currentPlayerIndex = 0;
            Console.Clear();
            Console.WriteLine("Игра началась!");
            while (!IsWinner())
            {
                Console.Clear();
                _rounds++;
                while (_players[currentPlayerIndex].Loser)
                {
                    if (currentPlayerIndex == _players.Count - 1)
                        currentPlayerIndex = 0;
                    else
                        currentPlayerIndex++;
                }
                var currentPlayer = _players[currentPlayerIndex];
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nИгрок " + currentPlayer.Name + ":");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nНажмите любую клавишу, чтобы бросить кости!\n");
                Console.ReadKey(true);
                var dices= Player.RollDices();
                Console.ForegroundColor = ConsoleColor.Green;
                currentPlayer.Move(dices[0] + dices[1]);
                Console.WriteLine("\nТекущая позиция: " + currentPlayer.Position + "\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey(true);
                DisplayMenu(currentPlayer, currentPlayerIndex, true);
                var turns = 1;
                while (Player.DoubleBool(dices))
                {
                    turns++;
                    if (turns == 4)
                    {
                        Console.WriteLine("Вы выбросили дубль третий раз подряд. Вы должны отправиться в тюрьму.");
                        currentPlayer.Jail = true;
                        currentPlayer.Position = 10;
                        Console.WriteLine("Сейчас вы в тюрьме. Нажмите любую клавишу, чтобы продолжить.\n");
                        Console.ReadKey(true);
                        break;
                    }                    
                    Console.WriteLine("\nОго, вы выбросили кости одинаковых значений, поэтому вы получаете доп. ход");
                    Console.WriteLine("\nНажмите любую клавишу, чтобы бросить кости!\n");
                    Console.ReadKey(true);
                    dices = Player.RollDices();
                    currentPlayer.Move(dices[0] + dices[1]);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nТекущая позиция: " + currentPlayer.Position + "\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey(true);
                    DisplayMenu(currentPlayer, currentPlayerIndex, true);                    
                }
                if (currentPlayerIndex == _players.Count - 1)
                    currentPlayerIndex = 0;
                else
                    currentPlayerIndex++;
            }
            Console.WriteLine("Победитель: " + _winner.Name);
            Console.ReadKey(true);
        }
        
        /// <summary>
        /// Метод IsWinner() проверяет,
        /// есть ли победитель в игре
        /// </summary>
        /// <returns>Возвращает true, если победитель вычислен, иначе возвращает false</returns>
        private bool IsWinner()
        {
            var lostPlayers = _players
                .Where(player => player.Money != 0)
                .ToList();

            if (lostPlayers.Count == 1)
            {
                _winner = lostPlayers.First();
                return true;
            }

            lostPlayers = _players
                .Where(player => player.Loser == false)
                .ToList();

            if (lostPlayers.Count == 1)
            {
                _winner = lostPlayers.First();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Метод DisplayMenu()
        /// отображает игровое меню
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <param name="playerIndex">Индекс игрока</param>
        /// <param name="pos">Позиция игрока</param>
        private void DisplayMenu(Player player, int playerIndex, bool pos)
        {
            Console.Clear();
            if (pos)
                DisplayPosition(player, playerIndex);
            
            int number;
            do
            {
                Console.WriteLine("\nДействия:\n");
                Console.WriteLine("0: Статус игры");
                Console.WriteLine("1: Завершить свой ход");
                Console.WriteLine("2: Ваша статистика");
                Console.WriteLine("3: Купить недвижимость");
                Console.WriteLine("4: Купить дом");
                Console.WriteLine("5: Купить отель");
                Console.WriteLine("6: Сделаться банкротом");
                Console.WriteLine("7: Выйти из игры");
                Console.Write("Пожалуйста, введите число от 0 до 7:");
                TryParse(Console.ReadLine(), out number);
            } while (number is < 0 or > 7);

            switch(number)
            {
                case 0:
                    Console.WriteLine("Статус игры:");
                    foreach (var currentPlayer in _players)
                        Console.WriteLine("\n" + currentPlayer);
                    Console.ReadKey();
                    Console.Clear();
                    DisplayMenu(player, playerIndex, pos);
                    break;
                case 1:
                    break;
                case 2:
                    Dashboard(player, playerIndex);
                    break;
                case 3:
                    PurchaseProperty(player, playerIndex);
                    break;
                case 4:
                    BuyHouseProperty(player, playerIndex);
                    break;
                case 5:
                    BuyHotelProperty(player, playerIndex);
                    break;
                case 6:
                    player.Loser = true;
                    break;
                case 7:
                    player.Money = 0;
                    player.Loser = true;
                    break;
            }
        }

        /// <summary>
        /// Метод DisplayPosition()
        /// отображает текущую позицию игрока
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <param name="playerIndex">Индекс игрока</param>
        private void DisplayPosition(Player player, int playerIndex)
        {
            var property = new Property("", PropertyType.Street, 0, 0, PropertyStatus.Free, null, 0);
            var bp = new BoughtProperty(property, null);
            var hsp = new HouseProperty(bp, null);
            var htp = new HotelProperty(hsp, null);
            var c = new Card(CardType.Chance, 0);
            var s = new Square();
            Console.WriteLine("Точка, в которой вы сейчас находитесь:");
            if (_boardGame.Squares[player.Position].GetType() == property.GetType())
            {
                property = (Property) _boardGame.Squares[player.Position];
                Console.WriteLine(property.ToString());
            }
            else if(_boardGame.Squares[player.Position].GetType() == bp.GetType())
            {
                bp = (BoughtProperty) _boardGame.Squares[player.Position];
                Console.WriteLine(bp.ToString());
                if (bp.Owner != player)
                {
                    Console.WriteLine("\nВы должны заплатить $" + bp.Taxes + " владельцу этого имущества (" + bp.Owner.Name + ")");
                    if (player.Money < bp.Taxes)
                    {
                        Console.WriteLine("У Вас не достаточно денег. Вы проиграли.");
                        player.Loser = true;
                        player.Money = 0;
                        Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                        Console.ReadKey(true);
                        DisplayMenu(player, playerIndex, false);
                    }
                    else
                    {
                        player.Money -= bp.Taxes;
                        bp.Owner.Money += bp.Taxes;
                        Console.WriteLine("Теперь у вас $" + player.Money);
                        Console.WriteLine("У владельца (" + bp.Owner.Name + ") имущества теперь $" + bp.Owner.Money);
                        Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                        Console.ReadKey(true);
                        DisplayMenu(player, playerIndex, false);
                    }
                }
            }
            else if (_boardGame.Squares[player.Position].GetType() == hsp.GetType())
            {
                hsp = (HouseProperty) _boardGame.Squares[player.Position];
                Console.WriteLine(hsp.ToString());
                if (hsp.Owner != player)
                {
                    Console.WriteLine("\nВы должны заплатить $" + hsp.Taxes + " владельцу этого имущества (" + hsp.Owner.Name + ")");
                    if (player.Money < hsp.Taxes)
                    {
                        Console.WriteLine("У Вас не достаточно денег. Вы проиграли.");
                        player.Loser = true;
                        player.Money = 0;
                        Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                        Console.ReadKey(true);
                        DisplayMenu(player, playerIndex, false);
                    }
                    else
                    {
                        player.Money -= hsp.Taxes;
                        hsp.Owner.Money += hsp.Taxes;
                        Console.WriteLine("Теперь у вас $" + player.Money);
                        Console.WriteLine("У владельца (" + hsp.Owner.Name + ") имущества теперь $" + hsp.Owner.Money);
                        Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                        Console.ReadKey(true);
                        DisplayMenu(player, playerIndex, false);
                    }
                }
            }
            else if (_boardGame.Squares[player.Position].GetType() == htp.GetType())
            {
                htp = (HotelProperty)_boardGame.Squares[player.Position];
                Console.WriteLine(htp.ToString());
                if (htp.Owner != player)
                {
                    Console.WriteLine("\nВы должны заплатить $" + htp.Taxes + " владельцу этого имущества (" + htp.Owner.Name + ")");
                    if (player.Money < htp.Taxes)
                    {
                        Console.WriteLine("У Вас не достаточно денег. Вы проиграли.");
                        player.Loser = true;
                        player.Money = 0;
                        Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                        Console.ReadKey(true);
                        DisplayMenu(player, playerIndex, false);
                    }
                    else
                    {
                        player.Money -= htp.Taxes;
                        htp.Owner.Money += htp.Taxes;
                        Console.WriteLine("Теперь у вас $" + player.Money);
                        Console.WriteLine("У владельца (" + htp.Owner.Name + ") имущества теперь $" + htp.Owner.Money);
                        Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                        Console.ReadKey(true);
                        DisplayMenu(player, playerIndex, false);
                    }
                }
            }
            else if (_boardGame.Squares[player.Position].GetType() == c.GetType())
            {
                c = (Card) _boardGame.Squares[player.Position];
                Console.WriteLine(c.CardType + " карточка!");
                CardSquare(c, player, playerIndex);
            }
            else if (_boardGame.Squares[player.Position].GetType() == s.GetType())
            {
                EmptySquare(player, playerIndex);
            }
        }

        /// <summary>
        /// Метод CardSquare()
        /// отображает карту, которая может выпасть
        /// на определённом участке игры
        /// </summary>
        /// <param name="c">Карточка</param>
        /// <param name="player">Игрок</param>
        /// <param name="playerIndex">Индекс игрока</param>
        private void CardSquare(Card c, Player player, int playerIndex)
        {
            Console.Write("Карточка шепчет: ");
            var randCash = Card.RandomCash();
            var randInt = Card.RandomInt();
            Console.WriteLine("'" + Card.CardInstruction(c.What, randCash, randInt) + "'");
            switch (c.What)
            {
                case 1 when player.Jail:
                    Console.WriteLine("Эта карта позволяет вам выбраться из тюрьмы. Теперь вы свободны.");
                    player.Jail = false;
                    break;
                case 1:
                    Console.WriteLine("Вы сейчас не в тюрьме. Вы можете оставить эту карту на потом.");
                    player.GetOutOfJailCard = true;
                    break;
                case 2 when _rounds < 2:
                    Console.WriteLine("Это ваш счастливый день: до вас никто не играл! Вам не нужно ничего платить.");
                    break;
                case 2 when player.Money < randCash:
                    Console.WriteLine("У вас недостаточно денег для оплаты. Вы проиграли.");
                    player.Loser = true;
                    break;
                case 2:
                    _players[playerIndex - 1].Money += randCash;
                    player.Money -= randCash;
                    Console.WriteLine("Вы дали $" + randCash + " игроку " + _players[playerIndex].Name);
                    Console.WriteLine("Теперь у вас $" + player.Money);
                    break;
                case 3 when player.Money < randCash:
                    Console.WriteLine("У вас недостаточно денег для оплаты. Вы проиграли.");
                    player.Loser = true;
                    break;
                case 3:
                    player.Money -= randCash;
                    Console.WriteLine("Вы заплатили $" + randCash + " за налог.");
                    Console.WriteLine("Теперь у вас $" + player.Money);
                    break;
                case 4:
                    player.Money += randCash;
                    Console.WriteLine("Вы получили $" + randCash + " из банка как налоговый вычет.");
                    Console.WriteLine("Теперь у вас $" + player.Money);
                    break;
                case 5:
                    player.Move(randInt);
                    Console.WriteLine("Ваша позиция сейчас: " + player.Position);
                    break;
                case 6:
                    player.MoveBackward(randInt);
                    Console.WriteLine("Ваша позиция сейчас: " + player.Position);
                    break;
                case 7:
                    player.Position = 10;
                    player.Jail = true;
                    Console.WriteLine("Вы сейчас в тюрьме.");
                    break;
            }
            Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Метод EmptySquare()
        /// позволяет отработать ячейка,
        /// в которых нельзя купить недвижимость
        /// </summary>
        /// <param name="player"></param>
        /// <param name="playerIndex"></param>
        private void EmptySquare(Player player, int playerIndex)
        {
            switch (player.Position)
            {
                case 0:
                    Console.WriteLine("\nВы в начальной позиции!");
                    break;
                case 4:
                {
                    Console.WriteLine("Время платить налоги!\nВам нужно заплатить $200.");
                    if (player.Money < 200)
                    {
                        Console.WriteLine("\nУ Вас не достаточно денег. Вы проиграли.");
                        player.Loser = true;
                        Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                        Console.ReadKey(true);
                        DisplayMenu(player, playerIndex, false);
                    }
                    else
                    {
                        player.Money -= 200;
                        Console.WriteLine("Теперь у вас $" + player.Money);
                        Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                        Console.ReadKey(true);
                        DisplayMenu(player, playerIndex, false);
                    }

                    break;
                }
                case 10:
                    Console.WriteLine("\nТюремная зона! Но не волнуйтесь, вы только в гостях.");
                    break;
                case 20:
                    Console.WriteLine("\nБесплатная парковка");
                    break;
                case 30:
                    Console.WriteLine("\nИдите в тюрьму!");
                    player.Jail = true;
                    player.Position = 10;
                    Console.WriteLine("Теперь Вы в тюрьме.");
                    Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                    Console.ReadKey(true);
                    DisplayMenu(player, playerIndex, false);
                    break;
                case 38:
                {
                    Console.WriteLine("Новые налоги!\nВам нужно заплатить $100.");
                    if (player.Money < 100)
                    {
                        Console.WriteLine("\nУ Вас не достаточно денег. Вы проиграли.");
                        player.Loser = true;
                        Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                        Console.ReadKey(true);
                        DisplayMenu(player, playerIndex, false);
                    }
                    else
                    {
                        player.Money -= 100;
                        Console.WriteLine("Теперь у вас $" + player.Money);
                        Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                        Console.ReadKey(true);
                        DisplayMenu(player, playerIndex, false);
                    }

                    break;
                }
            }
        }

        /// <summary>
        /// Метод PurchaseProperty()
        /// позволяет проверить валидность той или иной покупки
        /// недвижимости
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <param name="playerIndex">Индекс игрока</param>
        private void PurchaseProperty(Player player, int playerIndex)
        {
            var property = new Property("", PropertyType.Street, 0, 0, PropertyStatus.Free, null, 0);
            var bp = new BoughtProperty(property, null);
            var hsp = new HouseProperty(bp, null);
            var htp = new HotelProperty(hsp, null);
            if(_boardGame.Squares[player.Position].GetType() == bp.GetType() || _boardGame.Squares[player.Position].GetType() == hsp.GetType() || _boardGame.Squares[player.Position].GetType() == htp.GetType())
            {
                Console.WriteLine("Это действие не доступно.");
                Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                Console.ReadKey(true);
                DisplayMenu(player, playerIndex, false);
            }
            else if (_boardGame.Squares[player.Position].GetType() == property.GetType())
            {
                Console.Clear();
                Console.WriteLine("Недвижимость, которую вы хотите купить, является следующей:\n");
                property = (Property) _boardGame.Squares[player.Position];
                Console.WriteLine(property.ToString());
                if (property.BuyingCost > player.Money)
                {
                    Console.WriteLine("\nУ вас недостаточно денег для совершения этой покупки.");
                    Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                    Console.ReadKey(true);
                    DisplayMenu(player, playerIndex, true);
                }
                else
                {
                    Console.WriteLine("\nТеперь у вас: $" + player.Money);
                    int res;
                    do
                    {
                        Console.WriteLine("Вы уверены, что хотите совершить эту покупку?\n1: Да\n2: Нет");
                        TryParse(Console.ReadLine(), out res);
                    } while (res != 1 && res != 2);
                    if (res == 1)
                    {
                        Console.Clear();
                        property = new BoughtProperty(property, player);
                        BoughtProperty b = (BoughtProperty) property;
                        _boardGame.Squares[player.Position] = b;
                        player.Properties.Add(b);
                        player.Money -= property.BuyingCost;
                        Console.WriteLine("Поздравляем с новой покупкой!\n");
                        Console.WriteLine(b.ToString());
                        Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                        Console.ReadKey(true);
                        DisplayMenu(player, playerIndex, true);
                    }
                    else
                    {
                        Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                        Console.ReadKey(true);
                        DisplayMenu(player, playerIndex, true);
                    }
                }
            }
            else
            {
                Console.WriteLine("Эта клетка не является собственностью, вы не можете ее купить.");
                Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.\n");
                Console.ReadKey(true);
                DisplayMenu(player, playerIndex, true);
            }
        }

        /// <summary>
        /// Метод Dashboard()
        /// показывает статистику игрока
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <param name="playerIndex">Индекс игрока</param>
        private void Dashboard(Player player, int playerIndex)
        {
            Console.Clear();
            Console.WriteLine("Позиция: " + player.Position);
            Console.WriteLine("Количество денег: $" + player.Money);
            Console.WriteLine("В ваших владениях " + player.Properties.Count + " недвижимости:\n");
            if (player.Properties.Count != 0)
                foreach(var property in player.Properties)
                    Console.WriteLine(property.ToString());
            Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
            Console.ReadKey(true);
            DisplayMenu(player, playerIndex, false);
        }

        /// <summary>
        /// Метод BuyHouseProperty() позволяет
        /// игроку приобрести дом для недвижимости
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <param name="playerIndex">Индекс игрока</param>
        private void BuyHouseProperty(Player player, int playerIndex)
        {
            if (player.Properties.Count == 0)
            {
                Console.WriteLine("У вас нет никакой недвижимости.");
                Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
            }
            else
            {
                var i = 0;
                Console.WriteLine("Для какой недвижимости вы хотите купить дом?\n");
                foreach(var property in player.Properties)
                {
                    Console.WriteLine(i + 1 + " " + property + "\n");
                    i++;
                }
                TryParse(Console.ReadLine(), out i);
                i--;
                var bp = new BoughtProperty(player.Properties[i], player);
                while (player.Properties[i].GetType() != bp.GetType())
                {
                    Console.WriteLine("Вы не можете купить дом для этой собственности, потому что он уже есть.");
                    Console.WriteLine("1: Выбрать другую собственность\n2: Вернуться в меню");
                    TryParse(Console.ReadLine(), out var r);
                    if (r == 2) { DisplayMenu(player, playerIndex, false); }
                    else if (r == 1)
                    {
                        i = 0;
                        Console.WriteLine("Для какой недвижимости вы хотите купить дом?\n");
                        foreach (var property in player.Properties)
                        {
                            // Console.Write(i + 1);
                            Console.WriteLine(i + 1 + " " + property + "\n");
                            i++;
                        }
                        TryParse(Console.ReadLine(), out i);
                        i--;
                    }
                }
                bp = (BoughtProperty) player.Properties[i];
                var housePrice = bp.BuyingCost * 2;
                Console.WriteLine("Покупка дома для этой недвижимости стоит $" + housePrice);
                if (player.Money < housePrice)
                {
                    Console.WriteLine("\nУ вас недостаточно денег для совершения этой покупки.");
                    Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в меню.");
                    Console.ReadKey(true);
                    DisplayMenu(player, playerIndex, false);
                }
                else
                {
                    Console.WriteLine("\nСейчас у вас: $" + player.Money);
                    int res;
                    do
                    {
                        Console.WriteLine("Вы уверены, что хотите совершить эту покупку?");
                        Console.WriteLine("1: Да");
                        Console.WriteLine("2: Нет");
                        TryParse(Console.ReadLine(), out res);
                    } while (res != 1 && res != 2);
                    if (res == 1)
                    {
                        Console.Clear();
                        bp = new HouseProperty(bp, player);
                        var hsp = (HouseProperty) bp;
                        _boardGame.Squares[player.Position] = hsp;
                        var j = player.Properties.Count(prop => prop.Name != hsp.Name);
                        player.Properties[j] = hsp;
                        Console.WriteLine("Поздравляем Вас с новым домом!\n");
                        Console.WriteLine(hsp.ToString());
                        Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                        Console.ReadKey(true);
                        DisplayMenu(player, playerIndex, false);
                    }
                    else
                    {
                        Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                        Console.ReadKey(true);
                        DisplayMenu(player, playerIndex, false);
                    }
                }
            }
        }

        /// <summary>
        /// Метод BuyHotelProperty() позволяет
        /// игроку приобрести отель для недвижимости
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <param name="playerIndex">Индекс игрока</param>
        private void BuyHotelProperty(Player player, int playerIndex)
        {
            if (player.Properties.Count == 0)
            {
                Console.WriteLine("У вас нет никакой недвижимости.");
                Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
            }
            else
            {
                int i = 0;
                Console.WriteLine("Для какой недвижимости вы хотите купить отель?\n");
                foreach (var property in player.Properties)
                {
                    // Console.Write(i + 1);
                    Console.WriteLine(i + 1 + " " + property + "\n");
                    i++;
                }
                TryParse(Console.ReadLine(), out i);
                i--;
                var bp = new BoughtProperty(player.Properties[i], player);
                var hsp = new HouseProperty(bp, player);
                while (player.Properties[i].GetType() != hsp.GetType())
                {
                    Console.WriteLine("Вы не можете купить отель для этого объекта, потому что он уже есть у вас или у него еще нет дома.");
                    Console.WriteLine("1: Выбрать другую недвижимость\n2: Вернуться в меню");
                    TryParse(Console.ReadLine(), out var r);
                    if (r == 2) DisplayMenu(player, playerIndex, false);
                    else if (r == 1)
                    {
                        Console.WriteLine("Для какой недвижимости вы хотите купить отель?\n");
                        foreach (var property in player.Properties)
                        {
                            // Console.Write(i + 1);
                            Console.WriteLine(i + 1 + " " + property + "\n");
                            i++;
                        }
                        TryParse(Console.ReadLine(), out i);
                        i--;
                    }
                }
                hsp = (HouseProperty) player.Properties[i];
                var hotelPrice = hsp.BuyingCost * 3;
                    Console.WriteLine("Покупка отеля для этой недвижимости стоит $" + hotelPrice);
                    if (player.Money < hotelPrice)
                    {
                        Console.WriteLine("\nУ вас недостаточно денег для совершения этой покупки.");
                        Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в меню.");
                        Console.ReadKey(true);
                        DisplayMenu(player, playerIndex, false);
                    }
                    else
                    {
                        Console.WriteLine("\nСейчас у вас: $" + player.Money);
                        int res;
                        do
                        {
                            Console.WriteLine("Вы уверены, что хотите совершить эту покупку?");
                            Console.WriteLine("1: Да");
                            Console.WriteLine("2: Нет");
                            TryParse(Console.ReadLine(), out res);
                        } while (res != 1 && res != 2);
                        if (res == 1)
                        {
                            Console.Clear();
                            hsp = new HotelProperty(hsp, player);
                            var htp = (HotelProperty) hsp;
                            _boardGame.Squares[player.Position] = htp;
                            var j = player.Properties.Count(property => property.Name != htp.Name);
                            player.Properties[j] = htp;
                            player.Properties[j] = hsp;
                            Console.WriteLine("Поздравляем Вас с новым отелем!\n");
                            Console.WriteLine(htp.ToString());
                            Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                            Console.ReadKey(true);
                            DisplayMenu(player, playerIndex, false);
                        }
                        else
                        {
                            Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню.");
                            Console.ReadKey(true);
                            DisplayMenu(player, playerIndex, false);
                        }
                    }
            }
        }
    }
}
