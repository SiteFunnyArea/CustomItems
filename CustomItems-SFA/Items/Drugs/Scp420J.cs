namespace CustomItems_SFA.Items;

using CustomPlayerEffects;
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



    [Exiled.API.Features.Attributes.CustomItem(ItemType.SCP244b)]
    public class Scp420J : CustomItem
    {
        public override uint Id { get; set; } = 9;
        public override string Name { get; set; } = "SCP-420 J";
        public override string Description { get; set; } = "Das sum good sh//.";
        public override float Weight { get; set; } = 1f;
        public override SpawnProperties? SpawnProperties { get; set; } = new()
        {
            Limit = 1,
            StaticSpawnPoints = new List<StaticSpawnPoint>()
        {
            new()
            {
                Chance = 0,
                Name = "HczTestRoom",
                Position =new Vector3(2.9f, 0f, -4.4f),
            }
        },
        };

        protected override void SubscribeEvents()
        {
            Player.UsingItem += OnUsedItem;

            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Player.UsingItem -= OnUsedItem;

            base.UnsubscribeEvents();
        }

    private void OnUsedItem(UsingItemEventArgs ev)
    {
        if (!Check(ev.Item))
            return;
        ev.Player.RemoveItem(ev.Player.CurrentItem);

        Exiled.API.Features.Player p = ev.Player;
        p.EnableEffect(EffectType.Deafened, 30);
        p.EnableEffect(EffectType.Disabled, 30);
        p.EnableEffect(EffectType.Hemorrhage, 30);
        p.EnableEffect(EffectType.Exhausted, 30);
        p.EnableEffect(EffectType.SinkHole, 30);
    }
}

