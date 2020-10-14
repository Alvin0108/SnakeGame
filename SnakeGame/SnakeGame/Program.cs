using System;

namespace SnakeGame
{
	class Program
	{
		static void Main(string[] args)
		{
			// start game
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();

			// display this char on the console during the game
			string ch = "***";
			bool gameLive = true;
			ConsoleKeyInfo consoleKey; // holds whatever key is pressed

			// location info & display
			int x = 0, y = 2; // y is 2 to allow the top row for directions & space
			int dx = 1, dy = 0;
			int consoleWidthLimit = 79;
			int consoleHeightLimit = 24;

			// Creating space
			string space = "   ";

			//Position of food
			Random rand = new Random();
			char food = '@';
			int foodx = 0, foody = 0;
			foodx = rand.Next(2, consoleWidthLimit);
			foody = rand.Next(2, consoleHeightLimit);

			//Position of Obstacles
			string obstacle1 = "||";
			int ob1x = 0, ob1y = 0;
			ob1x = rand.Next(0, consoleWidthLimit);
			ob1y = rand.Next(0, consoleHeightLimit);

			string obstacle2 = "||";
			int ob2x = 0, ob2y = 0;
			ob2x = rand.Next(0, consoleWidthLimit);
			ob2y = rand.Next(0, consoleHeightLimit);

			// clear to color
			Console.BackgroundColor = ConsoleColor.DarkGray;
			Console.Clear();

			// delay to slow down the character movement so you can see it
			int delayInMillisecs = 50;

			//whether to keep trails
			bool trail = false;


			do // until escape
			{
				// print directions at top, then restore position
				// save then restore current color
				ConsoleColor cc = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.SetCursorPosition(0, 0);
				Console.WriteLine("Arrows move up/down/right/left. Press 'esc' quit.");
				Console.SetCursorPosition(x, y);
				Console.ForegroundColor = cc;

				// see if a key has been pressed
				if (Console.KeyAvailable)
				{
					// get key and use it to set options
					consoleKey = Console.ReadKey(true);
					switch (consoleKey.Key)
					{

						case ConsoleKey.UpArrow: //UP
							dx = 0;
							dy = -1;
							Console.ForegroundColor = ConsoleColor.Red;
							break;
						case ConsoleKey.DownArrow: // DOWN
							dx = 0;
							dy = 1;
							Console.ForegroundColor = ConsoleColor.Cyan;
							break;
						case ConsoleKey.LeftArrow: //LEFT
							dx = -1;
							dy = 0;
							Console.ForegroundColor = ConsoleColor.Green;
							break;
						case ConsoleKey.RightArrow: //RIGHT
							dx = 1;
							dy = 0;
							Console.ForegroundColor = ConsoleColor.Black;
							break;
						case ConsoleKey.Escape: //END
							gameLive = false;
							break;
					}
				}


				Console.SetCursorPosition(ob1x, ob1y);
				Console.Write(obstacle1);

				Console.SetCursorPosition(ob2x, ob2y);
				Console.Write(obstacle2);

				//Loop the food
				if (x == foodx && y == foody)
				{
					foodx = rand.Next(2, consoleWidthLimit);
					foody = rand.Next(2, consoleHeightLimit);
					space += " ";
					ch += "*";
				}
				Console.SetCursorPosition(foodx, foody);
				Console.Write(food);


				// find the current position in the console grid & erase the character there if don't want to see the trail
				Console.SetCursorPosition(x, y);
				if (trail == false)
					Console.Write(space);

				// calculate the new position
				// note x set to 0 because we use the whole width, but y set to 1 because we use top row for instructions
				x += dx;
				if (x > consoleWidthLimit)
					x = 0;
				if (x < 0)
					x = consoleWidthLimit;

				y += dy;
				if (y > consoleHeightLimit)
					y = 2; // 2 due to top spaces used for directions
				if (y < 2)
					y = consoleHeightLimit;

				// write the character in the new position
				Console.SetCursorPosition(x, y);
				Console.Write(ch);

				// pause to allow eyeballs to keep up
				System.Threading.Thread.Sleep(delayInMillisecs);

			} while (gameLive);
		}
	}


}
