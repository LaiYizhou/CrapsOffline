using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PokersManager : MonoBehaviour
{
    private Poker targetPoker;
    [SerializeField] private List<Vector2> pokerPosList;
    [SerializeField] private List<PokerItem> pokerItemList;
    [SerializeField] private List<Poker> pokerList;

    public void Init(Poker poker)
    {
        Reset();

        targetPoker = poker;
        pokerList = GameHelper.Instance.GetScratchPokers(targetPoker);

        StartCoroutine(DelayShowPokers());

        StartCoroutine(DelayPokerShine());

    }

    public void Reset()
    {
        pokerItemList.Clear();
        pokerList.Clear();

        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }

    }

    public void ScratchAll()
    {
        //AudioControl.Instance.PlaySound(AudioControl.EAudioClip.Scratch);

        for (int i = 0; i < pokerItemList.Count; i++)
        {
            if (!pokerItemList[i].IsShow)
            {
                pokerItemList[i].OnPokerButtonClicked();
            }
        }
    }

    IEnumerator DelayShowPokers()
    {

        yield return new WaitForEndOfFrame();

        for (int i = 0; i < pokerList.Count; i++)
        {
            GameObject goPrefab = Resources.Load("PokerItem") as GameObject;
            GameObject go = Instantiate(goPrefab) as GameObject;

            go.transform.SetParent(this.transform);
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one * 0.1f;
            go.transform.localPosition = new Vector3(25.0f, 190.0f, 0.0f);

            PokerItem pokerItem = go.GetComponent<PokerItem>();
            pokerItemList.Add(pokerItem);
            pokerItem.Init(pokerList[i]);

            AudioControl.Instance.PlaySound(AudioControl.EAudioClip.Deal);

            Sequence sequence = DOTween.Sequence();

            sequence.Insert(0.0f, go.transform.DOScale(Vector3.one, 0.3f));
            sequence.Insert(0.0f, go.transform.DOLocalMove(pokerPosList[i], 0.3f));

            //yield return new WaitForSeconds(0.05f);
            yield return new WaitForSeconds(0.2f);

        }
    }

    IEnumerator DelayPokerShine()
    {
        yield return new WaitForSeconds(1.5f);

        if (pokerItemList.TrueForAll((go) => go.IsShow))
        {
            yield break;
        }

        int index = Random.Range(0, pokerItemList.Count);
        while (pokerItemList[index].IsShow)
        {
            index = Random.Range(0, pokerItemList.Count);
        }

        pokerItemList[index].Shine();

        float f = Random.Range(0.0f, 2.0f);
        yield return new WaitForSeconds(f);

        StartCoroutine(DelayPokerShine());
    }
}
