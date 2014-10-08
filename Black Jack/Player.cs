using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
	public class Player
	{
		public string Name { get; set; }
		public List<Card> Hand = new List<Card>();

		// Adds the card generated from Deck.GetCard() to the List "Hand".
		public void AddCard(Card card)
		{
			Hand.Add(card);
		}

		/* Method for getting the correct sum of the hands of the
		 * player or dealer. */
		public int GetValue()
		{
			/* Linq is very nice.
			 * This line sorts the hand of the player/dealer into a new list.
			 * OrderByDescending (x.Value) sorts the hand with the card with the
			 * highest value as [0] and the lowest as the last card in the list. */
			List<Card> HandSorted = Hand.OrderByDescending(x => x.Value).ToList();

			int Sum = 0;
			foreach (var item in HandSorted)
			{ 
				// If the card is a Jack, Queen or King.
				if (item.Value == 11 || item.Value == 12 || item.Value == 13)
				{
					Sum += 10;
				}

				/* If the card is an Ace AND if adding the Ace's higher value would make						// Without sorting the hand, this block of code would always FAIL
				 * the sum less than, or equal to, 21. We add 11. */											// to get the correct value of the hand except for one specific occasion
				else if (item.Value == 1 && 11 + Sum <= 21)														// which was when the Ace was the very first card drawn into the hand.
				{																								//
					Sum += 11;																					// Every time Ace appeared as the 2nd, 3rd, 4th.... card, it would, more or less
				}																								// be correct, but if the Ace was checked as the FIRST card every time (even with
																												// five cards on hand) it would ALWAYS be it's higher value since it was never
				/* If the card is an Ace AND if adding the Ace's higher value would								// compared to the rest of the cards in the list.
				 * cause the player/dealer to bust. Add 1 instead of 11. */
				else if (item.Value == 1 && 11 + Sum > 21)
				{
					Sum += 1;
				}

				// For any other card, just add the value.
				else
				{
					Sum += item.Value;
				}
			}
			return Sum;
		}
	}
}
