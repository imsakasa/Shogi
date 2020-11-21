using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnPieces : MonoBehaviour
{
	[SerializeField] private Transform m_RookRoot;
	[SerializeField] private Transform m_BishopRoot;
	[SerializeField] private Transform m_GoldsRoot;
	[SerializeField] private Transform m_SilversRoot;
	[SerializeField] private Transform m_KnightsRoot;
	[SerializeField] private Transform m_LancesRoot;
	[SerializeField] private Transform m_PawnsRoot;

	public void AcquiredPiece(PieceInfo pieceInfo, Action<Square> onPressedPiece)
	{
		var parentTrans = GetParentTransform(pieceInfo);
		if (parentTrans == null)
		{
			return;
		}

		GameObject obj = (GameObject)Resources.Load("Prefabs/Square");
		GameObject instance = (GameObject)Instantiate(
				obj,
				Vector3.zero,
				Quaternion.identity,
				parentTrans);

		// TODO: Squareクラスは盤面の1つを表すクラスなので、本当はPieceクラスとかにしたい
		var square = instance.GetComponent<Square>();
		square.Setup(Address.INVALID_ADDRESS, onPressedPiece);
		square.SetPieceInfo(PieceUtility.ReversePieceInfo(pieceInfo));
	}

	private Transform GetParentTransform(PieceInfo pieceInfo)
	{
		switch (pieceInfo)
		{
			case PieceInfo.Rook:
			case PieceInfo.Pro_Rook:
			case PieceInfo.Enemy_Rook:
			case PieceInfo.Enemy_Pro_Rook:
				return m_RookRoot;

			case PieceInfo.Bishop:
			case PieceInfo.Pro_Bishop:
			case PieceInfo.Enemy_Bishop:
			case PieceInfo.Enemy_Pro_Bishop:
				return m_BishopRoot;

			case PieceInfo.Gold:
			case PieceInfo.Enemy_Gold:
				return m_GoldsRoot;

			case PieceInfo.Silver:
			case PieceInfo.Pro_Silver:
			case PieceInfo.Enemy_Silver:
			case PieceInfo.Enemy_Pro_Silver:
				return m_SilversRoot;

			case PieceInfo.Knight:
			case PieceInfo.Pro_Knight:
			case PieceInfo.Enemy_Knight:
			case PieceInfo.Enemy_Pro_Knight:
				return m_KnightsRoot;

			case PieceInfo.Lance:
			case PieceInfo.Pro_Lance:
			case PieceInfo.Enemy_Lance:
			case PieceInfo.Enemy_Pro_Lance:
				return m_LancesRoot;

			case PieceInfo.Pawn:
			case PieceInfo.Pro_Pawn:
			case PieceInfo.Enemy_Pawn:
			case PieceInfo.Enemy_Pro_Pawn:
				return m_PawnsRoot;

			default:
				return null;
		}
	}
}
