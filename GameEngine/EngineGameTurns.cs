/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Engine/EngineGameTurns.cs
 * PURPOSE:     Basic Time Count for the game
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

namespace GameEngine
{
    /// <summary>
    ///     Engine that counts Game Turns
    /// </summary>
    public static class EngineGameTurns
    {
        /// <summary>
        ///     The day Cycle.
        /// </summary>
        private static int _dayCycle;

        /// <summary>
        ///     The years.
        /// </summary>
        private static int _years;

        /// <summary>
        ///     Current Day
        /// </summary>
        public static int CurrentYear => CurrentCyle / _years;

        /// <summary>
        ///     Current Day
        /// </summary>
        public static int CurrentCyle { get; private set; }

        /// <summary>
        ///     Current Day Cycle
        /// </summary>
        public static int CycleModulo { get; private set; }

        /// <summary>
        ///     Basic Master Action Count
        /// </summary>
        public static int Mastercount { get; private set; }

        /// <summary>
        ///     The initiate.
        /// </summary>
        /// <param name="daycycle">10 split in half, 5 day, 5 night</param>
        /// <param name="startTime">Start Time</param>
        /// <param name="years">In Game Year</param>
        public static void Initiate(int daycycle, int startTime, int years)
        {
            _dayCycle = daycycle;
            if (startTime != 0) CountActions(startTime);

            _years = years;

            //Initiate Timer
            CurrentCyle = 0;
            CycleModulo = startTime;
        }

        /// <summary>
        ///     Action Counter
        /// </summary>
        /// <param name="actions">Number of Actions</param>
        public static void CountActions(int actions)
        {
            if (actions == 0) return;

            Mastercount += actions;

            var modulo = Mastercount % _dayCycle; //modulo
            var divide = Mastercount / _dayCycle; //divide

            CycleModulo = modulo;
            CurrentCyle = divide;
        }
    }
}