using UnityEngine;

namespace Gameplay
{
    public static class CanvasExtensions
    {
        public static Vector2 ConvertWorldToCanvasPoint(this Canvas canvas, Vector3 worldPosition, Camera camera = null)
        {
            camera ??= Camera.main;
            RectTransform canvasRectTransform = (RectTransform)canvas.transform;
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(camera, worldPosition);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPoint, 
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : camera, out Vector2 canvasPoint);
            return canvasPoint;
        }  
        
        public static void SetPositionFromWorldPoint(this RectTransform rectTransform, Vector3 worldPosition, Camera camera = null)
        {
            
        }
    }
}