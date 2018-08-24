using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogMessage : MonoBehaviour
{

    [SerializeField] private Transform panel;
    [SerializeField] private Text text;

	// Use this for initialization
	void Start () {
	
	}

    public void ShowDialogMessage(string s)
    {
        text.text = s;
        panel.gameObject.SetActive(true);
        StartCoroutine("DelayReset");
    }

    // Update is called once per frame
    void Update () {
	
	}

    IEnumerator DelayReset()
    {
        yield return new WaitForSeconds(1.0f);

        ClickScreenToReset();
    }

    public void OnMaskButtonClicked()
    {
        StopCoroutine("DelayReset");
        ClickScreenToReset();
    }

    private void ClickScreenToReset()
    {
        panel.gameObject.SetActive(false);
    }
}
