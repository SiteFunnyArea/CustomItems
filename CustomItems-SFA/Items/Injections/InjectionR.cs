namespace CustomItems_SFA.Items;

using Exiled.API.Enums;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Player = Exiled.Events.Handlers.Player;



    [Exiled.API.Features.Attributes.CustomItem(ItemType.Adrenaline)]
    public class InjectionR : CustomItem
    {
        public override uint Id { get; set; } = 2005;
        public override string Name { get; set; } = "Injection-R";
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
    public void Hurt(HurtingEventArgs ev)
    {
        if(ev.Attacker != null)
        {
            if (ev.Attacker.UniqueRole.Contains("-hasR"))
            {
                ev.DamageHandler.Damage *= 1.5f;
            }
        }
    }

    protected override void SubscribeEvents()
        {
            Player.UsingItem += OnUsingItem;
            Player.Hurting += Hurt;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Player.UsingItem -= OnUsingItem;
            Player.Hurting -= Hurt;

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
            Exiled.API.Features.Broadcast bc = new Exiled.API.Features.Broadcast("<color=#9e050a><i>You feel rage consume your soul...</i></color>", 5);
            ev.Player.Broadcast(bc);
            ev.Player.UniqueRole = ev.Player.UniqueRole + "-hasR";

            Timing.CallDelayed(13f, () =>
            {
                ev.Player.UniqueRole = "";
            });
        });
        }
    }

