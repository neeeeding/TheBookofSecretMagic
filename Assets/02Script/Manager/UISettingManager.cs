using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISettingManager : Singleton<UISettingManager>
{
    [SerializeField] private GameObject[] inGame;
    [Space(10f)]
    [SerializeField] private Dialog chat;
    [Space(10f)]
    [SerializeField] private GameObject store;
    [Space(10f)]
    [SerializeField] private GameObject coin;
    [Space(10f)]
    [SerializeField] private GameObject profile;
    [SerializeField] private GameObject likeItem;
    [SerializeField] private GameObject likeabilityGuide;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject setting;
    [SerializeField] private GameObject save;
    [Space (10f)]
    [SerializeField] private GameObject allSetting;
    [Space(10f)]
    [SerializeField] private GameObject backBtn;

    private bool isChat; // !isInGame
    private bool isStore;
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
        if ((isProfile || isSetting || isLikeabilityGuide || isLikeItem || isMap || isSetting || isSave || isCoin || isStore || isChat)&&PlayerMobileInput.Instance.CheckCanInput())
        {
            PlayerMobileInput.Instance.NoInput();
        }
    }

    public void InGame() //��������
    {
        AllHide();
        SettingAll();
        Time.timeScale = 1f;
        PlayerMobileInput.Instance.CanInput();
    }

    public void CloseChat() //ä�� �ݱ�
    {
        AllHide();
        isChat = false;
        SettingAll();
        chat.DialogSetting(null,null);
    }

    public void Coin() //���� ����
    {
        AllHide();
        isCoin = true;
        SettingAll();
    }

    public void Store() //����
    {
        AllHide();
        isStore = true;
        SettingAll();
    }

    public void Chat(CharacterSO so, Character character) //ä��
    {
        AllHide();
        isChat = true;
        SettingAll();
        chat.DialogSetting(so, character);
    }

    public void Profile() //������
    {
        AllHide();
        isProfile = true;
        SettingAll();
    }

    public void LiKeItme() //��ȣ ������
    {
        AllHide();
        isLikeItem = true;
        isLikeabilityGuide = true;
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
        store.SetActive(isStore);

        bool all = isProfile || isSetting || isLikeabilityGuide || isLikeItem || isMap || isSetting || isSave;
        allSetting.SetActive(all);
        backBtn.SetActive(all || isCoin || isStore);

        Time.timeScale = 0f;
    }

    private void AllHide() //���� �����
    {
        isCoin = false;
        //isChat = false;
        isStore = false;
        isProfile = false;
        isLikeItem = false;
        isLikeabilityGuide = false;
        isMap = false;
        isSetting = false;
        isSave = false;
    }
}
