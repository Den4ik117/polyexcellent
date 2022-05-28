namespace Polyexcellent
{
    class CardFactory : SquareFactory
    {
        public CardType CardType;

        public CardFactory(CardType cardType) 
        {
            this.CardType = cardType;
        }
        public override Square GetSquare(int position)
        {
            return new Card(CardType, position);
        }
    }
}
