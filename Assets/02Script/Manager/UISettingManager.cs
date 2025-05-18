using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UISettingManager : Singleton<UISettingManager>
{
    [Header("Need")]
    [SerializeField] private GameObject[] inGame; //�� ���� �ʿ� ��� (ex : ��, ����...)
    [SerializeField] private Dialog chat; //ä��
    [SerializeField] private GameObject coin; //���� ����

    [SerializeField] private GameObject backBtn; //�� ��������
    [Space(10f)]
    [Header("Need(setting)")]
    [SerializeField] private GameObject allSetting;//������ �� ��Ƶ� ���
    [SerializeField] private GameObject setting; //����
    [SerializeField] private GameObject profile; //������
    [SerializeField] private GameObject likeabilityGuide; //ĳ���͵�
    [SerializeField] private GameObject likeItem; //��ȣ ������
    [SerializeField] private LikeItemManager likeItemManager; //��ȣ ������ �Ŵ���
    [SerializeField] private GameObject map; //����
    [SerializeField] private GameObject save; //����
    [Space(10)]
    [Header("Select")]
    [SerializeField] private GameObject obj; //����(Ȥ�� ���� ����)

    private bool isChat; // !isInGame
    private bool isObj;
    private bool isCoin;
    private bool isProfile;
    private bool isLikeItem;
    private bool isLikeabilityGuide;
    private bool isMap;
    private bool isSetting;
    private bool isSave;

    private void Awake()
    {
        isChat = false;
        InGame();
    }

    private void OnEnable()
    {
        Character.OnChat += Chat;
    }

    private void OnDisable()
    {
        Character.OnChat -= Chat;
    }

    private void Update()
    {
        if ((isProfile || isSetting || isLikeabilityGuide || isLikeItem || isMap || isSetting || isSave || isCoin || isObj || isChat)&&PlayerMobileInput.Instance.CheckCanInput())
        {
            PlayerMobileInput.Instance.NoInput();
        }
    }
    public void InGame() //��������
    {
        GameManager.Instance.PlayerStat.isChat = false;
        AllHide();
        SettingAll();
        Time.timeScale = 1f;
        PlayerMobileInput.Instance.CanInput();
    }

    public void CloseChat() //ä�� �ݱ�
    {
        AllHide();
        isChat = false;
        GameManager.Instance.PlayerStat.isChat = false;
        SettingAll();
        InGame();
        //chat.DialogSetting(null,null);
    }

    public void Coin() //���� ����
    {
        AllHide();
        isCoin = true;
        SettingAll();
    }

    public void Obj() //���� Ȥ�� �б� ���� ����
    {
        AllHide();
        isObj = true;
        SettingAll();
    }

    public void Chat(CharacterSO so, Character character) //ä��
    {
        AllHide();
        isChat = true;
        SettingAll();
        chat.DialogSetting(so, character);
        GameManager.Instance.PlayerStat.isChat = true;
    }

    public void Profile() //������
    {
        AllHide();
        isProfile = true;
        SettingAll();
    }

    public void LiKeItme(CharacterSO so) //��ȣ ������
    {
        AllHide();
        isLikeItem = true;
        isLikeabilityGuide = true;
        likeItemManager.Setting(so);
        SettingAll();
    }

    public void LikeabilityGuide() //ȣ����
    {
        AllHide();
        isLikeabilityGuide = true;
        SettingAll();
    }

    public void Map() //����
    {
        AllHide();
        isMap = true;
        SettingAll();
    }

    public void Setting() // ����
    {
        AllHide();
        isSetting = true;
        SettingAll();
    }

    public void Save() //����
    {
        AllHide();
        isSave = true;
        SettingAll();
    }

    private void SettingAll() //���õ�
    {
        profile.SetActive(isProfile);
        likeItem.SetActive(isLikeItem);
        likeabilityGuide.SetActive(isLikeabilityGuide);
        map.SetActive(isMap);
        setting.SetActive(isSetting);
        save.SetActive(isSave);

        coin.SetActive(isCoin);
        chat.gameObject.SetActive(isChat);
        foreach (GameObject obj in inGame)
        {
            obj.SetActive(!isChat);
        }
        obj.SetActive(isObj);

        bool all = isProfile || isSetting || isLikeabilityGuide || isLikeItem || isMap || isSetting || isSave;
        allSetting.SetActive(all);
        backBtn.SetActive(all || isCoin || isObj);

        Time.timeScale = 0f;
    }

    private void AllHide() //���� �����
    {
        isCoin = false;
        //isChat = false;
        isObj = false;
        isProfile = false;
        isLikeItem = false;
        isLikeabilityGuide = false;
        isMap = false;
        isSetting = false;
        isSave = false;
    }
}
