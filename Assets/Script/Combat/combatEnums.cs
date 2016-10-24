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

public enum moveStatus
{
    ENABLED,
    DISABLED
}

public enum combatEvent
{
    COMBAT_START,
    COMBAT_END,

    ON_DEATH,

    BEFORE_MOVE,
    AFTER_MOVE,

    TURN_START,
    TURN_END,

    SPEED_TICK, // whenever a unit's delay is reduced by speed

    BEFORE_BUFF,
    BEFORE_BUFFED,
    AFTER_BUFF,
    AFTER_BUFFED,

    BEFORE_DAMAGE,
    AFTER_DAMAGE,
    BEFORE_DAMAGED,
    AFTER_DAMAGED
}