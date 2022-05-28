namespace Polyexcellent
{
    abstract class PropertyDecorator : Property
    {
        protected PropertyDecorator(Property prop, Player play)
        {
            Name = prop.Name;
            BuyingCost = prop.BuyingCost;
            PropertyType = prop.PropertyType;
            Owner = play;
            Position = prop.Position;
        }

        public override string ToString()
        {
            return "\tНазвание: " + Name + "\n\tТип: " + PropertyType + "\n\tЦена: $" + BuyingCost + "\n\tНалог: $" + Taxes +
                   "\n\tСтатус: " + Status + "\n\tВладелец: " + Owner.Name;
        }
    }

    class BoughtProperty : PropertyDecorator
    {

        public BoughtProperty(Property prop, Player play):base(prop, play)
        {            
            Taxes = prop.BuyingCost / 2;
            Status = PropertyStatus.Bought;
        }
    }

    class HouseProperty : BoughtProperty
    {
        public HouseProperty(BoughtProperty prop, Player play) : base(prop, play)
        {
            Taxes = prop.Taxes * 2;
            Status = PropertyStatus.House;
        }
    }

    class HotelProperty : HouseProperty
    {
        public HotelProperty(HouseProperty prop, Player play) : base(prop, play)
        {
            Taxes = prop.Taxes * 2;
            Status = PropertyStatus.Hotel;
        }
    }
}
