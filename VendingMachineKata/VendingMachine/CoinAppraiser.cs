using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
  public class CoinAppraiser : ICoinAppraiser
  {

    public decimal GetCoinValue(InsertedCoin coin)
    {
      if (coin == InsertedCoin.Nickel)
      {
        return (decimal)0.05;
      }
      if (coin == InsertedCoin.Dime)
      {
        return (decimal)0.10;
      }
      if (coin == InsertedCoin.Quarter)
      {
        return (decimal)0.25;
      }
      return (decimal)0.00;
    }

  }
}
