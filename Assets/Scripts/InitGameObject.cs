using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGameObject : MonoBehaviour
{
    [SerializeField] Texture2D[] images;

    private void Awake()
    {
        int thisGameObjectsCount = FindObjectsOfType<InitGameObject>().Length;
        if (thisGameObjectsCount > 1)
        {
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public Texture2D[] GetImages()
    {
        return images;
    }
}
