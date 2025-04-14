using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LikeItem : MonoBehaviour
{
    private Image itemImage; //������ �̹���
    private int colorCount; //�ܰ�

    private void Awake()
    {
        itemImage = GetComponent<Image>();
        if (colorCount <= 0 || colorCount > 3)
        {
            colorCount = 1;
        }
    }

    public void  ClcikBtn()
    {
        colorCount++;
        if(colorCount <= 0 || colorCount > 3)
        {
            colorCount = 1;
        }

        itemImage.color = colorCount switch
        {
            1 => Color.white,
            2 => Color.green,
            3 => Color.red,
            _ => Color.white
        };
    }
}
