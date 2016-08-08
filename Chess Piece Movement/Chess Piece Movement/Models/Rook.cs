using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Piece_Movement.Models
{
    public class Rook : ChessPiece
    {

        public Rook(string color) : base(color, "Rook")
        {

        }


        public bool CheckMove(int oCol, int oRow, int nCol, int nRow)
        {

            if (oCol == nCol)
            {
                return true;
            }
            else if (oRow == nRow)
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
