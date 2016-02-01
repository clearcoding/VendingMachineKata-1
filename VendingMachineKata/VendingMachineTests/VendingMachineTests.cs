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
      // NOTE: IoC probably needed in future
      this._vendingMachine = new VendingMachine.VendingMachine(
        new CoinAcceptor(),
        new CoinAppraiser(),
        new Dictionary<InsertedCoin, int>(),
        new Display(),
        new ProductSelector());
    }

    [TestMethod]
    public void whenAValidCoinIsInsertedTheCurrentAmountInsertedNeedsToBeUpdatedAlongWithDisplay()
    {
      //Act & Assert
      this._vendingMachine.InsertCoin(InsertableCoinWeights.WeightOfNickel, InsertableCoinSizes.SizeOfNickel);
      Assert.AreEqual((decimal)0.05, this._vendingMachine.CurrentAmountInserted);
      Assert.AreEqual("$0.05", this._vendingMachine.Display.Message);
    }

    [TestMethod]
    public void whenAnInvalidCoinIsInsertedItShouldBePlacedInTheCoinReturn()
    {
      //Act & Assert
      this._vendingMachine.InsertCoin(InsertableCoinWeights.WeightOfPenny, InsertableCoinSizes.SizeOfNickel);
      Assert.AreEqual((decimal)0.00, this._vendingMachine.CurrentAmountInserted);
      Assert.AreEqual(1, this._vendingMachine.CoinReturn[InsertedCoin.Rejected]);
    }

    [TestMethod]
    public void whenNothingIsInsertedDisplayInsertCoinsMessage()
    {
      //Act & Assert
      this._vendingMachine.InsertCoin(InsertableCoinWeights.WeightOfPenny, InsertableCoinSizes.SizeOfNickel);
      Assert.AreEqual(VendingMachine.VendingMachine.InsertCoinsMessage, this._vendingMachine.Display.Message);
    }

    [TestMethod]
    public void whenInitiallyStartedShouldDisplayInsertCoinsMessage()
    {
      //Act & Assert
      Assert.AreEqual(VendingMachine.VendingMachine.InsertCoinsMessage, this._vendingMachine.Display.Message);
    }

    [TestMethod]
    public void whenEnoughMoneyHasBeenInsertedToBuyItemThenThankTheUserAndNextReadResetsDisplay()
    {
      // Special arrangements here as we need to ensure events are complete 
      // before enforcing our assertions
      System.Threading.ManualResetEvent eventProduct = new System.Threading.ManualResetEvent(false);
      System.Threading.ManualResetEvent eventNextRead = new System.Threading.ManualResetEvent(false);
      bool wasProductRaised = false;
      bool wasReadRaised = false;

      // Insert coins
      this._vendingMachine.InsertCoin(InsertableCoinWeights.WeightOfQuarter, InsertableCoinSizes.SizeOfQuarter);
      this._vendingMachine.InsertCoin(InsertableCoinWeights.WeightOfQuarter, InsertableCoinSizes.SizeOfQuarter);

      // Tricky, after product is dispensed, we will set thank you message, but ALSO on next read
      // it will be a different message.   Wait for product changed handler to complete.
      this._vendingMachine.ProductSelectorButtons.OnSelectedProductChanged += delegate (object sender, ProductForSale e)
      {
        wasProductRaised = true;
        eventProduct.Set();
      };
      this._vendingMachine.Display.OnNextRead += delegate (object sender, EventArgs e)
      {
        wasReadRaised = true;
        eventNextRead.Set();
      };

      // This will start the events firing
      this._vendingMachine.ProductSelectorButtons.SelectedProduct = ProductForSale.Chips;

      eventProduct.WaitOne(5000, false);
      Assert.IsTrue(wasProductRaised);       // Ensures no false positive from a timeout
      Assert.AreEqual((decimal)0.50, this._vendingMachine.CurrentAmountInserted);         // Make sure current amount inserted remains untouched until next read

      string dummy = this._vendingMachine.Display.Message;
      eventNextRead.WaitOne(5000, false);
      Assert.IsTrue(wasReadRaised);         // Ensures no false positive from a timeout

      Assert.AreEqual((decimal)0.00, this._vendingMachine.CurrentAmountInserted);         // Make sure current amount inserted is reset after read
      Assert.AreEqual(VendingMachine.VendingMachine.ThankYouMessage, this._vendingMachine.Display.PreviousMessage);
      Assert.AreEqual(VendingMachine.VendingMachine.InsertCoinsMessage, this._vendingMachine.Display.Message);
    }

    [TestMethod]
    public void whenThereIsNotEnoughMoneyInsertedIndicatePriceAndNextReadResetsDisplayToCurrentAmountInserted()
    {
      // Special arrangements here as we need to ensure events are complete 
      // before enforcing our assertions
      System.Threading.ManualResetEvent eventProduct = new System.Threading.ManualResetEvent(false);
      System.Threading.ManualResetEvent eventNextRead = new System.Threading.ManualResetEvent(false);
      bool wasProductRaised = false;
      bool wasReadRaised = false;

      // Insert coins
      this._vendingMachine.InsertCoin(InsertableCoinWeights.WeightOfQuarter, InsertableCoinSizes.SizeOfQuarter);

      // Tricky, after product is dispensed, we will set thank you message, but ALSO on next read
      // it will be a different message.   Wait for product changed handler to complete.
      this._vendingMachine.ProductSelectorButtons.OnSelectedProductChanged += delegate (object sender, ProductForSale e)
      {
        wasProductRaised = true;
        eventProduct.Set();
      };
      this._vendingMachine.Display.OnNextRead += delegate (object sender, EventArgs e)
      {
        wasReadRaised = true;
        eventNextRead.Set();
      };

      // This will start the events firing
      this._vendingMachine.ProductSelectorButtons.SelectedProduct = ProductForSale.Chips;

      eventProduct.WaitOne(5000, false);
      Assert.IsTrue(wasProductRaised);       // Ensures no false positive from a timeout
      Assert.AreEqual((decimal)0.25, this._vendingMachine.CurrentAmountInserted);         // Make sure current amount inserted remains untouched until next read

      string dummy = this._vendingMachine.Display.Message;
      eventNextRead.WaitOne(5000, false);
      Assert.IsTrue(wasReadRaised);         // Ensures no false positive from a timeout

      Assert.AreEqual((decimal)0.25, this._vendingMachine.CurrentAmountInserted);         // Make sure current amount inserted is reset after read
      Assert.AreEqual("PRICE $0.50", this._vendingMachine.Display.PreviousMessage);
      Assert.AreEqual("$0.25", this._vendingMachine.Display.Message);
    }
  }
}
