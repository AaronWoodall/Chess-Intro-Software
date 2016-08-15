using Chess_Piece_Movement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Piece_Movement.Models
{
    public class Rook : ChessPiece
    {

        public Rook(PieceColor color) : base(color, Pieces.Rook)
        {

        }


        public List<int[]> GetMoves(int oCol, int oRow)
        {
            List<int[]> possibleMoves = new List<int[]>();
            int tempOCol = oCol;
            int tempORow = oRow;

            do
            {
                possibleMoves.Add(new int[] { tempOCol++, tempORow });
            }
            while (tempOCol < 8);
            tempOCol = oCol;
            tempORow = oRow;
            do
            {
                possibleMoves.Add(new int[] { tempOCol--, tempORow });
            }
            while (tempOCol > -1);
            tempOCol = oCol;
            tempORow = oRow;
            do
            {
                possibleMoves.Add(new int[] { tempOCol, tempORow++ });
            }
            while (tempORow < 8);
            tempOCol = oCol;
            tempORow = oRow;
            do
            {
                possibleMoves.Add(new int[] { tempOCol, tempORow-- });
            }
            while (tempORow > -1);
            tempOCol = oCol;
            tempORow = oRow;

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
