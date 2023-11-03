// -----------------------------------------------------------------------
// <copyright file="SniperRifle.cs" company="Joker119">
// Copyright (c) Joker119. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace CustomItems.Items;

using System.Collections.Generic;
using System.ComponentModel;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Firearms.Attachments;
using PlayerStatsSystem;
using YamlDotNet.Serialization;

/// <inheritdoc />
[CustomItem(ItemType.GunFRMG0)]
public class OverclockedFRMG : CustomWeapon
{
    /// <inheritdoc/>
    public override uint Id { get; set; } = 200;

    /// <inheritdoc/>
    public override string Name { get; set; } = "Overclocked FRMG";

    /// <inheritdoc/>
    public override string Description { get; set; } = "An FRMG that is completely unstable. It has <color=FFEA00>extra ammo in the mag</color> and gives the user <color=FFEA00>SCP-1853 while holding it</color>.<br><color=FFEA00>This gun has been forged by the Serpents Hand Leader.</color>";

    /// <inheritdoc/>
    public override float Weight { get; set; } = 3.25f;

    /// <inheritdoc/>
    public override byte ClipSize { get; set; } = 150;

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
        AttachmentName.Foregrip,
        AttachmentName.MuzzleBrake,
        AttachmentName.HeavyStock,
        AttachmentName.DrumMagFMJ,

    };

    protected override void OnChanging(ChangingItemEventArgs ev)
    {
        if (ev.Player.ActiveEffects.Contains(ev.Player.GetEffect(EffectType.Scp1853)) && !Check(ev.Item))
        {
            //ev.Player.DisableEffect(EffectType.Scp1853);
        }

        if (Check(ev.Item))
        {
            //ev.Player.EnableEffect(EffectType.Scp1853);
        }
    }
}