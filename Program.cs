using System;
using System.Linq;
using static System.Console;

namespace BattleShip_CSharp
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            char menuSelection; 


          
            do
            {
                string lastShot = "";
                int turnsRemaining = 8;
                int hitShip = 0;
                GameBoard board = new();


                board.DisplayStartMessage();

                do
                {
                    board.StartGame(lastShot, turnsRemaining);
               
                    string playerGuess = ReadLine();


                    while (board.IsInputInvalid(playerGuess))
                    {
                       board.InvalidInputMessage();
                        
                        playerGuess = ReadLine();
                    };

                    PlayerShot shell = new(playerGuess);

                    if (shell.DidRepeatShot(board.board))
                    {
                        lastShot = "RepeatShot";
                    }
                    else
                    {
                        if (shell.DidShotHitShip(board.ship, board.board))
                        {
                            hitShip++;
                            lastShot = "ShotHit";
                            board.MarkHit(shell.location);
                        }
                        else
                        {
                            lastShot = "ShotMissed";
                            board.MarkMiss(shell.location);
                        }
                    }
                    turnsRemaining--;
                } while (turnsRemaining > 0 && hitShip != 5);

                if (hitShip == 5)
                {
                    board.DisplayWinMessage();
                }
                else
                {
                    board.DisplayLoseMessage();
                }

                WriteLine();
                WriteLine("Press any key to return to the main menu or enter 'q' to quit");
                menuSelection = (char)Read();

            } while (menuSelection != 'q');
        }

       
    }
}