using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Piece_Movement.Models
{
    public abstract class ChessPiece
    {

        public string Color { get; set; }
        public string Name { get; set; }
        public char pieceSymbol { get; set; }


        public ChessPiece(string color, string name)
        {
            Name = name;

            switch (color.ToUpper())
            {
                case "L":
                    Color = "White";
                    break;
                case "D":
                    Color = "Black";
                    break;
            }

            if (Color == "White")
            {
                switch (Name)
                {
                    case "Pawn":
                        pieceSymbol = 'p';
                        break;
                    case "King":
                        pieceSymbol = 'k';
                        break;
                    case "Queen":
                        pieceSymbol = 'q';
                        break;
                    case "Rook":
                        pieceSymbol = 'r';
                        break;
                    case "Bishop":
                        pieceSymbol = 'b';
                        break;
                    case "Knight":
                        pieceSymbol = 'n';
                        break;
                }

            }
            else if (Color == "Black")
            {
                switch (Name)
                {
                    case "Pawn":
                        pieceSymbol = 'P';
                        break;
                    case "King":
                        pieceSymbol = 'K';
                        break;
                    case "Queen":
                        pieceSymbol = 'Q';
                        break;
                    case "Rook":
                        pieceSymbol = 'R';
                        break;
                    case "Bishop":
                        pieceSymbol = 'B';
                        break;
                    case "Knight":
                        pieceSymbol = 'N';
                        break;
                }
            }

        }
    }
}
