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

    TURN_START,
    TURN_END,

    SPEED_TICK, // triggers when delay is reduced by speed

    ON_DEATH,

    BEFORE_DAMAGE,
    AFTER_DAMAGE,
    BEFORE_DAMAGED,
    AFTER_DAMAGED
}