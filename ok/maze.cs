
namespace MyMaze 
{
    public class MazeGen
    {
    private List<string> Emojis= new List<string> {"🏘️", "🏚️", "⛪", "🌼", "🟦", "🧟", "🔴"};
    public bool[,] visited;
    public string[,] Maze;
    public int dimension;
    public static List<(int, int)> directions=[(0,1),  (0,-1), (1,0), (-1,0)];


        public MazeGen(int dim) 
        {
            dimension=dim;
            Maze = new string[dim, dim]; 
            generatemaze(); 
            place_emojis();
            Maze[dimension-2, dimension-2]="🏁";
        }

    public void generatemaze()
    {
        visited = new bool[dimension, dimension];
        for  (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                if( i%2==0 ||  j%2==0)

                {
                    Maze[i,j]="🌳";

                }
                else
                {
                    Maze[i,j]="⬜";
                }
            }
        }
        DFS((1,1));
    }
    public static void shuffle()
    {
        Random random=new Random();
        List <(int, int)> shuffled_directions=directions.OrderBy(n =>  random.Next()).ToList();
        directions=shuffled_directions;

    }
    public void DFS((int, int) coords)
{
    Stack<(int, int)> cells = new Stack<(int, int)>();
    (int, int) current, next;
    cells.Push(coords);
    visited[coords.Item1, coords.Item2] = true;
    while (cells.Count > 0)
    {
        current = cells.Peek();
        bool moved = false; 
        shuffle(); 
        foreach (var dir in directions)
        {
            next = (current.Item1 + dir.Item1 * 2, current.Item2 + dir.Item2 * 2);
            if (next.Item1 >= 1 && next.Item1 < dimension - 1 && next.Item2 >= 1 && next.Item2 < dimension - 1 && !visited[next.Item1, next.Item2] && Maze[next.Item1, next.Item2] == "⬜")
            {
                cells.Push(next);
                visited[next.Item1, next.Item2] = true;
                Maze[(current.Item1 + next.Item1) / 2, (current.Item2 + next.Item2) / 2] = "⬜"; 
                moved = true;
                break;
            }
        }

        if (!moved)
        {
            cells.Pop();
        }
    }
}
     public bool PosicionValida(int row, int col) 
        {
            return row >= 1 && row < dimension && col >= 1 && col < dimension; 
        }
    private void place_emojis()
    {
        Random rand =new Random();
        for (int i=0; i<dimension; i++)
        {
            for (int j=0; j<dimension; j++)
            {
                int posibilidad= rand.Next(1, dimension/4);
                if(Maze[i,j]=="⬜" && posibilidad==1)
                {
                    int  aleatorio=rand.Next(1, Emojis.Count);
                    Maze[i, j]=Emojis[aleatorio];
                }

            }
        }
    }
    public void DisplayMaze()
    {
        for  (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    Console.Write(Maze[i, j]);
                }
                Console.WriteLine();
            }

    }
        
        
        
    }
}


