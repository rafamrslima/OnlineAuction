using Alura.LeilaoOnline.Core.Enums;
using System.Collections.Generic;
using Alura.LeilaoOnline.Core.Interfaces;

namespace Alura.LeilaoOnline.Core
{
    public class Auction
    {
        private Interested _lastClient;
        private readonly IList<Bid> _bids;
        private IEvaluationType _evaluator;

        public IEnumerable<Bid> Bids => _bids;
        public string Item { get; }
        public Bid BidWinner { get; private set; }
        public AuctionState State { get; private set; }

        public Auction(string item, IEvaluationType evaluator)
        {
            Item = item;
            _bids = new List<Bid>();
            State = AuctionState.NotStarted;
            _evaluator = evaluator;
        }

        private bool NewBidIsAccepted(Interested client)
        {
            return State == AuctionState.AuctionOnGoing && client != _lastClient;
        }

        public void ReceiveBid(Interested client, double value)
        {
            if (NewBidIsAccepted(client))
            {
                _bids.Add(new Bid(client, value));
                _lastClient = client;
            }
        }

        public void StartAuction()
        {
            State = AuctionState.AuctionOnGoing;
        }

        public void FinishAuction()
        {
            if (State != AuctionState.AuctionOnGoing)
                throw new System.InvalidOperationException("Is not possible to finish the auction before it has started.");

            BidWinner = _evaluator.Evaluate(this);
            State = AuctionState.AuctionFinished;
        }
    }
}
