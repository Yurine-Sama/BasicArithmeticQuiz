using System;
using System.Threading;

class Program
{
    static void Main()
    {
        do
        {
            Console.WriteLine("Welcome to the Basic Arithmetic Quiz");
            Console.WriteLine("Select the mode:");
            Console.WriteLine("1. Easy");
            Console.WriteLine("2. Normal");
            Console.WriteLine("3. Hard");
            Console.WriteLine("4. Challenge");
            Console.WriteLine("Enter the number corresponding to your choice");
            Console.WriteLine("Type 'exit' to end the quiz.\n");

            int selectMode = int.Parse(Console.ReadLine());

            Random random = new Random();

            int totalQuestions = 5;

            int correctAnswers = 0;
            int maxNumber;
            int minNumber;
            string[] operation = { "+", "-", "*", "/", };

            switch (selectMode)
            {
                case 1:
                    //Easy mode (Numbers range: 1 to 10)
                    minNumber = 1;
                    maxNumber = 10;
                    break;
                case 2:
                    //Normal mode (Numbers range: 1 to 50)
                    minNumber = 1;
                    maxNumber = 50;
                    break;
                case 3:
                    //Hard mode (Numbers range: 1 to 100)
                    minNumber = 1;
                    maxNumber = 100;
                    break;
                case 4:
                    //Challenge mode (Number range: 1 to 1000)
                    minNumber = 1;
                    maxNumber = 1000;
                    break;
                default:
                    Console.WriteLine("Invalid mode selection. Exiting the quiz");
                    return;
            }

            for (int i = 0; i < totalQuestions; i++)
            {
                int num1 = random.Next(minNumber, maxNumber + 1);
                int num2 = random.Next(minNumber, maxNumber + 1);

                string randomOperator = operation[random.Next(operation.Length)];

                Console.Write($"Question {i + 1}: What is {num1} {randomOperator} {num2}? ");

                if (selectMode == 4)
                {
                    int timeLimitInSeconds = 10;
                    Console.Write($"You have {timeLimitInSeconds} seconds to answer. =>   ");
                   

                    bool timeIsUp = false;
                    bool userHasAnswered = false;

                    // Create a timer to track the time remaining for each question
                    var timer = new Timer((state) =>
                    {
                        timeIsUp = true;
                    }, null, timeLimitInSeconds * 1000, Timeout.Infinite);

                    // Display countdown timer and read the user's answer
                    for (int remainingTime = timeLimitInSeconds; remainingTime > 0; remainingTime--)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 5, Console.CursorTop); // Move cursor three positions back (to overwrite the previous countdown)
                        Console.Write($"{remainingTime:00}   "); // The ":00" format ensures that the number is displayed with leading zeros

                        if (timeIsUp)
                        {

                            break; // Immediately move to the next question
                        }

                        if (Console.KeyAvailable)
                        {
                            userHasAnswered = true;
                            break;
                        }

                        Thread.Sleep(1000); // Wait for one second
                    }

                    // Stop the timer when the user inputs an answer or time runs out
                    timer.Change(Timeout.Infinite, Timeout.Infinite);

                    Console.WriteLine();

                    if (timeIsUp)
                    {
                        Console.WriteLine("Time's up! Moving to the next question.");
                    }


                    if (userHasAnswered)
                    {
                        string userAnswer = Console.ReadLine();


                        if (userAnswer.ToLower().Trim() == "exit")
                        {
                            Console.WriteLine("Quiz ended.");
                            break;
                        }

                        double correctResult = CalculateResult(num1, num2, randomOperator);

                        //Define the tolerance for division answers
                        double tolerance = 0.01; // Accept answers within 0.01 difference

                        if (double.TryParse(userAnswer, out double userResult) && Math.Abs(userResult - correctResult) <= tolerance)
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
                }
                else
                {
                    Console.WriteLine();
                    string userAnswer = Console.ReadLine();


                    if (userAnswer.ToLower().Trim() == "exit")
                    {
                        Console.WriteLine("Quiz ended.");
                        break;
                    }

                    double correctResult = CalculateResult(num1, num2, randomOperator);

                    //Define the tolerance for division answers
                    double tolerance = 0.01; // Accept answers within 0.01 difference

                    if (double.TryParse(userAnswer, out double userResult) && Math.Abs(userResult - correctResult) <= tolerance)
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

            }

            Console.WriteLine($"You answered {correctAnswers} out of {totalQuestions} questions correctly.");
            Console.WriteLine("Do you want to play again? (y/n): ");
            string playAgainResponse = Console.ReadLine();

            if (playAgainResponse.Equals("y", StringComparison.OrdinalIgnoreCase) ||
               playAgainResponse.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                Console.Clear();
                continue;
            }
            else if (playAgainResponse.Equals("n", StringComparison.OrdinalIgnoreCase) ||
                      playAgainResponse.Equals("no", StringComparison.OrdinalIgnoreCase))
            {
                // If user does not want to play again, break the loop and exit the program
                break;
            }
            else
            {
                Console.WriteLine("Invalid response. Existing the quiz");
            }
        } while (true);

        Console.WriteLine("Thank for playing!");
    }

    static double CalculateResult(int num1, int num2, string op)
    {
        switch (op)
        {
            case ("+"):
                return num1 + num2;
            case ("-"):
                return num1 - num2;
            case ("*"):
                return num1 * num2;
            case ("/"):
                return (double)num1 / num2;
            default:
                throw new ArgumentException("Invalid operator");
        }
    }
}