/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Campaigns/Prompts.cs
 * PURPOSE:     Command Windows for Shortcuts
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using Debugger;
using Interpreter;
using Renderer;

namespace Campaigns
{
    /// <summary>
    ///     Basic Command Prompt
    /// </summary>
    internal static class Prompts
    {
        /// <summary>
        ///     The CPN prompt
        /// </summary>
        private const string CpnPrompt = "CampaignPrompt";

        /// <summary>
        ///     The prompt.
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
                    Command = "Dump",
                    Description = "Dump a Crash Log",
                    ParameterCount = 0
                }
            },
            {
                5,
                new InCommand
                {
                    Command = "Log",
                    Description = "Show log of current Session",
                    ParameterCount = 0
                }
            }
        };

        /// <summary>
        ///     The render tile.
        /// </summary>
        private static RenderTile _renderTile;

        /// <summary>
        ///     Initiate Prompt
        /// </summary>
        /// <param name="renderTile">Display Object</param>
        internal static void Initiate(RenderTile renderTile)
        {
            _renderTile = renderTile;
            _prompt = new Prompt();
            _prompt.SendLogs += SendLogs;
            _prompt.SendCommands += SendCommands;
            _prompt.Initiate(Register, CpnPrompt);
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
                    _prompt.Callbacks(CampaignsResources.PromptCampaigns);
                    break;
                //close the window
                case 1:
                    _prompt.Dispose();
                    break;

                case 2:
                    _renderTile.ShowGrid();
                    _prompt.Callbacks(CampaignsResources.PromptDone);
                    break;

                case 3:
                    _renderTile.ShowNumbers();
                    _prompt.Callbacks(CampaignsResources.PromptDone);
                    break;

                case 4:
                    DebugLog.CreateDump();
                    _prompt.Callbacks(CampaignsResources.PromptDone);
                    break;
                case 5:
                    DebugLog.CreateDump();

                    //Dump the current log into the Console
                    foreach (var log in DebugLog.CurrentLog) _prompt.Callbacks(log);
                    _prompt.Callbacks(CampaignsResources.PromptDone);
                    break;

                default:
                    DebugLog.CreateLogFile(CampaignsResources.ErrorPrompt, 0);
                    break;
            }
        }
    }
}