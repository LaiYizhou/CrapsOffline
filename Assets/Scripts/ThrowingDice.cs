using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;

public class ThrowingDice : MonoBehaviour
{

    public enum EThrowingDice
    {
        Dice1,
        Dice2
    }

    [SerializeField] private EThrowingDice ThrowingDiceType;

    [SerializeField] private Image throwingImage;
    [SerializeField] private Image showImage;

    [SerializeField] private bool isThrow;
    public bool IsThrow
    {
        get
        {
            return isThrow;
        }

        set
        {
            isThrow = value;
            if (isThrow)
            {
                throwingImage.gameObject.SetActive(true);
                showImage.gameObject.SetActive(false);
            }
            else
            {
                throwingImage.gameObject.SetActive(false);
                showImage.gameObject.SetActive(true);
                CanvasControl.Instance.gameCrap.diceManager.ResetTwoDices();
            }
        }
    }

    [SerializeField] private Vector2 velocity;
    [SerializeField] private float antiFactor;

    private float f = 0.45f;

    // Use this for initialization
    void Start () {
	
	}


    public void Throw(Vector2 dir, float v)
    {

        //temp code
        if (this.ThrowingDiceType == EThrowingDice.Dice1)
        {
            StartCoroutine("DelayAddDiceState");
        }

        antiFactor = 1.0f;
        velocity = dir * v;

        IsThrow = true;
    }

    // Update is called once per frame
	void Update () {

        if (IsThrow)
	    {
	        Vector2 tempPos = new Vector2(this.GetComponent<RectTransform>().anchoredPosition.x + velocity.x * Time.deltaTime, this.GetComponent<RectTransform>().anchoredPosition.y + velocity.y * Time.deltaTime);
	        this.GetComponent<RectTransform>().anchoredPosition = tempPos;
	    }
    }

    

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Debug.Log("Dice | OnTriggerEnter2D : " + coll.tag);

        if (IsThrow)
        {
            string tag = coll.tag;

            if (tag == "TopBorder" || tag == "BottomBorder")
            {
                this.velocity = new Vector2(velocity.x * f, -velocity.y * f);
                antiFactor = antiFactor * f;
            }
            else if (tag == "RightBorder")
            {
                this.velocity = new Vector2(-velocity.x * f, velocity.y * f);
                antiFactor = antiFactor * f;
            }
            else if (tag == "LeftBorder")
            {
                if (antiFactor < 1.0f)
                    this.velocity = new Vector2(-velocity.x * f, velocity.y * f);
                antiFactor = antiFactor * f;
            }
            else if (tag == "Dice")
            {
                if (CanvasControl.Instance.gameCrap.diceManager.IsOpenDiceCollider)
                    this.velocity = new Vector2(-velocity.x * f, -velocity.y * f);
            }

            ChecktoStop();
        }

    }


    //private IEnumerator coroutine;
    private void ChecktoStop()
    {
      
        StopCoroutine("Correct");
        StartCoroutine("Correct");

        if (antiFactor < 0.1f)
        {
            DOTween.To(() => velocity, x => velocity = x, Vector2.one, 0.4f).OnComplete(() => { Stop(); });
        }
    }

    IEnumerator Correct()
    {
        yield return new WaitForSeconds(1.5f);

        if (antiFactor >= 0.1f)
        {
            Debug.Log(" ### Dice Exception And Correct it now");
            Stop();
        }
    }

    private void Stop()
    {
        StopCoroutine("Correct");

        if (this.ThrowingDiceType == EThrowingDice.Dice1)
        {
           
            showImage.sprite =
                GameHelper.Instance.GetDiceSprite(CanvasControl.Instance.gameCrap.CurrentDiceState.Number1);
        }
        else if (this.ThrowingDiceType == EThrowingDice.Dice2)
        {
            showImage.sprite =
                GameHelper.Instance.GetDiceSprite(CanvasControl.Instance.gameCrap.CurrentDiceState.Number2);
        }

        IsThrow = false;
    }

    IEnumerator DelayAddDiceState()
    {

        yield return new WaitForSecondsRealtime(0.3f);

        yield return new WaitUntil(() =>
            ! CanvasControl.Instance.gameCrap.diceManager.Dice1.isThrow
            && ! CanvasControl.Instance.gameCrap.diceManager.Dice2.isThrow);
        
        CanvasControl.Instance.gameCrap.historyPanelManager.AddDiceState(CanvasControl.Instance.gameCrap.diceManager.CurrentDiceState);

    }

}
