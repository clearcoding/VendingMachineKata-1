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
  /// Class for testing the vending machines product selector
  /// </summary>
  [TestClass]
  public class ProductSelectorTests
  {
    private IProductSelector _productSelector = null;           // Product selector UUT

    [TestInitialize]
    public void arrangeForTests()
    {
      //Arrange
      this._productSelector = new ProductSelector();
    }

    [TestMethod]
    public void aColaShouldCostADollar()
    {
      //Act & Assert
      Assert.AreEqual((decimal)1.00, this._productSelector.GetProductPrice(ProductForSale.Cola));
    }

    [TestMethod]
    public void aBagOfChipsShouldCostFiftyCents()
    {
      //Act & Assert
      Assert.AreEqual((decimal)0.50, this._productSelector.GetProductPrice(ProductForSale.Chips));
    }

    [TestMethod]
    public void aPieceOfCandyShouldCostSixtyFiveCents()
    {
      //Act & Assert
      Assert.AreEqual((decimal)0.65, this._productSelector.GetProductPrice(ProductForSale.Candy));
    }

    [TestMethod]
    public void whenThereIsNotEnoughMoneyTheProductSelectorShouldNotRecommendPurchase()
    {
      //Act & Assert
      this._productSelector.SelectedProduct = ProductForSale.Cola;
      Assert.IsFalse(this._productSelector.CanSelectedProductBePurchased(this._productSelector.GetProductPrice(ProductForSale.Cola) - (decimal)0.01));
    }

    [TestMethod]
    public void whenThereIsJustEnoughMoneyTheProductSelectorShouldRecommendPurchase()
    {
      //Act & Assert
      this._productSelector.SelectedProduct = ProductForSale.Cola;
      Assert.IsTrue(this._productSelector.CanSelectedProductBePurchased(this._productSelector.GetProductPrice(ProductForSale.Cola)));
    }

    [TestMethod]
    public void whenThereIsMoreThanEnoughMoneyTheProductSelectorShouldRecommendPurchase()
    {
      //Act & Assert
      this._productSelector.SelectedProduct = ProductForSale.Cola;
      Assert.IsTrue(this._productSelector.CanSelectedProductBePurchased(this._productSelector.GetProductPrice(ProductForSale.Cola) + (decimal)0.01));
    }

    [TestMethod]
    public void whenProductSelectorStartsItShouldNotHaveAProductSelected()
    {
      //Act & Assert
      Assert.AreEqual(ProductForSale.Unknown, this._productSelector.SelectedProduct);
    }

    [TestMethod]
    public void whenThereIsNoProductSelectedTheProductSelectorShouldNotRecommendPurchase()
    {
      //Act & Assert
      this._productSelector.SelectedProduct = ProductForSale.Unknown;
      Assert.IsFalse(this._productSelector.CanSelectedProductBePurchased(this._productSelector.GetProductPrice(ProductForSale.Cola)));
    }


  }
}
