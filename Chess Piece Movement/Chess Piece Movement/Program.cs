using Chess_Piece_Movement.Enums;
using Chess_Piece_Movement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chess_Piece_Movement
{
    class Program
    {
        private List<List<ChessPiece>> chessBoard = new List<List<ChessPiece>>();
        private PieceColor currentTurn = PieceColor.White;


        static void Main(string[] args)
        {
            Run(args[0]);
        }

        private static void Run(string filePath)
        {
            Program pro = new Program();

            pro.BoardSetUp();
            string boardSetupFile = "C:\\Users\\Aaron Woodall\\Desktop\\Intro Software\\Chessboard Setup.txt";
            pro.ReadMoveFlie(pro, boardSetupFile);
            pro.PrintBoard();

            pro.ReadMoveFlie(pro, filePath);

            //Console.WriteLine("Press any key to continue . . . ");
            //Console.ReadKey();
        }

        private void PrintBoard()
        {
            for (int i = 7; i > -1; i--)
            {
                foreach (ChessPiece p in chessBoard[i])
                {
                    Console.Write(p.pieceSymbol);
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        private void BoardSetUp()
        {
            for (int k = 0; k < 8; k++)
            {
                List<ChessPiece> Row = new List<ChessPiece>
                {
                    new Pawn(PieceColor.Empty),
                    new Pawn(PieceColor.Empty),
                    new Pawn(PieceColor.Empty),
                    new Pawn(PieceColor.Empty),
                    new Pawn(PieceColor.Empty),
                    new Pawn(PieceColor.Empty),
                    new Pawn(PieceColor.Empty),
                    new Pawn(PieceColor.Empty)
                };
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

        private List<int[]> GetPieceMoves(ChessPiece firstPiece, int oCol, int oRow)
        {
            List<int[]> possibleMoves = new List<int[]>();

            switch (firstPiece.Name)
            {
                case Pieces.Rook:
                    possibleMoves = ((Rook)firstPiece).GetMoves(oCol, oRow);
                    break;
                case Pieces.Bishop:
                    possibleMoves = ((Bishop)firstPiece).GetMoves(oCol, oRow);
                    break;
                case Pieces.Knight:
                    possibleMoves = ((Knight)firstPiece).GetMoves(oCol, oRow);
                    break;
                case Pieces.Queen:
                    possibleMoves = ((Queen)firstPiece).GetMoves(oCol, oRow);
                    break;
                case Pieces.King:
                    possibleMoves = ((King)firstPiece).GetMoves(oCol, oRow);
                    break;
                case Pieces.Pawn:
                    possibleMoves = ((Pawn)firstPiece).GetMoves(oCol, oRow);
                    break;
            }

            return possibleMoves;
        }

        private bool IsVaildMove(int oCol, int oRow, int nCol, int nRow)
        {
            bool isVaild = false;
            ChessPiece firstPick = chessBoard[oCol][oRow];
            ChessPiece secondPick = chessBoard[nCol][nRow];
            List<int[]> possibleMoves = GetPieceMoves(firstPick, oCol, oRow);

            if (oCol == nCol && nRow == oRow)
            {
                return isVaild;
            }
            else
            {
                if (firstPick.Color == currentTurn)
                {
                    foreach (int[] move in possibleMoves)
                    {
                        if (move[0] == nCol && move[1] == nRow)
                        {
                            switch (firstPick.Name)
                            {
                                case Pieces.Empty:
                                    return isVaild;
                                case Pieces.Pawn:
                                    {
                                        if (secondPick.Name != Pieces.Empty)
                                        {
                                            return isVaild;
                                        }
                                        else
                                        {
                                            isVaild = true;
                                        }
                                    }
                                    break;
                                case Pieces.Knight:
                                    isVaild = true;
                                    break;
                                case Pieces.King:
                                    isVaild = true;
                                    break;
                                case Pieces.Rook:
                                    isVaild = CheckRookPath(oCol, oRow, nCol, nRow, possibleMoves);
                                    break;
                                case Pieces.Bishop:
                                    isVaild = CheckBishopPath(oCol, oRow, nCol, nRow, possibleMoves);
                                    break;
                                case Pieces.Queen:
                                    isVaild = CheckBishopPath(oCol, oRow, nCol, nRow, possibleMoves);
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }

            }
            return isVaild;
        }

        private bool CheckRookPath(int oCol, int oRow, int nCol, int nRow, List<int[]> possibleMoves)
        {

            List<int[]> temp = new List<int[]>();
            int distance;

            if (oCol == nCol && oRow == nRow)
            {
                return false;
            }

            foreach (int[] move in possibleMoves)
            {
                if (oCol == nCol)
                {
                    distance = nRow - oRow;

                    if (distance < 0)
                    {
                        if (move[1] < oRow && move[1] > nRow)
                        {
                            if (chessBoard[oCol][move[1]].Name != Pieces.Empty)
                            {
                                temp.Add(move);
                            }
                        }
                    }
                    else if (distance > 0)
                    {

                        if (move[1] > oRow && move[1] < nRow)
                        {
                            if (chessBoard[oCol][move[1]].Name != Pieces.Empty)
                            {
                                temp.Add(move);
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (oRow == nRow)
                {
                    distance = nCol - oCol;

                    if (distance < 0)
                    {
                        if (move[0] < oCol && move[0] > nCol)
                        {
                            if (chessBoard[move[0]][oRow].Name != Pieces.Empty)
                            {
                                temp.Add(move);
                            }
                        }
                    }
                    else if (distance > 0)
                    {
                        if (move[0] > oCol && move[0] < nCol)
                        {
                            if (chessBoard[move[0]][oRow].Name != Pieces.Empty)
                            {
                                temp.Add(move);
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            if (temp.Count > 1)
            {
                return false;
            }
            else if (temp.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckBishopPath(int oCol, int oRow, int nCol, int nRow, List<int[]> possibleMoves)
        {
            int cDistance = nCol - oCol;
            int rDistance = nRow - oRow;
            List<int[]> temp = new List<int[]>();

            //C R
            //- -
            foreach (int[] move in possibleMoves)
            {
                if (cDistance > 0 && rDistance > 0)
                {
                    if (move[0] < oCol && move[0] > nCol)
                    {
                        if (move[1] < oRow && move[1] > oCol)
                        {
                            if (chessBoard[move[0]][move[1]].Name != Pieces.Empty)
                            {
                                temp.Add(move);
                            }
                        }
                    }
                }
                //+ +
                else if (cDistance < 0 && rDistance < 0)
                {
                    if (move[0] > oCol && move[0] < nCol)
                    {
                        if (move[1] > oRow && move[1] < oCol)
                        {
                            if (chessBoard[move[0]][move[1]].Name != Pieces.Empty)
                            {
                                temp.Add(move);
                            }
                        }
                    }
                }
                //- +
                else if (cDistance > 0 && rDistance < 0)
                {
                    if (move[0] < oCol && move[0] > nCol)
                    {
                        if (move[1] > oRow && move[1] < oCol)
                        {
                            if (chessBoard[move[0]][move[1]].Name != Pieces.Empty)
                            {
                                temp.Add(move);
                            }
                        }
                    }
                }
                //+ -
                else if (cDistance < 0 && rDistance > 0)
                {
                    if (move[0] > oCol && move[0] < nCol)
                    {
                        if (move[1] < oRow && move[1] > oCol)
                        {
                            if (chessBoard[move[0]][move[1]].Name != Pieces.Empty)
                            {
                                temp.Add(move);
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
            }

            if (temp.Count > 1)
            {
                return false;
            }
            else if (temp.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool CheckQueenPath(int oCol, int oRow, int nCol, int nRow, List<int[]> possibleMoves)
        {
            if (oCol == nCol)
            {
                return CheckRookPath(oCol, oRow, nCol, nRow, possibleMoves);
            }
            else if (oRow == nRow)
            {
                return CheckRookPath(oCol, oRow, nCol, nRow, possibleMoves);
            }
            else
            {
                return CheckBishopPath(oCol, oRow, nCol, nRow, possibleMoves);
            }
        }

        private void ReadMoveFlie(Program pro, string filePath)
        {
            string[] fileLines = System.IO.File.ReadAllLines(filePath);
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

                    int[] firstPieceMove = MoveToLocation(locations[0][0], locations[0][1], locations[1][0], locations[1][1]);
                    int[] secondPieceMove = MoveToLocation(locations[2][0], locations[2][1], locations[3][0], locations[3][1]);

                    ChessPiece tempPiece = chessBoard[(firstPieceMove[0])][(firstPieceMove[1])];
                    chessBoard[(firstPieceMove[0])][(firstPieceMove[1])] = new Pawn(PieceColor.Empty);
                    chessBoard[(firstPieceMove[2])][(firstPieceMove[3])] = tempPiece;

                    Console.WriteLine("Double Move: " + match.Value);
                    pro.PrintBoard();

                    tempPiece = chessBoard[(secondPieceMove[0])][(secondPieceMove[1])];
                    chessBoard[(secondPieceMove[0])][(secondPieceMove[1])] = new Pawn(PieceColor.Empty);
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


                        //A8 Places in row 7 at 0
                        //D3 Places in row 3 at 3
                        //H6 Places in row 7 at 6

                        int[] locations = MoveToLocation(col, row);

                        (chessBoard[locations[0]])[locations[1]] = placePiece;


                    }
                    else
                    {
                        match = Regex.Match(line, capturePattern, RegexOptions.IgnoreCase);

                        if (match.Success)
                        {
                            string[] locations = match.Value.Split(' ');

                            int[] firstPieceMove = MoveToLocation(locations[0][0], locations[0][1], locations[1][0], locations[1][1]);

                            chessBoard[(firstPieceMove[2])][(firstPieceMove[3])] = chessBoard[(firstPieceMove[0])][(firstPieceMove[1])];
                            chessBoard[(firstPieceMove[0])][(firstPieceMove[1])] = new Pawn(PieceColor.Empty);

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

                                if (IsVaildMove(firstPieceMove[0], firstPieceMove[1], firstPieceMove[2], firstPieceMove[3]))
                                {
                                    chessBoard[(firstPieceMove[2])][(firstPieceMove[3])] = chessBoard[(firstPieceMove[0])][(firstPieceMove[1])];
                                    chessBoard[(firstPieceMove[0])][(firstPieceMove[1])] = new Pawn(PieceColor.Empty);

                                    Console.WriteLine("Move: " + match.Value);
                                    pro.PrintBoard();
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

