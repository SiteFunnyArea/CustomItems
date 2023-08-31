namespace CustomItems_SFA.Items;

using Exiled.API.Enums;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Player = Exiled.Events.Handlers.Player;



    [Exiled.API.Features.Attributes.CustomItem(ItemType.GrenadeHE)]
    public class InstantGrenade : CustomGrenade
{
        public override uint Id { get; set; } = 21;
        public override string Name { get; set; } = "Instant Grenade";
        public override string Description { get; set; } = "Once this item is thrown, if it hits anything, it will immediately explode.";
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
    public override bool ExplodeOnCollision { get; set; } = true;
    public override float FuseTime { get; set; } = 0f;

    protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            base.UnsubscribeEvents();
        }
    protected override void OnExploding(ExplodingGrenadeEventArgs ev)
    {
        ev.IsAllowed = false;
    }
}

