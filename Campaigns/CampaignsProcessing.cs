/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        AvalonsDen/Campaigns/CampaignsProcessing.cs
 * PURPOSE:     Processing of Campaigns, Center Class that will handle all Interfaces concerning the game
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System;
using CampaignDriver;
using Debugger;
using GameEngine;
using Resources;

namespace Campaigns
{
    /// <summary>
    ///     The campaigns processing class.
    /// </summary>
    internal static class CampaignsProcessing
    {
        /// <summary>
        ///     Initiate the basic data for the Map
        ///     Is also the entry for the Campaign
        /// </summary>
        /// <param name="mapname">Name of the Map</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="startPoint">id of Start point</param>
        /// <param name="characterId">id of Character</param>
        /// <param name="character"></param>
        /// <exception cref="NullReferenceException"></exception>
        internal static void InitiateCampaign(string mapname, string campaignName, int startPoint,
            int characterId, int character)
        {
            //basic Checks
            ExceptionCheck(mapname, campaignName, CampaignsResources.MethodNameInitiateMap);

            //get Handler
            var cpnIn = HandlerInputSingleton.Create();
            var cpnOut = HandlerOutputSingleton.Create();

            //Initiate Party, one Character only for now
            cpnIn.InitiateParty(character);

            //load data, Map, Events, Transitions und Master Tile
            var ldData = cpnOut.GetMapObjects(campaignName, mapname, CampaignsResources.SaveStateFalse);

            //load Inventory, only load the master Inventory
            var inventory = cpnOut.GetInventory(campaignName, CampaignsResources.SaveStateFalse);
            CampaignsRegister.SetInventory(inventory);

            //catch errors
            if (CheckLoadErrors(ldData))
                throw new ArgumentException(string.Concat(CampaignsResources.ErrorCouldNotLoadData,
                    nameof(InitiateCampaign)));

            if (inventory == null)
            {
                DebugLog.CreateLogFile(CampaignsResources.InformationCouldNotLoadInventory, ErCode.Error);
                inventory = new PartyInventory();
            }

            //Cleanup Autosave Folder, a must at least on startUp, must be here so we can clean old Save data
            SaveGame.Cleanup(ldData.CampaignName);

            //Campaign Register
            CampaignsRegister.SetRegister(ldData.TransitionDictionary, ldData.MapObject,
                ldData.EventCollection.EventTypeDictionary, ldData.CampaignName, ldData.MasterTileDictionary,
                characterId, inventory);

            //Initiate EventEngine
            var check = cpnIn.InitiateMove(ldData.EventCollection.CoordinatesId, ldData.MapObject.Height,
                ldData.MapObject.Length, ldData.MapObject.Borders, ldData.EventCollection.EventTypeDictionary);

            if (!check)
            {
                DebugLog.CreateLogFile(CampaignsResources.ErrorInitiateMovement, ErCode.Error);
                return;
            }

            //TODO we only Load the map not the MasterTileDictionary so use the Campaign one we will change this in the future so every map can have it's own MasterTileDictionary

            //Get Start Point, Character Id  and set it in CampaignRegister
            CampaignsRegister.SetPoints(startPoint);
        }

        /// <summary>
        ///     Execute the Map Change
        /// </summary>
        /// <param name="idForFurtherEventInfo">Id of the EventTypeExtension</param>
        /// <returns>Check if everything was done correctly</returns>
        internal static bool ExecuteMapChange(int idForFurtherEventInfo)
        {
            //get Handler
            var cpnOut = HandlerOutputSingleton.Create();

            //save the old map, old data and Add Inventory, this is completly optional, but we do it nonetheless
            cpnOut.SetAutosave(CampaignsRegister.CampaignName, CampaignsRegister.MapName,
                CampaignsRegister.PartyInventory);

            //Load Data for the Map, old data
            var mapName = cpnOut.GetMapName(idForFurtherEventInfo, CampaignsRegister.CampaignName,
                CampaignsRegister.MapName);

            //basic Checks
            ExceptionCheck(mapName, CampaignsRegister.CampaignName, CampaignsResources.MethodNameExecuteMapChange);

            //get the new data, new LoadUpCollection
            //was the Map changed, new data
            var mpgchange = cpnOut.ChangedEvent(CampaignsRegister.CampaignName, mapName);

            DebugLog.CreateLogFile(string.Concat(CampaignsResources.InformationMapChanged, mpgchange),
                ErCode.Information);

            //Get Coordinates in Place
            //We must load from the old Asset List, so before we rearrange everything make sure to get the data for the old shit!
            //old data
            var eventIdMap = cpnOut.GetId(idForFurtherEventInfo, CampaignsRegister.CampaignName,
                CampaignsRegister.MapName);

            //could not load new Map Coordinate, return value is -1 for error
            if (eventIdMap == -1)
            {
                DebugLog.CreateLogFile(CampaignsResources.ErrorCouldNotGetCoordinate, ErCode.Error);
                return false;
            }

            DebugLog.CreateLogFile(string.Concat(CampaignsResources.InformationIdofMap, eventIdMap),
                ErCode.Information);

            //load Map Data, new data
            var ldData = cpnOut.GetMapObjects(CampaignsRegister.CampaignName, mapName, mpgchange);

            //catch potential error, and we check Map Object because that is the first object we use and one of the more important ones we should check others as well but meh
            if (ldData?.MapObject == null)
            {
                DebugLog.CreateLogFile(CampaignsResources.ErrorCouldNotLoadData, ErCode.Error);
                return false;
            }

            DebugLog.CreateLogFile(string.Concat(CampaignsResources.InformationMapName, ldData.MapObject.MapName),
                ErCode.Information);

            //TODO we only Load the map not the MasterTileDictionary so use the Campaign one we will change this in the future so every map can have it's own MasterTileDictionary

            var oldData = CampaignsRegister.GetOld();

            //campaign Register, new data, order is important!
            CampaignsRegister.SetRegister(ldData.TransitionDictionary, ldData.MapObject,
                ldData.EventCollection.EventTypeDictionary);

            //get Handler
            var cpn = HandlerInputSingleton.Create();

            //Initiate EventEngine, new data
            var check = cpn.InitiateMove(ldData.EventCollection.CoordinatesId, ldData.MapObject.Height,
                ldData.MapObject.Length, ldData.MapObject.Borders, ldData.EventCollection.EventTypeDictionary);

            //Something went wrong along the way
            if (!check)
            {
                //Reload the Map with the old data to keep the game playable

                DebugLog.CreateLogFile(CampaignsResources.ErrorEventEngineRollback, ErCode.Error);
                CampaignsRegister.SetRegister(oldData.TransitionDictionary, oldData.MapObject,
                    oldData.EventCollection.EventTypeDictionary);
                cpn.InitiateMove(oldData.EventCollection.CoordinatesId, oldData.MapObject.Height,
                    oldData.MapObject.Length, oldData.MapObject.Borders,
                    oldData.EventCollection.EventTypeDictionary);

                return false;
            }

            // get new StartInfo, new data
            var newStartInfo = cpnOut.GetNewStart(eventIdMap,
                ldData.EventCollection.CoordinatesId,
                CampaignsRegister.Length,
                CampaignsRegister.ImageId,
                CampaignsRegister.Layer());

            //New Data

            //sanity check
            if (newStartInfo == null)
            {
                DebugLog.CreateLogFile(CampaignsResources.ErrorCouldNotGetCoordinate, ErCode.Error);
                return false;
            }

            // Set new StartInfo in Register, new data
            CampaignsRegister.SetPoints(newStartInfo);

            return true;
        }

        /// <summary>
        ///     Load a Saved game
        /// </summary>
        /// <param name="saveInfos">Save Object</param>
        internal static void LoadSave(SaveInfos saveInfos)
        {
            //basic Checks
            ExceptionCheck(saveInfos.MapName, saveInfos.CampaignName, CampaignsResources.MethodNameLoadSave);

            //get Handler
            var cpnIn = HandlerInputSingleton.Create();
            var cpnOut = HandlerOutputSingleton.Create();

            //Initiate Party, one Character only for now
            cpnIn.LoadParty(saveInfos.CharacterId);

            //get the new data, new LoadUpCollection
            //was the Map changed?
            var mapgChanged = cpnOut.ChangedEvent(saveInfos.CampaignName, saveInfos.MapName);

            //load data, Map, Events, Transitions und Master Tile, true if changed
            var ldData = cpnOut.GetMapObjects(saveInfos.CampaignName, saveInfos.MapName, mapgChanged);

            //load Inventory, from the Save file in the Temp, if Temp is empty we have to load the master
            var inventory = cpnOut.GetInventory(saveInfos.CampaignName, CampaignsResources.SaveStateFalse);
            CampaignsRegister.SetInventory(inventory);

            //catch error
            if (ldData == null)
            {
                DebugLog.CreateLogFile(CampaignsResources.ErrorCouldNotLoadData, ErCode.Error);
                return;
            }

            if (inventory == null)
            {
                DebugLog.CreateLogFile(CampaignsResources.InformationCouldNotLoadInventory, ErCode.Information);
                inventory = cpnOut.GetInventory(saveInfos.CampaignName, CampaignsResources.SaveStateFalse);

                if (inventory == null)
                {
                    DebugLog.CreateLogFile(CampaignsResources.ErrorCouldNotLoadInventory, ErCode.Error);
                    inventory = new PartyInventory();
                }
            }

            //Campaign Register
            CampaignsRegister.SetRegister(ldData.TransitionDictionary, ldData.MapObject,
                ldData.EventCollection.EventTypeDictionary, ldData.CampaignName, ldData.MasterTileDictionary,
                saveInfos.ImageId, inventory);

            //Initiate EventEngine
            var check = cpnIn.InitiateMove(ldData.EventCollection.CoordinatesId, ldData.MapObject.Height,
                ldData.MapObject.Length, ldData.MapObject.Borders, ldData.EventCollection.EventTypeDictionary);

            if (!check)
            {
                DebugLog.CreateLogFile(CampaignsResources.ErrorInitiateMovement, ErCode.Error);
                return;
            }

            //TODO we only Load the map not the MasterTileDictionary so use the Campaign one we will change this in the future so every map can have it's own MasterTileDictionary

            //Get Start Point, Character Id and set it in CampaignRegister
            CampaignsRegister.SetPoints(saveInfos.PositionId);
        }

        /// <summary>
        ///     TODO FIXME IMPLEMENT
        /// </summary>
        internal static void StartSound()
        {
            MusicEngine.PlayMusic(CampaignsResources.CoreSounds + "Finntroll-Tomhet och Tystnad Härska (Outro).mp3");
        }

        /// <summary>
        ///     Basic sanity checks if something goes wrong here we should definitely throw an Exception
        ///     Called on Start, Load and Map Change
        /// </summary>
        /// <param name="mapName">Name of the Map</param>
        /// <param name="campaignName">Name of the Campaign</param>
        /// <param name="methodName">Caller with the wrong Parameter</param>
        /// <exception cref="ArgumentException"></exception>
        private static void ExceptionCheck(string mapName, string campaignName, string methodName)
        {
            if (string.IsNullOrWhiteSpace(campaignName))
                throw new ArgumentException(string.Concat(CampaignsResources.ErrorNoValidCampaignName, methodName),
                    nameof(campaignName));

            if (string.IsNullOrWhiteSpace(mapName))
                throw new ArgumentException(string.Concat(CampaignsResources.ErrorNoValidMapName, methodName),
                    nameof(mapName));
        }

        /// <summary>
        ///     Check the load errors.
        /// </summary>
        /// <param name="ldData">The ldData.</param>
        /// <returns>The  status of the Checks<see cref="bool" />.</returns>
        private static bool CheckLoadErrors(LoaderContainer ldData)
        {
            if (ldData == null)
            {
                DebugLog.CreateLogFile(string.Concat(CampaignsResources.ErrorCouldNotLoadData, nameof(ldData)),
                    ErCode.Error);
                return true;
            }

            if (ldData.MapObject == null)
            {
                DebugLog.CreateLogFile(string.Concat(CampaignsResources.ErrorCouldNotLoadData, nameof(MapObject)),
                    ErCode.Error);
                return true;
            }

            if (ldData.MasterTileDictionary == null)
            {
                DebugLog.CreateLogFile(
                    string.Concat(CampaignsResources.ErrorCouldNotLoadData, nameof(ldData.MasterTileDictionary)),
                    ErCode.Error);
                return true;
            }

            // ReSharper disable once InvertIf, to keep it consistent here

            if (ldData.MasterBordersDictionary == null)
            {
                DebugLog.CreateLogFile(
                    string.Concat(CampaignsResources.ErrorCouldNotLoadData, nameof(ldData.MasterBordersDictionary)),
                    ErCode.Error);
                return true;
            }

            return false;
        }
    }
}