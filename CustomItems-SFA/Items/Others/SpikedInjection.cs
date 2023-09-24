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



    [Exiled.API.Features.Attributes.CustomItem(ItemType.Adrenaline)]
    public class SpikedInjection : CustomItem
    {
        public override uint Id { get; set; } = 33;
        public override string Name { get; set; } = "Spiked Injection";
        public override string Description { get; set; } = "You explode, you die. The End.";
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
            Player.UsedItem += OnUsedItem;

            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Player.UsedItem -= OnUsedItem;

            base.UnsubscribeEvents();
        }

        private void OnUsedItem(UsedItemEventArgs ev)
        {
        
        if (!Check(ev.Item))
            return;

        ev.Player.Explode();
        ev.Player.Kill(DamageType.Explosion);

    }
}

