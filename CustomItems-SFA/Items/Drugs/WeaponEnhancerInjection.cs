namespace CustomItems_SFA.Items;

using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Player = Exiled.Events.Handlers.Player;



    [Exiled.API.Features.Attributes.CustomItem(ItemType.Adrenaline)]
    public class WeaponEnhancerInjection : CustomItem
    {
        public override uint Id { get; set; } = 11;
        public override string Name { get; set; } = "Weapon-enhancer Injection";
        public override string Description { get; set; } = "This is a template item.";
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

        Effect e = new Effect();

        e.Type = EffectType.Scp1853;
        e.Intensity = 5;
        e.Duration = 0;
        e.IsEnabled = true;

        ev.Player.EnableEffect(e);

        //ev.Player.RemoveHeldItem(true);
    }
}

