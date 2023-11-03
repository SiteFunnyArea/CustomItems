namespace CustomItems_SFA.Items;

using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
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



[Exiled.API.Features.Attributes.CustomItem(ItemType.Jailbird)]
public class StandardStunBaton : CustomItem
{
    public override uint Id { get; set; } = 710;
    public override string Name { get; set; } = "Standard Stun Baton";
    public override string Description { get; set; } = "This is a template item.";
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
        Player.UsingItem += OnUsingItem;
        Player.Hurting += OnHarm;

        base.SubscribeEvents();
    }

    protected override void UnsubscribeEvents()
    {
        Player.UsingItem -= OnUsingItem;
        Player.Hurting -= OnHarm;

        base.UnsubscribeEvents();
    }

    public void OnHarm(HurtingEventArgs args)
    {
        if (args.Attacker == null) return;
        if (!Check(args.Attacker.CurrentItem)) return;
        if(args.Attacker == args.Player) return;
        if (args.Attacker.Role.Team == args.Player.Role.Team) return;


        args.Player.EnableEffect(EffectType.Disabled, 8f);
        args.Player.EnableEffect(EffectType.Blinded,8f);
        args.DamageHandler.Damage = 5;
    }

    private void OnUsingItem(UsingItemEventArgs ev)
    {
        if (!Check(ev.Player.CurrentItem))
            return;

        if (ev.Player.CurrentItem is Jailbird jb)
        {
            jb.TotalCharges = 0;
        }
    }
}

