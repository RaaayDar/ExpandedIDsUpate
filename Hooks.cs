using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using RWCustom;
using BepInEx;
using Debug = UnityEngine.Debug;


#pragma warning disable CS0618

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

namespace Hooks;

[BepInPlugin("RaaayDar.BiggerIDrange", "IDs Expanded UPDATED", "1.5.0")]
public class Hooks : BaseUnityPlugin
{
    
    private void OnEnable()
    {
        On.RainWorld.OnModsInit += RainWorldOnOnModsInit;
        MachineConnector.SetRegisteredOI("RaaayDar.BiggerIDrange", new Options());
    }

    private bool IsInit;
    private void RainWorldOnOnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
    {
        orig(self);
        try
        {
            if (IsInit) return;

            //Your hooks go here
            On.RainWorldGame.GetNewID += RainWorldGame_GetNewID;
            On.RainWorldGame.GetNewID_int += RainWorldGame_GetNewID_int;
            

            On.RainWorldGame.ShutDownProcess += RainWorldGameOnShutDownProcess;
            On.GameSession.ctor += GameSessionOnctor;

            IsInit = true;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex);
            throw;
        }
    }

    private EntityID RainWorldGame_GetNewID_int(On.RainWorldGame.orig_GetNewID_int orig, RainWorldGame self, int spawner)
    {
        return new EntityID(spawner, UnityEngine.Random.Range(-2147483648, 2147483647));
    }

    private EntityID RainWorldGame_GetNewID(On.RainWorldGame.orig_GetNewID orig, RainWorldGame self)
    {
        return new EntityID(-1, UnityEngine.Random.Range(-2147483648, 2147483647));
        
    }

    private void RainWorldGameOnShutDownProcess(On.RainWorldGame.orig_ShutDownProcess orig, RainWorldGame self)
    {
        orig(self);
        ClearMemory();
    }
    private void GameSessionOnctor(On.GameSession.orig_ctor orig, GameSession self, RainWorldGame game)
    {
        orig(self, game);
        ClearMemory();
    }

    #region Helper Methods

    private void ClearMemory()
    {
        //If you have any collections (lists, dictionaries, etc.)
        //Clear them here to prevent a memory leak
        //YourList.Clear();
    }


    #endregion
}