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
  }
}
