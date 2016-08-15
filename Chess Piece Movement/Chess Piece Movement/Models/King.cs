using Chess_Piece_Movement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Piece_Movement.Models
{
    public class King : ChessPiece
    {
        public King(PieceColor color) : base(color, Pieces.King)
        {

        }

        public List<int[]> GetMoves(int oCol, int oRow)
        {

            List<int[]> possibleMoves = new List<int[]>()
            {
                new int[] { oCol, oRow + 1 },
                new int[] { oCol + 1, oRow + 1 },
                new int[] { oCol + 1, oRow },
                new int[] { oCol + 1, oRow - 1 },
                new int[] { oCol, oRow - 1 },
                new int[] { oCol - 1, oRow - 1 },
                new int[] { oCol - 1, oRow },
                new int[] { oCol - 1, oRow + 1 },

            };

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

            return possibleMoves;

        }
    }
}
