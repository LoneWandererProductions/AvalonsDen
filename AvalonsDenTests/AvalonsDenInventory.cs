/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTests/InitiateInventory.cs
 * PURPOSE:     Tests for Inventory Screen
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AvalonsDenTests
{
    /// <summary>
    ///     The looting unit test class.
    /// </summary>
    [TestClass]
    public class AvalonsDenInventory
    {
        /// <summary>
        ///     Initiates the inventory.
        /// </summary>
        [TestMethod]
        public void InitiateInventory()
        {
            var inventory = new Dictionary<int, Slot>();
            var slot = new Slot
            {
                Amount = 1,
                Id = 0,
                CharacterId = 1,
                Position = 4
            };
            inventory.Add(slot.Id, slot);

            slot = new Slot
            {
                Amount = 2,
                Id = 0,
                CharacterId = 0,
                Position = 0
            };
            inventory.Add(slot.Id, slot);

            var character = new Dictionary<int, string> {{0, "Ed"}, {1, "Mike"}};

            //Got Data in Place
            var view = new InventoryView();
            view.Initiate(inventory, character);

            //Check if names were generated correctly
            Assert.AreEqual("Ed", InventoryRegister.Names[0], "done");
            Assert.AreEqual("Mike", InventoryRegister.Names[1], "done");

            //character Id is only 1
            var data = InventoryRegister.Party[1];
        }
    }
}