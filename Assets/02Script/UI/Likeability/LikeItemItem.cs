using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LikeItemItem : MonoBehaviour
{
    [SerializeField]private CharacterSO loveCharacter; //������ ����
    [SerializeField]private ItemSO loveItem; //������ ����

    private string savePath; //���� ���
    private Image itemImage; //������ �̹���
    private int colorCount; //(Ŭ��) �ܰ�

    public void SettingItem(CharacterSO characterSO,ItemSO itemSO) //������ ���� (��������, � ����������)
    {
        if(itemImage == null)
        {
            itemImage = GetComponent<Image>();
        }
        loveCharacter = characterSO;
        loveItem = itemSO;
        //itemImage.sprite = loveItem.itemImage;
        savePath = $"{loveCharacter}_{loveItem.itemType}likeItem";
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
        PlayerPrefs.Save();
    }
}
