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
  /// Class for testing the vending machines change provider
  /// </summary>
  [TestClass]
  public class ChangeProviderTests
  {
    private IChangeProvider _changeProvider = null;           // Change provider UUT
    private ICoinAppraiser _coinAppraiser = null;             // Coin approiser helper

    [TestInitialize]
    public void arrangeForTests()
    {
      //Arrange
      this._changeProvider = new ChangeProvider();
      this._coinAppraiser = new CoinAppraiser();              //todo: evaluate use of mock
    }

    [TestMethod]
    public void whenThereIsNothingToMakeChangeFromItCannnotBeProvided()
    {
      //Act & Assert
      Assert.IsNull(this._changeProvider.MakeChange((decimal)0.00, null, this._coinAppraiser));
      Assert.IsNull(this._changeProvider.MakeChange((decimal)0.00, new Dictionary<InsertedCoin, int>(), this._coinAppraiser));
    }

    [TestMethod]
    public void whenChangeIsNotMadeReturnNull()
    {
      // Arrange
      Dictionary<InsertedCoin, int> makeChangeFrom = new Dictionary<InsertedCoin, int>()
      {
        {InsertedCoin.Nickel, 1}
      };

      //Act & Assert
      Assert.IsNull(this._changeProvider.MakeChange((decimal)0.04, makeChangeFrom, this._coinAppraiser));
    }

    [TestMethod]
    public void whenNoAppraiserIsNotProvidedChangeCannotBeMade()
    {
      // Arrange
      Dictionary<InsertedCoin, int> makeChangeFrom = new Dictionary<InsertedCoin, int>()
      {
        {InsertedCoin.Nickel, 5}
      };

      //Act & Assert
      Assert.IsNull(this._changeProvider.MakeChange((decimal)0.15, makeChangeFrom, null));
    }

    [TestMethod]
    public void whenQuartersAreAvailableUseThem()
    {
      // Arrange
      Dictionary<InsertedCoin, int> makeChangeFrom = new Dictionary<InsertedCoin, int>()
      {
        {InsertedCoin.Quarter, 5}
      };

      //Act & Assert
      IDictionary<InsertedCoin, int> change = this._changeProvider.MakeChange((decimal)0.50, makeChangeFrom, this._coinAppraiser);
      Assert.IsNotNull(change);
      Assert.AreEqual(2, change[InsertedCoin.Quarter]);
      Assert.AreEqual(3, makeChangeFrom[InsertedCoin.Quarter]);
    }

    [TestMethod]
    public void whenDimesAreAvailableUseThem()
    {
      // Arrange
      Dictionary<InsertedCoin, int> makeChangeFrom = new Dictionary<InsertedCoin, int>()
      {
        {InsertedCoin.Quarter, 1},
        {InsertedCoin.Dime, 6}
      };

      //Act & Assert
      IDictionary<InsertedCoin, int> change = this._changeProvider.MakeChange((decimal)0.75, makeChangeFrom, this._coinAppraiser);
      Assert.IsNotNull(change);
      Assert.AreEqual(1, change[InsertedCoin.Quarter]);
      Assert.AreEqual(5, change[InsertedCoin.Dime]);
      Assert.AreEqual(0, makeChangeFrom[InsertedCoin.Quarter]);
      Assert.AreEqual(1, makeChangeFrom[InsertedCoin.Dime]);
    }

  }
}
