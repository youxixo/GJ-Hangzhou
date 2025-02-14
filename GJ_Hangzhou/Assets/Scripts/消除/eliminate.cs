using UnityEngine;
using System.Collections;

public abstract class eliminate : MonoBehaviour
{
    public static GameObject selectedObject1 = null;
    public static GameObject selectedObject2 = null;

    private bool playerInRange = false;
    public GameObject largeBookPrefab; // 预制体

    private CloudManager cloudManager;

    void Start()
    {
        cloudManager = FindObjectOfType<CloudManager>();
    }

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
                    Destroy(selectedObject1);
                    Destroy(selectedObject2);
                    GenerateLargeBookPrefab();

                    // 启动云朵移动
                    if (selectedObject1.CompareTag("Windows") && selectedObject2.CompareTag("Windows") && cloudManager != null)
                    {
                        Debug.Log("eliminate: Windows matched, calling WindowDestroyed");
                        cloudManager.WindowDestroyed();
                    }
                }
                else
                {
                    Debug.Log("标签不匹配: " + selectedObject1.tag + " != " + selectedObject2.tag);
                    SetOutlineThickness(selectedObject1, 0f); // 恢复默认厚度
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

    private void GenerateLargeBookPrefab()
    {
        if (largeBookPrefab != null)
        {
            Vector3 spawnPosition = new Vector3(67.8000031f,-3.9000001f,0.116799332f); // 设置生成位置，根据需要调整
            Quaternion spawnRotation = Quaternion.Euler(0, 0, 270); 
            GameObject largeBook = Instantiate(largeBookPrefab, spawnPosition, spawnRotation);
            Debug.Log("生成了 largeBookPrefab");

            // 启动协程延迟删除对象的Collider
            StartCoroutine(RemoveCollidersAfterDelay(largeBook, 30f));
        }
    }

    private IEnumerator RemoveCollidersAfterDelay(GameObject obj, float delay)
    {
        Debug.Log("RemoveCollidersAfterDelay started for " + obj.name);
        yield return new WaitForSeconds(delay);
        
        // 获取并删除所有 Collider 组件
        Collider2D[] colliders2D = obj.GetComponents<Collider2D>();

        foreach (var collider in colliders2D)
        {
            Destroy(collider);
        }

        Debug.Log("删除了 largeBookPrefab 的所有 Colliders");
    }
}
