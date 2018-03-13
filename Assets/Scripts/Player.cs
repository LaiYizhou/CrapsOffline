using UnityEngine;
using System.Collections;
using UnityEditor;

public class Player
{

    private long coins;
    public long Coins
    {
        get
        {
            //return coins;

            if (PlayerPrefs.HasKey("Coins"))
            {
                string s = PlayerPrefs.GetString("Coins");
                return long.Parse(s);
            }
            else
            {
                return -1;
            }

        }
        private set
        {
            if (value >= 0)
            {
                coins = value;
                Save();

                if (GameHelper.Instance != null)
                {
                    GameHelper.Instance.UpdatePlayerCoin();
                }

                //Test
                if (GameTestHelper.Instance != null)
                    GameTestHelper.Instance.ShowPlayerCoin();
            }
            
        }
    }

    public Player()
    {
        if (Coins == -1)
            ChangeCoins(1 + GameHelper.StartCoins);

    }

    public void ResetData()
    {
        this.Coins = GameHelper.StartCoins;
    }

    public void ChangeCoins(long addNumber)
    {
        this.Coins += addNumber;
    }

    private void Save()
    {
        string s = coins.ToString();
        PlayerPrefs.SetString("Coins", s);
    }

   
}
