using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
	public static readonly int BOARD_WIDTH = 11;

	public enum Player
	{
		Self,
		Enemy,
	}

	private EnemyAI m_EnemyAI = new EnemyAI();

	[SerializeField]
	private OwnPieces m_SelfAcquiredPieces;
	[SerializeField]
	private OwnPieces m_EnemyAcquiredPieces;

	// 将棋は 9 × 9 マスだが、番兵を入れるため上下左右1マスずつ追加
	// TODO: 番兵をまだ活用できてないので後ほど対応を検討
	private Square[,] m_Board = new Square[BOARD_WIDTH, BOARD_WIDTH];
	private Square GetSquare(Address address)
	{
		if (!address.IsValid())
		{
			return null;
		}

		return m_Board[address.X, address.Y];
	}

	private PieceMoveInfo m_PieceMoveInfo = new PieceMoveInfo();

	void Start()
	{
		for (int y = 1; y < BOARD_WIDTH - 1; y++)
		{
			GameObject rowObj = GameObject.Find($"Row_{y}");
			for (int x = 1; x < BOARD_WIDTH - 1; x++)
			{
				GameObject targetParts = rowObj.FindChild($"Column_{x}");
				m_Board[x, y] = targetParts.GetComponent<Square>();
				m_Board[x, y].Setup(new Address(x, y), OnPressedSquare);
			}
		}

		var eventHandlers = new SystemUI.EventHandlers
		{
			OnReset = InitBoard,
			OnChangeDifficulty = m_EnemyAI.SetDeepLevel,
	};
		SystemUI.I.RegisterEvent(eventHandlers);

		InitBoard();
	}

	public void InitBoard()
	{
		for (int x = 1; x < BOARD_WIDTH - 1; x++)
		{
			for (int y = 1; y < BOARD_WIDTH - 1; y++)
			{
				m_Board[x, y].SetPieceInfo(PieceInfo.Empty);
			}
		}

		InitSelfPiece();
		InitEnemyPiece();
	}

	private void InitSelfPiece()
	{
		for (int x = 1; x < BOARD_WIDTH - 1; x++)
		{
			m_Board[x, 7].SetPieceInfo(PieceInfo.Pawn);	// 歩兵
		}

		m_Board[2, 8].SetPieceInfo(PieceInfo.Rook);		// 飛車
		m_Board[8, 8].SetPieceInfo(PieceInfo.Bishop);	// 角行
		m_Board[1, 9].SetPieceInfo(PieceInfo.Lance);	// 香車
		m_Board[2, 9].SetPieceInfo(PieceInfo.Knight);	// 桂馬
		m_Board[3, 9].SetPieceInfo(PieceInfo.Silver);	// 銀将
		m_Board[4, 9].SetPieceInfo(PieceInfo.Gold);		// 金将
		m_Board[5, 9].SetPieceInfo(PieceInfo.King);		// 王将
		m_Board[6, 9].SetPieceInfo(PieceInfo.Gold);		// 金将
		m_Board[7, 9].SetPieceInfo(PieceInfo.Silver);	// 銀将
		m_Board[8, 9].SetPieceInfo(PieceInfo.Knight);	// 桂馬
		m_Board[9, 9].SetPieceInfo(PieceInfo.Lance);	// 香車
	}

	private void InitEnemyPiece()
	{
		for (int x = 1; x < BOARD_WIDTH - 1; x++)
		{
			m_Board[x, 3].SetPieceInfo(PieceInfo.Enemy_Pawn);	// 歩兵
		}

		m_Board[8, 2].SetPieceInfo(PieceInfo.Enemy_Rook);		// 飛車
		m_Board[2, 2].SetPieceInfo(PieceInfo.Enemy_Bishop);		// 角行
		m_Board[1, 1].SetPieceInfo(PieceInfo.Enemy_Lance);		// 香車
		m_Board[2, 1].SetPieceInfo(PieceInfo.Enemy_Knight);		// 桂馬
		m_Board[3, 1].SetPieceInfo(PieceInfo.Enemy_Silver);		// 銀将
		m_Board[4, 1].SetPieceInfo(PieceInfo.Enemy_Gold);		// 金将
		m_Board[5, 1].SetPieceInfo(PieceInfo.Enemy_King);		// 王将
		m_Board[6, 1].SetPieceInfo(PieceInfo.Enemy_Gold);		// 金将
		m_Board[7, 1].SetPieceInfo(PieceInfo.Enemy_Silver);		// 銀将
		m_Board[8, 1].SetPieceInfo(PieceInfo.Enemy_Knight);		// 桂馬
		m_Board[9, 1].SetPieceInfo(PieceInfo.Enemy_Lance);		// 香車
	}

	private void OnPressedSquare(Square pressedSquare)
	{
		if (m_PieceMoveInfo.IsSelecting)
		{
			PutPiece(pressedSquare);
		}
		else
		{
			SelectPiece(pressedSquare);
		}
	}

	private void SelectPiece(Square pressedSquare)
	{
		if (pressedSquare.IsEmpty() || pressedSquare.IsEnemy())
		{
			Debug.LogError("そのマスは選択できません。");
			return;
		}

		m_PieceMoveInfo.SetMoveFrom(pressedSquare);
		pressedSquare.SetSelectingColor(isSelecting: true);
	}

	private void PutPiece(Square moveToSquare)
	{
		var moveFromSquare = m_PieceMoveInfo.SelectingSquare;

		// 同じマスを選択すればリセット
		if (m_PieceMoveInfo.IsSameAddress(moveToSquare.Address))
		{
			moveFromSquare.SetSelectingColor(isSelecting: false);
			m_PieceMoveInfo.Reset();
			return;
		}

		m_PieceMoveInfo.SetMoveTo(moveToSquare.Address);

		if (!CanPutPiece(moveToSquare, moveFromSquare))
		{
			Debug.LogError("そのマスには置けません。");
			return;
		}

		moveFromSquare.SetSelectingColor(isSelecting: false);

		if (moveToSquare.IsEnemy())
		{
			// 敵の王将を取れば勝ち
			if (IsAcquiredKing(moveToSquare.PieceInfo, Player.Self))
			{
				FinishGame(isPlayerWin: true);
				return;
			}

			// 敵の駒を獲得したら持ち駒に追加
			AcquiredPiece(moveToSquare.PieceInfo, Player.Self);
		}

		moveToSquare.SetPieceInfo(moveFromSquare.PieceInfo);

		// 成ることができるなら成る/成らないのダイアログ表示
		if (CanPiecePromote(moveFromSquare, moveToSquare.Address))
		{
			SystemUI.I.OpenYesNoDialog(
				title: string.Empty,
				body: $"Do you want to promote piece?\n Selecting piece: {moveFromSquare.PieceInfo.ToString()}",
				yesCallback: () => {
					PromotePiece(moveToSquare);
					PutEnemyPiece();
				},
				noCallback: PutEnemyPiece
			);
			return;
		}

		PutEnemyPiece();
	}

	private async void PutEnemyPiece()
	{
		m_PieceMoveInfo.SelectingSquare.ResetPieceInfo();
		m_PieceMoveInfo.Reset();

		// 敵AIの手を打つまで少し時間を置く
		await Task.Delay(500);

		var enemyHand = m_EnemyAI.ThinkEnemyAIHand(m_Board);
		var moveFromSquare = GetSquare(enemyHand.MoveFrom);
		var moveToSquare = GetSquare(enemyHand.MoveTo);

		if (moveToSquare.IsSelf())
		{
			// 敵の王将を取れば勝ち
			if (IsAcquiredKing(moveToSquare.PieceInfo, Player.Enemy))
			{
				FinishGame(isPlayerWin: false);
				return;
			}

			// 敵の駒を獲得したら持ち駒に追加
			AcquiredPiece(moveToSquare.PieceInfo, Player.Enemy);
		}

		moveToSquare.SetPieceInfo(moveFromSquare.PieceInfo);
		moveFromSquare.ResetPieceInfo();
	}

	private bool CanPutPiece(Square moveToSquare, Square moveFromSquare)
	{
		if (moveToSquare.IsSelf())
		{
			return false;
		}

		// 二歩チェック
		if (BoardUtility.IsTwoPawn(m_Board, m_PieceMoveInfo, moveToSquare.Address.X))
		{
			return false;
		}

		bool isSelectingAcquiredPiece = !moveFromSquare.Address.IsValid();
		PlayerPieceBase piece = PieceUtility.CreatePiece(moveFromSquare.PieceInfo);
		if (!isSelectingAcquiredPiece && !piece.CanMove(m_Board, m_PieceMoveInfo))
		{
			return false;
		}

		return true;
	}

	private bool CanPiecePromote(Square moveFromSquare, Address putAddress)
	{
		// 持ち駒から置く場合は成らない
		if (!moveFromSquare.Address.IsValid())
		{
			return false;
		}

		return BoardUtility.IsEnemyArea(putAddress) && PieceUtility.CanPromote(moveFromSquare.PieceInfo);
	}

	private void PromotePiece(Square targetSquare)
	{
		var currentPieceInfo = targetSquare.PieceInfo;
		targetSquare.SetPieceInfo(currentPieceInfo | PieceInfo.Promoted);
	}

	public bool IsPuttedSelfPiece(Address address) => GetSquare(address).IsSelf();
	public bool IsPuttedEnemyPiece(Address address) => GetSquare(address).IsEnemy();

	private void AcquiredPiece(PieceInfo pieceInfo, Player currentPlayerType)
	{
		if (currentPlayerType == Player.Self)
		{
			// 敵の駒を獲得したら持ち駒に追加
			m_SelfAcquiredPieces.AcquiredPiece(pieceInfo, OnPressedSquare);
		}
		else if (currentPlayerType == Player.Enemy)
		{
			m_EnemyAcquiredPieces.AcquiredPiece(pieceInfo, null);
		}
	}

	private bool IsAcquiredKing(PieceInfo pieceInfo, Player currentPlayerType)
	{
		if (currentPlayerType == Player.Self)
		{
			return pieceInfo == PieceInfo.Enemy_King;
		}
		else
		{
			return pieceInfo == PieceInfo.King;
		}
	}

	private void FinishGame(bool isPlayerWin)
	{
		ResetGame();
	}

	private void ResetGame()
	{
		m_PieceMoveInfo.Reset();
		InitBoard();
	}
}
