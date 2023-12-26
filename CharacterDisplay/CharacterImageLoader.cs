/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Characters/CharacterImageLoader.cs
 * PURPOSE:     Loads all Images to Character Control
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.IO;
using System.Windows.Media.Imaging;
using Debugger;
using Imaging;

namespace CharacterDisplay
{
    /// <summary>
    ///     Loads the Images into the Control, can not be made Static because it binds with the WPF
    /// </summary>
    public sealed class CharacterImageLoader
    {
        /// <summary>
        ///     The render (readonly). Value: new ImageRender().
        /// </summary>
        private static readonly ImageRender Render = new();

        /// <summary>
        ///     Gets the wisdom Image.
        /// </summary>
        public static BitmapImage Wisdom
        {
            get
            {
                try
                {
                    return
                        Render.GetBitmapImage(Path.Combine(Directory.GetCurrentDirectory(),
                            CharacterResources.CoreIcons,
                            CharacterResources.SourceWisdomUri));
                }
                catch (NotSupportedException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (InvalidOperationException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (IOException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets the strength Image.
        /// </summary>
        public static BitmapImage Strength
        {
            get
            {
                try
                {
                    return
                        Render.GetBitmapImage(Path.Combine(Directory.GetCurrentDirectory(),
                            CharacterResources.CoreIcons,
                            CharacterResources.SourceStrengthUri));
                }
                catch (NotSupportedException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (InvalidOperationException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (IOException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets the agility Image.
        /// </summary>
        public static BitmapImage Agility
        {
            get
            {
                try
                {
                    return
                        Render.GetBitmapImage(Path.Combine(Directory.GetCurrentDirectory(),
                            CharacterResources.CoreIcons,
                            CharacterResources.SourceAgilityUri));
                }
                catch (NotSupportedException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (InvalidOperationException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (IOException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets the intelligence Image.
        /// </summary>
        public static BitmapImage Intelligence
        {
            get
            {
                try
                {
                    return
                        Render.GetBitmapImage(Path.Combine(Directory.GetCurrentDirectory(),
                            CharacterResources.CoreIcons,
                            CharacterResources.SourceIntelligenceUri));
                }
                catch (NotSupportedException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (InvalidOperationException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (IOException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets the charisma Image.
        /// </summary>
        public static BitmapImage Charisma
        {
            get
            {
                try
                {
                    return
                        Render.GetBitmapImage(Path.Combine(Directory.GetCurrentDirectory(),
                            CharacterResources.CoreIcons,
                            CharacterResources.SourceCharismaUri));
                }
                catch (NotSupportedException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (InvalidOperationException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (IOException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets the will Image.
        /// </summary>
        public static BitmapImage Will
        {
            get
            {
                try
                {
                    return
                        Render.GetBitmapImage(Path.Combine(Directory.GetCurrentDirectory(),
                            CharacterResources.CoreIcons,
                            CharacterResources.SourceWillUri));
                }
                catch (NotSupportedException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (InvalidOperationException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (IOException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets the endurance Image.
        /// </summary>
        public static BitmapImage Endurance
        {
            get
            {
                try
                {
                    return
                        Render.GetBitmapImage(Path.Combine(Directory.GetCurrentDirectory(),
                            CharacterResources.CoreIcons,
                            CharacterResources.SourceEnduranceUri));
                }
                catch (NotSupportedException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (InvalidOperationException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (IOException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets the carrying weight Image.
        /// </summary>
        public static BitmapImage CarryingWeight
        {
            get
            {
                try
                {
                    return
                        Render.GetBitmapImage(Path.Combine(Directory.GetCurrentDirectory(),
                            CharacterResources.CoreIcons,
                            CharacterResources.SourceCarryingWeightUri));
                }
                catch (NotSupportedException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (InvalidOperationException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (IOException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets the speech craft Image.
        /// </summary>
        public static BitmapImage SpeechCraft
        {
            get
            {
                try
                {
                    return
                        Render.GetBitmapImage(Path.Combine(Directory.GetCurrentDirectory(),
                            CharacterResources.CoreIcons,
                            CharacterResources.SourceSpeechCraftUri));
                }
                catch (NotSupportedException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (InvalidOperationException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (IOException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets the Combat Values Image.
        /// </summary>
        public static BitmapImage CombatValues
        {
            get
            {
                try
                {
                    return
                        Render.GetBitmapImage(Path.Combine(Directory.GetCurrentDirectory(),
                            CharacterResources.CoreIcons,
                            CharacterResources.SourceCombatvaluesUri));
                }
                catch (NotSupportedException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (InvalidOperationException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (IOException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets the spirit Image.
        /// </summary>
        public static BitmapImage Spirit
        {
            get
            {
                try
                {
                    return
                        Render.GetBitmapImage(Path.Combine(Directory.GetCurrentDirectory(),
                            CharacterResources.CoreIcons,
                            CharacterResources.SourceSpiritUri));
                }
                catch (NotSupportedException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (InvalidOperationException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (IOException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets the initiative Image.
        /// </summary>
        public static BitmapImage Initiative
        {
            get
            {
                try
                {
                    return
                        Render.GetBitmapImage(Path.Combine(Directory.GetCurrentDirectory(),
                            CharacterResources.CoreIcons,
                            CharacterResources.SourceInitiativeUri));
                }
                catch (NotSupportedException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (InvalidOperationException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (IOException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets the Action Points Image.
        /// </summary>
        public static BitmapImage ActionPoints
        {
            get
            {
                try
                {
                    return
                        Render.GetBitmapImage(Path.Combine(Directory.GetCurrentDirectory(),
                            CharacterResources.CoreIcons,
                            CharacterResources.SourceActionPointsUri));
                }
                catch (NotSupportedException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (InvalidOperationException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (IOException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets the critical chance Image.
        /// </summary>
        public static BitmapImage CriticalChance
        {
            get
            {
                try
                {
                    return
                        Render.GetBitmapImage(Path.Combine(Directory.GetCurrentDirectory(),
                            CharacterResources.CoreIcons,
                            CharacterResources.SourceCriticalchanceUri));
                }
                catch (NotSupportedException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (InvalidOperationException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (IOException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets the hit chance Image.
        /// </summary>
        public static BitmapImage HitChance
        {
            get
            {
                try
                {
                    return
                        Render.GetBitmapImage(Path.Combine(Directory.GetCurrentDirectory(),
                            CharacterResources.CoreIcons,
                            CharacterResources.SourceHitChanceUri));
                }
                catch (NotSupportedException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (InvalidOperationException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (IOException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets the resistance Image.
        /// </summary>
        public static BitmapImage Resistance
        {
            get
            {
                try
                {
                    return
                        Render.GetBitmapImage(Path.Combine(Directory.GetCurrentDirectory(),
                            CharacterResources.CoreIcons,
                            CharacterResources.SourceResistanceUri));
                }
                catch (NotSupportedException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (InvalidOperationException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (IOException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets the shielding Image.
        /// </summary>
        public static BitmapImage Shielding
        {
            get
            {
                try
                {
                    return
                        Render.GetBitmapImage(Path.Combine(Directory.GetCurrentDirectory(),
                            CharacterResources.CoreIcons,
                            CharacterResources.SourceShieldingUri));
                }
                catch (NotSupportedException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (InvalidOperationException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (IOException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets the body Image.
        /// </summary>
        public static BitmapImage Body
        {
            get
            {
                try
                {
                    return
                        Render.GetBitmapImage(Path.Combine(Directory.GetCurrentDirectory(),
                            CharacterResources.CoreIcons,
                            CharacterResources.SourceBodyUri));
                }
                catch (NotSupportedException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (InvalidOperationException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }
                catch (IOException ex)
                {
                    DebugLog.CreateLogFile(ex.ToString(), ErCode.Error);
                }

                return null;
            }
        }
    }
}