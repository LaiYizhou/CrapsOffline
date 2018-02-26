using UnityEngine;
using System.Collections;

public class ChipsManager : MonoBehaviour {


    public void BuildChips(CrapSceneInfo crapSceneInfo)
    {
        if (crapSceneInfo.CandiChipList.Count == this.transform.childCount)
        {
            int count = this.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                Chip chip = this.transform.GetChild(i).gameObject.GetComponent<Chip>();
                chip.Init(crapSceneInfo.CandiChipList[i]);
                
            }
        }
    }

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
