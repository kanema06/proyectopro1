using Personajes;
using MyMaze;
using Obstacles;
using System.Collections;
using System.Security;
class Game
{

    #region variables y metodos
    static Random  random = new Random();

    static bool music=true;
    static bool finish=false;
    public static void DisplayCharacterStats(Character personaje)
    {
        Console.WriteLine($"vida:{personaje.Life}❤️        mana:{personaje.Mana}🪄");
    }
     public static void UsePoder(int numhab, Character pers1,  Character pers2, bool mov, string[,] milaberinto, int dimension,ref int x,ref int y,ref string previoousemoji1,ref string previoousemoji2,ref int x2,ref int y2, string namepers1, string namepers2)
        {
            pers1.Mana-=pers1.Poderes[numhab-1].Costomana;
            Console.WriteLine($"Acabas de usar {pers1.Poderes[numhab-1].Nombre}");
            pers1.Poderes[numhab-1].DisplayPoderInfo();
            if (pers1.Poderes[numhab-1].PoderID==1 || pers1.Poderes[numhab-1].PoderID==3 || pers1.Poderes[numhab-1].PoderID==5 ||  pers1.Poderes[numhab-1].PoderID==7 ||  pers1.Poderes[numhab-1].PoderID==8 ||  pers1.Poderes[numhab-1].PoderID==10)
            {
            int n=pers1.Poderes[numhab-1].OthersLifeALteration;
            if(pers1.Poderes[numhab-1].PoderID==7)
            {
                pers1.Poderes[numhab-1].OthersLifeALteration=-random.Next(0, 4);
                Console.WriteLine($"EL daño fue de {-n}");
            }
            if (Poder.escamasdedragon==true && pers2.Name=="dragon")
            {
                Console.WriteLine("tus escamas te han protegido del ataque de tu rival, cuidado a partir de ahora");
                Poder.escamasdedragon=false;
            }
             else
             {
                pers2.Life+=n;
             }
             pers1.Life+=pers1.Poderes[numhab-1].SelfLifeAlteration;
             if(pers2.Life<=0)
             {
                milaberinto[x2,y2] = previoousemoji2;
                previoousemoji2="⬜";
                x2=pers2.Start.Item1;
                y2=pers2.Start.Item2;
                pers2.ClearStats();
                milaberinto[x2, y2] = pers2.Emojiof;
                Console.Clear();
                Console.WriteLine($"Oh no, {namepers2} te has quedado sin vida, vamos a regresarte al inicio, suerte!!!");
                mov=true;
                if(Poder.angelmuerte==true && pers2.Emojiof=="😇")
                        {
                            pers1.Life-=4;
                            Console.WriteLine("Con tu muerte se ha activado la habilidad especial ''suerte de morir''");
                            Console.WriteLine("Tu oponente pierde 3❤️");
                            Console.ReadKey();
                        }
             }
             if (pers1.Life<=0)
             {
                milaberinto[x,y] = previoousemoji1;
                previoousemoji1="⬜";
                x=pers1.Start.Item1;
                y=pers1.Start.Item2;
                pers1.ClearStats();
                milaberinto[x, y] = pers1.Emojiof;
                Console.Clear();
                Console.WriteLine($"Oh no, {namepers1} te has quedado sin vida, vamos a regresarte al inicio, suerte!!!");
                mov=true;
                if(Poder.angelmuerte==true && pers1.Emojiof=="😇")
                        {
                            pers2.Life-=4;
                            Console.WriteLine("Con tu muerte se ha activado la habilidad especial ''suerte de morir''");
                            Console.WriteLine("Tu oponente pierde 3❤️");
                            Console.ReadKey();
                        }
             }
            }
            else if (pers1.Poderes[numhab-1].PoderID==2)
            {
                Poder.escamasdedragon=true;
            
            }
            else if(pers1.Poderes[numhab-1].PoderID==4)
            {
                Poder.angelmuerte=true;
              
            }
            else if(pers1.Poderes[numhab-1].PoderID==6)
            {
               Poder.sirenacanto=true;
            }
            else if(pers1.Poderes[numhab-1].PoderID==9)
            {
                previoousemoji1="💣";

            }
        
        }
        
    public static string[] caraocruz = { "cara", "cruz" };
    public static int inicio;
    public static string previousemojich1 = "⬜";
    public static string previousemojich2 = "⬜";
    public static int initialcoordspl1fil = 1;
    public static int initialcoordspl1col = 1;
    public static int dimension;
    public static int initialcoordspl2fil=1;
    public static int initialcoordspl2col=1;
    public static string winner = "";
    public static bool winnerplayer1 = false;
    public static bool winnerplayer2 = false;
    public static bool player1mov = true;
    public static bool player2mov = true;
    #endregion
    public static void Main(string[] args)
    {
        #region musica
        string audioFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "audio");
        string[] audioFilePaths = {
            Path.Combine(audioFolderPath, "Laura Shigihara - Crazy Dave (Intro Theme).mp3"),
            Path.Combine(audioFolderPath, "Laura Shigihara - Choose Your Seeds (In-Game).mp3"),
            Path.Combine(audioFolderPath,"Laura Shigihara - Grasswalk (In-Game).mp3"),
            Path.Combine(audioFolderPath,"8-Bit Arcade - Super Mario Bros. - Level Complete.mp3")
        };
        BackgroundMusic backgroundMusic = new BackgroundMusic(audioFilePaths);
        backgroundMusic.Play(0);
        #endregion

        #region texto
        
        ObstacleDictionaries obstacleDictionaries = new();
        
        string s=@" 
        
██████╗ ██╗███████╗███╗   ██╗██╗   ██╗███████╗███╗   ██╗██╗██████╗  ██████╗ ███████╗         
██╔══██╗██║██╔════╝████╗  ██║██║   ██║██╔════╝████╗  ██║██║██╔══██╗██╔═══██╗██╔════╝         
██████╔╝██║█████╗  ██╔██╗ ██║██║   ██║█████╗  ██╔██╗ ██║██║██║  ██║██║   ██║███████╗         
██╔══██╗██║██╔══╝  ██║╚██╗██║╚██╗ ██╔╝██╔══╝  ██║╚██╗██║██║██║  ██║██║   ██║╚════██║         
██████╔╝██║███████╗██║ ╚████║ ╚████╔╝ ███████╗██║ ╚████║██║██████╔╝╚██████╔╝███████║██╗██╗██╗
╚═════╝ ╚═╝╚══════╝╚═╝  ╚═══╝  ╚═══╝  ╚══════╝╚═╝  ╚═══╝╚═╝╚═════╝  ╚═════╝ ╚══════╝╚═╝╚═╝╚═╝
";
        Console.Clear();
        Console.Write(s);
        Console.WriteLine("Presione cualquier tecla para continuar");
        Console.ReadKey();
        Console.Clear();
        Console.WriteLine("    ¿Desean jugar?");
        Console.WriteLine("1. Sí             2. No");
        string? stringopcion=Console.ReadLine();
         int? opcion;
        Console.Clear();
        while(String.IsNullOrEmpty(stringopcion) || (stringopcion!="1" && stringopcion!="2"))
        {
            Console.WriteLine("    ¿Desean jugar?");
            Console.WriteLine("1. Sí             2. No");
            Console.WriteLine("opcion no valida, trata de nuevo");
            stringopcion=Console.ReadLine();
            Console.Clear();
        }
         opcion = int.Parse(stringopcion);
        if (opcion == 1)
        {
            Console.WriteLine("Desean musica de fondo?");
            Console.WriteLine("1. Sí             2. No");
            string? stringopcion2 = Console.ReadLine();
            Console.Clear();
             while(String.IsNullOrEmpty(stringopcion2) || (stringopcion2!="1" && stringopcion2!="2"))
            {
            Console.WriteLine("Desean musica de fondo?");
            Console.WriteLine("1. Sí             2. No");
            Console.WriteLine("opcion no valida, trata de nuevo");
            stringopcion2=Console.ReadLine();
            Console.Clear();
            }
            if(int.Parse(stringopcion2)==2)
            {
                backgroundMusic.Stop();
                music=false;
            }
            Console.WriteLine("Mazes & Monsters es un juego diseñado para dos personas. Por ahora, díganme sus nombres...");
            Console.WriteLine("Jugador 1, introduzca su nombre:");
            string? player1 = Console.ReadLine();
            while(String.IsNullOrEmpty(player1))
            {
                Console.Clear();
                Console.WriteLine("El Nombre introducido no es valido, intente de nuevo");
                Console.WriteLine("Jugador 1, introduzca su nombre:");
                player1 = Console.ReadLine();
            }
            Console.Clear();
            Console.WriteLine("Jugador 2, introduzca su nombre:");
            string? player2 = Console.ReadLine();
            while(String.IsNullOrEmpty(player2))
            {
                Console.Clear();
                Console.WriteLine("El Nombre introducido no es valido, intente de nuevo");
                Console.WriteLine("Jugador 2, introduzca su nombre:");
                player2 = Console.ReadLine();
            }
            Console.Clear();
            Console.WriteLine($"Bienvenidos a la aventura, {player1} y {player2}");
            Console.WriteLine("Presione cualquier tecla para continuar");
            Console.ReadKey();
            Console.Clear();
            if(music==true)
            {
                backgroundMusic.Stop();
                backgroundMusic.Play(1);
            }
            Console.WriteLine("El objetivo del juego es llegar hasta la meta antes que su oponente. Por supuesto, encontrarán varios obstáculos en su aventura.");
            Console.WriteLine("Para jugar, pueden escoger entre varios personajes:");
            PersonajesData.ReadPersonajes();
            Console.WriteLine("Presiona A para elegir personajes");
            Console.WriteLine("Presiona S para conocer las habilidades de los personajes");
            ConsoleKey opcion2 = Console.ReadKey().Key;
            while(opcion2!=ConsoleKey.A)
           {
            if (opcion2!=ConsoleKey.S)
            {
            Console.Clear();
            Console.WriteLine("Presiona A para elegir personajes");
            Console.WriteLine("Presiona S para conocer las habilidades de los personajes");
            Console.WriteLine("opcion no valida intente de nuevo");
            opcion2=Console.ReadKey().Key;

            }
            if (opcion2 == ConsoleKey.S)
            {
                Console.Clear();
                Console.WriteLine("Los personajes tienen tres habilidades comunes y una habilidad unica");
                Console.WriteLine("Las habilidades comunes son:");
                PersonajesData.genericpower1.DisplayPoderInfo();
                PersonajesData.genericpower2.DisplayPoderInfo();
                PersonajesData.genericpower3.DisplayPoderInfo();
                Console.WriteLine("Presiona cualquier tecla para continuar");
                Console.ReadKey();
                Console.Clear();
                PersonajesData.ReadPersonajesInfo();
                Console.WriteLine("Presiona cualquier tecla para salir");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Presiona A para elegir personajes");
                Console.WriteLine("Presiona S para conocer las habilidades unicas de los personajes");
                opcion2=Console.ReadKey().Key;
            }
           }
            Console.Clear();
            Console.WriteLine("Ahora que conocen los personajes, pueden escoger con cuál desean jugar.");
            PersonajesData.ReadPersonajes();
            Console.WriteLine($"{player1}, seleccione un personaje introduciendo su número:");

            string? player1Personajestring = Console.ReadLine();
            Console.Clear();
            while(String.IsNullOrEmpty(player1Personajestring) || (player1Personajestring!="1" && player1Personajestring!="2" && player1Personajestring!="3" && player1Personajestring!="4" && player1Personajestring!="5" && player1Personajestring!="6" && player1Personajestring!="7"))
        {
            PersonajesData.ReadPersonajes();
            Console.WriteLine("opcion no valida");
            Console.WriteLine($"{player1}, seleccione un personaje introduciendo su número:");
            player1Personajestring=Console.ReadLine();
            Console.Clear();
        }
            Character player1character = PersonajesData.personajes[int.Parse(player1Personajestring)-1].Item2;
            player1character.Start=(0,0);
            PersonajesData.ReadPersonajes();
            Console.WriteLine($"{player2}, seleccione un personaje introduciendo su número:");
            string? player2Personajestring = Console.ReadLine();
            Console.Clear();
            while(String.IsNullOrEmpty(player2Personajestring) ||player2Personajestring==player1Personajestring || (player2Personajestring!="1" && player2Personajestring!="2" && player2Personajestring!="3" && player2Personajestring!="4" && player2Personajestring!="5" && player2Personajestring!="6" && player2Personajestring!="7"))
        {
            PersonajesData.ReadPersonajes();
            Console.WriteLine("opcion no valida");
            Console.WriteLine($"{player2}, seleccione un personaje introduciendo su número:");
            player2Personajestring=Console.ReadLine();
            Console.Clear();
        }
            Character player2character = PersonajesData.personajes[int.Parse(player2Personajestring)-1].Item2;
            Console.WriteLine("Perfecto, ¿de qué dimensión desean que sea el laberinto?");
            Console.WriteLine("el menor tamaño disponible es 15 y el mayor (recomendado) es 100");
            string? dimensionstring = Console.ReadLine();
            while(String.IsNullOrEmpty(dimensionstring) || !int.TryParse(dimensionstring, out int dimension) || int.Parse(dimensionstring)<15)
            {
                Console.WriteLine("La dimension introducida no es valida, por favor intente con otro valor");
                dimensionstring=Console.ReadLine();
            }
            dimension=int.Parse(dimensionstring);
                 if (dimension%2==0)
                {
                dimension++;
                }

            MazeGen mazeGen = new MazeGen(dimension);
            bool[,] MazeMask = new bool[dimension, dimension];
            mazeGen.DisplayMaze();
            if(music==true){
            backgroundMusic.Stop();
            backgroundMusic.Play(2);
            }
            
            Console.WriteLine("Vamos a empezar...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Decidamos quién empieza jugando...");
            Console.WriteLine($"{player1}, presiona 1 para cara o 2 para cruz:");
            
            string? player1opcioncaraocruzstring= Console.ReadLine();
            while(String.IsNullOrEmpty(player1opcioncaraocruzstring) || (player1opcioncaraocruzstring!="1" && player1opcioncaraocruzstring!="2"))
            {
                Console.Clear();
                Console.WriteLine("Decidamos quién empieza jugando...");
                Console.WriteLine($"{player1}, presiona 1 para cara o 2 para cruz:");
                Console.WriteLine("opcion no valida, intente de nuevo");
                player1opcioncaraocruzstring= Console.ReadLine();
            }
            int player1opcioncaraocruz = int.Parse(player1opcioncaraocruzstring);
            int player2opcioncaraocruz = player1opcioncaraocruz == 1 ? 2 : 1;
            Console.WriteLine($"{player2}, eso te deja con {(player2opcioncaraocruz == 1 ? "cara" : "cruz")}");
            Console.WriteLine("Presionen cualquier tecla para lanzar la moneda...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Estamos lanzando la moneda... presiona cualquier tecla para parar");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("La moneda ha caído...");
            Random random2 = new Random();
            int seleccioncaraocruz = random2.Next(1, 3);
            if (seleccioncaraocruz == player1opcioncaraocruz)
            {
                Console.WriteLine("La moneda ha salido cara");
                Console.WriteLine($"{player1} empieza jugando");
                inicio = 1;
                mazeGen.Maze[1,1]=player1character.Emojiof;
                Console.WriteLine($"{player1}, comienza tu turno");
                Console.WriteLine("Presiona cualquier tecla para comenzar el juego");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"La moneda ha salido {caraocruz[seleccioncaraocruz - 1]}");
                Console.WriteLine($"{player2} empieza jugando");
                inicio = 2;
                mazeGen.Maze[1,1]=player2character.Emojiof;
                Console.WriteLine($"{player2}, comienza tu turno");
                Console.WriteLine("Presiona cualquier tecla para comenzar el juego");
                Console.ReadKey();
            }
            #endregion

            while (!winnerplayer1 && !winnerplayer2)
            {
                #region movimiento jugador 1
                if (inicio == 1)
                {
                    if(initialcoordspl1fil==1 &&  initialcoordspl1col==1)
                   {
                    mazeGen.Maze[1,1]=player1character.Emojiof;
                   }
                    if (player1character.Life <= 0)
                                        {
                                            mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = previousemojich1;
                                            previousemojich1="⬜";
                                            initialcoordspl1fil = 1;
                                            initialcoordspl1col = 1;
                                            player1character.ClearStats();
                                            mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = player1character.Emojiof;
                                            Console.Clear();
                                            Console.WriteLine("Oh no, te has quedado sin vida, vamos a regresarte al inicio, suerte!!!");
                                            Console.ReadKey();
                                            if(Poder.angelmuerte==true && player1character.Emojiof=="😇")
                                                 {
                                                   Poder.angelmuerte=false;
                                                   player2character.Life-=3;
                                                   Console.WriteLine("Con tu muerte se ha activado la habilidad especial ''suerte de morir''");
                                                   Console.WriteLine("Tu oponente pierde 3❤️");
                                                   Console.ReadKey();
                                                 }
                                            Console.Clear();
                                            inicio++;
                                            Console.WriteLine($"{player2}, comienza tu turno");
                                            Console.ReadKey();
                                            
                                        }

                    else
                    {
                    if (Poder.sirenacanto &&  player2character.Name=="sirena"){
                        player2mov=true;
                        Poder.sirenacanto=false;
                        Console.WriteLine("Oh no, tu oponente se ha saltado tu turno, luego nos vemos!!!");
                        Console.WriteLine("Presiona cualquier tecla para continuar....");
                        Console.ReadKey();
                        inicio++;
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
                            Console.WriteLine("Seleccione 0 para volver atras");
                            string? habilidadnumberstring =Console.ReadLine();
                            while(string.IsNullOrEmpty(habilidadnumberstring) || !int.TryParse(habilidadnumberstring, out int habilidadnumberint) || habilidadnumberint<0|| habilidadnumberint>4)
                            {
                                 Console.Clear();
                                 int k = 1;
                                 foreach (var poder in player1character.Poderes)
                                  {
                                 Console.WriteLine($"{k}- {poder.Nombre}");
                                  poder.DisplayPoderInfo();
                                  k++;
                                  }
                                Console.WriteLine("Seleccione el numero de habilidad que desea usar:");
                                Console.WriteLine("Seleccione 0 para volver atras");
                                Console.WriteLine("opcion no valida, intente de nuevo");
                                habilidadnumberstring= Console.ReadLine();
                            }
                            int habilidadnumber = int.Parse(habilidadnumberstring);
                            if(habilidadnumber!=0){
                            if (player1character.Mana-player1character.Poderes[habilidadnumber - 1].Costomana>=0)
                            {
                            UsePoder(habilidadnumber, player1character, player2character, player2mov, mazeGen.Maze, dimension,ref initialcoordspl1fil,ref initialcoordspl1col,ref previousemojich1,ref previousemojich2,ref initialcoordspl2fil,ref initialcoordspl2col, player1, player2);
                            }
                            else
                            {
                                Console.WriteLine("No tienes suficiente mana para utilizar este poder");
                            }
                            opcion3 = Console.ReadKey();

                            }
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
                                            previousemojich1="⬜";
                                            initialcoordspl1fil = 1;
                                            initialcoordspl1col = 1;
                                            player1character.ClearStats();
                                            mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = player1character.Emojiof;
                                            Console.Clear();
                                            Console.WriteLine("Oh no, te has quedado sin vida, vamos a regresarte al inicio, suerte!!!");
                                            Console.ReadKey();
                                            if(Poder.angelmuerte==true && player1character.Emojiof=="😇")
                                                 {
                                                   player2character.Life-=3;
                                                   Poder.angelmuerte=false;
                                                   Console.WriteLine("Con tu muerte se ha activado la habilidad especial ''suerte de morir''");
                                                   Console.WriteLine("Tu oponente pierde 3❤️");
                                                   Console.ReadKey();
                                                 }
                                            Console.Clear();
                                            inicio++;
                                            Console.WriteLine($"{player2}, comienza tu turno");
                                            Console.ReadKey();
                                            break;
                                        }
                                        var key = Console.ReadKey().Key;
                                        if ((key == ConsoleKey.W || key==ConsoleKey.UpArrow) && initialcoordspl1fil - 1 >= 1 && mazeGen.Maze[initialcoordspl1fil - 1, initialcoordspl1col] != "🌳")
                                        {
                                            Console.Clear();
                                            mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = previousemojich1;
                                            previousemojich1 = mazeGen.Maze[initialcoordspl1fil - 1, initialcoordspl1col];
                                            mazeGen.Maze[initialcoordspl1fil - 1, initialcoordspl1col] = player1character.Emojiof;
                                            initialcoordspl1fil--;
                                            mazeGen.DisplayMaze();

                                        }
                                        else if ((key == ConsoleKey.S || key==ConsoleKey.DownArrow) && initialcoordspl1fil + 1 < dimension && mazeGen.Maze[initialcoordspl1fil + 1, initialcoordspl1col] != "🌳")
                                        {
                                            Console.Clear();
                                            mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = previousemojich1;
                                            previousemojich1 = mazeGen.Maze[initialcoordspl1fil + 1, initialcoordspl1col];
                                            mazeGen.Maze[initialcoordspl1fil + 1, initialcoordspl1col] = player1character.Emojiof;
                                            initialcoordspl1fil++;
                                            mazeGen.DisplayMaze();
                                        }
                                        else if ((key == ConsoleKey.A || key==ConsoleKey.LeftArrow) && initialcoordspl1col - 1 >= 1 && mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col - 1] != "🌳")
                                        {
                                            Console.Clear();
                                            mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col] = previousemojich1;
                                            previousemojich1 = mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col - 1];
                                            mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col - 1] = player1character.Emojiof;
                                            initialcoordspl1col--;
                                            mazeGen.DisplayMaze();
                                        }
                                        else if ((key == ConsoleKey.D || key== ConsoleKey.RightArrow) && initialcoordspl1col + 1 < dimension && mazeGen.Maze[initialcoordspl1fil, initialcoordspl1col + 1] != "🌳")
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
                                        if(Personajes.PersonajesData.personajesemojis.Contains(previousemojich1))
                                        {
                                            Console.WriteLine("Has caido en la misma casilla que tu rival, puedes volver a moverte este turno");
                                            player1mov=true;

                                        }
                                        else if (previousemojich1 != "⬜" && MazeMask[initialcoordspl1fil, initialcoordspl1col] == false)
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
                                             player1character.Life-=5;
                                             Console.WriteLine("Oh no, tu rival habia dejado una trampa en esta casilla, pierdes 5❤️");
                                            }
                                            else if (previousemojich1 == "🔴")
                                            {
                                                Random random4 = new Random();
                                                int obstaculonpc = random4.Next(1, 4);
                                              ObstacleDictionaries.RoundEmojiMeanings[obstaculonpc].Item2.DisplayNPCInfo();
                                                previousemojich1=ObstacleDictionaries.RoundEmojiMeanings[obstaculonpc].Item1;
                                                player1character.Life += ObstacleDictionaries.RoundEmojiMeanings[obstaculonpc].Item2.Lifealteration;
                                            }
                                            else
                                            {
                                               ObstacleDictionaries.SquareEmojiMeanings[previousemojich1].DisplayPlaceInfo();
                                                player1character.Life += ObstacleDictionaries.SquareEmojiMeanings[previousemojich1].Lifealteration;
                                                
                                            }
                                        }

                                    }
                                    if(winnerplayer1)
                                    {
                                        break;
                                    }
                                    opcion3 = Console.ReadKey();
                                }
                            }
                        if (opcion3.Key == ConsoleKey.E)
                        {
                            if(player1character.Mana<5){
                            player1character.Mana++;
                            }
                            inicio++;
                            Console.Clear();
                            Console.WriteLine($"{player2}, comienza tu turno");
                            Console.ReadKey();
                        }

                    }
                    }
                }
                #endregion


                #region movimiento jugador 2
                if (inicio == 2)
                {
                   if(initialcoordspl2fil==1 &&  initialcoordspl2col==1)
                   {
                    mazeGen.Maze[1,1]=player2character.Emojiof;
                   }
                   if (player2character.Life <= 0)
                                        {
                                            mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col] = previousemojich2;
                                            previousemojich2="⬜";
                                            initialcoordspl2fil = 1;
                                            initialcoordspl2col = 1;
                                            player2character.ClearStats();
                                            mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col] = player2character.Emojiof;
                                            Console.Clear();
                                            Console.WriteLine("Oh no, te has quedado sin vida, vamos a regresarte al inicio, suerte!!!");
                                            Console.ReadKey();
                                            if(Poder.angelmuerte==true && player2character.Emojiof=="😇")
                                            {
                                             Poder.angelmuerte=false;
                                             player1character.Life-=3;
                                             Console.WriteLine("Con tu muerte se ha activado la habilidad especial ''suerte de morir''");
                                             Console.WriteLine("Tu oponente pierde 3❤️");
                                             Console.ReadKey();
                                             }
                                            inicio--;
                                            Console.WriteLine($"{player1}, comienza tu turno");
                                            Console.ReadKey();
                                            
                                           
                                        }
                    else
                    {
                        if (Poder.sirenacanto &&  player1character.Name=="sirena"){
                        player1mov=true;
                        Poder.sirenacanto=false;
                        Console.WriteLine("Oh no, tu oponente se ha saltado tu turno, luego nos vemos!!!");
                        Console.WriteLine("Presiona cualquier tecla para continuar....");
                        Console.ReadKey();
                        inicio--;
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
                            Console.WriteLine("Seleccione 0 para volver atras");
                            string? habilidadnumberstring =Console.ReadLine();
                            while(string.IsNullOrEmpty(habilidadnumberstring) || !int.TryParse(habilidadnumberstring, out int habilidadnumberint) || habilidadnumberint<0|| habilidadnumberint>4)
                            {
                                 Console.Clear();
                                 int k = 1;
                                 foreach (var poder in player2character.Poderes)
                                  {
                                  Console.WriteLine($"{k}- {poder.Nombre}");
                                  poder.DisplayPoderInfo();
                                  k++;
                                  }
                                Console.WriteLine("Seleccione el numero de habilidad que desea usar:");
                                Console.WriteLine("Seleccione 0 para volver atras");
                                Console.WriteLine("opcion no valida, intente de nuevo");
                                habilidadnumberstring= Console.ReadLine();
                            }
                            int habilidadnumber = int.Parse(habilidadnumberstring);
                            if(habilidadnumber!=0){
                            if (player2character.Mana-player2character.Poderes[habilidadnumber - 1].Costomana>=0)
                            {
                            UsePoder(habilidadnumber, player2character, player1character, player1mov, mazeGen.Maze, dimension,ref initialcoordspl2fil,ref initialcoordspl2col,ref previousemojich2,ref previousemojich1,ref initialcoordspl1fil,ref initialcoordspl1col, player2, player1);
                            }
                            else
                            {
                                Console.WriteLine("No tienes suficiente mana para utilizar este poder");
                            }
                            opcion3 = Console.ReadKey();

                            }

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
                                            previousemojich2="⬜";
                                            initialcoordspl2fil = 1;
                                            initialcoordspl2col = 1;
                                            player2character.ClearStats();
                                            mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col] = player2character.Emojiof;
                                            Console.Clear();
                                            Console.WriteLine("Oh no, te has quedado sin vida, vamos a regresarte al inicio, suerte!!!");
                                            Console.ReadKey();
                                            if(Poder.angelmuerte==true && player2character.Emojiof=="😇")
                                            {
                                            player1character.Life-=3;
                                             Console.WriteLine("Con tu muerte se ha activado la habilidad especial ''suerte de morir''");
                                             Console.WriteLine("Tu oponente pierde 3❤️");
                                             Console.ReadKey();
                                             }
                                            inicio--;
                                            Console.WriteLine($"{player1}, comienza tu turno");
                                            Console.ReadKey();
                                            
                                            break;
                                        }
                                        var key = Console.ReadKey().Key;
                                        if ((key == ConsoleKey.W || key==ConsoleKey.UpArrow) && initialcoordspl2fil - 1 >= 1 && mazeGen.Maze[initialcoordspl2fil - 1, initialcoordspl2col] != "🌳")
                                        {
                                            Console.Clear();
                                            mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col] = previousemojich2;
                                            previousemojich2 = mazeGen.Maze[initialcoordspl2fil - 1, initialcoordspl2col];
                                            mazeGen.Maze[initialcoordspl2fil - 1, initialcoordspl2col] = player2character.Emojiof;
                                            initialcoordspl2fil--;
                                            mazeGen.DisplayMaze();

                                        }
                                        else if ((key == ConsoleKey.S || key==ConsoleKey.DownArrow) && initialcoordspl2fil + 1 < dimension && mazeGen.Maze[initialcoordspl2fil + 1, initialcoordspl2col] != "🌳")
                                        {
                                            Console.Clear();
                                            mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col] = previousemojich2;
                                            previousemojich2 = mazeGen.Maze[initialcoordspl2fil + 1, initialcoordspl2col];
                                            mazeGen.Maze[initialcoordspl2fil + 1, initialcoordspl2col] = player2character.Emojiof;
                                            initialcoordspl2fil++;
                                            mazeGen.DisplayMaze();
                                        }
                                        else if ((key == ConsoleKey.A || key==ConsoleKey.LeftArrow) && initialcoordspl2col - 1 >= 1 && mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col - 1] != "🌳")
                                        {
                                            Console.Clear();
                                            mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col] = previousemojich2;
                                            previousemojich2 = mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col - 1];
                                            mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col - 1] = player2character.Emojiof;
                                            initialcoordspl2col--;
                                            mazeGen.DisplayMaze();
                                        }
                                        else if ((key == ConsoleKey.D || key==ConsoleKey.RightArrow) && initialcoordspl2col + 1 < dimension && mazeGen.Maze[initialcoordspl2fil, initialcoordspl2col + 1] != "🌳")
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
                                        if(Personajes.PersonajesData.personajesemojis.Contains(previousemojich2))
                                        {
                                            Console.WriteLine("Has caido en la misma casilla que tu rival, puedes volver a moverte este turno");
                                            player2mov=true;

                                        }
                                        else if (previousemojich2 != "⬜" && MazeMask[initialcoordspl2fil, initialcoordspl2col] == false)
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
                                             player2character.Life-=5;
                                             Console.WriteLine("Oh no, tu rival habia dejado una trampa en esta casilla, pierdes 5❤️");
                                            }
                                            else if (previousemojich2 == "🔴")
                                            {
                                                Random random4 = new Random();
                                                int obstaculonpc = random4.Next(1, 4);
                                                 previousemojich2=ObstacleDictionaries.RoundEmojiMeanings[obstaculonpc].Item1;
                                                ObstacleDictionaries.RoundEmojiMeanings[obstaculonpc].Item2.DisplayNPCInfo();

                                                player2character.Life += ObstacleDictionaries.RoundEmojiMeanings[obstaculonpc].Item2.Lifealteration;
                                            }
                                            else
                                            {
                                                
                                                    ObstacleDictionaries.SquareEmojiMeanings[previousemojich2].DisplayPlaceInfo();
                                                    player2character.Life += ObstacleDictionaries.SquareEmojiMeanings[previousemojich2].Lifealteration;
                                                
                                            }
                                        }

                                    }
                                    if(winnerplayer2)
                                    {
                                        break;
                                    }
                                    opcion3 = Console.ReadKey();
                                }
                            }
                        if (opcion3.Key == ConsoleKey.E)
                        {
                            if(player2character.Mana<5){
                            player2character.Mana++;
                            }
                            inicio--;
                            Console.Clear();
                            Console.WriteLine($"{player1}, comienza tu turno");
                            Console.ReadKey();
                        }

                    }
                    }
                }

            }
                
            #endregion
            if(music==true)
            {
                backgroundMusic.Stop();
                backgroundMusic.Play(3);
            }
            Console.Clear();
            Console.WriteLine($"¡Felicidades, {winner}! Has ganado la partida.");
            finish=true;
        }
        if(opcion==2 || finish)
        {
            Console.WriteLine("¡Hasta la próxima!");
            Console.WriteLine("Presione cualquier tecla para salir...");
            Console.ReadKey();
            Environment.Exit(0);
        }
       
    }
}

