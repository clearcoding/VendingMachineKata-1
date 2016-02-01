using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
  public class CoinAcceptor : ICoinAcceptor
  {

    public InsertedCoin InsertCoin(InsertableCoinWeights weightOfCoin, InsertableCoinSizes sizeOfCoin)
    {
      if ((weightOfCoin == InsertableCoinWeights.WeightOfNickel) && (sizeOfCoin == InsertableCoinSizes.SizeOfNickel))
      {
        return InsertedCoin.Nickel;
      }
      if ((weightOfCoin == InsertableCoinWeights.WeightOfDime) && (sizeOfCoin == InsertableCoinSizes.SizeOfDime))
      {
        return InsertedCoin.Dime;
      }
      if ((weightOfCoin == InsertableCoinWeights.WeightOfQuarter) && (sizeOfCoin == InsertableCoinSizes.SizeOfQuarter))
      {
        return InsertedCoin.Quarter;
      }
      return InsertedCoin.Rejected;
    }

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
