using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// King.cs
public class King : Piece
{
    public override MoveInfo[] GetMoves()
    {
        // --- TODO ---
        return new MoveInfo[]
{
        new MoveInfo(0, 1, 1),    // 위로 한 칸
        new MoveInfo(0, -1, 1),   // 아래로 한 칸
        new MoveInfo(1, 0, 1),    // 오른쪽으로 한 칸
        new MoveInfo(-1, 0, 1),   // 왼쪽으로 한 칸
        new MoveInfo(1, 1, 1),    // 대각선 위 오른쪽 한 칸
        new MoveInfo(-1, 1, 1),   // 대각선 위 왼쪽 한 칸
        new MoveInfo(1, -1, 1),   // 대각선 아래 오른쪽 한 칸
        new MoveInfo(-1, -1, 1)   // 대각선 아래 왼쪽 한 칸
};
        // ------
    }
}
