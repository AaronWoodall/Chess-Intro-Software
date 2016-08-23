using Chess.Enums;
using Chess_Piece_Movement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCheckAndCheckmate.Models
{
    public class Board
    {
        /// <summary>
        /// Collection of rows of ChessPieces
        /// </summary>
        private List<List<ChessPiece>> chessBoard = new List<List<ChessPiece>>();

        /// <summary>
        /// Constuctor that fills the list with piece place holders.
        /// </summary>
        public Board()
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

        /// <summary>
        /// Sets the piece to the given col and row
        /// </summary>
        /// <param name="col">Column to place at</param>
        /// <param name="row">Row to place at</param>
        /// <param name="tempPiece">The Piece that will be placed</param>
        public void SetPiece(int col, int row, ChessPiece tempPiece)
        {
            chessBoard[col][row] = tempPiece;
        }

        /// <summary>
        /// Gets a piece from the chessBoard list and returns it
        /// </summary>
        /// <param name="Col">The column that the piece is at</param>
        /// <param name="Row">The row that the piece is at</param>
        /// <returns>The piece at the column and row specified</returns>
        public ChessPiece GetPiece(int Col, int Row)
        {
            return chessBoard[Col][Row];
        }

        /// <summary>
        /// Updates the pieces based on their position in the board List
        /// </summary>
        public void UpdatePieceMoves()
        {
            for (int i = 0; i < chessBoard.Count(); i++)
            {
                for (int k = 0; k < chessBoard[i].Count(); k++)
                {
                    ChessPiece currentPiece = chessBoard[i][k];
                    currentPiece.CurrentPos = new int[] { i, k };
                    currentPiece.UpdateMoves();
                }
            }

        }

        /// <summary>
        /// Prints the board out to the console
        /// </summary>
        public void PrintBoard()
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

        /// <summary>
        /// Checks the pieces if the specified move is possible.
        /// </summary>
        /// <param name="oCol">Column of the orignal position</param>
        /// <param name="oRow">Row of the orignal position</param>
        /// <param name="nCol">Column of the new position/destination</param>
        /// <param name="nRow">Row of the new position/destination</param>
        /// <param name="currentTurn">The current turn color used to check for vaild turns</param>
        /// <returns>If the move was vaild return true or false if it wasnt</returns>
        public bool IsVaildMove(int oCol, int oRow, int nCol, int nRow, PieceColor currentTurn)
        {
            bool isVaild = false;
            ChessPiece firstPick = chessBoard[oCol][oRow];
            ChessPiece secondPick = chessBoard[nCol][nRow]; ;



            //Invaildates moving a piece to its same location
            if (oRow == nRow && oCol == nCol)
            {
                return isVaild;
            }
            else
            {
                //Makes sure that the selected piece is owned by the players current turn
                if (firstPick.Color == currentTurn)
                {
                    //Makes sure if a destination does not match the same color as the first piece
                    if (firstPick.Color != secondPick.Color)
                    {
                        foreach (int[] move in firstPick.PossibleMoves)
                        {
                            if (nCol == move[0] && nRow == move[1])
                            {
                                switch (firstPick.Name)
                                {
                                    case Pieces.Empty:
                                        return isVaild;
                                    case Pieces.Pawn:
                                        {
                                            //Keeps pawns from capturing pieces in front of them
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
                                        isVaild = KeepKingOutOfCheck(oCol, oRow, nCol, nRow, firstPick.PossibleMoves, currentTurn);
                                        break;
                                    case Pieces.Rook:
                                        isVaild = CheckRookPath(oCol, oRow, nCol, nRow, firstPick.PossibleMoves);
                                        break;
                                    case Pieces.Bishop:
                                        isVaild = CheckBishopPath(oCol, oRow, nCol, nRow, firstPick.PossibleMoves);
                                        break;
                                    case Pieces.Queen:
                                        isVaild = CheckQueenPath(oCol, oRow, nCol, nRow, firstPick.PossibleMoves);
                                        break;
                                }
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

        /// <summary>
        /// First finds the king of the matching color for the current turn
        /// Then checks any piece can take out the attaking piece
        /// Then checks if the attacking piece can be blocked by another piece
        /// </summary>
        /// <param name="color">The color of the current</param>
        /// <returns>if the king is in checkmate</returns>
        public bool IsKingInCheckMate(PieceColor color)
        {
            King king = null;
            bool kingIsInCheckmate = true;

            foreach (List<ChessPiece> rows in chessBoard)
            {
                foreach (ChessPiece piece in rows)
                {
                    if (piece.Name == Pieces.King)
                    {
                        if (piece.Color == color)
                        {
                            king = (King)piece;
                        }
                    }
                }
            }


            foreach (List<ChessPiece> rows in chessBoard)
            {
                foreach (ChessPiece piece in rows)
                {
                    if (piece.Color == color)
                    {
                        foreach (ChessPiece attackingPiece in king.AttackingPieces)
                        {
                            foreach (int[] moves in piece.PossibleMoves)
                            {
                                if (moves[0] == attackingPiece.CurrentPos[0] && moves[1] == attackingPiece.CurrentPos[1])
                                {
                                    if (piece.Name == Pieces.King)
                                    {
                                        kingIsInCheckmate = !KeepKingOutOfCheck(piece.CurrentPos[0], piece.CurrentPos[1], attackingPiece.CurrentPos[0], attackingPiece.CurrentPos[1], piece.PossibleMoves, color);
                                    }
                                    else
                                    {
                                        kingIsInCheckmate = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }


            foreach (ChessPiece aPiece in king.AttackingPieces)
            {
                switch (aPiece.Name)
                {
                    case Pieces.Bishop:
                        List<int[]> bishopPath = GetBishopPath(aPiece.CurrentPos[0], aPiece.CurrentPos[1], king.CurrentPos[0], king.CurrentPos[1], aPiece.PossibleMoves);
                        foreach (List<ChessPiece> row in chessBoard)
                        {
                            foreach (ChessPiece piece in row)
                            {
                                if (piece.Color != aPiece.Color)
                                {
                                    foreach (int[] bpMove in bishopPath)
                                    {
                                        foreach (int[] pMove in piece.PossibleMoves)
                                        {
                                            if (pMove[0] == bpMove[0] && pMove[1] == bpMove[1])
                                            {
                                                kingIsInCheckmate = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case Pieces.Queen:
                        List<int[]> queenPath = GetQueenPath(aPiece.CurrentPos[0], aPiece.CurrentPos[1], king.CurrentPos[0], king.CurrentPos[1], aPiece.PossibleMoves);
                        foreach (List<ChessPiece> row in chessBoard)
                        {
                            foreach (ChessPiece piece in row)
                            {
                                if (piece.Color != aPiece.Color)
                                {
                                    foreach (int[] qpMove in queenPath)
                                    {
                                        foreach (int[] pMove in piece.PossibleMoves)
                                        {
                                            if (pMove[0] == qpMove[0] && pMove[1] == qpMove[1])
                                            {
                                                kingIsInCheckmate = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case Pieces.Rook:
                        List<int[]> rookPath = GetRookPath(aPiece.CurrentPos[0], aPiece.CurrentPos[1], king.CurrentPos[0], king.CurrentPos[1], aPiece.PossibleMoves);
                        foreach (List<ChessPiece> row in chessBoard)
                        {
                            foreach (ChessPiece piece in row)
                            {
                                if (piece.Color != aPiece.Color)
                                {
                                    foreach (int[] rpMove in rookPath)
                                    {
                                        foreach (int[] pMove in piece.PossibleMoves)
                                        {
                                            if (pMove[0] == rpMove[0] && pMove[1] == rpMove[1])
                                            {
                                                kingIsInCheckmate = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                }
            }


            return kingIsInCheckmate;
        }

        /// <summary>
        /// Checks if the king of the given color is in check
        /// </summary>
        /// <param name="color">The color of which to be checked</param>
        /// <returns>if the king is in check</returns>
        public bool CheckColorKing(PieceColor color)
        {
            King king = null;


            foreach (List<ChessPiece> rows in chessBoard)
            {
                foreach (ChessPiece piece in rows)
                {
                    if (piece.Name == Pieces.King)
                    {
                        if (piece.Color == color)
                        {
                            king = (King)piece;
                        }
                    }
                }
            }

            king.IsInCheck = false;

            foreach (List<ChessPiece> rows in chessBoard)
            {
                foreach (ChessPiece piece in rows)
                {
                    if (piece.Color != king.Color)
                    {
                        foreach (int[] move in piece.PossibleMoves)
                        {
                            if (move[0] == king.CurrentPos[0] && move[1] == king.CurrentPos[1])
                            {
                                king.IsInCheck = true;
                                king.AttackingPieces.Add(piece);
                            }
                        }
                    }
                }
            }

            return king.IsInCheck;

        }

        /// <summary>
        /// Gets the spaces inbetween the starting position to the end position for bishop
        /// </summary>
        /// <param name="oCol">The starting col</param>
        /// <param name="oRow">The starting row</param>
        /// <param name="nCol">The ending col</param>
        /// <param name="nRow">The ending row</param>
        /// <param name="possibleMoves">Possible moves for the bishop</param>
        /// <returns>A list of the spaces inbetween start and end</returns>
        private List<int[]> GetBishopPath(int oCol, int oRow, int nCol, int nRow, List<int[]> possibleMoves)
        {
            int cDistance = nCol - oCol;
            int rDistance = nRow - oRow;
            List<int[]> temp = new List<int[]>();

            //C R
            //- -
            foreach (int[] move in possibleMoves)
            {
                if (cDistance > 0 && rDistance > '0')
                {
                    if (move[0] < oCol && move[0] > nCol)
                    {
                        if (move[1] < oRow && move[1] > oCol)
                        {
                            if (GetPiece(move[0], move[1]).Name != Pieces.Empty)
                            {
                                temp.Add(move);
                            }
                        }
                    }
                }
                //+ +
                else if (cDistance < 0 && rDistance < '0')
                {
                    if (move[0] > oCol && move[0] < nCol)
                    {
                        if (move[1] > oRow && move[1] < oCol)
                        {
                            if (GetPiece(move[0], move[1]).Name != Pieces.Empty)
                            {
                                temp.Add(move);
                            }
                        }
                    }
                }
                //- +
                else if (cDistance > 0 && rDistance < '0')
                {
                    if (move[0] < oCol && move[0] > nCol)
                    {
                        if (move[1] > oRow && move[1] < oCol)
                        {
                            if (GetPiece(move[0], move[1]).Name != Pieces.Empty)
                            {
                                temp.Add(move);
                            }
                        }
                    }
                }
                //+ -
                else if (cDistance < 0 && rDistance > '0')
                {
                    if (move[0] > oCol && move[0] < nCol)
                    {
                        if (move[1] < oRow && move[1] > oCol)
                        {
                            if (GetPiece(move[0], move[1]).Name != Pieces.Empty)
                            {
                                temp.Add(move);
                            }
                        }
                    }
                }
                else
                {
                    return temp;
                }
            }

            return temp;
        }

        /// <summary>
        /// Gets the spaces inbetween the starting position to the end position for rook
        /// </summary>
        /// <param name="oCol">The starting column</param>
        /// <param name="oRow">The starting row</param>
        /// <param name="nCol">the ending column</param>
        /// <param name="nRow">the ending row</param>
        /// <param name="possibleMoves">The possible moves for the rook</param>
        /// <returns>The inbetween of the starting and ending moves</returns>
        private List<int[]> GetRookPath(int oCol, int oRow, int nCol, int nRow, List<int[]> possibleMoves)
        {
            List<int[]> temp = new List<int[]>();
            int distance;

            if (oCol == nCol && oRow == nRow)
            {
                return temp;
            }

            foreach (int[] move in possibleMoves)
            {
                if (oCol == nCol)
                {
                    distance = nRow - oRow;

                    if (distance < '0')
                    {
                        if (move[1] < oRow && move[1] > nRow)
                        {
                            if (GetPiece(move[0], move[1]).Name != Pieces.Empty)
                            {
                                temp.Add(move);
                            }
                        }
                    }
                    else if (distance > '0')
                    {

                        if (move[1] > oRow && move[1] < nRow)
                        {
                            if (GetPiece(move[0], move[1]).Name != Pieces.Empty)
                            {
                                temp.Add(move);
                            }
                        }
                    }
                    else
                    {
                        return temp;
                    }
                }
                else if (oRow == nRow)
                {
                    distance = nCol - oCol;

                    if (distance < '0')
                    {
                        if (move[0] < oCol && move[0] > nCol)
                        {
                            if (GetPiece(move[0], move[1]).Name != Pieces.Empty)
                            {
                                temp.Add(move);
                            }
                        }
                    }
                    else if (distance > '0')
                    {
                        if (move[0] > oCol && move[0] < nCol)
                        {
                            if (GetPiece(move[0], move[1]).Name != Pieces.Empty)
                            {
                                temp.Add(move);
                            }
                        }
                    }
                    else
                    {
                        return temp;
                    }
                }
            }
            return temp;
        }

        /// <summary>
        /// Gets the spaces inbetween the starting position to the end position for queen
        /// </summary>
        /// <param name="oCol">The starting column</param>
        /// <param name="oRow">The starting row</param>
        /// <param name="nCol">the ending column</param>
        /// <param name="nRow">the ending row</param>
        /// <param name="possibleMoves">The possible moves for the rook</param>
        /// <returns>The inbetween of the starting and ending moves</returns>
        private List<int[]> GetQueenPath(int oCol, int oRow, int nCol, int nRow, List<int[]> possibleMoves)
        {
            if (oCol == nCol)
            {
                return GetRookPath(oCol, oRow, nCol, nRow, possibleMoves);
            }
            else if (oRow == nRow)
            {
                return GetRookPath(oCol, oRow, nCol, nRow, possibleMoves);
            }
            else
            {
                return GetBishopPath(oCol, oRow, nCol, nRow, possibleMoves);
            }
        }

        /// <summary>
        /// Checks the spaces inbetween for a bishop piece the starting and destination to 
        /// make sure there is nothing in the way of the move.
        /// Uses the distance between the positions in order to find the direction
        /// Continues to find the spaces inbetween the positions based on the distance/direction
        /// </summary>
        /// <param name="oCol">Column of the orginal position of the piece</param>
        /// <param name="oRow">Row of the orginal position of the piece</param>
        /// <param name="nCol">Column of the new positsion of the piece</param>
        /// <param name="nRow">Row of the new posistion of the piece</param>
        /// <param name="possibleMoves">All possiple moves for the piece</param>
        /// <returns>If there was any obstruction to the path then return false otherwise returns true</returns>
        private bool CheckBishopPath(int oCol, int oRow, int nCol, int nRow, List<int[]> possibleMoves)
        {
            int cDistance = nCol - oCol;
            int rDistance = nRow - oRow;
            List<int[]> bishopPath = GetBishopPath(oCol, oRow, nCol, nRow, possibleMoves);

            if (bishopPath.Count > 1)
            {
                return false;
            }
            else if (bishopPath.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks the spaces inbetween for a bishop piece the starting and destination to 
        /// make sure there is nothing in the way of the move.
        /// Uses the distance between the positions in order to find the direction
        /// Continues to find the spaces inbetween the positions based on the distance/direction
        /// </summary>
        /// <param name="oCol">Column of the orginal position of the piece</param>
        /// <param name="oRow">Row of the orginal position of the piece</param>
        /// <param name="nCol">Column of the new positsion of the piece</param>
        /// <param name="nRow">Row of the new posistion of the piece</param>
        /// <param name="possibleMoves">All possiple moves for the piece</param>
        /// <returns>If there was any obstruction to the path then return false otherwise returns true</returns>
        public bool CheckRookPath(int oCol, int oRow, int nCol, int nRow, List<int[]> possibleMoves)
        {

            List<int[]> temp = GetRookPath(oCol, oRow, nCol, nRow, possibleMoves);

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

        /// <summary>
        /// Checks the spaces inbetween for a Queen piece the starting and destination to 
        /// make sure there is nothing in the way of the move.
        /// Uses the other Bishop/Rook Method at specific conditions to accomplish the task
        /// </summary>
        /// <param name="oCol">Column of the orginal position of the piece</param>
        /// <param name="oRow">Row of the orginal position of the piece</param>
        /// <param name="nCol">Column of the new positsion of the piece</param>
        /// <param name="nRow">Row of the new posistion of the piece</param>
        /// <param name="possibleMoves">All possiple moves for the piece</param>
        /// <returns>If there was any obstruction to the path then return false otherwise returns true</returns>
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

        /// <summary>
        /// A move checking method that makes sure the king does not move into check
        /// </summary>
        /// <param name="oCol">Starting column</param>
        /// <param name="oRow">Starting row</param>
        /// <param name="nCol">Ending column</param>
        /// <param name="nRow">Ending row</param>
        /// <param name="possibleMoves">Possible king moves</param>
        /// <param name="currentTurn">The color of the current turn</param>
        /// <returns>If the move is vaild or not</returns>
        private bool KeepKingOutOfCheck(int oCol, int oRow, int nCol, int nRow, List<int[]> possibleMoves, PieceColor currentTurn)
        {
            bool putsInCheck = false;

            foreach (List<ChessPiece> rows in chessBoard)
            {
                foreach (ChessPiece piece in rows)
                {
                    if (piece.Color != currentTurn)
                    {
                        foreach (int[] move in piece.PossibleMoves)
                        {
                            if (move[0] == nCol && move[1] == nRow)
                            {
                                putsInCheck = true;
                            }
                        }
                    }
                }
            }

            if (putsInCheck)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
