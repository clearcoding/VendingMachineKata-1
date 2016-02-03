using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
  /// <summary>
  /// Interface for vending machine
  /// </summary>
  public interface IVendingMachine
  {
    /// <summary>
    /// Accepts coins
    /// </summary>
    void InsertCoin(InsertableCoinWeights coinWeight, InsertableCoinSizes coinSize);

    /// <summary>
    /// Current amount, in dollars, inserted into the machine
    /// </summary>
    decimal CurrentAmountInserted { get; }

    /// <summary>
    /// Coin return, consisting of types of coins and associated quantity
    /// </summary>
    ICoinCollection CoinReturn { get; }

    /// <summary>
    /// Gets the current display in use
    /// </summary>
    IDisplay Display { get; }

    /// <summary>
    /// Interface for product selection
    /// </summary>
    IProductSelector ProductSelectorButtons { get;  }
  }
}
