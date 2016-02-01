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
    private VendingMachine() :
      this(
        new CoinAcceptor(),
        new CoinAppraiser())
    {
    }

    /// <summary>
    /// Initialization constructor
    /// </summary>
    /// <param name="coinAcceptor">Coin acceptor interface to use</param>
    /// <param name="coinAppraiser">Coin appraiser interface to use</param>
    public VendingMachine(
      ICoinAcceptor coinAcceptor,
      ICoinAppraiser coinAppraiser)
    {
      this.CoinAcceptor = coinAcceptor;
      this.CoinAppraiser = coinAppraiser;
    }

    private ICoinAcceptor CoinAcceptor { get; set; }
    private ICoinAppraiser CoinAppraiser { get; set; }

    #endregion

    #region Accepts Coins

    public void InsertCoin(InsertableCoinWeights coinWeight, InsertableCoinSizes coinSize)
    {
      this.CurrentAmountInserted += this.CoinAppraiser.GetCoinValue(this.CoinAcceptor.InsertCoin(coinWeight, coinSize));
    }

    public decimal CurrentAmountInserted { get; private set; }

    #endregion

  }
}
