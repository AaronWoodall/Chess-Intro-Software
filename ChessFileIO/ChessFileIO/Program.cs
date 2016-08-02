using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ChessFileIO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Program program = new Program();
            program.ReadFile();
        }


        private void ReadFile()
        {
            Console.Write("Please enter path to chess moves:");

            string fileName = Console.ReadLine();

            string[] fileLines = System.IO.File.ReadAllLines(fileName);
            string movePattern = @"([A-H][1-8]\s[A-H][1-8])";
            string placePattern = @"([KQBNRP][LD][A-H][1-8])";
            string capturePattern = @"([A-H][1-8]\s[A-H][1-8][*])";
            string doubleMovepattern = @"([A-H][1-8]\s[A-H][1-8]\s[A-H][1-8]\s[A-H][1-8])";

            //C:\Users\Aaron Woodall\Desktop\Intro Software\ChessMoves.txt


            foreach (string line in fileLines)
            {

                Match match = Regex.Match(line, doubleMovepattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    string[] locations = match.Value.Split(' ');
                    Console.WriteLine("Moves piece at " + locations[0] + " to " + locations[1] + " and moves piece at " + locations[2] + " to " + locations[3]);
                }
                else
                {
                    match = Regex.Match(line, placePattern, RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        char piece = (line.ToUpper())[0];
                        char color = (line.ToUpper())[1];
                        string placeLoc = (line.ToUpper())[2] + "" + (line.ToUpper())[3];
                        string pieceName = "[PROGRAMMER ERROR]";
                        string pieceColor = "[PROGRAMMER ERROR]";

                        switch (piece)
                        {
                            case 'K':
                                pieceName = "king";
                                break;
                            case 'Q':
                                pieceName = "queen";
                                break;
                            case 'B':
                                pieceName = "bishop";
                                break;
                            case 'N':
                                pieceName = "knight";
                                break;
                            case 'R':
                                pieceName = "rook";
                                break;
                            case 'P':
                                pieceName = "pawn";
                                break;
                        }

                        switch (color)
                        {
                            case 'L':
                                pieceColor = "white";
                                break;
                            case 'D':
                                pieceColor = "black";
                                break;
                        }


                        Console.WriteLine("Places the " + pieceColor + " " + pieceName + " at " + placeLoc);
                    }
                    else
                    {
                        match = Regex.Match(line, capturePattern, RegexOptions.IgnoreCase);

                        if (match.Success)
                        {
                            string[] locations = match.Value.Split(' ');
                            Console.WriteLine("Moves piece at " + locations[0] + " to " + locations[1][0] + locations[1][1] + " and captures piece at " + locations[1][0] + locations[1][1]);
                        }
                        else
                        {
                            match = Regex.Match(line, movePattern, RegexOptions.IgnoreCase);

                            if (match.Success)
                            {
                                string[] locations = match.Value.Split(' ');
                                Console.WriteLine("Moves piece at " + locations[0] + " to " + locations[1]);
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Reading done. Press any key to close window.");
            Console.ReadKey();
        }
    }
}
