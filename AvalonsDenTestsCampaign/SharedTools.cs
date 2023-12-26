/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/AvalonsDenTestsCampaign/SharedTools.cs
 * PURPOSE:     Shared Functions
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.IO;
using Resources;

// ReSharper disable CyclomaticComplexity, yeah it is big but we use it to generate the Basic Db

namespace AvalonsDenTestsCampaign
{
    /// <summary>
    ///     The shared tools class.
    /// </summary>
    internal static class SharedTools
    {
        /// <summary>
        ///     Checks if Item is equal
        /// </summary>
        /// <param name="item">Weapon</param>
        /// <param name="weapon">Weapon</param>
        /// <returns>True if equal</returns>
        internal static bool Compare(Weapon item, Weapon weapon)
        {
            if (item.BaseName != weapon.BaseName) return false;

            if (item.CustomDescription != weapon.CustomDescription) return false;

            if (item.CustomName != weapon.CustomName) return false;

            if (item.Description != weapon.Description) return false;

            if (item.MaxStack != weapon.MaxStack) return false;

            if (item.ImageId != weapon.ImageId) return false;

            if (item.IdOfAttributes != weapon.IdOfAttributes) return false;

            if (item.Worth != weapon.Worth) return false;

            if (item.Description != weapon.Description) return false;

            if (item.CustomDescription != weapon.CustomDescription) return false;

            if (item.CustomName != weapon.CustomName) return false;

            if (item.BaseName != weapon.BaseName) return false;

            if (item.Rarity != weapon.Rarity) return false;

            if (item.Weight != weapon.Weight) return false;

            //stop

            if (item.Durability != weapon.Durability) return false;

            if (item.Armor != weapon.Armor) return false;

            if (item.Damage != weapon.Damage) return false;

            if (item.Range != weapon.Range) return false;

            if (item.DamageRange != weapon.DamageRange) return false;

            return item.DamageType == weapon.DamageType;
        }

        /// <summary>
        ///     Checks if Item is equal
        /// </summary>
        /// <param name="item">Miscellaneous</param>
        /// <param name="misc">Miscellaneous</param>
        /// <returns>True if equal</returns>
        internal static bool Compare(Miscellaneous item, Miscellaneous misc)
        {
            if (item.BaseName != misc.BaseName) return false;

            if (item.CustomDescription != misc.CustomDescription) return false;

            if (item.CustomName != misc.CustomName) return false;

            if (item.Description != misc.Description) return false;

            if (item.MaxStack != misc.MaxStack) return false;

            if (item.ImageId != misc.ImageId) return false;

            if (item.IdOfAttributes != misc.IdOfAttributes) return false;

            if (item.Worth != misc.Worth) return false;

            if (item.Description != misc.Description) return false;

            if (item.CustomDescription != misc.CustomDescription) return false;

            if (item.CustomName != misc.CustomName) return false;

            if (item.BaseName != misc.BaseName) return false;

            if (item.Rarity != misc.Rarity) return false;

            if (item.Weight != misc.Weight) return false;

            //stop

            return item.Type == misc.Type;
        }

        /// <summary>
        ///     Checks if Item is equal
        /// </summary>
        /// <param name="item">Armor</param>
        /// <param name="armor">Armor</param>
        /// <returns>True if equal</returns>
        internal static bool Compare(Armor item, Armor armor)
        {
            if (item.BaseName != armor.BaseName) return false;

            if (item.CustomDescription != armor.CustomDescription) return false;

            if (item.CustomName != armor.CustomName) return false;

            if (item.Description != armor.Description) return false;

            if (item.MaxStack != armor.MaxStack) return false;

            if (item.ImageId != armor.ImageId) return false;

            if (item.IdOfAttributes != armor.IdOfAttributes) return false;

            if (item.Worth != armor.Worth) return false;

            if (item.Description != armor.Description) return false;

            if (item.CustomDescription != armor.CustomDescription) return false;

            if (item.CustomName != armor.CustomName) return false;

            if (item.BaseName != armor.BaseName) return false;

            if (item.Rarity != armor.Rarity) return false;

            if (item.Weight != armor.Weight) return false;

            //stop

            if (item.ArmorValue != armor.ArmorValue) return false;

            if (item.Durability != armor.Durability) return false;

            if (item.ArmorClass != armor.ArmorClass) return false;

            return item.Durability == armor.Durability;
        }

        /// <summary>
        ///     Creates some Dummy Files we will delete
        /// </summary>
        /// <param name="path">target Path</param>
        /// <param name="fileExtList">Extension List</param>
        internal static void CreateFiles(string path, IEnumerable<string> fileExtList)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            var fileName = 0;

            foreach (var ext in fileExtList)
            {
                fileName++;

                var file = path + Path.DirectorySeparatorChar + fileName + ext;

                if (File.Exists(file)) continue;

                using var fs = File.Create(file);
                for (byte i = 0; i < 10; i++) fs.WriteByte(i);
            }
        }
    }
}