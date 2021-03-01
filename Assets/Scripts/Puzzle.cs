using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    // TODO CHECK IS MOVE FINISHING.

    [SerializeField] private  Texture2D image;
    [SerializeField] private int blocksPerLine = 4;
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GameObject gridImage;
    private GameObject[,] blocks;
    private GameObject emptyBlock;
    private bool canMove = true;
    private int shuffleMoveCounter = 100;
    // Start is called before the first frame update
    void Start()
    {
        blocks = new GameObject[blocksPerLine, blocksPerLine];
        CreatePuzzles();
    }

    private void CreatePuzzles()
    {
        Texture2D[,] imageParts = ImageSclicer.GetSlices(image, blocksPerLine);

        for (int y = 0; y < blocksPerLine; y++)
        {
            for (int x = 0; x < blocksPerLine; x++)
            {
                Vector2 pos = -Vector2.one * (blocksPerLine - 1) * .5f + new Vector2(x, y);
                Vector3 objPos = new Vector3(pos.x, pos.y, -10f);
                GameObject blockObject = Instantiate(blockPrefab, pos, Quaternion.identity);
                blockObject.transform.parent = transform; 
                blockObject.GetComponent<Block>().SetPuzzle(gameObject.GetComponent<Puzzle>());
                blockObject.GetComponent<Block>().Init(imageParts[x, y]);
                blockObject.GetComponent<Block>().coord = new Vector2Int(x, y);
                blockObject.GetComponent<Block>().targetCoord = new Vector2Int(x, y);
            
                blocks[x, y] = blockObject;
                if (y == 0 && x == blocksPerLine-1)
                {
                    blockObject.SetActive(false);
                    emptyBlock = blockObject;
                }
            }
        }

        Camera.main.orthographicSize = blocksPerLine + 0.6f;
        gridImage.transform.localScale = new Vector2(blocksPerLine+0.6f, blocksPerLine+0.6f);
        StartShuffle();
    }


    public void MoveBlock(GameObject blockToMove)
    {
        if (canMove)
        {
            
            if ((blockToMove.transform.position - emptyBlock.transform.position).sqrMagnitude == 1)
            {
                canMove = false;
                Vector2 tempPos = emptyBlock.transform.position;
                emptyBlock.transform.position = blockToMove.transform.position;
                blockToMove.GetComponent<Block>().StartMove(tempPos, .2f);

                Vector2Int moveBlockCoords = blockToMove.GetComponent<Block>().coord;
                Vector2Int emptyBlockCoords = emptyBlock.GetComponent<Block>().coord;
               
                blockToMove.GetComponent<Block>().coord = emptyBlockCoords;
                emptyBlock.GetComponent<Block>().coord = moveBlockCoords;

                blocks[moveBlockCoords.x, moveBlockCoords.y] = emptyBlock;
                blocks[emptyBlockCoords.x, emptyBlockCoords.y] = blockToMove;

            }
        } 
    }

    private void CheckGameWin()
    {
        int matchCounter = 0;

        for (int y = 0; y < blocksPerLine; y++)
        {
            for (int x = 0; x < blocksPerLine; x++)
            {
                if (blocks[x,y].GetComponent<Block>().coord == blocks[x, y].GetComponent<Block>().targetCoord)
                {
                    matchCounter++;
                }
            }
        }

        if (matchCounter == blocksPerLine*blocksPerLine)
        {
            emptyBlock.SetActive(true);
        }
    }

    public void SetCanMoveTrue()
    {
        CheckGameWin();
        canMove = true;
        
    }

    private void ShuffleMove()
    {
        Vector2Int[] offsets = { new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1)};
        int randomindex = Random.Range(0, offsets.Length);

        for (int i = 0; i < offsets.Length; i++)
        {
            Vector2Int offset = offsets[(randomindex + i) % offsets.Length];
            Vector2Int moveBlockCoord = emptyBlock.GetComponent<Block>().coord + offset;
            Vector2Int emptyBlockCoord = emptyBlock.GetComponent<Block>().coord;
            if (moveBlockCoord.x >= 0 && moveBlockCoord.x < blocksPerLine && moveBlockCoord.y >= 0 && moveBlockCoord.y < blocksPerLine)
            {
                Vector2 tempPos = emptyBlock.transform.position;
                GameObject blockToMove = blocks[moveBlockCoord.x, moveBlockCoord.y];
                blocks[moveBlockCoord.x, moveBlockCoord.y] = emptyBlock;
                blocks[emptyBlockCoord.x, emptyBlockCoord.y] = blockToMove;

                emptyBlock.GetComponent<Block>().coord = new Vector2Int(moveBlockCoord.x, moveBlockCoord.y);
                blockToMove.GetComponent<Block>().coord = new Vector2Int(emptyBlockCoord.x, emptyBlockCoord.y);
                emptyBlock.transform.position = blockToMove.transform.position;
                blockToMove.transform.position = tempPos;
                
                break;
            }
        }
    }

    private void StartShuffle()
    {
        while (shuffleMoveCounter > 0)
        {
            shuffleMoveCounter--;
            ShuffleMove();
        }
        

    }

}
