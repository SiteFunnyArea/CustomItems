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
public class CounterInsurgencySniperRifle : CustomWeapon
{
    /// <inheritdoc/>
    public override uint Id { get; set; } = 20;

    /// <inheritdoc/>
    public override string Name { get; set; } = "Counter-Insurgency Sniper Rifle";

    /// <inheritdoc/>
    public override string Description { get; set; } = "This Modified E-11 Rifle specializes in terminating rogue personnel and Chaos Insurgency. Use this to terminate any members not loyal to the Foundation and blind any SCPs.\n";

    /// <inheritdoc/>
    public override float Weight { get; set; } = 3.25f;

    /// <inheritdoc/>
    public override byte ClipSize { get; set; } = 2;

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
                Chance = 100,
                Position = new UnityEngine.Vector3(39.108f, 1001.982f, -52.789f),
            },

        },
    };

    /// <inheritdoc />
    [YamlIgnore]
    public override AttachmentName[] Attachments { get; set; } = new[]
    {
        AttachmentName.StandardBarrel,
        AttachmentName.ScopeSight,
    };

    /// <summary>
    /// Gets or sets the amount of extra damage this weapon does, as a multiplier.
    /// </summary>
    [Description("The amount of extra damage this weapon does, as a multiplier.")]
    public float DamageMultiplier { get; set; } = 4.5f;

    /// <inheritdoc/>
    protected override void OnHurting(HurtingEventArgs ev)
    {
        if (ev.Attacker != ev.Player && ev.DamageHandler.Base is FirearmDamageHandler firearmDamageHandler && firearmDamageHandler.WeaponType == ev.Attacker.CurrentItem.Type)
            ev.Amount *= DamageMultiplier;
            ev.Player.EnableEffect(EffectType.Flashed, 4f);
    }
}