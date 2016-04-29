﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Card : MonoBehaviour {

	public string    suit;
	public int       rank;
	public Color     color = Color.black;
	public string    colS = "Black";  // or "Red"
	
	public List<GameObject> decoGOs = new List<GameObject>();
	public List<GameObject> pipGOs = new List<GameObject>();
	
	public GameObject back;     // back of card;
	public CardDefinition def;  // from DeckXML.xml	

    //List of the SpriteRenderer Componenets of this GameObject and its children
    public SpriteRenderer[] spriteRenderers;
    

    void Start()
    {
        SetSortOrder(0); //Ensures that the card starts properly depth sorted
    }	

	
	// property
	public bool faceUp {
		get {
			return (!back.activeSelf);
		}		
		set {
			back.SetActive(!value);
		}
	}
    //If spriteRenderes is not yet defined, this function defines it
    public void PopulateSpriteRenderers()
    {
        //If spriteRederers is null or empty
        if (spriteRenderers == null || spriteRenderers.Length == 0)
        {
            //Get SpriteRendererComponents of this GameObject and its children
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }
    }

    //Sets the sortingLayerNameon all SpriteRenderer Components
    public void SetSortingLayerName(string tSLN)
    {
        PopulateSpriteRenderers();

        foreach (SpriteRenderer tSR in spriteRenderers)
        {
            tSR.sortingLayerName = tSLN;
        }
    }

    //Set the sorttngOrder of all SpriteRendererComponenets
    public void SetSortOrder(int sOrd)
    {
        PopulateSpriteRenderers();
        //White background of the card is on bottom (sOrd)
        //Top o fthat are all the pips, decs, face, etc. (sOrd +1)
        //Back is on top so that when visible, it covers the rest (sOrd + 2)

        //Iterate through all the spriteRenderers as tSR
        foreach (SpriteRenderer tSR in spriteRenderers)
        {
            if (tSR.gameObject == this.gameObject)
            {
                tSR.sortingOrder = sOrd;
                continue;
            }
            switch (tSR.gameObject.name)
            {
                case "back":
                    tSR.sortingOrder = sOrd + 2;
                    break;
                case "face":
                    tSR.sortingOrder = sOrd + 1;
                    break;
                default:
                    tSR.sortingOrder = sOrd + 1;
                    break;
            }
        }
    }

    virtual public void OnMouseUpAsButton()
    {
        print(name);
    }
} // class Card

[System.Serializable]
public class Decorator{
	public string	type;			// For card pips, tyhpe = "pip"
	public Vector3	loc;			// location of sprite on the card
	public bool		flip = false;	//whether to flip vertically
	public float 	scale = 1.0f;
}

[System.Serializable]
public class CardDefinition{
	public string	face;	//sprite to use for face cart
	public int		rank;	// value from 1-13 (Ace-King)
	public List<Decorator> pips = new List<Decorator>();  // Pips Used					
} // Class CardDefinition
