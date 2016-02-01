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
    /// <param name="makeChangeFrom">The change that we have to make change from</param>
    /// <returns>A list containing all coins to dispense.   Null indicates change could not be made</returns>
    IList<InsertedCoin> MakeChange(decimal amountOfChange, IDictionary<InsertedCoin, int> makeChangeFrom);

  }
}
