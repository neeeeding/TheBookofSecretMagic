using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private void Awake()
    {
        imageRect = mapImage.GetComponent<RectTransform>();
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
}
