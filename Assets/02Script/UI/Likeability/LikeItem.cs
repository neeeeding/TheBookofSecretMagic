using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LikeItem : MonoBehaviour
{
    [SerializeField]private ItemSO loveItem; //아이템 종류

    private string savePath; //저장 경로
    private Image itemImage; //아이템 이미지
    private int colorCount; //(클릭) 단계

    private void Awake()
    {
        itemImage = GetComponent<Image>();
    }
    
    public void SettingItem(ItemSO itemSO) //아이템 세팅
    {
        loveItem = itemSO;
        itemImage.sprite = loveItem.itemImage;
        savePath = $"{loveItem.itemType}likeItem";
        colorCount = PlayerPrefs.GetInt(savePath) - 1; //더해주니까.
        ChangeColorLove();
    }

    public void  ChangeColorLove() //아이템 눌러 색 바꾸기
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
        PlayerPrefs.SetInt(savePath, colorCount);
    }
}
