using Alura.LeilaoOnline.Core;
using Xunit;
using Alura.LeilaoOnline.Core.Interfaces;

namespace Alura.LeilaoOnline.Tests
{
    public class FinishAuction
    {
        [Theory]
        [InlineData(1200, 1250, new double[] { 800, 1150, 1400, 1250 })]
        public void ReturnHigherValueCloserForAuctionOfThisType(
            double destinyValue,
            double expectedValue,
            double[] offers)
        {
            //Arranje
            IEvaluationType evaluationType = new HigherOfferCloser(destinyValue);
            var auction = new Auction("Van Gogh", evaluationType);
            var john = new Interested("Jhon", auction);
            var mike = new Interested("Mike", auction);

            auction.StartAuction();
            for (int i = 0; i < offers.Length; i++)
            {
                if ((i % 2 == 0))
                    auction.ReceiveBid(john, offers[i]);
                else
                    auction.ReceiveBid(mike, offers[i]);
            }

            //Act
            auction.FinishAuction();

            //Assert
            Assert.Equal(expectedValue, auction.BidWinner.Value);
        }

        [Theory]
        [InlineData(1200, new double[] { 800, 900, 1000, 1200 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800 })]
        public void ReturnHigherValueForAuctionWithAtLeastOneAuction(
            double expectedValue,
            double[] offers)
        {
            //Arranje  
            IEvaluationType evaluationType = new HigherValue();
            var auction = new Auction("Van Gogh", evaluationType);
            var john = new Interested("Jhon", auction);
            var mike = new Interested("Mike", auction);
            auction.StartAuction();

            for (int i = 0; i < offers.Length; i++)
            {
                var valor = offers[i];
                if ((i % 2) == 0)
                    auction.ReceiveBid(john, valor);
                else
                    auction.ReceiveBid(mike, valor);
            }

            //Act 
            auction.FinishAuction();

            //Assert
            var valueGot = auction.BidWinner.Value;
            Assert.Equal(expectedValue, valueGot);
        }

        [Fact]
        public void ReturnInvalidOperationExceptionForAuctionNotStarted()
        {
            //Arranje 
            IEvaluationType evaluationType = new HigherValue();
            var auction = new Auction("Van Gogh", evaluationType);

            //Assert
            var exception = Assert.Throws<System.InvalidOperationException>(
                //Act - método sob teste
                () => auction.FinishAuction()
            );

            var expectedMessage = "Is not possible to finish the auction before it has started.";
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void ReturnZeroForAuctionsWithoutBids()
        {
            //Arranje 
            IEvaluationType evaluationType = new HigherValue();
            var auction = new Auction("Van Gogh", evaluationType);
            auction.StartAuction();

            //Act 
            auction.FinishAuction();

            //Assert
            var expectedValue = 0;
            var valueGot = auction.BidWinner.Value;

            Assert.Equal(expectedValue, valueGot);
        }
    }
}
