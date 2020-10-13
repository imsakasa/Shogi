using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 盤面の1マスを表すクラス
/// </summary>
public class Square
{
	public Square(int x, int y)
	{
		m_Address = new Address(x, y);
		m_PieceInfo = PieceInfo.OutOfBoard;
	}

	private Address m_Address;
	private PieceInfo m_PieceInfo;

	public void SetPieceInfo(PieceInfo info) => m_PieceInfo = info;

	public bool IsSelf(PieceInfo info)
	{
		return (PieceInfo.King <= info && info <= PieceInfo.Pro_Pawn);
	}

	public bool IsEnemy(PieceInfo info)
	{
		return (info & PieceInfo.Enemy) == PieceInfo.Enemy;
	}
}
