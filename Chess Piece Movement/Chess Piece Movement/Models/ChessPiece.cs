using Chess_Piece_Movement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Piece_Movement.Models
{
    public abstract class ChessPiece
    {

        public PieceColor Color { get; set; } = PieceColor.Empty;
        public Pieces Name { get; set; } = Pieces.Empty;
        public char pieceSymbol { get; set; } = '-';


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
    }
}
