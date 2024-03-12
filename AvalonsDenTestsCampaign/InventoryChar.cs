/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDenTestsCampaign/InventoryChar.cs
 * PURPOSE:     Test Campaign Interactions
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using InventoryHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AvalonsDenTestsCampaign
{
    /// <summary>
    ///     The campaign interaction, mostly checks Exceptions
    /// </summary>
    [TestClass]
    public sealed class InventoryChar
    {

        [TestMethod]
        public void Inventory()
        {
            //all Artefacts beyond 5
            var limit = new List<int>
            {
                0,
                1,
                2,
                3,
                4
            };

            var concept = new Concept
            {
                Name = "test",
                CharacterId = 0,
                Limitations = limit
            };

            //add some stuff to Inventory
            var item = new ItemA
            {
                Weight = 1,
                ItemId = 1,
                Slot = -1
            };
            concept.Inventory.Add(21, item);

            item = new ItemA
            {
                Weight = 1,
                ItemId = 1,
                Slots = new List<int> { 0, 1, 3, 4, 5 }
            };
            concept.Inventory.Add(22, item);

        }
    }
}