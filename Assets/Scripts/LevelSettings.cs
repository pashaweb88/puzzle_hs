using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level", fileName = "Level")]
public class LevelSettings : ScriptableObject
{
    

    [SerializeField] private int blockPerLine;

    [SerializeField] private int timer;

    [SerializeField] private int moves;

   
    public int GetBlocksPerLine()
    {
        return blockPerLine;
    }
    public int GetTimer()
    {
        return timer;
    }
    public int GetMoves()
    {
        return moves;
    }
}
