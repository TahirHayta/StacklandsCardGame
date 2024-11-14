using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class Test : MonoBehaviour
{
    public Card cardInstance; ///Buraya inspectordan ekledim. oyunda ek oluştururken böyle olmayacak muhtemelen


    // Start is called before the first frame update
    void Start()
    {
        // Define a position to instantiate the card at (e.g., center of screen)
        cardInstance.createPhysicalCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
