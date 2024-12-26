using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject effectPrefab;
    private Transform effectParent;
    private List<GameObject> currentEffects = new List<GameObject>();   // 현재 effect들을 저장할 리스트
    
    public void Initialize(GameManager gameManager, GameObject effectPrefab, Transform effectParent)
    {
        this.gameManager = gameManager;
        this.effectPrefab = effectPrefab;
        this.effectParent = effectParent;
    }

    private bool TryMove(Piece piece, (int, int) targetPos, MoveInfo moveInfo)
    {
        // moveInfo의 distance만큼 direction을 이동시키며 이동이 가능한지를 체크
        // 보드에 있는지, 다른 piece에 의해 막히는지 등을 체크
        // 폰에 대한 예외 처리를 적용
        // --- TODO ---
        int dx = moveInfo.dirX;
        int dy = moveInfo.dirY;
        (int x, int y) = piece.MyPos;

        for (int i = 1; i <= moveInfo.distance; i++)
        {
            x += dx;
            y += dy;

            // 보드 범위 밖이면 이동 불가
            if (!Utils.IsInBoard((x, y))) return false;

            // 목표 위치에 다른 말이 있으면
            var targetPiece = gameManager.Pieces[x, y];
            if (targetPiece != null)
            {
                // 같은 팀이면 막힘
                if (targetPiece.PlayerDirection == piece.PlayerDirection) return false;

                // 다른 팀이라면 해당 위치가 공격 가능한 최종 위치인지 확인
                return i == moveInfo.distance && (x, y) == targetPos;
            }

            // 목표 위치에 도달했는지 확인
            if ((x, y) == targetPos) return true;
        }

        return false;
        // ------
    }

    // 체크를 제외한 상황에서 가능한 움직임인지를 검증
    private bool IsValidMoveWithoutCheck(Piece piece, (int, int) targetPos)
    {
        if (!Utils.IsInBoard(targetPos) || targetPos == piece.MyPos) return false;

        foreach (var moveInfo in piece.GetMoves())
        {
            if (TryMove(piece, targetPos, moveInfo))
                return true;
        }
        
        return false;
    }

    // 체크를 포함한 상황에서 가능한 움직임인지를 검증
    public bool IsValidMove(Piece piece, (int, int) targetPos)
    {
        if (!IsValidMoveWithoutCheck(piece, targetPos)) return false;

        // 체크 상태 검증을 위한 임시 이동
        var originalPiece = gameManager.Pieces[targetPos.Item1, targetPos.Item2];
        var originalPos = piece.MyPos;

        gameManager.Pieces[targetPos.Item1, targetPos.Item2] = piece;
        gameManager.Pieces[originalPos.Item1, originalPos.Item2] = null;
        piece.MyPos = targetPos;

        bool isValid = !IsInCheck(piece.PlayerDirection);

        // 원상 복구
        gameManager.Pieces[originalPos.Item1, originalPos.Item2] = piece;
        gameManager.Pieces[targetPos.Item1, targetPos.Item2] = originalPiece;
        piece.MyPos = originalPos;

        return isValid;
    }

    // 체크인지를 확인
    private bool IsInCheck(int playerDirection)
    {
        (int, int) kingPos = (-1, -1); // 왕의 위치
        for (int x = 0; x < Utils.FieldWidth; x++)
        {
            for (int y = 0; y < Utils.FieldHeight; y++)
            {
                var piece = gameManager.Pieces[x, y];
                if (piece is King && piece.PlayerDirection == playerDirection)
                {
                    kingPos = (x, y);
                    break;
                }
            }
            if (kingPos.Item1 != -1 && kingPos.Item2 != -1) break;
        }

        // 왕이 지금 체크 상태인지를 리턴
        // gameManager.Pieces에서 Piece들을 참조하여 움직임을 확인
        // --- TODO ---
        for (int x = 0; x < Utils.FieldWidth; x++)
        {
            for (int y = 0; y < Utils.FieldHeight; y++)
            {
                var piece = gameManager.Pieces[x, y];
                if (piece != null && piece.PlayerDirection != playerDirection)
                {
                    foreach (var moveInfo in piece.GetMoves())
                    {
                        if (TryMove(piece, kingPos, moveInfo)) return true;
                    }
                }
            }
        }

        return false; // 체크 상태가 아님
        // ------
    }

    public void ShowPossibleMoves(Piece piece)
    {
        ClearEffects();

        // 가능한 움직임을 표시
        // IsValidMove를 사용
        // effectPrefab을 effectParent의 자식으로 생성하고 위치를 적절히 설정
        // currentEffects에 effectPrefab을 추가
        // --- TODO ---
        foreach (var moveInfo in piece.GetMoves())
        {
            int dx = moveInfo.dirX;
            int dy = moveInfo.dirY;

            (int x, int y) = piece.MyPos;

            for (int i = 1; i <= moveInfo.distance; i++)
            {
                x += dx;
                y += dy;
                if (!Utils.IsInBoard((x, y))) break;

                if (IsValidMove(piece, (x, y)))
                {
                    
                    GameObject effect = Instantiate(effectPrefab, effectParent);
                    effect.transform.position = new Vector3(x, y, 0);
                    currentEffects.Add(effect);
                }

                if (gameManager.Pieces[x, y] != null) break;
            }
        }
        // ------
    }

    // 효과 비우기
    public void ClearEffects()
    {
        foreach (var effect in currentEffects)
        {
            if (effect != null) Destroy(effect);
        }
        currentEffects.Clear();
    }
}