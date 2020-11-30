using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI
{
	[System.Flags]
	public enum Difficulty
	{
		Easy = 1,
		Normal = 3,
		Hard = 5,
	}

	private int m_DeepLevel = (int)Difficulty.Easy;
	public void SetDeepLevel(Difficulty difficulty) => m_DeepLevel = (int)difficulty;

	private Square[,] m_Board;

	public PieceMoveInfo ThinkEnemyAIHand(Square[,] board)
	{
		m_Board = BoardUtility.CreateCopyBoard(board);

		BestHandInfo bestHand = AlphaBetaMax(level: m_DeepLevel, alpha: 0, beta: 999999);

		var moveFromSquare = board[bestHand.MoveInfo.MoveFrom.X, bestHand.MoveInfo.MoveFrom.Y];
		var moveToSquare = board[bestHand.MoveInfo.MoveTo.X, bestHand.MoveInfo.MoveTo.Y];

		Debug.LogError(bestHand.MoveInfo.MoveTo+" 駒:"+bestHand.MoveInfo.PieceInfo);

		return bestHand.MoveInfo;
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
		return bestHandInfoList[Random.Range(0, bestHandInfoList.Count)];
	}

	private BestHandInfo ThinkBestPlayerHand(Square[,] board)
	{
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
					bestHandInfoList.Clear();
					bestHandInfoList.Add(bestHand);
				}
				else if (bestHand.Score == bestHandInfoList[0].Score)
				{
					bestHandInfoList.Add(bestHand);
				}
			}
		}

		var best = bestHandInfoList[Random.Range(0, bestHandInfoList.Count)];
		best.Score *= -1; // AIにとってはマイナスなので、-1をかける
		return best;
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
		List<PieceMoveInfo> allHandList = GetAllHandList(m_Board, Board.Player.Enemy);

		const int INIT_SCORE = -9999;
		var bestHand = new BestHandInfo{Score = INIT_SCORE};
		foreach (var hand in allHandList)
		{
			int score = 0;
			var saveBoard = BoardUtility.CreateCopyBoard(m_Board);

			// AIの手を打つ
			m_Board[hand.MoveFrom.X, hand.MoveFrom.Y].SetPieceInfoSimple(PieceInfo.Empty);
			m_Board[hand.MoveTo.X, hand.MoveTo.Y].SetPieceInfoSimple(hand.PieceInfo);

			// 次の相手の手
			var best = AlphaBetaMin(level - 1, alpha, beta);
			score = best.Score;

			// AIの手を戻す
			m_Board = saveBoard;

			if (score >= beta)
			{
				// beta値を上回ったら探索中止
				return bestHand;
			}

			if (score >= bestHand.Score)
			{
				// スコアが同じ場合はランダム抽選
				if (score != INIT_SCORE &&
					score == bestHand.Score &&
					Random.Range(0, 2) == 0)
				{
					continue;
				}

				// より良い手が見つかった
				bestHand.Score = score;
				alpha = Mathf.Max(alpha, bestHand.Score);
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
			return ThinkBestPlayerHand(m_Board); // 現在の局面の評価
		}

		// プレイヤーの可能な手を全て取得
		List<PieceMoveInfo> allHandList = GetAllHandList(m_Board, Board.Player.Self);

		const int INIT_SCORE = 9999;
		var bestHand = new BestHandInfo{Score = INIT_SCORE};
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

			if (score <= bestHand.Score)
			{
				// スコアが同じ場合はランダム抽選
				if (score != INIT_SCORE &&
					score == bestHand.Score &&
					Random.Range(0, 2) == 0)
				{
					continue;
				}

				bestHand.Score = score;
				beta = Mathf.Min(beta, bestHand.Score);
				bestHand.MoveInfo.SetMoveFrom(m_Board[hand.MoveFrom.X, hand.MoveFrom.Y]);
				bestHand.MoveInfo.SetMoveTo(hand.MoveTo);
			}
		}

		return bestHand;
	}

	private List<PieceMoveInfo> GetAllHandList(Square[,] board, Board.Player playerType)
	{
		var handList = new List<PieceMoveInfo>();

		for (int x = 1; x < Board.BOARD_WIDTH - 1; x++)
		for (int y = 1; y < Board.BOARD_WIDTH - 1; y++)
		{
			Square square = board[x, y];
			PieceBase piece = (playerType == Board.Player.Self) ? GetSquarePlayerPiece(square) : GetSquareEnemyPiece(square);
			if (piece == null) continue;

			var moveRanges = piece.MoveRanges(board, square.Address);
			for (int i = 0; i < moveRanges.Count; i++)
			{
				handList.Add(new PieceMoveInfo(square.Address, moveRanges[i], square.PieceInfo));
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
