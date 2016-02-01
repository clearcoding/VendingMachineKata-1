using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
  /// <summary>
  /// Interface for coin valuation
  /// </summary>
  public interface ICoinAppraiser
  {
    /// <summary>
    /// Gets value of coin
    /// </summary>
    /// <param name="coin">The coin to obtain value from</param>
    /// <returns>Value of coin, in dollars</returns>
    decimal GetCoinValue(InsertedCoin coin);
  }
}
