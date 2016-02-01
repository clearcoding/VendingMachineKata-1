using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
  /// <summary>
  /// Interface for making change
  /// </summary>
  public interface IChangeProvider
  {
    /// <summary>
    /// Makes change for the specified amount
    /// </summary>
    /// <param name="amountOfChange">The amount of money for which change is required</param>
    /// <param name="makeChangeFrom">The change that we have to make change from, coin types as keys with quanities as the value</param>
    /// <param name="coinAppraiser">Used to determine value of coins in use</param>
    /// <returns>
    /// A dictionary containing all coins to dispense (types and quantity, similar to makeChangeFrom).   
    /// Null indicates change could not be made
    /// </returns>
    /// <remarks>The pool of coins within makeChangeFrom will be reduced by the change that was made</remarks>
    IDictionary<InsertedCoin, int> MakeChange(decimal amountOfChange, IDictionary<InsertedCoin, int> makeChangeFrom, ICoinAppraiser coinAppraiser);

  }
}
