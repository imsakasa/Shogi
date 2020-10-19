using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMoveInfo
{
	public Address MoveFrom { get; private set; } = Address.INVALID_ADDRESS;
	public Address MoveTo { get; private set; } = Address.INVALID_ADDRESS;
	public PieceInfo PieceInfo { get; private set; } = PieceInfo.Empty;

	public bool IsSelecting => MoveFrom.IsValid();
	public bool IsPromote => (PieceInfo & PieceInfo.Promoted) == PieceInfo.Promoted;

	public void SetMoveFrom(Square square)
	{
		MoveFrom = square.Address;
		PieceInfo = square.PieceInfo;
	}
	public void SetMoveTo(Address to) => MoveTo = to;
	public bool IsSameAddress(Address to) => MoveFrom == to;

	public void Reset()
	{
		MoveFrom = Address.INVALID_ADDRESS;
		MoveTo = Address.INVALID_ADDRESS;
		PieceInfo = PieceInfo.OutOfBoard;
	}
}
