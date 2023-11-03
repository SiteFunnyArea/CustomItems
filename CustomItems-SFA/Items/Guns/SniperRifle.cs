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
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Firearms.Attachments;
using PlayerStatsSystem;
using YamlDotNet.Serialization;

/// <inheritdoc />
[CustomItem(ItemType.GunE11SR)]
public class SniperRifle : CustomWeapon
{
    /// <inheritdoc/>
    public override uint Id { get; set; } = 25;

    /// <inheritdoc/>
    public override string Name { get; set; } = "Prototype Epsilon-11 SR";

    /// <inheritdoc/>
    public override string Description { get; set; } = "This modified E11SR is a <color=#FFEA00>Sniper Rifle</color> that can probably one shot someone.";

    /// <inheritdoc/>
    public override float Weight { get; set; } = 3.25f;

    /// <inheritdoc/>
    public override byte ClipSize { get; set; } = 1;

    /// <inheritdoc/>
    public override bool ShouldMessageOnGban { get; } = true;

    /// <inheritdoc/>
    [YamlIgnore]
    public override float Damage { get; set; }

    public override Pickup? Spawn(float x, float y, float z, Item item)
    {
        x = Room.Get(RoomType.LczArmory).Position.x + 2.5f;
        y = Room.Get(RoomType.LczArmory).Position.y + 4.8f;
        z = Room.Get(RoomType.LczArmory).Position.z - 3.7f;
        PluginAPI.Core.Log.Debug(Room.Get(RoomType.LczArmory).Position.ToString());
        return base.Spawn(x, y, z, item);
    }

    public override Pickup? Spawn(float x, float y, float z)
    {
        x = Room.Get(RoomType.LczArmory).Position.x + 2.5f;
        y = Room.Get(RoomType.LczArmory).Position.y + 4.8f;
        z = Room.Get(RoomType.LczArmory).Position.z - 3.7f;
        PluginAPI.Core.Log.Debug(Room.Get(RoomType.LczArmory).Position.ToString());
        return base.Spawn(x, y, z);
    }
    /// <inheritdoc/>
    public override SpawnProperties? SpawnProperties { get; set; } = new()
    {
        Limit = 1,
        DynamicSpawnPoints = new List<DynamicSpawnPoint>
        {
            new()
            {
                Chance = 100,
                Location = SpawnLocationType.InsideHid,
            },
        },
    };

    /// <inheritdoc />
    [YamlIgnore]
    public override AttachmentName[] Attachments { get; set; } = new[]
    {
        AttachmentName.ExtendedBarrel,
        AttachmentName.ScopeSight,
    };

    /// <summary>
    /// Gets or sets the amount of extra damage this weapon does, as a multiplier.
    /// </summary>
    [Description("The amount of extra damage this weapon does, as a multiplier.")]
    public float DamageMultiplier { get; set; } = 7.5f;

    /// <inheritdoc/>
    protected override void OnHurting(HurtingEventArgs ev)
    {
        if (ev.Attacker != ev.Player && ev.DamageHandler.Base is FirearmDamageHandler firearmDamageHandler && firearmDamageHandler.WeaponType == ev.Attacker.CurrentItem.Type)
            ev.Amount *= DamageMultiplier;
    }
}