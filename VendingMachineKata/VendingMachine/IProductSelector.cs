﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
  /// <summary>
  /// Enum containing products that can be selected for purchase
  /// </summary>
  /// <remarks>
  /// Enum value is price of item, in cents
  /// </remarks>
  public enum ProductForSale
  {
    Cola = 100,
    Chips = 50,
    Candy = 65,
    Unknown = 0
  }
    
  /// <summary>
  /// Interface for product selection
  /// </summary>
  public interface IProductSelector
  {
    /// <summary>
    /// Gets/sets currently selected product
    /// </summary>
    ProductForSale SelectedProduct { get; set; }

    /// <summary>
    /// Event fired when selected product changes
    /// </summary>
    event EventHandler<ProductForSale> OnSelectedProductChanged;

    /// <summary>
    /// Determines if a product can be selected (i.e. it can be purchased and enough money is available)
    /// </summary>
    /// <param name="toPurchase">The product being selected for purchase</param>
    /// <param name="amountProvided">The money provided for the product, in dollars and cents</param>
    /// <returns>True if the product can be selected</returns>
    bool CanSelectedProductBePurchased(decimal amountProvided);

    /// <summary>
    /// Determines price of product, as a decimal (in dollars and cents)
    /// </summary>
    decimal GetProductPrice(ProductForSale product);
  }
}
