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
  /// Class for testing a coin holder
  /// </summary>
  [TestClass]
  public class CoinCollectionTests
  {
    private ICoinCollection _coinCollection = null;           // Coin acceptor UUT

    [TestInitialize]
    public void arrangeForTests()
    {
      //Arrange
      this._coinCollection = new CoinCollection();
    }

    [TestMethod]
    public void shouldAllowAllCoinsToBeRetrievedOrSet()
    {
      // Arrange
      IDictionary<InsertedCoin, int> coinage = new Dictionary<InsertedCoin, int>() { { InsertedCoin.Quarter, 10 } };

      //Act & Assert
      Assert.IsNotNull(this._coinCollection);
      Assert.AreEqual(0, this._coinCollection.AllCoins.Count);
      this._coinCollection.AllCoins = coinage;
      Assert.IsNotNull(this._coinCollection);
      Assert.AreEqual(1, this._coinCollection.AllCoins.Count);
      Assert.AreEqual(10, this._coinCollection.AllCoins[InsertedCoin.Quarter]);
    }

    [TestMethod]
    public void shouldAllowTypesOfCoinsToBeAddedToTheCollection()
    {
      //Act & Assert
      this._coinCollection.Add(InsertedCoin.Quarter, 5);
      Assert.AreEqual(5, this._coinCollection.AllCoins[InsertedCoin.Quarter]);

      //Act & Assert [Test code path where coin already exists]
      this._coinCollection.Add(InsertedCoin.Quarter, 5);
      Assert.AreEqual(10, this._coinCollection.AllCoins[InsertedCoin.Quarter]);
    }

    [TestMethod]
    public void shouldAllowSingleCoinToBeAddedToTheCollection()
    {
      //Act & Assert
      this._coinCollection.Add(InsertedCoin.Quarter);
      Assert.AreEqual(1, this._coinCollection.AllCoins[InsertedCoin.Quarter]);
    }

  }
}
