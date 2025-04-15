using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LikeItem : MonoBehaviour
{
    [SerializeField]private ItemSO loveItem; //������ ����

    private string savePath; //���� ���
    private Image itemImage; //������ �̹���
    private int colorCount; //(Ŭ��) �ܰ�

    private void Awake()
    {
        itemImage = GetComponent<Image>();
    }
    
    public void SettingItem(ItemSO itemSO) //������ ����
    {
        loveItem = itemSO;
        itemImage.sprite = loveItem.itemImage;
        savePath = $"{loveItem.itemType}likeItem";
        colorCount = PlayerPrefs.GetInt(savePath) - 1; //�����ִϱ�.
        ChangeColorLove();
    }

    public void  ChangeColorLove() //������ ���� �� �ٲٱ�
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
