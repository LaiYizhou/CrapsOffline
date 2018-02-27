using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CrapsTableAreaManager : MonoBehaviour
{

    [SerializeField] private List<CrapsTableArea> crapsTableAreaList;

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
	
	// Update is called once per frame
	void Update () {
	
	}


}
