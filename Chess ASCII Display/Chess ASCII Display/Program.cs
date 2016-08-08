using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chess_ASCII_Display
{
    public class Program
    {
        private List<List<string>> chessBoard = new List<List<string>>();

        static void Main(string[] args)
        {
            Run(args[0]);
        }



        private static void Run(string filePath)
        {
            Program pro = new Program();
            pro.BoardSetUp();
            pro.PrintBoard();
            pro.ReadMoveFlie(pro, filePath);

            //Console.WriteLine("Press any key to continue . . . ");
            //Console.ReadKey();
        }

        private void PrintBoard()
        {
            for (int i = 7; i > -1; i--)
            {
                foreach (string p in chessBoard[i])
                {
                    Console.Write(p);
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        private void BoardSetUp()
        {
            for (int k = 0; k < 8; k++)
            {
                List<string> Row = new List<string> { " - ", " - ", " - ", " - ", " - ", " - ", " - ", " - " };
                chessBoard.Add(Row);
            }


        }

        private int[] MoveToLocation(char orginalCol, char orginalRow, char newCol, char newRow)
        {
            char oCol = orginalCol;
            char nCol = newCol;
            string oRow = orginalRow + "";
            string nRow = newRow + "";
            int orginalLocCol = CharToInt(oCol);
            int newLocCol = CharToInt(nCol);
            int orginalLocRow = 0;
            int newLocRow = 0;
            int.TryParse(oRow, out orginalLocRow);
            int.TryParse(nRow, out newLocRow);

            int[] locations = { orginalLocRow - 1, orginalLocCol, newLocRow - 1, newLocCol };

            return locations;
        }

        private int[] MoveToLocation(char orginalCol, char orginalRow)
        {
            char oCol = orginalCol;
            string oRow = orginalRow + "";
            int orginalLocCol = CharToInt(oCol);
            int orginalLocRow = 0;
            int.TryParse(oRow, out orginalLocRow);

            int[] locations = { orginalLocRow - 1, orginalLocCol };

            return locations;
        }


        private int CharToInt(char letter)
        {
            int value = 0;
            string sLetter = letter + "";
            switch (sLetter.ToUpper())
            {
                case "A":
                    value = 0;
                    break;
                case "B":
                    value = 1;
                    break;
                case "C":
                    value = 2;
                    break;
                case "D":
                    value = 3;
                    break;
                case "E":
                    value = 4;
                    break;
                case "F":
                    value = 5;
                    break;
                case "G":
                    value = 6;
                    break;
                case "H":
                    value = 7;
                    break;
            }

            return value;
        }

        private void ReadMoveFlie(Program pro, string filePath)
        {



            string[] fileLines = File.ReadAllLines(filePath);
            string movePattern = @"([A-H][1-8]\s[A-H][1-8])";
            string placePattern = @"([KQBNRP][LD][A-H][1-8])";
            string capturePattern = @"([A-H][1-8]\s[A-H][1-8][*])";
            string doubleMovepattern = @"([A-H][1-8]\s[A-H][1-8]\s[A-H][1-8]\s[A-H][1-8])";




            foreach (string line in fileLines)
            {

                Match match = Regex.Match(line, doubleMovepattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    string[] locations = match.Value.Split(' ');

                    int[] firstPieceMove = MoveToLocation(locations[0][0], locations[0][1], locations[1][0], locations[1][1]);
                    int[] secondPieceMove = MoveToLocation(locations[2][0], locations[2][1], locations[3][0], locations[3][1]);

                    string tempPiece = chessBoard[(firstPieceMove[0])][(firstPieceMove[1])];
                    chessBoard[(firstPieceMove[0])][(firstPieceMove[1])] = " - ";
                    chessBoard[(firstPieceMove[2])][(firstPieceMove[3])] = tempPiece;

                    Console.WriteLine("Double Move: " + match.Value);
                    pro.PrintBoard();

                    tempPiece = chessBoard[(secondPieceMove[0])][(secondPieceMove[1])];
                    chessBoard[(secondPieceMove[0])][(secondPieceMove[1])] = " - ";
                    chessBoard[(secondPieceMove[2])][(secondPieceMove[3])] = tempPiece;

                    pro.PrintBoard();

                }
                else
                {
                    match = Regex.Match(line, placePattern, RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        char piece = (line.ToUpper())[0];
                        char color = (line.ToUpper())[1];
                        char row = line[3];
                        char col = line[2];
                        string placePiece = "[Programmer Error]";

                        switch (piece)
                        {
                            case 'K':
                                placePiece = " K ";
                                break;
                            case 'Q':
                                placePiece = " Q ";
                                break;
                            case 'B':
                                placePiece = " B ";
                                break;
                            case 'N':
                                placePiece = " N ";
                                break;
                            case 'R':
                                placePiece = " R ";
                                break;
                            case 'P':
                                placePiece = " P ";
                                break;
                        }
                        switch (color)
                        {
                            case 'L':
                                placePiece = placePiece.ToLower();
                                break;
                            case 'D':
                                placePiece = placePiece.ToUpper();
                                break;
                        }


                        //A8 Places in row 7 at 0
                        //D3 Places in row 3 at 3
                        //H6 Places in row 7 at 6

                        int[] locations = MoveToLocation(col, row);

                        (chessBoard[locations[0]])[locations[1]] = placePiece;

                        Console.WriteLine("Place: " + match.Value);
                        pro.PrintBoard();

                    }
                    else
                    {
                        match = Regex.Match(line, capturePattern, RegexOptions.IgnoreCase);

                        if (match.Success)
                        {
                            string[] locations = match.Value.Split(' ');

                            int[] firstPieceMove = MoveToLocation(locations[0][0], locations[0][1], locations[1][0], locations[1][1]);

                            chessBoard[(firstPieceMove[2])][(firstPieceMove[3])] = chessBoard[(firstPieceMove[0])][(firstPieceMove[1])];
                            chessBoard[(firstPieceMove[0])][(firstPieceMove[1])] = " - ";

                            Console.WriteLine("Cap: " + match.Value);
                            pro.PrintBoard();
                        }
                        else
                        {
                            match = Regex.Match(line, movePattern, RegexOptions.IgnoreCase);

                            if (match.Success)
                            {
                                string[] locations = match.Value.Split(' ');

                                int[] firstPieceMove = MoveToLocation(locations[0][0], locations[0][1], locations[1][0], (locations[1])[1]);

                                chessBoard[(firstPieceMove[2])][(firstPieceMove[3])] = chessBoard[(firstPieceMove[0])][(firstPieceMove[1])];
                                chessBoard[(firstPieceMove[0])][(firstPieceMove[1])] = " - ";


                                Console.WriteLine("Move: " + match.Value);
                                pro.PrintBoard();
                            }
                        }
                    }
                }
            }
        }
    }
}
