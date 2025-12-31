using System;

class NumberGuessingGame
{
    static void Main()
    {
        PlayGame();
    }

    static void PlayGame()
    {
        int targetNumber = GenerateRandomNumber();
        int guessCount = 0;
        bool isGuessedCorrectly = false;

        while (!isGuessedCorrectly)
        {
            string input = GetUserInput();
            
            if (!IsValidGuess(input))
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 100.");
                continue;
            }

            int guess = int.Parse(input);
            guessCount++;

            isGuessedCorrectly = CheckGuess(guess, targetNumber, guessCount);
        }
    }

    static int GenerateRandomNumber()
    {
        Random random = new Random();
        return random.Next(1, 101);
    }

    static string GetUserInput()
    {
        Console.Write("Guess a number between 1 and 100: ");
        return Console.ReadLine();
    }

    static bool IsValidGuess(string input)
    {
        return int.TryParse(input, out int number) && number >= 1 && number <= 100;
    }

    static bool CheckGuess(int guess, int targetNumber, int guessCount)
    {
        if (guess < targetNumber)
        {
            Console.WriteLine("Too low. Guess again.");
            return false;
        }
        else if (guess > targetNumber)
        {
            Console.WriteLine("Too high. Guess again.");
            return false;
        }
        else
        {
            Console.WriteLine($"You guessed it in {guessCount} guesses!");
            return true;
        }
    }
}
