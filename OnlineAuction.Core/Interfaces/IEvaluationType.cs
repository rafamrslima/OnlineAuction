namespace Alura.LeilaoOnline.Core.Interfaces
{
    public interface IEvaluationType
    {
        Bid Evaluate(Auction auction);
    }
}
