using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TheCard : MonoBehaviour
{
    public List<Card> thisCard = new List<Card>();
    public int thisId;

    public int id;
    public string cardName;
    public string description;

    public TextMeshProUGUI  idText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public Sprite thisSprite;
    public Image thisImage;

    public Image idColor;
    
    Color cardColor = new Color();

    public bool cardBack;
    public bool staticCardBack;

    public GameObject Hand;

    // Start is called before the first frame update
    void Start()
    {
        thisCard[0] = CardDataBase.cardList[thisId];
        ColorUtility.TryParseHtmlString(thisCard[0].color, out cardColor);
        Hand = GameObject.Find("Hand");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.parent == Hand.transform)
        {
            cardBack = false;
        }
        
        id = thisCard[0].id;
        cardName = thisCard[0].cardName;
        description = thisCard[0].cardDescription;
        thisSprite = thisCard[0].thisImage;

        idText.text = id.ToString();
        nameText.text = cardName;
        descriptionText.text = description;
        thisImage.sprite = thisSprite;

        idColor.GetComponent<Image>().color = cardColor;

        staticCardBack = cardBack;
    }
}
