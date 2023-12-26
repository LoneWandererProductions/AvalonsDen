/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTests/AvalonsDenLooting.cs
 * PURPOSE:     Tests for basic clicks and Item Exchange
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using ItemExchange;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AvalonsDenTests
{
    /// <summary>
    ///     The looting unit test class.
    /// </summary>
    [TestClass]
    public class AvalonsDenLooting
    {
        /// <summary>
        ///     The basic swaps.
        ///     0,1     10,11
        ///     2,3     12,13
        ///     4,5     14,15
        ///     6,7     16,17
        ///     8,9     18,19
        /// </summary>
        [TestMethod]
        public void BasicSwaps()
        {
            Initiate();

            //1-6
            //simple Swap
            StackExchange.Move(0, 2);

            Assert.AreEqual(1, StackExchange.Items[2].Id, "Swap was done");
            Assert.AreEqual(5, StackExchange.Items[2].Amount, "Swap was done");

            Assert.AreEqual(3, StackExchange.Items[0].Id, "Swap was done");
            Assert.AreEqual(1, StackExchange.Items[0].Amount, "Swap was done");

            //simple move
            StackExchange.Move(10, 3);

            Assert.AreEqual(1, StackExchange.Items[3].Id, "Move was done");
            Assert.AreEqual(5, StackExchange.Items[3].Amount, "Move was done");

            Assert.AreEqual(-1, StackExchange.Items[10].Id, "Move was done");
            Assert.AreEqual(0, StackExchange.Items[10].Amount, "Move was done");

            //real live test that causes problems

            Initiate();

            Assert.AreEqual(2, StackExchange.Items[1].Id, "Move was done");
            Assert.AreEqual(2, StackExchange.Items[1].Amount, "Move was done");

            StackExchange.Move(1, 11);

            Assert.AreEqual(-1, StackExchange.Items[1].Id, "Move was done");
            Assert.AreEqual(0, StackExchange.Items[1].Amount, "Move was done");

            Assert.AreEqual(2, StackExchange.Items[11].Id, "Move was done");
            Assert.AreEqual(2, StackExchange.Items[11].Amount, "Move was done");

            Initiate();

            StackExchange.Move(1, 6);

            Assert.AreEqual(-1, StackExchange.Items[1].Id, "Move was done");
            Assert.AreEqual(0, StackExchange.Items[1].Amount, "Move was done");

            Assert.AreEqual(2, StackExchange.Items[6].Id, "Move was done");
            Assert.AreEqual(2, StackExchange.Items[6].Amount, "Move was done");

            Initiate();

            //Fill
            StackExchange.Move(14, 15);

            Assert.AreEqual(-1, StackExchange.Items[14].Id, "Fill was done");
            Assert.AreEqual(0, StackExchange.Items[14].Amount, "Fill was done");

            Assert.AreEqual(4, StackExchange.Items[15].Id, "Fill was done");
            Assert.AreEqual(3, StackExchange.Items[15].Amount, "Fill was done");

            //Spill
            StackExchange.Move(12, 13);

            Assert.AreEqual(4, StackExchange.Items[12].Id, "Spill was done");
            Assert.AreEqual(2, StackExchange.Items[12].Amount, "Spill was done");

            Assert.AreEqual(4, StackExchange.Items[13].Id, "Spill was done");
            Assert.AreEqual(3, StackExchange.Items[13].Amount, "Spill was done");
        }

        /// <summary>
        ///     Some simple Basic tests if our Hitboxes are working
        /// </summary>
        [TestMethod]
        public void HitBox()
        {
            StackExchange.GenerateCubes(5, 4, 100, 0, 0);

            var test = StackExchange.GetCell(5, 105);

            Assert.AreEqual("Tile01", test, "Wrong Cell: " + test);

            test = StackExchange.GetCell(300, 90);

            Assert.AreEqual("Tile20", test, "Wrong Cell: " + test);

            test = StackExchange.GetCell(399, 399);

            Assert.AreEqual("Tile33", test, "Wrong Cell: " + test);

            test = StackExchange.GetCell(399, 499);

            Assert.AreEqual("Tile34", test, "Wrong Cell: " + test);
        }

        /// <summary>
        ///     Initiates this instance.
        /// </summary>
        private void Initiate()
        {
            //add some Test Data
            var loot = StackExchange.InitiateStack(5, 4);
            //add some Test Data
            //first
            loot[0].Id = 1;
            loot[0].MaxStack = 8;
            loot[0].Amount = 5;

            //second
            loot[10].Id = 1;
            loot[10].MaxStack = 8;
            loot[10].Amount = 5;

            //second
            loot[1].Id = 2;
            loot[1].MaxStack = 3;
            loot[1].Amount = 2;

            //third
            loot[2].Id = 3;
            loot[2].MaxStack = 3;
            loot[2].Amount = 1;

            //Fourth
            loot[12].Id = 4;
            loot[12].MaxStack = 3;
            loot[12].Amount = 2;

            //Fiveth
            loot[13].Id = 4;
            loot[13].MaxStack = 3;
            loot[13].Amount = 3;

            //Sixth
            loot[14].Id = 4;
            loot[14].MaxStack = 3;
            loot[14].Amount = 2;

            //Seventh
            loot[15].Id = 4;
            loot[15].MaxStack = 3;
            loot[15].Amount = 1;

            StackExchange.Initiate(loot);
        }
    }
}