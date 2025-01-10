using Personajes;
using MyMaze;

class Game
{
    public static string[] caraocruz = { "cara", "cruz" };
    public static int inicio;
    public static string previousemoji = "⬜";
    public static int initialcoordspl1fil = 0;
    public static int initialcoordspl1col = 0;
    public static int dimension;
    public static int initialcoordspl2fil;
    public static int initialcoordspl2col;

    public static void Main(string[] args)
    {
        Console.WriteLine("Bienvenidos al Laberinto Magico");
        Console.WriteLine("presione cualquier tecla para continuar(menos Esc)");
        Console.ReadKey();
        Console.WriteLine("¿Desean jugar?");
        Console.WriteLine("1. Si");
        Console.WriteLine("2. No");
        int opcion = int.Parse(Console.ReadLine());
        if (opcion == 1)
        {
            Console.WriteLine("El laberinto magico es un juego disennado para dos personas, por ahora diganme sus nombres...");
            Console.WriteLine("Jugador 1 introduzca su nombre");
            string player1 = Console.ReadLine();
            Console.WriteLine("Jugador 2 introduzca su nombre");
            string player2 = Console.ReadLine();
            Console.WriteLine($"Bienvenidos a la aventura {player1} y {player2}");
            Console.WriteLine("El objetivo del juego es llegar hasta la meta antes que su oponente, por supuesto encontraran varios obstaculos en su aventura, ");
            Console.WriteLine("para jugar pueden escoger entre varios personajes... Quisieran ver la informacion de los personajes?");
            Console.WriteLine("1. Si");
            Console.WriteLine("2. No");
            Console.Clear();
            int opcion2 = int.Parse(Console.ReadLine());
            if (opcion2 == 1)
            {
                PersonajesData.ReadPersonajes();
            }
            Console.ReadKey();
            Console.WriteLine("Ahora que conocen los personajes, pueden escoger auqe con el que desean jugar");
            Console.WriteLine($"{player1} seleccione  un personaje introduciendo su numero");
            int player1Personaje = int.Parse(Console.ReadLine());
            Character player1character = PersonajesData.personajes[player1Personaje - 1].Item2;

            Console.WriteLine($"{player2} seleccione un personaaje introduciendo su numero");
            int player2Personaje = int.Parse(Console.ReadLine());
            Character player2character = PersonajesData.personajes[player2Personaje - 1].Item2;

            Console.WriteLine("Perfecto, ahora de que dimension desean que sea el laberinto?");
            dimension = int.Parse(Console.ReadLine());
            initialcoordspl2fil = dimension - 1;
            initialcoordspl2col = dimension - 1;
            MazeGen mazeGen = new MazeGen(dimension, dimension);
            bool[,] MazeMask = new bool[dimension, dimension];
            mazeGen.GenerateMaze();
            mazeGen.PlaceGoal();
            mazeGen.Maze[0, 0] = player1character.Emojiof;
            mazeGen.Maze[dimension - 1, dimension - 1] = player2character.Emojiof;
            mazeGen.PlaceEmojis();
            mazeGen.DisplayMaze();
            Console.WriteLine("Vamos a empezar...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Decidamos quien empieza jugando...");
            Console.WriteLine($"{player1} presiona 1 para cara o 2 para cruz");
            int player1opcioncaraocruz = int.Parse(Console.ReadLine());
            int player2opcioncaraocruz = player1opcioncaraocruz == 1 ? 2 : 1;
            Console.WriteLine($"{player2} eso te deja con {(player2opcioncaraocruz == 1 ? "cara" : "cruz")}");
            Console.WriteLine("Estamos lanzando la moneda...");
            Console.WriteLine("La moneda ha caido...");
            Random random2 = new Random();
            int seleccioncaraocruz = random2.Next(1, 3);
            if (seleccioncaraocruz == player1opcioncaraocruz)
            {
                Console.WriteLine("La moneda ha salido cara");
                Console.WriteLine($"{player1} empieza jugando");
                inicio = 1;
            }
            else
            {
                Console.WriteLine($"La moneda ha salido {caraocruz[seleccioncaraocruz - 1]}");
                Console.WriteLine($"{player2} empieza jugando");
                inicio = 2;
            }

            while (!MazeMask[dimension / 2, dimension / 2])
            {
                if (inicio == 1)
                {
                    Console.WriteLine($"{player1} comienza tu turno");
                    Console.WriteLine("Presiona X para tirar el dado y avanzar");
                    Console.WriteLine("Presione z para acceder a las habilidades especiales");
                    string opcion3 = Console.ReadLine();

                    if (opcion3=="z")
                    {
                        player1character.DisplayCharacterStats(); 
                        Console.WriteLine("Seleccione el numero de habilidad que desea usar");
                        
                    }


                    if (opcion3 == "x")
                    {
                        Random random3 = new Random();
                        int dado = random3.Next(1, 7);
                        Console.WriteLine($"El dado ha caido en {dado}");
                        Console.ReadKey();
                        for (int i = 0; i < dado; i++)
                        {
                            Console.Clear();
                            Console.WriteLine($"Tienes {dado-i} movimientos");
                            mazeGen.DisplayMaze();
                            var key = Console.ReadKey().Key;
                            if (key == ConsoleKey.W && initialcoordspl1fil - 1 >= 0 && mazeGen.Maze[initialcoordspl1fil - 1, initialcoordspl1col] != "🌳")
                            {
                                Console.Clear();
                                mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = previousemoji;
                                previousemoji = mazeGen.Maze[initialcoordspl1fil - 1, initialcoordspl1col];
                                mazeGen.Maze[initialcoordspl1fil - 1, initialcoordspl1col] = player1character.Emojiof;
                                initialcoordspl1fil--;
                                mazeGen.DisplayMaze();
                            }
                            else if (key == ConsoleKey.S && initialcoordspl1fil + 1 < dimension && mazeGen.Maze[initialcoordspl1fil + 1, initialcoordspl1col] != "🌳")
                            {
                                Console.Clear();
                                mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = previousemoji;
                                previousemoji = mazeGen.Maze[initialcoordspl1fil + 1, initialcoordspl1col];
                                mazeGen.Maze[initialcoordspl1fil + 1, initialcoordspl1col] = player1character.Emojiof;
                                initialcoordspl1fil++;
                                mazeGen.DisplayMaze();
                            }
                            else if (key == ConsoleKey.A && initialcoordspl1col - 1 >= 0 && mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col-1] != "🌳")
                            {
                                Console.Clear();
                                mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = previousemoji;
                                previousemoji = mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col-1];
                                mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col-1] = player1character.Emojiof;
                                initialcoordspl1col--;
                                mazeGen.DisplayMaze();
                            }
                            else if (key == ConsoleKey.D && initialcoordspl1col + 1 < dimension && mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col+1] != "🌳")
                            {
                                Console.Clear();
                                mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = previousemoji;
                                previousemoji = mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col+1];
                                mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col+1] = player1character.Emojiof;
                                initialcoordspl1col++;
                                mazeGen.DisplayMaze();
                            }
                            else
                            {
                                Console.Clear();
                                mazeGen.DisplayMaze();
                                Console.WriteLine("La casilla a la que desea moverse no es alcanzable, trate de nuevo");
                                Console.ReadKey();
                                i--;
                            }
                            if(mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col]!= "⬜" && MazeMask[initialcoordspl1fil, initialcoordspl1col]==false)
                            {
                             //obstaculos
                             MazeMask[initialcoordspl1fil, initialcoordspl1col]=true;
                            }
                        }
                    }
                    Console.WriteLine("Presione E para terminar su turno");
                    var key2=Console.ReadKey();
                    if (key2.Key == ConsoleKey.E)
                    {
                        Console.Clear();
                        inicio++;
                    }

                }



                if (inicio==2)
                {

                }
            }
        }
    }
}
