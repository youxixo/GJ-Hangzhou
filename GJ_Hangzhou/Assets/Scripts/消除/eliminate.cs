using UnityEngine;

public abstract class eliminate : MonoBehaviour
{
    public static GameObject selectedObject1 = null;
    public static GameObject selectedObject2 = null;

    private void OnMouseDown()
    {
        if (selectedObject1 == null)
        {
            selectedObject1 = this.gameObject;
            Debug.Log("获取1");
            HighlightObject(selectedObject1);
        }
        else if (selectedObject2 == null)
        {
            selectedObject2 = this.gameObject;
            HighlightObject(selectedObject2);

            if (selectedObject1 == selectedObject2)
            {
                selectedObject2 = null; // Reset if the same object is selected twice
                return;
            }

            if (selectedObject1.CompareTag(selectedObject2.tag))
            {
                Destroy(selectedObject1);
                Destroy(selectedObject2);
            }
            else
            {
                RemoveHighlight(selectedObject1);
                selectedObject1 = null;
            }

            selectedObject2 = null;
        }
    }

    private void HighlightObject(GameObject obj)
    {
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = Color.yellow; // 改变颜色以示高亮
        }
    }

    private void RemoveHighlight(GameObject obj)
    {
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = Color.white; // 恢复原始颜色
        }
    }
}