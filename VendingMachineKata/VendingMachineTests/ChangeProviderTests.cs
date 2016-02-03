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
      Assert.IsNull(this._changeProvider.MakeChange((decimal)0.00, new CoinCollection(), this._coinAppraiser));
    }

    [TestMethod]
    public void whenChangeIsNotMadeReturnNull()
    {
      // Arrange
      ICoinCollection makeChangeFrom = new CoinCollection();
      makeChangeFrom.Add(InsertedCoin.Nickel, 1);

      //Act & Assert
      Assert.IsNull(this._changeProvider.MakeChange((decimal)0.04, makeChangeFrom, this._coinAppraiser));
    }

    [TestMethod]
    public void whenNoAppraiserIsNotProvidedChangeCannotBeMade()
    {
      // Arrange
      ICoinCollection makeChangeFrom = new CoinCollection();
      makeChangeFrom.Add(InsertedCoin.Nickel, 5);

      //Act & Assert
      Assert.IsNull(this._changeProvider.MakeChange((decimal)0.15, makeChangeFrom, null));
    }

    [TestMethod]
    public void whenQuartersAreAvailableUseThem()
    {
      // Arrange
      ICoinCollection makeChangeFrom = new CoinCollection();
      makeChangeFrom.Add(InsertedCoin.Quarter, 5);

      //Act & Assert
      ICoinCollection change = this._changeProvider.MakeChange((decimal)0.50, makeChangeFrom, this._coinAppraiser);
      Assert.IsNotNull(change);
      Assert.AreEqual(2, change.GetNumberOf(InsertedCoin.Quarter));
      Assert.AreEqual(3, makeChangeFrom.GetNumberOf(InsertedCoin.Quarter));
    }

    [TestMethod]
    public void whenDimesAreAvailableUseThem()
    {
      // Arrange
      ICoinCollection makeChangeFrom = new CoinCollection();
      makeChangeFrom.Add(InsertedCoin.Quarter, 1);
      makeChangeFrom.Add(InsertedCoin.Dime, 6);

      //Act & Assert
      ICoinCollection change = this._changeProvider.MakeChange((decimal)0.75, makeChangeFrom, this._coinAppraiser);
      Assert.IsNotNull(change);
      Assert.AreEqual(1, change.GetNumberOf(InsertedCoin.Quarter));
      Assert.AreEqual(5, change.GetNumberOf(InsertedCoin.Dime));
      Assert.AreEqual(0, makeChangeFrom.GetNumberOf(InsertedCoin.Quarter));
      Assert.AreEqual(1, makeChangeFrom.GetNumberOf(InsertedCoin.Dime));
    }

    [TestMethod]
    public void whenNickelsAreAvailableUseThem()
    {
      // Arrange
      ICoinCollection makeChangeFrom = new CoinCollection();
      makeChangeFrom.Add(InsertedCoin.Quarter, 1);
      makeChangeFrom.Add(InsertedCoin.Dime, 6);
      makeChangeFrom.Add(InsertedCoin.Nickel, 5);

      //Act & Assert
      ICoinCollection change = this._changeProvider.MakeChange((decimal)1.00, makeChangeFrom, this._coinAppraiser);
      Assert.IsNotNull(change);
      Assert.AreEqual(1, change.GetNumberOf(InsertedCoin.Quarter));
      Assert.AreEqual(6, change.GetNumberOf(InsertedCoin.Dime));
      Assert.AreEqual(3, change.GetNumberOf(InsertedCoin.Nickel));
      Assert.AreEqual(0, makeChangeFrom.GetNumberOf(InsertedCoin.Quarter));
      Assert.AreEqual(0, makeChangeFrom.GetNumberOf(InsertedCoin.Dime));
      Assert.AreEqual(2, makeChangeFrom.GetNumberOf(InsertedCoin.Nickel));
    }
  }
}
