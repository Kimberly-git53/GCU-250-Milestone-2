using MineSweeperClasses;

namespace MineSweeperConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, welcome to Minesweeper");
            // size 10 and difficulty 0.1
            Board board = new Board(10, 0.1f);
            PrintBoard(board);
            
            // Deactivate second board game
            // Size 15 and difficulty 0.15
            //board = new Board(15, 0.15f);
            //Console.WriteLine("Here is the answer key for the second board.");
            //PrintAnswers(board);

            bool victory = false; // player wins
            bool death = false; // player loses

            // Repeat until game is over
            while (!victory && !death)
            {
                // Prompt the player to play
                Console.WriteLine("Enter a row number from the board: ");
                int row = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter a column number from the board: ");
                int col = int.Parse(Console.ReadLine());
                // Ask if this is for Flag or Visit or Use Reward
                Console.WriteLine("Choose an action you wish to play and type the name as seen: Flag, Visit, or Use Reward (Undo)");
                string action = Console.ReadLine();

                // Update cell if Flag was chosen
                if (action == "Flag")
                {
                    board.Cells[row, col].IsFlagged = true;
                    Console.WriteLine($"Cell ({row}, {col}) is flagged.");
                }
                // Update cell if Visit is chosen
                else if (action == "Visit")
                {
                    if (board.Cells[row, col].IsBomb)
                    {
                        death = true;
                        board.Cells[row, col].IsVisited = true;
                        Console.WriteLine($"Cell ({row}, {col}) is a bomb.");
                        
                    }
                    else
                    {
                        board.Cells[row, col].IsVisited = true;
                        Console.WriteLine($"Cell ({row}, {col}) has been visited. No bomb present.");
                    }
                }
                else if (action == "Use Reward")
                {
                    board.Cells[row, col].Undo();
                    Console.WriteLine($"Undo used on cell ({row}, {col}).");
                }
                else
                {
                    Console.WriteLine("Input invalid please type either Flag, Visit, or Use Reward.");
                }
                
                victory = true;
                //Force to visit cells until all non-bomb cells have been found.
                foreach (Cell cell in board.Cells)
                {
                    if (!cell.IsBomb && !cell.IsVisited)
                    {
                        victory = false; // Found a non-bomb cell that isn't visited
                        break;
                    }
                }


                PrintBoard(board);

            }
            
             //If not death then the play has won otherwise they lost
                if (!death)
                {
                    Console.WriteLine("You won the game!");
                }
                else
                {
                    Console.WriteLine("You hit a bomb. You lost the game.");
                }
            
            Console.WriteLine("Here is the answer key for the board.");
            PrintAnswers(board);
        }

        private static void PrintBoard(Board board)
        {
            // Print column numbers
            Console.WriteLine(" ");
            Console.Write("  ");
            for (int col = 0; col < board.Size; col++)
            {

                Console.Write($" {col}");
            }
            Console.WriteLine();

            // Print divider line
            Console.WriteLine(new string('-', board.Size * 2 + 3));

            // Print the rows and cells
            for (int row = 0; row < board.Size; row++)
            {
                // Print row number
                Console.Write($"{row}| ");
                // Print the cells
                for (int col = 0; col < board.Size; col++)
                {
                    PrintCell(board.Cells[row, col]);
                }
                Console.WriteLine();
            }
            // Print divider line
            Console.WriteLine(new string('-', board.Size * 2 + 3));
        }
        // Print a single cell
        static void PrintCell(Cell cell)
        {
            if (!cell.IsVisited)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("? ");
            }
            else if (cell.IsBomb)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("B ");
            }
            else if (cell.NumberOfBombNeighbors > 0)
            {
                SetNeighborColor(cell.NumberOfBombNeighbors);
                Console.Write($"{cell.NumberOfBombNeighbors} ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(". ");
            }
            Console.ResetColor();
        }
        // Set color based on number of bombs near by
        static void SetNeighborColor(int bombNeighbors)
        {
            switch (bombNeighbors)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case 7:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

// Method to print the board answers
static void PrintAnswers(Board board)
        {
            // Print column numbers
            Console.WriteLine(" ");
            Console.Write("  ");
            for (int col = 0; col < board.Size; col++)
            {
                
                Console.Write($" {col}");
            }
            Console.WriteLine();

            // Print divider line
            Console.WriteLine(new string('-', board.Size * 2 + 3));

            // Print the rows and cells
            for (int row = 0; row < board.Size; row++)
            {
                // Print row number
                Console.Write($"{row}| ");
                // Print the cells
                for (int col = 0; col < board.Size; col++)
                {
                    // Print the bomb if it is a bomb
                    if (board.Cells[row, col].IsBomb)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("B ");
                    }
                    // Print the number 1-8 if it is not a bomb
                    else if (board.Cells[row, col].NumberOfBombNeighbors == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write($"{board.Cells[row, col].NumberOfBombNeighbors} ");
                    }
                    else if (board.Cells[row, col].NumberOfBombNeighbors == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{board.Cells[row, col].NumberOfBombNeighbors} ");
                    }
                    else if (board.Cells[row, col].NumberOfBombNeighbors == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write($"{board.Cells[row, col].NumberOfBombNeighbors} ");
                    }
                    else if (board.Cells[row, col].NumberOfBombNeighbors == 4)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write($"{board.Cells[row, col].NumberOfBombNeighbors} ");
                    }
                    else if (board.Cells[row, col].NumberOfBombNeighbors == 5)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write($"{board.Cells[row, col].NumberOfBombNeighbors} ");
                    }
                    else if (board.Cells[row, col].NumberOfBombNeighbors == 6)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write($"{board.Cells[row, col].NumberOfBombNeighbors} ");
                    }
                    else if (board.Cells[row, col].NumberOfBombNeighbors == 7)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write($"{board.Cells[row, col].NumberOfBombNeighbors} ");
                    }
                    else if (board.Cells[row, col].NumberOfBombNeighbors == 8)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write($"{board.Cells[row, col].NumberOfBombNeighbors} ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(". ");
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine(new string('-', board.Size * 2 + 3));
        }
    }
}
