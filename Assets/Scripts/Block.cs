using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Puzzle puzzle;

    public Vector2Int coord;
    public Vector2Int targetCoord;
    
    private void OnMouseDown()
    {
        
        puzzle.MoveBlock(gameObject);
    }
    public void Init(Texture2D image)
    {
        //GetComponent<MeshRenderer>().material.shader = Resources.Load<Material>("Block");
        //GetComponent<MeshRenderer>().material.mainTexture = image;
        Sprite sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
        GetComponent<SpriteRenderer>().sprite = sprite;
        GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Sliced;
        GetComponent<SpriteRenderer>().size = new Vector2(1f,1f);
    }
    public void SetPuzzle(Puzzle puzz)
    {
        puzzle = puzz;
    }
    public void StartMove(Vector2 target, float duration)
    {
        StartCoroutine(AnimateMove(target, duration));
    }
    IEnumerator AnimateMove(Vector2 target, float duration)
    {
        Vector2 initPos = transform.position;
        float percent = 0;

        while(percent < 1)
        {
            percent += Time.deltaTime / duration;
            transform.position = Vector2.Lerp(initPos, target, percent);
            yield return null;
        }
        puzzle.SetCanMoveTrue();
    }
}
