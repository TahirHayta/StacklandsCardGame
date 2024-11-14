using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class CardManager : MonoBehaviour
{
    public GameObject cardPrefab; // Reference to the Card prefab
    public Canvas uiCanvas; // Assign this in the Inspector

    // Start is called before the first frame update
    void Start()
    {
       CreateCards(); 
    }
    private void CreateCards()
    {      
        List<Card> allOfTheCards = new List<Card>();
        // Instantiate some of the cards in the stacklands game
        // A New World
        allOfTheCards.Add(CreateACardFromName("Basic Human"));
        allOfTheCards.Add(CreateACardFromName("Coin"));
        allOfTheCards.Add(CreateACardFromName("Wood"));
        allOfTheCards.Add(CreateACardFromName("Stone"));
        allOfTheCards.Add(CreateACardFromName("Rock"));
        allOfTheCards.Add(CreateACardFromName("Berry Bush"));
        allOfTheCards.Add(CreateACardFromName("Berry"));
        // Humble Beginnings

        foreach (Card card in allOfTheCards)
        {
            GameObject cardObject = Instantiate(cardPrefab, uiCanvas.transform); // Parent to canvas
            card.createPhysicalCard(cardObject);
        }
    }
    private Card CreateACardFromName(string cardName){

        if (cardName.Equals("Basic Human")){
            return new Villager("Basic Human",15,2,Villager.AttackType.Melee,5);
        }
        else if (cardName.Equals("Coin")){
            return new Coin("Coin");
        }
        else if (cardName.Equals("Wood")){
            return new Resource("Wood", 1);
        }
        else if (cardName.Equals("Stone")){
            return new Resource("Stone", 1);
        }
        else if (cardName.Equals("Rock")){
            return new NaturalStructure("Rock",0, 20, new string[] {"Stone"}, 1);
        }

        else if (cardName.Equals("Berry Bush")){
            return new NaturalStructure("Berry Bush",0, 30, new string[] {"Berry"}, 4);
        }
        else if (cardName.Equals("Berry")){
            return new Food("Berry", 2, true, 1, false, 0);
        }
        else {
            return null;
        }

    }

    /*
    allOfTheCards.Add(new Resource("Stone", 2));
        allOfTheCards.Add(new Resource("Iron Ore", 5));
        allOfTheCards.Add(new Resource("Iron Bar", 10, new Resource("Steel Bar", 20)));
        allOfTheCards.Add(new Resource("Gold Ore", 10));
        allOfTheCards.Add(new Resource("Gold Bar", 20));
        allOfTheCards.Add(new Resource("Cotton", 2));
        allOfTheCards.Add(new Resource("Wool", 3));
        allOfTheCards.Add(new Resource("Fabric", 5, new Resource("Cloth", 10)));
        allOfTheCards.Add(new Food("Apple", 2, true, 5, true, 3));
        allOfTheCards.Add(new Food("Milk", 3, true, 7, true, 1));
        allOfTheCards.Add(new Food("Carrot", 1, true, 3, true, 5));
        allOfTheCards.Add(new Food("Tomato", 1, true, 3, true, 5));
        allOfTheCards.Add(new Food("Wheat", 1, false, 0, false, 100));
        allOfTheCards.Add(new Food("Egg", 5, true, 10, true, 1));
        allOfTheCards.Add(new FriendlyMob("Chicken", 5, 1, Alive.AttackType.Melee, 2f, new List<Card>(), new Card[] {new Food("Egg", 5, true, 10, true, 1)}));
        allOfTheCards.Add(new FriendlyMob("Cow", 20, 3, Alive.AttackType.Melee, 1f, new List<Card>(), new Card[] {new Food("Milk", 3, true, 7, true, 1)}));
        allOfTheCards.Add(new FriendlyMob("Rabbit", 10, 1, Alive.AttackType.Melee, 3f, new List<Card>(), new Card[] {new Food("Rabbit Meat", 7, true("Rock", ));
    */

    // Update is called once per frame
    void Update()
    {
        
    }
}
