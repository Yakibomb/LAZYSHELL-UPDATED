using System;
using System.Collections.Generic;
using System.Text;

namespace LAZYSHELL
{
    // stats
    [Flags]
    public enum Status
    {
        Mute = 1, Sleep = 2, Poison = 4, Fear = 8,
        Berserk = 16, Mushroom = 32, Scarecrow = 64, Invincible = 128
    }
    [Flags]
    public enum Targetting
    {
        LiveAlly = 2, Enemy = 4, All = 16, WoundedOnly = 32, OnePartyOnly = 64, NotSelf = 128
    }
    // scripts
    // other
    public enum MenuType
    {
        GameSelect, OverworldMain, OverworldItem, OverworldStatus,
        OverworldSpecial, OverworldEquip, OverworldSpecialItem,
        OverworldSwitch, OverworldStarPieces,
        Shop, ShopBuy, ShopSellItems, ShopSellWeapons
    }
    public enum FontType
    {
        Dialogue, Menu, Description, Triangles, FlowerBonus, BattleMenu
    }
    // Audio
    public enum Pitch
    {
        C, Cs, D, Ds, E, F, Fs, G, Gs, A, As, B, Rest, Tie, NULL
    }
    public enum Accidental
    {
        None, Flat, Natural, Sharp
    }
    public enum Key
    {
        CMajor, GMajor, DMajor, AMajor, EMajor, BMajor, FsMajor, CsMajor, // Sharps
        FMajor, BbMajor, EbMajor, AbMajor, DbMajor, GbMajor, CbMajor, // Flats
        AMinor, EMinor, BMinor, FsMinor, CsMinor, GsMinor, DsMinor, AsMinor, // Sharps
        DMinor, GMinor, CMinor, FMinor, BbMinor, EbMinor, AbMinor // Flats
    }
    public enum Beat
    {
        Whole,
        HalfDotted,
        Half,
        QuarterDotted,
        Quarter,
        EighthDotted,
        QuarterTriplet,
        Eighth,
        EighthTriplet,
        Sixteenth,
        SixteenthTriplet,
        ThirtySecond,
        SixtyFourth,
        NULL
    }
    public enum SPCType
    {
        SPCTrack, EventSFX, BattleSFX
    }
    public enum NativeSPC
    {
        SMRPG,
        SMWLevel,
        SMWOverworld,
        Custom
    }
    //
    public enum EType
    {
        ActionScript,
        AnimationScript,
        BattleScript,
        EventScript,
        Level,
        MineCart,
        SPCBattle,
        SPCEvent,
        SPCTrack,
        Sprites,
        Effects
    }
    public enum MessageIcon
    {
        None,
        Error,
        Info,
        Warning
    }
    public enum TilesetType
    {
        Level, SideScrolling, Mode7, Title, WorldMap, WorldMapLogo, Opening, GameSelectMenu, StarPiecesOverworldMenu, MenuBackground
    }
    public enum TilemapType
    {
        Level, Mod, Template, None
    }
    public enum Drawing
    {
        None, Draw, Erase, Fill, FillAll, ReplaceColor, Dropper, Select
    }
}
