using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
  /// <summary>
  /// Enum containing coins that may be placed in an ICoinAcceptor
  /// </summary>
  /// <remarks>
  /// Enum value represents value of coin, in cents
  /// </remarks>
  public enum InsertableCoins
  {
    Penny = 1,
    Nickel = 5,
    Dime = 10,
    Quarter = 25
  }

  /// <summary>
  /// Interface for coin acceptor
  /// </summary>
  public interface ICoinAcceptor
  {
    /// <summary>
    /// Inserts a coin into the acceptor
    /// </summary>
    /// <param name="coinToInsert">Coin to be inserted into the acceptor</param>
    /// <returns>True if the coin was accepted, false if not</returns>
    bool InsertCoin(InsertableCoins coinToInsert);
  }
}
