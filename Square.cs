namespace Polyexcellent
{
    class Square
    {
        public int Position { get; protected init; }

        protected Square(int position)
        {
            Position = position;
        }

        public Square() {}
    }
}
