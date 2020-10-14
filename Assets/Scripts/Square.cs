using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 盤面の1マスを表すクラス
/// </summary>
public class Square : MonoBehaviour
{
	[SerializeField]
	private Image m_PieceImage;

	public void Setup(int x, int y)
	{
		m_Address = new Address(x, y);
		m_PieceInfo = PieceInfo.OutOfBoard;
		m_PieceImage.enabled = false;
	}

	private Address m_Address;
	private PieceInfo m_PieceInfo;

	public void SetPieceInfo(PieceInfo info)
	{
		m_PieceInfo = info;
		if (info > PieceInfo.Empty)
		{
			var sprite = Resources.Load<Sprite>($"Textures/japanese-chess/koma/60x64/{info.ToString()}");
			m_PieceImage.sprite = sprite;
			m_PieceImage.enabled = true;
		}
		else
		{
			m_PieceImage.sprite = null;
			m_PieceImage.enabled = false;
		}
	}

	public bool IsSelf(PieceInfo info)
	{
		return (PieceInfo.King <= info && info <= PieceInfo.Pro_Pawn);
	}

	public bool IsEnemy(PieceInfo info)
	{
		return (info & PieceInfo.Enemy) == PieceInfo.Enemy;
	}
}
