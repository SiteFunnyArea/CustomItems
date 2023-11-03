namespace CustomItems_SFA.Items;

using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Usables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YamlDotNet.Serialization;
using Player = Exiled.Events.Handlers.Player;



    [Exiled.API.Features.Attributes.CustomItem(ItemType.SCP268)]
    public class Scp268TP : CustomItem
    {
        public override uint Id { get; set; } = 203;
        public override string Name { get; set; } = "SCP 268-TP";
        public override string Description { get; set; } = "Teleports to random room in the same zone you are in.";
        public override float Weight { get; set; } = 1f;
        public int Cooldown { get; set; } = 30;
    [YamlIgnore]
    public bool CooldownActive;
        public override SpawnProperties? SpawnProperties { get; set; } = new()
        {
            Limit = 1,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
        {
            new()
            {
                Chance = 100,
                Location = SpawnLocationType.InsideLczArmory,
            },
        },
        };

        protected override void SubscribeEvents()
        {
            Player.UsingItem += OnUsingItem;

            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Player.UsingItem -= OnUsingItem;

            base.UnsubscribeEvents();
        }

    private void OnUsingItem(UsingItemEventArgs ev)
    {
        if (!Check(ev.Player.CurrentItem))
            return;

        ev.IsAllowed = false;
        Exiled.API.Features.Player p = ev.Player;

        Room r = Room.Get(p.CurrentRoom.Zone).GetRandomValue();
        if (r.Type == RoomType.Lcz173)
            r = Room.Get(RoomType.Lcz330);
        Vector3 v3 = new Vector3(r.Position.x + 0.4f, r.Position.y + 1f, r.Position.z + 0.6f);
        p.Transform.position = v3;
        //ev.Player.RemoveItem(ev.Player.CurrentItem);




    }
}

