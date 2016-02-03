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
      return 0;
    }

    public void Add(InsertedCoin typeOfCoin, int quantity)
    {
      if (this.AllCoins.ContainsKey(typeOfCoin))
      {
        this.AllCoins[typeOfCoin] += quantity;
      }
      else
      {
        this.AllCoins[typeOfCoin] = quantity;
      }
    }

    #endregion
  }
}
