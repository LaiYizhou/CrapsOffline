using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChipsManager : MonoBehaviour
{


    [SerializeField] private List<Chip> TableChipList = new List<Chip>();


    public void BuildCandiChips(CrapSceneInfo crapSceneInfo)
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

    public void BuildTableChip(Vector3 pos, Chip chip)
    {
        GameObject goPrefab = Resources.Load("chip") as GameObject;

        GameObject go = Instantiate(goPrefab) as GameObject;
        go.transform.SetParent(this.transform);
        go.transform.localScale = GameHelper.ChipOnDragScale;
        go.transform.localPosition = pos;

        Chip itemChip = go.GetComponent<Chip>();
        itemChip.Init(chip,chip.OriginalPos);

        TableChipList.Add(itemChip);

    }

    public void Undo()
    {
        int count = TableChipList.Count;
        if (count > 0)
        {
            TableChipList[count-1].TakeBack();
            TableChipList.RemoveAt(count-1);
        }
    }

    public void Clear()
    {
        for (int i = 0; i < TableChipList.Count; i++)
        {
            TableChipList[i].TakeBack();
        }

        TableChipList.Clear();
    }

// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
