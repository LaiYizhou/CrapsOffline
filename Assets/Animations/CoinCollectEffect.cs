using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CoinCollectEffect : MonoBehaviour {

    public GameObject PlaceHolder0;
    public GameObject PlaceHolder1;
    public GameObject PlaceHolder2;
    public GameObject PlaceHolder3;

    public int CoinCount = 10;
    public long ToAddChips = 0;
    public GameObject CoinElement;

    private List<GameObject> CoinList;

    private IEnumerator curCoroutine;

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
        if (CoinList == null)
        {
            CoinList = new List<GameObject>();
        }

        CoinList.Clear();

        for (int i = 0; i < CoinCount; i++)
        {
            if (CoinList.Count <= i)
            {
                GameObject curCoin = Instantiate(CoinElement);
                curCoin.transform.SetParent(this.transform);
                curCoin.transform.localScale = Vector3.one;
                curCoin.transform.localPosition = Vector3.zero;
                CoinList.Add(curCoin);
            }


            float randomX = Random.Range(1, 50) - 25;
            float randomY = Random.Range(1, 50) - 25;
            Vector3 pos0 = PlaceHolder0.transform.localPosition + new Vector3(randomX, randomY, 0.0f);

            randomX *= Random.Range(5, 10);
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

        yield return new WaitForSecondsRealtime(1.5f);

          
        for (int i = 0; i < CoinList.Count; i++)
        {
            Destroy(CoinList[i].gameObject);
            yield return new WaitForSecondsRealtime(0.04f);
        }

        this.gameObject.SetActive(false);

        if (ToAddChips > 0)
        {
            GameHelper.Instance.ShowAddCoins(ToAddChips, false);
            ToAddChips = 0;
        }
        else
        {
            ToAddChips = 0;
            GameHelper.Instance.ShowAddCoins(0, false);
        }

        

        yield break;
    }

    private void SetPlaceHolder3Position()
    {
        if (CanvasControl.Instance.gameHall.gameObject.activeInHierarchy)
        {
            PlaceHolder3.GetComponent<RectTransform>().anchoredPosition = new Vector3(-143, 289, 0);
        }
        else
        {
            PlaceHolder3.GetComponent<RectTransform>().anchoredPosition = new Vector3(-569, 284, 0);
        }

    }

    private void OnDisable()
    {
        if(ToAddChips > 0)
            GameHelper.player.ChangeCoins(ToAddChips);

        if (CoinList != null)
        {
            for (int i = 0; i < CoinList.Count; i++)
            {
                Destroy(CoinList[i].gameObject);
            }
        }     
           

    }
}
