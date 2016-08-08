using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Piece_Movement.Models
{
    public class Queen : ChessPiece
    {
        public Queen(string color) : base(color, "Queen")
        {

        }


        public bool CheckMove(int oCol, int oRow, int nCol, int nRow)
        {

            List<int[]> possibleMoves = new List<int[]>();

            int tempCol = oCol;
            int tempRow = oRow;

            do
            {
                possibleMoves.Add(new int[] { tempCol++, tempRow++ });
            }
            while (tempCol < 8 && tempRow < 8);
            do
            {
                possibleMoves.Add(new int[] { tempCol--, tempRow-- });
            }
            while (tempCol > -1 && tempRow > -1);
            do
            {
                possibleMoves.Add(new int[] { tempCol--, tempRow++ });
            }
            while (tempCol > -1 && tempRow < 8);
            do
            {
                possibleMoves.Add(new int[] { tempCol++, tempRow-- });
            }
            while (tempCol < 8 && tempRow > -1);

            if (oCol == nCol)
            {
                return true;
            }
            else if (oRow == nRow)
            {
                return true;
            }
            else if (possibleMoves.Contains(new int[] { nCol, nRow }))
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
