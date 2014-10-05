using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black_Jack_3
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

		public int GetValue()
		{
			int Sum = 0;
			foreach (var item in Hand)
			{ 
				if (item.Value == 11 || item.Value == 12 || item.Value == 13)
				{
					Sum += 10;
				}
				else if (item.Value == 1 && 11 + Sum <= 21)
				{
					Sum += 11;
				}
				else if (item.Value == 1 && 11 + Sum > 21)
				{
					Sum += 1;
				}
				else
				{
					Sum += item.Value;
				}
				

			}
			return Sum;
		}
	}
}
