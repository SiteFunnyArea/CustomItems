﻿namespace CustomItems_SFA.Items;

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
public class EnhancedAdrenaline : CustomItem
{
    public override uint Id { get; set; } = 5;
    public override string Name { get; set; } = "Enhanced Adrenaline";
    public override string Description { get; set; } = "This is a template item.";
    public override float Weight { get; set; } = 1f;
    public override SpawnProperties? SpawnProperties { get; set; } = new()
    {
        Limit = 1,
        DynamicSpawnPoints = new List<DynamicSpawnPoint>
        {
            new()
            {
                Chance = 100,
                Location = SpawnLocationType.InsideGateB,
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

        Timing.CallDelayed(1.95f, () =>
        {
            ev.Player.AddAhp(75);
            ev.Player.EnableEffect(EffectType.MovementBoost);
            ev.Player.ChangeEffectIntensity(EffectType.MovementBoost, 10);

            //ev.Player.EnableEffect(EffectType.Burned);
            //ev.Player.ChangeEffectIntensity(EffectType.Burned, 1);

            ev.Player.RemoveItem(ev.Player.CurrentItem);

            ev.Player.IsUsingStamina = false;
            Timing.CallDelayed(45f, () =>
            {
                ev.Player.IsUsingStamina = true;
                ev.Player.DisableEffect(EffectType.MovementBoost);
                ev.Player.DisableEffect(EffectType.Burned);
            });
        });
    }
}