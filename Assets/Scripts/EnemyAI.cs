using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI
{
	private Square[,] m_Board;
	public void PutPiece(Square[,] board)
	{
		m_Board = BoardUtility.CreateCopyBoard(board);

		BestHandInfo bestHand = AlphaBetaMax(level: 2, alpha: 0, beta: 999999);
		Debug.LogError("MoveFrom::"+bestHand.MoveInfo.MoveFrom+" MoveTo:"+bestHand.MoveInfo.MoveTo+"=");

		var moveFromSquare = board[bestHand.MoveInfo.MoveFrom.X, bestHand.MoveInfo.MoveFrom.Y];
		var moveToSquare = board[bestHand.MoveInfo.MoveTo.X, bestHand.MoveInfo.MoveTo.Y];

		moveToSquare.SetPieceInfo(moveFromSquare.PieceInfo);
		moveFromSquare.ResetPieceInfo();
	}

	private BestHandInfo ThinkBestAIHand(Square[,] board)
	{
		var bestHandInfoList = new List<BestHandInfo>();
		bestHandInfoList.Add(new BestHandInfo { Score = -9999 });

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
				Debug.LogError("=駒::"+bestHand.MoveInfo.PieceInfo+" MoveTo::"+bestHand.MoveInfo.MoveTo+"==Max::"+bestHand.Score+"==");
				if (bestHand.Score > bestHandInfoList[0].Score)
				{
					bestHandInfoList.Clear();
					bestHandInfoList.Add(bestHand);
				}
				else if (bestHand.Score == bestHandInfoList[0].Score)
				{
					bestHandInfoList.Add(bestHand);
				}
			}
		}

		var a = bestHandInfoList[Random.Range(0, bestHandInfoList.Count)];
		Debug.LogError("=ベスト 駒"+a.MoveInfo.PieceInfo+"==MoveTo::"+a.MoveInfo.MoveTo+"==");
		return bestHandInfoList[Random.Range(0, bestHandInfoList.Count)];
	}

	private BestHandInfo ThinkBestPlayerHand(Square[,] board)
	{
		// Debug.LogError("===ThinkBestPlayerHand===");
		var bestHandInfoList = new List<BestHandInfo>();
		bestHandInfoList.Add(new BestHandInfo { Score = -9999 });

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

				if (bestHand.Score > bestHandInfoList[0].Score)
				{
					// Debug.LogError("=駒::"+bestHand.MoveInfo.PieceInfo+" MoveTo::"+bestHand.MoveInfo.MoveTo+"===Max::"+bestHand.Score+"=");
					bestHandInfoList.Clear();
					bestHandInfoList.Add(bestHand);
				}
				else if (bestHand.Score == bestHandInfoList[0].Score)
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
			m_Board[hand.MoveFrom.X, hand.MoveFrom.Y].SetPieceInfoSimple(PieceInfo.Empty);
			m_Board[hand.MoveTo.X, hand.MoveTo.Y].SetPieceInfoSimple(hand.PieceInfo);

			// 次の相手の手
			score = AlphaBetaMin(level - 1, alpha, beta).Score;

			// AIの手を戻す
			m_Board = saveBoard;

			if (score >= beta)
			{
				Debug.LogError("=探索中止==score::"+score+"=beta::"+beta+"=");
				// beta値を上回ったら探索中止
				return bestHand;
			}

			if (score > bestHand.Score)
			{
				Debug.LogError("=より良い手が見つかった==score::"+score+"=bestHand.ScoreMax::"+bestHand.Score+"=");
				// より良い手が見つかった
				bestHand.Score = score;
				alpha = Mathf.Max(alpha, bestHand.Score);
				bestHand.MoveInfo.SetMoveFrom(m_Board[hand.MoveFrom.X, hand.MoveFrom.Y]);
				bestHand.MoveInfo.SetMoveTo(hand.MoveTo);
			}
		}

		Debug.LogError("=駒:"+bestHand.MoveInfo.PieceInfo+"==MoveTo::"+bestHand.MoveInfo.MoveTo+"==bestHand.Score::"+bestHand.Score+"==");
		return bestHand;
	}

	/// <summary>
	/// 相手の手を調べる
	/// </summary>
	private BestHandInfo AlphaBetaMin(int level, int alpha, int beta)
	{
		if (level == 0)
		{
			return ThinkBestPlayerHand(m_Board); // 現在の局面の評価
		}

		// プレイヤーの可能な手を全て取得
		List<HandInfo> allHandList = GetAllPlayerHandList(m_Board);

		var bestHand = new BestHandInfo();
		foreach (var hand in allHandList)
		{
			int score = 0;
			var saveBoard = BoardUtility.CreateCopyBoard(m_Board);
			// プレイヤーの手を打つ
			m_Board[hand.MoveFrom.X, hand.MoveFrom.Y].SetPieceInfoSimple(PieceInfo.Empty);
			m_Board[hand.MoveTo.X, hand.MoveTo.Y].SetPieceInfoSimple(hand.PieceInfo);

			// 次のAIの手
			score = AlphaBetaMax(level - 1, alpha, beta).Score;

			// プレイヤーの手を戻す
			m_Board = saveBoard;

			if (score <= alpha)
			{
				// alpha値を下回ったら探索中止
				return bestHand;
			}

			if (score < bestHand.Score)
			{
				Debug.LogError("=より悪い手が見つかった==score::"+score+"=Max::"+bestHand.Score+"=");
				bestHand.Score = score;
				beta = Mathf.Min(beta, bestHand.Score);
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
	public int Score = 0;
	public PieceMoveInfo MoveInfo = new PieceMoveInfo();
}

public class HandInfo
{
	public Address MoveFrom;
	public Address MoveTo;
	public PieceInfo PieceInfo;
}
