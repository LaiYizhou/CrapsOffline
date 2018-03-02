using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CrapsTableAreaManager : MonoBehaviour
{

    [SerializeField] private List<CrapsTableArea> crapsTableAreaList;
    [SerializeField] private List<RectTransform> comeOddsAreaTransformList;
    [SerializeField] private List<RectTransform> dontComeOddsAreaTransformList;

    public void ShowAllUIs()
    {
        for (int i = 0; i < crapsTableAreaList.Count; i++)
        {
            crapsTableAreaList[i].ShowUI();
        }
    }

    public void ResetAllUIs()
    {
        for (int i = 0; i < crapsTableAreaList.Count; i++)
        {
            crapsTableAreaList[i].ResetUI();
        }
    }

    // Use this for initialization
	void Start () {
	
        //Debug.Log(this.transform.childCount);
	    crapsTableAreaList = this.GetComponentsInChildren<CrapsTableArea>().ToList();
        Debug.Log("crapsTableAreaList.Count = " + crapsTableAreaList.Count);

	}

    public RectTransform GetComeOdds(int point)
    {
        if (point >= 4 && point <= 10 && point != 7)
            return comeOddsAreaTransformList[point - 4];
        else
            return null;
    }

    public RectTransform GetDontComeOdds(int point)
    {
        if (point >= 4 && point <= 10 && point != 7)
            return dontComeOddsAreaTransformList[point - 4];
        else
            return null;
    }

    // Update is called once per frame
	void Update () {
	
	}


}
