using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override MoveInfo[] GetMoves()
    {
        // --- TODO ---
        return new MoveInfo[]
        {
        new MoveInfo(1, 1, Utils.FieldWidth),   // 대각선 위 오른쪽
        new MoveInfo(-1, 1, Utils.FieldWidth),  // 대각선 위 왼쪽
        new MoveInfo(1, -1, Utils.FieldWidth),  // 대각선 아래 오른쪽
        new MoveInfo(-1, -1, Utils.FieldWidth)  // 대각선 아래 왼쪽
        };
        // ------
    }
}