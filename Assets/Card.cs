using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;


// I am making Stacklands!!

namespace Cards
{
    public class Card : MonoBehaviour
    {
        public int2 Size { get; set; } = new int2(30, 50);
        public string CardName { get; set; }
        public Vector3 Position { get; set; } = new Vector3(0, 0, 0);
        public GameObject CardPrefab; // Assign this in the Inspector //Change that

        public Card(string cardName)
        {
            this.CardName = cardName;
        }

        public virtual GameObject createPhysicalCard()
        {
            // Instantiate the physical card object in the game world
            GameObject cardObject = Instantiate(CardPrefab);  // Use CardPrefab
            // Optionally, set properties like position, rotation, or scale
            cardObject.transform.position = this.Position; // Use Position
                                                           // Optionally, add components or set attributes unique to this card
            cardObject.name = this.CardName; // Use CardName
            return cardObject;
        }
        public void beDestroyed()
        {
            //TODO
        }
    }

    public class Coin : Card
    {
        public int Quantity { get; set; } = 1;
        public Coin(string cardName) : base(cardName)
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

        public Alive(string cardName, int health, int damage, AttackType attackType) : base(cardName)
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

        public Villager(string cardName, int health, int damage, AttackType attackType, int workCapacity) : base(cardName, health, damage, attackType)
        {
            this.WorkCapacity = workCapacity;
        }
    }

    public class Mob : Alive
    {
        public float Speed { get; set; }
        public List<Card> DeathOccurences { get; set; }

        public Mob(string cardName, int health, int damage, AttackType attackType, float speed, List<Card> deathOccurences) : base(cardName, health, damage, attackType)
        {
            this.Speed = speed;
            this.DeathOccurences = deathOccurences;
        }
    }

    public class FriendlyMob : Mob
    {
        public Card[] Production { get; set; }

        public FriendlyMob(string cardName, int health, int damage, AttackType attackType, float speed, List<Card> deathOccurences, Card[] production) : base(cardName, health, damage, attackType, speed, deathOccurences)
        {

            this.Production = production;
        }
    }

    public class UnfriendlyMob : Mob
    {
        public UnfriendlyMob(string cardName, int health, int damage, AttackType attackType, float speed, List<Card> deathOccurences) : base(cardName, health, damage, attackType, speed, deathOccurences)
        {
        }
    }

    public class Job : MonoBehaviour
    {
        public Resource[] TargetResources { get; set; }
        public Structure WorkLocation { get; set; }
        public bool IsVillagerNecessary { get; set; }
        public int BaseWorkRequired { get; set; }
        public Card[] Result { get; set; }


        public Job(Resource[] targetResources, Structure workLocation, bool isVillagerNecessary, int baseWorkRequired, Card[] result)
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
        public int Index { get; set; } = 0;
        public List<Card> CardStack { get; set; } = new List<Card>();

        public Sellable(string cardName, int price) : base(cardName)
        {
            this.Price = price;
            this.CardStack.Add(this);
        }
        public void addToStack(Sellable sellable)
        {
            this.CardStack.Add(sellable);
            sellable.Index = (this.Index + 1);
            sellable.CardStack = (this.CardStack);

        }
        public void leaveFromStack(Sellable sellable)
        {
            this.CardStack.Remove(sellable);
            sellable.Index = 0;
            sellable.CardStack = (new List<Card>());
        }






    }

    public class Food : Sellable
    {
        public bool IsConsumable { get; set; }
        public int HungerRestoration { get; set; }
        public bool IsPerishable { get; set; }
        public int ShelfLife { get; set; }

        public Food(string cardName, int price, bool isConsumable, int hungerRestoration, bool isPerishable, int shelfLife) : base(cardName, price)
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

        public Resource(string cardName, int price) : base(cardName, price)
        {
        }

        public Resource(string cardName, int price, Card nextLevel) : base(cardName, price)
        {
            this.NextLevel = nextLevel;
        }
    }

    public class Structure : Sellable
    {
        public int WorkRequiredEach { get; set; }

        public Structure(string cardName, int price, int workRequiredEach) : base(cardName, price)
        {
            this.WorkRequiredEach = workRequiredEach;
        }
    }

    public class NaturalStructure : Structure
    {
        public Card[] Goods { get; set; }
        public int TimesProccessable { get; set; }

        public NaturalStructure(string cardName, int price, int workRequiredEach, Card[] goods, int timesProccessable) : base(cardName, price, workRequiredEach)
        {
            this.Price = 0; // Initialize Price to 0
            this.Goods = goods;
            this.TimesProccessable = timesProccessable;
        }
    }

    public class AutomateStructure : Structure
    {
        public Dictionary<Card, Card> InAndOutDictionary { get; set; }

        public AutomateStructure(string cardName, int price, int workRequiredEach, Dictionary<Card, Card> inAndOutDictionary) : base(cardName, price, workRequiredEach)
        {
            this.InAndOutDictionary = inAndOutDictionary;
        }
    }

    public class WorkPlaceStructure : Structure
    {
        public Card Good { get; set; }

        public WorkPlaceStructure(string cardName, int price, int workRequiredEach, Card good) : base(cardName, price, workRequiredEach)
        {
            this.Good = good;
        }
    }

    public class HolderStructure : Structure
    {
        public int Capacity { get; set; }
        public Card[] CanHold { get; set; }

        public HolderStructure(string cardName, int price, int workRequiredEach, int capacity, Card[] canHold) : base(cardName, price, workRequiredEach)
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

        public Equipment(string cardName, int price, EquipmentSlot slot, int durability, int damageAddition, int armor) : base(cardName, price)
        {
            this.Slot = slot;
            this.Durability = durability;
            this.DamageAddition = damageAddition;
            this.Armor = armor;
        }
    }

    public class Idea : Card{
        public Dictionary<Card, int> Ingredients { get; set; }
        public string Description { get; set; }
        public int HowManyCoins { get; set; }

        public Idea(string cardName, Dictionary<Card, int> ingredients, string description, int howManyCoins) : base(cardName)
        {
            this.Ingredients = ingredients;
            this.Description = description;
            this.HowManyCoins = howManyCoins;
        }
    }

    public class Location : Card
    {
        public List<Card> Items { get; set; }
        public List<Mob> Mobs { get; set; }
        public int DiscoveryWorkRequired { get; set; }

        public Location(string cardName, List<Card> items, List<Mob> mobs, int discoveryWorkRequired) : base(cardName)
        {
            this.Items = items;
            this.Mobs = mobs;
            this.DiscoveryWorkRequired = discoveryWorkRequired;
        }
    }


}
