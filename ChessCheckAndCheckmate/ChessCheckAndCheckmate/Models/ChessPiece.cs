using Chess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Piece_Movement.Models
{
    public abstract class ChessPiece
    {

        /// <summary>
        /// The color of the piece
        /// </summary>
        public PieceColor Color { get; private set; } = PieceColor.Empty;

        /// <summary>
        /// The name of the piece
        /// </summary>
        public Pieces Name { get; private set; } = Pieces.Empty;

        /// <summary>
        /// The character that is used to be prited out 
        /// </summary>
        public char pieceSymbol { get; private set; } = '-';
        /// <summary>
        /// The current position of on the board that the piece is at
        /// </summary>
        public int[] CurrentPos { get; private set; }
        /// <summary>
        /// The possible moves for this piece
        /// </summary>
        public List<int[]> PossibleMoves { get; private set; } = new List<int[]>();

        /// <summary>
        /// Constructer for the chesspiece setting properties based on the Color and name
        /// </summary>
        /// <param name="color">Contols the casing of the pieceSymbol</param>
        /// <param name="name">Contols the character that is the pieceSymbol</param>
        public ChessPiece(PieceColor color = PieceColor.Empty, Pieces name = Pieces.Empty)
        {
            Name = name;

            Color = color;

            if (Color == PieceColor.White)
            {
                switch (Name)
                {
                    case Pieces.King:
                        pieceSymbol = 'k';
                        break;
                    case Pieces.Queen:
                        pieceSymbol = 'q';
                        break;
                    case Pieces.Bishop:
                        pieceSymbol = 'b';
                        break;
                    case Pieces.Rook:
                        pieceSymbol = 'r';
                        break;
                    case Pieces.Knight:
                        pieceSymbol = 'n';
                        break;
                    case Pieces.Pawn:
                        pieceSymbol = 'p';
                        break;
                }

            }
            else if (Color == PieceColor.Black)
            {
                switch (Name)
                {
                    case Pieces.King:
                        pieceSymbol = 'K';
                        break;
                    case Pieces.Queen:
                        pieceSymbol = 'Q';
                        break;
                    case Pieces.Bishop:
                        pieceSymbol = 'B';
                        break;
                    case Pieces.Rook:
                        pieceSymbol = 'R';
                        break;
                    case Pieces.Knight:
                        pieceSymbol = 'N';
                        break;
                    case Pieces.Pawn:
                        pieceSymbol = 'P';
                        break;
                }
            }
            else if (Color == PieceColor.Empty)
            {
                Name = Pieces.Empty;
            }

        }

        /// <summary>
        /// Updates the possible moves 
        /// </summary>
        public abstract void UpdateMoves();
    }
}
