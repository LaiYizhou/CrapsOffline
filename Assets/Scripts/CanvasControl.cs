using UnityEngine;
using System.Collections;

public class CanvasControl : MonoBehaviour
{

    public static CanvasControl Instance;

    public HistoryPanelManager historyPanelManager;

    // Use this for initialization
    void Start ()
    {

        Instance = this;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
