using System;
using System.IO;

namespace SnakeGame
{
	class Program
	{
		static void Main(string[] args)
		{
			int selection;
			do
			{
				//Main Menu
				Console.Clear();
				Console.WriteLine("--Main Menu--");
				Console.WriteLine("1. Start ");
				Console.WriteLine("2. View Score");
				Console.WriteLine("3. Help Page");
				Console.WriteLine("4. Exit");
				Console.WriteLine("Choose one to proceed...");
				Console.Write("-->");
				selection = int.Parse(Console.ReadLine());

				if (selection == 1)
				{

					// Difficulty and life system
					string difficulty = " ";
					int life = 5;

					// You need to pick a difficulty
					do
					{
						Console.WriteLine("\nStage Mode- easy,normal,hard");
						Console.Write("Select your difficulty : ");
						difficulty = Console.ReadLine();
					} while (difficulty != "easy" && difficulty != "normal" && difficulty != "hard");

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
					int score = 0;

					// Creating space
					string space = "   ";


					//Position of food
					Random rand = new Random();
					char food = '@';
					int foodx = 0, foody = 0;
					foodx = rand.Next(2, consoleWidthLimit);
					foody = rand.Next(2, consoleHeightLimit);

					//Special food
					char s_food = '+';
					int s_foodx = 0, s_foody = 0;
					s_foodx = rand.Next(2, consoleWidthLimit);
					s_foody = rand.Next(2, consoleHeightLimit);

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
					Console.BackgroundColor = ConsoleColor.DarkCyan;
					Console.Clear();

					// delay to slow down the character movement so you can see it
					int delayInMillisecs = 50;

					// count steps for food each time the snake moves
					int stepFood = 0;
					// count steps for bounty food each time the snake moves
					int stepBonusFood = 0;
					int foodInterval = 100;

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
						Console.WriteLine("Obtain score 50 to win the game...");
						Console.WriteLine("Score: " + score);
						Console.WriteLine("Life: " + life);
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


						//Snake eat food
						if ((x == foodx && y == foody) || stepFood > foodInterval)
						{
							//Increase score when snake eat the food
							if (x == foodx && y == foody)
							{
								score += 5;
								space += " ";
								ch += "*";
							}
							//Remove the food
							Console.SetCursorPosition(foodx, foody);
							Console.Write(' ');
							//New food
							foodx = rand.Next(2, consoleWidthLimit - 1);
							foody = rand.Next(2, consoleHeightLimit - 1);
							stepFood = 0;
						}
						++stepFood;

						Console.SetCursorPosition(foodx, foody);
						Console.Write(food);


						//Snake eat bonus food
						if (x == s_foodx && y == s_foody || stepBonusFood > foodInterval)
						{
							//Increase score when snake eat the food
							if (x == s_foodx && y == s_foody)
							{
								score += 10;
							}
							//Remove the bonus food
							Console.SetCursorPosition(s_foodx, s_foody);
							Console.Write(' ');
							//New bonus food
							s_foodx = rand.Next(2, consoleWidthLimit - 1);
							s_foody = rand.Next(2, consoleHeightLimit - 1);
							stepBonusFood = 0;
						}
						++stepBonusFood;

						Console.SetCursorPosition(s_foodx, s_foody);
						Console.Write(s_food);


						// If hit obstacle, decrease life
						if (x == ob1x && y == ob1y)
						{
							if (difficulty == "easy")
							{
								life--;
							}
							if (difficulty == "normal")
							{
								life -= 2;
							}
							if (difficulty == "hard")
							{
								life -= 4;
							}
							ob1x = rand.Next(0, consoleWidthLimit - 1);
							ob1y = rand.Next(0, consoleHeightLimit - 1);
						}

						if (x == ob2x && y == ob2y)
						{
							if (difficulty == "easy")
							{
								life--;
							}
							if (difficulty == "normal")
							{
								life -= 2;
							}
							if (difficulty == "hard")
							{
								life -= 4;
							}
							ob2x = rand.Next(0, consoleWidthLimit - 1);
							ob2y = rand.Next(0, consoleHeightLimit - 1);
						}

						// End the game if snake's life hit 0
						if (life <= 0)
						{
							gameLive = false;
							break;
						}

						//Set winning condition
						if (score == 50)
						{
							Console.Clear();
							Console.WriteLine("Congratulations You win!!!");
							Console.WriteLine("Press any key to proceed!");
							Console.ReadKey();
							gameLive = false;
							break;
						}

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

					if (gameLive == false)
					{
						Console.Write("Input your name : ");
						string name = Console.ReadLine();
						StreamWriter sw = File.AppendText("Score.txt");
						sw.WriteLine(name + "\t" + score);
						sw.Close();
					}
				}
				else if (selection == 2)
				{
					//View Score
					Console.Clear();
					Console.WriteLine("---Score shown below---");
					using (StreamReader file = new StreamReader("Score.txt"))
					{
						string line;
						while ((line = file.ReadLine()) != null)
						{
							Console.WriteLine(line);
						}
					}
					Console.WriteLine("\n---End---");
					Console.WriteLine("Press any key back to main menu");
					Console.ReadKey();
				}
				else if (selection == 3)
				{
					Console.Clear();
					Console.WriteLine("~~~Help Page~~~");
					Console.WriteLine("1. Movement of Snake");
					Console.WriteLine("- Up/Down/Right/Left buttons respectively control the movement of the snake");
					Console.WriteLine("\n2. Goal of the Game");
					Console.WriteLine("- Obtain 50 score to win the game");
					Console.WriteLine("\n3. Special Food");
					Console.WriteLine("- Special food + to earn bonus score");
					Console.WriteLine("\n4. Obstacles");
					Console.WriteLine("- Hit the obstacles(||) will decrease the player's life");
					Console.WriteLine("\n5. Food");
					Console.WriteLine("- Eat food(@) to earn score");
					Console.WriteLine("\n~~~END~~~");
					Console.WriteLine("Press any key to go back to main menu");
					Console.ReadKey();

				}
				else if (selection == 4)
				{
					//Exit
					Console.WriteLine("Program terminated");
					Console.WriteLine("Press any key again to exit...");
					Console.ReadKey();

				}
			} while (selection != 4);
		}
	}


}
