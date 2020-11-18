using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PieceBase
{
	public abstract List<Address> MoveRanges(Square[,] board, Address from);

	public BestHandInfo GetBestHand(Square[,] board, Address moveFrom)
	{
		BestHandInfo bestHandInfo = new BestHandInfo();
		bestHandInfo.MoveInfo.SetMoveFrom(board[moveFrom.X, moveFrom.Y]);

		var moveRanges = MoveRanges(board, moveFrom);

		foreach (var moveTo in moveRanges)
		{
			PieceInfo pieceInfo = board[moveTo.X, moveTo.Y].PieceInfo;
			int pieceValue = PieceDefine.GetPieceValue(pieceInfo);
			// TODO: 空のマスにも移動できるように >= の = も付けているが、それでいいのか再検討
			if (pieceValue >= bestHandInfo.ScoreMax)
			{
				bestHandInfo.ScoreMax = pieceValue;
				bestHandInfo.MoveInfo.SetMoveTo(moveTo);
			}
		}

		return bestHandInfo;
	}
}
