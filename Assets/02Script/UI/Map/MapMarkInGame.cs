using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapMarkInGame : MonoBehaviour
{
    [Header("Need")]
    [SerializeField] private RawImage mapImage; // 맵
    [SerializeField]private Camera mainCamera; //카메라
    [SerializeField] private GameObject mapMark; //맵 마크
    [SerializeField] private Canvas canvas;
    [Space(20f)]
    [Header("Show")]
    [SerializeField] private RectTransform imageRect; //맵 크기

    private void Awake()
    {
        imageRect = mapImage.GetComponent<RectTransform>();
    }

    public void ClcikMapMark() //맵 마크 버튼 누를 때
    {
        if (ConvertWorldToRawImagePos(GameManager.Instance.Player.transform.position, out Vector2 mapPos))
        {
            GameObject mark = Instantiate(mapMark, mapImage.transform);

            Vector2 mapSize = imageRect.rect.size; //맵 사이즈
            Vector2 mapPosition = imageRect.position; //맵 위치 (보정을 위해)

            Vector2 pos = new Vector2(
                ((mapPos.x - (mapSize.x / 2)) + mapPosition.x),
                ((mapPos.y - (mapSize.y / 2)) + mapPosition.y)); //최종 마크 위치 결정 (비율과 보정을 함)

            mark.transform.localPosition = pos;
        }
    }

    bool ConvertWorldToRawImagePos(Vector3 player, out Vector2 mapPos)  //캐릭터 위치를 찾고 그 것을 맵 위치로 변환
    {
        mapPos = Vector2.zero;

        // 월드 -> 스크린
        Vector3 screenPos = mainCamera.WorldToScreenPoint(player);

        Camera uiCam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        // UI 카메라가 없을 경우 null (Screen Space - Overlay용)
        bool success = RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imageRect, screenPos, uiCam, out Vector2 localPoint);

        //범위 벗어날 때를 생각
        mapPos = localPoint;
        return true;
    }
}
