﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
  /// <summary>
  /// Interface for coin acceptor
  /// </summary>
  public class CoinAcceptor : ICoinAcceptor
  {
    /// <summary>
    /// Inserts a coin into the acceptor
    /// </summary>
    /// <param name="coinToInsert">Coin to be inserted into the acceptor</param>
    /// <returns>True if the coin was accepted, false if not</returns>
    public bool InsertCoin(InsertableCoins coinToInsert)
    {
      switch (coinToInsert)
      {
        case InsertableCoins.Nickel:
          return true;
      }
      return false;      
    }
  }
}