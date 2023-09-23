namespace CustomItems_SFA.Items;

using Exiled.API.Enums;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Player = Exiled.Events.Handlers.Player;



    [Exiled.API.Features.Attributes.CustomItem(ItemType.Adrenaline)]
    public class DietCoke : CustomItem
    {
        public override uint Id { get; set; } = 754;
        public override string Name { get; set; } = "Diet Coke";
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
            Player.Dying += OnDying;
            Player.Hurting += OnHurting;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Player.UsingItem -= OnUsingItem;
            Player.Dying -= OnDying;
            Player.Hurting -= OnHurting;
            base.UnsubscribeEvents();
        }

        private void OnUsingItem(UsingItemEventArgs ev)
        {
        if (!Check(ev.Player.CurrentItem))
            return;
        ev.Player.UniqueRole = ev.Player.UniqueRole + "-207Prevent";
        
        ev.Player.RemoveItem(ev.Player.CurrentItem);
        }

        public void OnDying(DyingEventArgs ev)
    {
        if (ev.Player.UniqueRole.Contains("-207Prevent"))
        {
            ev.Player.UniqueRole = "";
        }
    }

    public void OnHurting(HurtingEventArgs ev)
    {
        if(ev.DamageHandler.Type == DamageType.Scp207 && ev.Player.UniqueRole.Contains("-207Prevent"))
        {
            ev.IsAllowed = false;
        }
        else
        {
            ev.IsAllowed = true;
        }
    }
    }

