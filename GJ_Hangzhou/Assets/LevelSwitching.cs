using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitching : MonoBehaviour
{
    public Animator animator;
    public float transitionDuration = 1f;
    private bool isPlayerInTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(SwitchLevel());
        }
    }

    private IEnumerator SwitchLevel()
    {
        // Play the transition animation
        animator.SetTrigger("StartTransition");

        // Wait for the animation to finish
        yield return new WaitForSeconds(transitionDuration);

        // Load the next scene
        SceneManager.LoadScene("Level2");
    }
}
