using Libraries;
using Libraries.Enum;
using MonsterEnum = Libraries.Enum.Monsters;

namespace HeroQuestApp;

public class Ennemy
{
    public List<Monster> Monsters { get; set; } = [];

    public Monster ChaosWarlock { get; set; } = new();

    public void BuildMonster(Monster monster) {
        switch (monster.Type) {
            case MonsterEnum.Goblin:
                monster.Stats = new()
                {
                    MovementDice = 1,
                    AttackDice = 2,
                    DefenseDice = 1,
                    Body = 1,
                    Mind = 1
                };
                monster.Weapon = new()
                {
                    Price = 50,
                    Name = Weapons.Dagger,
                    AttackBonusDice = 1
                };
                break;
            case MonsterEnum.Orc:
                monster.Stats = new()
                {
                    MovementDice = 2,
                    AttackDice = 3,
                    DefenseDice = 2,
                    Body = 2,
                    Mind = 2
                };
                monster.Weapon = new()
                {
                    Price = 100,
                    Name = Weapons.ShortSword,
                    AttackBonusDice = 2
                };
                break;
            case MonsterEnum.Skeleton:
                monster.Stats = new()
                {
                    MovementDice = 2,
                    AttackDice = 2,
                    DefenseDice = 2,
                    Body = 2,
                    Mind = 2
                };
                monster.Weapon = new()
                {
                    Price = 150,
                    Name = Weapons.Broadsword,
                    AttackBonusDice = 3
                };
                break;
            case MonsterEnum.ChaosWarlock:
                monster.Stats = new()
                {
                    MovementDice = 2,
                    AttackDice = 2,
                    DefenseDice = 2,
                    Body = 3,
                    Mind = 4
                };
                monster.Weapon = new()
                {
                    Price = 200,
                    Name = Weapons.Staff,
                    AttackBonusDice = 2,
                    IsForWizard = true,
                    HasDiagonalAttack = true
                };
                break;
            default:
                WriteLine("ERROR: check Enum 'Monsters'");
                break;
        }
        
        Monsters.Add(monster);
    }
}
