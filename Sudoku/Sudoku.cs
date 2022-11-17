namespace Sudoku
{
    internal class Sudoku
    {
        private readonly int[,] sudoku = new int[9, 9]; // Where We Save The Sudoku

        private int[] selection = { 1, 2, 3, 4, 5, 6, 7, 8, 9 }; // This Method Of A Predefined Selection Worked The Best So Far

        private readonly string path = Directory.GetCurrentDirectory() + "/sudoku.txt"; // Where To Create / Append The Export

        public bool Setup() // App Control
        {
            if (Desicion('m')) // M == Mode => True == Generate
            { // The Original Method Was A Bool And Because I Merged Another Method Into This One True Means Generate
                Console.WriteLine(Message.Generate);
                Algorithm('g'); // G == Generate
            }
            else // Therefore False Means Solving A Board
            {
                Input(); // The We Need A Board Inputted By The User
                if (BoardCheckup()) // Input Board Is Valid
                {
                    Console.WriteLine(Message.Valid); // We Output The Fact That It's Valid
                    Algorithm('s'); // S == Solve
                    Console.WriteLine(Message.Solve);
                }
                else
                {
                    Console.WriteLine(Message.Invalid); // We Output The Fact That It's NON-Valid
                }
            }

            Output('o'); // O == Output To Console

            if (Desicion('s')) // S == Save (Export)
            {
                Output('e'); // E == Export To TXT
            }

            ResetArray(); // Fill The Array With [0]

            return Desicion('r'); // R == Restart
        }

        private bool Algorithm(char mode) // Generating Or Solving
        {
            for (int y = 0; y < sudoku.GetLength(1); y++) // Getting The Entire Board-Array By The Y Axis
            {
                for (int x = 0; x < sudoku.GetLength(0); x++) // Getting The Entire Board-Array By The X Axis
                {
                    if (sudoku[y, x] == 0) // If We Find A 0
                    {
                        for (int i = 0; i < sudoku.GetLength(0); i++) // Loop 9 Times For Numbers 1 - 9
                        {
                            if (mode == 'g') // If It Is For Generating A Board The Selection Should Be Random
                            {
                                ShuffleSelection();
                            } // For Solving It Is Better If We Don't Shuffle (Tested)

                            if (Valid(selection[i], x, y)) // If The Number At The Current Position Works
                            {
                                sudoku[y, x] = selection[i]; // Add It To The Board

                                if (Algorithm(mode)) // Recursive Call To The Function Is Efficient Because We Only Focus On Digit [0]
                                {
                                    return true; // # Which Will True / Close The Other Calls
                                }
                                else
                                {
                                    sudoku[y, x] = 0; // We Set The Last Number We Placed To [0] And Try The Rest Of Them
                                    // We Only Have To Set The Previous One To [0] Because We Only Place Other Numbers When They Are Valid
                                    // Which Is Not The Case And Therefore Only The Previous Positions Have Numbers Other Than [0]
                                }
                            }
                        }
                        return false; // If Numbers 1 - 9 Couldn't Be Legally Placed
                    }
                }
            }
            return true; // # If The Entire Board Has Been Filled
        }

        private bool BoardCheckup() // Checking Board Validation
        {
            for (int y = 0; y < sudoku.GetLength(1); y++) // Looping Through The Y Dimension
            {
                for (int x = 0; x < sudoku.GetLength(0); x++) // Looping Through The X Dimension
                {
                    if (sudoku[y, x] != 0) // We Only Need To Check Numbers That Are Inputted By The User
                    {
                        int number = sudoku[y, x]; // We Save The Number At The Current Position
                        sudoku[y, x] = 0; // That The Board Position To [0] Because The Valid Check Does Not Skip The Current Position

                        if (!Valid(number, x, y)) // If It's Not Valid The Board Returns False
                        {
                            return false;
                        }
                        sudoku[y, x] = number; // Otherwise We Can Be Sure It Is Valid And Return The Number Back Into The Board
                    }
                }
            }
            return true; // If We Loop Through All Without Interference The Board Is Valid
        }

        private bool VerticalCheckup(int number, int x)
        {
            for (int i = 0; i < sudoku.GetLength(1); i++) // Looping Through The Y Axis So X Must Be Fixed
            {
                if (sudoku[i, x] == number)
                {
                    return true; // If We Find The Number It's True For Finding A Duplicate
                }
            }
            return false; // Otherwise It Must Be False
        }

        private bool HorizontalCheckup(int number, int y)
        {
            for (int i = 0; i < sudoku.GetLength(0); i++) // Looping Through The X Axis So Y Must Be Fixed
            {
                if (sudoku[y, i] == number)
                {
                    return true; // If We Find The Number It's True For Finding A Duplicate
                }
            }
            return false; // Otherwise It Must Be False
        }

        private bool GridCheckup(int number, int xPos, int yPos)
        {
            int xStart = xPos - xPos % 3;
            int yStart = yPos - yPos % 3;
            /*
             * After Understanding That A Smaller Number Modulo A Bigger One Always Returns The Smaller Number
             * It Was Quite Easy To Figure This Out By Using The Width And Height Which Is Always 3
             * To Get The Starting Position Of The Sub-Grid We're In
             */
            for (int y = yStart; y < yStart + 3; y++) // First Top Position Of The Sub-Grid And Want To End At + 3
            {
                for (int x = xStart; x < xStart + 3; x++) // First Left Position Of The Sub-Grid And Want To End At + 3
                {
                    if (sudoku[y, x] == number)
                    {
                        return true; // If We Find The Number It's True For Finding A Duplicate
                    }
                }
            }
            return false; // Otherwise It Must Be False
        }

        private bool Valid(int number, int x, int y)
        {
            // Because They All Return True If A Number Is A Duplicate But We Are Asking Whether It Is Valid
            // Inverting The Result Will Logically Answer The Question More Precisely
            return !VerticalCheckup(number, x)
            && !HorizontalCheckup(number, y)
            && !GridCheckup(number, x, y);
        }

        private void ShuffleSelection()
        {
            Random rnd = new();
            selection = selection.OrderBy(x => rnd.Next()).ToArray(); // Some Research + Magic == Shuffle Array
        }

        private void Input() // Allows User To Input A Board To Solve
        {
            bool success = false;

            Console.WriteLine(Message.Rules); // Printing Rules 😱
            Console.WriteLine("\nWhiteSpaces Will Be Ignored So You Can Use It");
            Console.WriteLine("\nEmpty Fields Are To Be Represented By A [0]");
            Console.WriteLine("\nLeaving The Input Empty Will Reset To Row [1]");
            Console.WriteLine(Message.Input);

            Console.CursorVisible = true; // Now It Should Probably Be Active. Right ❓
            while (!success) // Loops Until U Figured Out How To Follow My Rules 😜
            {
                for (int y = 0; y < sudoku.GetLength(0); y++)
                {
                    Console.Write("\nInput Row [" + (y + 1) + "]: ");
                    string? input = Console.ReadLine(); // I Made It Null-Able Because Visual Studio Told Me To Do It

                    if (input != null) // Therefore It Was A Safer Option To Make Sure That It Isn't Null
                    {
                        input = input.Replace(" ", ""); // So You Can Use WhiteSpace For Input If U Wanted To

                        if (input == String.Empty) // Like I Stated In My Rules If U Need To Start Again Enter Nothing
                        {
                            y = -1;
                            continue;
                        }

                        if (int.TryParse(input, out _) && input.Length == 9) // Otherwise We Just Reset The Current Row
                        {
                            for (int x = 0, i = 0; x < sudoku.GetLength(0); x++, i++)
                            {
                                sudoku[y, x] = input[i] - 48; // ASCII Magic
                            }
                        }
                        else
                        {
                            Console.WriteLine("Your Input Is Invalid"); // Well Duh
                            y--;
                            continue;
                        }
                    }
                }
                success = true; // We Did It 🥳
            }
            Console.CursorVisible = false;
        }

        private void Output(char mode) // Either Console Or TXT Export
        {
            // 'O' Stands For Console Output And 'E' For The TXT Export
            string ySep = new('=', 25);
            if (mode == 'o')
            {
                Console.WriteLine();
            }
            else if (mode == 'e')
            {
                DateTime timeStamp = DateTime.Now; // Create A Date Object Because Internet Told You To
                File.AppendAllText(path, "Exported At: " + timeStamp + "\n");
            }
            for (int y = 0; y < sudoku.GetLength(1); y++) // The Rest Is Some Math Logic Fiddling And Trying As I Go Along
                                                          // It May Look Complicated At First Glance But It's Mostly Because I Merged The Console Output With The Export
            {
                if (y % 3 == 0)
                {
                    if (mode == 'o')
                    {
                        Console.WriteLine(ySep);
                    }
                    else if (mode == 'e')
                    {
                        File.AppendAllText(path, ySep + "\n");
                    }
                }
                for (int x = 0; x < sudoku.GetLength(0); x++)
                {
                    if (x % 3 == 0)
                    {
                        if (mode == 'o')
                        {
                            Console.Write("| ");
                        }
                        else if (mode == 'e')
                        {
                            File.AppendAllText(path, "| ");
                        }
                    }
                    if (mode == 'o')
                    {
                        Console.Write(sudoku[y, x] + " ");
                    }
                    else if (mode == 'e')
                    {
                        File.AppendAllText(path, sudoku[y, x] + " ");
                    }
                }
                if (mode == 'o')
                {
                    Console.Write("|\n");
                }
                else if (mode == 'e')
                {
                    File.AppendAllText(path, "|\n");
                }
            }
            if (mode == 'o')
            {
                Console.WriteLine(ySep);
            }
            else if (mode == 'e')
            {
                File.AppendAllText(path, ySep + "\n");
                Console.WriteLine("\nExported At: " + path);
            }
        }

        private void ResetArray() // Fills The Array With [0] After The Option To Export
        {
            for (int y = 0; y < sudoku.GetLength(1); y++)
            {
                for (int x = 0; x < sudoku.GetLength(0); x++)
                {
                    sudoku[y, x] = 0;
                }
            }
        }

        private static bool Desicion(char type)
        {
            while (true) // Like I Said In The Setup() I Merged 2 Methods Into One So It May Look Big But It's Simple
            {
                if (type == 'm')
                {
                    Console.WriteLine("\nDo You Want To (G)enerate Or (S)olve A Sudoku? (G|S)");
                    char answer = Console.ReadKey(true).KeyChar;

                    if (answer == 'g')
                    {
                        return true;
                    }
                    else if (answer == 's')
                    {
                        return false;
                    }
                    Console.WriteLine("This Answer Won't Do At All :/");
                }
                else if (type == 'r' || type == 's')
                {
                    if (type == 's')
                    {
                        Console.WriteLine("\nDo You Want To Export? (Y|N)");
                    }
                    else if (type == 'r')
                    {
                        Console.WriteLine("\nDo You Want To Restart? (Y|N)");
                    }
                    char answer = Console.ReadKey(true).KeyChar;

                    if (answer == 'y')
                    {
                        return true;
                    }
                    else if (answer == 'n')
                    {
                        return false;
                    }
                    Console.WriteLine("This Answer Won't Do At All :/");
                }
            }
        }
    }
}