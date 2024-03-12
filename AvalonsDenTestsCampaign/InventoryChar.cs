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
                Slot = 20, //inventory
                Stack =3,
                MaxStack =3
            };
            concept.Inventory.Add(21, item);

            //artefact
            item = new ItemA
            {
                Weight = 1,
                ItemId = 2,
                Slots = new List<int> { 0, 1, 3, 4, 5 },
                Stack = 1,
                MaxStack = 1
            };
            concept.Inventory.Add(22, item);

            //helmet
            var helmet = new ItemA
            {
                Weight = 1,
                ItemId = 3,
                Slot =6,
                Stack = 1,
                MaxStack = 1
            };
            concept.Inventory.Add(22, item);

            //TODO

        }
    }
}