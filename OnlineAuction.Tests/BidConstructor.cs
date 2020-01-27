using Alura.LeilaoOnline.Core;
using System;
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

            //Act
            Action act = () => new Bid(null, negativeValue);

            //assert
             Assert.Equal("Bid`s value should be greater or equals to zero.", 
                 Assert.Throws<ArgumentException>(act).Message); 
        }

    }
}
