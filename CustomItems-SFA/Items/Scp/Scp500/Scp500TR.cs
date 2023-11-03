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
    public class Scp500TR : CustomItem
    {
        public override uint Id { get; set; } = 43;
        public override string Name { get; set; } = "SCP-500 TR";
        public override string Description { get; set; } = "Switches you to the other team when taken.";
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

            if (p.Role.Team == PlayerRoles.Team.ChaosInsurgency)
            {
                if (p.Role.Type == PlayerRoles.RoleTypeId.ChaosConscript)
                {
                    int r = UnityEngine.Random.Range(1, 100);
                    if (r <= 50)
                    {
                        ev.Player.Role.Set(PlayerRoles.RoleTypeId.NtfPrivate, PlayerRoles.RoleSpawnFlags.None);
                        ev.Player.RoleManager.ServerSetRole(PlayerRoles.RoleTypeId.NtfPrivate, PlayerRoles.RoleChangeReason.Respawn, PlayerRoles.RoleSpawnFlags.None);
                    }
                    else if (r > 50)
                    {
                        ev.Player.Role.Set(PlayerRoles.RoleTypeId.FacilityGuard, PlayerRoles.RoleSpawnFlags.None);
                        ev.Player.RoleManager.ServerSetRole(PlayerRoles.RoleTypeId.FacilityGuard, PlayerRoles.RoleChangeReason.Respawn, PlayerRoles.RoleSpawnFlags.None);
                    }
                }
                else if (p.Role.Type == PlayerRoles.RoleTypeId.ChaosMarauder)
                {
                    ev.Player.Role.Set(PlayerRoles.RoleTypeId.NtfCaptain, PlayerRoles.RoleSpawnFlags.None);
                    ev.Player.RoleManager.ServerSetRole(PlayerRoles.RoleTypeId.NtfCaptain, PlayerRoles.RoleChangeReason.Respawn, PlayerRoles.RoleSpawnFlags.None);
                }
                else if (p.Role.Type == PlayerRoles.RoleTypeId.ChaosRepressor)
                {
                    ev.Player.Role.Set(PlayerRoles.RoleTypeId.NtfSergeant, PlayerRoles.RoleSpawnFlags.None);
                    ev.Player.RoleManager.ServerSetRole(PlayerRoles.RoleTypeId.NtfSergeant, PlayerRoles.RoleChangeReason.Respawn, PlayerRoles.RoleSpawnFlags.None);
                }
                else if (p.Role.Type == PlayerRoles.RoleTypeId.ChaosRifleman)
                {
                    ev.Player.Role.Set(PlayerRoles.RoleTypeId.NtfSpecialist, PlayerRoles.RoleSpawnFlags.None);
                    ev.Player.RoleManager.ServerSetRole(PlayerRoles.RoleTypeId.NtfSpecialist, PlayerRoles.RoleChangeReason.Respawn, PlayerRoles.RoleSpawnFlags.None);

                }
            }
            else if (p.Role.Team == PlayerRoles.Team.FoundationForces)
            {
                if (p.Role.Type == PlayerRoles.RoleTypeId.FacilityGuard)
                {
                    ev.Player.Role.Set(PlayerRoles.RoleTypeId.ChaosConscript, PlayerRoles.RoleSpawnFlags.None);
                    ev.Player.RoleManager.ServerSetRole(PlayerRoles.RoleTypeId.ChaosConscript, PlayerRoles.RoleChangeReason.Respawn, PlayerRoles.RoleSpawnFlags.None);

                }
                else if (p.Role.Type == PlayerRoles.RoleTypeId.NtfCaptain)
                {
                    ev.Player.Role.Set(PlayerRoles.RoleTypeId.ChaosMarauder, PlayerRoles.RoleSpawnFlags.None);
                    ev.Player.RoleManager.ServerSetRole(PlayerRoles.RoleTypeId.ChaosMarauder, PlayerRoles.RoleChangeReason.Respawn, PlayerRoles.RoleSpawnFlags.None);

                }
                else if (p.Role.Type == PlayerRoles.RoleTypeId.NtfPrivate)
                {
                    ev.Player.Role.Set(PlayerRoles.RoleTypeId.ChaosConscript, PlayerRoles.RoleSpawnFlags.None);
                    ev.Player.RoleManager.ServerSetRole(PlayerRoles.RoleTypeId.ChaosConscript, PlayerRoles.RoleChangeReason.Respawn, PlayerRoles.RoleSpawnFlags.None);

                }
                else if (p.Role.Type == PlayerRoles.RoleTypeId.NtfSergeant)
                {
                    ev.Player.Role.Set(PlayerRoles.RoleTypeId.ChaosRepressor, PlayerRoles.RoleSpawnFlags.None);
                    ev.Player.RoleManager.ServerSetRole(PlayerRoles.RoleTypeId.ChaosRepressor, PlayerRoles.RoleChangeReason.Respawn, PlayerRoles.RoleSpawnFlags.None);

                }
                else if (p.Role.Type == PlayerRoles.RoleTypeId.NtfSpecialist)
                {
                    ev.Player.Role.Set(PlayerRoles.RoleTypeId.ChaosRifleman, PlayerRoles.RoleSpawnFlags.None);
                    ev.Player.RoleManager.ServerSetRole(PlayerRoles.RoleTypeId.ChaosRifleman, PlayerRoles.RoleChangeReason.Respawn, PlayerRoles.RoleSpawnFlags.None);

                }
            }
            else if (p.Role.Team == PlayerRoles.Team.ClassD)
            {
                ev.Player.Role.Set(PlayerRoles.RoleTypeId.Scientist, PlayerRoles.RoleSpawnFlags.None);
                ev.Player.RoleManager.ServerSetRole(PlayerRoles.RoleTypeId.Scientist, PlayerRoles.RoleChangeReason.Respawn, PlayerRoles.RoleSpawnFlags.None);

            }
            else if (p.Role.Team == PlayerRoles.Team.Scientists)
            {
                ev.Player.Role.Set(PlayerRoles.RoleTypeId.ClassD, PlayerRoles.RoleSpawnFlags.None);
                ev.Player.RoleManager.ServerSetRole(PlayerRoles.RoleTypeId.ClassD, PlayerRoles.RoleChangeReason.Respawn, PlayerRoles.RoleSpawnFlags.None);
            }
            else
            {
                ev.Player.Explode();
                ev.Player.Kill(DamageType.Explosion);
            }
        });


    }
}

