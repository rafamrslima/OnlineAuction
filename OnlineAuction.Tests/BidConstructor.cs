using Alura.LeilaoOnline.Core;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class BidConstructor
    {
        [Fact]
        public void ThrowsArgumentExceptionForNegativeValues()
        {
            //Arranje
            var negativeValue = -100;

            //Assert
            Assert.Throws<System.ArgumentException>(
                //Act
                () => new Bid(null, negativeValue)
            );
        }

    }
}
