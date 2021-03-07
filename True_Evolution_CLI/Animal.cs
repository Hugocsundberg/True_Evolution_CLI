using System;
namespace True_Evolution_CLI
{
    public class Animal
    {
        public int health;
        public int maxHealth;
        public int damage;
        public int angryLevel;
        public int charisma;
        public string firstHalfName;
        public string secondHalfName;

        public Animal(int health, int maxHealth, int damage, string firstHalfName, string secondHalfName, bool announceBirth = true, int angryLevel = 5, int charisma = 5)
        {
            this.health = health;
            this.maxHealth = maxHealth;
            this.angryLevel = angryLevel;
            this.damage = damage;
            this.charisma = charisma;
            this.firstHalfName = firstHalfName;
            this.secondHalfName = secondHalfName;
            if(announceBirth == true)
            {
                Console.WriteLine($"\nA {firstHalfName + secondHalfName} has been born!");
            }
        }

        public string GetName()
        {
            return firstHalfName + secondHalfName;
        }
    }
}
