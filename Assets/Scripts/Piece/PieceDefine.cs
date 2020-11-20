using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceDefine
{
	public class PieceImageName
	{
		public static readonly string King = "King";
		public static readonly string Rook = "Rook";
		public static readonly string Bishop = "Bishop";
		public static readonly string Gold = "Gold";
		public static readonly string Silver = "Silver";
		public static readonly string Knight = "Knight";
		public static readonly string Lance = "Lance";
		public static readonly string Pawn = "Pawn";

		public static readonly string Pro_Rook = "Pro_Rook";
		public static readonly string Pro_Bishop = "Pro_Bishop";
		public static readonly string Pro_Silver = "Pro_Silver";
		public static readonly string Pro_Knight = "Pro_Knight";
		public static readonly string Pro_Lance = "Pro_Lance";
		public static readonly string Pro_Pawn = "Pro_Pawn";

		public static readonly string Enemy_King = "Enemy_King_sub";
		public static readonly string Enemy_Rook = "Enemy_Rook";
		public static readonly string Enemy_Bishop = "Enemy_Bishop";
		public static readonly string Enemy_Gold = "Enemy_Gold";
		public static readonly string Enemy_Silver = "Enemy_Silver";
		public static readonly string Enemy_Knight = "Enemy_Knight";
		public static readonly string Enemy_Lance = "Enemy_Lance";
		public static readonly string Enemy_Pawn = "Enemy_Pawn";

		public static readonly string Enemy_Pro_Rook = "Enemy_Pro_Rook";
		public static readonly string Enemy_Pro_Bishop = "Enemy_Pro_Bishop";
		public static readonly string Enemy_Pro_Silver = "Enemy_Pro_Silver";
		public static readonly string Enemy_Pro_Knight = "Enemy_Pro_Knight";
		public static readonly string Enemy_Pro_Lance = "Enemy_Pro_Lance";
		public static readonly string Enemy_Pro_Pawn = "Enemy_Pro_Pawn";
	}

	public static string GetPieceImageName(PieceInfo pieceInfo)
	{
		switch (pieceInfo)
		{
			case PieceInfo.King: return PieceImageName.King;
			case PieceInfo.Rook: return PieceImageName.Rook;
			case PieceInfo.Bishop: return PieceImageName.Bishop;
			case PieceInfo.Gold: return PieceImageName.Gold;
			case PieceInfo.Silver: return PieceImageName.Silver;
			case PieceInfo.Knight: return PieceImageName.Knight;
			case PieceInfo.Lance: return PieceImageName.Lance;
			case PieceInfo.Pawn: return PieceImageName.Pawn;

			case PieceInfo.Pro_Rook: return PieceImageName.Pro_Rook;
			case PieceInfo.Pro_Bishop: return PieceImageName.Pro_Bishop;
			case PieceInfo.Pro_Silver: return PieceImageName.Pro_Silver;
			case PieceInfo.Pro_Knight: return PieceImageName.Pro_Knight;
			case PieceInfo.Pro_Lance: return PieceImageName.Pro_Lance;
			case PieceInfo.Pro_Pawn: return PieceImageName.Pro_Pawn;

			case PieceInfo.Enemy_King: return PieceImageName.Enemy_King;
			case PieceInfo.Enemy_Rook: return PieceImageName.Enemy_Rook;
			case PieceInfo.Enemy_Bishop: return PieceImageName.Enemy_Bishop;
			case PieceInfo.Enemy_Gold: return PieceImageName.Enemy_Gold;
			case PieceInfo.Enemy_Silver: return PieceImageName.Enemy_Gold;
			case PieceInfo.Enemy_Knight: return PieceImageName.Enemy_Knight;
			case PieceInfo.Enemy_Lance: return PieceImageName.Enemy_Lance;
			case PieceInfo.Enemy_Pawn: return PieceImageName.Enemy_Pawn;

			case PieceInfo.Enemy_Pro_Rook: return PieceImageName.Enemy_Pro_Rook;
			case PieceInfo.Enemy_Pro_Bishop: return PieceImageName.Enemy_Pro_Bishop;
			case PieceInfo.Enemy_Pro_Silver: return PieceImageName.Enemy_Pro_Silver;
			case PieceInfo.Enemy_Pro_Knight: return PieceImageName.Enemy_Pro_Knight;
			case PieceInfo.Enemy_Pro_Lance: return PieceImageName.Enemy_Pro_Lance;
			case PieceInfo.Enemy_Pro_Pawn: return PieceImageName.Enemy_Pro_Pawn;

			default: return string.Empty;
		}
	}

	// 評価関数用の駒の価値
	public class PieceValue
	{
		public static int Empty = 0;
		public static int King = 10000;		// 王将
		public static int Rook = 2000;		// 飛車
		public static int Bishop = 1800;	// 角行
		public static int Gold = 1200;		// 金将
		public static int Silver = 1000;	// 銀将
		public static int Knight = 700;		// 桂馬
		public static int Lance = 600;		// 香車
		public static int Pawn = 100;		// 歩兵

		public static int PromotedRook = 2200;		// 竜王(成り飛車)
		public static int PromotedBishop = 2000; 	// 竜馬(成り角行)
		public static int PromotedSilver = 1190; 	// 成銀
		public static int PromotedKnight = 1180; 	// 成桂
		public static int PromotedLance = 1170;		// 成香
		public static int PromotedPawn = 1160;		// と金
	}

	public static int GetPieceValue(PieceInfo pieceInfo, bool isAcquiredPiece = false)
	{
		int pieceValue = 0;
		switch (pieceInfo)
		{
			case PieceInfo.King:
			case PieceInfo.Enemy_King:
				pieceValue += PieceValue.King; break;

			case PieceInfo.Rook:
			case PieceInfo.Enemy_Rook:
				pieceValue += PieceValue.Rook; break;

			case PieceInfo.Bishop:
			case PieceInfo.Enemy_Bishop:
				pieceValue += PieceValue.Bishop; break;

			case PieceInfo.Gold:
			case PieceInfo.Enemy_Gold:
				pieceValue += PieceValue.Gold; break;

			case PieceInfo.Silver:
			case PieceInfo.Enemy_Silver:
				pieceValue += PieceValue.Silver; break;

			case PieceInfo.Knight:
			case PieceInfo.Enemy_Knight:
			pieceValue += PieceValue.Knight; break;

			case PieceInfo.Lance:
			case PieceInfo.Enemy_Lance:
				pieceValue += PieceValue.Lance; break;

			case PieceInfo.Pawn:
			case PieceInfo.Enemy_Pawn:
				pieceValue += PieceValue.Pawn; break;

			case PieceInfo.Pro_Rook:
			case PieceInfo.Enemy_Pro_Rook:
			pieceValue += PieceValue.PromotedRook; break;
			
			case PieceInfo.Pro_Bishop:
			case PieceInfo.Enemy_Pro_Bishop:
			pieceValue += PieceValue.PromotedBishop; break;

			case PieceInfo.Pro_Silver:
			case PieceInfo.Enemy_Pro_Silver:
			pieceValue += PieceValue.PromotedSilver; break;

			case PieceInfo.Pro_Knight:
			case PieceInfo.Enemy_Pro_Knight:
			pieceValue += PieceValue.PromotedKnight; break;

			case PieceInfo.Pro_Lance:
			case PieceInfo.Enemy_Pro_Lance:
			pieceValue += PieceValue.PromotedLance; break;

			case PieceInfo.Pro_Pawn:
			case PieceInfo.Enemy_Pro_Pawn:
			pieceValue += PieceValue.PromotedPawn; break;
		}

		if (isAcquiredPiece)
		{
			pieceValue += (pieceValue * 105 / 100);
		}

		return pieceValue;
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
