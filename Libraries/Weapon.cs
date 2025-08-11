using Libraries.Enum;

namespace Libraries;

public class Weapon
{
    public Weapons Name { get; set; }

    public string? Description { get; set; }

    public uint Price { get; set; }

    public uint AttackBonusDice { get; set; }

    public bool IsForWizard { get; set; } = true;

    public bool Ranged { get; set; } = false;

    public uint Distance { get; set; } = 1;

    public bool IsReloadNeeded { get; set; } = false;

    public bool IsAllowedWithShield { get; set; } = true;

    public bool HasDiagonalAttack { get; set; } = false;

    public bool CanUseSpell { get; set; } =  false;
}
