using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{   
  public class ProductSelector : IProductSelector
  {

    public bool CanProductBeSelectedForPurchase(ProductsForSale toSelect, decimal amountProvided)
    {
      return (bool)(this.GetProductPrice(toSelect) <= amountProvided);
    }

    public decimal GetProductPrice(ProductsForSale product)
    {
      return (decimal)(((decimal)product) / (decimal)100);
    }

  }
}
