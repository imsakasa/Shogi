using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceDefine
{
	public class PieceImageName
	{
		public static readonly string KING = "King";
		public static readonly string ROOK = "Rook";
		public static readonly string Bishop = "Bishop";
		public static readonly string Gold = "Gold";
		public static readonly string Silver = "Silver";
		public static readonly string Knight = "Knight";
		public static readonly string Lance = "Lance";
		public static readonly string Pawn = "Pawn";

		public static readonly string Pro_Rook = "Pro_Rook";
		public static readonly string Pro_Bishop = "Pro_Bishop";
		public static readonly string Pro_Gold = "Pro_Gold";
		public static readonly string Pro_Silver = "Pro_Silver";
		public static readonly string Pro_Knight = "Pro_Knight";
		public static readonly string Pro_Lance = "Pro_Lance";
		public static readonly string Pro_Pawn = "Pro_Pawn";

		public static readonly string Enemy_King = "Enemy_King";
		public static readonly string Enemy_Rook = "Enemy_Rook";
		public static readonly string Enemy_Bishop = "Enemy_Bishop";
		public static readonly string Enemy_Gold = "Enemy_Gold";
		public static readonly string Enemy_Silver = "Enemy_Silver";
		public static readonly string Enemy_Knight = "Enemy_Knight";
		public static readonly string Enemy_Lance = "Enemy_Lance";
		public static readonly string Enemy_Pawn = "Enemy_Pawn";

		public static readonly string Enemy_Pro_Rook = "Enemy_Pro_Rook";
		public static readonly string Enemy_Pro_Bishop = "Enemy_Pro_Bishop";
		public static readonly string Enemy_Pro_Gold = "Enemy_Pro_Gold";
		public static readonly string Enemy_Pro_Silver = "Enemy_Pro_Silver";
		public static readonly string Enemy_Pro_Knight = "Enemy_Pro_Knight";
		public static readonly string Enemy_Pro_Lance = "Enemy_Pro_Lance";
		public static readonly string Enemy_Pro_Pawn = "Enemy_Pro_Pawn";
	}
}

[Flags]
public enum PieceInfo
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

	Promoted = 16,

	Pro_Rook = Promoted + Rook,		// 竜王(成り飛車)
	Pro_Bishop = Promoted + Bishop, // 竜馬(成り角行)
	Pro_Silver = Promoted + Silver, // 成銀
	Pro_Knight = Promoted + Knight, // 成桂
	Pro_Lance = Promoted + Lance,	// 成香
	Pro_Pawn = Promoted + Pawn,		// と金

	Enemy = 32,
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
	Enemy_Pro_Silver = Enemy + Pro_Silver,
	Enemy_Pro_Knight = Enemy + Pro_Knight,
	Enemy_Pro_Lance = Enemy + Pro_Lance,
	Enemy_Pro_Pawn = Enemy + Pro_Pawn,
}
