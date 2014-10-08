using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BlackJack
{
	public class Deck
	{
		Random random = new Random();
		public List<Card> deck = new List<Card>();
		public Card card { get; set; }
		public int MaxValueRand = 52;

		// Creates all the cards and adds them to the deck.
		public Deck()
		{
			for (int i = 1; i < 14; i++)
			{
				Card card = new Card()
				{
					Value = i,
					Suit = Color.Spades
				};
				deck.Add(card);
			}

			for (int i = 1; i < 14; i++)
			{
				Card card = new Card()
				{
					Value = i,
					Suit = Color.Hearts
				};
				deck.Add(card);

			}

			for (int i = 1; i < 14; i++)
			{
				Card card = new Card()
				{
					Value = i,
					Suit = Color.Clubs
				};
				deck.Add(card);

			}

			for (int i = 1; i < 14; i++)
			{
				Card card = new Card()
				{
					Value = i,
					Suit = Color.Diamonds
				};
				deck.Add(card);

			}
		}

		/* Randomly generates a card from the deck. Also removes that card from the
		 * deck so that it may never be drawn again in the same round. */
		public Card GetCard()
		{
			int card = random.Next(0, MaxValueRand);
			MaxValueRand--;
			return deck[card];
		}

		// Gets the proper name of a card to output to console.
		public string GetCardName(Card card)
		{
			if (card.Value == 1)
			{
				return ("Ace of " + card.Suit);
			}
			else if (card.Value == 11)
			{
				return ("Jack of " + card.Suit);
			}
			else if (card.Value == 12)
			{
				return ("Queen of " + card.Suit);
			}
			else if (card.Value == 13)
			{
				return ("King of " + card.Suit);
			}
			else
			{
				return (card.Value + " of " + card.Suit);
			}
		}
	}
}
