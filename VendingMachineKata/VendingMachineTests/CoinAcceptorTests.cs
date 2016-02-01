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
      Assert.IsFalse(this._coinAcceptor.InsertCoin(InsertableCoins.Penny));
    }

    [TestMethod]
    public void whenANickelIsInsertedIntoTheCoinAcceptorItShouldBeAccepted()
    {
      //Act & Assert
      Assert.IsTrue(this._coinAcceptor.InsertCoin(InsertableCoins.Nickel));
    }

    [TestMethod]
    public void whenADimeIsInsertedIntoTheCoinAcceptorItShouldBeAccepted()
    {
      //Act & Assert
      Assert.IsTrue(this._coinAcceptor.InsertCoin(InsertableCoins.Dime));
    }

    [TestMethod]
    public void whenAQuarterIsInsertedIntoTheCoinAcceptorItShouldBeAccepted()
    {
      //Act & Assert
      Assert.IsTrue(this._coinAcceptor.InsertCoin(InsertableCoins.Quarter));
    }

  }
}
