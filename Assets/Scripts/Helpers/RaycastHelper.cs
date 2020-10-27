using UnityEngine;

namespace TapToDefuse.Helpers
{
    public static class RaycastHelper
    {
        public static Camera MainCamera
        {
            get
            {
                if (_mainCamera == null)
                    _mainCamera = Camera.main;
                return _mainCamera;
            }
        }
        private static Camera _mainCamera;
        
        public static T GetObject<T>(Vector3 tapPos, float distance, LayerMask interactionLayer)
        {
            Vector3 point = MainCamera.ScreenToWorldPoint(tapPos);
            RaycastHit2D hit2D = Physics2D.Raycast(point, -Vector3.up, distance, interactionLayer);

            if (hit2D.transform != null && hit2D.transform.TryGetComponent(out T component))
            {
                return component;
            }
            return default;
        }
    }
}
