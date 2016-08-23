using Chess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Piece_Movement.Models
{
    public class Pawn : ChessPiece
    {

        private bool HasMoved { get; set; }

        public Pawn(PieceColor Color) : base(Color, Pieces.Pawn)
        {

        }

        public override void UpdateMoves()
        {
            List<int[]> possibleMoves;

            if (Name != Pieces.Empty)
            {
                if (HasMoved)
                {
                    possibleMoves = new List<int[]>()
                    {
                        new int[] { CurrentPos[0], CurrentPos[1] + 1 }
                    };
                }
                else
                {
                    possibleMoves = new List<int[]>()
                    {
                        new int[] { CurrentPos[0], CurrentPos[1] + 2 },
                        new int[] { CurrentPos[0], CurrentPos[1] + 1 }
                    };
                }


                for (int i = 0; i < possibleMoves.Count; i++)
                {
                    if (possibleMoves[i][0] < 0 && possibleMoves[i][0] > 7)
                    {
                        possibleMoves.RemoveAt(i);
                    }
                    else if (possibleMoves[i][1] < 0 && possibleMoves[i][1] > 7)
                    {
                        possibleMoves.RemoveAt(i);
                    }
                }
                PossibleMoves = possibleMoves;
            }
        }
    }
}
