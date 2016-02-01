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
  public interface IDisplay
  {
    /// <summary>
    /// Reads message from display
    /// </summary>
    string Message { get; set; }

    /// <summary>
    /// Event for signalling upon the next read of the display
    /// </summary>
    event EventHandler OnNextRead;
  }
}
