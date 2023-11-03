namespace CustomItems_SFA.Items;

using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Components;
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
    public class HolyShitGrenade : CustomGrenade
{
        public override uint Id { get; set; } = 2013;
        public override string Name { get; set; } = "Holy Shit Grenade";
        public override string Description { get; set; } = "DO NOT THROW IT, YOU WILL DIE.";
        public override float Weight { get; set; } = 1.15f;
    public int AmountOfGrenades { get; set; } = 50;
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
        ev.IsAllowed = true;
        //Room room = Room.FindParentRoom(ev.Projectile.GameObject);

        for(int i = 0; i < AmountOfGrenades; i++)
        {
            if(ev.Player.IsDead)
                break;
            
            Projectile Pro = ev.Player.ThrowGrenade(ProjectileType.FragGrenade).Projectile;

            Pro.GameObject.AddComponent<CollisionHandler>().Init(ev.Player.GameObject, Pro.Base);
        }

    }
}

