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
	public static readonly Color SELECTING_COLOR = new Color(1f, 0.6f, 0f);
	public static readonly Color NON_SELECTING_COLOR = Color.white;

	[SerializeField]
	private Image m_PieceImage;

	public Address Address { get; private set; }
	public PieceInfo PieceInfo { get; private set; }
	private Action<Square> m_OnPressed;

	public Square DeepCopy()
	{
		Square other = (Square)this.MemberwiseClone();
		other.Address = new Address(Address.X, Address.Y);
		other.PieceInfo = PieceInfo;
		return other;
	}

	public void Setup(Address address, Action<Square> onPressed)
	{
		Address = address;
		PieceInfo = PieceInfo.OutOfBoard;
		m_PieceImage.enabled = false;
		m_OnPressed = onPressed;
	}

	public void SetPieceInfoSimple(PieceInfo info) => PieceInfo = info;

	public void SetPieceInfo(PieceInfo info)
	{
		PieceInfo = info;
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

	public void ResetPieceInfo() => SetPieceInfo(PieceInfo.Empty);

	public bool IsSelf()
	{
		return (PieceInfo.King <= PieceInfo && PieceInfo <= PieceInfo.Pro_Pawn);
	}

	public bool IsEnemy()
	{
		return (PieceInfo & PieceInfo.Enemy) == PieceInfo.Enemy;
	}

	public bool IsEmpty() => PieceInfo <= PieceInfo.Empty;

	public void SetSelectingColor(bool isSelecting)
	{
		m_PieceImage.color = isSelecting ?
			SELECTING_COLOR : NON_SELECTING_COLOR;
	}

	public void OnPressed()
	{
		m_OnPressed.Invoke(this);
	}
}
