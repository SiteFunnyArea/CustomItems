﻿namespace CustomItems_SFA.Items;

using Exiled.API.Enums;
using Exiled.API.Extensions;
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
    public class InjectionTP : CustomItem
    {
        public override uint Id { get; set; } = 2003;
        public override string Name { get; set; } = "Injection-TP";
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
            Broadcast bc = new Broadcast("<color=#8860f7><i>You begin to feel tired...</i></color>", 5);
            ev.Player.Broadcast(bc);
            Effect e = new Effect(EffectType.SinkHole, 3, 1);

            ev.Player.EnableEffect(e);

            Timing.CallDelayed(3f, () =>
            {
                Room r = Room.Get(ev.Player.CurrentRoom.Zone).GetRandomValue();
                if (r.Type == RoomType.Lcz173)
                    r = Room.Get(RoomType.Lcz330);
                Vector3 v3 = new Vector3(r.Position.x + 0.4f, r.Position.y + 1f, r.Position.z + 0.6f);
                ev.Player.Transform.position = v3;

            });


        });
        }
    }

