using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyPieceBase
{
	public abstract List<Address> MoveRanges(Square[,] board, Address from);
	public abstract int GetPieceValue();

	public BestHandInfo GetBestHand(Square[,] board, Address moveFrom)
	{
		BestHandInfo bestHandInfo = new BestHandInfo();
		bestHandInfo.MyPieceValue = GetPieceValue();
		bestHandInfo.MoveInfo.SetMoveFrom(board[moveFrom.X, moveFrom.Y]);

		var moveRanges = MoveRanges(board, moveFrom);

		foreach (var moveTo in moveRanges)
		{
			var square = board[moveTo.X, moveTo.Y];
			PieceInfo pieceInfo = square.PieceInfo;
			int pieceValue = PieceDefine.GetPieceValue(pieceInfo);
			// TODO: 空のマスにも移動できるように >= の = も付けているが、それでいいのか再検討
			if (pieceValue >= bestHandInfo.MaxPieceValue)
			{
				bestHandInfo.MaxPieceValue = pieceValue;
				bestHandInfo.MoveInfo.SetMoveTo(square.Address);
			}
		}

		return bestHandInfo;
	}
}
