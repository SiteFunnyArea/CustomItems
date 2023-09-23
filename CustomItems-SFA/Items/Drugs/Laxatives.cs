namespace CustomItems_SFA.Items;

using Exiled.API.Enums;
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



    [Exiled.API.Features.Attributes.CustomItem(ItemType.Painkillers)]
    public class Laxatives : CustomItem
    {
        public override uint Id { get; set; } = 20;
        public override string Name { get; set; } = "Laxatives";
        public override string Description { get; set; } = "Gives you a slight movement boost... but you have 10 seconds to find a toilet.";
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

        ev.Player.EnableEffect(EffectType.MovementBoost, 10);
        ev.Player.ChangeEffectIntensity(EffectType.MovementBoost, 75);

        ev.Player.RemoveItem(ev.Player.CurrentItem);

        Timing.CallDelayed(10f, () => { 
            ev.Player.PlaceTantrum(true);
            ev.Player.DisableEffect(EffectType.MovementBoost);
        });

    }
}

