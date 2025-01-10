using System;
using System.Collections.Generic;

namespace Obstacles
{
    public class Obstacle
    {
        public string Name { get; private set; }
        public int Lifealteration { get; set; }
        public string Description { get; private set; }

        public Obstacle(string name, int lifealteration, string description)
        {
            Name = name;
            Lifealteration = lifealteration;
            Description = description;
        }

        public void DisplayPlaceInfo()
        {
            Console.WriteLine($"Has llegado a {Name}!!!");
            Console.WriteLine(Description);
            if (Lifealteration < 0)
            {
                Console.WriteLine($"Oh no, tu vida se ha visto afectada {Lifealteration}❤️");
            }
            else if (Lifealteration == 0)
            {
                Console.WriteLine("tu vida no se ha visto alterada");
            }
            else
            {
                Console.WriteLine($"Wow, tu vida se ha visto afectada... positivamente!! +{Lifealteration}❤️");
            }
        }

        public void DisplayNPCInfo()
        {
            Console.WriteLine($"Te has encontrado a {Name}!!!");
            Console.WriteLine(Description);
            if (Name == "una bruja")
            {
                Console.WriteLine("la bruja te ha retado a un juego de piedra, papel, tijera, lagarto, spock, por si no sabes jugar ahi te van las reglas");
                Console.WriteLine("las tijeras cortan el papel");
                Console.WriteLine("el papel cubre la piedra");
                Console.WriteLine("la piedra aplasta al lagarto");
                Console.WriteLine("el lagarto envenena a spock");
                Console.WriteLine("spock destroza las tijeras");
                Console.WriteLine("las tijeras decapitan al lagarto");
                Console.WriteLine("el lagarto se come al papel");
                Console.WriteLine("el papel refuta a spock");
                Console.WriteLine("spock vaporiza la piedra");
                Console.WriteLine("y como siempre, la piedra aplasta a las tijeras");
                Console.WriteLine("Que vas a jugar?");
                Console.WriteLine("1. papel🖐  2. piedra✊  3. tijera✌️  4. spock🖖  5. lagarto🫳");
                int choice = int.Parse(Console.ReadLine());
                Lifealteration = Minigame(choice);
                if (Lifealteration == 1)
                {
                    Console.WriteLine("Has ganado!!");
                    Console.WriteLine("🧙: No esperaba perder contra ti, supongo que te puedo ayudar con tu viaje");
                    Console.WriteLine("felicidades has ganado +1❤️");
                }
                else if (Lifealteration == 0)
                {
                    Console.WriteLine("Has empatado!!");
                    Console.WriteLine("🧙:Es un empate... que aburrido, bueno sigue con tu camino. No tengo mas nada que decirte");
                }
                else if (Lifealteration == -1)
                {
                    Console.WriteLine("Has perdido!!");
                    Console.WriteLine("🧙:Jajaja, sabia que ibas a perder, ya no me siento indecisa");
                    Console.WriteLine("Oh no!!! Parece que la bruja te ha lanzado un hechizo... has perdido 1❤️");
                }
            }
            else if (Lifealteration < 0)
            {
                Console.WriteLine($"Oh no, tu vida se ha visto afectada {Lifealteration}❤️");
            }
            else
            {
                Console.WriteLine($"Wow, tu vida se ha visto afectada... positivamente!! +{Lifealteration}❤️");
            }
        }

        private int Minigame(int choice)
        {
            Random random = new Random();
            int computer = random.Next(1, 6);
            if (choice == 1 && computer == 2) return 1;
            if (choice == 1 && computer == 3) return -1;
            if (choice == 1 && computer == 4) return 1;
            if (choice == 1 && computer == 5) return -1;
            if (choice == 2 && computer == 1) return -1;
            if (choice == 2 && computer == 3) return 1;
            if (choice == 2 && computer == 4) return -1;
            if (choice == 2 && computer == 5) return 1;
            if (choice == 3 && computer == 1) return 1;
            if (choice == 3 && computer == 2) return -1;
            if (choice == 3 && computer == 4) return -1;
            if (choice == 3 && computer == 5) return 1;
            if (choice == 4 && computer == 1) return -1;
            if (choice == 4 && computer == 2) return 1;
            if (choice == 4 && computer == 3) return 1;
            if (choice == 4 && computer == 5) return -1;
            if (choice == 5 && computer == 1) return 1;
            if (choice == 5 && computer == 2) return -1;
            if (choice == 5 && computer == 3) return -1;
            if (choice == 5 && computer == 4) return 1;
            return 0;
        }
    }

    public class ObstacleDictionaries
    {
        public static Dictionary<int, Obstacle> RoundEmojiMeanings { get; set; }
        public static Dictionary<string, Obstacle> SquareEmojiMeanings { get; set; }

        public ObstacleDictionaries()
        {
            RoundEmojiMeanings = new Dictionary<int, Obstacle>
            {
                {1, new Obstacle("un lobo", -1, "🐺:que haces por aqui tan solo...") },
                {2, new Obstacle("una bruja", 0, "🧙:hoy me siento indecisa... vamos a echarlo a piedra, papel, tijera, lagarto, spock")},
                {3, new Obstacle("un hada", 1, "🧚:tienes que tener cuidado... dejame ayudarte un poco")}
            };

            SquareEmojiMeanings = new Dictionary<string, Obstacle>
            {
                { "🏘️", new Obstacle("una ciudad", 0, "las ciudades son lugares seguros, no pasa nada muy interesante...") },
                { "🏚️", new Obstacle("unas ruinas", -4, "las ruinas pueden ser muy peligrosas, cuidado no te vaya a caer ese techo arriba... tarde") },
                { "⛪", new Obstacle("un santuario", 2, "es un buen momento para un poco de relajacion espiritual, no sientes la vida volver a ti...?") },
                { "🌼", new Obstacle("una pradera", 1, "las praderas son un buen lugar para descansar y recuperar un poco de vida") },
                { "🟦", new Obstacle("un lago", -3, "los lagos pueden ser una vista muy bonita, pero quizas no sea tan buena idea pasar por ahi...")},
                { " 🪦", new Obstacle("un cementerio", -1, "los cementerios son lugares verdaderamente aterradores... alguien mas vio eso moverse?💀")}
            };
        }
    }
}
