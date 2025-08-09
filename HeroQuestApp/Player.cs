using Libraries;
using Libraries.Enum;

namespace HeroQuestApp;

public class Player(string name)
{
    public string Name = name;

    public Hero Hero = new();

    public Random Dice = new();

    public void BuildHero(Heroes heroClass, string heroName) {
        Hero.Name = heroName;
        Hero.Class = heroClass;

        switch (heroClass) {
            case Heroes.Barbarian:
                Hero.Stats = new() {
                    MovementDice = 2,
                    AttackDice = 3,
                    DefenseDice = 2,
                    Body = 8,
                    Mind = 2
                };
                Hero.Weapon = new() {
                    Price = 250,
                    Name = Weapons.Broadsword,
                    AttackBonusDice = 3
                };
                break;
            case Heroes.Elf:
                Hero.Stats = new() {
                    MovementDice = 2,
                    AttackDice = 2,
                    DefenseDice = 2,
                    Body = 6,
                    Mind = 4
                };
                Hero.Weapon = new() {
                    Price = 150,
                    Name = Weapons.ShortSword,
                    AttackBonusDice = 2
                };
                break;
            case Heroes.Dwarf:
                Hero.Stats = new() {
                    MovementDice = 2,
                    AttackDice = 2,
                    DefenseDice = 2,
                    Body = 7,
                    Mind = 3
                };
                Hero.Weapon = new() {
                    Price = 150,
                    Name = Weapons.ShortSword,
                    AttackBonusDice = 2
                };
                break;
            case Heroes.Wizard:
                Hero.Stats = new() {
                    MovementDice = 2,
                    AttackDice = 1,
                    DefenseDice = 2,
                    Body = 4,
                    Mind = 6
                };
                Hero.Weapon = new() {
                    Price = 25,
                    Name = Weapons.Dagger,
                    AttackBonusDice = 1,
                    IsForWizard = true
                };
                break;
            default:
                throw new ArgumentException("ERROR: check Enum 'Heroes'");
        }
    }

    public int Moves() {
        int movements = 0;
        
        for (int i = 0; i < Hero.Stats.MovementDice; i++) {
            int roll = Dice.Next(1, 7);
            movements += roll;
        }

        return movements;
    }

    // Create and implement methods to interact with the Hero etc.
    public void Attack(Monster monster) {
        uint heroSkulls = 0;
        uint monsterShields = 0;

        for (int i = 0; i < Hero.Stats.AttackDice; i++) {
            int roll = Dice.Next(1, 7);
            if (roll <= 3) {
                heroSkulls++;
            }
        }

        for (int i = 0; i < monster.Stats.DefenseDice; i++) {
            int roll = Dice.Next(1, 7);
            if (roll == 6) {
                monsterShields++;
            }
        }
        
        if (heroSkulls > monsterShields) {
            uint damage = heroSkulls - monsterShields;
            monster.Stats.Body -= damage;

            WriteLine($"The {monster.Type} received {damage} hit(s).");
        } else {
            WriteLine("Missed.");
        }

        if (monster.Stats.Body <= 0) {
            WriteLine($"{Hero.Name} killed the {monster.Type}.");
        }
    }

    public void Defense(Monster monster) {
        uint heroShields = 0;
        uint monsterSkulls = 0;

        for (int i = 0; i < Hero.Stats.DefenseDice; i++) {
            int roll = Dice.Next(1, 7);
            if (roll == 4 && roll == 5) {
                heroShields++;
            }
        }

        for (int i = 0; i < monster.Stats.AttackDice; i++) {
            int roll = Dice.Next(1, 7);
            if (roll <= 3) {
                monsterSkulls++;
            }
        }

        if (monsterSkulls > heroShields) {
            uint damage = monsterSkulls - heroShields;
            Hero.Stats.Body -= damage;

            WriteLine($"{Hero.Name} received {damage} hit(s).");
        } else {
            WriteLine("Missed.");
        }

        if (Hero.Stats.Body <= 0) {
            WriteLine($"Fuck. {Hero.Name} is unconscious");
        }
    }
}
