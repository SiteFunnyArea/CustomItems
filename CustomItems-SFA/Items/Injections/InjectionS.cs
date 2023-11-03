﻿namespace CustomItems_SFA.Items;

using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Player = Exiled.Events.Handlers.Player;



    [Exiled.API.Features.Attributes.CustomItem(ItemType.Adrenaline)]
    public class InjectionS : CustomItem
    {
        public override uint Id { get; set; } = 2002;
        public override string Name { get; set; } = "Injection-S";
        public override string Description { get; set; } = "This is a template item.";
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
            Player.UsingItem += OnUsingItem;

            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Player.UsingItem -= OnUsingItem;

            base.UnsubscribeEvents();
        }

        private void OnUsingItem(UsingItemEventArgs ev)
        {
        if (!Check(ev.Player.CurrentItem))
            return;

        Timing.CallDelayed(1.95f, () =>
        {
            ev.IsAllowed = false;
            ev.Player.RemoveItem(ev.Player.CurrentItem);
            Broadcast bc = new Broadcast("<color=#75d4ed><i>You feel faster, a lot faster...</i></color>", 5);
            ev.Player.Broadcast(bc);
            Effect e = new Effect(EffectType.MovementBoost, 15, 110);
            Effect e2 = new Effect(EffectType.Invigorated, 15, 1);
            ev.Player.EnableEffect(e);
            ev.Player.EnableEffect(e2);
        });
        }
    }

