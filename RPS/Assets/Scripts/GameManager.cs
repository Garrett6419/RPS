using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Cams
    public Camera cam1;
    public Camera cam2;

    // PLAYER VARIABLES
    public List<Card> deck = new List<Card>();
    public int deckSize;
    public Transform[] cardSlots;
    public Transform playerDeckTran;
    public int playerHandSize;
    public List<Card> Hand = new List<Card>();
    public int battleHearts = 3;
    public int gameHearts = 3;
    public List<Card> battleHeartsList = new List<Card>();
    public List<Card> gameHeartsList = new List<Card>();

    // ENEMY VARIABLES
    public List<Card> enemyDeck = new List<Card>();
    public int enemyDeckSize;
    public Transform[] enemyCardSlots;
    public Transform enemyDeckTran;
    public int enemyHandSize;
    public List<Card> enemyHand = new List<Card>();
    public int enemyHearts = 3;
    public List<Card> enemyHeartsList = new List<Card>();

    // GAME VARIABLES
    public int gameRound;
    public bool isBossRound = false;
    public int cardSpeed = 1;

    // Shop Variables
    public int Money = 10;
    public Text moneyText;
    public Text ShopMoneyText;
    public Text roundText;
    public List<Card> shopCards = new List<Card>();
    public List<Card> shopHand = new List<Card>();
    public Transform[] shopCardSlots;
    public Transform used;
    public bool  isShopOpen = false;
    public List<Text> shopCardPrices = new List<Text>();


    public void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
        StartCoroutine(GetHand());
        moneyText.text = "Money: $" + Money;
        roundText.text = "Round: " + gameRound;
    }

    // Function to draw a random card
    private IEnumerator GetHand()
    {
        if (Hand == null || enemyHand == null)
        {
            yield break;
        }
        StartCoroutine(GetPlayerHand());
        StartCoroutine(GetEnemyHand());
        yield return new WaitUntil(() => Hand.Count == playerHandSize && enemyHand.Count == enemyHandSize);
        // Add a small delay to ensure the cards are fully moved to their slots
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator GetPlayerHand()
    {
        for (int i = 0; i < playerHandSize; i++)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];
            randCard.gameObject.SetActive(true);
            randCard.transform.position = playerDeckTran.position;
            yield return MoveCardToSlot(randCard, cardSlots[i].position, cardSpeed);
            deck.Remove(randCard);
            Hand.Add(randCard);
        }
    }

    private IEnumerator GetEnemyHand()
    {
        for (int i = 0; i < enemyHandSize; i++)
        {
            Card randCard = enemyDeck[Random.Range(0, enemyDeckSize)];
            randCard.gameObject.SetActive(true);
            randCard.transform.position = enemyDeckTran.position;
            yield return MoveCardToEnemySlot(randCard, enemyCardSlots[i].position, cardSpeed);
            enemyDeck.Remove(randCard);
            enemyDeckSize--;
            enemyHand.Add(randCard);
        }
    }

    private IEnumerator MoveCardToSlot(Card card, Vector2 destination, float speed)
    {
        while (Vector2.Distance(card.transform.position, destination) > 0.01f)
        {
            card.transform.position = Vector2.MoveTowards(card.transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        card.transform.position = destination;
    }

    private IEnumerator MoveCardToEnemySlot(Card card, Vector2 destination, float speed)
    {
        while (Vector2.Distance(card.transform.position, destination) > 0.01f)
        {
            card.transform.position = Vector2.MoveTowards(card.transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        card.transform.position = destination;
    }

    public void battle(Card playerCard)
    {
        int enemyCardType = enemyPickCard();
        int winner = playerCard.getWinner(enemyCardType);
        if (winner == 0)
        {
            resetBoard();
            StartCoroutine(GetHand()) ;
        }
        else if (winner == 2)
        {
            int exp = Random.Range(0,10)%2;
            if(exp == 1) {
                winner = 1 ;
            }
            else {
                winner = 2 ;
            }
        }
        if (winner == 1)
        {
            dealDamage();
            resetBoard();
            StartCoroutine(GetHand()) ;
            
        }
        else if (winner == -1)
        {
            takeDamage();
            resetBoard();
            StartCoroutine(GetHand()) ;
            
        }
    }

    public void takeDamage()
    {
        battleHeartsList[battleHearts - 1].gameObject.SetActive(false);
        battleHearts--;

        if (battleHearts == 0)
        {
            gameHeartsList[gameHearts - 1].gameObject.SetActive(false);
            gameHearts--;
            if (gameHearts < 1)
            {
                gameOver();
            }
            battleHearts = 3;
            for (int i = 0; i < 3; i++)
            {
                battleHeartsList[i].gameObject.SetActive(true);
            }
        }
    }

    public void dealDamage()
    {
        enemyHeartsList[enemyHearts - 1].gameObject.SetActive(false);
        enemyHearts--;

        if (enemyHearts < 1)
        {
            
            increaseGameRound();
            
        }
        if (isBossRound && enemyHearts == 1)
        {
            refreshEnemy();
            isBossRound = false;
        }
    }

    public void resetBoard()
    {
        StartCoroutine(ResetBoardCoroutine());
    }

    private IEnumerator ResetBoardCoroutine()
    {
        StartCoroutine(ResetPlayerHand());
        StartCoroutine(ResetEnemyHand());
        yield return new WaitUntil(() => Hand.Count == 0 && enemyHand.Count == 0);
        yield return new WaitForEndOfFrame(); // wait for the frame to finish
   
    }



    private IEnumerator ResetEnemyHand()
    {
        for (int i = enemyHand.Count - 1; i >= 0; i--)
        {
            Card card = enemyHand[i];
            yield return MoveCardToEnemyDeck(card, enemyDeckTran.position, cardSpeed);
            card.gameObject.SetActive(false);
            enemyDeck.Insert(0, card);
            enemyDeckSize++ ;
            enemyHand.RemoveAt(i);
        }
    }

    private IEnumerator ResetPlayerHand()
    {
        for (int i = Hand.Count - 1; i >= 0; i--)
        {
            Card card = Hand[i];
            yield return MoveCardToDeck(card, playerDeckTran.position, cardSpeed);
            card.gameObject.SetActive(false);
            deck.Add(card);
            deckSize++;
            Hand.RemoveAt(i);
        }
    }

    private IEnumerator MoveCardToDeck(Card card, Vector2 destination, float speed)
    {
        while (Vector2.Distance(card.transform.position, destination) > 0.01f)
        {
            card.transform.position = Vector2.MoveTowards(card.transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        card.transform.position = destination;
    }

    private IEnumerator MoveCardToEnemyDeck(Card card, Vector2 destination, float speed)
    {
        while (Vector2.Distance(card.transform.position, destination) > 0.01f)
        {
            card.transform.position = Vector2.MoveTowards(card.transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        card.transform.position = destination;
    }

    public int enemyPickCard()
    {
        int chosenCard = enemyHand[Random.Range(0, enemyHandSize)].cardType;
        int[] enemyCardUtility = new int[enemyHandSize];
        int min = 100;
        int max = -100;
        for (int i = 0; i < enemyHandSize; i++)
        {
            enemyCardUtility[i] = 0;
            for (int j = 0; j < Hand.Count; j++)
            {
                enemyCardUtility[i] -= Hand[j].getWinner(enemyHand[i].cardType);
            }
            if (enemyCardUtility[i] < min)
            {
                min = enemyHand[i].cardType;
            }
            if (enemyCardUtility[i] > max)
            {
                max = enemyHand[i].cardType;
            }
        }

        if (gameRound < 3)
        {
            return min;
        }
        else if (gameRound < 6)
        {
            return chosenCard;
        }
        else if (gameRound >= 6)
        {
            return max;
        }
        return 0;
    }

    public void playerWins()
    {
        Debug.Log("Player wins!");
    }

    public void gameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene("Main Menu");
    }

    public void increaseGameRound()
    {
        gameRound++;
        updateEnemyDeck();
        refreshEnemy();
        refreshPlayer();
        if (gameRound >= 9)
        {
            isBossRound = true;
        }
        if (gameRound >= 10)
        {
            playerWins();
        }
        Money += 5;
        StartCoroutine(HideAllCards());
    }

    public void refreshEnemy()
    {
        enemyHearts = 3;
        for (int i = 0; i < enemyHearts; i++)
        {
            enemyHeartsList[i].gameObject.SetActive(true);
        }
    }

    public void refreshPlayer()
    {
        battleHearts = 3;
        for (int i = 0; i < battleHearts; i++)
        {
            battleHeartsList[i].gameObject.SetActive(true);
        }
    }

    public void updateEnemyDeck()
    {
        for (int i = 0; i < 3; i++)
        {
            int newCardPos = enemyDeckSize;
            int randCard = Random.Range(newCardPos, enemyDeck.Count);
            Card temp = enemyDeck[randCard];
            enemyDeck.RemoveAt(randCard);
            enemyDeck.Insert(0, temp);
            enemyDeckSize++;
        }
    }

    public void changeScene()
    {

        for (int i = 0; i < shopHand.Count; i++)
        {
            shopHand[i].transform.position = used.position;
            shopHand.RemoveAt(0);
        }
        cam1.enabled = !cam1.enabled;
        cam2.enabled = !cam2.enabled;
        moneyText.text = "Money: $" + Money;
        if (isBossRound)
        {
            roundText.text = "Round: Boss Round!";
        }
        else
        {
            roundText.text = "Round: " + gameRound;
        }

        

        

    }

    public void initShop()
    {
        for (int i = 0; i < 3; i++)
        {
            Card randCard = shopCards[Random.Range(0, shopCards.Count)];
            shopCardPrices[i].text = "$" + BaseCard.cardValues[randCard.cardType, 25];
            shopHand.Add(randCard);
            randCard.gameObject.SetActive(true);
            randCard.transform.position = shopCardSlots[i].transform.position;
        }

        ShopMoneyText.text = "$" + Money;
    }

    public void buyCard(Card boughtCard)
    {
        if (Money >= BaseCard.cardValues[boughtCard.cardType, 25])
        {
            boughtCard.isShopCard = false;
            Money -= BaseCard.cardValues[boughtCard.cardType, 25];
            ShopMoneyText.text = "$" + Money;
            deck.Add(boughtCard);
            int index = shopHand.IndexOf(boughtCard);
            shopCardPrices[index].text = "";
            shopCards.Remove(boughtCard);
            boughtCard.transform.position = used.position;
        }
    }

    private IEnumerator HideAllCards()
    {

        foreach (Card card in Hand)
        {
            card.gameObject.SetActive(false);
        }
        foreach (Card card in enemyHand)
        {
            card.gameObject.SetActive(false);
        }
        yield return new WaitForEndOfFrame(); // wait for the frame to finish
        OnAllCardsHidden(); // call the callback
    }

    private void OnAllCardsHidden()
    {
        // this method will be called when all cards are hidden
        changeScene();
        initShop();
    }

    public void goToBattleScene()
    {
        SceneManager.LoadScene("Battle Scene");
    }

}