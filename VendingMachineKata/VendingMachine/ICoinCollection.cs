using System;
using System.Collections.Generic;

namespace VendingMachine
{
  /// <summary>
  /// Interface for coin holder
  /// </summary>
  public interface ICoinCollection
  {
    /// <summary>
    /// Gets all coins held
    /// </summary>
    IDictionary<InsertedCoin, int> AllCoins { get; set; }

    /// <summary>
    /// Gets the number of the specified type of coin
    /// </summary>
    /// <param name="typeOfCoin">The type of coin we are curious about</param>
    /// <returns>Total quantity of this coin</returns>
    int GetNumberOf(InsertedCoin typeOfCoin);

    /// <summary>
    /// Adds specified number of coins to collection
    /// </summary>
    /// <param name="typeOfCoin">The type of coin we are adding</param>
    /// <param name="quantity">The quanity of coins to add</param>
    void Add(InsertedCoin typeOfCoin, int quantity);
  }
}
