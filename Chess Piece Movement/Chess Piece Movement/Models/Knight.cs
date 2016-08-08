using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Piece_Movement.Models
{
    public class Knight : ChessPiece
    {

        public Knight(string color) : base(color, "Knight")
        {

        }

        public bool CheckMove(int oCol, int oRow, int nCol, int nRow)
        {
            List<int[]> possibleMovements = new List<int[]>()
            {

                new int[] { oCol + 1,  oRow + 2},
                new int[] { oCol - 1,  oRow + 2},
                new int[] { oCol - 2,  oRow - 1},
                new int[] { oCol - 2,  oRow + 1},
                new int[] { oCol + 1,  oRow - 2},
                new int[] { oCol - 1,  oRow - 2},
                new int[] { oCol + 2,  oRow - 1},
                new int[] { oCol + 2,  oRow + 1},

            };

            int[] chosenMove = new int[] { nCol, nRow };

            if (possibleMovements.Contains(chosenMove))
            {
                return true;
            }
            else
            {
                return false;
            }


        }

    }
}
