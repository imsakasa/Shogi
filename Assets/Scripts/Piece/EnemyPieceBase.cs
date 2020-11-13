using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyPieceBase
{
	public abstract List<Address> MoveRanges(Square[,] board, Address from);
}
