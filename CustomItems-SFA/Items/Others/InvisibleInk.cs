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



    [Exiled.API.Features.Attributes.CustomItem(ItemType.SCP1853)]
    public class InvisibleInk : CustomItem
    {
        public override uint Id { get; set; } = 30;
        public override string Name { get; set; } = "Invisible Ink";
        public override string Description { get; set; } = "Makes everything invisible!<br>(FINE PRINT: Will cause your hands to fall off.)";
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

        Exiled.API.Features.Player p = ev.Player;

        p.EnableEffect(EffectType.Invisible);
        p.ChangeEffectIntensity(EffectType.Invisible, 1);

        p.EnableEffect(EffectType.SeveredHands);
        p.ChangeEffectIntensity(EffectType.SeveredHands, 1);
    }
}

