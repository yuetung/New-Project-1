public enum CombatType
{
    FIRE,
    WATER,
    LIGHTNING,
    NONE
}

public enum CombatStance
{
    STANDING,
    FLYING,
    GROUNDED
}

public enum CombatEvent
{
    COMBAT_START,
    COMBAT_END,

    TURN_START,
    TURN_END,

    SPEED_TICK, // triggers when delay is reduced by speed

    ON_DEATH,

    BEFORE_DAMAGE,
    AFTER_DAMAGE,
    BEFORE_DAMAGED,
    AFTER_DAMAGED,

    BEFORE_BUFF,
    AFTER_BUFF,
    BEFORE_BUFFED,
    AFTER_BUFFED
}