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


  }
}
