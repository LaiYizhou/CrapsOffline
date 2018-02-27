using UnityEngine;
using System.Collections;

public class GameHall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnCrapPlayNowButtonClicked()
    {

    }

    public void LoadCrapScene(int levelId)
    {
        //todo
        // player coins Limit

        if (levelId > 0 && levelId <= 6)
        {
            CanvasControl.Instance.chipsManager.BuildCandiChips(GameHelper.Instance.GetCrapSceneInfo(levelId));
            this.gameObject.SetActive(false);
        }
        
    }
}
