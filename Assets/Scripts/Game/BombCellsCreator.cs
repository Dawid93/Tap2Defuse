using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace TapToDefuse.Game
{
    public class BombCellsCreator : MonoBehaviour
    {
        [SerializeField] private RectTransform parent;
        [SerializeField] private BombCell cell;
        
        [SerializeField] private float bombRadius;
        [SerializeField] private float spaceBetweenX;
        [SerializeField] private float spaceBetweenY;

        public void PrepareBoard(Action<BombCell[]> callback)
        {
            if(parent.childCount > 0)
                RemoveCells();
            callback?.Invoke(CreateCells());
        }

        [Button("Create Cells")]
        private BombCell[] CreateCells()
        {
            List<BombCell> cells = new List<BombCell>();
            for (float y = 0; y < parent.rect.height - 2 * bombRadius;)
            {
                y = y + spaceBetweenY + bombRadius * 2;
                for (float x = 0; x < parent.rect.width - 2 * bombRadius;)
                {
                    x = x + spaceBetweenX + bombRadius * 2;
                    BombCell bc = Instantiate(cell, parent);
                    bc.RectTransform.anchoredPosition = new Vector2(x, -y);
                    cells.Add(bc);
                }
            }

            return cells.ToArray();
        }

        [Button("Destroy")]
        private void RemoveCells()
        {
            BombCell[] cells = parent.GetComponentsInChildren<BombCell>();
            foreach (var bombCell in cells)
            {
                DestroyImmediate(bombCell.gameObject);
            }
        }
    }
}
