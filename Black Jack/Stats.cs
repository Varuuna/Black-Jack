using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BlackJack
{
	public class Stats
	{
		// Prints the stats from the file to the player when requested.
		public void GetStats()
		{
			string[] stats = File.ReadAllLines(@".\Stats");

			Console.WriteLine();
			Console.WriteLine("----------Stats----------");
			Console.WriteLine(" Player total wins:   " + stats[0]);
			Console.WriteLine(" Dealer total wins:   " + stats[1]);
			Console.WriteLine("-------------------------");
		}

		/* Tries to open a file "Stats" and writes the updated stats to the file.
		 * If the file doesn't exist (first-time run) it will be created in the same
		 * folder as the game itself. */
		public void StatsToFile(int WhoWon)
		{
			try
			{
				/* Opens a file and reads the previous recorded stats [0] for the player
				 * and [1] for the dealer. */
				string[] stats = File.ReadAllLines(@".\Stats");
				int PlayerStats = int.Parse(stats[0]);
				int DealerStats = int.Parse(stats[1]);

				// 'WhoWon' will be 0 if the player won and 1 if the dealer won.
				if (WhoWon == 0)
				{
					PlayerStats++;
					stats[0] = PlayerStats.ToString();
				}
				else if (WhoWon == 1)
				{
					DealerStats++;
					stats[1] = DealerStats.ToString();
				}

				File.WriteAllLines(@".\Stats", stats);
			}
			catch (Exception)
			{
				File.WriteAllText(@".\Stats", "0" + "\r\n" + "0");
			}
		}
	}
}
