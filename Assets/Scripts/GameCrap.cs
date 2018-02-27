using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum EGameStage
{
    ComeOut = 2,

    Dont_Pass_Point = 4,
    Pass_Point = 6

}

public class GameCrap : MonoBehaviour
{

    public EGameStage GameStage;

    [Header("CrapsPoint")]
    [SerializeField] private Image crapsPointImage;
    [SerializeField] private Sprite crapsPointOffSprite;
    [SerializeField] private Sprite crapsPointOnSprite;

    // Use this for initialization
    void Start () {

        //Test code
	    GameStage = EGameStage.ComeOut;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
