using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHold : MonoBehaviour
{
    private ItemSO so;
    private ItemCard card;
    
    public void Setting(ItemSO currentSO, ItemCard currentCard)
    {
        so = currentSO;
        card = currentCard;
    }

    private void UseItem()
    {
        card.HideItem();
    }
}
