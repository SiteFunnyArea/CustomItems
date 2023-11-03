// -----------------------------------------------------------------------
// <copyright file="TranquilizerGun.cs" company="Joker119">
// Copyright (c) Joker119. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace CustomItems.Items;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pools;
using Exiled.API.Features.Roles;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Interfaces;
using Exiled.Events.EventArgs.Player;

using MEC;
using Mirror;
using PlayerRoles;
using PlayerStatsSystem;
using UnityEngine;
using Ragdoll = Exiled.API.Features.Ragdoll;
using Random = UnityEngine.Random;

/// <inheritdoc />
[CustomItem(ItemType.GunRevolver)]
public class TranquilizerGun : CustomWeapon
{
    private readonly Dictionary<Player, float> tranquilizedPlayers = new();
    private readonly List<Player> activeTranqs = new();
    private readonly List<Player> playersWhoRecievedAmmo = new();
    private readonly List<Player> CooldownPlayers = new();

    /// <inheritdoc/>
    public override uint Id { get; set; } = 26;

    /// <inheritdoc/>
    public override string Name { get; set; } = "Standard Stun Gun";

    /// <inheritdoc/>
    public override string Description { get; set; } = "This stun gun fires <color=#FFEA00>non-lethal tranq rounds</color> at any target shot at. It has a chance to affect <color=#FFEA00>SCPs</color> as well.";

    /// <inheritdoc/>
    public override float Weight { get; set; } = 1.55f;

    /// <inheritdoc />
    public override SpawnProperties? SpawnProperties { get; set; } = new()
    {
        Limit = 1,
        DynamicSpawnPoints = new List<DynamicSpawnPoint>
        {
            new()
            {
                Chance = 100,
                Location = SpawnLocationType.InsideGr18,
            },
        },
    };

    /// <inheritdoc/>
    public override byte ClipSize { get; set; } = 1;
    /// <inheritdoc/>
    public override float Damage { get; set; } = 5;
    public float Cooldown { get; set; } = 7;
    public float CooldownElapsed { get; set; } = 0;
    /// <summary>
    /// Gets or sets a value indicating whether or not SCPs should be resistant to tranquilizers. (Being resistant gives them a chance to not be tranquilized when shot).
    /// </summary>
    [Description("Whether or not SCPs should be resistant to tranquilizers. (Being resistant gives them a chance to not be tranquilized when shot).")]
    public bool ResistantScps { get; set; } = true;

    /// <summary>
    /// Gets or sets the amount of time a successful tranquilization lasts for.
    /// </summary>
    [Description("The amount of time a successful tranquilization lasts for.")]
    public float Duration { get; set; } = 5f;

    /// <summary>
    /// Gets or sets the exponential modifier used to determine how much time is removed from the effect, everytime a player is tranquilized, they gain a resistance to further tranquilizations, reducing the duration of future effects.
    /// </summary>
    [Description("Everytime a player is tranquilized, they gain a resistance to further tranquilizations, reducing the duration of future effects. This number signifies the exponential modifier used to determine how much time is removed from the effect.")]
    public float ResistanceModifier { get; set; } = 1.2f;

    /// <summary>
    /// Gets or sets a value indicating how often player resistances are reduced.
    /// </summary>
    [Description("How often the plugin should reduce the resistance amount for players, in seconds.")]
    public float ResistanceFalloffDelay { get; set; } = 120f;

    /// <summary>
    /// Gets or sets a value indicating whether or not tranquilized targets should drop all of their items.
    /// </summary>
    [Description("Whether or not tranquilized targets should drop all of their items.")]
    public bool DropItems { get; set; } = true;

    /// <summary>
    /// Gets or sets the percent chance an SCP will resist being tranquilized. This has no effect if ResistantScps is false.
    /// </summary>
    [Description("The percent chance an SCP will resist being tranquilized. This has no effect if ResistantScps is false.")]
    public int ScpResistChance { get; set; } = 40;

    /// <inheritdoc/>
    protected override void UnsubscribeEvents()
    {
        Exiled.Events.Handlers.Player.PickingUpItem -= OnDeniableEvent;
        Exiled.Events.Handlers.Player.ChangingItem -= OnDeniableEvent;
        Exiled.Events.Handlers.Scp049.StartingRecall -= OnDeniableEvent;
        Exiled.Events.Handlers.Scp106.Teleporting -= OnDeniableEvent;
        Exiled.Events.Handlers.Scp096.Charging -= OnDeniableEvent;
        Exiled.Events.Handlers.Scp096.Enraging -= OnDeniableEvent;
        Exiled.Events.Handlers.Scp096.AddingTarget -= OnDeniableEvent;
        Exiled.Events.Handlers.Scp939.PlacingAmnesticCloud -= OnDeniableEvent;
        Exiled.Events.Handlers.Player.VoiceChatting -= OnDeniableEvent;
        activeTranqs.Clear();
        tranquilizedPlayers.Clear();
        playersWhoRecievedAmmo.Clear();
        CooldownPlayers.Clear();

        Timing.KillCoroutines($"{nameof(TranquilizerGun)}-{Id}-reducer");
        base.UnsubscribeEvents();
    }

    /// <inheritdoc/>
    protected override void SubscribeEvents()
    {
        Timing.RunCoroutine(ReduceResistances(), $"{nameof(TranquilizerGun)}-{Id}-reducer");
        Exiled.Events.Handlers.Player.PickingUpItem += OnDeniableEvent;
        Exiled.Events.Handlers.Player.ChangingItem += OnDeniableEvent;
        Exiled.Events.Handlers.Scp049.StartingRecall += OnDeniableEvent;
        Exiled.Events.Handlers.Scp106.Teleporting += OnDeniableEvent;
        Exiled.Events.Handlers.Scp096.Charging += OnDeniableEvent;
        Exiled.Events.Handlers.Scp096.Enraging += OnDeniableEvent;
        Exiled.Events.Handlers.Scp096.AddingTarget += OnDeniableEvent;
        Exiled.Events.Handlers.Scp939.PlacingAmnesticCloud += OnDeniableEvent;
        Exiled.Events.Handlers.Player.VoiceChatting += OnDeniableEvent;
        base.SubscribeEvents();
    }
    protected override void OnPickingUp(PickingUpItemEventArgs ev)
    {
        if (!Check(ev.Pickup))
            return;
        if (playersWhoRecievedAmmo.Contains(ev.Player))
            return;

        ev.Player.AddAmmo(AmmoType.Ammo44Cal, 6);
        playersWhoRecievedAmmo.Add(ev.Player);

        base.OnPickingUp(ev);
    }

    protected override void OnShooting(ShootingEventArgs ev)
    {
        if (!Check(ev.Player)) return;

        if (CooldownPlayers.Contains(ev.Player))
        {
            if (!ev.Player.HasHint)
            {
                Hint h = new Hint($"<i>You must wait {Cooldown - CooldownElapsed} seconds to shoot.</i>\r\n", 7);
                ev.Player.ShowHint(h);
            }
            ev.IsAllowed = false;
            return;
        }


        base.OnShooting(ev);
    }

    /// <inheritdoc/>
    protected override void OnHurting(HurtingEventArgs ev)
    {
        base.OnHurting(ev);

        if (ev.Attacker == ev.Player)
            return;

        if (ev.Player.Role.Team == Team.SCPs)
        {
            int r = Random.Range(1, 101);
            Log.Debug($"{Name}: SCP roll: {r} (must be greater than {ScpResistChance})");
            if (r <= ScpResistChance)
            {
                Log.Debug($"{Name}: {r} is too low, no tranq.");
                return;
            }
        }

        float duration = Duration;

        if (!tranquilizedPlayers.TryGetValue(ev.Player, out _))
        {
            CooldownPlayers.Add(ev.Attacker);
            Timing.CallDelayed(Cooldown, () =>
            {
                CooldownPlayers.Remove(ev.Attacker);
            });
            tranquilizedPlayers.Add(ev.Player, 1);
        }
            


        tranquilizedPlayers[ev.Player] *= ResistanceModifier;
        Log.Debug($"{Name}: Resistance Duration Mod: {tranquilizedPlayers[ev.Player]}");

        duration -= tranquilizedPlayers[ev.Player];
        Log.Debug($"{Name}: Duration: {duration}");

        if (duration > 0f)
            Timing.RunCoroutine(DoTranquilize(ev.Player, duration));
    }

    private IEnumerator<float> DoTranquilize(Player player, float duration)
    {
        activeTranqs.Add(player);
        Vector3 oldPosition = player.Position;
        Item previousItem = player.CurrentItem;
        Vector3 previousScale = player.Scale;
        float newHealth = player.Health - Damage;
        List<StatusEffectBase> activeEffects = ListPool<StatusEffectBase>.Pool.Get();
        player.CurrentItem = null;
        CooldownElapsed++;

        if (newHealth <= 0)
            yield break;

        activeEffects.AddRange(player.ActiveEffects.Where(effect => effect.IsEnabled));

        try
        {
            if (DropItems)
            {
                if (player.Items.Count > 0)
                {
                    //foreach (Item item in player.Items.ToList())
                    //{
                    //     if (TryGet(item, out CustomItem? customItem))
                    //     {
                    //          customItem?.Spawn(player.Position, item, player);
                    //          player.RemoveItem(item);
                    //      }
                    //   }
                    //
                    player.DropHeldItem();
                }
            }
        }
        catch (Exception e)
        {
            Log.Error($"{nameof(DoTranquilize)}: {e}");
        }

        Ragdoll? ragdoll = null;
        if (player.Role.Type != RoleTypeId.Scp106)
            ragdoll = Ragdoll.CreateAndSpawn(player.Role, player.DisplayNickname, "Tranquilized", player.Position, player.ReferenceHub.PlayerCameraReference.rotation, player);

        if (player.Role is Scp096Role scp)
            scp.RageManager.ServerEndEnrage();

        try
        {
            player.EnableEffect<Invisible>(duration);
            player.Scale = Vector3.one * 0.2f;
            player.Health = newHealth;
            player.IsGodModeEnabled = true;

            player.EnableEffect<AmnesiaVision>(duration);
            player.EnableEffect<AmnesiaItems>(duration);
            player.EnableEffect<Ensnared>(duration);
        }
        catch (Exception e)
        {
            Log.Error(e);
        }

        yield return Timing.WaitForSeconds(duration);

        try
        {
            if (ragdoll != null)
                NetworkServer.Destroy(ragdoll.GameObject);

            if (player.GameObject == null)
                yield break;

            newHealth = player.Health;

            player.IsGodModeEnabled = false;
            player.Scale = previousScale;
            player.Health = newHealth;

            CooldownElapsed = 0;

            if (!DropItems)
                player.CurrentItem = previousItem;

            foreach (StatusEffectBase effect in activeEffects.Where(effect => (effect.Duration - duration) > 0))
                player.EnableEffect(effect, effect.Duration);

            activeTranqs.Remove(player);
            ListPool<StatusEffectBase>.Pool.Return(activeEffects);
        }
        catch (Exception e)
        {
            Log.Error($"{nameof(DoTranquilize)}: {e}");
        }

        if (Warhead.IsDetonated && player.Position.y < 900)
        {
            player.Hurt(new UniversalDamageHandler(-1f, DeathTranslations.Warhead));
            yield break;
        }

        player.Position = oldPosition;
    }

    private IEnumerator<float> ReduceResistances()
    {
        for (; ;)
        {
            foreach (Player player in tranquilizedPlayers.Keys)
                tranquilizedPlayers[player] = Mathf.Max(0, tranquilizedPlayers[player] / 2);

            yield return Timing.WaitForSeconds(ResistanceFalloffDelay);
        }
    }

    private void OnDeniableEvent(IDeniableEvent ev)
    {
        if (ev is IPlayerEvent eP)
        {
            if (activeTranqs.Contains(eP.Player))
                ev.IsAllowed = false;
        }
    }
}