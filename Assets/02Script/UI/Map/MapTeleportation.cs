using _02Script.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace _02Script.UI.Map
{
    public class MapTeleportation : MonoBehaviour
    {
        [Header("Need")]
        [SerializeField] private RawImage mapImage; // 맵
        [SerializeField] private Camera mainCamera; //카메라
        [SerializeField] private Transform inGameMap;
        [SerializeField] private Canvas canvas;

        private RectTransform imageRect; //맵 크기
        
        private void Awake()
        {
            imageRect = mapImage.GetComponent<RectTransform>();
        }
        
        public void TeleportationPos()
        {
            Vector2 mapSize = imageRect.rect.size; //맵 사이즈
            Vector2 mapPosition = imageRect.position; //맵 위치
            
            Camera uiCam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                imageRect, Input.mousePosition, uiCam, out Vector2 mapPos);
            
            // mapPos = new Vector3(
            //     ((localPoint.x - (mapSize.x / 2)) + mapPosition.x),
            //     ((localPoint.y - (mapSize.y / 2)) + mapPosition.y), 0);
            
            ConvertRawImageToWorldPos(mapPos, out Vector3 playerPos);
            
            GameManager.Instance.Player.transform.position = playerPos;
        }
        
        private void ConvertRawImageToWorldPos(Vector3 mapPos, out Vector3 playerPos)
        {
            Vector3 worldPos = mainCamera.ViewportToScreenPoint(mapPos);
            worldPos.z = -1;
            print(mapPos);
            print(worldPos);
            
            bool success = RectTransformUtility.ScreenPointToWorldPointInRectangle(imageRect, mapPos, mainCamera, out Vector3 worldPoint);
            //bool success = RectTransformUtility.ScreenPointToWorldPointInRectangle(imageRect, mapPos, mainCamera, out Vector2 worldPoint);
            //Vector3 worldPoint = RectTransformUtility.WorldToScreenPoint(mainCamera, mapPos);
            //Vector3 worldPoint = mainCamera.ScreenToWorldPoint(worldPos);
            //worldPoint = mainCamera.ScreenToWorldPoint(worldPoint);
            print(worldPoint);
            worldPoint.z = 0;
            playerPos = worldPoint;
        }
    }
}