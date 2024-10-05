using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ShopManagerScript : MonoBehaviour
{
    public int startingMoney;
    public Text MoneyTXT;

    public Transform used;

    public Transform[] ShopCardSpots;

    public List<Card> shopDeck = new List<Card>();



    void Start()
    {
        MoneyTXT.text = "$" + MainManager.Instance.Money.ToString();

        for (int i = 0; i < 3; i++)
        {
            Card randCard = shopDeck[Random.Range(0, shopDeck.Count)];
            randCard.gameObject.SetActive(true);
            randCard.transform.position = ShopCardSpots[i].position;
            shopDeck.Remove(randCard);
        }
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (MainManager.Instance.Money >= BaseCard.cardValues[ButtonRef.GetComponent<ButtonInfo>().ItemID, 25])
        {
            MainManager.Instance.Money -= BaseCard.cardValues[ButtonRef.GetComponent<ButtonInfo>().ItemID, 25];
            MoneyTXT.text = "$" + MainManager.Instance.Money.ToString();
            ButtonRef.transform.position = used.position;

        }
    }

    public void GoToBattle()
    {
        SceneManager.LoadScene("Battle Scene");
    }
}
