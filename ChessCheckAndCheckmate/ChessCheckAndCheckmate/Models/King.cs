using Chess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Piece_Movement.Models
{
    public class King : ChessPiece
    {

        /// <summary>
        /// Boolean that tells if the piece is in check
        /// </summary>
        public bool IsInCheck { get; set; } = false;

        /// <summary>
        /// Pieces that put the king in check
        /// </summary>
        public List<ChessPiece> AttackingPieces { get; set; } = new List<ChessPiece>();

        public King(PieceColor color) : base(color, Pieces.King)
        {

        }

        public override void UpdateMoves()
        {
            List<int[]> possibleMoves = new List<int[]>()
            {
                new int[] { CurrentPos[0], CurrentPos[1] + 1 },
                new int[] { CurrentPos[0] + 1, CurrentPos[1] + 1 },
                new int[] { CurrentPos[0] + 1, CurrentPos[1] },
                new int[] { CurrentPos[0] + 1, CurrentPos[1] - 1 },
                new int[] { CurrentPos[0], CurrentPos[1] - 1 },
                new int[] { CurrentPos[0] - 1, CurrentPos[1] - 1 },
                new int[] { CurrentPos[0] - 1, CurrentPos[1] },
                new int[] { CurrentPos[0] - 1, CurrentPos[1] + 1 },

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
            PossibleMoves = possibleMoves;
        }
    }
}
