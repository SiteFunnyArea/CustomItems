namespace CustomItems_SFA.Items;

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



    [Exiled.API.Features.Attributes.CustomItem(ItemType.SCP500)]
    public class Scp500R : CustomItem
    {
        public override uint Id { get; set; } = 41;
        public override string Name { get; set; } = "SCP 500-R";
        public override string Description { get; set; } = "Gives you 20 seconds of damage reduction, making you lose less health than usual if you were to be injured.";
        public override float Weight { get; set; } = 1f;
        public override SpawnProperties? SpawnProperties { get; set; } = new()
        {
            Limit = 1,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
        {
            new()
            {
                Chance = 100,
                Location = SpawnLocationType.InsideLczArmory,
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

        Timing.CallDelayed(1.3f, () =>
        {
            Exiled.API.Features.Player p = ev.Player;

            Effect DR = new Effect(EffectType.DamageReduction, 20, 150, false, true);
            Effect BR = new Effect(EffectType.BodyshotReduction, 20, 150, false, true);

            p.EnableEffect(DR);
            p.EnableEffect(BR);

            ev.IsAllowed = false;
            ev.Player.RemoveItem(ev.Player.CurrentItem);
        });

    }
}

