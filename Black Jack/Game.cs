using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
	public class Game
	{
		public Deck deck { get; set; }
		public Player player { get; set; }
		public Player dealer { get; set; }

		public static void StartGame()
		{
			/* Tries to open the file "Stats" but will not write anything to it, should it exist.
			 * If it doesn't exist, the file will be created and its contents set to "0" + "0".
			 * This is only done once per start-up. */
			Stats stats = new Stats();
			stats.StatsToFile(-1);
			
			// Welcomes the player, allowing him to input his/her name.
			Console.WriteLine("Welcome to Black Jack! (v1.2)\nCreated by: Fredrik Andreasson\n");
			Console.Write("Please enter your name: ");
			string playername = Console.ReadLine();

			/* This string needs to be outside the do-while in order to be recognized at the very bottom.
			 * It is used to restart the game and recreate all the needed objects from scratch.
			 * Otherwise, we'd be left with an incomplete deck to start with. */
			string Regame;
			do
			{
				/* Clears the console from junk. Useless at first-time run, amazing when playing
				 * more than one round in a row. */
				Console.Clear();

				/* Creates necessary objects: The deck, the player and the dealer.
				 
				 * NOTE: Previously the creation of the player and dealer objects were
				 * above the do-while. While this was perfectly fine, I wanted to allow players
				 * to restart new rounds without entering a new name every time.
				 * This didn't work as the object would not be created anew and would remember the old Hands
				 * of both players. This solution fixed that while still allowing the player to play several rounds
				 * without having to enter his/her name every time. */
				Deck deck = new Deck();
				Player player = new Player()
					{
						Name = playername
					};
				Player dealer = new Player();

				/* Needed variables. PlayerSum and DealerSum are used to calculate the total of
				 * the hands of both the Player and the Dealer. CardName is used to print the proper name
				 * of chosen card into the console. (i.e. "Ace of Hearts", rather than "1 of Hearts") */
				int PlayerSum = 0, DealerSum = 0;
				string CardName;

				/* Explanations of the game. Hit 'f' to continue, 's' to stay, 'y' to play again when the game asks,
				 * 'n' to stop playing and exit the game. The player can alse press 'q' at any time to view recorded stats. */
				Console.WriteLine("\nSit down at the table " + player.Name + " and let me quickly explain how to play.");
				Console.WriteLine("It's simple. Just press 'F' to draw more cards or 'S' to stay where you are.");
				Console.WriteLine("When the game is over, press 'Y' to play again or 'N' to stop playing.");
				Console.WriteLine("\nYou can press 'Q' at any time to view recorded\nstats between the player and dealer.\n");
				Console.WriteLine("Let's begin!\n");

				// Player gets his first card.
				Console.WriteLine("------------------------------\n Your first card is:\n");
				Card playercard = deck.GetCard();
				CardName = deck.GetCardName(playercard);

				/* Card gets printed out into the console and gets added to the hand,
				 * it then removes the card from the deck. It also checks if the card is an Ace, Jack, Queen or King
				 * and properly outputs the names rather than "1", "11", "12" or "13" of <suit> */
				Console.WriteLine("    " + CardName);
				player.AddCard(playercard);
				deck.deck.Remove(playercard);
				// Gets the sum of the player's hand.
				PlayerSum = player.GetValue();
				Console.WriteLine("\n Total of your hand: " + PlayerSum + "\n------------------------------");

				// Same for the dealer.
				Console.WriteLine("\n------------------------------\n The dealer's first card is:\n");
				Card dealercard = deck.GetCard();

				CardName = deck.GetCardName(dealercard);
				Console.WriteLine("    " + CardName);

				dealer.AddCard(dealercard);
				deck.deck.Remove(dealercard);
				DealerSum = dealer.GetValue();
				Console.WriteLine("\n Total of the dealer's hand: " + DealerSum + "\n------------------------------");

				// Player starts making the choices here.

				/* Using 'ConsoleKeyInfo' and 'Console.ReadKey()' allows the player to simple hit the button
				 * he/she would like to press without having to press 'enter' every time afterwards to confirm. */
				ConsoleKeyInfo Choice;
				do{
					Choice = Console.ReadKey();

					/* If the player hits 'f', he gets a new card. Game checks for the total of his hand
					 * and determines if the player has won through Black Jack, lost through busting
					 * or neither, in which the player can choose to draw again */
					if (Choice.Key == ConsoleKey.F)
					{
						Console.WriteLine(" - YOU DRAW!");

						playercard = deck.GetCard();
						CardName = deck.GetCardName(playercard);
						Console.WriteLine("------------------------------\n You got:\n");
						Console.WriteLine("    " + CardName);
						player.AddCard(playercard);
						deck.deck.Remove(playercard);

						PlayerSum = player.GetValue();
						Console.WriteLine("\n Total of your hand: " + PlayerSum + "\n------------------------------");
						if (PlayerSum >= 22)
						{
							Console.WriteLine("------------------------------\nBUST! Your hand exceeded 21!\n------------------------------");
							stats.StatsToFile(1);
							break;
						}
						else if (PlayerSum == 21)
						{
							Console.WriteLine("------------------------------\nBLACKJACK! You win!\n------------------------------");
							stats.StatsToFile(0);
							break;
						}
					}

					/* The player has pressed the 's' key. The dealer draws cards.
					 * The dealer will continue drawing until he has at least a total hand-value of 17.
					 * The dealer will automatically lose if he busts. */
					else if (Choice.Key == ConsoleKey.S)
					{
						Console.WriteLine(" - YOU STAY! DEALER'S TURN!");
						while (DealerSum < 17)
						{
							dealercard = deck.GetCard();
							CardName = deck.GetCardName(dealercard);

							Console.WriteLine("------------------------------\n The dealer draws:\n");
							Console.WriteLine("    " + CardName);

							dealer.AddCard(dealercard);
							deck.deck.Remove(dealercard);

							DealerSum = dealer.GetValue();
							Console.WriteLine("\n Total of the dealer's hand: " + DealerSum + "\n------------------------------");
							if (DealerSum >= 22)
							{
								Console.WriteLine("------------------------------\nThe dealer busts! You WIN!\n------------------------------");
								stats.StatsToFile(0);
								break;
							}
						}

						/* If neither the player nor the dealer has gotten an instant win or loss through
						 * the written code above, the game will check the difference in hand-values between
						 * the player and the dealer to determine who is the winner. */
						if (PlayerSum > DealerSum)
						{
							Console.WriteLine("------------------------------\nYou have the higher hand! You WIN!\n------------------------------");
							stats.StatsToFile(0);
						}
						else if (PlayerSum < DealerSum && DealerSum <= 21)
						{
							Console.WriteLine("------------------------------\nThe dealer has the higher hand! Aww, you LOSE!\n------------------------------");
							stats.StatsToFile(1);
						}
						else if (PlayerSum == DealerSum)
						{
							Console.WriteLine("------------------------------\nYou and the dealer stopped at the same total! It's a DRAW!\n------------------------------");
							stats.StatsToFile(-1);
						}

					}

					// If the player presses 'Q', the stats (from a file) will be shown on screen.
					else if (Choice.Key == ConsoleKey.Q)
					{
						stats.GetStats();
					}

				} while (Choice.Key != ConsoleKey.S);

				// Want to play again?
				Console.WriteLine("Do you want to play again? (y)es or (n)o.");
				Console.WriteLine("(You can also check stats now by pressing 'Q'.");
				Regame = null;

				/* Usually I wouldn't use a "never-ending loop" such as this, but I felt
				 * the impact it might've had was too insignificant to worry about.
				 * The loop will end immediately if a valid key was pressed, and will continue
				 * to loop until such action occurs. */
				while (1 != 0)
				{
					ConsoleKeyInfo Rematch = Console.ReadKey();
					if (Rematch.Key == ConsoleKey.Y)
					{
						Regame = "yes";
						break;
					}
					else if (Rematch.Key == ConsoleKey.N)
					{
						Regame = "no";
						break;
					}
					else if (Rematch.Key == ConsoleKey.Q)
					{
						stats.GetStats();
					}
					else
					{
						Console.WriteLine("Invalid key pressed!");
					}
				}

			} while (Regame == "yes");
		}
	}
}
