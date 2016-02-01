using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
  /// <summary>
  /// Interface for vending machine
  /// </summary>
  public class VendingMachine : IVendingMachine
  {

    #region Construction & Members

    /// <summary>
    /// Hide default constructor
    /// </summary>
    public VendingMachine() :
      this(
        new CoinAcceptor(),
        new CoinAppraiser(),
        new Dictionary<InsertedCoin, int>(),
        new Display())
    {
    }

    /// <summary>
    /// Initialization constructor
    /// </summary>
    /// <param name="coinAcceptor">Coin acceptor interface to use</param>
    /// <param name="coinAppraiser">Coin appraiser interface to use</param>
    /// <param name="coinReturn">Coin return, with coins and their quanities</param>
    /// todo: IoC needed here down the road
    public VendingMachine(
      ICoinAcceptor coinAcceptor,
      ICoinAppraiser coinAppraiser,
      IDictionary<InsertedCoin, int> coinReturn,
      IDisplay display)
    {
      this.CoinAcceptor = coinAcceptor;
      this.CoinAppraiser = coinAppraiser;
      this.CoinReturn = coinReturn;
      this.Display = display;
      this.Display.Message = VendingMachine.InsertCoinsMessage;
    }

    private ICoinAcceptor CoinAcceptor { get; set; }
    private ICoinAppraiser CoinAppraiser { get; set; }

    #endregion

    #region IVendingMachine Members

    public IDisplay Display { get; private set;  }

    public void InsertCoin(InsertableCoinWeights coinWeight, InsertableCoinSizes coinSize)
    {
      InsertedCoin newCoin = this.CoinAcceptor.InsertCoin(coinWeight, coinSize);

      // Rejected coins should go to return.  Otherwise, add them to current amount inserted.
      if (newCoin == InsertedCoin.Rejected)
      {
        if (this.CoinReturn.ContainsKey(newCoin))
        {
          this.CoinReturn[newCoin]++;
        }
        else
        {
          this.CoinReturn.Add(newCoin, 1);
        }
      }
      else
      {
        this.CurrentAmountInserted += this.CoinAppraiser.GetCoinValue(newCoin);
      }

      // Update display if nothing is inserted
      if (this.CurrentAmountInserted == 0)
      {
        this.Display.Message = VendingMachine.InsertCoinsMessage;
      }
    }

    public decimal CurrentAmountInserted { get; private set; }
    public IDictionary<InsertedCoin, int> CoinReturn { get; private set; }

    #endregion

    #region Display Strings

    public const string InsertCoinsMessage = "INSERT COINS";

    #endregion

  }
}
