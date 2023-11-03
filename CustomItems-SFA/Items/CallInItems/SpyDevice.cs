namespace CustomItems_SFA.Items;

using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using CustomRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YamlDotNet.Serialization;
using Player = Exiled.Events.Handlers.Player;



public enum Roles
{
    CurrentRole,
    ClassD,
    FacilityGuard,
    Scientist,

}
    [Exiled.API.Features.Attributes.CustomItem(ItemType.KeycardZoneManager)]
   
    public class SpyDevice : CustomItem
    {
        public override uint Id { get; set; } = 4500;
        public override string Name { get; set; } = "Spy Device";
        public override string Description { get; set; } = "This is a template item.";
        public override float Weight { get; set; } = 1f;
    [YamlIgnore]
        public Roles role = Roles.CurrentRole;
    [YamlIgnore]
    public string Display = "your Current Role";

    [YamlIgnore]
    public bool CooldownEnabled = false;

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
            Player.TogglingNoClip += NoClip;

            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Player.TogglingNoClip -= NoClip;

            base.UnsubscribeEvents();
        }

    protected override void OnDropping(DroppingItemEventArgs ev)
    {
        if (!Check(ev.Item))
            return;

        if (CustomRole.Get(2009) != null && CustomRole.Get(2009).TrackedPlayers.Contains(ev.Player))
        {
            if (CooldownEnabled)
            {
                ev.Player.ShowHint("<align=center>Spy Device on cooldown.</align>", 5);
                ev.IsAllowed = false;
                return;
            }

            if (role == Roles.CurrentRole)
            {
                ev.Player.ChangeAppearance(PlayerRoles.RoleTypeId.ChaosConscript, true);
                ev.Player.CustomInfo = ev.Player.CustomName + "\n" + "Spy";
            }
            else if (role == Roles.ClassD)
            {
                ev.Player.ChangeAppearance(PlayerRoles.RoleTypeId.ClassD, true);
                ev.Player.CustomInfo = ev.Player.CustomName + "\n" + "Class D";
            }
            else if (role == Roles.FacilityGuard)
            {
                ev.Player.ChangeAppearance(PlayerRoles.RoleTypeId.FacilityGuard, true);
                ev.Player.CustomInfo = ev.Player.CustomName + "\n" + "Facility Guard";
            }
            else if (role == Roles.Scientist)
            {
                ev.Player.ChangeAppearance(PlayerRoles.RoleTypeId.Scientist, true);
                ev.Player.CustomInfo = ev.Player.CustomName + "\n" + "Scientist";
            }

            if(role != Roles.CurrentRole)
            {
                ev.Player.ShowHint("Successfully disguised as " + Display + "!", 5);
            }
            else
            { 
                ev.Player.ShowHint("<align=center>Successfully undisguised.</align>", 5);
            }

            CooldownEnabled = true;
            ev.IsAllowed = false;
            Timing.CallDelayed(30f, () =>
            {
                CooldownEnabled = false;
                ev.Player.ShowHint("<align=center>You can now use your Spy Device.</align>", 5);

            });
        }
        base.OnDropping(ev);
    }

    public void NoClip(TogglingNoClipEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem))
                return;

        //ev.Player.RemoveItem(ev.Player.CurrentItem);
        if (CustomRole.Get(2009) != null && CustomRole.Get(2009).TrackedPlayers.Contains(ev.Player))

            {
                if (role == Roles.CurrentRole)
                {
                    role = Roles.ClassD;
                    Display = "a Class D";
            }
            else if (role == Roles.ClassD)
                {
                    role = Roles.FacilityGuard;
                    Display = "a Facility Guard";

            }
            else if (role == Roles.FacilityGuard)
                {
                    role = Roles.Scientist;
                    Display = "a Scientist";
            }
                else if (role == Roles.Scientist)
                {
                    role = Roles.CurrentRole;
                Display = "your Current Role";
            }
                
                ev.Player.ShowHint($"<align=left>You will be disguised as {Display}.</align>", 5);
        }
        }

    }

