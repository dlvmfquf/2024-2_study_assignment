using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rook.cs
public class Rook : Piece
{
    public override MoveInfo[] GetMoves()
    {
        // --- TODO ---
        return new MoveInfo[]
{
        new MoveInfo(1, 0, Utils.FieldWidth),  // 오른쪽으로 최대 거리 이동
        new MoveInfo(-1, 0, Utils.FieldWidth), // 왼쪽으로 최대 거리 이동
        new MoveInfo(0, 1, Utils.FieldHeight), // 위쪽으로 최대 거리 이동
        new MoveInfo(0, -1, Utils.FieldHeight) // 아래쪽으로 최대 거리 이동
};
        // ------
    }
}
