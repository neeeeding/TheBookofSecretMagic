using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMarkInGame : MonoBehaviour
{
    [SerializeField] private RawImage mapImage; // ��
    [SerializeField]private Camera mainCamera; //ī�޶�
    [SerializeField] private GameObject mapMark; //�� ��ũ

    public void ClcikMapMark() //�� ��ũ ��ư ���� ��
    {
        if (ConvertWorldToRawImagePos(GameManager.Instance.Player.transform.position, out Vector2 mapPos))
        {
            print("click");
            GameObject mark = Instantiate(mapMark, mapImage.transform);

            RectTransform markRect = mark.GetComponent<RectTransform>();
            markRect.anchoredPosition = mapPos;
        }
        else
        {
            print("wath?");
        }
    }

    bool ConvertWorldToRawImagePos(Vector3 player, out Vector2 mapPos)
    {
        mapPos = Vector2.zero;

        // ���� -> ��ũ��
        Vector3 screenPos = mainCamera.WorldToScreenPoint(player);
        print($"{screenPos} : sc");

        RectTransform imageRect = mapImage.GetComponent<RectTransform>();

        // UI ī�޶� ���� ��� null (Screen Space - Overlay��)
        bool success = RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imageRect, screenPos, null, out Vector2 localPoint);

        print($"{localPoint} : lo");

        //if (!success)
        //    return false;

        // ���� ������ Ȯ�� (Pivot �����̱� ������ rect�� -width/2 ~ +width/2 ������)
        Rect rect = imageRect.rect;
        if (!rect.Contains(localPoint))
            return false;

        mapPos = localPoint;
        return true;
    }
}
