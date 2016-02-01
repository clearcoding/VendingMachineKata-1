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
        new ProductSelector(),
        new ChangeProvider(),
        new Dictionary<InsertedCoin, int>())
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
      IProductSelector productSelector,
      IChangeProvider changeProvider,
      IDictionary<InsertedCoin, int> internalSafe)
    {
      this.CoinAcceptor = coinAcceptor;
      this.CoinAppraiser = coinAppraiser;
      this.CoinReturn = coinReturn;
      this.Display = display;
      this.Display.Message = VendingMachine.InsertCoinsMessage;
      this.ProductSelectorButtons = productSelector;
      this.ProductSelectorButtons.OnSelectedProductChanged += this.OnProductSelected;
      this.ChangeProvider = changeProvider;
      this.InternalSafe = internalSafe;
    }

    private ICoinAcceptor CoinAcceptor { get; set; }
    private ICoinAppraiser CoinAppraiser { get; set; }
    private IChangeProvider ChangeProvider { get; set; }

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
        this.AddToCoinReturn(newCoin);
      }
      else
      {
        this.CurrentAmountInserted += this.CoinAppraiser.GetCoinValue(newCoin);
        if (this.InternalSafe.ContainsKey(newCoin))
        {
          this.InternalSafe[newCoin]++;
        }
        else
        {
          this.InternalSafe.Add(newCoin, 1);
        }
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
    public IDictionary<InsertedCoin, int> InternalSafe { get; private set;  }

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
        // Dispense, and thank user
        this.Display.Message = VendingMachine.ThankYouMessage;

        // Subscribe so that on next read, we can reset our current amount inserted and display the
        // INSERT COINS message
        this.Display.OnNextRead += this.OnDisplayReadAfterProductDispensed;

        // Any remaining change should go to coin return
        this.DispenseChange();
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
      if (this.CurrentAmountInserted == 0)
      {
        this.Display.Message = VendingMachine.InsertCoinsMessage;
      }
      else
      {
        this.Display.Message = string.Format(VendingMachine.CurrentAmountMessageFormat, this.CurrentAmountInserted);
      }
    }

    /// <summary>
    /// Dispenses change
    /// </summary>
    private void DispenseChange()
    {
      // Difference is what is owed
      decimal owed = this.CurrentAmountInserted;
      owed -= this.ProductSelectorButtons.GetProductPrice(this.ProductSelectorButtons.SelectedProduct);
     
      // Make our change
      IDictionary<InsertedCoin, int> change = this.ChangeProvider.MakeChange(owed, this.InternalSafe, this.CoinAppraiser);
      if (change != null)
      {
        //todo: enumerate or something else.  For now we know we are only limited to valid coins
        this.AddToCoinReturn(InsertedCoin.Quarter, change[InsertedCoin.Quarter]);
        this.AddToCoinReturn(InsertedCoin.Dime, change[InsertedCoin.Dime]);
        this.AddToCoinReturn(InsertedCoin.Nickel, change[InsertedCoin.Nickel]);
      }
    }

    /// <summary>
    /// Adds coin(s) to coin return
    /// </summary>
    /// <param name="coin">The type of coin to place in the coin return</param>
    /// <param name="quantity">The number of coins</param>
    private void AddToCoinReturn(InsertedCoin coin, int quantity = 1)
    {
      if (this.CoinReturn.ContainsKey(coin))
      {
        this.CoinReturn[coin] += quantity;
      }
      else
      {
        this.CoinReturn.Add(coin, quantity);
      }
    }

    #endregion

  }
}
