using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using System.Linq;

namespace CardManagerAndNecessaryClasses{

public class CardManager : MonoBehaviour
{
    public GameObject CardPrefab; // Reference to the Card prefab
    public Canvas UICanvas; // Assign this in the Inspector

    public LinkedList<Recipy> Recipies { get; set; } = {new Recipy({"Berry":1, "Soil":1}, "Berry Bush")};    

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
        allOfTheCards.Add(CreateACardFromName("Soil"));

        // Humble Beginnings

        foreach (Card card in allOfTheCards)
        {
            GameObject cardObject = Instantiate(CardPrefab, UICanvas.transform); // Parent to canvas
            card.createPhysicalCard(cardObject);
        }
    }

    private static Card CreateACardFromName(string cardName){
        switch (cardName)
        {
            case "Basic Human":return new Villager(CardNameToID("Basic Human"),"Basic Human",15,2,Villager.AttackType.Melee,5);
            case "Coin":return new Coin(CardNameToID("Coin"),"Coin");
            case "Wood":return new Resource(CardNameToID("Wood"),"Wood", 1);
            case "Stone":return new Resource(CardNameToID("Stone"),"Stone", 1);
            case "Rock":return new NaturalStructure(CardNameToID("Rock"),"Rock",0, 20, new string[] {"Stone"}, 1);
            case "Berry Bush":return new NaturalStructure(CardNameToID("Berry Bush"),"Berry Bush",0, 30, new string[] {"Berry"}, 4);
            case "Berry":return new Food(CardNameToID("Berry"),"Berry", 2, true, 1, false, 0);
            case "Soil":return new Resource(CardNameToID("Soil"),"Soil" ,3);
            default: return null;
        //Contunie from 7
    }}


    // Helper function to convert card ID to name
    public static string IDtoCardName(int cardID){
        switch (cardID)
        {
            case 1: return "Coin";
            case 2: return "Wood";
            case 3: return "Stone";
            case 4: return "Berry";
            case 5: return "Rock";
            case 6: return "Berry Bush";
            case 7: return "Basic Human";
            case 8: return "Soil";
            default: return null;
        }
    }
    


    // Helper function to convert card name to ID (you'll need to implement this)
    public static int CardNameToID(string cardName)
    {
        // Reverse lookup logic (you need to implement this based on your card data)
        switch(cardName) {
            case "Coin": return 1;
            case "Wood": return 2;
            case "Stone": return 3;
            case "Berry": return 4;
            case "Rock": return 5;  // Corrected ID for Rock
            case "Berry Bush": return 6;
            case "Basic Human": return 7;
            case "Soil": return 8;
            default: return null; // Return null for unknown names
        }

    }


    /*
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

public struct Recipy
{
    public int SumOfIDS { get; set; } = 0;// just to make search easy.
    public Dictionary<int, int> IDandQuantity { get; set; } // Which  card (ID) and how many times it is used
    public string ResultCardName { get; set; }
    public string Explanation;

    public Recipy(Dictionary<string, int> cardNameAndQuantity, string resultCardName)
    {
        this.ResultCardName = resultCardName;
        this.IDandQuantity = new Dictionary<int, int>();  // Initialize
        StringBuilder exp = new StringBuilder();

        foreach (KeyValuePair<string, int> kvp in cardNameAndQuantity)
        {
            int cardID = CardNameToID(kvp.Key);
            this.SumOfIDS+ = cardID;
            this.IDandQuantity.Add(cardID.Value, kvp.Value);
            exp.AppendFormat("{0} x {1}\n", kvp.Value, kvp.Key);
            
        }
        exp.AppendFormat("===>> {0}", this.ResultCardName);
        this.Explanation = exp.ToString();
        }
    public override string ToString() => this.Explanation;
}


}