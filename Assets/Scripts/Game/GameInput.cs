using TapToDefuse.Helpers;
using TapToDefuse.Interface;
using UnityEngine;

namespace TapToDefuse.Game
{
    public class GameInput : MonoBehaviour
    {
        [SerializeField] private LayerMask bombLayer;
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var pos = Input.mousePosition;
                var tapObject = RaycastHelper.GetObject<ITapable>(pos, 20, bombLayer);
                tapObject.OnTap();
            }
        }
    }
}
