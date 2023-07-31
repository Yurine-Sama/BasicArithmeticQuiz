using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to the Basic Arithmetic Quiz");
        Console.WriteLine("Type 'exit' to end the quiz.\n");

        Random random = new Random();
        int totalQuestions = 5;
        int correctAnswers = 0;

        for(int i = 0;  i < totalQuestions; i++)
        {
            int num1 = random.Next(1, 11);
            int num2 = random.Next(1, 11);
            string[] operation = { "+", "-", "*", "/", };
            string randomOperation = operation[random.Next(operation.Length)];

            Console.WriteLine($"Question {i+1}: What is {num1} {randomOperation} {num2}?");
            string userAnswer = Console.ReadLine();

            if(userAnswer.ToLower().Trim() == "exit")
            {
                Console.WriteLine("Quiz ended.");
                break;
            }

            double correctResult = CalculateResult(num1, num2, randomOperation);

            if(double.TryParse(userAnswer, out double userResult) && userResult == correctResult) 
            {
                Console.WriteLine("Correct!");
                correctAnswers++;
            }
            else
            {
                Console.WriteLine($"Incorrect! the correct answer is {correctResult}");
            }

            Console.WriteLine();
        }

        Console.WriteLine($"You answered {correctAnswers} out of {totalQuestions} questions correctly.");
        Console.WriteLine("Thank for playing!");
    }

    static double CalculateResult(int num1, int num2, string op)
    {
        switch(op)
        {
            case ("+"):
                return num1 + num2;
            case ("-"):
                return num1 - num2;
            case ("*"):
                return num1 * num2;
            case ("/"):
                return num1 / num2;
            default:
                throw new ArgumentException("Invalid operator");
        }   
    }
}