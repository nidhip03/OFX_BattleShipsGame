using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFX_BattleShipsGame.StateTrackerAPI;
using OFX_BattleShipsGame.StateTrackerAPI.Enums;
using OFX_BattleShipsGame.StateTrackerAPI.Classes;

namespace OFX_BattleShipsGame.App
{
    public class Game
    {
        readonly PlayerProfile playerprofile;
        public Game()
        {
            playerprofile = new PlayerProfile() { IsPlayer1 = false, Player1 = new Player(), Player2 = new Player() };
        }

        public void Start()
        {
            Setups gameSetups = new Setups(playerprofile);
            gameSetups.Setup();

            //Board Set - For Player1
            gameSetups.SetBoard();

            Board b = new Board();
            b.DrawBoards(playerprofile.IsPlayer1 ? playerprofile.Player2 : playerprofile.Player1);

            //To Hit or Miss the target
            FireShotResponse shotresponse;
            shotresponse = Shot(playerprofile.IsPlayer1 ? playerprofile.Player2 : playerprofile.Player1, playerprofile.IsPlayer1 ? playerprofile.Player1 : playerprofile.Player2, out Coordinates ShotPoint);
            ShowShotResult(shotresponse, ShotPoint, playerprofile.IsPlayer1 ? playerprofile.Player1.Name : playerprofile.Player2.Name);

            //FireShotResponse shotresponse;
            //_ = new Coordinates(1, 1);
            //shotresponse = Shot(playerprofile.IsPlayer1 ? playerprofile.Player2 : playerprofile.Player1, playerprofile.IsPlayer1 ? playerprofile.Player1 : playerprofile.Player2, out Coordinates ShotPoint);
            //ShowShotResult(shotresponse, ShotPoint, playerprofile.IsPlayer1 ? playerprofile.Player1.Name : playerprofile.Player2.Name);
        }

        private FireShotResponse Shot(Player victim, Player Shooter, out Coordinates ShotPoint)
        {
            
            FireShotResponse fire; Coordinates WhereToShot;
            WhereToShot = Inputs.GetShotLocationFromUser();
            fire = victim.PlayerBoard.FireShot(WhereToShot);

            //do
            //{
            //    if (!Shoter.IsPC)
            //    {
            //        WhereToShot = Inputs.GetShotLocationFromUser();
            //        fire = victim.PlayerBoard.FireShot(WhereToShot);
            //        if (fire.ShotStatus == ShotStatus.Invalid || fire.ShotStatus == ShotStatus.Duplicate)
            //            ShowShotResult(fire, WhereToShot, "");
            //    }
            //    else
            //    {
            //        WhereToShot = Inputs.GetShotLocationFromComputer(victim.PlayerBoard);
            //        fire = victim.PlayerBoard.FireShot(WhereToShot);
            //    }
            //} while (fire.ShotStatus == ShotStatus.Duplicate || fire.ShotStatus == ShotStatus.Invalid);


            ShotPoint = WhereToShot;
            return fire;
        }


        public static void ShowShotResult(FireShotResponse shotresponse, Coordinates c, string playername)
        {
            String str = "";
            switch (shotresponse.ShotStatus)
            {
                case ShotStatus.Duplicate:
                    Console.ForegroundColor = ConsoleColor.Red;
                    str = "Shot location: " + Board.GetLetterFromNumber(c.XCoordinate) + c.YCoordinate.ToString() + "\t result: Duplicate shot location!";
                    break;
                case ShotStatus.Hit:
                    Console.ForegroundColor = ConsoleColor.Green;
                    str = "Shot location: " + Board.GetLetterFromNumber(c.XCoordinate) + c.YCoordinate.ToString() + "\t result: Hit!";
                    break;
                case ShotStatus.Invalid:
                    Console.ForegroundColor = ConsoleColor.Red;
                    str = "Shot location: " + Board.GetLetterFromNumber(c.XCoordinate) + c.YCoordinate.ToString() + "\t result: Invalid hit location!";
                    break;
                case ShotStatus.Miss:
                    Console.ForegroundColor = ConsoleColor.White;
                    str = "Shot location: " + Board.GetLetterFromNumber(c.XCoordinate) + c.YCoordinate.ToString() + "\t result: Miss!";
                    break;
            }
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
        }
    }
}
