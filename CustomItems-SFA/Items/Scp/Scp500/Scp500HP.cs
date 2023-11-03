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



    [Exiled.API.Features.Attributes.CustomItem(ItemType.SCP500)]
    public class Scp500HP : CustomItem
    {
        public override uint Id { get; set; } = 39;
        public override string Name { get; set; } = "SCP-500 HP";
        public override string Description { get; set; } = "Increases your health from 100 to 150.";
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

        private void OnUsingItem(UsingItemEventArgs ev)
        {
        if (!Check(ev.Player.CurrentItem))
            return;
        
        Timing.CallDelayed(1.3f, () =>
        {
            Exiled.API.Features.Player p = ev.Player;
            ev.Player.MaxHealth += 50;
            ev.Player.Health = ev.Player.MaxHealth;
            ev.IsAllowed = false;
            ev.Player.RemoveItem(ev.Player.CurrentItem);

        });


    }
}

