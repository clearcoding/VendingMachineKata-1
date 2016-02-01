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
  /// Class for testing the coin appraiser
  /// </summary>
  [TestClass]
  public class CoinAppraiserTests
  {
    private ICoinAppraiser _coinAppraiser = null;           // Coin appraiser UUT

    [TestInitialize]
    public void arrangeForTests()
    {
      //Arrange
      this._coinAppraiser = new CoinAppraiser();
    }

    [TestMethod]
    public void aNickelIsWorthFiveCents()
    {
      //Act & Assert
      Assert.AreEqual((decimal)0.05, this._coinAppraiser.GetCoinValue(InsertedCoin.Nickel));
    }

    [TestMethod]
    public void aDimeIsWorthTenCents()
    {
      //Act & Assert
      Assert.AreEqual((decimal)0.10, this._coinAppraiser.GetCoinValue(InsertedCoin.Dime));
    }

    [TestMethod]
    public void aQuarterIsWorthTwentyFiveCents()
    {
      //Act & Assert
      Assert.AreEqual((decimal)0.25, this._coinAppraiser.GetCoinValue(InsertedCoin.Quarter));
    }

    [TestMethod]
    public void aRejectedCoinIsWorthNothing()
    {
      //Act & Assert
      Assert.AreEqual((decimal)0.00, this._coinAppraiser.GetCoinValue(InsertedCoin.Rejected));
    }


  }
}
