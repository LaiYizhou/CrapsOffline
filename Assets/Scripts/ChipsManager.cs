﻿using UnityEngine;
using System.Collections.Generic;

public class ChipsManager : MonoBehaviour
{

    [SerializeField] private Transform candiChipsTranforTransform;

    [SerializeField] private List<Chip> TableAllChipList = new List<Chip>();

    [SerializeField] private List<Chip> TableCurrentChipList = new List<Chip>();

    [Header("BetMax And TableMax")]
    [SerializeField] private List<long> eAreaChipsValueList;

    public bool IsChipsCanDrag = true;

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

        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.BetDownChip);

        GameObject goPrefab = Resources.Load("chip") as GameObject;

        GameObject go = Instantiate(goPrefab) as GameObject;
        go.transform.SetParent(this.transform);
        go.transform.localScale = GameHelper.ChipOnDragScale;

        //go.transform.localPosition = pos;
        go.transform.localPosition = GetTableChipPos(pos);

        Chip itemChip = go.GetComponent<Chip>();
        itemChip.Init(chip, GetTableChipPos(chip.OriginalPos), area);

        TableAllChipList.Add(itemChip);
        TableCurrentChipList.Add(itemChip);

        eAreaChipsValueList[(int) itemChip.OnArea] += itemChip.Value;

        CanvasControl.Instance.gameCrap.AddChipArea(area.AreaType);

        
    }

    public bool IsContainLineChip()
    {
        long sum = GetEAreaChipsValue(EArea.PassLine) + GetEAreaChipsValue(EArea.DontPassV) +
                   GetEAreaChipsValue(EArea.DontPassH);
        return sum > 0;
    }

    public long GetEAreaChipsValue(EArea eArea)
    {
        int index = (int) eArea;

        if (index >= 0 && index < eAreaChipsValueList.Count)
            return eAreaChipsValueList[index];
        else
            return 0L;
    }

    public long GetAllChipsValue()
    {
        long res = 0;
        for (int i = 0; i < eAreaChipsValueList.Count; i++)
        {
            res += eAreaChipsValueList[i];
        }

        return res;
    }

    /// <summary>
    /// convert LocalPostion (parent : candiChipsTranforTransform) to LocalPositon (parent : this.transform)
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public Vector3 GetTableChipPos(Vector3 pos)
    {
        Vector3 res = this.transform.InverseTransformVector(GetDragChipWorldPos(pos));
        return new Vector3(res.x, res.y, 0.0f);
    }

    public Vector3 GetDragChipWorldPos(Vector3 pos)
    {
        return candiChipsTranforTransform.TransformPoint(pos);
    }

    public void CheckChips()
    {
        Debug.Log("Check Total Chips : " + TableAllChipList.Count);

        List<Chip> toBeRemovedList = new List<Chip>();

        for (int i = 0; i < TableAllChipList.Count; i++)
        {
            //Debug.Log("### CheckChips ["+i+"] ...");
            bool isRemain = TableAllChipList[i].Check();
            if (!isRemain)
                toBeRemovedList.Add(TableAllChipList[i]);
        }

        for (int i = 0; i < toBeRemovedList.Count; i++)
        {
            if (TableAllChipList.Contains(toBeRemovedList[i]))
            {
                TableAllChipList.Remove(toBeRemovedList[i]);

                eAreaChipsValueList[(int) toBeRemovedList[i].OnArea] -= toBeRemovedList[i].Value;

            }
           
        }

    }

    public void Undo()
    {

        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        int count = TableCurrentChipList.Count;
        if (count > 0)
        {
            TableCurrentChipList[count-1].TakeBack();
            GameHelper.player.ChangeCoins(TableCurrentChipList[count-1].Value);

            if (TableAllChipList.Contains(TableCurrentChipList[count - 1]))
            {
                TableAllChipList.Remove(TableCurrentChipList[count - 1]);

                eAreaChipsValueList[(int) TableCurrentChipList[count - 1].OnArea] -=
                    TableCurrentChipList[count - 1].Value;
            }
           

            TableCurrentChipList.RemoveAt(count-1);
        }
    }

    public void OnClearButtonClicked(bool isReturnCoin)
    {
        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.ButtonClick);

        Clear(isReturnCoin);
    }

    public void Clear(bool isReturnCoin)
    {

        for (int i = 0; i < TableCurrentChipList.Count; i++)
        {
            TableCurrentChipList[i].TakeBack();

            if (TableAllChipList.Contains(TableCurrentChipList[i]))
            {
                TableAllChipList.Remove(TableCurrentChipList[i]);

                eAreaChipsValueList[(int)TableCurrentChipList[i].OnArea] -=
                    TableCurrentChipList[i].Value;

            }
            
            if(isReturnCoin)
                GameHelper.player.ChangeCoins(TableCurrentChipList[i].Value);
        }

        TableCurrentChipList.Clear();
    }

    public void ClearAllChip()
    {
        for (int i = 0; i < TableAllChipList.Count; i++)
        {
            eAreaChipsValueList[(int)TableAllChipList[i].OnArea] -= TableAllChipList[i].Value;
            TableAllChipList[i].TakeBack();
           
        }

        TableAllChipList.Clear();
    }

    public void ClearTableCurrentChipList()
    {
        TableCurrentChipList.Clear();
    }

}
