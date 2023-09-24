namespace CustomItems_SFA.Items;

using Exiled.API.Enums;
using Exiled.API.Features;
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
    public class DisguisedAmmoBox : CustomItem
    {
        public override uint Id { get; set; } = 29;
        public override string Name { get; set; } = "Disguised Ammo Box";
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

        ev.Player.RemoveItem(ev.Player.CurrentItem);
        ev.Player.AddAmmo(AmmoType.Ammo44Cal, 4);
        ev.Player.AddAmmo(AmmoType.Nato9, 30);
        ev.Player.AddAmmo(AmmoType.Nato556, 20);
        ev.Player.AddAmmo(AmmoType.Nato762, 20);
        ev.Player.AddAmmo(AmmoType.Ammo12Gauge, 8);


    }
}

