using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;

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

    public void BuildTableChip(Vector3 pos, Chip chip, CrapsTableArea area)
    {
        GameObject goPrefab = Resources.Load("chip") as GameObject;

        GameObject go = Instantiate(goPrefab) as GameObject;
        go.transform.SetParent(this.transform);
        go.transform.localScale = GameHelper.ChipOnDragScale;
        go.transform.localPosition = pos;

        Chip itemChip = go.GetComponent<Chip>();
        itemChip.Init(chip,chip.OriginalPos, area);

        TableChipList.Add(itemChip);
        CanvasControl.Instance.gameCrap.AddChipArea(area.AreaType);

        Debug.Log("! ! ! Use Coins : " + chip.Value);
        GameHelper.player.ChangeCoins(-1L * chip.Value);

    }

    public void CheckChips()
    {
        Debug.Log("Check Total Chips : " + TableChipList.Count);

        List<Chip> toBeRemovedList = new List<Chip>();

        for (int i = 0; i < TableChipList.Count; i++)
        {
            Debug.Log("### CheckChips ["+i+"] ...");
            bool isRemain = TableChipList[i].Check();
            if (!isRemain)
                toBeRemovedList.Add(TableChipList[i]);
        }

        for (int i = 0; i < toBeRemovedList.Count; i++)
        {
            if (TableChipList.Contains(toBeRemovedList[i]))
                TableChipList.Remove(toBeRemovedList[i]);
        }


    }

    public void Undo()
    {
        int count = TableChipList.Count;
        if (count > 0)
        {
            TableChipList[count-1].TakeBack();
            GameHelper.player.ChangeCoins(TableChipList[count-1].Value);
            TableChipList.RemoveAt(count-1);
        }
    }

    public void Clear()
    {
        for (int i = 0; i < TableChipList.Count; i++)
        {
            TableChipList[i].TakeBack();
            GameHelper.player.ChangeCoins(TableChipList[i].Value);
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
