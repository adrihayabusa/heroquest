using Libraries.Enum;
using Libraries.Shared;

namespace Libraries;

// The interface that defines the common behavior for all spell effects
public interface ISpellEffect
{
    // The name of the effect
    string Name { get; }

    // The method that applies the effect
    void ApplyEffect(Entity caster, Entity target, Board board);
}

// A concrete class that implements the fire wall effect
public class FireWallEffect : ISpellEffect
{
    public string Name => "Fire Wall";

    public void ApplyEffect(Entity caster, Entity target, Board board)
    {
        
    }
}

// A concrete class that implements the push effect
public class PushEffect : ISpellEffect
{
    public string Name => "Push";

    public void ApplyEffect(Entity caster, Entity target, Board board)
    {
        // The logic for pushing the target away from the caster
        // For example, you could calculate the direction and distance of the push
        // and move the target to the nearest free tile in that direction
        // You could also check if the target hits a wall or another entity and deal damage accordingly
    }
}

// A concrete class that implements the heal effect
public class HealEffect : ISpellEffect
{
    public string Name => "Heal";

    public void ApplyEffect(Entity caster, Entity target, Board board)
    {
        // The logic for healing the target
        // For example, you could roll the damage dice of the spell and add the caster's ability power
        // and increase the target's health by that amount
        // You could also check if the target is at full health and apply some bonus effect
    }
}

// The class that represents a spell
public class Spell(
    Spells spellName,
    ISpellEffect effect,
    uint damageDice,
    bool isMultiTarget,
    uint range)
{
    // The name of the spell
    public Spells SpellName { get; set; } = spellName;

    // The effect of the spell
    public ISpellEffect Effect { get; set; } = effect;

    // The number of dice to roll for damage
    public uint DamageDice { get; set; } = damageDice;

    // Whether the spell can target multiple entities or not
    public bool IsMultiTarget { get; set; } = isMultiTarget;

    // The range of the spell in tiles
    public uint Range { get; set; } = range;

    // The method that uses the spell
    public void UseSpell(Entity caster, Entity target, Board board) {
        // The logic for using the spell
        // For example, you could check if the caster has enough Mind and the target is in range
        // and then call the ApplyEffect() method of the spell's effect
    }
}
