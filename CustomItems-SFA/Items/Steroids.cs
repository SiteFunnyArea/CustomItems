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



    [Exiled.API.Features.Attributes.CustomItem(ItemType.Painkillers)]
    public class Steroids : CustomItem
    { 
        public override uint Id { get; set; } = 24;
        public override string Name { get; set; } = "Steroids";
        public override string Description { get; set; } = "Makes you fast... but with a catch, your life will suffer.";
        public override float Weight { get; set; } = 1f;
        public override SpawnProperties? SpawnProperties { get; set; } = new()
        {
            Limit = 1,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
        {
            new()
            {
                Chance = 0,
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

        private void OnUsingItem(UsedItemEventArgs ev)
        {
        if (!Check(ev.Player.CurrentItem))
            return;

        Exiled.API.Features.Player p = ev.Player;

        p.EnableEffect(EffectType.MovementBoost, 30);
        p.ChangeEffectIntensity(EffectType.MovementBoost, 100);

        p.EnableEffect(EffectType.CardiacArrest, 30);
        p.ChangeEffectIntensity(EffectType.CardiacArrest, 1);
    }
}

