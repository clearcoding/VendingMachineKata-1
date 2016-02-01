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
      Assert.AreEqual((decimal)1.00, this._productSelector.GetProductPrice(ProductsForSale.Cola));
    }

  }
}
