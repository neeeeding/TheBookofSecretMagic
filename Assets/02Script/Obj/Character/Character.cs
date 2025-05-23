using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    //������ num : (é����) �ѹ�/ chapter : é��/ text : ������ ��ȭ
    //LikeabilityCard = 
    public static Action<CharacterSO,Character> OnChat;

    [SerializeField] private CharacterSO characterSO;
    [SerializeField] private int chapter; //é��
    [SerializeField] private int finallNum; //��ȣ
    private PlayerStatSC path; //���� (���� ����)
    private bool isChat;

    private void Awake()
    {
        isChat = false;
        path = GameManager.Instance.PlayerStat;
        int.TryParse(path.characterlastText[characterSO.characterName][DialogType.Chapter],out chapter);
        int.TryParse(path.characterlastText[characterSO.characterName][DialogType.Num],out finallNum);
    }

    public void Load() //�ε� �� ��
    {
        path = GameManager.Instance.PlayerStat; // 

        if (path != null)
        {
            int.TryParse(path.characterlastText[characterSO.characterName][DialogType.Chapter], out chapter);
            int.TryParse(path.characterlastText[characterSO.characterName][DialogType.Num], out finallNum);
        }

        //�������̳� Ư�� ��ȭ������ ������ ������ Ȯ�� �� ��
        if (GameManager.Instance.PlayerStat.isChat)
        {
            UISettingManager.Instance.CloseChat();
            finallNum--; //��ȭ�� ���� �� �� 1�� �߰��ϰ� ����������.
            UISettingManager.Instance.Chat(GameManager.Instance.PlayerStat.lastSO, GameManager.Instance.PlayerStat.lastCharacter);
        }
        else
        {
            UISettingManager.Instance.CloseChat();
        }
    }

    public void NextDialog(int i) //��ȭ�� ���� �� ������
    {
        finallNum = i;

        path.characterlastText[characterSO.characterName][DialogType.Num] = finallNum.ToString();
    }

    public int[] CurrentDialog() //���� ���� ���� (é��, �ѹ� �� �Ѱ��ֱ�)
    {
        return new int[]{chapter, finallNum};
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isChat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isChat = false;
        }
    }

    public void NextChapter() //���� é�ͷ� ���� ���ֱ�
    {
        finallNum = 1;
        chapter++;

        path.characterlastText[characterSO.characterName][DialogType.Chapter] = chapter.ToString();
        path.characterlastText[characterSO.characterName][DialogType.Num] = finallNum.ToString();
    }

    public void ClickCharacter() //��ȭ �ϱ� (Ŭ��)
    {
        if (isChat)
        {
            //finallNum = 1;
            OnChat?.Invoke(characterSO,this);
        }
    }

    private void OnEnable()
    {
        LoadCard.OnLoad += Load;
    }

    private void OnDisable()
    {
        LoadCard.OnLoad -= Load;
    }
}
