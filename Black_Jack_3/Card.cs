﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black_Jack_3
{
	public class Card
	{
		public Color Suit { get; set; }
		public int Value { get; set; }

	}

	public enum Color
	{
		Spades,
		Hearts,
		Clubs,
		Diamonds
	}
}
