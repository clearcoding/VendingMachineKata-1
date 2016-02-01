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
    private string _message = string.Empty;

    public string Message
    {
      get
      {
        if (this.OnNextRead != null)
        {
          this.OnNextRead(this, EventArgs.Empty);
        }
        return this._message;
      }
      set
      {
        this._message = value;
      }
    }

    public event EventHandler OnNextRead;
  }
}
