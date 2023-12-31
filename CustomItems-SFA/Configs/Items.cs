// -----------------------------------------------------------------------
// <copyright file="Items.cs" company="Joker119">
// Copyright (c) Joker119. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

#pragma warning disable SA1200

using CustomItems.Items;

namespace CustomItems.Configs;

using CustomItems_SFA.Items;
using CustomItems_SFA.Items.Armour;
using InventorySystem.Items.Usables;
using System.Collections.Generic;
using System.ComponentModel;

/// <summary>
/// All item config settings.
/// </summary>
public class Items
{
    /// <summary>
    /// Gets the list of emp greanades.
    /// </summary>
    [Description("The list of EMP grenades.")]
    public List<EmpGrenade> EmpGrenades { get; private set; } = new()
    {
        new EmpGrenade(),
    };

    /// <summary>
    /// Gets the list of grenade launchers.
    /// </summary>
    [Description("The list of grenade launchers.")]
    public List<GrenadeLauncher> GrenadeLaunchers { get; private set; } = new()
    {
        new GrenadeLauncher(),
    };

    /// <summary>
    /// Gets the list of implosion grenades.
    /// </summary>
    [Description("The list of implosion grenades.")]
    public List<ImplosionGrenade> ImplosionGrenades { get; private set; } = new()
    {
        new ImplosionGrenade(),
    };

    /// <summary>
    /// Gets the list of lethal injections.
    /// </summary>
    [Description("The list of lethal injections.")]
    public List<LethalInjection> LethalInjections { get; private set; } = new()
    {
        new LethalInjection(),
    };

    /// <summary>
    /// Gets the list of lucky coins.
    /// </summary>
    [Description("The list of lucky coins.")]
    public List<LuckyCoin> LuckyCoins { get; private set; } = new()
    {
        new LuckyCoin(),
    };

    /// <summary>
    /// Gets the list of mediGuns.
    /// </summary>
    [Description("The list of mediGuns.")]
    public List<MediGun> MediGuns { get; private set; } = new()
    {
        new MediGun(),
    };

    /// <summary>
    /// Gets the list of Scp127s.
    /// </summary>
    [Description("The list of Scp127s.")]
    public List<Scp127> Scp127s { get; private set; } = new()
    {
        new Scp127(),
    };

    /// <summary>
    /// Gets the list of Scp1499s.
    /// </summary>
    [Description("The list of Scp1499s.")]
    public List<Scp1499> Scp1499s { get; private set; } = new()
    {
        new Scp1499(),
    };

    /// <summary>
    /// Gets the list of sniper rifles.
    /// </summary>
    [Description("The list of sniper rifles.")]
    public List<SniperRifle> SniperRifle { get; private set; } = new()
    {
        new SniperRifle(),
    };

    /// <summary>
    /// Gets the list of tranquilizer guns.
    /// </summary>
    [Description("The list of tranquilizer guns.")]
    public List<TranquilizerGun> TranquilizerGun { get; private set; } = new()
    {
        new TranquilizerGun(),
    };

    /// <summary>
    /// Gets the list of Scp714s.
    /// </summary>
    [Description("The list of Scp714s.")]
    public List<Scp714> Scp714s { get; private set; } = new()
    {
        new Scp714(),
    };

    /// <summary>
    /// Gets the list of Anti-Memetic Pills.
    /// </summary>
    [Description("The list of Anti-Memetic Pills.")]
    public List<AntiMemeticPills> AntiMemeticPills { get; private set; } = new()
    {
        new AntiMemeticPills(),
    };

    /// <summary>
    /// Gets the list of DeflectorSheilds.
    /// </summary>
    [Description("The list of DeflectorSheilds.")]
    public List<DeflectorShield> DeflectorSheilds { get; private set; } = new()
    {
        new DeflectorShield(),
    };

    /// <summary>
    /// Gets the list of <see cref="Scp2818"/>s.
    /// </summary>
    [Description("The list of SCP-2818s.")]
    public List<Scp2818> Scp2818s { get; private set; } = new()
    {
        new Scp2818(),
    };

    /// <summary>
    /// Gets the list of C4Charges.
    /// </summary>
    [Description("The list of C4Charges.")]
    public List<C4Charge> C4Charges { get; private set; } = new()
    {
        new C4Charge(),
    };

    /// <summary>
    /// Gets the list of AutoGuns.
    /// </summary>
    [Description("The list of AutoGuns.")]
    public List<AutoGun> AutoGuns { get; private set; } = new()
    {
        new AutoGun(),
    };

    [Description("The list of Impact Grenades.")]
    public List<ImpactGrenade> ImpactGrenades { get; private set; } = new()
    {
        new ImpactGrenade(),
    };

    [Description("The list of Invisible Inks.")]
    public List<InvisibleInk> InvisibleInks { get; private set; } = new()
    {
        new InvisibleInk(),
    };

    [Description("The list of Laxatives.")]
    public List<Laxatives> Laxatives { get; private set; } = new()
    {
        new Laxatives(),
    };

    [Description("The list of SCP 500-R's.")]
    public List<Scp500R> Scp500Rs { get; private set; } = new()
    {
        new Scp500R(),
    };

    [Description("The list of Steroids.")]
    public List<Steroids> Steroids { get; private set; } = new()
    {
        new Steroids(),
    };

    [Description("The list of Undroppable Keycards.")]
    public List<UndroppableKeycard> UndroppableKeycards { get; private set; } = new()
    {
        new UndroppableKeycard(),
    };

    [Description("The list of Enhanced Adrenalines.")]
    public List<EnhancedAdrenaline> EnhancedAdrenalines { get; private set; } = new()
    {
        new EnhancedAdrenaline(),
    };

    [Description("The list of Delta-4 Transmitters.")]
    public List<Delta4Transmitter> Delta4Transmitters { get; private set; } = new()
    {
        new Delta4Transmitter(),
    };

    [Description("The list of Crowd Control Napalms.")]
    public List<CrowdControlNapalm> CrowdControlNapalms { get; private set; } = new()
    {
        new CrowdControlNapalm(),
    };

    [Description("The list of Nerve Agent Grenades.")]
    public List<NerveAgentGrenade> NerveAgentGrenades { get; private set; } = new()
    {
        new NerveAgentGrenade(),
    };

    [Description("The list of SCP-500 HPs.")]
    public List<Scp500HP> Scp500HPs { get; private set; } = new()
    {
        new Scp500HP(),
    };

    [Description("The list of SCP-500 TRs.")]
    public List<Scp500TR> Scp500TRs { get; private set; } = new()
    {
        new Scp500TR(),
    };

    [Description("The list of SCP-500 IVs.")]
    public List<Scp500IV> Scp500IVs { get; private set; } = new()
    {
        new Scp500IV(),
    };

    [Description("The list of SCP-500 SHs.")]
    public List<Scp500SH> Scp500SHs { get; private set; } = new()
    {
        new Scp500SH(),
    };


    [Description("The list of Spiked Injections.")]
    public List<SpikedInjection> SpikedInjections { get; private set; } = new()
    {
        new SpikedInjection(),
    };

    [Description("The list of RRCs.")]
    public List<RapidResponseCall> RRCs { get; private set; } = new()
    {
        new RapidResponseCall(),
    };

    [Description("The list of SCP 420-Js.")]
    public List<Scp420J> Scp420Js { get; private set; } = new()
    {
        new Scp420J(),
    };

    [Description("The list of Scp127HPs.")]
    public List<Scp127HP> Scp127HPs { get; private set; } = new()
    {
        new Scp127HP(),
    };

    [Description("The list of WeaponEnhancerInjections.")]
    public List<WeaponEnhancerInjection> WeaponEnhancerInjections { get; private set; } = new()
    {
        new WeaponEnhancerInjection(),
    };

    [Description("The list of FlameRoundE11SRs.")]
    public List<FlameRoundE11SR> FlameRoundE11SRs { get; private set; } = new()
    {
        new FlameRoundE11SR(),
    };

    [Description("The list of FrostBurnRoundE11SRs.")]
    public List<FrostBurnRoundE11SR> FrostBurnRoundE11SRs { get; private set; } = new()
    {
        new FrostBurnRoundE11SR(),
    };

    [Description("The list of DietCokes.")]
    public List<DietCoke> DietCokes { get; private set; } = new()
    {
        new DietCoke(),
    };

    [Description("The list of SuperHeavyArmours.")]
    public List<SuperHeavyArmour> SuperHeavyArmours { get; private set; } = new()
    {
        new SuperHeavyArmour(),
    };

    [Description("The list of MedicalEnhancerInjections.")]
    public List<MedicalEnhancerInjection> MedicalEnhancerInjections { get; private set; } = new()
    {
        new MedicalEnhancerInjection(),
    };

    [Description("The list of SuperChargedGrenades.")]
    public List<SuperChargedGrenade> SuperChargedGrenades { get; private set; } = new()
    {
        new SuperChargedGrenade(),
    };

    [Description("The list of DisguisedAmmoBoxs.")]
    public List<DisguisedAmmoBox> DisguisedAmmoBoxs { get; private set; } = new()
    {
        new DisguisedAmmoBox(),
    };

    public List<KeycardSerpentsDevice> KeycardSerpentsDevices { get; private set; } = new()
    {
        new KeycardSerpentsDevice(),
    };

    public List<OverclockedFRMG> OverclockedFRMGs { get; private set; } = new()
    {
        new OverclockedFRMG(),
    };

    public List<OverclockedEmpGrenade> OverclockedEmpGrenades { get; private set; } = new()
    {
        new OverclockedEmpGrenade(),
    };
    public List<Scp268TP> Scp268TPs { get; private set; } = new()
    {
        new Scp268TP(),
    };
    public List<ExplosiveReductionArmor> ExplosiveReductionArmors { get; private set; } = new()
    {
        new ExplosiveReductionArmor(),
    };
    public List<ModifiedSerpentsRifle> ModifiedSerpentsRifles { get; private set; } = new()
    {
        new ModifiedSerpentsRifle(),
    };

    public List<CounterInsurgencySniperRifle> CounterInsurgencySniperRifles { get; private set; } = new()
    {
        new CounterInsurgencySniperRifle(),
    };

    public List<StandardStunBaton> StandardStunBatons { get; private set; } = new()
    {
        new StandardStunBaton(),
    };

    public List<InjectionDR> InjectionDRs { get; private set; } = new()
    {
        new InjectionDR(),
    };

    public List<InjectionHP> InjectionHPs { get; private set; } = new()
    {
        new InjectionHP(),
    };

    public List<InjectionIV> InjectionIVs { get; private set; } = new()
    {
        new InjectionIV(),
    };

    public List<InjectionS> InjectionSs { get; private set; } = new()
    {
        new InjectionS(),
    };

    public List<InjectionTP> InjectionTPs { get; private set; } = new()
    {
        new InjectionTP(),
    };

    public List<InjectionR> InjectionRs { get; private set; } = new()
    {
        new InjectionR(),
    };

    public List<Scp500TM> Scp500TMs { get; private set; } = new()
    {
        new Scp500TM(),
    };
    public List<Scp500S> Scp500Ss { get; private set; } = new()
    {
        new Scp500S(),
    };
    public List<Scp500TP> Scp500TPs { get; private set; } = new()
    {
        new Scp500TP(),
    };
    public List<OverclockedGrenadeLauncher> OverclockedGrenadeLaunchers { get; private set; } = new()
    {
        new OverclockedGrenadeLauncher(),
    };
    public List<HolyShitGrenade> HolyShitGrenades { get; private set; } = new()
    {
        new HolyShitGrenade(),
    };

    public List<SpyDevice> SpyDevices { get; private set; } = new()
    {
        new SpyDevice(),
    };
}
