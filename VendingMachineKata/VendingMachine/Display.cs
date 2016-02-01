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
    private string _messageAfter = null;
    private string _message = string.Empty;

    public string Message
    {
      get
      {
        string retVal = this._message;
        if (this._messageAfter != null)
        {
          this._message = this._messageAfter;
          this._messageAfter = null;
        }
        return retVal;
      }
      set
      {
        this._message = value;
      }
    }

    public void SetMessageAfterNextRead(string message)
    {
      this._messageAfter = message;
    }
  }
}
