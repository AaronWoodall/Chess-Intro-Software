using Chess_Piece_Movement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Piece_Movement.Models
{
    public class Bishop : ChessPiece
    {

        public Bishop(PieceColor color) : base(color, Pieces.Bishop)
        {

        }

        public List<int[]> GetMoves(int oCol, int oRow)
        {

            List<int[]> possibleMoves = new List<int[]>();

            int tempCol = oCol;
            int tempRow = oRow;

            do
            {
                possibleMoves.Add(new int[] { tempCol++, tempRow++ });
            }
            while (tempCol < 8 && tempRow < 8);
            tempCol = oCol;
            tempRow = oRow;
            do
            {
                possibleMoves.Add(new int[] { tempCol--, tempRow-- });
            }
            while (tempCol > -1 && tempRow > -1);
            tempCol = oCol;
            tempRow = oRow;
            do
            {
                possibleMoves.Add(new int[] { tempCol--, tempRow++ });
            }
            while (tempCol > -1 && tempRow < 8);
            tempCol = oCol;
            tempRow = oRow;
            do
            {
                possibleMoves.Add(new int[] { tempCol++, tempRow-- });
            }
            while (tempCol < 8 && tempRow > -1);

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
