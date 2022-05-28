using System;
using System.Collections.Generic;
using System.Linq;

namespace Polyexcellent
{
    class Player
    {
        public string Name { get; init; }
        public int Position;
        public long Money = 1500;
        public bool Jail = false;
        public readonly List<Property> Properties = new List<Property>();
        public bool GetOutOfJailCard = false;
        public bool Loser = false;

        public override string ToString()
        {
            Console.WriteLine("\nСтатистика игрока " + Name);
            foreach (var property in Properties)
                Console.WriteLine(property.ToString());
            return "Игрок: " + Name + "\nПозиция: " + Position + "\nКоличество денег: $" + Money + "\nНедвижимость: " +
                   (Properties?.Count ?? 0) ;
        }

        /// <summary>
        /// Метод RollDices() позволяет
        /// "кинуть" игровые кости
        /// </summary>
        /// <returns>Возвращает массив чисел, состоящий из двух значений ― первого и второго броска</returns>
        public static int[] RollDices()
        {
            var random = new Random();
            var diceOne = random.Next(1, 7);
            var diceTwo = random.Next(1, 7);
            var total = diceOne + diceTwo;
            Console.WriteLine("На первой игровой кости ― " + diceOne);
            Console.WriteLine("На второй игровой кости ― " + diceTwo);
            Console.WriteLine("Всего: " + total);
            return new []{ diceOne, diceTwo };
        }

        /// <summary>
        /// Метод DoubleBool() проверяет,
        /// совпали ли значения первого и второго бросков
        /// </summary>
        /// <param name="tab">Массив значений бросков</param>
        /// <returns>Возвращает true, если значения равны, иначе ― false</returns>
        public static bool DoubleBool(int[] tab)
        {
            return tab[0] == tab[1];
        }

        /// <summary>
        /// Метод Move() перемещает игрока
        /// на определённое число едениц
        /// </summary>
        /// <param name="number">То, на сколько переместить игрока</param>
        public void Move(int number)
        {
            Position += number;
            
            if (Position >= 40)
            {
                Position -= 40;
                Money += 200;
            }
        }
        
        public void MoveBackward(int number)
        {
            Position -= number;

            if (Position < 0)
                Position += 40;
        }
    }
}