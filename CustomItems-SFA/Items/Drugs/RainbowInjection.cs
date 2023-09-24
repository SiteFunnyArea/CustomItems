namespace CustomItems_SFA.Items;

using Exiled.API.Enums;
using Exiled.API.Features;
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
public class RainbowInjection : CustomItem
{
    public override uint Id { get; set; } = 8;
    public override string Name { get; set; } = "Rainbow Injection";
    public override string Description { get; set; } = "Gives you all the candy effects";
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
        Player.UsedItem += OnUsedItem;

        base.SubscribeEvents();
    }

    protected override void UnsubscribeEvents()
    {
        Player.UsedItem -= OnUsedItem;

        base.UnsubscribeEvents();
    }

    private void OnUsedItem(UsedItemEventArgs ev)
    {

        if (!Check(ev.Item))
            return;
        Effect e1 = new Effect();
        e1.Type = EffectType.Vitality;
        e1.Duration = 30;
        e1.Intensity = 3;
        Effect e2 = new Effect();
        e2.Type = EffectType.DamageReduction;
        e2.Duration = 30;
        e2.Intensity = 40;
        Effect e3 = new Effect();
        e3.Type = EffectType.RainbowTaste;
        e3.Duration = 30;
        e3.Intensity = 1;
        Effect e4 = new Effect();
        e4.Type = EffectType.Invigorated;
        e4.Duration = 30;
        e4.Intensity = 1;
        Effect e5 = new Effect();
        e5.Type = EffectType.BodyshotReduction;
        e5.Duration = 30;
        e5.Intensity = 1;
        Effect e6 = new Effect();
        e6.Type = EffectType.MovementBoost;
        e6.Duration = 30;
        e6.Intensity = 10;

        ev.Player.EnableEffect(e1);
        ev.Player.EnableEffect(e2);
        ev.Player.EnableEffect(e3);
        ev.Player.EnableEffect(e4);
        ev.Player.EnableEffect(e5);
        ev.Player.EnableEffect(e6);
        ev.Player.Heal(50);
 
        ev.Item.Destroy();
    }
}

