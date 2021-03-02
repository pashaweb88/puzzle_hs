using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class AlbumSceneCo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Image imageShow;
    [SerializeField] private TextMeshProUGUI noticeTextObject;
    private Texture2D[] imagesAll;
    private InitGameObject initGameObject;
    private int levelsComplete = 0;
    private int currentPage = 0;
    private bool haveNotice = false;
    private string noticeText = "LEVEL 0 NEEDED \n TO VIEW PICTURE";
    
    private void Start()
    {
        initGameObject = FindObjectOfType<InitGameObject>();

        imagesAll = initGameObject.GetImages();

        if (PlayerPrefs.HasKey("saver"))
        {
            levelsComplete = PlayerPrefs.GetInt("saver");
        }

        noticeText = "LEVEL " + (levelsComplete + 2) + " NEEDED \n TO VIEW PICTURE";
        noticeTextObject.SetText(noticeText);
        noticeTextObject.alpha = 0;
        
        RefreshInfoText();
        RenderImage();
    }

    private void RefreshInfoText()
    {
        int currPageText = currentPage + 1;
        int allPagesText = levelsComplete + 1;
        infoText.SetText(currPageText + " / " + allPagesText);
    }

    public void OnCloseButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    public void OnNextButtonClick()
    {
        if (currentPage < levelsComplete)
        {
            currentPage++;
        } else
        {
            ShowBlockUI();
        }
        RenderImage();
        RefreshInfoText();
    }

    public void OnPrevButtonClick()
    {
        if (currentPage > 0)
        {
            currentPage--;
        }
        RenderImage();
        RefreshInfoText();
    }

    private void RenderImage()
    {
        Sprite sprite = Sprite.Create(imagesAll[currentPage], new Rect(0, 0, imagesAll[currentPage].width, imagesAll[currentPage].height), new Vector2(0.5f, 0.5f));
        imageShow.sprite = sprite;
    }

    private void ShowBlockUI ()
    {
        Debug.Log("BlockUI");
        if (!haveNotice)
        {
            haveNotice = true;
            imageShow.color = new Color(0f,0f,0f,1f);
            noticeTextObject.alpha = 1;
        } else
        {
            haveNotice = false;
            imageShow.color = new Color(1f, 1f, 1f, 1f);
            noticeTextObject.alpha = 0;
        }
        
    }
}
