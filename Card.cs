using System;

namespace Polyexcellent
{
    public enum CardType { Chance, CommunityChest }

    class Card: Square
    {
        public readonly CardType CardType;
        public readonly int What;

        public Card(CardType cardType, int position) : base(position)
        {
            CardType = cardType;
            What = RandomInt();
            Position = position;
        }

        public static int RandomInt()
        {
            var rnd = new Random();
            var result = rnd.Next(1, 8);
            return result;
        }

        public static int RandomCash()
        {
            var rnd = new Random();
            var result = rnd.Next(1, 1000);
            return result;
        }

        public static string CardInstruction(int what, int rand_cash, int rand_int)
        {
            if (what == 1) { return "Выбраться из тюрьмы."; }
            else if (what == 2) { return "Заплатите $" + rand_cash + " игроку, которые ходил до вас"; }
            else if (what == 3) { return "Заплатите $" + rand_cash + " налога"; }
            else if (what == 4) { return "Получите $" + rand_cash + " налогового вычета из банка"; }
            else if (what == 5) { return "Отправьтесь на " + rand_int + " клеток вперёд"; }
            else if (what == 6) { return "Отправьтесь на " + rand_int + " клеток назад"; }
            else if (what == 7) { return "Идите в тюрьму"; }
            else { return "Неверная карточка"; }
        }
    }
}
