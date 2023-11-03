namespace CustomItems_SFA.Items;

using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Components;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using Exiled.API.Features.Pickups.Projectiles;
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
        public override uint Id { get; set; } = 18;
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
                Chance = 0,
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
        //Room room = Room.FindParentRoom(ev.Projectile.GameObject);

        ExplosiveGrenade g = (ExplosiveGrenade)Item.Create(ItemType.GrenadeHE);
        g.FuseTime = 1;
        g.SpawnActive(ev.Projectile.Position, ev.Player);

        ExplosiveGrenade g2 = (ExplosiveGrenade)Item.Create(ItemType.GrenadeHE);
        g2.FuseTime = 1;
        g2.SpawnActive(ev.Projectile.Position, ev.Player);

        ev.IsAllowed = true;

    }
}

