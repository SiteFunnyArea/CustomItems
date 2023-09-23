namespace CustomItems_SFA.Items;

using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Player = Exiled.Events.Handlers.Player;



    [Exiled.API.Features.Attributes.CustomItem(ItemType.GrenadeHE)]
    public class SuperChargedGrenade : CustomGrenade
{
        public override uint Id { get; set; } = 5420;
        public override string Name { get; set; } = "Super Charged Grenade";
        public override string Description { get; set; } = "test";
        public override float Weight { get; set; } = 1.15f;
    public override SpawnProperties? SpawnProperties { get; set; } = new()
    {
        Limit = 1,
        DynamicSpawnPoints = new List<DynamicSpawnPoint>
        {
            new()
            {
                Chance = 100f,
                Location = SpawnLocationType.InsideNukeArmory,
            },
        },
    };

    public override bool ExplodeOnCollision { get; set; } = false;
    public override float FuseTime { get; set; } = 3f;

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
        ev.IsAllowed = true;
        Room room = Room.FindParentRoom(ev.Projectile.GameObject);
        ev.Projectile.Explode();
        ev.Projectile.Explode();
    }
}

