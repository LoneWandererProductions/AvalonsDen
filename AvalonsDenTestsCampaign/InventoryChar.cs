/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDenTestsCampaign/InventoryChar.cs
 * PURPOSE:     Test Campaign Interactions
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

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
            var item = new ItemA(null, 3, 3, 1, 1, 20);
            concept.Inventory.Add(21, item);

            //artifact
            item = new ItemA(new List<int> { 0, 1, 3, 4, 5 }, 1, 3, 1, 1, 5);
            concept.Inventory.Add(22, item);

            //helmet
            var helmet = new ItemA(null, 1, 1, 1, 3, 6);

            concept.Inventory.Add(22, helmet);

            //TODO
        }
    }
}