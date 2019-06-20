public enum GameStates
{
    Menu,
    LevelMap,
    Upgrade,
    Playing,
    Pause,
    End
}
public enum EnemyStates
{
    Run = 0,
    Die = 1
}
public enum EnemyTypes
{
    Runner = 0,
    DarkWarrior = 1,
    Butcher = 2,
    Ghost = 3,
    Hellguarder = 4,
    Darkwizard = 5,
    Necromancer = 6,
    UndeadWorm = 7,
    Shadow = 8,
    Glutondragon = 9,
    Wyvern = 10,
    Carrier = 11,
    Tyrantdragon = 12,
    Nightmareshipper = 13,
    Immortaldevil = 14
}
public enum TowerTypes
{
    Block = 99,
    Normal = 0,
    NormalIceLv1 = 5,
    NormalIceLv2 = 6,
    NormalFireLv1 = 7,
    NormalFireLv2 = 8,
    Canon = 1,
    CanonLv2 = 10,
    CanonGoldLv1 = 11,
    CanonGoldLv2 = 12,
    CanonBloodLv1 = 13,
    CanonBloodLv2 = 14,
    LongRange = 2,
    LongRangeLv2 = 20,
    LongRangeMarkLv1 = 21,
    LongRangeMarkLv2 = 22,
    LongRangeSuperLv1 = 23,
    LongRangeSuperLv2 = 24,
    Magic = 3,
    MagicLv2 = 30,
    MagicMysteriousLv1 = 31,
    MagicMysteriousLv2 = 32,
    MagicStormLv1 = 33,
    MagicStormLv2 = 34,
    Soul = 4,
    SoulLv2 = 40,
    SoulSealLv1 = 41,
    SoulSealLv2 = 42,
    SoulFrozenLv1 = 43,
    SoulFrozenLv2 = 44,
}
public enum ArrowTypes
{
    Normal = 0,
    FrozenLevel1 = 1,
    FrozenLevel2 = 2,
    FireLevel1 = 3,
    FireLevel2 = 4
}
public enum MortarTypes
{
    Normal = 0,
    StunnedLevel1 = 1,
    StunnedLevel2 = 2
}
public enum SkillTypes
{
    Time = 0,
    Frozen = 1,
    Mine = 2,
    Meteor = 3
}
public enum SoundState
{
    MuteAll = 0,
    SoundOn = 1,
    MusicOn = 2,
    All = 3
}
public enum TrapType
{
    Poison = 0,
    Fire = 1,
    Ice = 2

}