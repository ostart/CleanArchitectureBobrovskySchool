using System;

namespace CleanArchitectureBobrovskySchool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var robot = new RobotCleaner(TransferToCleaner);
            robot.Work("inputCommands.txt");
            Console.WriteLine("Goodbuy World!");
        }

        private static void TransferToCleaner(string message)
        {
            Console.WriteLine(message);
        }
    }
}
