using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
  /// <summary>
  /// Interface for vending machine display
  /// </summary>
  public class Display : IDisplay
  {
    public string Message { get; set; }

    public void SetMessageAfterNextRead(string message)
    {
    }
  }
}
