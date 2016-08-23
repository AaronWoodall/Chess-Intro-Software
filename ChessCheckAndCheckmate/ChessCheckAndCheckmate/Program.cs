using Chess.Enums;
using Chess_Piece_Movement.Models;
using ChessCheckAndCheckmate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChessCheckAndCheckmate
{
    public class Program
    {

        /// <summary>
        /// Controls the current turn by changing to the opposing color
        /// </summary>
        private PieceColor currentTurn = PieceColor.White;
        /// <summary>
        /// The board used during the game
        /// </summary>
        private static Board chessBoard;

        static void Main(string[] args)
        {
            Run(args[0]);
        }

        /// <summary>
        /// The main method that contols/runs the program.
        /// Takes in the file path to the moves text file.
        /// </summary>
        /// <param name="filePath">
        /// The path to the moves file needed to make moves.
        /// </param>
        private static void Run(string filePath)
        {
            Program pro = new Program();
            chessBoard = new Board();

            //A file that places the pieces at a starting position
            string boardSetupFile = "../../TestFiles/SetupFile.txt";
            pro.ReadMoveFlie(pro, boardSetupFile);
            chessBoard.PrintBoard();

            //Reads the file containing the moves based on the Setup File
            pro.ReadMoveFlie(pro, filePath);

            //Console.WriteLine("Press any key to continue . . . ");
            //Console.ReadKey();
        }

        /// <summary>
        /// Converts chess move convention to ints that can be used in a 2D array.
        /// </summary>
        /// <param name="firstChar">The first char that is in the chess move.</param>
        /// <param name="secondChar">The second char that is in the chess move.</param>
        /// <returns>Returns a int array containing the </returns>
        private int[] MoveToLocation(char firstChar, char secondChar)
        {
            char oRow = firstChar;
            string oCol = secondChar + "";
            int orginalLocCol = 0;
            int orginalLocRow = CharToInt(oRow);
            int.TryParse(oCol, out orginalLocCol);

            int[] locations = { orginalLocCol - 1, orginalLocRow };

            return locations;
        }

        /// <summary>
        /// Converts Chars to ints
        /// </summary>
        /// <param name="letter">Char that is meant to be converted</param>
        /// <returns>Int based on the given char</returns>
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

        /// <summary>
        /// Using Regex it takes in a file and converts the matches into moves based on set patterns
        /// Depending on the match it will do a Move, Double move, Placement, or Capture
        /// </summary>
        /// <param name="pro">Program that the run method is using so they share the same instance</param>
        /// <param name="filePath">The path to the text file of moves that are ment to be preformed</param>
        private void ReadMoveFlie(Program pro, string filePath)
        {
            string[] fileLines = System.IO.File.ReadAllLines(filePath);
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

                    int[] firstPStartingPos = MoveToLocation(locations[0][0], locations[0][1]);
                    int[] firstPDestionsation = MoveToLocation(locations[1][0], locations[1][1]);
                    int[] secondPStartingPos = MoveToLocation(locations[2][0], locations[2][1]);
                    int[] secondPDestination = MoveToLocation(locations[3][0], locations[3][1]);

                    ChessPiece tempPiece = chessBoard.GetPiece(firstPStartingPos[0], firstPStartingPos[1]);
                    chessBoard.SetPiece(firstPStartingPos[0], firstPStartingPos[1], new Pawn(PieceColor.Empty));
                    chessBoard.SetPiece(firstPDestionsation[0], firstPDestionsation[1], tempPiece);

                    Console.WriteLine("Double Move: " + match.Value);
                    chessBoard.PrintBoard();

                    tempPiece = chessBoard.GetPiece(secondPStartingPos[0], secondPStartingPos[1]);
                    chessBoard.SetPiece(secondPStartingPos[0], secondPStartingPos[1], new Pawn(PieceColor.Empty));
                    chessBoard.SetPiece(secondPDestination[0], secondPDestination[1], tempPiece);

                    chessBoard.PrintBoard();

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
                        ChessPiece placePiece = new Pawn(PieceColor.Empty);


                        switch (color)
                        {
                            case 'L':
                                switch (piece)
                                {
                                    case 'K':
                                        placePiece = new King(PieceColor.White);
                                        break;
                                    case 'Q':
                                        placePiece = new Queen(PieceColor.White);
                                        break;
                                    case 'B':
                                        placePiece = new Bishop(PieceColor.White);
                                        break;
                                    case 'N':
                                        placePiece = new Knight(PieceColor.White);
                                        break;
                                    case 'R':
                                        placePiece = new Rook(PieceColor.White);
                                        break;
                                    case 'P':
                                        placePiece = new Pawn(PieceColor.White);
                                        break;
                                }
                                break;
                            case 'D':
                                switch (piece)
                                {
                                    case 'K':
                                        placePiece = new King(PieceColor.Black);
                                        break;
                                    case 'Q':
                                        placePiece = new Queen(PieceColor.Black);
                                        break;
                                    case 'B':
                                        placePiece = new Bishop(PieceColor.Black);
                                        break;
                                    case 'N':
                                        placePiece = new Knight(PieceColor.Black);
                                        break;
                                    case 'R':
                                        placePiece = new Rook(PieceColor.Black);
                                        break;
                                    case 'P':
                                        placePiece = new Pawn(PieceColor.Black);
                                        break;
                                }
                                break;
                        }

                        int[] locations = MoveToLocation(col, row);

                        chessBoard.SetPiece(locations[0], locations[1], placePiece);
                        chessBoard.UpdatePieceMoves();


                    }
                    else
                    {
                        match = Regex.Match(line, capturePattern, RegexOptions.IgnoreCase);

                        if (match.Success)
                        {
                            string[] locations = match.Value.Split(' ');

                            int[] firstPStartingPos = MoveToLocation(locations[0][0], locations[0][1]);
                            int[] firstDestination = MoveToLocation(locations[1][0], locations[1][1]);

                            chessBoard.SetPiece(firstDestination[0], firstDestination[1], chessBoard.GetPiece(firstPStartingPos[0], firstPStartingPos[1]));
                            chessBoard.SetPiece(firstPStartingPos[0], firstPStartingPos[1], new Pawn(PieceColor.Empty));

                            Console.WriteLine("Cap: " + match.Value);
                            chessBoard.PrintBoard();
                        }
                        else
                        {
                            match = Regex.Match(line, movePattern, RegexOptions.IgnoreCase);

                            if (match.Success)
                            {
                                string[] locations = match.Value.Split(' ');

                                int[] firstPStartingPos = MoveToLocation(locations[0][0], locations[0][1]);
                                int[] firstDestination = MoveToLocation(locations[1][0], locations[1][1]);

                                if (chessBoard.IsVaildMove(firstPStartingPos[0], firstPStartingPos[1], firstDestination[0], firstDestination[1], currentTurn))
                                {
                                    chessBoard.SetPiece(firstDestination[0], firstDestination[1], chessBoard.GetPiece(firstPStartingPos[0], firstPStartingPos[1]));
                                    chessBoard.SetPiece(firstPStartingPos[0], firstPStartingPos[1], new Pawn(PieceColor.Empty));

                                    Console.WriteLine("Move: " + match.Value);
                                    chessBoard.PrintBoard();
                                    chessBoard.UpdatePieceMoves();
                                    if (currentTurn == PieceColor.White)
                                    {
                                        currentTurn = PieceColor.Black;
                                    }
                                    else
                                    {
                                        currentTurn = PieceColor.White;
                                    }

                                    bool isInCheck = chessBoard.CheckColorKing(currentTurn);
                                    bool isInCheckmate = chessBoard.IsKingInCheckMate(currentTurn);
                                    if (isInCheck)
                                    {
                                        Console.WriteLine(currentTurn + "King is in check");
                                    }
                                    if (isInCheckmate)
                                    {
                                        Console.WriteLine(currentTurn + "King is in checkmate. Gameover");
                                        return;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Move: " + match.Value + " was not a vaild move");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
