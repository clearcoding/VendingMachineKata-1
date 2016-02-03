using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
  public class CoinCollection : ICoinCollection
  {

    #region Constructors

    public CoinCollection() :
      this(new Dictionary<InsertedCoin, int>())
    {
    }

    public CoinCollection(IDictionary<InsertedCoin, int> coinsToHold)
    {
      this.AllCoins = coinsToHold;
    }

    #endregion

    #region ICoinHolder Members

    public IDictionary<InsertedCoin, int> AllCoins { get; set; }

    public int GetNumberOf(InsertedCoin typeOfCoin)
    {
      int retVal = 0;
      this.AllCoins.TryGetValue(typeOfCoin, out retVal);
      return retVal;
    }

    public void Add(InsertedCoin typeOfCoin, int quantity = 1)
    {
      int existingQuantity = 0;
      this.AllCoins.TryGetValue(typeOfCoin, out existingQuantity);
      existingQuantity += quantity;
      if (existingQuantity < 0)
      {
        quantity -= existingQuantity;
        existingQuantity = 0;
      }
      this.AllCoins[typeOfCoin] = existingQuantity;
      this.TotalQuantity += quantity;
      this.HasAnyCoins = (this.TotalQuantity > 0);
    }

    public bool HasAnyCoins { get; private set; }

    #endregion

    #region Helpers

    /// <summary>
    /// Total quantity of coins
    /// </summary>
    private int TotalQuantity { get; set; }
    
    #endregion
  }
}
