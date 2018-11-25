using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hangman_New
{
    static class Globals
    {
        //The location of the wordlist
        public static string wordLocation = AppDomain.CurrentDomain.BaseDirectory + "words.txt";
    }

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                //All variables/lists used within
                string word = newRnd();
                List<char> wordchars = new List<char>();
                List<char> guesschars = new List<char>();
                List<char> usedchars = new List<char>();
                string usedstring;
                string wordstring;
                string opt;
                char guess;
                bool won = false;

                //Used to generate a list of chars from the generated word
                foreach (char c in word)
                {
                    wordchars.Add(c);
                    guesschars.Add('_');
                }

                //The cycle of the game
                for (int x = 1; x < 7; x++)
                {
                    Console.Clear();
                    drawSplash();

                    //Checks if there are any chars in usedchars, needs to be at least 1
                    if (usedchars.Any())
                        gen_guess(word, guesschars, wordchars, usedchars);

                    Console.WriteLine("Debug: " + word);
                    //Draws the hangman ascii
                    draw(x);

                    //Displays the guessed characters
                    foreach (char c in guesschars)
                        Console.Write(c + " ");

                    //Displays used characters when guessing
                    Console.WriteLine("\n");
                    if (usedchars.Any())
                    {
                        Console.Write("Used: ");
                        foreach (char c in usedchars)
                            Console.Write(c + " ");
                    }
                    else
                        Console.Write("Used: ");

                    //Used for the string comparison to trigger a win
                    usedstring = string.Join(",", usedchars.ToArray());
                    wordstring = string.Join(",", wordchars.ToArray());

                    //Check to see if you have guessed the word
                    if (usedstring == wordstring)
                    {
                        Console.Clear();
                        centerText(@"__   __            _    _ _         _ ");
                        centerText(@"\ \ / /           | |  | (_)       | |");
                        centerText(@" \ V /___  _   _  | |  | |_ _ __   | |");
                        centerText(@"  \ // _ \| | | | | |/\| | | '_ \  | |");
                        centerText(@"  | | (_) | |_| | \  /\  / | | | | |_|");
                        centerText(@"  \_/\___/ \__,_|  \/  \/|_|_| |_| (_)");
                        Console.WriteLine();
                        centerText("The word was: " + word);
                        won = true;
                    }

                    //Input guess from the user
                    if (won == false)
                    {
                        while (true)
                        {
                            Console.Write("\n\nEnter character guess: ");
                            string guessString = Console.ReadLine();
                            if (!char.TryParse(guessString, out guess))
                            {
                                Console.WriteLine("'{0}' is not a character", guessString);
                            }
                            else
                                break;
                        }

                        usedchars.Add(guess);
                    }
                }
                if (won == false)
                {
                    Console.Clear();
                    centerText(@"__   __            _                      _ ");
                    centerText(@"\ \ / /           | |                    | |");
                    centerText(@" \ V /___  _   _  | |     ___  ___  ___  | |");
                    centerText(@"  \ // _ \| | | | | |    / _ \/ __|/ _ \ | |");
                    centerText(@"  | | (_) | |_| | | |___| (_) \__ \  __/ |_|");
                    centerText(@"  \_/\___/ \__,_| \_____/\___/|___/\___| (_)");
                    Console.WriteLine();
                    centerText("The word was: " + word);
                    Console.ReadLine();
                }

                while (true)
                {
                    //Checks if the player wants to play again
                    Console.Clear();
                    Console.Write("Do you want to play again? Y or N: ");
                    opt = Console.ReadLine();

                    if (opt.ToUpper() == "N")
                        Environment.Exit(0);
                    //Checks if the input is invalid
                    else if (opt.ToUpper() != "Y" && opt.ToUpper() != "N")
                    {
                        Console.WriteLine("Error! Invalid input");
                    }
                }
            }
        }

        //Rand function used to pick a word from a list 
        static string newRnd()
        {
            string[] words;
            words = File.ReadAllLines(Globals.wordLocation);

            Random rnd = new Random();
            int wNum = rnd.Next(0, words.Length);
            
            return words[wNum];
        }

        //Function to check the chars guessed against the chars in the word
        static string gen_guess(string word, List<char> guesschars, List<char> wordchars, List<char> usedchars)
        {
            for (int x = 0; x < wordchars.Count; x++)
            {
                if (usedchars.Contains(wordchars[x]))
                {
                    guesschars.RemoveAt(x);
                    guesschars.Insert(x, wordchars[x]);
                }
            }

            return "";
        }

        //Function used to center text on the cmd
        static string centerText(string textToEnter)
        {
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnter.Length / 2)) + "}", textToEnter));

            return "";
        }

        static void drawSplash()
        {
            Console.WriteLine();
            centerText("██╗  ██╗ █████╗ ███╗   ██╗ ██████╗ ███╗   ███╗ █████╗ ███╗   ██╗");
            centerText("██║  ██║██╔══██╗████╗  ██║██╔════╝ ████╗ ████║██╔══██╗████╗  ██║");
            centerText("███████║███████║██╔██╗ ██║██║  ███╗██╔████╔██║███████║██╔██╗ ██║");
            centerText("██╔══██║██╔══██║██║╚██╗██║██║   ██║██║╚██╔╝██║██╔══██║██║╚██╗██║");
            centerText("██║  ██║██║  ██║██║ ╚████║╚██████╔╝██║ ╚═╝ ██║██║  ██║██║ ╚████║");
            centerText("╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚═╝     ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝");
        }

        //Draws the hangman ascii
        static string draw(int n)
        {
            switch (n)
            {
                case 1:
                    Console.WriteLine(@"  +---+
  |   |
      |
      |
      |
      |
========= ");
                    break;

                case 2:
                    Console.WriteLine(@"  +---+
  |   |
  O   |
      |
      |
      |
=========");
                    break;

                case 3:
                    Console.WriteLine(@"  +---+
  |   |
  O   |
  |   |
      |
      |
=========");
                    break;

                case 4:
                    Console.WriteLine(@"  +---+
  |   |
  O   |
 /|   |
      |
      |
=========");
                    break;

                case 5:
                    Console.WriteLine(@"  +---+
  |   |
  O   |
 /|\  |
      |
      |
=========");
                    break;

                case 6:
                    Console.WriteLine(@"  +---+
  |   |
  O   |
 /|\  |
 /    |
      |
=========");
                    break;

                case 7:
                    Console.WriteLine(@"  +---+
  |   |
  O   |
 /|\  |
 / \  |
      |
=========");
                    break;
            }

            return "";
        }
    }
}