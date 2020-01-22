using Alura.LeilaoOnline.Core.Interfaces;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public class HigherValue : IEvaluationType
    {
        public Bid Evaluate(Auction auction)
        {
            return auction.Bids
                .DefaultIfEmpty(new Bid(null, 0))
                .OrderBy(l => l.Value)
                .LastOrDefault();
        }
    }
}
