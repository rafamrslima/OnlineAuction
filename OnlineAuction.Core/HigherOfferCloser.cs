using Alura.LeilaoOnline.Core.Interfaces;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public class HigherOfferCloser : IEvaluationType
    {
        public double DestinyValue { get; }

        public HigherOfferCloser(double valorDestino)
        {
            DestinyValue = valorDestino;
        }

        public Bid Evaluate(Auction auction)
        {
            return auction.Bids
                .DefaultIfEmpty(new Bid(null, 0))
                .Where(l => l.Value > DestinyValue)
                .OrderBy(l => l.Value)
                .FirstOrDefault();
        }
    }
}
