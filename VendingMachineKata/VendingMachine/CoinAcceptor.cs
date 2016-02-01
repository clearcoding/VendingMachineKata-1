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

  }
}
