using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black_Jack_3
{
	public class Game
	{
		public Deck deck { get; set; }
		public Player player { get; set; }
		public Player dealer { get; set; }

		public static void StartGame()
		{	
			// This string needs to be outside the do-while in order to be recognized at the very bottom.
			string Regame;
			do
			{
				// Creates necessary objects.
				Random random = new Random();
				Deck deck = new Deck();
				Player player = new Player();
				Player dealer = new Player();
				
				// Clears the console and resets everything. Welcomes the player and allows him to enter his/her name.
				Console.Clear();
				int PlayerSum = 0, DealerSum = 0;
				Console.WriteLine("Welcome to Black Jack! (v0.7)\nCreated by: Fredrik Andreasson\n");
				Console.Write("Please enter your name: ");
				player.Name = Console.ReadLine();
				Console.Clear();

				Console.WriteLine("Sit down at the table " + player.Name + " and let me quickly explain how to play.");
				Console.WriteLine("It's very simple. Just press 'f' to draw more cards or 's' to stop where you are.");
				Console.WriteLine("When the game is over, press 'y' to play again or 'n' to stop playing.");
				Console.WriteLine("Let's begin!\n");

				// Player gets his first card.
				Console.WriteLine("Your first card is:");
				Card playercard = deck.GetCard();
				// Card gets printed out into the console and gets added to the hand.
				Console.WriteLine(playercard.Value + " of " + playercard.Suit);
				player.AddCard(playercard);
				// Gets the sum of the player's hand.
				PlayerSum = player.GetValue();
				Console.WriteLine("Total of your hand: " + PlayerSum);

				// Same for the dealer.
				Console.WriteLine("\nThe dealer's first card is: ");
				Card dealercard = deck.GetCard();
				Console.WriteLine(dealercard.Value + " of " + dealercard.Suit);
				dealer.AddCard(dealercard);
				DealerSum = dealer.GetValue();
				Console.WriteLine("Total of the dealer's hand: " + DealerSum);

				// Player starts making the choices here.
				ConsoleKeyInfo Choice;
				do{
					Choice = Console.ReadKey();
					if (Choice.Key == ConsoleKey.F)
					{
						playercard = deck.GetCard();
						Console.WriteLine("\nYou got:");
						Console.WriteLine(playercard.Value + " of " + playercard.Suit);
						player.AddCard(playercard);

						PlayerSum = player.GetValue();
						Console.WriteLine("Total of your hand: " + PlayerSum);
						if (PlayerSum >= 22)
						{
							Console.WriteLine("BUST! Your hand exceeded 21!");
							break;
						}
						else if (PlayerSum == 21)
						{
							Console.WriteLine("BLACKJACK! You win!");
							break;
						}
					}
					else if (Choice.Key == ConsoleKey.S)
					{
						while (DealerSum < 17)
						{
							dealercard = deck.GetCard();
							Console.WriteLine("\nThe dealer draws:");
							Console.WriteLine(dealercard.Value + " of " + dealercard.Suit);
							dealer.AddCard(dealercard);

							DealerSum = dealer.GetValue();
							Console.WriteLine("Total of the dealer's hand: " + DealerSum);
							if (DealerSum >= 22)
							{
								Console.WriteLine("The dealer busts! You WIN!");
								break;
							}
						}

						if (PlayerSum > DealerSum)
						{
							Console.WriteLine("You have the higher hand! You WIN!");
						}
						else if (PlayerSum < DealerSum && DealerSum <= 21)
						{
							Console.WriteLine("The dealer has the higher hand! Aww, you LOSE!");
						}
						else if (PlayerSum == DealerSum)
						{
							Console.WriteLine("You and the dealer stopped at the same total! It's a DRAW!");
						}

					}

				} while (Choice.Key != ConsoleKey.S);

				// Want a re-game?
				Console.WriteLine("Do you want to play again? (y)es or (n)o.");
				ConsoleKeyInfo Rematch = Console.ReadKey();
				Regame = null;
				if (Rematch.Key == ConsoleKey.Y)
				{
					Regame = "yes";
				}
				else if (Rematch.Key == ConsoleKey.N)
				{
					Regame = "no";
				}

			} while (Regame == "yes");
		}
	}
}
