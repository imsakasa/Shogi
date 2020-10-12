using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 盤面の1マスを表すクラス
/// </summary>
public class Square
{
	[Flags]
	public enum SquareInfo
	{
		OutOfBoard = -1,
		Empty = 0,

		King = 1,		// 王将
		Rook = 2,		// 飛車
		Bishop = 3,		// 角行
		Gold = 4,		// 金将
		Silver = 5,		// 銀将
		Knight = 6,		// 桂馬
		Lance = 7,		// 香車
		Pawn = 8,		// 歩兵

		Promoted = 8,

		Pro_Rook = Promoted + Rook,		// 竜王(成り飛車)
		Pro_Bishop = Promoted + Bishop, // 竜馬(成り角行)
		Pro_Gold = Promoted + Gold,		// 成金
		Pro_Silver = Promoted + Silver, // 成銀
		Pro_Knight = Promoted + Knight, // 成桂
		Pro_Lance = Promoted + Lance,	// 成香
		Pro_Pawn = Promoted + Pawn,		// と金

		Enemy = 16,
		Enemy_King = Enemy + King,
		Enemy_Rook = Enemy + Rook,
		Enemy_Bishop = Enemy + Bishop,
		Enemy_Gold = Enemy + Gold,
		Enemy_Silver = Enemy + Silver,
		Enemy_Knight = Enemy + Knight,
		Enemy_Lance = Enemy + Lance,
		Enemy_Pawn = Enemy + Pawn,

		Enemy_Pro_Rook = Enemy + Pro_Rook,
		Enemy_Pro_Bishop = Enemy + Pro_Bishop,
		Enemy_Pro_Gold = Enemy + Pro_Gold,
		Enemy_Pro_Silver = Enemy + Pro_Silver,
		Enemy_Pro_Knight = Enemy + Pro_Knight,
		Enemy_Pro_Lance = Enemy + Pro_Lance,
		Enemy_Pro_Pawn = Enemy + Pro_Pawn,
	}

	public SquareInfo Info;

	public bool IsSelf(SquareInfo info)
	{
		return (SquareInfo.King <= info && info <= SquareInfo.Pro_Pawn);
	}

	public bool IsEnemy(SquareInfo info)
	{
		return (info & SquareInfo.Enemy) == SquareInfo.Enemy;
	}
}
