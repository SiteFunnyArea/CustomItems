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
    public class Scp500IV : CustomItem
    {
        public override uint Id { get; set; } = 40;
        public override string Name { get; set; } = "SCP-500 IV";
        public override string Description { get; set; } = "Makes you invisible for a brief amount of time.";
        public override float Weight { get; set; } = 1f;
        public override SpawnProperties? SpawnProperties { get; set; } = new()
        {
            Limit = 1,
            StaticSpawnPoints = new List<StaticSpawnPoint>()
        {
            new()
            {
                Chance = 100,
                Name = "HczTestRoom",
                Position =new Vector3(2.9f, 0f, -4.4f),
            }
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
            p.EnableEffect(EffectType.Invisible, 15);
            ev.IsAllowed = false;
            ev.Player.RemoveItem(ev.Player.CurrentItem);
        });

    }
}

