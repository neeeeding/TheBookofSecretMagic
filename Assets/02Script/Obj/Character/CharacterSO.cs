using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "SO/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public CharacterName characterName;
    [Space(10f)]
    public PlayerJob characterJob;
    [Space(15f)]
    public Sprite characterImage;
    [Space(20f)]
    [Header("Dialog")]
    public TextAsset[] characterDialog;
}

public enum CharacterName
{
    [Description("���ΰ�")] me,
    [Description("����Ƽ")] resty,
    [Description("ũ����")] chris,
    [Description("�ǿ�")] pio,
    [Description("�׿�")] theo,
    [Description("���")] noah,
    [Description("�Ͼ�")] nia,
    [Description("����")] villain,
    [Description("�ظ�")] harry,
    [Description("�ٴϿ�")] daniel
}

public enum PlayerJob
{
    [Description("����")] none = 0,
    [Description("��")] brack,
    [Description("ġ��")] heal,
    [Description("��")] fire,
    [Description("��")] water,
    [Description("����")] copy,
    [Description("����")] potion
}
