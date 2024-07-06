using UnityEngine;

public abstract class eliminate : MonoBehaviour
{
    public static GameObject selectedObject1 = null;
    public static GameObject selectedObject2 = null;

    private bool playerInRange = false;
    public GameObject largeBookPrefab; // 用于生成“大书”的预制件

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (selectedObject1 == null)
            {
                selectedObject1 = this.gameObject;
                Debug.Log("获取1: " + selectedObject1.name);
                SetOutlineThickness(selectedObject1, 15f);
            }
            else if (selectedObject2 == null)
            {
                selectedObject2 = this.gameObject;
                Debug.Log("获取2: " + selectedObject2.name);
                SetOutlineThickness(selectedObject2, 15f);

                if (selectedObject1 == selectedObject2)
                {
                    selectedObject2 = null; // Reset if the same object is selected twice
                    return;
                }

                if (selectedObject1.CompareTag(selectedObject2.tag))
                {
                    Debug.Log("标签匹配: " + selectedObject1.tag);
                    Vector3 spawnPosition = (selectedObject1.transform.position + selectedObject2.transform.position) / 2;
                    Destroy(selectedObject1);
                    Destroy(selectedObject2);
                    GenerateLargeBook(spawnPosition);
                }
                else
                {
                    Debug.Log("标签不匹配: " + selectedObject1.tag + " != " + selectedObject2.tag);
                    SetOutlineThickness(selectedObject1, 1f); // 恢复默认厚度
                    selectedObject1 = null;
                }

                selectedObject2 = null;
            }
        }
    }

    private void SetOutlineThickness(GameObject obj, float thickness)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            foreach (Material mat in renderer.materials)
            {
                if (mat.HasProperty("_Thickness"))
                {
                    mat.SetFloat("_Thickness", thickness);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered the range of " + gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player exited the range of " + gameObject.name);
        }
    }

    private void GenerateLargeBook(Vector3 position)
    {
        if (largeBookPrefab != null)
        {
            Instantiate(largeBookPrefab, position, Quaternion.identity);
            Debug.Log("生成大书对象");
        }
        else
        {
            Debug.LogError("largeBookPrefab 未设置");
        }
    }
}
