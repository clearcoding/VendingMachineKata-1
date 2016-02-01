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
      if ((makeChangeFrom == null) || (makeChangeFrom.Count == 0))
      {
        return null;
      }

      List<InsertedCoin> change = new List<InsertedCoin>();

      if (change.Count == 0)
      {
        return null;
      }
      return change;
    }

  }
}
