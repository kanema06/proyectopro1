﻿using System;
using System.Collections.Generic;

namespace Personajes
{
    public static class PersonajesData
    {
        public static List<(int, Character)> personajes;
        
        public static Random random=new();



        static PersonajesData()
        {
           Poder genericpower1= new Poder("Leche Mu-Mu", 8, 1, 0, 3, "Puede curar 1❤️");
           Poder genericpower2= new Poder("trampa magica", 9, 0, -5, 2, "Le provoca un daño de 1❤️ a su oponente si pasa por la trampa");
           Poder genericpower3= new Poder("polvo de hadas", 10, -3, 0, -3, "A cambio de un 1❤️ recupera 3 de mana🪄");
            List<Poder> podereshumano = new List<Poder>
            {
                new Poder("intercambio",1, -2, -1, 1, "Puede quitarse 2❤️ para hacerle a su enemigo un daño de 1❤️"),

                genericpower1,
                genericpower2,
                genericpower3

            };
            List<Poder> poderesdragon = new List<Poder>
            {
                new Poder("escamas de dragon",2, 2, 0, 5, "Gracias a su fuerte armadura de escamas los ataques le infligen 1❤️ menos"),
                genericpower1,
                genericpower2,
                genericpower3
            };
            List<Poder> poderesunicornio = new List<Poder>
            {
                new Poder("super curacion",3, 2, 0, 5, "Puede sanar 3❤️"),
                genericpower1,
                genericpower2,
                genericpower3
            };
            List<Poder> poderesdemonio = new List<Poder>
            {
                new Poder("daño con azar", 7, 0, -random.Next(0, 4), 5, "Puede hacer un daño desde 1❤️ hasta 3❤️ al enemigo, no se puede elegir el daño, es al azar"),
                genericpower1,
                genericpower2,
                genericpower3
            };
            List<Poder> poderesangel = new List<Poder>
            {
                new Poder("suerte de morir",4, 0, -4, 2, "si mueres tu oponente pierde 3❤️"),
                genericpower1,
                genericpower2,
                genericpower3

            };
            List<Poder> poderesvampiro = new List<Poder>
            {
                new Poder("alimentacion",5, 1, -1, 5, "le puedes absorber 2❤️ a tu oponente"),
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
           




            personajes = new List<(int, Character)>();
            
                personajes.Add((1, new Character("humano",(0,0), "🧍",5, 5, podereshumano)));
                personajes.Add((2, new Character("dragon",(0,0),"🐉",5, 5, poderesdragon)));
                personajes.Add((3, new Character("unicornio",(0,0),"🦄",5, 5, poderesunicornio)));
                personajes.Add((4, new Character("demonio",(0,0),"😈",5, 5, poderesdemonio)));
                personajes.Add((5, new Character("angel",(0,0),"😇",5, 5, poderesangel)));
                personajes.Add((6, new Character("vampiro",(0,0),"🧛",5, 5, poderesvampiro)));
                personajes.Add((7, new Character("sirena",(0,0),"🧜",5, 5, poderessirena)));
        }

        public static void ReadPersonajes()
        {
            foreach (var pers in personajes)
            {
                Console.WriteLine($"{pers.Item1}");
                pers.Item2.DisplayCharacterInfo();
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
       /* public void playermovement(int dimension, int x, int y, Character personajeamover, string[,] milaberinto, ConsoleKey destino)
        {
         if(destino==ConsoleKey.W && puedemoversea(dimension, milaberinto, x, y, destino))
            {
            string previoousemoji;
            if(milaberinto[x-1, y]!=personajeamover.Emojiof)previoousemoji = milaberinto[x-1, y];

            milaberinto[x-1, y]=personajeamover.Emojiof;

            }
            else if(destino==ConsoleKey.S && x+1<dimension && milaberinto[x+1, y]!="🌳")
            {
                return true;
            }
            else if(destino==ConsoleKey.A && y-1>=0 && milaberinto[x, y-1]!="🌳")
            {
                return true;
            }
            else if(destino==ConsoleKey.D && y+1<dimension && milaberinto[x, y+1]!="🌳")
            {
                return true;
            }
        }
        public bool puedemoversea(int dimension, string[,] milaberinto, int x, int y, ConsoleKey destino)
        {
            if(destino==ConsoleKey.W && x-1>=0 && milaberinto[x-1, y]!="🌳")
            {
            return true;
            }
            else if(destino==ConsoleKey.S && x+1<dimension && milaberinto[x+1, y]!="🌳")
            {
                return true;
            }
            else if(destino==ConsoleKey.A && y-1>=0 && milaberinto[x, y-1]!="🌳")
            {
                return true;
            }
            else if(destino==ConsoleKey.D && y+1<dimension && milaberinto[x, y+1]!="🌳")
            {
                return true;
            }
            return false;
        }*/
       
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
        public void DisplayPoderInfo()
        {
            Console.WriteLine($"Costo de mana:{Costomana}");
            Console.WriteLine(Description);
        }
         
    }
}
