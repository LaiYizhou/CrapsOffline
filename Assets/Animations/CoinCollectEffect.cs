using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class CoinCollectEffect : MonoBehaviour {

    [SerializeField] private GameObject PlaceHolder0;
    [SerializeField] private GameObject PlaceHolder1;
    [SerializeField] private GameObject PlaceHolder2;
    [SerializeField] private GameObject PlaceHolder3;
    [SerializeField] private GameObject CoinElement;

    [Space(10)]
    private int coinCount = 10;
   
    public long ToAddChips = 0;

    private List<GameObject> CoinList;
    private IEnumerator curCoroutine;

    [Space(10)]
    [SerializeField] private Transform coinElementTransform;
    [SerializeField] private Transform hallCoinTransform;
    [SerializeField] private Transform crapsCoinTransform;

    private readonly Vector3 PlaceHolderPos0 = new Vector3(297.0f, 0.0f, 0.0f);
    private readonly Vector3 PlaceHolderPos1 = new Vector3(0.0f, 0.0f, 0.0f);
    private readonly Vector3 PlaceHolderPos2 = new Vector3(-300.0f, 130.0f, 0.0f);
    private readonly Vector3 PlaceHolderPos3 = new Vector3(-143.0f, 289.0f, 0.0f);

    public void RunEffect(long chips, Vector3 sourcePos)
    {
        this.ToAddChips += chips;

        PlaceHolder0.GetComponent<RectTransform>().anchoredPosition = sourcePos;
        PlaceHolder1.GetComponent<RectTransform>().anchoredPosition = PlaceHolderPos1;
        PlaceHolder2.GetComponent<RectTransform>().anchoredPosition = PlaceHolderPos2;
        SetPlaceHolder3Position();

        RunEffect();
    }


    public void RunEffect(long chips, Vector3 sourcePos, Vector3 targetPos)
    {
        this.ToAddChips += chips;

        PlaceHolder0.GetComponent<RectTransform>().anchoredPosition = sourcePos;
        PlaceHolder1.GetComponent<RectTransform>().anchoredPosition = PlaceHolderPos1;
        PlaceHolder2.GetComponent<RectTransform>().anchoredPosition = PlaceHolderPos2;
        PlaceHolder3.GetComponent<RectTransform>().anchoredPosition = targetPos;

        RunEffect();
    }


    public void RunEffect(long chips, Vector3 sourcePos, Vector3 midPos1, Vector3 midPos2)
    {
        this.ToAddChips += chips;

        PlaceHolder0.GetComponent<RectTransform>().anchoredPosition = sourcePos;
        PlaceHolder1.GetComponent<RectTransform>().anchoredPosition = midPos1;
        PlaceHolder2.GetComponent<RectTransform>().anchoredPosition = midPos2;
        SetPlaceHolder3Position();

        RunEffect();
    }

    public void RunEffect(long chips, Vector3 sourcePos, Vector3 midPos1, Vector3 midPos2, Vector3 targetPos)
    {
        this.ToAddChips += chips;

        PlaceHolder0.GetComponent<RectTransform>().anchoredPosition = sourcePos;
        PlaceHolder1.GetComponent<RectTransform>().anchoredPosition = midPos1;
        PlaceHolder2.GetComponent<RectTransform>().anchoredPosition = midPos2;
        PlaceHolder3.GetComponent<RectTransform>().anchoredPosition = targetPos;

        RunEffect();
    }

    public void RunEffect(long chips)
    {
        this.ToAddChips += chips;

        PlaceHolder0.GetComponent<RectTransform>().anchoredPosition = PlaceHolderPos0;
        PlaceHolder1.GetComponent<RectTransform>().anchoredPosition = PlaceHolderPos1;
        PlaceHolder2.GetComponent<RectTransform>().anchoredPosition = PlaceHolderPos2;
        SetPlaceHolder3Position();

        RunEffect();
    }

    public void TestRunEffect(long chips)
    {
        this.ToAddChips += chips;

        SetPlaceHolder3Position();

        //Debug.LogError(string.Format("Pos0 : {0}; Pos1: {1}; Pos2: {2}",
        //    PlaceHolder0.GetComponent<RectTransform>().anchoredPosition.ToString(),
        //    PlaceHolder1.GetComponent<RectTransform>().anchoredPosition.ToString(),
        //    PlaceHolder2.GetComponent<RectTransform>().anchoredPosition.ToString()
        //    ));

        RunEffect();
    }


    private void RunEffect()
    {
        this.gameObject.SetActive(true);

        if (curCoroutine != null)
        {
            StopCoroutine(curCoroutine);
            curCoroutine = null;
        }

        curCoroutine = CoinFlyAction();
        StartCoroutine(curCoroutine);
    }

    private IEnumerator CoinFlyAction()
    {

        AudioControl.Instance.PlaySound(AudioControl.EAudioClip.CollectCoins);

        if (CoinList == null)
        {
            CoinList = new List<GameObject>();
        }

        CoinList.Clear();

        for (int i = 0; i < coinCount; i++)
        {
            //if (CoinList.Count <= i)
            {
                GameObject curCoin = Instantiate(CoinElement);
                curCoin.transform.SetParent(coinElementTransform);
                curCoin.transform.localScale = Vector3.one;
                curCoin.transform.localPosition = Vector3.zero;
                CoinList.Add(curCoin);
            }


            float randomX = Random.Range(1, 50) - 25;
            float randomY = Random.Range(1, 50) - 25;
            Vector3 pos0 = PlaceHolder0.transform.localPosition + new Vector3(randomX, randomY, 0.0f);

            randomX *= Random.Range(3, 5);
            randomY = Random.Range(1, 100) - 50;
            Vector3 pos1 = PlaceHolder1.transform.localPosition + new Vector3(randomX, randomY, 0.0f);

            randomX *= Random.Range(1f, 1.2f);
            randomY = Random.Range(1, 100) - 50;
            Vector3 pos2 = PlaceHolder2.transform.localPosition + new Vector3(randomX, randomY, 0.0f);

            Vector3 pos3 = PlaceHolder3.transform.localPosition;


            CoinList[i].gameObject.SetActive(false);

            CoinList[i].transform.DOKill();
            CoinList[i].transform.localScale = Vector3.one;
            CoinList[i].transform.localPosition = pos0;

            CoinList[i].gameObject.SetActive(true);

            CoinList[i].transform.DOLocalPath(new Vector3[] {pos0, pos1, pos2, pos3}, 2.0f, PathType.CatmullRom);

            yield return new WaitForSecondsRealtime(0.01f);
        }

        yield return new WaitForSecondsRealtime(1.8f);


        for (int i = 0; i < coinElementTransform.childCount; i++)
        {
            Destroy(coinElementTransform.GetChild(i).gameObject);
        }

        yield return new WaitForEndOfFrame();
        CoinList.Clear();

        if (ToAddChips > 0)
        {
            GameHelper.Instance.ShowAddCoins(ToAddChips, false);
           
        }

        ToAddChips = 0;
        this.gameObject.SetActive(false);

        yield break;
    }

    private void SetPlaceHolder3Position()
    {

        Transform targetCoinTransform = CanvasControl.Instance.gameHall.gameObject.activeInHierarchy
            ? hallCoinTransform
            : crapsCoinTransform;

        PlaceHolder3.GetComponent<RectTransform>().anchoredPosition = GameHelper.Instance.ToCanvasLocalPos(
                targetCoinTransform.parent.TransformPoint(targetCoinTransform.localPosition));

    }

    private void OnDisable()
    {
        if (ToAddChips > 0)
        {
            GameHelper.player.ChangeCoins(ToAddChips);
            ToAddChips = 0;
        }

        for (int i = 0; i < coinElementTransform.childCount; i++)
        {
            Destroy(coinElementTransform.GetChild(i).gameObject);
        }

        CoinList.Clear();
    }
}
