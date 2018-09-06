using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokerScratcherItem : MonoBehaviour
{

    [Header("PurchasePanel")]
    [SerializeField] private Transform purchasePanelTransform;

    [Header("OwnPanel")]
    [SerializeField] private Transform ownPanelTransform;
    [SerializeField] private Text numberText;


    public void Init()
    {
        int number = ScratcherManager.Instance.GetNumber(EScratcherType.Poker);

        if (number > 0)
        {
            purchasePanelTransform.gameObject.SetActive(false);
            ownPanelTransform.gameObject.SetActive(true);
            numberText.text = number.ToString();
        }
        else
        {
            purchasePanelTransform.gameObject.SetActive(true);
            ownPanelTransform.gameObject.SetActive(false);
        }
    }

}
