using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Personajes
{
    public static class PersonajesData
    {
        public static List <string> personajesemojis;
        public static List<(int, Character)> personajes;
        
        public static Random random=new();
         public static Poder genericpower1= new Poder("Leche Mu-Mu", 8, 1, 0, 3, "Puede curar 1❤️");
         public static Poder genericpower2= new Poder("trampa magica", 9, 0, -5, 3, "Le provoca un daño de 5❤️ a su oponente si pasa por la trampa");
         public static Poder genericpower3= new Poder("polvo de hadas", 10, -3, 0, -3, "A cambio de un 3❤️ recupera 3 de mana🪄");



        static PersonajesData()
        {
           
            List<Poder> podereshumano = new List<Poder>
            {
                new Poder("intercambio",1, -1, -2, 3, "Puede quitarse 1❤️ para hacerle a su enemigo un daño de 2❤️"),

                genericpower1,
                genericpower2,
                genericpower3

            };
            List<Poder> poderesdragon = new List<Poder>
            {
                new Poder("escamas de dragon",2, 2, 0, 5, "Activa su fuerte armadura de escamas y los ataques de sus enemigos no le provocan ningun  daño"),

                genericpower1,
                genericpower2,
                genericpower3
            };
            List<Poder> poderesunicornio = new List<Poder>
            {
                new Poder("super curacion",3, 3, 0, 5, "Puede sanar 3❤️"),
                genericpower1,
                genericpower2,
                genericpower3
            };
            List<Poder> poderesdemonio = new List<Poder>
            {
                new Poder("daño con azar", 7, 0, -random.Next(0, 4), 5, "Puede hacer un daño desde 0❤️ hasta 3❤️ al enemigo, no se puede elegir el daño, es al azar"),
                genericpower1,
                genericpower2,
                genericpower3
            };
            List<Poder> poderesangel = new List<Poder>
            {
                new Poder("suerte de morir",4, 0, -4, 2, "si mueres tu oponente pierde 4❤️"),
                genericpower1,
                genericpower2,
                genericpower3

            };
            List<Poder> poderesvampiro = new List<Poder>
            {
                new Poder("alimentacion",5, 1, -1, 5, "le puedes absorber 1❤️ a tu oponente"),
                genericpower1,
                genericpower2,
                genericpower3
            };
            List<Poder> poderessirena = new List<Poder>
            {
                new Poder("aturdimiento",6, 0, 0, 4, "con su voz puede aturdir a su oponente por un turno"),
                genericpower1,
                genericpower2,
                genericpower3
            };
           


            personajesemojis=new List<string>();
            personajesemojis.Add("🐉");
            personajesemojis.Add("🦄");
            personajesemojis.Add("😈");
            personajesemojis.Add("🧍");
            personajesemojis.Add("😇");
            personajesemojis.Add("🧛");
            personajesemojis.Add("🧜");

            personajes = new List<(int, Character)>();
            
                personajes.Add((1, new Character("humano",(1,1), "🧍",5, 5, podereshumano)));
                personajes.Add((2, new Character("dragon",(1,1),"🐉",5, 5, poderesdragon)));
                personajes.Add((3, new Character("unicornio",(1,1),"🦄",5, 5, poderesunicornio)));
                personajes.Add((4, new Character("demonio",(1,1),"😈",5, 5, poderesdemonio)));
                personajes.Add((5, new Character("angel",(1,1),"😇",5, 5, poderesangel)));
                personajes.Add((6, new Character("vampiro",(1,1),"🧛",5, 5, poderesvampiro)));
                personajes.Add((7, new Character("sirena",(1,1),"🧜",5, 5, poderessirena)));
        }

        public static void ReadPersonajes()
        {
            foreach (var pers in personajes)
            {
                Console.WriteLine($"{pers.Item1}.{pers.Item2.Name}{pers.Item2.Emojiof}");
                
            }
        }
        public static void ReadPersonajesInfo()
        {
          foreach (var pers in personajes)
            {
                Console.WriteLine($"{pers.Item1}.{pers.Item2.Name}{pers.Item2.Emojiof}");
                pers.Item2.DisplayCharacterInfoo();
            }  
        }
    }

    public class Character
    {
        public string Name { get; set; }
        public (int,int) Start{get; set;}
        public string Emojiof {get; set;}
        public int Mana {get; set;}

        public int Life { get; set; }
        public List<Poder> Poderes { get; set; }

        public Character(string name,(int,int)start,string emojiof, int mana, int life, List<Poder> poderes)
        {
            Emojiof= emojiof;
            Name = name;
            Start=start;
            Mana=mana;
            Life = life;
            Poderes = poderes;
        }
     
        public void ClearStats()
        {
            Mana=5;
            Life=5;
            
        }
        public void DisplayCharacterInfo()
        {
            Console.WriteLine($"{Emojiof}.Nombre: {Name}, Vida: {Life}❤️, Poderes:");
            foreach (var poder in Poderes)
            {
                Console.WriteLine($"- {poder.Nombre}");
                poder.DisplayPoderInfo();
            }
        }
        public void DisplayCharacterInfoo()
        {
            Console.WriteLine($"{Emojiof}.Nombre: {Name}, Vida: {Life}❤️, Poderes:");
            Poderes[0].DisplayPoderInfo();
        }
    }

    public class Poder(string nombre, int poderid, int selflifeAlteration, int othersLifeALteration, int costomana, string description)
    {
        public string Nombre { get; set; } = nombre;
        public int PoderID { get; set; } = poderid;

        public int Costomana { get; set; } = costomana;
        public int SelfLifeAlteration { get; set; } = selflifeAlteration;
        public int OthersLifeALteration { get; set; } = othersLifeALteration;

        public string Description { get; set; } = description;
        public static bool escamasdedragon;
        public static bool angelmuerte;
        public static bool sirenacanto;
        public void DisplayPoderInfo()
        {
            Console.WriteLine($"Poder: {Nombre}");
            Console.WriteLine($"Costo de mana:{Costomana}");
            Console.WriteLine(Description);
        }
         
    }
}
