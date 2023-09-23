using CustomItems;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CI = CustomItems.CustomItems;

namespace CustomItems_SFA.Items.Armour;

[CustomItem(ItemType.ArmorHeavy)]
public class SuperHeavyArmour : CustomArmor
{
    public override uint Id { get; set; } = 3914;
    public override string Name { get; set; } = "Super Heavy Armour";
    public override string Description { get; set; } = "";
    public override float Weight { get; set; } = 2f;
    public override SpawnProperties? SpawnProperties { get; set; }

    public override ItemType Type { get; set; } = ItemType.ArmorHeavy;

    protected override void OnPickingUp(PickingUpItemEventArgs ev)
    {
        ev.Player.AddAhp(50, 75, 0);
        ev.Player.UniqueRole = ev.Player.UniqueRole + "-SHA";
        base.OnPickingUp(ev);
    }

    protected override void OnDropping(DroppingItemEventArgs ev)
    {
        ev.Player.ArtificialHealth = 0;
        if (ev.Player.UniqueRole.Contains(CI.Instance.Config.ItemConfigs.RRCs[0].RoleName))
        {
            ev.Player.UniqueRole = CI.Instance.Config.ItemConfigs.RRCs[0].RoleName;
        }
        else if (ev.Player.UniqueRole.Contains(CI.Instance.Config.ItemConfigs.Delta4Transmitters[0].RoleName))
        {
            ev.Player.UniqueRole = CI.Instance.Config.ItemConfigs.Delta4Transmitters[0].RoleName;
        }
        else
        {
            ev.Player.UniqueRole = "";
        }
        
        base.OnDropping(ev);
    }

    public void OnHurt(HurtingEventArgs ev)
    {
        if (ev.Player.UniqueRole.Contains("-SHA"))
        {
            Timing.CallDelayed(10f, () => {
                ev.Player.ArtificialHealth = 50;
            });
        }
    }

}

