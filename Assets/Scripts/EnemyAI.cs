using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI
{
	private Square[,] m_Board;
	public void PutPiece(Square[,] board)
	{
		m_Board = BoardUtility.CreateCopyBoard(board);
		Debug.LogError("==前=board::"+board[5, 1].PieceInfo+"==");
		m_Board[5, 1].SetPieceInfo(PieceInfo.Empty);
		Debug.LogError("==後=board::"+board[5, 1].PieceInfo+"==");

		// Debug.LogError("==前=board::"+board[5,1].PieceInfo+"==");
		BestHandInfo bestHand = AlphaBetaMax(level: 1, alpha: 0, beta: 999999);
		// Debug.LogError("==後=board::"+board[5,1].PieceInfo+"==");

		var moveFromSquare = bestHand.MoveInfo.SelectingSquare;
		Debug.LogError("===MoveFrom::"+bestHand.MoveInfo.MoveFrom+"==MoveTo::"+bestHand.MoveInfo.MoveTo+"==");
		var moveToSquare = board[bestHand.MoveInfo.MoveTo.X, bestHand.MoveInfo.MoveTo.Y];

		moveToSquare.SetPieceInfo(moveFromSquare.PieceInfo);
		moveFromSquare.ResetPieceInfo();
	}

	private BestHandInfo ThinkBestAIHand(Square[,] board)
	{
		var bestHandInfoList = new List<BestHandInfo>();
		bestHandInfoList.Add(new BestHandInfo());

		for (int x = 1; x < Board.BOARD_WIDTH - 1; x++)
		{
			for (int y = 1; y < Board.BOARD_WIDTH - 1; y++)
			{
				Square square = board[x, y];
				EnemyPieceBase enemyPiece = GetSquareEnemyPiece(square);
				if (enemyPiece == null) continue;

				var bestHand = enemyPiece.GetBestHand(board, square.Address);
				if (!bestHand.MoveInfo.MoveTo.IsValid())
				{
					continue;
				}
				if (bestHand.ScoreMax > bestHandInfoList[0].ScoreMax)
				{
					bestHandInfoList.Clear();
					bestHandInfoList.Add(bestHand);
				}
				else if (bestHand.ScoreMax == bestHandInfoList[0].ScoreMax)
				{
					bestHandInfoList.Add(bestHand);
				}
			}
		}

		return bestHandInfoList[Random.Range(0, bestHandInfoList.Count)];
	}

	private BestHandInfo ThinkBestPlayerHand(Square[,] board)
	{
		var bestHandInfoList = new List<BestHandInfo>();
		bestHandInfoList.Add(new BestHandInfo());

		for (int x = 1; x < Board.BOARD_WIDTH - 1; x++)
		{
			for (int y = 1; y < Board.BOARD_WIDTH - 1; y++)
			{
				Square square = board[x, y];
				PieceBase playerPiece = GetSquarePlayerPiece(square);
				if (playerPiece == null) continue;

				var bestHand = playerPiece.GetBestHand(board, square.Address);
				if (!bestHand.MoveInfo.MoveTo.IsValid())
				{
					continue;
				}
				if (bestHand.ScoreMax > bestHandInfoList[0].ScoreMax)
				{
					bestHandInfoList.Clear();
					bestHandInfoList.Add(bestHand);
				}
				else if (bestHand.ScoreMax == bestHandInfoList[0].ScoreMax)
				{
					bestHandInfoList.Add(bestHand);
				}
			}
		}

		return bestHandInfoList[Random.Range(0, bestHandInfoList.Count)];
	}

	/// <summary>
	/// 自分(AI)の手を調べる
	/// </summary>
	private BestHandInfo AlphaBetaMax(int level, int alpha, int beta)
	{
		if (level == 0)
		{
			return ThinkBestAIHand(m_Board); // 現在の局面の評価
		}

		// AIの可能な手を全て取得
		List<HandInfo> allHandList = GetAllAIHandList(m_Board);

		var bestHand = new BestHandInfo();
		foreach (var hand in allHandList)
		{
			int score = 0;
			var saveBoard = BoardUtility.CreateCopyBoard(m_Board);
			// AIの手を打つ
			m_Board[hand.MoveFrom.X, hand.MoveFrom.Y].SetPieceInfo(PieceInfo.Empty);
			m_Board[hand.MoveTo.X, hand.MoveTo.Y].SetPieceInfo(hand.PieceInfo);

			// 次の相手の手
			score = AlphaBetaMin(level - 1, alpha, beta).ScoreMin;

			// AIの手を戻す
			m_Board = saveBoard;

			if (score >= beta)
			{
				Debug.LogError("=探索中止==score::"+score+"=beta::"+beta+"=");
				// beta値を上回ったら探索中止
				return bestHand;
			}

			if (score > bestHand.ScoreMax)
			{
				Debug.LogError("=より良い手が見つかった==score::"+score+"=bestHand.ScoreMax::"+bestHand.ScoreMax+"=");
				// より良い手が見つかった
				bestHand.ScoreMax = score;
				alpha = Mathf.Max(alpha, bestHand.ScoreMax);
				bestHand.MoveInfo.SetMoveFrom(m_Board[hand.MoveFrom.X, hand.MoveFrom.Y]);
				bestHand.MoveInfo.SetMoveTo(hand.MoveTo);
			}
		}

		return bestHand;
	}

	/// <summary>
	/// 相手の手を調べる
	/// </summary>
	private BestHandInfo AlphaBetaMin(int level, int alpha, int beta)
	{
		if (level == 0)
		{
			BestHandInfo best = ThinkBestPlayerHand(m_Board);
			return best;
			// return ThinkBestPlayerHand(m_Board); // 現在の局面の評価
		}

		// プレイヤーの可能な手を全て取得
		List<HandInfo> allHandList = GetAllPlayerHandList(m_Board);

		var bestHand = new BestHandInfo();
		foreach (var hand in allHandList)
		{
			int score = 0;
			var saveBoard = BoardUtility.CreateCopyBoard(m_Board);
			// プレイヤーの手を打つ
			m_Board[hand.MoveFrom.X, hand.MoveFrom.Y].SetPieceInfo(PieceInfo.Empty);
			m_Board[hand.MoveTo.X, hand.MoveTo.Y].SetPieceInfo(hand.PieceInfo);

			// 次のAIの手
			score = AlphaBetaMax(level - 1, alpha, beta).ScoreMax;

			// プレイヤーの手を戻す
			m_Board = saveBoard;

			if (score <= alpha)
			{
				// alpha値を下回ったら探索中止
				return bestHand;
			}

			if (score < bestHand.ScoreMin)
			{
				Debug.LogError("=より良い手が見つかった==score::"+score+"=ScoreMin::"+bestHand.ScoreMin+"=");
				bestHand.ScoreMin = score;
				beta = Mathf.Min(beta, bestHand.ScoreMin);
				bestHand.MoveInfo.SetMoveFrom(m_Board[hand.MoveFrom.X, hand.MoveFrom.Y]);
				bestHand.MoveInfo.SetMoveTo(hand.MoveTo);
			}
		}

		return bestHand;
	}

	private List<HandInfo> GetAllAIHandList(Square[,] board)
	{
		var handList = new List<HandInfo>();

		for (int x = 1; x < Board.BOARD_WIDTH - 1; x++)
		for (int y = 1; y < Board.BOARD_WIDTH - 1; y++)
		{
			Square square = board[x, y];
			EnemyPieceBase enemyPiece = GetSquareEnemyPiece(square);
			if (enemyPiece == null) continue;

			var moveRanges = enemyPiece.MoveRanges(board, square.Address);
			for (int i = 0; i < moveRanges.Count; i++)
			{
				handList.Add(new HandInfo {
					MoveFrom = square.Address,
					MoveTo = moveRanges[i],
					PieceInfo = square.PieceInfo,
				});
			}
		}

		return handList;
	}

	private List<HandInfo> GetAllPlayerHandList(Square[,] board)
	{
		var handList = new List<HandInfo>();

		for (int x = 1; x < Board.BOARD_WIDTH - 1; x++)
		for (int y = 1; y < Board.BOARD_WIDTH - 1; y++)
		{
			Square square = board[x, y];
			PieceBase playerPiece = GetSquarePlayerPiece(square);
			if (playerPiece == null) continue;

			var moveRanges = playerPiece.MoveRanges(board, square.Address);
			for (int i = 0; i < moveRanges.Count; i++)
			{
				handList.Add(new HandInfo {
					MoveFrom = square.Address,
					MoveTo = moveRanges[i],
					PieceInfo = square.PieceInfo,
				});
			}
		}

		return handList;
	}

	// private BestHandInfo ThinkBestHandAfterThreeMove(Square[,] board)
	// {
	// 	Square[,] copyBoard = BoardUtility.CreateCopyBoard(board);

	// 	// 1手先:自分(敵AI)の手を探索
	// 	for (int x = 1; x < Board.BOARD_WIDTH - 1; x++)
	// 	for (int y = 1; y < Board.BOARD_WIDTH - 1; y++)
	// 	{
	// 		Square square = copyBoard[x, y];
	// 		EnemyPieceBase enemyPiece = GetSquareEnemyPiece(square);
	// 		if (enemyPiece == null) continue;

	// 		var moveRanges = enemyPiece.MoveRanges(copyBoard, square.Address);

	// 		// 2手先:相手の手を探索
	// 		for (int i = 0; i < moveRanges.Count; i++)
	// 		for (int xx = 1; xx < Board.BOARD_WIDTH - 1; xx++)
	// 		for (int yy = 1; yy < Board.BOARD_WIDTH - 1; yy++)
	// 		{
	// 			var range = moveRanges[i];
	// 			Square[,] copyBoard2 = BoardUtility.CreateCopyBoard(copyBoard);
	// 			copyBoard2[xx, yy].SetPieceInfo(square.PieceInfo);

	// 			// 駒の1手先の移動に対して、相手の移動先を探索
	// 			for (int xxx = 1; xxx < Board.BOARD_WIDTH - 1; xxx++)
	// 			for (int yyy = 1; yyy < Board.BOARD_WIDTH - 1; yyy++)
	// 			{

	// 			}
	// 		}
	// 	}
	// }

	private EnemyPieceBase GetSquareEnemyPiece(Square square)
	{
		if (!square.IsEnemy())
		{
			return null;
		}

		return PieceUtility.CreateEnemyPiece(square.PieceInfo);
	}

	private PieceBase GetSquarePlayerPiece(Square square)
	{
		if (!square.IsSelf())
		{
			return null;
		}

		return PieceUtility.CreatePiece(square.PieceInfo);
	}
}

public class BestHandInfo
{
	public int ScoreMax = -99999;
	public int ScoreMin = 99999;
	public PieceMoveInfo MoveInfo = new PieceMoveInfo();
}

public class HandInfo
{
	public Address MoveFrom;
	public Address MoveTo;
	public PieceInfo PieceInfo;
}
