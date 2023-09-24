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
[CustomItem(ItemType.GunCOM18)]
public class Scp127HP : CustomWeapon
{
    /// <inheritdoc/>
    public override uint Id { get; set; } = 36;

    /// <inheritdoc/>
    public override string Name { get; set; } = "SCP 127-HP";

    /// <inheritdoc/>
    public override string Description { get; set; } = "A gun that will grant you HP whenever you shoot someone.";

    /// <inheritdoc/>
    public override float Weight { get; set; } = 3.25f;

    /// <inheritdoc/>
    public override byte ClipSize { get; set; } = 7;

    /// <inheritdoc/>
    public override bool ShouldMessageOnGban { get; } = true;

    /// <inheritdoc/>
    [YamlIgnore]
    public override float Damage { get; set; }

    /// <inheritdoc/>
    public override SpawnProperties? SpawnProperties { get; set; } = new()
    {
        Limit = 1,
        DynamicSpawnPoints = new List<DynamicSpawnPoint>
        {
            new()
            {
                Chance = 0,
                Location = SpawnLocationType.InsideHid,
            },
            new()
            {
                Chance = 0,
                Location = SpawnLocationType.InsideHczArmory,
            },
        },
    };

    /// <inheritdoc />
    [YamlIgnore]
    public override AttachmentName[] Attachments { get; set; } = new[]
    {
        AttachmentName.Laser,
        AttachmentName.ScopeSight,
    };

    /// <inheritdoc/>
    protected override void OnHurting(HurtingEventArgs ev)
    {
        if (ev.Attacker != ev.Player && ev.DamageHandler.Base is FirearmDamageHandler firearmDamageHandler && firearmDamageHandler.WeaponType == ev.Attacker.CurrentItem.Type)
            ev.Attacker.Heal(10);
    }
}