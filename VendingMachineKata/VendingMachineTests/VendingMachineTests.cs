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
  /// Class for testing the vending machines
  /// </summary>
  [TestClass]
  public class VendingMachineTests
  {
    private IVendingMachine _vendingMachine = null;           // Vending machine UUT

    [TestInitialize]
    public void arrangeForTests()
    {
      //Arrange
      this._vendingMachine = new VendingMachine.VendingMachine(
        new CoinAcceptor(),
        new CoinAppraiser());
    }

    [TestMethod]
    public void whenAValidCoinIsInsertedTheCurrentAmountInsertedNeedsToBeUpdated()
    {
      //Act & Assert
      this._vendingMachine.InsertCoin(InsertableCoinWeights.WeightOfNickel, InsertableCoinSizes.SizeOfNickel);
      Assert.AreEqual((decimal)0.05, this._vendingMachine.CurrentAmountInserted);
    }

  }
}
