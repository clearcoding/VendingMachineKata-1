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

    public ProductForSale SelectedProduct { get; set; }

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
