namespace Polyexcellent
{
    public enum PropertyType { TrainStation, Service, Street }
    public enum PropertyStatus { Free, Bought, House, Hotel }

    class Property: Square
    {
        public string Name;
        public PropertyType PropertyType;
        public long BuyingCost;
        public long Taxes;
        public PropertyStatus Status; 
        public Player Owner;

        public Property(string name, PropertyType propertyType, long buyingCost, long taxes, PropertyStatus status, Player owner, int position) : base(position)
        {
            Name = name;
            PropertyType = propertyType;
            BuyingCost = buyingCost;
            Taxes = taxes;
            Status = status;
            Owner = owner;
            Position = position;
        }

        protected Property() { }

        public override string ToString()
        {
            return "\tНазвание: " + Name + "\n\tТип: " + PropertyType + "\n\tЦена: $" + BuyingCost + "\n\tНалог: $" + Taxes +
                   "\n\tСтатус: " + Status;
        }
    }
}