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
    [TestMethod]
    public void whenAPennyIsInsertedIntoTheCoinAcceptorItShouldBeRejected()
    {
      //Arrange
      ICoinAcceptor acceptor = new CoinAcceptor();

      //Act
      bool accepted = acceptor.InsertCoin(AllowableCoins.Penny);

      //Assert
      Assert.IsFalse(accepted);
    }
  }
}
