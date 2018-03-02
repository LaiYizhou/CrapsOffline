using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;

public class ChipsManager : MonoBehaviour
{


    [SerializeField] private List<Chip> TableChipList = new List<Chip>();
    [SerializeField] private Transform candiChipsTranforTransform;


    public void BuildCandiChips(CrapSceneInfo crapSceneInfo)
    {
        if (crapSceneInfo.CandiChipList.Count == candiChipsTranforTransform.childCount)
        {
            int count = candiChipsTranforTransform.childCount;
            for (int i = 0; i < count; i++)
            {
                Chip chip = candiChipsTranforTransform.GetChild(i).gameObject.GetComponent<Chip>();
                chip.Init(crapSceneInfo.CandiChipList[i]);

            }
        }
        else
        {
            Debug.LogError("CandiChips number error");
        }
    }

    public void BuildTableChip(Vector3 pos, Chip chip, CrapsTableArea area)
    {
        GameObject goPrefab = Resources.Load("chip") as GameObject;

        GameObject go = Instantiate(goPrefab) as GameObject;
        go.transform.SetParent(this.transform);
        go.transform.localScale = GameHelper.ChipOnDragScale;

        //go.transform.localPosition = pos;
        go.transform.localPosition = GetTableChipPos(pos);

        Chip itemChip = go.GetComponent<Chip>();
        //itemChip.Init(chip,chip.OriginalPos, area);
        itemChip.Init(chip, GetTableChipPos(chip.OriginalPos), area);

        TableChipList.Add(itemChip);
        CanvasControl.Instance.gameCrap.AddChipArea(area.AreaType);

        Debug.Log("! ! ! Use Coins : " + chip.Value);
        GameHelper.player.ChangeCoins(-1L * chip.Value);

    }


    /// <summary>
    /// convert LocalPostion (parent : candiChipsTranforTransform) to LocalPositon (parent : this.transform)
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public Vector3 GetTableChipPos(Vector3 pos)
    {

        // No Usage
        Vector3 res = this.transform.InverseTransformVector(candiChipsTranforTransform.TransformPoint(pos));
        return new Vector3(res.x, res.y, 0.0f);
    }

    public void CheckChips()
    {
        Debug.Log("Check Total Chips : " + TableChipList.Count);

        List<Chip> toBeRemovedList = new List<Chip>();

        for (int i = 0; i < TableChipList.Count; i++)
        {
            //Debug.Log("### CheckChips ["+i+"] ...");
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
