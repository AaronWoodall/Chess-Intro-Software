using Chess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Piece_Movement.Models
{
    public class Queen : ChessPiece
    {
        public Queen(PieceColor color) : base(color, Pieces.Queen)
        {

        }

        public override void UpdateMoves()
        {

            List<int[]> possibleMoves = new List<int[]>();

            int tempCol = CurrentPos[0];
            int tempRow = CurrentPos[1];

            do
            {
                possibleMoves.Add(new int[] { tempCol++, tempRow++ });
            }
            while (tempCol < 8 && tempRow < 8);
            tempCol = CurrentPos[0];
            tempRow = CurrentPos[1];

            do
            {
                possibleMoves.Add(new int[] { tempCol--, tempRow-- });
            }
            while (tempCol > -1 && tempRow > -1);
            tempCol = CurrentPos[0];
            tempRow = CurrentPos[1];
            do
            {
                possibleMoves.Add(new int[] { tempCol--, tempRow++ });
            }
            while (tempCol > -1 && tempRow < 8);
            tempCol = CurrentPos[0];
            tempRow = CurrentPos[1];
            do
            {
                possibleMoves.Add(new int[] { tempCol++, tempRow-- });
            }
            while (tempCol < 8 && tempRow > -1);
            tempCol = CurrentPos[0];
            tempRow = CurrentPos[1];
            do
            {
                possibleMoves.Add(new int[] { tempCol++, tempRow });
            }
            while (tempCol < 8);
            tempCol = CurrentPos[0];
            tempRow = CurrentPos[1];
            do
            {
                possibleMoves.Add(new int[] { tempCol--, tempRow });
            }
            while (tempCol > -1);
            tempCol = CurrentPos[0];
            tempRow = CurrentPos[1];
            do
            {
                possibleMoves.Add(new int[] { tempCol, tempRow++ });
            }
            while (tempRow < 8);
            tempCol = CurrentPos[0];
            tempRow = CurrentPos[1];
            do
            {
                possibleMoves.Add(new int[] { tempCol, tempRow-- });
            }
            while (tempRow > -1);
            tempCol = CurrentPos[0];
            tempRow = CurrentPos[1];

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
