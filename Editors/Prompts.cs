/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Editors/Prompts.cs
 * PURPOSE:     Command Windows for Shortcuts
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using DatabaseDriver;
using Debugger;
using Interpreter;

namespace Editors
{
    /// <summary>
    ///     Basic Command Prompt
    /// </summary>
    internal static class Prompts
    {
        //TODO add regeneration!

        /// <summary>
        ///     The Editor prompt
        /// </summary>
        private const string EdrPrompt = "EditorPrompt";

        /// <summary>
        ///     The Command prompt.
        /// </summary>
        private static Prompt _prompt;

        /// <summary>
        ///     Our Command Input Register
        /// </summary>
        private static readonly Dictionary<int, InCommand> Register = new()
        {
            {
                0,
                new InCommand
                {
                    Command = "Info",
                    Description = "Info about this Prompt this one is for the Editor only",
                    ParameterCount = 0
                }
            },
            {
                1,
                new InCommand
                {
                    Command = "Close",
                    Description = "Close the prompt",
                    ParameterCount = 0
                }
            },
            {
                2,
                new InCommand
                {
                    Command = "Grid",
                    Description = "Switch state of Grid",
                    ParameterCount = 0
                }
            },
            {
                3,
                new InCommand
                {
                    Command = "Numbers",
                    Description = "Switch state of Numbers",
                    ParameterCount = 0
                }
            },
            {
                4,
                new InCommand
                {
                    Command = "RepairItemDb",
                    Description =
                        "Tries to repair Tables to the ITem Database, first Parameter is the location, second is the Name without extension",
                    ParameterCount = 2
                }
            },
            {
                5,
                new InCommand
                {
                    Command = "Dump",
                    Description = "Dump a Crash Log",
                    ParameterCount = 0
                }
            }
        };

        /// <summary>
        ///     Start up the prompt
        /// </summary>
        internal static void Initiate()
        {
            _prompt = new Prompt();
            _prompt.SendLogs += SendLogs;
            _prompt.SendCommands += SendCommands;
            _prompt.Initiate(Register, EdrPrompt);
            _prompt.StartWindow();
        }

        /// <summary>
        ///     Just send our Debug stuff to our Handler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The Command e.</param>
        private static void SendLogs(object sender, string e)
        {
            DebugLog.CreateLogFile(e, ErCode.External);
        }

        /// <summary>
        ///     Handle the received commands.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The Command e.</param>
        private static void SendCommands(object sender, OutCommand e)
        {
            switch (e.Command)
            {
                //Just show some stuff
                case 0:
                    _prompt.Callbacks(EditorResources.PromptEditors);
                    break;

                //close the window
                case 1:
                    _prompt.Dispose();
                    break;

                case 2:
                    EditorHandleGraphics.ShowGrid();
                    _prompt.Callbacks(EditorResources.PromptDone);
                    break;

                case 3:
                    EditorHandleGraphics.ShowNumbers();
                    _prompt.Callbacks(EditorResources.PromptDone);
                    break;
                //Internal Tool that tries to add Changes of the Database Layout to existing Databases, might be extended in the Future
                case 4:
                    var db = HandlerInputSingleton.Create(e.Parameter[0], e.Parameter[1]);
                    _prompt.Callbacks(db.TryRepair());
                    break;

                case 5:
                    DebugLog.CreateDump();
                    _prompt.Callbacks(EditorResources.PromptDone);
                    break;

                default:
                    _prompt.Callbacks(EditorResources.PromptDone);
                    break;
            }
        }
    }
}