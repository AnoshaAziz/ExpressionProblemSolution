using System;

namespace ExpressionProblemSolution
{
    class Program
    {
        private static ExpressionAssignmentCalculator calculator = new ExpressionAssignmentCalculator();

        static void Main(string[] args)
        {
            Console.WriteLine("--------------Calculator started------------------\nenter 'exit' to terminate");
            // Uncommment this section if you want to take user input
            //while (true)
            //{
            //    var input = Console.ReadLine();
            //    if (input != null)
            //    {
            //        if (input.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
            //        {
            //            break;
            //        }
            //        else
            //        {
            //            calculator.Calculate(input);
            //        }
            //    }
            //}

            MockUserInput();
        }

        static void MockUserInput()
        {
            Console.WriteLine("a = 3");
            calculator.Calculate("a = 3");
            Console.WriteLine("b = 2 + 4");
            calculator.Calculate("b = 2 + 4");
            Console.WriteLine("c = a");
            calculator.Calculate("c = a");
            Console.WriteLine("d = b + 7");
            calculator.Calculate("d = b + 7");
            Console.WriteLine("A = b + c + d");
            calculator.Calculate("A = b + c + d");
            Console.WriteLine("f = g");
            calculator.Calculate("f = g");
            Console.WriteLine("g = 1 + 2 + 3");
            calculator.Calculate("g = 1 + 2 + 3");
            Console.WriteLine("h = i + A + lemon");
            calculator.Calculate("h = i + A + lemon");
            Console.WriteLine("lemon = k + lima");
            calculator.Calculate("lemon = k + lima");
            Console.WriteLine("i = A + 5");
            calculator.Calculate("i = A + 5");
            Console.WriteLine("k = a + b");
            calculator.Calculate("k = a + b");
            Console.WriteLine("lima = k + k");
            calculator.Calculate("lima = k + k");
        }
    }
}
