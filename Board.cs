namespace Polyexcellent
{
    class Board
    {
        public static Board Instance;
        public readonly Square[] Squares = new Square[40];

        public Board()
        {
            CreateBoard();  
        }

        private void CreateBoard()
        {
            var cardFactoryOne = new CardFactory(CardType.CommunityChest);
            var cardFactoryTwo = new CardFactory(CardType.Chance);

            Squares[0] = new Square();
            Squares[1] = new Property("ул. Центральная", PropertyType.Street, 60, 0, PropertyStatus.Free, null, 1);
            Squares[2] = cardFactoryOne.GetSquare(2);
            Squares[3] = new Property("ул. Молодёжная", PropertyType.Street, 60, 0, PropertyStatus.Free, null, 3);
            Squares[4] = new Square(); // Плати налог $200
            Squares[5] = new Property("ул. Школьная", PropertyType.TrainStation, 200, 0, PropertyStatus.Free, null, 5);
            Squares[6] = new Property("ул. Лесная", PropertyType.Street, 100, 0, PropertyStatus.Free, null, 6);
            Squares[7] = cardFactoryTwo.GetSquare(7);
            Squares[8] = new Property("ул. Советская", PropertyType.Street, 100, 0, PropertyStatus.Free, null, 8);
            Squares[9] = new Property("ул. Новая", PropertyType.Street, 120, 0, PropertyStatus.Free, null, 9);
            Squares[10] = new Square(); // Тюрьма
            Squares[11] = new Property("ул. Садовая", PropertyType.Street, 140, 0, PropertyStatus.Free, null, 11);
            Squares[12] = new Property("ул. Набережная", PropertyType.Service, 150, 0, PropertyStatus.Free, null, 12);
            Squares[13] = new Property("ул. Заречная", PropertyType.Street, 140, 0, PropertyStatus.Free, null, 13);
            Squares[14] = new Property("ул. Зелёная", PropertyType.Street, 160, 0, PropertyStatus.Free, null, 14);
            Squares[15] = new Property("ул. Мира", PropertyType.TrainStation, 200, 0, PropertyStatus.Free, null, 15);
            Squares[16] = new Property("ул. Ленина", PropertyType.Street, 180, 0, PropertyStatus.Free, null, 16);
            Squares[17] = cardFactoryTwo.GetSquare(17);
            Squares[18] = new Property("ул. Полевая", PropertyType.Street, 180, 0, PropertyStatus.Free, null, 18);
            Squares[19] = new Property("ул. Луговая", PropertyType.Street, 200, 0, PropertyStatus.Free, null, 19);
            Squares[20] = new Square(); // Ничего
            Squares[21] = new Property("ул. Октябрьская", PropertyType.Street, 220, 0, PropertyStatus.Free, null, 21);
            Squares[22] = cardFactoryOne.GetSquare(22);
            Squares[23] = new Property("ул. Комсомольская", PropertyType.Street, 220, 0, PropertyStatus.Free, null, 23);
            Squares[24] = new Property("ул. Гагарина", PropertyType.Street, 240, 0, PropertyStatus.Free, null, 24);
            Squares[25] = new Property("ул. Первомайская", PropertyType.TrainStation, 200, 0, PropertyStatus.Free, null, 25);
            Squares[26] = new Property("ул. Северная", PropertyType.Street, 260, 0, PropertyStatus.Free, null, 26);
            Squares[27] = new Property("ул. Солнечная", PropertyType.Street, 260, 0, PropertyStatus.Free, null, 27);
            Squares[28] = new Property("ул. Степная", PropertyType.Service, 150, 0, PropertyStatus.Free, null, 28);
            Squares[29] = new Property("ул. Южная", PropertyType.Street, 280, 0, PropertyStatus.Free, null, 29);
            Squares[30] = new Square(); // Оказался здесь - идёшь в тюрьму
            Squares[31] = new Property("ул. Береговая", PropertyType.Street, 300, 0, PropertyStatus.Free, null, 31);
            Squares[32] = new Property("ул. Кирова", PropertyType.Street, 300, 0, PropertyStatus.Free, null, 32);
            Squares[33] = cardFactoryOne.GetSquare(33);
            Squares[34] = new Property("ул. Пионерская", PropertyType.Street, 320, 0, PropertyStatus.Free, null, 34);
            Squares[35] = new Property("ул. Юбилейная", PropertyType.TrainStation, 200, 0, PropertyStatus.Free, null, 35);
            Squares[36] = cardFactoryTwo.GetSquare(36);
            Squares[37] = new Property("ул. Речная", PropertyType.Street, 350, 0, PropertyStatus.Free, null, 37);
            Squares[38] = new Square(); // Наградные $100
            Squares[39] = new Property("ул. Нагорная", PropertyType.Street, 400, 0, PropertyStatus.Free, null, 38);
        }

    }
}
