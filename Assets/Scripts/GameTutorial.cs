using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameTutorial : MonoBehaviour
{

    private const int TutorialMax = 5;

    [Header("Content")]
    [SerializeField] private Image contentImage;
    [SerializeField] private List<Sprite> contentSpriteList;

    [Header("Arrows")]
    [SerializeField] private Button leftButton;
    [SerializeField] private Sprite leftButtonDarkSprite;
    [SerializeField] private Sprite leftButtonNormalSprite;

    [Space(5)]
    [SerializeField] private Button rightButton;
    [SerializeField] private Sprite rightButtonDarkSprite;
    [SerializeField] private Sprite rightButtonNormalSprite;

    [Header("Points")]
    [SerializeField] private List<Image> pointImageList;
    [SerializeField] private Sprite pointSprite;
    [SerializeField] private Sprite pointOnSprite;

    [Space(10)]
    [SerializeField] private Button closeButton;

    private int currentIndex = 0;

    // Use this for initialization
    void Start ()
    {

        leftButton.onClick.AddListener(OnLeftButtonClicked);
        rightButton.onClick.AddListener(OnRightButtonClicked);
        closeButton.onClick.AddListener(OnCloseButtonClicked);

        //Test Code
        Show();

    }

    public void Show()
    {
        currentIndex = 0;
        this.gameObject.SetActive(true);
        ShowTutorialPage();
    }

    private void OnLeftButtonClicked()
    {
        currentIndex--;
        ShowTutorialPage();
    }

    private void OnRightButtonClicked()
    {
        currentIndex++;
        ShowTutorialPage();
    }

    private void OnCloseButtonClicked()
    {
        currentIndex = 0;
        this.gameObject.SetActive(false);
    }

    private void ShowTutorialPage()
    {
        if (currentIndex >= 0 && currentIndex < TutorialMax)
        {
            contentImage.sprite = contentSpriteList[currentIndex];

            leftButton.GetComponent<Image>().sprite = currentIndex == 0 ? leftButtonDarkSprite : leftButtonNormalSprite;
            leftButton.interactable = (currentIndex != 0);

            rightButton.GetComponent<Image>().sprite = currentIndex == TutorialMax-1 ? rightButtonDarkSprite : rightButtonNormalSprite;
            rightButton.interactable = (currentIndex != TutorialMax - 1);

            LightPoints();

        }
    }

    private void LightPoints()
    {
        if (currentIndex >= 0 && currentIndex < TutorialMax)
        {
            pointImageList.ForEach((image) =>
            {
                image.sprite = pointSprite;
            });

            pointImageList[currentIndex].sprite = pointOnSprite;
        }
    }
}
