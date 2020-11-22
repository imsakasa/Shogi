using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMoveInfo
{
	public Address MoveFrom { get; private set; } = Address.INVALID_ADDRESS;
	public Address MoveTo { get; private set; } = Address.INVALID_ADDRESS;
	public PieceInfo PieceInfo { get; private set; } = PieceInfo.Empty;

	public bool IsSelecting { get; private set; }
	public bool IsMoveAcquiredPiece => IsSelecting && !MoveFrom.IsValid();

	public PieceMoveInfo(){}

	public PieceMoveInfo(Address moveFrom, Address moveTo, PieceInfo pieceInfo)
	{
		MoveFrom = moveFrom;
		MoveTo = moveTo;
		PieceInfo = pieceInfo;
		IsSelecting = true;
	}

	public void SetMoveFrom(Square square)
	{
		MoveFrom = square.Address;
		PieceInfo = square.PieceInfo;
		IsSelecting = true;
	}
	public void SetMoveTo(Address to) => MoveTo = to;
	public bool IsSameAddress(Address to) => MoveFrom == to;

	public void Reset()
	{
		MoveFrom = Address.INVALID_ADDRESS;
		MoveTo = Address.INVALID_ADDRESS;
		PieceInfo = PieceInfo.OutOfBoard;
		IsSelecting = false;
	}
}
