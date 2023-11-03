namespace CustomItems_SFA.Items;

using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using CustomRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YamlDotNet.Serialization;
using Player = Exiled.Events.Handlers.Player;
using Exiled.Events.EventArgs.Item;
using Exiled.Events.Handlers;

[Exiled.API.Features.Attributes.CustomItem(ItemType.KeycardJanitor)]
   
    public class LockdownDevice : CustomItem
    {
        public override uint Id { get; set; } = 4501;
        public override string Name { get; set; } = "Lockdown Device";
        public override string Description { get; set; } = "The <color=#FFEA00>Lockdown Device</color> will lockdown any door for 10 seconds, though it has a <color=#FFEA00>Cooldown of 20 seconds.</color>";
        public override float Weight { get; set; } = 1f;

    [YamlIgnore]
    public bool CooldownEnabled = false;

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
        Player.InteractingDoor += OnDropping;

        base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
        Player.InteractingDoor -= OnDropping;

            base.UnsubscribeEvents();
        }

    public void OnDropping(InteractingDoorEventArgs ev)
    {
        if (!Check(ev.Player.CurrentItem))
            return;
        if (CooldownEnabled == true)
            return;

        ev.Door.Lock(10, DoorLockType.Lockdown079);
        PluginAPI.Core.Log.Debug("Interacting with Door");
        CooldownEnabled = true;
        Timing.CallDelayed(20f, () =>
        {
            CooldownEnabled = false;
        });
    }

    }

