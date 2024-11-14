using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class CardManager : MonoBehaviour
{
    public Card cardPrefab; // Reference to the Card prefab
    // Start is called before the first frame update
    void Start()
    {
       CreateCards(); 
    }
    private void CreateCards()
    {
        
                
        List<Card> allOfTheCards = new List<Card>();
        
        /*
        villager, brick, coin, corpse, cotton, fabric, gold ore, gold bar, ıron ore, ıron bar, wood, wool, stick, stone, sand, rope, poop, paper, apple, milk, berry, carrot, tomato, wheat, egg, rabbit, chicken, cow, ogre, rebbit, skeleton, goblin, graveyard, old village, forest, axe, pickaxe, hammer
        */
        // Instantiate some of the cards in the stacklands game
        // A New World
        allOfTheCards.Add(new Villager("Basic Human",15,2,Villager.AttackType.Melee,5));
        allOfTheCards.Add(new Coin("Coin"));
        allOfTheCards.Add(new Resource("Wood", 1));
        allOfTheCards.Add(new NaturalStructure("Rock",0, 20, new Card[] {new Resource("Stone", 1)}, 1));
        allOfTheCards.Add(new NaturalStructure("Berry Bush",0, 30, new Card[] {new Food("Berry", 2, true, 1, false, 0)}, 4));
        // Humble Beginnings



        foreach (Card card in allOfTheCards)
        {
            card.createPhysicalCard();
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
