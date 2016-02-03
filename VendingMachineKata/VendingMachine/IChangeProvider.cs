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
    /// <param name="makeChangeFrom">The collection of change that we have to make change from</param>
    /// <param name="coinAppraiser">Used to determine value of coins in use</param>
    /// <returns>
    /// A coin collection containing all coins to dispense
    /// Null indicates change could not be made
    /// </returns>
    /// <remarks>The pool of coins within makeChangeFrom will be reduced by the change that was made</remarks>
    ICoinCollection MakeChange(decimal amountOfChange, ICoinCollection makeChangeFrom, ICoinAppraiser coinAppraiser);

  }
}
