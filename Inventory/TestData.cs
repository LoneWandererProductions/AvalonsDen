using System.Collections.Generic;

namespace Inventory
{
    public static class TestData
    {
        public static Dictionary<int, string> Character { get; set; }

        public static Dictionary<int, Slot> Inventory { get; set; }

        internal static void Initiate()
        {
            //Id is the Slot of the inventory
            Inventory = new Dictionary<int, Slot>();

            var slot = new Slot
            {
                Amount = 1,
                Id = 0,
                CharacterId = 1,
                //RingLeft
                Position = 4
            };

            Inventory.Add(0, slot);

            slot = new Slot
            {
                Amount = 2,
                Id = 0,
                CharacterId = 1,
                //RingLeft
                Position = 0
            };

            Inventory.Add(1, slot);

            // add equipped Items

            slot = new Slot
            {
                Amount = 1,
                Id = 0,
                CharacterId = 0,
                //Chest
                Position = 2
            };

            Inventory.Add(2, slot);

            slot = new Slot
            {
                Amount = 1,
                Id = 0,
                CharacterId = 1,
                //Chest
                Position = 2
            };

            Inventory.Add(3, slot);

            slot = new Slot
            {
                Amount = 1,
                Id = 0,
                CharacterId = 1,
                //Trouser
                Position = 10
            };

            Inventory.Add(4, slot);

            slot = new Slot
            {
                Amount = 1,
                Id = 0,
                CharacterId = 0,
                //Trouser
                Position = 10
            };

            Inventory.Add(5, slot);

            Character = new Dictionary<int, string> { { 0, "Ed" }, { 1, "Mike" } };
        }
    }
}