using System;
using System.Threading;

namespace True_Evolution_CLI
{
    class Program
    {        
        static Animal playerAnimal;

        static void Start(string startMessage)
        {
            Console.WriteLine("Press any key to start new world");
            Console.ReadKey(true);            
            playerAnimal = new Animal(10, 10, 2, "Squi", "rrel", true, 0, 5);
            Console.WriteLine($"\n{startMessage}");
        }

        static void LifeCheck()
        {
            if(playerAnimal.health <= 0)
            {
                Console.WriteLine("You're extinct.");
                Main();
            }
        }

        static bool Befriend(Animal animal)
        {
            Random rnd = new Random();
            int playerAnimalPower = rnd.Next(playerAnimal.charisma);
            int EncounterAnimalPower = rnd.Next(animal.angryLevel);
            if (playerAnimalPower >= EncounterAnimalPower)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void Mate(Animal animal)
        {
            Random rnd = new Random();
            int chance = rnd.Next(1);
            int health = chance == 1 ? playerAnimal.maxHealth : animal.maxHealth;
            int damage = chance == 1 ? animal.damage : playerAnimal.damage;
            int charisma = playerAnimal.charisma;
            string firstHalfName = chance == 1 ? animal.firstHalfName : playerAnimal.firstHalfName;
            string secondHalfName = chance == 1 ? playerAnimal.secondHalfName: animal.secondHalfName;

            string playerAnimalPreviousName = playerAnimal.GetName();
            string animalName = animal.GetName();
            ;
            playerAnimal = new Animal(health, health, damage, firstHalfName, secondHalfName, false, 0, charisma);

            if (chance == 1)
            {
                Console.WriteLine($"{playerAnimal.GetName()} has been born. This is now your main animal\n it has inherited health from {playerAnimalPreviousName} and damage from {animalName}");
            }
            else if (chance == 0)
            {
                Console.WriteLine($"{playerAnimal.GetName()} has been born. This is now your main animal\n it has inherited health from {animalName} and damage from {playerAnimalPreviousName}");
            }
    
            Thread.Sleep(1500);
            Console.WriteLine($"{playerAnimal.GetName()} stats: health: {playerAnimal.health}, damage: {playerAnimal.damage}, charisma: {playerAnimal.charisma}");
            Thread.Sleep(500);
            Console.WriteLine("OK [Enter]");
            Console.ReadLine();
        }

        static void Attacked(Animal animal)
        {
            Thread.Sleep(1500);
            Random multiplyerRandom = new Random();
            int multiplyer = multiplyerRandom.Next(3);
            playerAnimal.health = playerAnimal.health - animal.damage * multiplyer;
            int healthLeft = playerAnimal.health < 0 ? 0 : playerAnimal.health;
            if (multiplyer == 0)
            {
                Console.WriteLine($"{animal.GetName()} tripped and dealt no damage\n");
            }
            if (multiplyer == 1)
            {
                Console.WriteLine($"Weak hit. {animal.GetName()} hit you with {multiplyer * animal.damage}. You have {healthLeft} health left.\n");
            }
            else if (multiplyer == 2)
            {
                Console.WriteLine($"Regular hit. {animal.GetName()} hit you with {multiplyer * animal.damage}. You have {healthLeft} health left.\n");
            }
            else if (multiplyer == 3)
            {
                Console.WriteLine($"Critical hit! {animal.GetName()} hit you with {multiplyer * animal.damage}. You have {healthLeft} health left.\n");
            }
            LifeCheck();
            Thread.Sleep(1500);
        }

        static void Attack(Animal animal)
        {            
            Random multiplyerRandom = new Random();
            int multiplyer = multiplyerRandom.Next(3);
            animal.health = animal.health - playerAnimal.damage * multiplyer;           
            if (multiplyer == 0)
            {
                Console.WriteLine($"Trip. You tripped and dealt no damage\n");
            }
            
            if (multiplyer == 1)
            {
                if (animal.health <= 0)
                {
                    Console.WriteLine($"{animal.GetName()} was killed with a weak hit.\n");
                } else
                {
                    Console.WriteLine($"Weak hit. You hit {animal.GetName()} with {multiplyer * playerAnimal.damage}. {animal.GetName()} has {animal.health} health left.\n");
                }
                
            }
            else if (multiplyer == 2)
            {
                if (animal.health <= 0)
                {
                    Console.WriteLine($"{animal.GetName()} was killed with a regular hit.\n");
                } else
                {
                    Console.WriteLine($"Regular hit. You hit {animal.GetName()} with {multiplyer * playerAnimal.damage}. {animal.GetName()} has {animal.health} health left.\n");
                }
                
            }
            else if (multiplyer == 3)
            {
                if (animal.health <= 0)
                {
                    Console.WriteLine($"{animal.GetName()} was killed with a critical hit.\n");
                } else
                {
                    Console.WriteLine($"Critical hit! You hit {animal.GetName()} with {multiplyer * playerAnimal.damage}. {animal.GetName()} has {animal.health} health left.\n");
                }
            }

            if (animal.health <= 0)
            {
                Thread.Sleep(1500);
                playerAnimal.health = playerAnimal.maxHealth;
                Console.WriteLine("You have 1 new skillpoint to spend. Increase your [H]Health, [D]Damage or [C]Charisma\n (Charisma will increase your chanses when paying respect or mating)");
                var input = Console.ReadKey(true).Key.ToString().ToUpper();               
                if (input == "H")
                {
                    playerAnimal.health++;
                    Console.WriteLine($"Your health-level is now {playerAnimal.health}\n");
                }
                else if (input == "D")
                {
                    playerAnimal.damage++;
                    Console.WriteLine($"Your damage-level is now {playerAnimal.damage}\n");
                }
                else if (input == "C")
                {
                    playerAnimal.charisma++;
                    Console.WriteLine($"Your charisma-level is now {playerAnimal.charisma}\n");
                }
                Thread.Sleep(500);
                Console.WriteLine("OK [Enter]");
                Console.ReadLine();
            }
            else
            {
                Thread.Sleep(500);
                Console.WriteLine("OK [Enter]");
                Console.ReadLine();
                Attacked(animal);
            }
            
            
        }

        static void Encounter(Animal animal)
        {
            //You
            Console.WriteLine($"A wild {animal.GetName()} appeared!");
            Thread.Sleep(500);
            Console.WriteLine($"Your stats:");
            Thread.Sleep(100);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Health: {playerAnimal.health}");
            Thread.Sleep(100);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Damage: {playerAnimal.damage}");
            Thread.Sleep(100);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Charisma: {playerAnimal.charisma}");
            Thread.Sleep(100);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("(Charisma will increase your chances when paying respect or mating. Equal anger and charisma will give e 50 % success rate)\n");
            Console.ForegroundColor = ConsoleColor.White;
            //Enemy
            Thread.Sleep(1000);
            Console.WriteLine($"{animal.GetName()} stats:");
            Thread.Sleep(100);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Health: {animal.health}");
            Thread.Sleep(100);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Damage: {animal.damage}");
            Thread.Sleep(100);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Anger: {animal.angryLevel}");
            Thread.Sleep(100);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("(Anger will decrease your chances when paying respect or mating.");
            Console.ForegroundColor = ConsoleColor.White;

            //Console.WriteLine($"{}Health: {playerAnimal.health}, Damage: {playerAnimal.damage}, Charisma: {playerAnimal.charisma} (Charisma will increase your chances when paying respect or mating. Equal anger and charisma will give e 50% success rate)");
            //Thread.Sleep(100);
            //Console.WriteLine($"{animal.GetName()} stats:");
            //Thread.Sleep(100);
            //Console.WriteLine($"Health: {animal.health}, Damage: {animal.damage}, Anger: {animal.angryLevel} (Anger will decrease your chances when paying respect or mating.)");
            //Thread.Sleep(1500);
            while(animal.health > 0)
            {                
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Press 'F' to pay respects, 'A' to attack or 'M' to mate\n");                
                Console.ForegroundColor = ConsoleColor.White;
                var input = Console.ReadKey(true);
                if (input.KeyChar.ToString().ToUpper() == "F")
                {
                    if(Befriend(animal))
                    {
                        Console.WriteLine($"You payed respects and {animal.GetName()} showed you mercy\n");
                        break;
                    } else
                    {
                        Console.WriteLine($"You payed respects but {animal.GetName()} dont give a shit\n");
                        Attacked(animal);
                        continue;
                    }                    
                }
                if (input.KeyChar.ToString().ToUpper() == "M")
                {                  
                    if (Befriend(animal))
                    {
                        Mate(animal);
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"{animal.GetName()} did not want to mate");
                        Attacked(animal);
                        continue;
                    }
                    
                }
                if (input.KeyChar.ToString().ToUpper() == "A")
                {
                    Attack(animal);
                    continue;   
                }
                Console.WriteLine("Valid keys are 'F', 'M' or 'A'\n");
                continue;
            }
           
            
        }

        static void Main()
        {                       
            Start("A new life begins\n");
            Thread.Sleep(1500);            
            Animal ant = new Animal(1, 1, 2, "An", "t", false, 7);
            Encounter(ant);           
            Animal duck = new Animal(6, 6, 3, "Du", "ck", false, 5);
            Encounter(duck);
            Animal snake = new Animal(2, 2, 5, "Sna", "ke", false, 7);       
            Encounter(snake);
            Animal birb = new Animal(2, 2, 1, "Bi", "rb", false, 1);
            Encounter(birb);           
            Animal doge = new Animal(8, 8, 4, "Do", "ge", false, 1);
            Encounter(doge);
            Animal honeybadger = new Animal(16, 16, 1, "Honey", "Badger", false, 1000);
            Encounter(honeybadger);
            Animal spider = new Animal(2, 2, 100, "Spi", "da", false, 9);
            Encounter(spider);
            Animal kangaroo= new Animal(14, 14, 6, "Kanga", "roo", false, 4);
            Encounter(kangaroo);
            Animal bear = new Animal(16, 16, 5, "Be", "ar", false, 5);
            Encounter(bear);
            Console.WriteLine("Congraulations, you are on top of the foodchain");
        }
    }
}
