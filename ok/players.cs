using System;
using System.Collections.Generic;

namespace Personajes
{
    public static class PersonajesData
    {
        public static List<(int, Character)> personajes;

        static PersonajesData()
        {
            List<Poder> podereshumano = new List<Poder>
            {
                new Poder("sanacion por turno", 1, 0, "Puede decidir perder el turno para acampara, sanando 1❤️. No puede usarse dos turnos seguidos")
            };
            List<Poder> poderesdragon = new List<Poder>
            {
                new Poder("armadura", 2, 0, "armadura de 2❤️, solo puedo usarlo una vez en el juego")
            };
            List<Poder> poderesunicornio = new List<Poder>
            {
                new Poder("super sanacion", 2, 0, "puede sanar el doble de corazones")
            };
            List<Poder> poderesdemonio = new List<Poder>();
            List<Poder> poderesangel = new List<Poder>
            {
                new Poder("muerte con azar", 0, 0, "al morir lanza un dado de 3 resultados")
            };
            List<Poder> poderesvampiro = new List<Poder>
            {
                new Poder("absorcion", 1, -1, "cuando se encuentra en la misma fila o columna qu2 otro jugadr le puede absorber 1❤️")
            };
            List<Poder> poderessirena = new List<Poder>
            {
                new Poder("aturdimiento", 0, 0, "al caer en linea con otro jugador lo aturde por un turno")
            };

            personajes = new List<(int, Character)>();
            
                personajes.Add((1, new Character("humano", "🧍", 5, podereshumano)));
                personajes.Add((2, new Character("dragon","🐉", 5, poderesdragon)));
                personajes.Add((3, new Character("unicornio","🦄", 5, poderesunicornio)));
                personajes.Add((4, new Character("demonio","😈", 5, poderesdemonio)));
                personajes.Add((5, new Character("angel","😇", 5, poderesangel)));
                personajes.Add((6, new Character("vampiro","🧛", 5, poderesvampiro)));
                personajes.Add((7, new Character("sirena","🧜", 5, poderessirena)));
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
        public string Emojiof {get; set;}

        public int Life { get; set; }
        public List<Poder> Poderes { get; set; }

        public Character(string name,string emojiof, int life, List<Poder> poderes)
        {
            Emojiof= emojiof;
            Name = name;
            Life = life;
            Poderes = poderes;
        }
        public void DisplayCharacterStats(){
            Console.WriteLine($"{Life}❤️");
            int n=1;
            foreach (var poder in Poderes)
            {
                Console.WriteLine($"{n}- {poder.Nombre}");
                poder.DisplayPoderInfo();
                n++;
            }
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

    public class Poder
    {
        public string Nombre { get; set; }
        public int SelfLifeAlteration { get; set; }
        public int OthersLifeALteration {get; set;}

        public string Description { get; set; }

        public Poder(string nombre, int selflifeAlteration, int othersLifeALteration, string description)
        {
            Nombre = nombre;
            SelfLifeAlteration = selflifeAlteration;
            OthersLifeALteration=othersLifeALteration;
            Description = description;
        }

        public void DisplayPoderInfo()
        {
            Console.WriteLine(Description);
        }
    }
}
