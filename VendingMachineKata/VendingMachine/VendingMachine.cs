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
        new Display(),
        new ProductSelector())
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
      IDisplay display,
      IProductSelector productSelector)
    {
      this.CoinAcceptor = coinAcceptor;
      this.CoinAppraiser = coinAppraiser;
      this.CoinReturn = coinReturn;
      this.Display = display;
      this.Display.Message = VendingMachine.InsertCoinsMessage;
      this.ProductSelectorButtons = productSelector;
      this.ProductSelectorButtons.OnSelectedProductChanged += this.OnProductSelected;
    }

    private ICoinAcceptor CoinAcceptor { get; set; }
    private ICoinAppraiser CoinAppraiser { get; set; }

    #endregion

    #region IVendingMachine Members

    public IDisplay Display { get; private set;  }
    public IProductSelector ProductSelectorButtons { get; private set;  }

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

      // Update display
      if (this.CurrentAmountInserted == 0)
      {
        this.Display.Message = VendingMachine.InsertCoinsMessage;
      }
      else
      {
        this.Display.Message = string.Format(VendingMachine.CurrentAmountMessageFormat, this.CurrentAmountInserted);
      }
    }

    public decimal CurrentAmountInserted { get; private set; }
    public IDictionary<InsertedCoin, int> CoinReturn { get; private set; }

    #endregion

    #region Display Strings, Event Handlers, and other Helpers

    public const string InsertCoinsMessage = "INSERT COINS";
    private const string CurrentAmountMessageFormat = "${0:#0.00}";
    public const string ThankYouMessage = "THANK YOU";
    private const string PriceMessageFormat = "PRICE ${0:#0.00}";

    /// <summary>
    /// Occurs when product selection is changed
    /// </summary>
    private void OnProductSelected(object sender, ProductForSale selected)
    {
      // If enough money has been inserted for the selected product, dispense it and thank user
      if (this.ProductSelectorButtons.CanSelectedProductBePurchased(this.CurrentAmountInserted))
      {
        this.Display.Message = VendingMachine.ThankYouMessage;

        // Subscribe so that on next read, we can reset our current amount inserted and display the
        // INSERT COINS message
        this.Display.OnNextRead += this.OnDisplayReadAfterProductDispensed;
      }
      else
      {
        this.Display.Message = string.Format(VendingMachine.PriceMessageFormat, this.ProductSelectorButtons.GetProductPrice(selected));
        
        // Subscribe so that on next read, we can display the PRICE and display the
        // INSERT COINS message or current amount inserted
        this.Display.OnNextRead += this.OnDisplayReadAfterProductNotDispensed;
      }
    }

    /// <summary>
    /// Occurs after product is dispensed, resets amount inserted, and displays new message
    /// </summary>
    private void OnDisplayReadAfterProductDispensed(object sender, EventArgs e)
    {
      this.Display.OnNextRead -= this.OnDisplayReadAfterProductDispensed;
      this.Display.Message = VendingMachine.InsertCoinsMessage;
      this.CurrentAmountInserted = 0;
    }

    /// <summary>
    /// Occurs after product is not dispensed, displays INSERT COINS or amount inserted
    /// </summary>
    private void OnDisplayReadAfterProductNotDispensed(object sender, EventArgs e)
    {
      this.Display.OnNextRead -= this.OnDisplayReadAfterProductDispensed;
      this.Display.Message = string.Format(VendingMachine.CurrentAmountMessageFormat, this.CurrentAmountInserted);
    }

    #endregion

  }
}
