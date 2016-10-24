public enum combatType
{
    FIRE,
    WATER,
    LIGHTNING,
    NONE
}

public enum combatStance
{
    STANDING,
    FLYING,
    GROUNDED
}

public enum combatEvent
{
    COMBAT_START,
    COMBAT_END,

    ON_DEATH,

    BEFORE_DAMAGE,
    AFTER_DAMAGE,
    BEFORE_DAMAGED,
    AFTER_DAMAGED
}