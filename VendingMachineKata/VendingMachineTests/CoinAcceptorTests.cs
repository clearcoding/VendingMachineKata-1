using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine;

namespace VendingMachineTests
{
  /// <summary>
  /// Class for testing the vending machines coin acceptor
  /// </summary>
  [TestClass]
  public class CoinAcceptorTests
  {
    private ICoinAcceptor _coinAcceptor = null;           // Coin acceptor UUT

    [TestInitialize]
    public void arrangeForTests()
    {
      //Arrange
      this._coinAcceptor = new CoinAcceptor();
    }

    [TestMethod]
    public void whenAPennyIsInsertedIntoTheCoinAcceptorItShouldBeRejected()
    {
      //Act & Assert
      Assert.AreEqual(InsertedCoin.Rejected, this._coinAcceptor.InsertCoin(InsertableCoinWeights.WeightOfPenny, InsertableCoinSizes.SizeOfPenny));
    }

    [TestMethod]
    public void whenANickelIsInsertedIntoTheCoinAcceptorItShouldBeAccepted()
    {
      //Act & Assert
      Assert.AreEqual(InsertedCoin.Nickel, this._coinAcceptor.InsertCoin(InsertableCoinWeights.WeightOfNickel, InsertableCoinSizes.SizeOfNickel));
    }

    //[TestMethod]
    //public void whenADimeIsInsertedIntoTheCoinAcceptorItShouldBeAccepted()
    //{
    //  //Act & Assert
    //  Assert.IsTrue(this._coinAcceptor.InsertCoin(InsertableCoinWeights.Dime));
    //}

    //[TestMethod]
    //public void whenAQuarterIsInsertedIntoTheCoinAcceptorItShouldBeAccepted()
    //{
    //  //Act & Assert
    //  Assert.IsTrue(this._coinAcceptor.InsertCoin(InsertableCoinWeights.Quarter));
    //}

    //[TestMethod]
    //public void whenACoinIsInsertedIntoTheCoinAcceptorOnlyQuartersDimesAndNickelsShouldBeAccepted()
    //{
    //  //Act & Assert
    //  const int foreignCoin = 56;                                                   // Some foreign coin value not within our known coins
    //  Assert.IsFalse(Enum.IsDefined(typeof(InsertableCoinWeights), foreignCoin));         // Ensure our test value doesn't happen to be a valid coin by some chance
    //  Assert.IsFalse(this._coinAcceptor.InsertCoin((InsertableCoinWeights)foreignCoin));  // Someone's trying to trick us!
    //}

  }
}
