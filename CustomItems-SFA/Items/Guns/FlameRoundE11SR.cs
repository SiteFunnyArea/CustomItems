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

/// <inheritdoc />
[CustomItem(ItemType.GunE11SR)]
public class FlameRoundE11SR : CustomWeapon
{
    private int Multiply { get; set; } = 0;
    /// <inheritdoc/>
    public override uint Id { get; set; } = 1751;

    /// <inheritdoc/>
    public override string Name { get; set; } = "Flame Round E-11 SR";

    /// <inheritdoc/>
    public override string Description { get; set; } = "This is a template item.";

    /// <inheritdoc/>
    public override float Weight { get; set; } = 3.25f;

    /// <inheritdoc/>
    public override byte ClipSize { get; set; } = 31;

    /// <inheritdoc/>
    public override bool ShouldMessageOnGban { get; } = true;

    /// <inheritdoc/>
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

    /// <inheritdoc />
    [YamlIgnore]
    public override AttachmentName[] Attachments { get; set; } = new[]
    {
        AttachmentName.HoloSight,
    };

    /// <summary>
    /// Gets or sets the amount of extra damage this weapon does, as a multiplier.
    /// </summary>

    /// <inheritdoc/>
    protected override void OnHurting(HurtingEventArgs ev)
    {
        if (ev.Attacker != ev.Player && ev.DamageHandler.Base is FirearmDamageHandler firearmDamageHandler && firearmDamageHandler.WeaponType == ev.Attacker.CurrentItem.Type)
        {
            if(Multiply <= 5)
            {
                Multiply += 1;
                ev.Player.EnableEffect(EffectType.Burned, 3 * Multiply);

            }
            else
            {
                Multiply = 5;
                ev.Player.EnableEffect(EffectType.Burned, 3 * Multiply);

            }
        }

    }
}