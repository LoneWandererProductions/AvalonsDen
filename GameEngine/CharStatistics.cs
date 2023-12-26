/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/CharacterDisplay/CharStatistics.cs
 * PURPOSE:     Basic Character Statistics
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using System.Windows.Input;
using Resources;
using ViewModel;

namespace GameEngine
{
    /// <inheritdoc />
    /// <summary>
    ///     Description of Character
    /// </summary>
    public sealed class CharStatistics : ObservableObject
    {
        /// <summary>
        ///     The focus Parameter (const). Value: 0.1.
        /// </summary>
        private const double FocusParam = 0.1;

        /// <summary>
        ///     The aether Parameter (const). Value: 0.1.
        /// </summary>
        private const double AetherParam = 0.1;

        /// <summary>
        ///     The rage Parameter (const). Value: 0.1.
        /// </summary>
        private const double RageParam = 0.1;

        /// <summary>
        ///     The spirit Parameter (const). Value: 0.1.
        /// </summary>
        private const double SpiritParam = 0.1;

        /// <summary>
        ///     The body Parameter (const). Value: 0.1.
        /// </summary>
        private const double BodyParam = 0.1;

        /// <summary>
        ///     The wisdom Parameter (const). Value: 0.1.
        /// </summary>
        private const double WisdomParam = 0.1;

        /// <summary>
        ///     The resistance Parameter (const). Value: 0.1.
        /// </summary>
        private const double ResistanceParam = 0.1;

        /// <summary>
        ///     The shielding Parameter (const). Value: 0.1.
        /// </summary>
        private const double ShieldingParam = 0.1;

        /// <summary>
        ///     The carrying weight Parameter (const). Value: 0.1.
        /// </summary>
        private const double CarryingWeightParam = 0.1;

        /// <summary>
        ///     The speech craft Parameter (const). Value: 0.1.
        /// </summary>
        private const double SpeechCraftParam = 0.1;

        /// <summary>
        ///     The agility.
        /// </summary>
        private static int _agility;

        /// <summary>
        ///     The charisma.
        /// </summary>
        private static int _charisma;

        /// <summary>
        ///     The intelligence.
        /// </summary>
        private static int _intelligence;

        /// <summary>
        ///     The strength.
        /// </summary>
        private static int _strength;

        /// <summary>
        ///     The will.
        /// </summary>
        private static int _will;

        /// <summary>
        ///     The wisdom.
        /// </summary>
        private static int _wisdom;

        /// <summary>
        ///     The endurance.
        /// </summary>
        private static int _endurance;

        /// <summary>
        ///     The calculated spirit.
        /// </summary>
        private static int _calcSpirit;

        /// <summary>
        ///     The calculated body.
        /// </summary>
        private static int _calcBody;

        /// <summary>
        ///     The calculated resistance.
        /// </summary>
        private static int _calcResistance;

        /// <summary>
        ///     The calculated shielding.
        /// </summary>
        private static int _calcShielding;

        /// <summary>
        ///     The calculated hit chance.
        /// </summary>
        private static int _calcHitChance;

        /// <summary>
        ///     The calculated critical chance.
        /// </summary>
        private static int _calcCriticalChance;

        /// <summary>
        ///     The calculated initiative.
        /// </summary>
        private static int _calcInitiative;

        /// <summary>
        ///     The calculated action points.
        /// </summary>
        private static int _calcActionPoints;

        /// <summary>
        ///     The calculated carrying weight.
        /// </summary>
        private static int _calcCarryingWeight;

        /// <summary>
        ///     The calculated speech craft.
        /// </summary>
        private static int _calcSpeechCraft;

        /// <summary>
        ///     The calculated combat value.
        /// </summary>
        private static int _calcCombatValue;

        private ICommand _addEndurance, _addCharisma, _addIntelligence, _addStrength, _addWill, _addAgility, _addWisdom;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CharStatistics" /> class.
        /// </summary>
        /// <param name="stats">The stats.</param>
        public CharStatistics(CharacterBaseStats stats)
        {
            if (stats == null) return;

            //ClassType = stats.CharacterClass; to be calculated
            Wisdom = stats.Wisdom;
            Strength = stats.Strength;
            Agility = stats.Agility;
            Intelligence = stats.Intelligence;
            Will = stats.Will;
            Charisma = stats.Charisma;
            Endurance = stats.Endurance;
        }

        /// <summary>
        ///     0 is Mana
        ///     1 is Rage
        ///     2 is Focus
        /// </summary>
        public int ClassType { get; set; }

        /// <summary>
        ///     Gets or sets the stat points.
        /// </summary>
        public int StatPoints { get; set; }

        /// <summary>
        ///     Gets or sets the skill points.
        /// </summary>
        public int SkillPoints { get; set; }

        /// <summary>
        ///     Main Stat
        /// </summary>
        public int Wisdom
        {
            get => _wisdom;
            set
            {
                _wisdom = value;
                CalcSpirits();
                CalcBodys();
                Resistances();
                Shielding();
                Initiatives();
                ActionPoints();
                SpeechCrafts();
                Combatvalues();
                RaisePropertyChangedEvent(nameof(Wisdom));
            }
        }

        /// <summary>
        ///     Main Stat
        /// </summary>
        public int Strength
        {
            get => _strength;
            set
            {
                _strength = value;
                CalcBodys();
                Resistances();
                ActionPoints();
                CarryingWeights();
                Combatvalues();
                RaisePropertyChangedEvent(nameof(Strength));
            }
        }

        /// <summary>
        ///     Main Stat
        /// </summary>
        public int Agility
        {
            get => _agility;
            set
            {
                _agility = value;
                CalcBodys();
                HitChances();
                Initiatives();
                CriticalChances();
                ActionPoints();
                Combatvalues();
                RaisePropertyChangedEvent(nameof(Agility));
            }
        }

        /// <summary>
        ///     Main Stat
        /// </summary>
        public int Intelligence
        {
            get => _intelligence;
            set
            {
                _intelligence = value;
                CalcSpirits();
                Shielding();
                HitChances();
                Initiatives();
                CriticalChances();
                Combatvalues();
                RaisePropertyChangedEvent(nameof(Intelligence));
            }
        }

        /// <summary>
        ///     Main Stat
        /// </summary>
        public int Will
        {
            get => _will;
            set
            {
                _will = value;
                CalcSpirits();
                Resistances();
                Shielding();
                CarryingWeights();
                Combatvalues();
                RaisePropertyChangedEvent(nameof(Will));
            }
        }

        /// <summary>
        ///     Main Stat
        /// </summary>
        public int Charisma
        {
            get => _charisma;
            set
            {
                _charisma = value;
                SpeechCrafts();
                RaisePropertyChangedEvent(nameof(Charisma));
            }
        }

        /// <summary>
        ///     Main Stat
        /// </summary>
        public int Endurance
        {
            get => _endurance;
            set
            {
                _endurance = value;
                RaisePropertyChangedEvent(nameof(Endurance));
            }
        }

        /// <summary>
        ///     Gets or sets the calculated spirit.
        /// </summary>
        public int CalcSpirit
        {
            get => _calcSpirit;
            set
            {
                _calcSpirit = value;
                SpeechCrafts();
                RaisePropertyChangedEvent(nameof(CalcSpirit));
            }
        }

        /// <summary>
        ///     Gets or sets the calculate body.
        /// </summary>
        public int CalcBody
        {
            get => _calcBody;
            set
            {
                _calcBody = value;
                SpeechCrafts();
                RaisePropertyChangedEvent(nameof(CalcBody));
            }
        }

        /// <summary>
        ///     Gets or sets the calculate resistance.
        /// </summary>
        public int CalcResistance
        {
            get => _calcResistance;
            set
            {
                _calcResistance = value;
                RaisePropertyChangedEvent(nameof(CalcResistance));
            }
        }

        /// <summary>
        ///     Gets or sets the calculate shielding.
        /// </summary>
        public int CalcShielding
        {
            get => _calcShielding;
            set
            {
                _calcShielding = value;
                RaisePropertyChangedEvent(nameof(CalcShielding));
            }
        }

        /// <summary>
        ///     Gets or sets the calculate hit chance.
        /// </summary>
        public int CalcHitChance
        {
            get => _calcHitChance;
            set
            {
                _calcHitChance = value;
                RaisePropertyChangedEvent(nameof(CalcHitChance));
            }
        }

        /// <summary>
        ///     Gets or sets the calculate critical chance.
        /// </summary>
        public int CalcCriticalChance
        {
            get => _calcCriticalChance;
            set
            {
                _calcCriticalChance = value;
                RaisePropertyChangedEvent(nameof(CalcCriticalChance));
            }
        }

        /// <summary>
        ///     Gets or sets the calculate initiative.
        /// </summary>
        public int CalcInitiative
        {
            get => _calcInitiative;
            set
            {
                _calcInitiative = value;
                RaisePropertyChangedEvent(nameof(CalcInitiative));
            }
        }

        /// <summary>
        ///     Gets or sets the calculate action points.
        /// </summary>
        public int CalcActionPoints
        {
            get => _calcActionPoints;
            set
            {
                _calcActionPoints = value;
                RaisePropertyChangedEvent(nameof(CalcActionPoints));
            }
        }

        /// <summary>
        ///     Gets or sets the calculate carrying weight.
        /// </summary>
        public int CalcCarryingWeight
        {
            get => _calcCarryingWeight;
            set
            {
                _calcCarryingWeight = value;
                RaisePropertyChangedEvent(nameof(CalcCarryingWeight));
            }
        }

        /// <summary>
        ///     Gets or sets the calculate speech craft.
        /// </summary>
        public int CalcSpeechCraft
        {
            get => _calcSpeechCraft;
            set
            {
                _calcSpeechCraft = value;
                RaisePropertyChangedEvent(nameof(CalcSpeechCraft));
            }
        }

        /// <summary>
        ///     Gets or sets the calculate combat value.
        /// </summary>
        public int CalcCombatValue
        {
            get => _calcCombatValue;
            set
            {
                _calcCombatValue = value;
                RaisePropertyChangedEvent(nameof(CalcCombatValue));
            }
        }

        /// <summary>
        ///     Gets the add charisma.
        /// </summary>
        public ICommand AddCharisma =>
            _addCharisma ??= new DelegateCommand<object>(AddCharismas, CanExecute);

        /// <summary>
        ///     Gets the add intelligence.
        /// </summary>
        public ICommand AddIntelligence =>
            _addIntelligence ??= new DelegateCommand<object>(AddIntelligences, CanExecute);

        /// <summary>
        ///     Gets the add strength.
        /// </summary>
        public ICommand AddStrength =>
            _addStrength ??= new DelegateCommand<object>(AddStrengths, CanExecute);

        /// <summary>
        ///     Gets the add will.
        /// </summary>
        public ICommand AddWill => _addWill ??= new DelegateCommand<object>(AddWills, CanExecute);

        /// <summary>
        ///     Gets the add agility.
        /// </summary>
        public ICommand AddAgility =>
            _addAgility ??= new DelegateCommand<object>(AddAgilitys, CanExecute);

        /// <summary>
        ///     Gets the add wisdom.
        /// </summary>
        public ICommand AddWisdom => _addWisdom ??= new DelegateCommand<object>(AddWisdoms, CanExecute);

        /// <summary>
        ///     Gets the add endurance.
        /// </summary>
        public ICommand AddEndurance =>
            _addEndurance ??= new DelegateCommand<object>(AddEndurances, CanExecute);

        /// <summary>
        ///     Gets a value indicating whether this instance can execute.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can execute the specified object; otherwise, <c>false</c>.
        /// </returns>
        /// <value>
        ///     <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </value>
        public bool CanExecute(object obj)
        {
            // check if executing is allowed, i.e., validate, check if a process is running, etc.
            return true;
        }

        /// <summary>
        ///     Calculate spirit.
        /// </summary>
        private void CalcSpirits()
        {
            CalcSpirit = (int)Math.Round(_intelligence * _will * SpiritParam + _wisdom * WisdomParam);
        }

        /// <summary>
        ///     Calculate body.
        /// </summary>
        private void CalcBodys()
        {
            CalcBody = (int)Math.Round(_strength * _agility * BodyParam + _wisdom * WisdomParam);
        }

        /// <summary>
        ///     Calculate resistances.
        /// </summary>
        private void Resistances()
        {
            CalcResistance = (int)Math.Round(_strength * _will * ResistanceParam + _wisdom * WisdomParam);
        }

        /// <summary>
        ///     Calculate shielding.
        /// </summary>
        private void Shielding()
        {
            CalcShielding = (int)Math.Round(_intelligence * _will * ShieldingParam + _wisdom * WisdomParam);
        }

        /// <summary>
        ///     Calculate Hit Chances.
        /// </summary>
        private void HitChances()
        {
            CalcHitChance = (int)Math.Round(_intelligence * _agility * 0.25);
        }

        /// <summary>
        ///     Calculate Critical Chances.
        /// </summary>
        private void CriticalChances()
        {
            CalcCriticalChance = (int)Math.Round(_intelligence * _agility * 0.25);
        }

        /// <summary>
        ///     Calculate initiatives.
        /// </summary>
        private void Initiatives()
        {
            CalcInitiative = (int)Math.Round(_wisdom * _intelligence * _agility * 0.25);
        }

        /// <summary>
        ///     Calculate ActionPoints.
        /// </summary>
        private void ActionPoints()
        {
            CalcActionPoints = (int)Math.Round(_wisdom * _agility * _strength * 0.25);
        }

        /// <summary>
        ///     Calculate carrying weights.
        /// </summary>
        private void CarryingWeights()
        {
            CalcCarryingWeight = (int)Math.Round(10 * _strength + _will * CarryingWeightParam);
        }

        /// <summary>
        ///     Calculate speech craft.
        /// </summary>
        private void SpeechCrafts()
        {
            CalcSpeechCraft =
                (int)
                Math.Round(_charisma + _wisdom * WisdomParam + _calcSpirit * SpeechCraftParam
                           + _calcBody * SpeechCraftParam);
        }

        /// <summary>
        ///     Calculate the Combat Value.
        /// </summary>
        private void Combatvalues()
        {
            CalcCombatValue = GetCombatValue();
        }

        /// <summary>
        ///     Calculate Damage Modifier
        /// </summary>
        /// <returns>Calculated Combat Value</returns>
        private int GetCombatValue()
        {
            switch (ClassType)
            {
                case 0:
                    return (int)Math.Round(_agility * _will * FocusParam + _wisdom * WisdomParam);

                case 1:
                    return (int)Math.Round(_strength * _will * RageParam + _wisdom * WisdomParam);

                case 2:
                    return (int)Math.Round(_intelligence * _will * AetherParam + _wisdom * WisdomParam);

                default:
                    return 0;
            }
        }

        /// <summary>
        ///     Add the Charisma.
        /// </summary>
        private void AddCharismas(object obj)
        {
            Charisma++;
        }

        /// <summary>
        ///     Add the intelligences.
        /// </summary>
        private void AddIntelligences(object obj)
        {
            Intelligence++;
        }

        /// <summary>
        ///     Add the strengths.
        /// </summary>
        private void AddStrengths(object obj)
        {
            Strength++;
        }

        /// <summary>
        ///     Add the wills.
        /// </summary>
        private void AddWills(object obj)
        {
            Will++;
        }

        /// <summary>
        ///     Add the agility.
        /// </summary>
        private void AddAgilitys(object obj)
        {
            Agility++;
        }

        /// <summary>
        ///     Add the wisdoms.
        /// </summary>
        private void AddWisdoms(object obj)
        {
            Wisdom++;
        }

        /// <summary>
        ///     Add the endurances.
        /// </summary>
        private void AddEndurances(object obj)
        {
            Endurance++;
        }
    }
}