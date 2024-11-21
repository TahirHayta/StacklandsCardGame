using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;


// I am making Stacklands!!

namespace Cards
{
    public class Card : MonoBehaviour
    {
        public int2 Size { get; set; } = new int2(20, 35);
        public string CardName { get; set; }
        public int CardID { get; set; } //For more efficiency, when checking cards, compare cardID
        public Vector3 Position { get; set; } = new Vector3(0, 0, 0);
        public GameObject CardObject; // Assign this in the Inspector //Change that
        public int Index { get; set; }
        public List<Card> CardStack { get; set; } = new List<Card>();// Card stack means this card and newly added all cards.
        public Card(int cardID , string cardName)
        {
            this.CardName = cardName;
            this.CardStack.Add(this);
            this.CardID = cardID;
        }
        public override string ToString(){ //Rewrite ToString to show CardName
            return CardName;}
        public override bool Equals(object obj){ //   Rewrite Equals to compare names
        if (obj is Card otherCard){
            // Compare names
            return this.CardName == otherCard.CardName;}
        return false;}
        public override int GetHashCode(){ //For equals function. Necessary somehow. 
            return CardName.GetHashCode(); }
        
        public virtual void createPhysicalCard(GameObject cardObject)
        {
            this.CardObject = cardObject;
            cardObject.transform.position = this.Position; // Use Position
            cardObject.name = this.CardName; // Use CardName
            TextMeshProUGUI cardNameText = cardObject.GetComponentInChildren<TextMeshProUGUI>();
            cardNameText.text = this.CardName; 
        
        }
        public void beDestroyed()
        {
            //TODO
        }
        
        public void addToStack(Card card) //New cards will be added upon this.CardStack. And their CardStack will be updated.
        {
            foreach (Card newComingCard in card.CardStack){
                this.CardStack.Add(newComingCard);
                newComingCard.Index = (this.CardStack.Count-1);
                newComingCard.CardStack = this.CardStack;
            }
        }
        public void leaveFromStack(Card card) // card and Cards below that card are leaving. Update this.CardStack and make a newStack for them.
        {
            int startIndex = this.CardStack.IndexOf(card);
            List<Card> newStack = new List<Card>();
            for (int i = startIndex; i < this.CardStack.Count; i++)
                {
                Card outGoingCard = this.CardStack[i];
                newStack.Add(outGoingCard);                
                outGoingCard.Index = newStack.Count-1;         
                outGoingCard.CardStack = newStack;     
                }
            this.CardStack.RemoveRange(startIndex, this.CardStack.Count- startIndex);
        }
        public void doesCombination(List<Card> CardStack ){
            foreach (Card card in CardStack)
            {
                
            }
        }




}

    public class Coin : Card
    {
        public int Quantity { get; set; } = 1;
        public Coin(int cardID ,string cardName) : base(cardID,cardName)
        {
        }
    }

    public class Alive : Card
    {
        public int Health { get; set; }
        public int Damage { get; set; }
        public AttackType attackType { get; set; }

        public enum AttackType
        {
            Melee,
            Ranged,
            Mage
        }

        public Alive(int cardID ,string cardName, int health, int damage, AttackType attackType) : base(cardID,cardName)
        {
            this.Health = health;
            this.Damage = damage;
            this.attackType = attackType;
        }
    }

    public class Villager : Alive
    {
        public int WorkCapacity { get; set; }
        public Job CurrentJob { get; set; }

        public Villager(int cardID ,string cardName, int health, int damage, AttackType attackType, int workCapacity) : base(cardID,cardName, health, damage, attackType)
        {
            this.WorkCapacity = workCapacity;
        }
    }

    public class Mob : Alive
    {
        public float Speed { get; set; }
        public List<Card> DeathOccurences { get; set; }

        public Mob(int cardID ,string cardName, int health, int damage, AttackType attackType, float speed, List<Card> deathOccurences) : base(cardID,cardName, health, damage, attackType)
        {
            this.Speed = speed;
            this.DeathOccurences = deathOccurences;
        }
    }

    public class FriendlyMob : Mob
    {
        public string[] Production { get; set; }

        public FriendlyMob(int cardID ,string cardName, int health, int damage, AttackType attackType, float speed, List<Card> deathOccurences, string[] production) : base(cardID, cardName, health, damage, attackType, speed, deathOccurences)
        {

            this.Production = production;
        }
    }

    public class UnfriendlyMob : Mob
    {
        public UnfriendlyMob(int cardID ,string cardName, int health, int damage, AttackType attackType, float speed, List<Card> deathOccurences) : base(cardID, cardName, health, damage, attackType, speed, deathOccurences)
        {
        }
    }

    public class Job : MonoBehaviour
    {
        public Resource[] TargetResources { get; set; }
        public Structure WorkLocation { get; set; }
        public bool IsVillagerNecessary { get; set; }
        public int BaseWorkRequired { get; set; }
        public string[] Result { get; set; }


        public Job(Resource[] targetResources, Structure workLocation, bool isVillagerNecessary, int baseWorkRequired, string[] result)
        {
            this.TargetResources = targetResources;
            this.WorkLocation = workLocation;
            this.IsVillagerNecessary = isVillagerNecessary;
            this.BaseWorkRequired = baseWorkRequired;
            this.Result = result;
        }
    }

    public class Sellable : Card
    {
        public int Price { get; set; }
        public int QuantityOfSame { get; set; } = 1;
        public bool DoesIncludeDifferentCards { get; set; } = false;

        public Sellable(int cardID ,string cardName, int price) : base(cardID,cardName)
        {
            this.Price = price;
        }



    }

    public class Food : Sellable
    {
        public bool IsConsumable { get; set; }
        public int HungerRestoration { get; set; }
        public bool IsPerishable { get; set; }
        public int ShelfLife { get; set; }

        public Food(int cardID ,string cardName, int price, bool isConsumable, int hungerRestoration, bool isPerishable, int shelfLife) : base(cardID,cardName, price)
        {
            this.IsConsumable = isConsumable;
            this.HungerRestoration = hungerRestoration;
            this.IsPerishable = isPerishable;
            this.ShelfLife = shelfLife;
        }
    }

    public class Resource : Sellable
    {
        public Card NextLevel { get; set; }

        public Resource(int cardID ,string cardName, int price) : base(cardID,cardName, price)
        {
        }

        public Resource(int cardID ,string cardName, int price, Card nextLevel) : base(cardID,cardName, price)
        {
            this.NextLevel = nextLevel;
        }
    }

    public class Structure : Sellable
    {
        public int WorkRequiredEach { get; set; }

        public Structure(int cardID ,string cardName, int price, int workRequiredEach) : base(cardID,cardName, price)
        {
            this.WorkRequiredEach = workRequiredEach;
        }
    }

    public class NaturalStructure : Structure
    {
        public string[] Goods { get; set; }
        public int TimesProccessable { get; set; }

        public NaturalStructure(int cardID ,string cardName, int price, int workRequiredEach, string[] goods, int timesProccessable) : base(cardID,cardName, price, workRequiredEach)
        {
            this.Price = 0; // Initialize Price to 0
            this.Goods = goods;
            this.TimesProccessable = timesProccessable;
        }
    }

    public class AutomateStructure : Structure
    {
        public Dictionary<string, string> InAndOutDictionary { get; set; }

        public AutomateStructure(int cardID ,string cardName, int price, int workRequiredEach, Dictionary<string, string> inAndOutDictionary) : base(cardID,cardName, price, workRequiredEach)
        {
            this.InAndOutDictionary = inAndOutDictionary;
        }
    }

    public class WorkPlaceStructure : Structure
    {
        public string Good { get; set; }

        public WorkPlaceStructure(int cardID ,string cardName, int price, int workRequiredEach, string good) : base(cardID,cardName, price, workRequiredEach)
        {
            this.Good = good;
        }
    }

    public class HolderStructure : Structure
    {
        public int Capacity { get; set; }
        public string[] CanHold { get; set; }

        public HolderStructure(int cardID ,string cardName, int price, int workRequiredEach, int capacity, string[] canHold) : base(cardID,cardName, price, workRequiredEach)
        {
            this.Capacity = capacity;
            this.CanHold = canHold;
        }
    }

    public class Equipment : Sellable
    {
        public enum EquipmentSlot
        {
            Head,
            Chest,
            Legs,
            Weapon,
            Shield
        }

        public EquipmentSlot Slot { get; set; }
        public int Durability { get; set; }
        public int DamageAddition { get; set; }
        public int Armor { get; set; }

        public Equipment(int cardID ,string cardName, int price, EquipmentSlot slot, int durability, int damageAddition, int armor) : base(cardID,cardName, price)
        {
            this.Slot = slot;
            this.Durability = durability;
            this.DamageAddition = damageAddition;
            this.Armor = armor;
        }
    }

    public class Idea : Card{
        public Dictionary<string, int> Ingredients { get; set; }
        public string Description { get; set; }
        public int HowManyCoins { get; set; }

        public Idea(int cardID ,string cardName, Dictionary<string, int> ingredients, string description, int howManyCoins) : base(cardID,cardName)
        {
            this.Ingredients = ingredients;
            this.Description = description;
            this.HowManyCoins = howManyCoins;
        }
    }

    public class Location : Card
    {
        public List<string> Items { get; set; }
        public List<Mob> Mobs { get; set; }
        public int DiscoveryWorkRequired { get; set; }

        public Location(int cardID ,string cardName, List<string> items, List<Mob> mobs, int discoveryWorkRequired) : base(cardID,cardName)
        {
            this.Items = items;
            this.Mobs = mobs;
            this.DiscoveryWorkRequired = discoveryWorkRequired;
        }
    }


}
