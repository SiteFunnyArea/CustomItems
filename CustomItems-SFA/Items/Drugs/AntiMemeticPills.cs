// -----------------------------------------------------------------------
// <copyright file="AntiMemeticPills.cs" company="Joker119">
// Copyright (c) Joker119. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace CustomItems.Items;

using System.Collections.Generic;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Roles;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;

/// <inheritdoc />
[CustomItem(ItemType.SCP500)]
public class AntiMemeticPills : CustomItem
{
    /// <inheritdoc/>
    public override uint Id { get; set; } = 4;

    /// <inheritdoc/>
    public override string Name { get; set; } = "Gamma-5 AM Pills";

    /// <inheritdoc/>
    public override string Description { get; set; } =
        "Drugs that make you forget things. If you use these while you are targeted by <color=#FFEA00>SCP-096</color>.<br>Which you will forget what he looks like.";

    /// <inheritdoc/>
    public override float Weight { get; set; } = 1f;

    /// <inheritdoc/>
    public override SpawnProperties? SpawnProperties { get; set; } = new()
    {
        DynamicSpawnPoints = new List<DynamicSpawnPoint>
        {
            new() { Chance = 100, Location = SpawnLocationType.Inside096 },
        },
    };

    /// <inheritdoc/>
    protected override void SubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsingItem += OnUsingItem;
        base.SubscribeEvents();
    }

    /// <inheritdoc/>
    protected override void UnsubscribeEvents()
    {
        Exiled.Events.Handlers.Player.UsingItem -= OnUsingItem;
        base.UnsubscribeEvents();
    }

    private void OnUsingItem(UsingItemEventArgs ev)
    {
        if (!Check(ev.Player.CurrentItem))
            return;

        IEnumerable<Player> scp096S = Player.Get(RoleTypeId.Scp096);

        Timing.CallDelayed(1f, () =>
        {
            foreach (Player scp in scp096S)
            {
                if (scp.Role is Scp096Role scp096)
                {
                    if (scp096.HasTarget(ev.Player))
                        scp096.RemoveTarget(ev.Player);
                }
            }

            ev.Player.EnableEffect<AmnesiaVision>(10f, true);
        });
    }
}