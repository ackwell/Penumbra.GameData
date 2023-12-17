using Penumbra.GameData.DataContainers;
using Penumbra.GameData.DataContainers.Bases;
using Penumbra.GameData.Enums;
using Penumbra.GameData.Structs;

namespace Penumbra.GameData.Data;

public sealed class ItemData(ItemsByType _byType, ItemsPrimaryModel _primary, ItemsSecondaryModel _secondary, ItemsTertiaryModel _tertiary)
    : IAsyncService
{
    public readonly ItemsByType         ByType    = _byType;
    public readonly ItemsPrimaryModel   Primary   = _primary;
    public readonly ItemsSecondaryModel Secondary = _secondary;
    public readonly ItemsTertiaryModel  Tertiary  = _tertiary;

    public Task Awaiter { get; } = Task.WhenAll(_byType.Awaiter, _primary.Awaiter, _secondary.Awaiter, _tertiary.Awaiter);

    private static readonly IReadOnlyDictionary<SetId, FullEquipType> WeaponTypes = new Dictionary<SetId, FullEquipType>
    {
        [101] = FullEquipType.Fists,
    };

    public int Count
        => ByType.TotalCount;

    public IEnumerable<(ItemId, EquipItem)> AllItems(bool main)
        => (main ? (ItemDictionary)Primary : Secondary).Select(i => ((ItemId)i.Key, i.Value));

    public bool TryGetValue(ItemId key, EquipSlot slot, out EquipItem value)
    {
        var dict = slot is EquipSlot.OffHand ? (ItemDictionary)Secondary : Primary;
        if (slot is EquipSlot.Hands && Tertiary.TryGetValue(key.Id, out var v) || dict.TryGetValue(key.Id, out v))
        {
            value = v;
            return true;
        }

        value = default;
        return false;
    }


    private static readonly (FullEquipType Main, FullEquipType Off, short Offset)[] WeaponIdTable =
    {
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Shield, FullEquipType.Unknown, 100),
        (FullEquipType.Sword, FullEquipType.Unknown, 100),
        (FullEquipType.Fists, FullEquipType.FistsOff, 50),
        (FullEquipType.Axe, FullEquipType.Unknown, 100),
        (FullEquipType.Lance, FullEquipType.Unknown, 100),
        (FullEquipType.Bow, FullEquipType.BowOff, 97),
        (FullEquipType.Wand, FullEquipType.Unknown, 100),
        (FullEquipType.Staff, FullEquipType.Unknown, 100),
        (FullEquipType.Wand, FullEquipType.Unknown, 100),
        (FullEquipType.Staff, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Broadsword, FullEquipType.Unknown, 100),
        (FullEquipType.Fists, FullEquipType.FistsOff, 50),
        (FullEquipType.Book, FullEquipType.Unknown, 100),
        (FullEquipType.Daggers, FullEquipType.DaggersOff, 50),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Gun, FullEquipType.GunOff, 98),
        (FullEquipType.Orrery, FullEquipType.OrreryOff, 98),
        (FullEquipType.Katana, FullEquipType.KatanaOff, 50),
        (FullEquipType.Rapier, FullEquipType.RapierOff, 50),
        (FullEquipType.Cane, FullEquipType.Unknown, 100),
        (FullEquipType.Gunblade, FullEquipType.Unknown, 100),
        (FullEquipType.Glaives, FullEquipType.GlaivesOff, 50),
        (FullEquipType.Nouliths, FullEquipType.Unknown, 100),
        (FullEquipType.Scythe, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Saw, FullEquipType.ClawHammer, 40),
        (FullEquipType.CrossPeinHammer, FullEquipType.File, 40),
        (FullEquipType.RaisingHammer, FullEquipType.Pliers, 40),
        (FullEquipType.LapidaryHammer, FullEquipType.GrindingWheel, 40),
        (FullEquipType.Knife, FullEquipType.Awl, 40),
        (FullEquipType.Needle, FullEquipType.SpinningWheel, 40),
        (FullEquipType.Alembic, FullEquipType.Mortar, 40),
        (FullEquipType.Frypan, FullEquipType.CulinaryKnife, 40),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Unknown, FullEquipType.Unknown, 100),
        (FullEquipType.Pickaxe, FullEquipType.Sledgehammer, 50),
        (FullEquipType.Hatchet, FullEquipType.GardenScythe, 50),
        (FullEquipType.FishingRod, FullEquipType.Gig, 50),
    };

    public static FullEquipType ConvertWeaponId(SetId id)
    {
        var quotient = Math.DivRem(id.Id - 1, 100, out var remainder);
        if (quotient > WeaponIdTable.Length)
            return FullEquipType.Unknown;

        var (primary, secondary, offset) = WeaponIdTable[quotient];
        return remainder >= offset ? secondary : primary;

        // return id.Id switch
        // {
        //     > 0100 and <= 0200 => FullEquipType.Shield,
        //     > 0200 and <= 0300 => FullEquipType.Sword,
        //     > 0300 and <= 0350 => FullEquipType.Fists,
        //     > 0350 and <= 0400 => FullEquipType.FistsOff,
        //     > 0400 and <= 0500 => FullEquipType.Axe,
        //     > 0500 and <= 0600 => FullEquipType.Lance,
        //     > 0600 and <= 0650 => FullEquipType.Bow,
        //     > 0650 and <= 0700 => FullEquipType.BowOff,
        //     > 0700 and <= 0800 => FullEquipType.Wand,
        //     > 0800 and <= 0900 => FullEquipType.Staff,
        //     > 0900 and <= 1000 => FullEquipType.Wand,
        //     > 1000 and <= 1100 => FullEquipType.Staff,
        //     > 1500 and <= 1600 => FullEquipType.Broadsword,
        //     > 1600 and <= 1650 => FullEquipType.Fists,
        //     > 1650 and <= 1700 => FullEquipType.FistsOff,
        //     > 1700 and <= 1800 => FullEquipType.Book,
        //     > 1800 and <= 1850 => FullEquipType.Daggers,
        //     > 1850 and <= 1900 => FullEquipType.DaggersOff,
        //     > 2000 and <= 2050 => FullEquipType.Gun,
        //     > 2050 and <= 2100 => FullEquipType.GunOff,
        //     > 2100 and <= 2150 => FullEquipType.Orrery,
        //     > 2150 and <= 2200 => FullEquipType.OrreryOff,
        //     > 2200 and <= 2250 => FullEquipType.Katana,
        //     > 2250 and <= 2300 => FullEquipType.KatanaOff,
        //     > 2300 and <= 2350 => FullEquipType.Rapier,
        //     > 2350 and <= 2400 => FullEquipType.RapierOff,
        //     > 2400 and <= 2500 => FullEquipType.Cane,
        //     > 2500 and <= 2600 => FullEquipType.Gunblade,
        //     > 2600 and <= 2650 => FullEquipType.Glaives,
        //     > 2650 and <= 2700 => FullEquipType.GlaivesOff,
        //     > 2700 and <= 2800 => FullEquipType.Nouliths,
        //     > 2800 and <= 2900 => FullEquipType.Scythe,
        //     > 5040 and <= 5100 => FullEquipType.ClawHammer,
        //     > 5100 and <= 5140 => FullEquipType.CrossPeinHammer,
        //     > 5140 and <= 5200 => FullEquipType.File,
        //     > 5200 and <= 5240 => FullEquipType.RaisingHammer,
        //     > 5240 and <= 5300 => FullEquipType.Pliers,
        //     > 5300 and <= 5340 => FullEquipType.LapidaryHammer,
        //     > 5340 and <= 5400 => FullEquipType.GrindingWheel,
        //     > 5400 and <= 5440 => FullEquipType.Knife,
        //     > 5440 and <= 5500 => FullEquipType.Awl,
        //     > 5500 and <= 5540 => FullEquipType.Needle,
        //     > 5540 and <= 5600 => FullEquipType.SpinningWheel,
        //     > 5600 and <= 5640 => FullEquipType.Alembic,
        //     > 5640 and <= 5700 => FullEquipType.Mortar,
        //     > 5700 and <= 5740 => FullEquipType.Frypan,
        //     > 5740 and <= 5800 => FullEquipType.CulinaryKnife,
        //     > 7000 and <= 7050 => FullEquipType.Pickaxe,
        //     > 7050 and <= 7100 => FullEquipType.Sledgehammer,
        //     > 7100 and <= 7150 => FullEquipType.Hatchet,
        //     > 7150 and <= 7200 => FullEquipType.GardenScythe,
        //     > 7200 and <= 7250 => FullEquipType.FishingRod,
        //     > 7250 and <= 7300 => FullEquipType.Gig,
        //     _ => FullEquipType.Unknown,
        // };
    }
}
