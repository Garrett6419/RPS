using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ShopManagerScript : MonoBehaviour
{
    public float money;
    public Text MoneyTXT;

    public Transform used;

    public Transform[] ShopCardSpots;

    public List<Card> deck = new List<Card>();



    void Start()
    {
        MoneyTXT.text = "$" + money.ToString();

        for (int i = 0; i < 3; i++)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];
            randCard.gameObject.SetActive(true);
            randCard.transform.position = ShopCardSpots[i].position;
            deck.Remove(randCard);
        }
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (money >= BaseCard.cardValues[ButtonRef.GetComponent<ButtonInfo>().ItemID, 25])
        {
            money -= BaseCard.cardValues[ButtonRef.GetComponent<ButtonInfo>().ItemID, 25];
            MoneyTXT.text = "$" + money.ToString();
            ButtonRef.transform.position = used.position;

        }
    }

    public void GoToBattle()
    {
        SceneManager.LoadScene("Battle Scene");
    }
}
