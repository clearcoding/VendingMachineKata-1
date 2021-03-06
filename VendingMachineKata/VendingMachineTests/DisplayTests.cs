﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine;

namespace VendingMachineTests
{
  /// <summary>
  /// Class for testing the vending machines display
  /// </summary>
  [TestClass]
  public class DisplayTests
  {
    private IDisplay _display = null;           // Display UUT

    [TestInitialize]
    public void arrangeForTests()
    {
      //Arrange
      this._display = new Display();
    }

    [TestMethod]
    public void shouldAllowMessageToBeSetOnDisplay()
    {
      //Act & Assert
      const string message = "Testing set message";
      this._display.Message = message;
      Assert.AreEqual(message, this._display.Message);
    }

    [TestMethod]
    public void shouldAllowEventToBeFiredOnNextRead()
    {
      //Act & Assert
      bool wasRaised = false;
      this._display.OnNextRead += delegate (object sender, EventArgs e)
      {
        wasRaised = true;
      };

      Assert.IsFalse(wasRaised);
      string dummy = this._display.Message;
      Assert.IsTrue(wasRaised);
    }

  }
}
