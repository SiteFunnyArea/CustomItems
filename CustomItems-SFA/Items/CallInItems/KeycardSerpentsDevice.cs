namespace CustomItems_SFA.Items;

using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using GameCore;
using InventorySystem;
using MapGeneration;
using Mirror;
using PluginAPI.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Player = Exiled.Events.Handlers.Player;



[Exiled.API.Features.Attributes.CustomItem(ItemType.KeycardChaosInsurgency)]
public class KeycardSerpentsDevice : CustomItem
{

    public override uint Id { get; set; } = 201;
    public override string Name { get; set; } = "Serpents Device";
    public override string Description { get; set; } = "This keycard can be used to call in <color=#FFEA00>1-2 spectators from the dead.</color><br>To use it, you <color=#FFEA00>hold out</color> the keycard <color=#FFEA00>then drop it through your inventory</color> to spawn <color=#960018>Serpents Hand Conscript(s)</color>.\r\n";
    public override float Weight { get; set; } = 1f;
        public override SpawnProperties? SpawnProperties { get; set; } = new()
        {
            Limit = 1,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
        {
            new()
            {
                Chance = 0,
                Location = SpawnLocationType.InsideGateB,
            },
        },
        };

        protected override void SubscribeEvents()
        {
        Player.DroppingItem += DroppingItem;
        base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Player.DroppingItem -= DroppingItem;
            base.UnsubscribeEvents();
        }


    private void DroppingItem(DroppingItemEventArgs ev)
    {
        if (!Check(ev.Player.CurrentItem))
            return;
        int Amount = 0;
        if (Exiled.API.Features.Player.List.Count > 1)
        {
            foreach (Exiled.API.Features.Player p in Exiled.API.Features.Player.List)
            {
                if (p.Role.Type == PlayerRoles.RoleTypeId.Spectator && Amount != 2)
                {
                    Amount++;
                    if(EarlyRespawnWave.Plugin.Instance.sM != null)
                    {
                        EarlyRespawnWave.Managers.SpawnManager spawn = EarlyRespawnWave.Plugin.Instance.sM;
                        spawn.SpawnClass(EarlyRespawnWave.Plugin.Instance.Config.SerpentsHand.SHConscript, p);
                        p.Transform.position = new Vector3(ev.Player.Position.x + 0.1f, ev.Player.Position.y + 0.2f, ev.Player.Position.z - 0.1f);
                        ev.Player.Broadcast(10, $"<b>{Amount} Serpents Hand Conscript(s) were called in.</b>");
                        ev.IsAllowed = false;
                        ev.Player.RemoveItem(ev.Player.CurrentItem);
                    }
                }
            }
        }
        else
        {
            ev.Player.Broadcast(10, "<b>No one was called in.</b>");
            ev.Player.RemoveItem(ev.Player.CurrentItem);
        }
    }

}

