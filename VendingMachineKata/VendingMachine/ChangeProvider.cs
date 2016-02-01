using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
  public class ChangeProvider : IChangeProvider
  {

    public IList<InsertedCoin> MakeChange(decimal amountOfChange, IDictionary<InsertedCoin, int> makeChangeFrom)
    {
      return null;
    }

  }
}
