/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDenTestsCampaign/InventoryChar.cs
 * PURPOSE:     Test Campaign Interactions
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Collections.Generic;
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
            //invetory slot 1->20 +1
            var item = new ItemA(null, 3, 3, 1, 1, 20);
            concept.Inventory.Add(21, item);

            //artifact
            //invetory slot 2->20 +2
            item = new ItemA(new List<int> { 0, 1, 3, 4, 5 }, 1, 3, 1, 1);
            concept.Inventory.Add(22, item);

            //helmet
            //invetory slot 3->20 +3
            var helmet = new ItemA(null, 1, 1, 1, 3, 6);

            concept.Inventory.Add(23, helmet);

            //TODO
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckExeptionsStack()
        {
            //add some stuff to Inventory
            var item = new ItemA(null, 3, 1, 1, 1, 20);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckExeptionsNull()
        {
            //add some stuff to Inventory
            var item = new ItemA(null, 0, 1, 1, 1, 20);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckExeptionsSlot()
        {
            //add some stuff to Inventory
            var item = new ItemA(null, 3, 1, 1, 1);
        }
    }
}