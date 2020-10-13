using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
	public static readonly int BOARD_WIDTH = 11;

	// 将棋は 9 × 9 マスだが、番兵を入れるため上下左右1マスずつ追加
	Square[,] m_Board = new Square[BOARD_WIDTH, BOARD_WIDTH];

	public void InitBoard()
	{
		for (int x = 0; x < BOARD_WIDTH; x++)
		{
			for (int y = 0; y < BOARD_WIDTH; y++)
			{
				m_Board[x, y] = new Square(x, y);
			}
		}

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
		m_Board[8, 8].SetPieceInfo(PieceInfo.Bishop);		// 角行

		m_Board[1, 9].SetPieceInfo(PieceInfo.Lance);		// 香車
		m_Board[2, 9].SetPieceInfo(PieceInfo.Knight);		// 桂馬
		m_Board[3, 9].SetPieceInfo(PieceInfo.Silver);		// 銀将
		m_Board[4, 9].SetPieceInfo(PieceInfo.Gold);		// 金将
		m_Board[5, 9].SetPieceInfo(PieceInfo.King);		// 王将
		m_Board[6, 9].SetPieceInfo(PieceInfo.Gold);		// 金将
		m_Board[7, 9].SetPieceInfo(PieceInfo.Silver);		// 銀将
		m_Board[8, 9].SetPieceInfo(PieceInfo.Knight);		// 桂馬
		m_Board[9, 9].SetPieceInfo(PieceInfo.Lance);		// 香車
	}

	public void InitEnemyPiece()
	{
		for (int x = 1; x < BOARD_WIDTH - 1; x++)
		{
			m_Board[x, 3].SetPieceInfo(PieceInfo.Enemy_Pawn);	// 歩兵
		}

		m_Board[2, 2].SetPieceInfo(PieceInfo.Enemy_Rook);		// 飛車
		m_Board[8, 2].SetPieceInfo(PieceInfo.Enemy_Bishop);	// 角行

		m_Board[1, 1].SetPieceInfo(PieceInfo.Enemy_Lance);		// 香車
		m_Board[2, 1].SetPieceInfo(PieceInfo.Enemy_Knight);	// 桂馬
		m_Board[3, 1].SetPieceInfo(PieceInfo.Enemy_Silver);	// 銀将
		m_Board[4, 1].SetPieceInfo(PieceInfo.Enemy_Gold);		// 金将
		m_Board[5, 1].SetPieceInfo(PieceInfo.Enemy_King);		// 王将
		m_Board[6, 1].SetPieceInfo(PieceInfo.Enemy_Gold);		// 金将
		m_Board[7, 1].SetPieceInfo(PieceInfo.Enemy_Silver);	// 銀将
		m_Board[8, 1].SetPieceInfo(PieceInfo.Enemy_Knight);	// 桂馬
		m_Board[9, 1].SetPieceInfo(PieceInfo.Enemy_Lance);		// 香車
	}
}
