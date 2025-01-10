using Personajes;
using MyMaze;
using Obstacles;
using System.Collections;
class Game
{

    public static string[] caraocruz = { "cara", "cruz" };
    public static int inicio;
    public static string previousemojich1 = "⬜";
    public static string previousemojich2 = "⬜";
    public static int initialcoordspl1fil = 0;
    public static int initialcoordspl1col = 0;
    public static int dimension;
    public static int initialcoordspl2fil;
    public static int initialcoordspl2col;
    public static string winner="";

    public static void Main(string[] args)
    {
        
        ObstacleDictionaries obstacleDictionaries = new();
        Console.WriteLine("Bienvenidos al Laberinto Mágico");
        Console.WriteLine("Presione cualquier tecla para continuar (menos Esc)");
        Console.ReadKey();
        Console.WriteLine("¿Desean jugar?");
        Console.WriteLine("1. Sí");
        Console.WriteLine("2. No");
        int opcion = int.Parse(Console.ReadLine());
        if (opcion == 1)
        {
            Console.WriteLine("El laberinto mágico es un juego diseñado para dos personas. Por ahora, díganme sus nombres...");
            Console.WriteLine("Jugador 1, introduzca su nombre:");
            string player1 = Console.ReadLine();
            Console.WriteLine("Jugador 2, introduzca su nombre:");
            string player2 = Console.ReadLine();
            Console.WriteLine($"Bienvenidos a la aventura, {player1} y {player2}");
            Console.WriteLine("El objetivo del juego es llegar hasta la meta antes que su oponente. Por supuesto, encontrarán varios obstáculos en su aventura.");
            Console.WriteLine("Para jugar, pueden escoger entre varios personajes... ¿Quisieran ver la información de los personajes?");
            Console.WriteLine("1. Sí");
            Console.WriteLine("2. No");
            int opcion2 = int.Parse(Console.ReadLine());
            if (opcion2 == 1)
            {
                PersonajesData.ReadPersonajes();
            }
            Console.ReadKey();
            Console.WriteLine("Ahora que conocen los personajes, pueden escoger con cuál desean jugar.");
            Console.WriteLine($"{player1}, seleccione un personaje introduciendo su número:");
            int player1Personaje = int.Parse(Console.ReadLine());
            Character player1character = PersonajesData.personajes[player1Personaje - 1].Item2;

            Console.WriteLine($"{player2}, seleccione un personaje introduciendo su número:");
            int player2Personaje = int.Parse(Console.ReadLine());
            Character player2character = PersonajesData.personajes[player2Personaje - 1].Item2;

            Console.WriteLine("Perfecto, ¿de qué dimensión desean que sea el laberinto?");
            dimension = int.Parse(Console.ReadLine());
            initialcoordspl2fil = dimension - 1;
            initialcoordspl2col = dimension - 1;
            MazeGen mazeGen = new MazeGen(dimension);
            mazeGen.Maze[dimension/2, dimension/2]="🏁";
            bool[,] MazeMask = new bool[dimension, dimension];
            mazeGen.Maze[0, 0] = player1character.Emojiof;
            mazeGen.Maze[dimension - 1, dimension - 1] = player2character.Emojiof;
            mazeGen.DisplayMaze();
            Console.WriteLine("Vamos a empezar...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Decidamos quién empieza jugando...");
            Console.WriteLine($"{player1}, presiona 1 para cara o 2 para cruz:");
            int player1opcioncaraocruz = int.Parse(Console.ReadLine());
            int player2opcioncaraocruz = player1opcioncaraocruz == 1 ? 2 : 1;
            Console.WriteLine($"{player2}, eso te deja con {(player2opcioncaraocruz == 1 ? "cara" : "cruz")}");
            Console.WriteLine("Estamos lanzando la moneda...");
            Console.WriteLine("La moneda ha caído...");
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

            while (winner=="")
            {
                if (inicio == 1)
                {
                    Console.WriteLine($"{player1}, comienza tu turno");
                    Console.WriteLine("Presiona X para tirar el dado🎲 y avanzar");
                    Console.WriteLine("Presiona Z para acceder a las habilidades especiales");
                    
                    var opcion3 = Console.ReadKey();
                    if (opcion3.Key == ConsoleKey.Z)
                    {
                        Console.Clear();
                        player1character.DisplayCharacterStats();
                        Console.WriteLine("Seleccione el número de habilidad que desea usar:");
                        int habilidadnumber=int.Parse(Console.ReadLine());
                        if(player1character.Mana>=player1character.Poderes[habilidadnumber-1].Costomana){
                            player1character.Mana-=player1character.Poderes[habilidadnumber-1].Costomana;
                            //player1character.Poderes[habilidadnumber-1].UsePoder(player1character, player2character);
                        }
                        else
                        {
                          Console.WriteLine("No tienes suficiente mana para utilizar este poder");
                        }
                        Console.WriteLine("Presiona X para continuar a lanzar el dado🎲");
                        opcion3=Console.ReadKey();

                    }

                    if (opcion3.Key == ConsoleKey.X)
                    {
                        Console.Clear();
Random random3 = new Random();
int dado = random3.Next(1, 6);
Console.WriteLine($"El dado ha caído en {dado}🎲");
Console.ReadKey();
mazeGen.DisplayMaze();
for (int i = 0; i < dado; i++)
{
    var key = Console.ReadKey().Key;

    if (key == ConsoleKey.W && initialcoordspl1fil - 1 >= 0 && mazeGen.Maze[initialcoordspl1fil - 1, initialcoordspl1col] != "🌳")
    {
        Console.Clear();
        mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = previousemojich1;
        previousemojich1 = mazeGen.Maze[initialcoordspl1fil - 1, initialcoordspl1col];
        mazeGen.Maze[initialcoordspl1fil - 1, initialcoordspl1col] = player1character.Emojiof;
        initialcoordspl1fil--;
        mazeGen.DisplayMaze();
        Console.WriteLine($"x:{initialcoordspl1fil}, y:{initialcoordspl1col}");
        Console.WriteLine(previousemojich1);

    }
    else if (key == ConsoleKey.S && initialcoordspl1fil + 1 < dimension && mazeGen.Maze[initialcoordspl1fil + 1, initialcoordspl1col] != "🌳")
    {
        Console.Clear();
        mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = previousemojich1;
        previousemojich1 = mazeGen.Maze[initialcoordspl1fil + 1, initialcoordspl1col];
        mazeGen.Maze[initialcoordspl1fil + 1, initialcoordspl1col] = player1character.Emojiof;
        initialcoordspl1fil++;
        mazeGen.DisplayMaze();
        Console.WriteLine($"x:{initialcoordspl1fil}, y:{initialcoordspl1col}");
        Console.WriteLine(previousemojich1);
    }
    else if (key == ConsoleKey.A && initialcoordspl1col - 1 >= 0 && mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col - 1] != "🌳")
    {
        Console.Clear();
        mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = previousemojich1;
        previousemojich1 = mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col - 1];
        mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col - 1] = player1character.Emojiof;
        initialcoordspl1col--;
        mazeGen.DisplayMaze();
        Console.WriteLine($"x:{initialcoordspl1fil}, y:{initialcoordspl1col}");
        Console.WriteLine(previousemojich1);
    }
    else if (key == ConsoleKey.D && initialcoordspl1col + 1 < dimension && mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col + 1] != "🌳")
    {
        Console.Clear();
        mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = previousemojich1;
        previousemojich1 = mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col + 1];
        mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col + 1] = player1character.Emojiof;
        initialcoordspl1col++;
        mazeGen.DisplayMaze();
        Console.WriteLine($"x:{initialcoordspl1fil}, y:{initialcoordspl1col}");
        Console.WriteLine(previousemojich1);
    }
    else
    {
        Console.Clear();
        mazeGen.DisplayMaze();
        Console.WriteLine("La casilla a la que desea moverse no es alcanzable, trate de nuevo");
        Console.ReadKey();
        i--;
    }
    if(previousemojich1!="⬜" && MazeMask[initialcoordspl1fil, initialcoordspl1col]==false)
    {
       MazeMask[initialcoordspl1fil, initialcoordspl1col] = true;
                                
                                if(previousemojich1=="🏁")
                                {
                                    winner=player1;
                                    break;
                                }
                                  else if(previousemojich1=="🔴")
                                {
                                    Random random4= new Random();
                                    int obstaculonpc= random4.Next(1,3);
                                    ObstacleDictionaries.RoundEmojiMeanings[obstaculonpc].DisplayNPCInfo();
                                    player2character.Life+=ObstacleDictionaries.RoundEmojiMeanings[obstaculonpc].Lifealteration;
                                }
                                else 
                                {
                                    bool visitadoadyacentes=false;
                                    foreach(var dir in MazeGen.directions)
                                    {
                                        if(MazeGen.IsInBounds(initialcoordspl1fil+dir.Item1, initialcoordspl1col+dir.Item2) && MazeMask[initialcoordspl1fil+dir.Item1, initialcoordspl1col+dir.Item2]==true)
                                        {
                                        visitadoadyacentes=true;
                                        MazeMask[initialcoordspl1fil, initialcoordspl1col]=true;
                                        break;
                                        }
                                    }
                                    if(!visitadoadyacentes)
                                    {
                                ObstacleDictionaries.SquareEmojiMeanings[previousemojich1].DisplayPlaceInfo();
                                player1character.Life+=ObstacleDictionaries.SquareEmojiMeanings[previousemojich1].Lifealteration;
                                    }
                                }   
    }

}

                    }

                    Console.WriteLine("Presione E para terminar su turno");
                    var key2 = Console.ReadKey();
                    while(key2.Key != ConsoleKey.E)
                    {
                    Console.WriteLine("no puedes hacer mas nada en este turno, presiona E");
                    key2 = Console.ReadKey();
                    }
                    Console.Clear();
                    inicio++;
                
                }

                if (inicio == 2)
                {
                    Console.WriteLine($"{player2} comienza tu turno");
                    Console.WriteLine("Presiona X para tirar el dado y avanzar");
                    Console.WriteLine("Presiona Z para acceder a las habilidades especiales");
                     var opcion3 = Console.ReadKey();
                    if (opcion3.Key == ConsoleKey.Z)
                    {
                        player2character.DisplayCharacterStats();
                        Console.WriteLine("Seleccione el número de habilidad que desea usar:");
                    }

                    if (opcion3.Key == ConsoleKey.X)
                    {
                        Random random3 = new Random();
                        int dado = random3.Next(1, 6);
                        Console.WriteLine($"El dado ha caído en {dado}🎲");
                        Console.ReadKey();
                        mazeGen.DisplayMaze();
                        for (int i = 0; i < dado; i++)
                        {
                            var key = Console.ReadKey().Key;
                            if (key == ConsoleKey.W && initialcoordspl2fil - 1 >= 0 && mazeGen.Maze[initialcoordspl2fil - 1, initialcoordspl2col] != "🌳")
                            {
                                Console.Clear();
                                mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col] = previousemojich2;
                                previousemojich2 = mazeGen.Maze[initialcoordspl2fil - 1, initialcoordspl2col];
                                mazeGen.Maze[initialcoordspl2fil - 1, initialcoordspl2col] = player2character.Emojiof;
                                initialcoordspl2fil--;
                                mazeGen.DisplayMaze();
                            }
                            else if (key == ConsoleKey.S && initialcoordspl2fil + 1 < dimension && mazeGen.Maze[initialcoordspl2fil + 1, initialcoordspl2col] != "🌳")
                            {
                                Console.Clear();
                                mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col] = previousemojich2;
                                previousemojich2 = mazeGen.Maze[initialcoordspl2fil + 1, initialcoordspl2col];
                                mazeGen.Maze[initialcoordspl2fil + 1, initialcoordspl2col] = player2character.Emojiof;
                                initialcoordspl2fil++;
                                mazeGen.DisplayMaze();
                            }
                            else if (key == ConsoleKey.A && initialcoordspl2col - 1 >= 0 && mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col - 1] != "🌳")
                            {
                                Console.Clear();
                                mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col] = previousemojich2;
                                previousemojich2 = mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col - 1];
                                mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col - 1] = player2character.Emojiof;
                                initialcoordspl2col--;
                                mazeGen.DisplayMaze();
                            }
                            else if (key == ConsoleKey.D && initialcoordspl2col + 1 < dimension && mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col + 1] != "🌳")
                            {
                                Console.Clear();
                                mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col] = previousemojich2;
                                previousemojich2 = mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col + 1];
                                mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col + 1] = player2character.Emojiof;
                                initialcoordspl2col++;
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
                            if (previousemojich2 != "⬜" && MazeMask[initialcoordspl2fil, initialcoordspl2col] == false)
                            {
                                MazeMask[initialcoordspl2fil, initialcoordspl2col] = true;
                                
                                if(previousemojich2=="🏁")
                                {
                                    winner=player2;
                                    break;
                                }
                                  else if(previousemojich2=="🔴")
                                {
                                    Random random4= new Random();
                                    int obstaculonpc= random4.Next(1,3);
                                    ObstacleDictionaries.RoundEmojiMeanings[obstaculonpc].DisplayNPCInfo();
                                    player2character.Life+=ObstacleDictionaries.RoundEmojiMeanings[obstaculonpc].Lifealteration;
                                }
                                else 
                                {
                                    bool visitadoadyacentes=false;
                                    foreach(var dir in MazeGen.directions)
                                    {
                                        if(MazeGen.IsInBounds(initialcoordspl2fil+dir.Item1, initialcoordspl2col+dir.Item2) && MazeMask[initialcoordspl2fil+dir.Item1, initialcoordspl2col+dir.Item2]==true)
                                        {
                                        visitadoadyacentes=true;
                                        MazeMask[initialcoordspl2fil, initialcoordspl2col]=true;
                                        break;
                                        }
                                    }
                                    if(!visitadoadyacentes)
                                    {
                                ObstacleDictionaries.SquareEmojiMeanings[previousemojich2].DisplayPlaceInfo();
                                player2character.Life+=ObstacleDictionaries.SquareEmojiMeanings[previousemojich2].Lifealteration;
                                    }
                            }
                            }
                        }
                    }
                     Console.WriteLine("Presione E para terminar su turno");
                    var key2 = Console.ReadKey();
                    while(key2.Key != ConsoleKey.E)
                    {
                    Console.WriteLine("no puedes hacer mas nada en este turno, presiona E");
                     key2 = Console.ReadKey();
                    }
                    Console.Clear();
                    inicio--;
                }
            }
            Console.WriteLine($"¡Felicidades, {winner}! Has ganado la partida.");
        }
        else
        {
            Console.WriteLine("Gracias por jugar. ¡Hasta la próxima!");
        }
    }
}