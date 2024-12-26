using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 프리팹들
    public GameObject TilePrefab;       // 0        1     2      3      4        5
    public GameObject[] PiecePrefabs;   // King, Queen, Bishop, Knight, Rook, Pawn 순
    public GameObject EffectPrefab;

    // 오브젝트의 parent들
    private Transform TileParent;
    private Transform PieceParent;
    private Transform EffectParent;
    
    private MovementManager movementManager;
    private UIManager uiManager;
    
    public int CurrentTurn = 1; // 현재 턴 1 - 백, 2 - 흑
    public Tile[,] Tiles = new Tile[Utils.FieldWidth, Utils.FieldHeight];   // Tile들
    public Piece[,] Pieces = new Piece[Utils.FieldWidth, Utils.FieldHeight];    // Piece들

    void Awake()
    {
        TileParent = GameObject.Find("TileParent").transform;
        PieceParent = GameObject.Find("PieceParent").transform;
        EffectParent = GameObject.Find("EffectParent").transform;
        
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        movementManager = gameObject.AddComponent<MovementManager>();
        movementManager.Initialize(this, EffectPrefab, EffectParent);
        
        InitializeBoard();
    }

    void InitializeBoard()
    {
        // 8x8로 타일들을 배치
        // TilePrefab을 TileParent의 자식으로 생성하고, 배치함
        // Tiles를 채움
        // --- TODO ---
        GameObject TileParent = new GameObject("TileParent");
        for (int i = 0; i < Utils.FieldWidth; i++)
        {
            for (int j = 0; j < Utils.FieldHeight; j++)
            {
                GameObject tile = Instantiate(TilePrefab, TileParent.transform);
                tile.transform.position = new Vector3(i * Utils.TileSize, j * Utils.TileSize, 0);
                Tile tileScript = tile.GetComponent<Tile>();
                tileScript.Set((i, j));
            }
        }
        // ------

        PlacePieces(1);
        PlacePieces(2);
    }

    void PlacePieces(int direction)
    {
        // PlacePiece를 사용하여 Piece들을 적절한 모양으로 배치
        // --- TODO ---
        int BackRow;
        int PawnRow;
        if (direction == 1) 
        {
            BackRow = 0;
            PawnRow = 1;
        }
        else
        {
            BackRow = 7;
            PawnRow = 6;
        }
  
        //킹 퀸
        PlacePiece(0, (4, BackRow), direction); 
        PlacePiece(1, (3, BackRow), direction); 

        //비숍
        PlacePiece(2, (2, BackRow), direction);
        PlacePiece(2, (5, BackRow), direction);

        //나이트
        PlacePiece(3, (1, BackRow), direction);
        PlacePiece(3, (6, BackRow), direction);

        //룩
        PlacePiece(4, (0, BackRow), direction);
        PlacePiece(4, (7, BackRow), direction);

        //폰
        for (int i = 0; i < Utils.FieldWidth; i++)
        {
            PlacePiece(5, (i, PawnRow), direction); 
        }

        


        // ------
    }

    Piece PlacePiece(int pieceType, (int, int) pos, int direction)
    {
        // Piece를 배치 후, initialize
        // PiecePrefabs의 원소를 사용하여 배치, PieceParent의 자식으로 생성
        // Pieces를 채움
        // 배치한 Piece를 리턴
        // --- TODO ---
        GameObject pieceObject = Instantiate(PiecePrefabs[pieceType], PieceParent);
        pieceObject.transform.position = new Vector3(pos.Item1 * Utils.TileSize, pos.Item2 * Utils.TileSize, 0);

        // Piece 초기화
        Piece piece = pieceObject.GetComponent<Piece>();
        piece.initialize(pos, direction);

        // Board에 말 등록
        Pieces[pos.Item1, pos.Item2] = piece;

        return piece;
        // ------
    }

    public bool IsValidMove(Piece piece, (int, int) targetPos)
    {
        return movementManager.IsValidMove(piece, targetPos);
    }

    public void ShowPossibleMoves(Piece piece)
    {
        movementManager.ShowPossibleMoves(piece);
    }

    public void ClearEffects()
    {
        movementManager.ClearEffects();
    }


    public void Move(Piece piece, (int, int) targetPos)
    {
        if (!IsValidMove(piece, targetPos)) return;

        // 해당 위치에 다른 Piece가 있다면 삭제
        // Piece를 이동시킴
        // --- TODO ---
        Piece targetPiece = Pieces[targetPos.Item1, targetPos.Item2];
        if (targetPiece != null)
        {
            Destroy(targetPiece.gameObject);
        }


        Pieces[piece.MyPos.Item1, piece.MyPos.Item2] = null;
        piece.MoveTo(targetPos); 
        Pieces[targetPos.Item1, targetPos.Item2] = piece; 

      
        ChangeTurn();
        // ------
    }

    void ChangeTurn()
    {
        // 턴을 변경하고, UI에 표시
        // --- TODO ---
        CurrentTurn = (CurrentTurn == 1) ? 2 : 1; 
        uiManager.UpdateTurn(CurrentTurn);
        // ------
    }
}
