using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Piece_Movement.Models
{
    public class King : ChessPiece
    {
        public King(string color) : base(color, "Knight")
        {

        }

        public bool CheckMove(int oCol, int oRow, int nCol, int nRow)
        {

            List<int[]> possibleMoves = new List<int[]>()
            {
                new int[] { oCol, oRow + 1 },
                new int[] { oCol + 1, oRow + 1 },
                new int[] { oCol + 1, oRow },
                new int[] { oCol - 1, oRow + 1 },
                new int[] { oCol, oRow - 1 },
                new int[] { oCol - 1, oRow - 1 },
                new int[] { oCol - 1, oRow },
                new int[] { oCol - 1, oRow + 1 },

            };


            if (oCol == 0)
            {
                possibleMoves.Remove(new int[] { oCol - 1, oRow - 1 });
                possibleMoves.Remove(new int[] { oCol - 1, oRow });
                possibleMoves.Remove(new int[] { oCol - 1, oRow + 1 });
            }

            if (oCol == 7)
            {

                possibleMoves.Remove(new int[] { oCol + 1, oRow + 1 });
                possibleMoves.Remove(new int[] { oCol + 1, oRow });
                possibleMoves.Remove(new int[] { oCol - 1, oRow - 1 });

            }

            if (oRow == 0)
            {

            }

            if (oRow == 7)
            {

            }

        }
    }
}
