using UnityEngine;
using UnityEngine.UI;

public class MapMarkInGame : MonoBehaviour
{
    [Header("Need")]
    [SerializeField] private RawImage mapImage; // ��
    [SerializeField]private Camera mainCamera; //ī�޶�
    [SerializeField] private GameObject mapMark; //�� ��ũ
    [SerializeField] private Canvas canvas;
    [Space(20f)]
    [Header("Show")]
    [SerializeField] private RectTransform imageRect; //�� ũ��

    private static int num = 0; //��°
    private string numPath = "mapPos";
    private string path = $"mapPos_"; //���� �̸�

    private void Awake()
    {
        num = PlayerPrefs.GetInt(numPath);
        imageRect = mapImage.GetComponent<RectTransform>();
        LoadMapMark();
    }

    public void ResetMapMarkNum()
    {
        num = 0;
        PlayerPrefs.SetInt(numPath, num);
        PlayerPrefs.Save();
    }

    public void ClcikMapMark() //�� ��ũ ��ư ���� ��
    {
        if (ConvertWorldToRawImagePos(GameManager.Instance.Player.transform.position, out Vector2 mapPos))
        {
            GameObject mark = Instantiate(mapMark, mapImage.transform);

            Vector2 mapSize = imageRect.rect.size; //�� ������
            Vector2 mapPosition = imageRect.position; //�� ��ġ (������ ����)

            Vector2 pos = new Vector2(
                ((mapPos.x - (mapSize.x / 2)) + mapPosition.x),
                ((mapPos.y - (mapSize.y / 2)) + mapPosition.y)); //���� ��ũ ��ġ ���� (������ ������ ��)

            mark.transform.localPosition = pos;

            //��ġ ����
            num++;
            PlayerPrefs.SetInt(numPath, num);
            PlayerPrefs.Save();
            PlayerPrefs.SetFloat($"{path}_{num}_X", pos.x);
            PlayerPrefs.SetFloat($"{path}_{num}_Y", pos.y);
        }
    }

    bool ConvertWorldToRawImagePos(Vector3 player, out Vector2 mapPos)  //ĳ���� ��ġ�� ã�� �� ���� �� ��ġ�� ��ȯ
    {
        mapPos = Vector2.zero;

        // ���� -> ��ũ��
        Vector3 screenPos = mainCamera.WorldToScreenPoint(player);

        Camera uiCam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        // UI ī�޶� ���� ��� null (Screen Space - Overlay��)
        bool success = RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imageRect, screenPos, uiCam, out Vector2 localPoint);

        //���� ��� ���� ����
        mapPos = localPoint;
        return true;
    }

    private void LoadMapMark() //�� ��ũ ��ġ ����
    {
        if(num > 0)
        {
            int count = num + 1;
            for(int i = 1; i < count; i++)
            {
                num = i;
                GameObject mark = Instantiate(mapMark, mapImage.transform);
                float x = PlayerPrefs.GetFloat($"{path}_{num}_X");
                float y = PlayerPrefs.GetFloat($"{path}_{num}_Y");
                mark.transform.localPosition = new Vector2(x, y);
            }
        }
    }
}
