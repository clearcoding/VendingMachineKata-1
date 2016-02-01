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

    [TestMethod]
    public void whenADimeIsInsertedIntoTheCoinAcceptorItShouldBeAccepted()
    {
      //Act & Assert
      Assert.AreEqual(InsertedCoin.Dime, this._coinAcceptor.InsertCoin(InsertableCoinWeights.WeightOfDime, InsertableCoinSizes.SizeOfDime));
    }

    [TestMethod]
    public void whenAQuarterIsInsertedIntoTheCoinAcceptorItShouldBeAccepted()
    {
      //Act & Assert
      Assert.AreEqual(InsertedCoin.Quarter, this._coinAcceptor.InsertCoin(InsertableCoinWeights.WeightOfQuarter, InsertableCoinSizes.SizeOfQuarter));
    }

    [TestMethod]
    public void whenACoinIsInsertedIntoTheCoinAcceptorOnlyWeightsOfQuartersDimesAndNickelsShouldBeAccepted()
    {
      //Act & Assert
      const int foreignCoinWeight = 56;       // Some foreign coin weight not within our known coins

      Assert.IsFalse(Enum.IsDefined(typeof(InsertableCoinWeights), foreignCoinWeight));    // Ensure our test value doesn't happen to be a valid coin by some chance

      // Validate against all known sizes
      var sizes = Enum.GetValues(typeof(InsertableCoinSizes));
      foreach (InsertableCoinSizes size in sizes)
      {
        Assert.AreEqual(InsertedCoin.Rejected, this._coinAcceptor.InsertCoin((InsertableCoinWeights)foreignCoinWeight, size));    // Someone's trying to trick us!
      }
    }

  }
}
