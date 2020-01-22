namespace Alura.LeilaoOnline.Core
{
    public class Bid
    {
        public Interested Client { get; }
        public double Value { get; }

        public Bid(Interested client, double value)
        {
            if (value < 0)
                throw new System.ArgumentException(" Bid`s value should be greater or equals to zero.");

            Client = client;
            Value = value;
        }
    }
}
