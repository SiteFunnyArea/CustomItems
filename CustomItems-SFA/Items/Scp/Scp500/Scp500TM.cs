namespace CustomItems_SFA.Items;

using Exiled.API.Enums;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Player = Exiled.Events.Handlers.Player;



    [Exiled.API.Features.Attributes.CustomItem(ItemType.SCP500)]
    public class Scp500TM : CustomItem
    {
        public override uint Id { get; set; } = 2008;
        public override string Name { get; set; } = "SCP-500 TM";
        public override string Description { get; set; } = "Increases your health from 100 to 150.";
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
        
        Timing.CallDelayed(1.3f, () =>
        {
            Exiled.API.Features.Player p = ev.Player;
            ev.IsAllowed = false;
            ev.Player.RemoveItem(ev.Player.CurrentItem);

            RoleTypeId selRole = RoleTypeId.None;
            int count = 0;

            if(p.Role.Team == Team.ClassD)
            {
                selRole = RoleTypeId.ChaosConscript;
            }
            if (p.Role.Team == Team.Scientists)
            {
                selRole = RoleTypeId.FacilityGuard;
            }
            if (p.Role.Team == Team.ChaosInsurgency)
            {
                selRole = RoleTypeId.ChaosMarauder;
            }
            if (p.Role.Team == Team.FoundationForces)
            {
                selRole = RoleTypeId.NtfPrivate;
            }

            if(selRole != RoleTypeId.None)
            {
                foreach(Exiled.API.Features.Player pl in Exiled.API.Features.Player.Get(RoleTypeId.Spectator))
                {
                    if(count != 2)
                    {
                        pl.Role.Set(selRole);
                        pl.RoleManager.ServerSetRole(selRole,RoleChangeReason.Respawn);
                        pl.Position = p.Position;
                        count += 1;
                    }
                }
            }

            
        });


    }
}

