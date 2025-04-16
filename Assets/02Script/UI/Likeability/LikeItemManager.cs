using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LikeItemManager : MonoBehaviour
{
    [Header("Items")]
    [SerializeField]
    private ItemSO[] loveItems; //�������� �� �����۵�
    [Space(30f)]
    [Header("Prefabs Need")]
    [SerializeField] private GameObject Item; //������Ʈ
    private CharacterSO character; //���� ĳ���� ����

    public void Setting(CharacterSO so) //����
    {
        character = so;
        SettingItem();
    }

    private void MakeItem()  //�����۵� ����
    {
        for (int i = 0; i < loveItems.Length; i++)
        {
            GameObject loveItem = Instantiate(Item, transform);
            loveItem.SetActive(true);
        }
    }

    private void SettingItem() //������ ����
    {
        if(transform.childCount == 0)
        {
            MakeItem();
        }
        for(int i = 0; i < loveItems.Length; i++)
        {
            GameObject loveItem = transform.GetChild(i).gameObject;
            LikeItemItem itemSc = loveItem.GetComponent<LikeItemItem>();
            itemSc.SettingItem(character, loveItems[i]);
        }
    }
}
