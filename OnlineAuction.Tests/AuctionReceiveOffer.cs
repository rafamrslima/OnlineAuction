using Xunit;
using Alura.LeilaoOnline.Core;
using System.Linq;
using Alura.LeilaoOnline.Core.Interfaces;

namespace Alura.LeilaoOnline.Tests
{
    public class AuctionReceiveOffer
    {
        [Fact]
        public void DoesntAcceptBidFromTheLastClientWhoMadeBid()
        {
            //Arranje
            IEvaluationType evaluationType = new HigherValue();
            var auction = new Auction("Van Gogh", evaluationType);
            var john = new Interested("John", auction);
            auction.StartAuction();
            auction.ReceiveBid(john, 800);

            //Act 
            auction.ReceiveBid(john, 1000);

            //Assert
            var quantityExpected = 1;
            var quantityGot = auction.Bids.Count();
            Assert.Equal(quantityExpected, quantityGot);
        }

        [Theory]
        [InlineData(4, new double[] { 1000, 1200, 1400, 1300 })]
        [InlineData(2, new double[] { 800, 900 })]
        public void DoesntAllowNewBidsAfterAuctionIsFinished(int quantityExpected, double[] offers)
        {
            //Arranje 
            IEvaluationType evaluationType = new HigherValue();
            var auction = new Auction("Van Gogh", evaluationType);
            var john = new Interested("Jhon", auction);
            var mike = new Interested("Mike", auction);

            auction.StartAuction();
            for (int i = 0; i < offers.Length; i++)
            {
                var value = offers[i];
                if ((i % 2) == 0)
                    auction.ReceiveBid(john, value);
                else
                    auction.ReceiveBid(mike, value); 
            }

            auction.FinishAuction();

            //Act 
            auction.ReceiveBid(john, 1000);

            //Assert
            var quantityGot = auction.Bids.Count();
            Assert.Equal(quantityExpected, quantityGot);
        }
    }
}
