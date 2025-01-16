using Personajes;
using MyMaze;
using Obstacles;
using System.Collections;
class Game
{

    #region variables y metodos
    public static void DisplayCharacterStats(Character personaje)
    {
        Console.WriteLine($"vida:{personaje.Life}❤️        mana:{personaje.Mana}🪄");
    }
     public static void UsePoder(int numhab, Character pers1,  Character pers2, bool mov, string[,] milaberinto, int dimension, int x, int y,string previoousemoji1, string previoousemoji2, int x2, int y2, string namepers1, string namepers2)
        {
            Console.WriteLine($"Acabas de usar {pers1.Poderes[numhab-1].Nombre}");
            pers1.Poderes[numhab-1].DisplayPoderInfo();
            if (pers1.Poderes[numhab-1].PoderID==1 ||  pers1.Poderes[numhab-1].PoderID==5 ||  pers1.Poderes[numhab-1].PoderID==7 ||  pers1.Poderes[numhab-1].PoderID==8 ||  pers1.Poderes[numhab-1].PoderID==10)
            {
             pers1.Life+=pers1.Poderes[numhab-1].SelfLifeAlteration;
             pers2.Life+=pers1.Poderes[numhab-1].OthersLifeALteration;
             pers1.Mana-=pers1.Poderes[numhab-1].Costomana;
             if(pers2.Life<=0)
             {
                milaberinto[x2,y2] = previoousemoji2;
                x2=pers2.Start.Item1;
                y2=pers2.Start.Item2;
                pers2.ClearStats();
                milaberinto[x2, y2] = pers2.Emojiof;
                Console.Clear();
                Console.WriteLine($"Oh no, {namepers2} te has quedado sin vida, vamos a regresarte al inicio, suerte!!!");
                mov=true;
                if(Poder.angelmuerte==true && pers2.Emojiof=="😇")
                        {
                            pers1.Life-=3;
                            Console.WriteLine("Con tu muerte se ha activado la habilidad especial ''suerte de morir''");
                            Console.WriteLine("Tu oponente pierde 3❤️");
                            Console.ReadKey();
                        }
             }
             if (pers1.Life<=0)
             {
                milaberinto[x,y] = previoousemoji1;
                x=pers1.Start.Item1;
                y=pers1.Start.Item2;
                pers1.ClearStats();
                milaberinto[x, y] = pers1.Emojiof;
                Console.Clear();
                Console.WriteLine($"Oh no, {namepers1} te has quedado sin vida, vamos a regresarte al inicio, suerte!!!");
                mov=true;
                if(Poder.angelmuerte==true && pers1.Emojiof=="😇")
                        {
                            pers2.Life-=3;
                            Console.WriteLine("Con tu muerte se ha activado la habilidad especial ''suerte de morir''");
                            Console.WriteLine("Tu oponente pierde 3❤️");
                            Console.ReadKey();
                        }
             }
            }
            else if (pers1.Poderes[numhab-1].PoderID==3)
            {
                Poder.escamasdedragon=true;
            
            }
            else if(pers1.Poderes[numhab-1].PoderID==4)
            {
                Poder.angelmuerte=true;
            }
            else if(pers1.Poderes[numhab-1].PoderID==6)
            {
               mov=false;
            }
            else if(pers1.Poderes[numhab-1].PoderID==9)
            {
                milaberinto[x, y]="💣";

            }
        
        }
        
    public static string[] caraocruz = { "cara", "cruz" };
    public static int inicio;
    public static string previousemojich1 = "⬜";
    public static string previousemojich2 = "⬜";
    public static int initialcoordspl1fil = 0;
    public static int initialcoordspl1col = 0;
    public static int dimension;
    public static int initialcoordspl2fil;
    public static int initialcoordspl2col;
    public static string winner = "";
    public static bool winnerplayer1 = false;
    public static bool winnerplayer2 = false;
    public static bool player1mov = true;
    public static bool player2mov = true;
    #endregion
    public static void Main(string[] args)
    {

        #region texto
        ObstacleDictionaries obstacleDictionaries = new();
        
        Console.WriteLine("Bienvenidos al Laberinto Mágico");
        Console.WriteLine("Presione cualquier tecla para continuar (menos Esc)");
        Console.ReadKey();
        Console.WriteLine("¿Desean jugar?");
        Console.WriteLine("1. Sí");
        Console.WriteLine("2. No");
        string? stringopcion=Console.ReadLine();
        int? opcion = int.Parse(stringopcion);
        if (opcion == 1)
        {
            Console.WriteLine("El laberinto mágico es un juego diseñado para dos personas. Por ahora, díganme sus nombres...");
            Console.WriteLine("Jugador 1, introduzca su nombre:");
            string? player1 = Console.ReadLine();
            Console.WriteLine("Jugador 2, introduzca su nombre:");
            string? player2 = Console.ReadLine();
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
            player1character.Start=(0,0);
            Console.WriteLine($"{player2}, seleccione un personaje introduciendo su número:");
            int player2Personaje = int.Parse(Console.ReadLine());
            Character player2character = PersonajesData.personajes[player2Personaje - 1].Item2;
            Console.WriteLine("Perfecto, ¿de qué dimensión desean que sea el laberinto?");
            dimension = int.Parse(Console.ReadLine());
            player2character.Start=(dimension-1, dimension-1);
            initialcoordspl2fil = dimension-1;
            initialcoordspl2col = dimension-1;
            MazeGen mazeGen = new MazeGen(dimension);
            mazeGen.Maze[dimension / 2, dimension / 2] = "🏁";
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
                Console.WriteLine($"{player1}, comienza tu turno");
            }
            else
            {
                Console.WriteLine($"La moneda ha salido {caraocruz[seleccioncaraocruz - 1]}");
                Console.WriteLine($"{player2} empieza jugando");
                inicio = 2;
                Console.WriteLine($"{player2}, comienza tu turno");
            }
            #endregion

            while (!winnerplayer1 && !winnerplayer2)
            {
                #region movimiento jugador 1
                if (inicio == 1)
                {
                    if (player1character.Life <= 0)
                    {
                        mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = previousemojich1;
                        initialcoordspl1fil = 0;
                        initialcoordspl1col = 0;
                        player1character.ClearStats();
                        mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = player1character.Emojiof;
                        Console.Clear();
                        Console.WriteLine("Oh no, te has quedado sin vida, vamos a regresarte al inicio, suerte!!!");
                        inicio++;
                        if(Poder.angelmuerte==true && player1character.Emojiof=="😇")
                         {
                           player2character.Life-=3;
                           Console.WriteLine("Con tu muerte se ha activado la habilidad especial ''suerte de morir''");
                           Console.WriteLine("Tu oponente pierde 3❤️");
                           Console.ReadKey();
                         }
                    }

                    else
                    {
                        player2mov=true;
                        Console.Clear();
                        Console.WriteLine($"Turno de {player1}.");
                        DisplayCharacterStats(player1character);
                        Console.WriteLine("Presiona X para tirar el dado🎲 y avanzar");
                        Console.WriteLine("Presiona Z para acceder a las habilidades especiales");
                        Console.WriteLine("Presiona E para finalizar tu turno");
                        var opcion3 = Console.ReadKey();



                        if (opcion3.Key == ConsoleKey.Z)
                        {
                            Console.Clear();
                            int n = 1;
                            foreach (var poder in player1character.Poderes)
                            {
                                Console.WriteLine($"{n}- {poder.Nombre}");
                                poder.DisplayPoderInfo();
                                n++;
                            }
                            Console.WriteLine("Seleccione el numero de habilidad que desea usar:");
                            int habilidadnumber = int.Parse(Console.ReadLine());
                            if (player1character.Mana-player1character.Poderes[habilidadnumber - 1].Costomana>=0)
                            {
                        
                            UsePoder(habilidadnumber, player1character, player2character, player2mov, mazeGen.Maze, dimension, initialcoordspl1fil, initialcoordspl1col,previousemojich1, previousemojich2, initialcoordspl2fil, initialcoordspl2col, player1, player2);
                            }
                            else
                            {
                                Console.WriteLine("No tienes suficiente mana para utilizar este poder");
                            }
                            opcion3 = Console.ReadKey();

                        }

                        if (opcion3.Key == ConsoleKey.X)
                            if (!player1mov)
                            {
                                Console.Clear();
                                Console.WriteLine("No puedes hacer esto en este turno");
                                Console.ReadKey();
                            }
                            else
                            {
                                {
                                    player1mov = false;
                                    Console.Clear();
                                    Random random3 = new Random();
                                    int dado = random3.Next(1, 7);
                                    Console.WriteLine($"El dado ha caído en {dado}🎲");
                                    Console.ReadKey();
                                    mazeGen.DisplayMaze();
                                    for (int i = 0; i < dado; i++)
                                    {
                                        if (player1character.Life <= 0)
                                        {
                                            mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = previousemojich1;
                                            initialcoordspl1fil = 0;
                                            initialcoordspl1col = 0;
                                            player1character.ClearStats();
                                            mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = player1character.Emojiof;
                                            Console.Clear();
                                            Console.WriteLine("Oh no, te has quedado sin vida, vamos a regresarte al inicio, suerte!!!");
                                            inicio++;
                                            Console.WriteLine($"{player2}, comienza tu turno");
                                            Console.ReadKey();
                                            if(Poder.angelmuerte==true && player1character.Emojiof=="😇")
                                                 {
                                                   player2character.Life-=3;
                                                   Console.WriteLine("Con tu muerte se ha activado la habilidad especial ''suerte de morir''");
                                                   Console.WriteLine("Tu oponente pierde 3❤️");
                                                   Console.ReadKey();
                                                 }
                                            break;
                                        }
                                        var key = Console.ReadKey().Key;
                                        if (key == ConsoleKey.W && initialcoordspl1fil - 1 >= 0 && mazeGen.Maze[initialcoordspl1fil - 1, initialcoordspl1col] != "🌳")
                                        {
                                            Console.Clear();
                                            mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = previousemojich1;
                                            previousemojich1 = mazeGen.Maze[initialcoordspl1fil - 1, initialcoordspl1col];
                                            mazeGen.Maze[initialcoordspl1fil - 1, initialcoordspl1col] = player1character.Emojiof;
                                            initialcoordspl1fil--;
                                            mazeGen.DisplayMaze();

                                        }
                                        else if (key == ConsoleKey.S && initialcoordspl1fil + 1 < dimension && mazeGen.Maze[initialcoordspl1fil + 1, initialcoordspl1col] != "🌳")
                                        {
                                            Console.Clear();
                                            mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = previousemojich1;
                                            previousemojich1 = mazeGen.Maze[initialcoordspl1fil + 1, initialcoordspl1col];
                                            mazeGen.Maze[initialcoordspl1fil + 1, initialcoordspl1col] = player1character.Emojiof;
                                            initialcoordspl1fil++;
                                            mazeGen.DisplayMaze();
                                        }
                                        else if (key == ConsoleKey.A && initialcoordspl1col - 1 >= 0 && mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col - 1] != "🌳")
                                        {
                                            Console.Clear();
                                            mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = previousemojich1;
                                            previousemojich1 = mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col - 1];
                                            mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col - 1] = player1character.Emojiof;
                                            initialcoordspl1col--;
                                            mazeGen.DisplayMaze();
                                        }
                                        else if (key == ConsoleKey.D && initialcoordspl1col + 1 < dimension && mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col + 1] != "🌳")
                                        {
                                            Console.Clear();
                                            mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = previousemojich1;
                                            previousemojich1 = mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col + 1];
                                            mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col + 1] = player1character.Emojiof;
                                            initialcoordspl1col++;
                                            mazeGen.DisplayMaze();
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            mazeGen.DisplayMaze();
                                            Console.WriteLine("La casilla a la que desea moverse no es alcanzable, trate de nuevo");
                                            i--;
                                        }
                                        if (previousemojich1 != "⬜" && MazeMask[initialcoordspl1fil, initialcoordspl1col] == false)
                                        {
                                            MazeMask[initialcoordspl1fil, initialcoordspl1col] = true;

                                            if (previousemojich1 == "🏁")
                                            {
                                                winner = player1;
                                                winnerplayer1 = true;
                                                break;
                                            }
                                            else if(previousemojich1=="💣")
                                            {
                                             player2character.Life--;
                                             Console.WriteLine("Oh no, tu rival habia dejado una trampa en esta casilla, pierdes 1❤️");
                                            }
                                            else if (previousemojich1 == "🔴")
                                            {
                                                Random random4 = new Random();
                                                int obstaculonpc = random4.Next(1, 4);
                                                ObstacleDictionaries.RoundEmojiMeanings[obstaculonpc].DisplayNPCInfo();
                                                player1character.Life += ObstacleDictionaries.RoundEmojiMeanings[obstaculonpc].Lifealteration;
                                            }
                                            else
                                            {
                                                bool visitadoadyacentes = false;
                                                foreach (var dir in MazeGen.directions)
                                                {
                                                    if (MazeGen.PosicionValida(initialcoordspl1fil + dir.Item1, initialcoordspl1col + dir.Item2) && MazeMask[initialcoordspl1fil + dir.Item1, initialcoordspl1col + dir.Item2] == true)
                                                    {
                                                        visitadoadyacentes = true;
                                                        MazeMask[initialcoordspl1fil, initialcoordspl1col] = true;
                                                        break;
                                                    }
                                                }
                                                if (!visitadoadyacentes)
                                                {
                                                    ObstacleDictionaries.SquareEmojiMeanings[previousemojich1].DisplayPlaceInfo();
                                                    player1character.Life += ObstacleDictionaries.SquareEmojiMeanings[previousemojich1].Lifealteration;
                                                }
                                            }
                                            Console.ReadKey();
                                        }

                                    }
                                    opcion3 = Console.ReadKey();
                                }
                            }
                        if (opcion3.Key == ConsoleKey.E)
                        {
                            player1character.Mana++;
                            inicio++;
                            Console.Clear();
                            Console.WriteLine($"{player2}, comienza tu turno");
                            Console.ReadKey();
                        }

                    }
                }
                #endregion


                #region movimiento jugador 2
                if (inicio == 2)
                {
                    if (player2character.Life <= 0)
                    {
                        mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col] = previousemojich2;
                        initialcoordspl2fil = dimension-1;
                        initialcoordspl2col = dimension-1;
                        player2character.ClearStats();
                        mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col] = player2character.Emojiof;
                        Console.Clear();
                        Console.WriteLine("Oh no, te has quedado sin vida, vamos a regresarte al inicio, suerte!!!");
                        inicio--;
                        if(Poder.angelmuerte==true && player2character.Emojiof=="😇")
                        {
                            player1character.Life-=3;
                            Console.WriteLine("Con tu muerte se ha activado la habilidad especial ''suerte de morir''");
                            Console.WriteLine("Tu oponente pierde 3❤️");
                            Console.ReadKey();
                        }
                    }

                    else
                    {
                        player1mov=true;
                        Console.Clear();
                        Console.WriteLine($"Turno de {player2}.");
                        DisplayCharacterStats(player2character);
                        Console.WriteLine("Presiona X para tirar el dado🎲 y avanzar");
                        Console.WriteLine("Presiona Z para acceder a las habilidades especiales");
                        Console.WriteLine("Presiona E para finalizar tu turno");
                        var opcion3 = Console.ReadKey();



                        if (opcion3.Key == ConsoleKey.Z)
                        {
                            Console.Clear();
                            int n = 1;
                            foreach (var poder in player2character.Poderes)
                            {
                                Console.WriteLine($"{n}- {poder.Nombre}");
                                poder.DisplayPoderInfo();
                                n++;
                            }
                            Console.WriteLine("Seleccione el numero de habilidad que desea usar:");
                            int habilidadnumber = int.Parse(Console.ReadLine());
                            if (player2character.Mana - player2character.Poderes[habilidadnumber - 1].Costomana>=0)
                            {
                                player2character.Mana -= player2character.Poderes[habilidadnumber - 1].Costomana;
                                UsePoder(habilidadnumber, player2character, player1character, player1mov, mazeGen.Maze, dimension, initialcoordspl2fil, initialcoordspl2col,previousemojich2, previousemojich1, initialcoordspl1fil, initialcoordspl1col, player2, player1);
                            }
                            else
                            {
                                Console.WriteLine("No tienes suficiente mana para utilizar este poder");
                            }
                            opcion3 = Console.ReadKey();

                        }

                        if (opcion3.Key == ConsoleKey.X)
                            if (!player2mov)
                            {
                                Console.Clear();
                                Console.WriteLine("No puedes hacer esto en este turno");
                                Console.ReadKey();
                            }
                            else
                            {
                                {
                                    player2mov = false;
                                    Console.Clear();
                                    Random random3 = new Random();
                                    int dado = random3.Next(1, 7);
                                    Console.WriteLine($"El dado ha caído en {dado}🎲");
                                    Console.ReadKey();
                                    mazeGen.DisplayMaze();
                                    for (int i = 0; i < dado; i++)
                                    {
                                        if (player2character.Life <= 0)
                                        {
                                            mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col] = previousemojich2;
                                            initialcoordspl1fil = dimension-1;
                                            initialcoordspl1col = dimension-1;
                                            player2character.ClearStats();
                                            mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col] = player2character.Emojiof;
                                            Console.Clear();
                                            Console.WriteLine("Oh no, te has quedado sin vida, vamos a regresarte al inicio, suerte!!!");
                                            inicio++;
                                            Console.WriteLine($"{player1}, comienza tu turno");
                                            Console.ReadKey();
                                            if(Poder.angelmuerte==true && player2character.Emojiof=="😇")
                                            {
                                            player1character.Life-=3;
                                             Console.WriteLine("Con tu muerte se ha activado la habilidad especial ''suerte de morir''");
                                             Console.WriteLine("Tu oponente pierde 3❤️");
                                             Console.ReadKey();
                                             }
                                            break;
                                        }
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
                                            i--;
                                        }
                                        if (previousemojich2 != "⬜" && MazeMask[initialcoordspl2fil, initialcoordspl2col] == false)
                                        {
                                            MazeMask[initialcoordspl2fil, initialcoordspl2col] = true;

                                            if (previousemojich2 == "🏁")
                                            {
                                                winner = player2;
                                                winnerplayer2 = true;
                                                break;
                                            }
                                            else if(previousemojich2=="💣")
                                            {
                                             player2character.Life--;
                                             Console.WriteLine("Oh no, tu rival habia dejado una trampa en esta casilla, pierdes 1❤️");
                                            }
                                            else if (previousemojich2 == "🔴")
                                            {
                                                Random random4 = new Random();
                                                int obstaculonpc = random4.Next(1, 4);
                                                ObstacleDictionaries.RoundEmojiMeanings[obstaculonpc].DisplayNPCInfo();
                                                player2character.Life += ObstacleDictionaries.RoundEmojiMeanings[obstaculonpc].Lifealteration;
                                            }
                                            else
                                            {
                                                bool visitadoadyacentes = false;
                                                foreach (var dir in MazeGen.directions)
                                                {
                                                    if (MazeGen.PosicionValida(initialcoordspl2fil + dir.Item1, initialcoordspl2col + dir.Item2) && MazeMask[initialcoordspl2fil + dir.Item1, initialcoordspl2col + dir.Item2] == true)
                                                    {
                                                        visitadoadyacentes = true;
                                                        MazeMask[initialcoordspl2fil, initialcoordspl2col] = true;
                                                        break;
                                                    }
                                                }
                                                if (!visitadoadyacentes)
                                                {
                                                    ObstacleDictionaries.SquareEmojiMeanings[previousemojich2].DisplayPlaceInfo();
                                                    player2character.Life += ObstacleDictionaries.SquareEmojiMeanings[previousemojich2].Lifealteration;
                                                }
                                            }
                                            Console.ReadKey();
                                        }

                                    }
                                    opcion3 = Console.ReadKey();
                                }
                            }
                        if (opcion3.Key == ConsoleKey.E)
                        {
                            player2character.Mana++;
                            inicio--;
                            Console.Clear();
                            Console.WriteLine($"{player1}, comienza tu turno");
                            Console.ReadKey();
                        }

                    }
                }
            }
                
            #endregion
            Console.WriteLine($"¡Felicidades, {winner}! Has ganado la partida.");
        }
        else
        {
            Console.WriteLine("¡Hasta la próxima!");
        }
    }
}

