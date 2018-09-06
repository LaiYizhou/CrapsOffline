using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Serialization;
using Debug = UnityEngine.Debug;

public enum EScratcherType
{
    Poker
}

[Serializable]
public class ScratcherManager
{

    [XmlElement]
    public int PokerNumber { get; private set; }

    private static ScratcherManager instance;
    public static ScratcherManager Instance
    {
        get
        {
            if (instance == null)
            {
                var xmlGet = PlayerPrefs.GetString("ScratcherManager");
                if (!string.IsNullOrEmpty(xmlGet))
                {
                    try
                    {
                        instance = (ScratcherManager)SerializerManager.Deserialize(typeof(ScratcherManager), xmlGet);
                    }
                    catch (Exception exception)
                    {
                        Debug.LogError(exception.Message);
                    }
                }

                if (instance == null)
                {
                    instance = new ScratcherManager();
                    instance.Init();
                }
            }
            return instance;
        }
    }


    public int GetNumber(EScratcherType type)
    {
        switch (type)
        {
            case EScratcherType.Poker:
                return PokerNumber;
            default:
                return -1;
        }
    }

    public void NumberAddOne(EScratcherType type)
    {
        switch (type)
        {
            case EScratcherType.Poker:
                PokerNumber++;
                break;;
            default:
               break;
        }

        Save();
    }

    public void NumberMinusOne(EScratcherType type)
    {
        switch (type)
        {
            case EScratcherType.Poker:
                PokerNumber--;
                break; ;
            default:
                break;
        }

        Save();
    }

    private void Init()
    {
        PokerNumber = 0;

        Save();
    }

    //public ScratcherManager()
    //{
    //    if (PlayerPrefs.HasKey("ScratcherManager"))
    //    {
    //        var xmlGet = PlayerPrefs.GetString("ScratcherManager");
    //        if (!string.IsNullOrEmpty(xmlGet))
    //        {
    //            try
    //            {
    //                instance = (ScratcherManager)SerializerManager.Deserialize(typeof(ScratcherManager), xmlGet);
    //            }
    //            catch (Exception exception)
    //            {
    //                Debug.LogError(exception.Message);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Init();
    //    }
    //}

    private void Save()
    {
        var xmlSet = SerializerManager.Serialize(instance);
        PlayerPrefs.SetString("ScratcherManager", xmlSet);
    }

}
