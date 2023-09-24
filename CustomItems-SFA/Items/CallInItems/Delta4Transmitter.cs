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



[Exiled.API.Features.Attributes.CustomItem(ItemType.KeycardGuard)]
public class Delta4Transmitter : CustomItem
{

    public override uint Id { get; set; } = 2;
    public override string Name { get; set; } = "Delta-4 Keycard";
    public override string Description { get; set; } = "Once this item is dropped while in your hands, 1-2 reinforcements will be called.";
    [Description("Message that will appear on reinforcements' screens once they spawn.")]
    public Broadcast RespawnMessage { get; set; } = new()
    {
        Content = "<b>You have been called in as</b> <color=#9ca3a3><b>Combatant Reinforcement:</b></color> <color=#2bad33><b>Delta-4 RRT</b></color><br><i>There has been a containment breach at the site.</i><br><i>Follow orders from the Captain.</i>",
        Duration = 10,
        Show = true,
        Type = global::Broadcast.BroadcastFlags.Normal
    };
    [Description("Message that will appear on the person requesting reinforcements if none can spawn.")]
    public Broadcast FailedMessage { get; set; } = new()
    {
        Content = "<b>No reinforcements could be called.</b>\r\n",
        Duration = 10,
        Show = true,
        Type = global::Broadcast.BroadcastFlags.Normal
    };

    [Description("Player Count needed for the item to spawn in reinforcements.")]
    public int PlayerListCountToSpawn { get; set; } = 1;

    [Description("The offset that reinforcements will spawn at once teleported to the person requesting reinforcements.")]
    public Vector3 Offset { get; set; } = new Vector3()
    {
        x = 0.4f,
        y = 0f,
        z = -0.2f,
    };

    [Description("The following info below is for the Delta-4 Reinforcements Role.")]

    public string RoleName { get; set; } = "Delta4CombatantReinforcement";
    public string CustomInfo { get; set; } = "Delta-4 Combatant Reinforcement";
    public List<ItemType> Inventory { get; set; } = new()
    {
        ItemType.GunFSP9,
        ItemType.Radio,
        ItemType.KeycardMTFPrivate,
        ItemType.Flashlight,
        ItemType.Painkillers,
        ItemType.ArmorCombat,
    };

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
        Player.Dying += OnDeath;
        Player.ChangingRole += OnRoleChanging;
        base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Player.DroppingItem -= DroppingItem;
            Player.Dying -= OnDeath;
            Player.ChangingRole -= OnRoleChanging;
            base.UnsubscribeEvents();
        }

    private void OnDeath(DyingEventArgs ev)
    {
        if(ev.Player.UniqueRole.Contains (RoleName))
        {
            ev.Player.CustomInfo = "";
            ev.Player.UniqueRole = "";
        }
    }

    private void OnRoleChanging(ChangingRoleEventArgs ev)
    {
        if (ev.Player.UniqueRole.Contains(RoleName))
        {
            ev.Player.CustomInfo = "";
            ev.Player.UniqueRole = "";
        }
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
                if(p.Role.Type == PlayerRoles.RoleTypeId.Spectator && Amount != 2)
                {
                    Amount++;
                    SpawnDelta7Role(p, ev.Player);
                }
            }
            if(Amount <= 0)
            {
                ev.Player.Broadcast(FailedMessage);

            }
            else
            {
                Broadcast Success = new Broadcast();
                Success.Duration = 10;
                Success.Content = $"<b>{Amount} reinforcement(s) were called.</b>\r\n";
                Success.Show = true;
                Success.Type = global::Broadcast.BroadcastFlags.Normal;
                ev.Player.Broadcast(Success);
                
            }

            ev.IsAllowed = false;
            ev.Player.RemoveItem(ev.Player.CurrentItem);
        }
        else
        {
            ev.Player.Broadcast(FailedMessage);
        }


    }
    public void SpawnDelta7Role(Exiled.API.Features.Player reinforcement, Exiled.API.Features.Player reinforcer)
    {
        reinforcement.RoleManager.ServerSetRole(PlayerRoles.RoleTypeId.FacilityGuard, PlayerRoles.RoleChangeReason.Revived);
        Vector3 v = new Vector3(reinforcer.Transform.position.x + Offset.x, reinforcer.Transform.position.y + Offset.y, reinforcer.Transform.position.z + Offset.z);
        reinforcement.Teleport(v);
        reinforcement.Broadcast(RespawnMessage);
        reinforcement.UniqueRole = RoleName;
        reinforcement.CustomInfo = CustomInfo;
        reinforcement.ClearInventory();
        foreach (ItemType it in Inventory)
        {
            reinforcement.AddItem(it);
        }
        reinforcement.AddAmmo(AmmoType.Nato9, 40);
    }


}

