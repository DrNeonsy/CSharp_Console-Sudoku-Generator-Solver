namespace Sudoku
{
    internal class App
    {
        static void Main(string[] _) // Missile Entry Point ;)
        {
            Console.CursorVisible = false; // Because It Just Looks Better
            Sudoku app = new(); // Getting Access To The Actual App

            Console.WriteLine(Message.Start); // Printing My Predefined String Resource
            Thread.Sleep(1239); // Pause

            while (true)
            {
                if (!app.Setup()) // Break The Loop / Close The App When Restart == N
                {
                    break;
                }
            }

            Console.WriteLine(Message.Exit); // Printing My Predefined String Resource
            Thread.Sleep(1932); // Pause
        }
    }
}