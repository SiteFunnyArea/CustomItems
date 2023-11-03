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
    public class Scp500SH : CustomItem
    {
        public override uint Id { get; set; } = 42;
        public override string Name { get; set; } = "SCP-500 SH";
        public override string Description { get; set; } = "Gives you 50 AHP when taken.";
        public override float Weight { get; set; } = 1f;
        public override SpawnProperties? SpawnProperties { get; set; } = new()
        {

            Limit = 1,
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
            p.AddAhp(75, 75, 0);
            Effect DR = new(EffectType.DamageReduction, 0, 15);
            p.EnableEffect(DR);

            ev.IsAllowed = false;
            ev.Player.RemoveItem(ev.Player.CurrentItem);

        });
    }
}

