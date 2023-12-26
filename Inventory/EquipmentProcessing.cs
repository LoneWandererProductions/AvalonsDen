using System.Collections.Generic;
using DatabaseDriver;
using ExtendedSystemObjects;
using Resources;

namespace Inventory
{
    internal static class EquipmentProcessing
    {
        /// <summary>
        ///     Gets the equipment.
        ///     Initiate Registry
        /// </summary>
        /// <param name="inventory">The inventory.</param>
        internal static void GetEquipment(Dictionary<int, Slot> inventory)
        {
            if (inventory == null) return;

            var party = new List<Slot>();
            var backpack = new List<Slot>();

            SetItemMaster(inventory);

            //split into inventory and equipped
            foreach (var items in inventory.Values)
                if (items.Position == 0)
                    party.Add(items);
                else
                    backpack.Add(items);

            InventoryRegister.Party = new Dictionary<int, CharacterOverview>(party.Count);

            SetInventoryParty(party, backpack);
        }

        /// <summary>
        ///     Sets the item master.
        /// </summary>
        /// <param name="inventory">The inventory.</param>
        private static void SetItemMaster(Dictionary<int, Slot> inventory)
        {
            //get all Items into Inventory
            var output = HandlerOutputSingleton.GetInstance();
            var lst = new List<int>();

            foreach (var slot in inventory.Values) lst.AddDistinct(slot.Id);

            InventoryRegister.ItemMaster = output.GetItems(lst);
        }


        /// <summary>
        ///     Sets the inventory and party inventory.
        /// </summary>
        /// <param name="party">The party inventory.</param>
        /// <param name="backpack">The backpack inventory.</param>
        private static void SetInventoryParty(IEnumerable<Slot> party, IEnumerable<Slot> backpack)
        {
            foreach (var value in party)
                if (InventoryRegister.Party.ContainsKey(value.CharacterId))
                {
                    InventoryRegister.Party[value.CharacterId] =
                        ConvertSlot(value, InventoryRegister.Party[value.CharacterId]);
                }
                else
                {
                    var item = ConvertSlot(value, null);
                    InventoryRegister.Party.Add(value.CharacterId, item);
                }

            foreach (var value in backpack)
                if (InventoryRegister.Party.ContainsKey(value.CharacterId))
                {
                    InventoryRegister.Party[value.CharacterId].Inventory.Add(value.Position, value);
                }
                else
                {
                    var chr = new CharacterOverview { CharacterId = value.CharacterId };
                    InventoryRegister.Party.Add(value.CharacterId, chr);
                }
        }

        /// <summary>
        ///     Converts the slot.
        /// </summary>
        /// <param name="slot">The value.</param>
        /// <param name="eqp">The equipment.</param>
        /// <returns></returns>
        private static CharacterOverview ConvertSlot(Slot slot, CharacterOverview eqp)
        {
            eqp ??= new CharacterOverview();

            //TODO add method to get Slot Type
            //var rest = InventoryRegister.ItemMaster[slot.Id].;

            eqp.CharacterId = slot.CharacterId;


            //TODO Implement Data Handle to get Item
            switch (0)
            {
                case (int)InventoryEnum.EnumSlot.Head:
                    if (eqp.IsAllowedHead) eqp.Head = slot.Id;
                    break;
                case (int)InventoryEnum.EnumSlot.Amulet:
                    if (eqp.IsAllowedAmulet) eqp.Amulet = slot.Id;
                    break;
                case (int)InventoryEnum.EnumSlot.Chest:
                    if (eqp.IsAllowedChest) eqp.Chest = slot.Id;
                    break;
                case (int)InventoryEnum.EnumSlot.Gloves:
                    if (eqp.IsAllowedGloves) eqp.Gloves = slot.Id;
                    break;
                case (int)InventoryEnum.EnumSlot.RingLeft:
                    if (eqp.IsAllowedRingLeft)
                        eqp.RingLeft = slot.Id;
                    break;
                case (int)InventoryEnum.EnumSlot.RingRight:
                    if (eqp.IsAllowedRingRight)
                        eqp.RingRight = slot.Id;
                    break;
                case (int)InventoryEnum.EnumSlot.MainHand:
                    if (eqp.IsAllowedMainHand)
                        eqp.MainHand = slot.Id;
                    break;
                case (int)InventoryEnum.EnumSlot.OffHand:
                    if (eqp.IsAllowedOffHand)
                        eqp.OffHand = slot.Id;
                    break;
                case (int)InventoryEnum.EnumSlot.Belt:
                    if (eqp.IsAllowedBelt)
                        eqp.Belt = slot.Id;
                    break;
                case (int)InventoryEnum.EnumSlot.Trousers:
                    if (eqp.IsAllowedTrousers)
                        eqp.Trousers = slot.Id;
                    break;
                case (int)InventoryEnum.EnumSlot.Shoes:
                    if (eqp.IsAllowedShoes)
                        eqp.Shoes = slot.Id;
                    break;
                case (int)InventoryEnum.EnumSlot.EquipmentSlotOne:
                    if (eqp.IsAllowedEquipmentSlotOne)
                        eqp.EquipmentSlotOne = slot.Id;
                    break;
                case (int)InventoryEnum.EnumSlot.EquipmentSlotTwo:
                    if (eqp.IsAllowedEquipmentSlotTwo)
                        eqp.EquipmentSlotTwo = slot.Id;
                    break;
                default:
                    return null;
            }

            return eqp;
        }
    }

    /// <summary>
    ///     The base Character Inventory
    /// </summary>
    internal sealed class CharacterContainer
    {
        public int CharacterId { get; set; }

        /// <summary>
        ///     Gets or sets the inventory.
        /// </summary>
        /// <value>
        ///     The inventory.
        /// </value>
        public Dictionary<int, Slot> Inventory { get; set; }

        /// <summary>
        ///     Gets or sets the equipment.
        /// </summary>
        /// <value>
        ///     The equipment.
        /// </value>
        public Dictionary<int, Equipped> Equipment { get; set; }
    }
}