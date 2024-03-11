/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDenTestsCampaign/InventoryChar.cs
 * PURPOSE:     Test Campaign Interactions
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using InventoryHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AvalonsDenTestsCampaign
{
    /// <summary>
    ///     The campaign interaction, mostly checks Exceptions
    /// </summary>
    [TestClass]
    public sealed class InventoryChar
    {
        /// <summary>
        ///     Check our Exceptions
        public void Inventory()
        {
            var concept = new Concept();
        }
    }
}