using System; // Importa el espacio de nombres System
using System.Collections.Generic; // Importa el espacio de nombres System.Collections.Generic

namespace MyMaze // Define el espacio de nombres MyMaze
{
    public class MazeGen // Declara la clase pública MazeGen
    {
        public static int Dimension { get; set; } // Propiedad pública para la dimensión del laberinto
        public string[,] Maze { get; private set; } // Propiedad pública para la matriz del laberinto, solo lectura desde fuera de la clase
        private Random rand = new Random(); // Instancia de la clase Random para generar números aleatorios
        public static List<(int, int)> directions = new List<(int, int)> { (0, 1), (1, 0), (0, -1), (-1, 0) }; // Lista de tuplas que representan direcciones posibles
        private List<string> Emojis= new List<string> {"🏘️", "🏚️", "⛪", "🌼", "🟦", "🪦 ", "🔴"};
        private List<string> SquareEmojis= new List<string>{"🏘️", "🏚️", "⛪", "🌼", "🟦", "🪦 "};

        public MazeGen(int dimension) // Constructor de la clase MazeGen
        {
            Dimension = dimension; // Inicializa la propiedad Dimension
            Maze = new string[dimension, dimension]; // Inicializa la matriz Maze
            GenerateMaze(); // Llama al método GenerateMaze para generar el laberinto
            Placestartingpoints();
            PlaceEmojis();
        }

        private void GenerateMaze() // Método para generar el laberinto
        {
            for (int i = 0; i < Dimension; i++) // Itera sobre las filas del laberinto
                for (int j = 0; j < Dimension; j++) // Itera sobre las columnas del laberinto
                    Maze[i, j] = "🌳"; // Inicializa cada celda del laberinto con "🌳"
            List<(int, int)> walls = new List<(int, int)>(); // Lista de paredes
            int startX = rand.Next(Dimension); // Punto de inicio aleatorio en x
            int startY = rand.Next(Dimension); // Punto de inicio aleatorio en y
            Maze[startY, startX] = "⬜"; // Marca el punto de inicio con "⬜"

            foreach (var (dy, dx) in directions) // Itera sobre las direcciones posibles
            {
                int ny = startY + dy, nx = startX + dx; // Calcula las nuevas coordenadas
                if (IsInBounds(ny, nx)) // Verifica si las coordenadas están dentro del laberinto
                {
                    walls.Add((ny, nx)); // Agrega las coordenadas a la lista de paredes
                }
            }

            while (walls.Count > 0) // Mientras haya paredes en la lista
            {
                var (y, x) = walls[rand.Next(walls.Count)]; // Selecciona una pared aleatoriamente
                walls.Remove((y, x)); // Elimina la pared seleccionada de la lista

                if (Maze[y, x] == "🌳") // Si la celda es una pared
                {
                    List<(int, int)> neighbors = new List<(int, int)>(); // Lista de vecinos
                    foreach (var (dy, dx) in directions) // Itera sobre las direcciones posibles
                    {
                        int ny = y + dy, nx = x + dx; // Calcula las nuevas coordenadas
                        if (IsInBounds(ny, nx) && Maze[ny, nx] == "⬜") // Verifica si las coordenadas están dentro del laberinto y son camino
                        {
                            neighbors.Add((ny, nx)); // Agrega las coordenadas a la lista de vecinos
                        }
                    }

                    if (neighbors.Count == 1) // Si hay exactamente un vecino
                    {
                        Maze[y, x] = "⬜"; // Marca la celda como camino
                        int ny = (y + neighbors[0].Item1) / 2; // Calcula la coordenada intermedia en y
                        int nx = (x + neighbors[0].Item2) / 2; // Calcula la coordenada intermedia en x
                        Maze[ny, nx] = "⬜"; // Marca la celda intermedia como camino

                        foreach (var (dy, dx) in directions) // Itera sobre las direcciones posibles
                        {
                            int nwy = y + dy, nwx = x + dx; // Calcula las nuevas coordenadas
                            if (IsInBounds(nwy, nwx)) // Verifica si las coordenadas están dentro del laberinto
                            {
                                walls.Add((nwy, nwx)); // Agrega las coordenadas a la lista de paredes
                            }
                        }
                    }
                }
            }
        }

       public static bool IsInBounds(int row, int col) // Método para verificar si una celda está dentro del laberinto
        {
            return row >= 0 && row < Dimension && col >= 0 && col < Dimension; // Retorna verdadero si las coordenadas están dentro del laberinto
        }

        public void DisplayMaze() // Método para mostrar el laberinto
        {
            Console.Clear(); // Limpia la consola
            for (int i = 0; i < Dimension; i++) // Itera sobre las filas del laberinto
            {
                for (int j = 0; j < Dimension; j++) // Itera sobre las columnas del laberinto
                {
                    Console.Write(Maze[i, j]); // Muestra el contenido de la celda
                }
                Console.WriteLine(); // Salta a la siguiente línea después de cada fila
            }
        }
        private void Placestartingpoints()
        {
           Maze[0,0]= "⬜";
           Maze[Dimension - 1, Dimension - 1] = "⬜";
           Maze[Dimension/2-1, Dimension/2-1]="⬜";
           bool[,] visited= new bool[Dimension, Dimension];
           for(int i=0; i<Dimension; i++)
           {
            for (int j=0; j<Dimension; j++)
            {
                if(Maze[i,j]=="⬜")
                {
                    visited[i, j]=true;
                       if(!CheckConnected(i,j, visited))
                        {
                    int aux=0;
                    while(aux==0)
                    {
                    Random random=new Random();
                    int Randdir=random.Next(0, 4);
                        if (IsInBounds(i+directions[Randdir].Item1, j+directions[Randdir].Item2))
                        {
                        Maze[i+directions[Randdir].Item1, j+directions[Randdir].Item2]="⬜";
                        aux=1;
                        break;
                        }
                    }
                         }
                }
            }

           }

        }
        private bool CheckConnected(int i, int j, bool[,] b){
            foreach(var  (dy, dx) in directions)
                    {
                      if(IsInBounds(i+dx, j+dy) &&  Maze[i+dx, j+dy]=="⬜" && b[i+dx, j+dy]==false)
                      {
                        return true;
                      }
                    }
                    return false;
        }
        private void PlaceEmojis()
        {
            for(int i=0; i<Dimension; i++)
            {
                for(int j=0; j<Dimension; j++)
                {
                    int posibilidad=rand.Next(1, Dimension);
                    if (posibilidad==1){
                        Maze[i, j]=Emojis[rand.Next(Emojis.Count())];
                        if(SquareEmojis.Contains(Maze[i, j]))
                        {
                            foreach(var dir in directions)
                            {
                              if(IsInBounds(i+dir.Item1, j+dir.Item2) && Maze[i+dir.Item1, j+dir.Item2]=="⬜")
                              {
                                Maze[i+dir.Item1, j+dir.Item2]=Maze[i, j];
                              }
                            }
                        }
                    }
                }
            }
        }
      
    }
}


