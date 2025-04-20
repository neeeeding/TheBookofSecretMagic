using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMarkInGame : MonoBehaviour
{
    [SerializeField] private RawImage mapImage; // 맵
    [SerializeField]private Camera mainCamera; //카메라
    [SerializeField] private GameObject mapMark; //맵 마크

    public void ClcikMapMark() //맵 마크 버튼 누를 때
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

        // 월드 -> 스크린
        Vector3 screenPos = mainCamera.WorldToScreenPoint(player);
        print($"{screenPos} : sc");

        RectTransform imageRect = mapImage.GetComponent<RectTransform>();

        // UI 카메라가 없을 경우 null (Screen Space - Overlay용)
        bool success = RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imageRect, screenPos, null, out Vector2 localPoint);

        print($"{localPoint} : lo");

        //if (!success)
        //    return false;

        // 영역 안인지 확인 (Pivot 기준이기 때문에 rect가 -width/2 ~ +width/2 범위임)
        Rect rect = imageRect.rect;
        if (!rect.Contains(localPoint))
            return false;

        mapPos = localPoint;
        return true;
    }
}
