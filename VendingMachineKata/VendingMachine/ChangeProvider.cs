using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
  public class ChangeProvider : IChangeProvider
  {

    public IList<InsertedCoin> MakeChange(decimal amountOfChange, IDictionary<InsertedCoin, int> makeChangeFrom, ICoinAppraiser coinAppraiser)
    {
      // Ensure we have a stash to make change from and that we have an appraisor to determine value of coinage
      if (
            (makeChangeFrom == null) || 
            (makeChangeFrom.Count == 0) || 
            (coinAppraiser == null)
         )
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
