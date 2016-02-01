using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
  public class ChangeProvider : IChangeProvider
  {

    public IDictionary<InsertedCoin, int> MakeChange(decimal amountOfChange, IDictionary<InsertedCoin, int> makeChangeFrom, ICoinAppraiser coinAppraiser)
    {
      // Ensure we have a stash to make change from and that we have an appraisor to determine value of coinage
      if (
            (makeChangeFrom == null) ||
            (makeChangeFrom.Count == 0) ||
            (coinAppraiser == null)
         )
      {
        return null;
      }

      // Calculate change
      IDictionary<InsertedCoin, int> change = new Dictionary<InsertedCoin, int>();
      decimal remaining = amountOfChange;

      remaining = this.CreateChange(InsertedCoin.Quarter, change, remaining, makeChangeFrom, coinAppraiser);
      
      // Return
      if (change.Count == 0)
      {
        return null;
      }
      return change;
    }

    /// <summary>
    /// Helper function to get maximum change using specified coin, as well as to update running totals
    /// </summary>
    /// <param name="coinToUse">Coin to use</param>
    /// <param name="change">Change to be made</param>
    /// <param name="amountOfChange">The amount of change to make</param>
    /// <param name="makeChangeFrom">Pool of money to make change from</param>
    /// <param name="coinAppraiser">Provides values for coins</param>
    /// <returns>Remaining change to make</returns>
    private decimal CreateChange(
      InsertedCoin coinToUse,
      IDictionary<InsertedCoin, int> change,
      decimal amountOfChange,
      IDictionary<InsertedCoin, int> makeChangeFrom,
      ICoinAppraiser coinAppraiser)
    {
      decimal retVal = amountOfChange;

      // If the coinage exists within our pool to make change from, then do our adjustments
      if ((makeChangeFrom.ContainsKey(coinToUse)) && (retVal > 0))
      {
        decimal coinValue = coinAppraiser.GetCoinValue(coinToUse);
        int numCoins = (int)(retVal / coinValue);
        numCoins = Math.Min(numCoins, makeChangeFrom[coinToUse]);
        retVal -= (numCoins * coinValue);
        makeChangeFrom[coinToUse] -= numCoins;
        if (change.ContainsKey(coinToUse))
        {
          change[coinToUse] += numCoins;
        }
        else
        {
          change.Add(coinToUse, numCoins);
        }
      }

      // Return what's left
      return retVal;
    }
  }
}
