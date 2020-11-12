using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
	public static readonly int BOARD_WIDTH = 11;

	private EnemyAI m_EnemyAI = new EnemyAI();

	[SerializeField]
	private OwnPieces m_SelfAcquiredPieces;
	[SerializeField]
	private OwnPieces m_EnemyAcquiredPieces;

	// 将棋は 9 × 9 マスだが、番兵を入れるため上下左右1マスずつ追加
	// TODO: 番兵をまだ活用できてないので後ほど対応を検討
	private Square[,] m_Board = new Square[BOARD_WIDTH, BOARD_WIDTH];
	public Square GetSquare(Address address)
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

	public void InitSelfPiece()
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

	public void InitEnemyPiece()
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
			bool isNextEnemyTurn = false;
			PutPiece(pressedSquare, out isNextEnemyTurn);
			if (isNextEnemyTurn)
			{
				// TODO: 敵の手
				// await Task.Delay(500);
				// m_EnemyAI.PutPiece(m_Board);
			}
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

	private void PutPiece(Square moveToSquare, out bool isNextEnemyTurn)
	{
		var moveFromSquare = m_PieceMoveInfo.SelectingSquare;

		// 同じマスを選択すればリセット
		if (m_PieceMoveInfo.IsSameAddress(moveToSquare.Address))
		{
			moveFromSquare.SetSelectingColor(isSelecting: false);
			m_PieceMoveInfo.Reset();
			isNextEnemyTurn = false;
			return;
		}

		m_PieceMoveInfo.SetMoveTo(moveToSquare.Address);

		if (!CanPutPiece(moveToSquare, moveFromSquare))
		{
			Debug.LogError("そのマスには置けません。");
			isNextEnemyTurn = false;
			return;
		}

		moveFromSquare.SetSelectingColor(isSelecting: false);

		if (moveToSquare.IsEnemy())
		{
			// 敵の王将を取れば勝ち
			if (IsAcquiredEnemyKing(moveToSquare.PieceInfo))
			{
				FinishGame(isWin: true);
				isNextEnemyTurn = false;
				return;
			}

			// 敵の駒を獲得したら持ち駒に追加
			AcquiredEnemyPiece(moveToSquare);
		}

		moveToSquare.SetPieceInfo(moveFromSquare.PieceInfo);

		// 成ることができるなら成る/成らないのダイアログ表示
		if (CanPiecePromote(moveToSquare.Address, moveFromSquare.PieceInfo))
		{
			SystemUI.I.OpenYesNoDialog(
				string.Empty,
				$"Do you want to promote piece?\n Selecting piece: {moveFromSquare.PieceInfo.ToString()}",
				() => PromotePiece(moveToSquare));
		}

		moveFromSquare.ResetPieceInfo();
		m_PieceMoveInfo.Reset();

		isNextEnemyTurn = true;
	}

	private bool CanPutPiece(Square pressedSquare, Square selectingSquare)
	{
		if (pressedSquare.IsSelf())
		{
			return false;
		}

		// 二歩チェック
		if (IsTwoPawn(m_PieceMoveInfo, pressedSquare.Address.X))
		{
			return false;
		}

		PieceBase piece = PieceUtility.CreatePiece(selectingSquare.PieceInfo);
		if (!piece.CanMove(this, m_PieceMoveInfo))
		{
			return false;
		}

		return true;
	}

	private bool IsTwoPawn(PieceMoveInfo pieceMoveInfo, int x)
	{
		if (!pieceMoveInfo.IsAcquiredPiece)
		{
			return false;
		}

		if (pieceMoveInfo.PieceInfo != PieceInfo.Pawn)
		{
			return false;
		}

		var ranges = BoardUtility.VerticalRanges(x);
		return ranges.Any(address => GetSquare(address).PieceInfo == PieceInfo.Pawn);
	}

	private bool CanPiecePromote(Address putAddress, PieceInfo pieceInfo)
	{
		return BoardUtility.IsEnemyArea(putAddress) && PieceUtility.CanPromote(pieceInfo);
	}

	private void PromotePiece(Square targetSquare)
	{
		var currentPieceInfo = targetSquare.PieceInfo;
		targetSquare.SetPieceInfo(currentPieceInfo | PieceInfo.Promoted);
	}

	public bool IsPuttedSelfPiece(Address address)
	{
		return GetSquare(address).IsSelf();
	}

	public bool IsPuttedEnemyPiece(Address address)
	{
		return GetSquare(address).IsEnemy();
	}

	private void AcquiredEnemyPiece(Square pressedSquare)
	{
		// 敵の駒を獲得したら持ち駒に追加
		m_SelfAcquiredPieces.AcquiredPiece(pressedSquare.PieceInfo, OnPressedSquare);
	}

	private bool IsAcquiredEnemyKing(PieceInfo pieceInfo)
	{
		return pieceInfo == PieceInfo.Enemy_King;
	}

	private void FinishGame(bool isWin)
	{
		ResetGame();
	}

	private void ResetGame()
	{
		m_PieceMoveInfo.Reset();
		InitBoard();
	}
}
