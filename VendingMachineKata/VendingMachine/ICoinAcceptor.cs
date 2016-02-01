using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
  /// <summary>
  /// Enum containing weights of coins that may be placed in an ICoinAcceptor
  /// </summary>
  public enum InsertableCoinWeights
  {
    WeightOfPenny,
    WeightOfNickel,
    WeightOfDime,
    WeightOfQuarter
  }

  /// <summary>
  /// Enum containing sizes of coins that may be placed in an ICoinAcceptor
  /// </summary>
  public enum InsertableCoinSizes
  {
    SizeOfPenny,
    SizeOfNickel,
    SizeOfDime,
    SizeOfQuarter
  }

  /// <summary>
  /// Enum containing coins that may be placed in an ICoinAcceptor
  /// </summary>
  public enum InsertedCoin
  {
    Nickel,
    Dime,
    Quarter,
    Rejected
  }
    
  /// <summary>
  /// Interface for coin acceptor
  /// </summary>
  public interface ICoinAcceptor
  {
    /// <summary>
    /// Inserts a coin into the acceptor
    /// </summary>
    /// <param name="weightOfCoin">Weight of coin inserted into the acceptor</param>
    /// <returns>InsertableCoins.Rejected if an unknown coin, other enumeration values if valid</returns>
    InsertedCoin InsertCoin(InsertableCoinWeights weightOfCoin, InsertableCoinSizes sizeOfCoin);

  }
}
