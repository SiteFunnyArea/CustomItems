namespace CustomItems_SFA.Items;

using Exiled.API.Enums;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;



    [Exiled.API.Features.Attributes.CustomItem(ItemType.KeycardJanitor)]
    public class UndroppableKeycard : CustomItem
    {
        public override uint Id { get; set; } = 70;
        public override string Name { get; set; } = "Undroppable Keycard";
        public override string Description { get; set; } = "Well it's not exactly undroppable... But if you do drop it, you will fucking die.";
        public override float Weight { get; set; } = 1f;
        public override SpawnProperties? SpawnProperties { get; set; } = new()
        {
            Limit = 1,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
        {
            new()
            {
                Chance = 1,
                Location = SpawnLocationType.InsideGateB,
            },
        },
        };

    protected override void SubscribeEvents()
        {
        Exiled.Events.Handlers.Player.UsingItem += OnUsingItem;
        base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
        Exiled.Events.Handlers.Player.UsingItem -= OnUsingItem;

        base.UnsubscribeEvents();
        }

        private void OnUsingItem(UsingItemEventArgs ev)
        {
        if (!Check(ev.Player.CurrentItem))
            return;

        ev.Player.RemoveItem(ev.Player.CurrentItem);
        Debug.Log("Server recieved Item");
        }

    protected override void OnDropping(DroppingItemEventArgs ev)
    {
        if (!Check(ev.Player.CurrentItem))
            return;

        ev.Player.Kill("Shouldn't have dropped the Undroppable Keycard.");
        base.OnDropping(ev);
    }
}

