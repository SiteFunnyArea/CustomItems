// -----------------------------------------------------------------------
// <copyright file="SniperRifle.cs" company="Joker119">
// Copyright (c) Joker119. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace CustomItems.Items;

using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Firearms.Attachments;
using PlayerStatsSystem;
using YamlDotNet.Serialization;

public enum Modes
{
    Burned,
    Sinkhole,
    Poison
}

/// <inheritdoc />
[CustomItem(ItemType.GunE11SR)]
public class ModifiedSerpentsRifle : CustomWeapon
{
    /// <inheritdoc/>
    public override uint Id { get; set; } = 204;

    /// <inheritdoc/>
    public override string Name { get; set; } = "Serpents Hand Rifle";

    /// <inheritdoc/>
    public override string Description { get; set; } = "This is a template item.";

    /// <inheritdoc/>
    public override float Weight { get; set; } = 3.25f;

    /// <inheritdoc/>
    public override byte ClipSize { get; set; } = 25;

    /// <inheritdoc/>
    public override bool ShouldMessageOnGban { get; } = true;

    public Modes selModes = Modes.Burned;

    [YamlIgnore]
    public override float Damage { get; set; }



    /// <inheritdoc/>
    public override SpawnProperties? SpawnProperties { get; set; } = new()
    {
        Limit = 1,
        StaticSpawnPoints = new List<StaticSpawnPoint>
        {
            new()
            {
                Name = "Surface",
                Chance = 0,
                Position = new UnityEngine.Vector3(39.108f, 1001.982f, -52.789f),
            },

        },
    };

    protected override void SubscribeEvents()
    {
        Exiled.Events.Handlers.Player.TogglingNoClip += TogglingNoClip;

        base.SubscribeEvents();
    }

    protected override void UnsubscribeEvents()
    {
        Exiled.Events.Handlers.Player.TogglingNoClip -= TogglingNoClip;

        base.UnsubscribeEvents();
    }

    protected override void OnHurting(HurtingEventArgs ev)
    {
        if (Check(ev.Attacker.CurrentItem))
        {

            if (selModes.Equals(Modes.Burned))
            {
                ev.Player.EnableEffect(EffectType.Burned,45);
            }
            else if (selModes.Equals(Modes.Sinkhole))
            {
                ev.Player.EnableEffect(EffectType.SinkHole,45);
            }
            else if (selModes.Equals(Modes.Poison))
            {
                ev.Player.EnableEffect(EffectType.Poisoned, 45);
            }
        }
    }

    /// <inheritdoc />
    [YamlIgnore]
    public override AttachmentName[] Attachments { get; set; } = new[]
    {
        AttachmentName.HoloSight,
    };

    public void TogglingNoClip(TogglingNoClipEventArgs ev)
    {
        //a player can toggle the fire mode only if:
        //1. his item is set
        //2. his item is part of the weapon config
        //3. he is not reloading / unloading (done to prevent de-sync)
        if (ev.Player.CurrentItem == null) return;
        if (!Check(ev.Player.CurrentItem)) return;
        if (ev.Player.IsReloading) return;
        if (ev.Player.CurrentItem.IsWeapon)
        {

            ev.IsAllowed = false; //prevent from going in noclip

            if (selModes.Equals(Modes.Burned))
            {
                selModes = Modes.Sinkhole;
            }
            else if (selModes.Equals(Modes.Sinkhole))
            {
                selModes = Modes.Poison;
            }
            else if (selModes.Equals(Modes.Poison))
            {
                selModes = Modes.Burned;
            }
            //WeaponData wd = Main.Singleton.WeaponMemory[e.Player.CurrentItem.Serial];
            //wd.FireMode = GetNextFireMode(e.Player.CurrentItem.Type, (int)wd.FireMode);

            string Hint = BuildHint(selModes);
            ev.Player.ShowHint(Hint);


        }


    }
    public string BuildHint(Modes FiringMode)
    {
        string UnformattedString = "<align=left>Firing Mode: {firemode}\\\\n";


        return UnformattedString.Replace("{firemode}", $"{FiringMode.ToString()}");
    }
}