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
				m_Board[x, y].SetSquareInfo(Square.SquareInfo.Empty);
			}
		}

		InitSelfPiece();
		InitEnemyPiece();
	}

	public void InitSelfPiece()
	{
		for (int x = 1; x < BOARD_WIDTH - 1; x++)
		{
			m_Board[x, 7].SetSquareInfo(Square.SquareInfo.Pawn);	// 歩兵
		}

		m_Board[2, 8].SetSquareInfo(Square.SquareInfo.Rook);		// 飛車
		m_Board[8, 8].SetSquareInfo(Square.SquareInfo.Bishop);		// 角行

		m_Board[1, 9].SetSquareInfo(Square.SquareInfo.Lance);		// 香車
		m_Board[2, 9].SetSquareInfo(Square.SquareInfo.Knight);		// 桂馬
		m_Board[3, 9].SetSquareInfo(Square.SquareInfo.Silver);		// 銀将
		m_Board[4, 9].SetSquareInfo(Square.SquareInfo.Gold);		// 金将
		m_Board[5, 9].SetSquareInfo(Square.SquareInfo.King);		// 王将
		m_Board[6, 9].SetSquareInfo(Square.SquareInfo.Gold);		// 金将
		m_Board[7, 9].SetSquareInfo(Square.SquareInfo.Silver);		// 銀将
		m_Board[8, 9].SetSquareInfo(Square.SquareInfo.Knight);		// 桂馬
		m_Board[9, 9].SetSquareInfo(Square.SquareInfo.Lance);		// 香車
	}

	public void InitEnemyPiece()
	{
		for (int x = 1; x < BOARD_WIDTH - 1; x++)
		{
			m_Board[x, 3].SetSquareInfo(Square.SquareInfo.Enemy_Pawn);	// 歩兵
		}

		m_Board[2, 2].SetSquareInfo(Square.SquareInfo.Enemy_Rook);		// 飛車
		m_Board[8, 2].SetSquareInfo(Square.SquareInfo.Enemy_Bishop);	// 角行

		m_Board[1, 1].SetSquareInfo(Square.SquareInfo.Enemy_Lance);		// 香車
		m_Board[2, 1].SetSquareInfo(Square.SquareInfo.Enemy_Knight);	// 桂馬
		m_Board[3, 1].SetSquareInfo(Square.SquareInfo.Enemy_Silver);	// 銀将
		m_Board[4, 1].SetSquareInfo(Square.SquareInfo.Enemy_Gold);		// 金将
		m_Board[5, 1].SetSquareInfo(Square.SquareInfo.Enemy_King);		// 王将
		m_Board[6, 1].SetSquareInfo(Square.SquareInfo.Enemy_Gold);		// 金将
		m_Board[7, 1].SetSquareInfo(Square.SquareInfo.Enemy_Silver);	// 銀将
		m_Board[8, 1].SetSquareInfo(Square.SquareInfo.Enemy_Knight);	// 桂馬
		m_Board[9, 1].SetSquareInfo(Square.SquareInfo.Enemy_Lance);		// 香車
	}
}
