using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
  public class ProductSelector : IProductSelector
  {
    public ProductSelector()
    {
      this.SelectedProduct = ProductForSale.Unknown;
    }

    private ProductForSale _selectedProduct = ProductForSale.Unknown;

    public ProductForSale SelectedProduct
    {
      get
      {
        return this._selectedProduct;
      }
      set
      {
        if (this._selectedProduct != value)
        {
          this._selectedProduct = value;
          if (this.OnSelectedProductChanged != null)
          {
            this.OnSelectedProductChanged(this, this._selectedProduct);
          }
        }
      }
    }

    public event EventHandler<ProductForSale> OnSelectedProductChanged;

    public bool CanSelectedProductBePurchased(decimal amountProvided)
    {
      return (bool)(
        (this.SelectedProduct != ProductForSale.Unknown) &&
        (this.GetProductPrice(this.SelectedProduct) <= amountProvided)
      );
    }

    public decimal GetProductPrice(ProductForSale product)
    {
      return (decimal)(((decimal)product) / (decimal)100);
    }

  }
}
