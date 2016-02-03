using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
  public class ChangeProvider : IChangeProvider
  {

    public ICoinCollection MakeChange(decimal amountOfChange, ICoinCollection makeChangeFrom, ICoinAppraiser coinAppraiser)
    {
      // Ensure we have a stash to make change from and that we have an appraisor to determine value of coinage
      if (
            (makeChangeFrom == null) ||
            (coinAppraiser == null)
         )
      {
        return null;
      }

      // Calculate change

      //*****************************************************
      //*****************************************************
      // todo: smells, need second set of eyes
      // there are better ways to do this, but for now IJW
      // it's covered under test, so we should know if it fails
      // Although, I doubt full code coverage exists within current 
      // test harness

      ICoinCollection change = new CoinCollection();
      decimal remaining = amountOfChange;

      remaining = this.CreateChange(InsertedCoin.Quarter, change, remaining, makeChangeFrom, coinAppraiser);
      remaining = this.CreateChange(InsertedCoin.Dime, change, remaining, makeChangeFrom, coinAppraiser);
      remaining = this.CreateChange(InsertedCoin.Nickel, change, remaining, makeChangeFrom, coinAppraiser);

      //
      //*****************************************************
      //*****************************************************

      // Return
      //todo: refactoring for use if ICoinCollection, but need to add HasAny method to collection
      if ((!change.HasAnyCoins) || (remaining > 0))
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
      ICoinCollection change,
      decimal amountOfChange,
      ICoinCollection makeChangeFrom,
      ICoinAppraiser coinAppraiser)
    {
      decimal retVal = amountOfChange;
      int totalNumberOfCoins = makeChangeFrom.GetNumberOf(coinToUse);

      // If the coinage exists within our pool to make change from, then do our adjustments
      if ((totalNumberOfCoins > 0) && (retVal > 0))
      {
        decimal coinValue = coinAppraiser.GetCoinValue(coinToUse);
        int desiredNumberOfCoins = (int)(retVal / coinValue);
        desiredNumberOfCoins = Math.Min(desiredNumberOfCoins, totalNumberOfCoins);
        retVal -= (desiredNumberOfCoins * coinValue);
        makeChangeFrom.Remove(coinToUse, desiredNumberOfCoins);      
        change.Add(coinToUse, desiredNumberOfCoins);
      }

      // Return what's left
      return retVal;
    }
  }
}
