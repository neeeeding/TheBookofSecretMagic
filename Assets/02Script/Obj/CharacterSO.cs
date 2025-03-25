using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "SO/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public CharacterName characterName;

    public PlayerJob characterJob;

    public Sprite characterImage;
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
    [Description("����")] none,
    [Description("��")] brack,
    [Description("ġ��")] heal,
    [Description("��")] fire,
    [Description("��")] water,
    [Description("����")] copy,
    [Description("����")] potion
}
