using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopManagerScript : MonoBehaviour
{
    public float money;
    public Text MoneyTXT;

    public Transform[] ShopCardSpots;

    public GameObject[] Cards;

    void Start()
    {
        MoneyTXT.text = "$" + money.ToString();

        for (int i = 0; i < 3; i++)
        {
            Cards[i] = ShopCardSpots[i].transform.position;
        }
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (money >= BaseCard.cardValues[ButtonRef.GetComponent<ButtonInfo>().ItemID, 25])
        {
            money -= BaseCard.cardValues[ButtonRef.GetComponent<ButtonInfo>().ItemID, 25];
            MoneyTXT.text = "$" + money.ToString();

        }
    }
}
