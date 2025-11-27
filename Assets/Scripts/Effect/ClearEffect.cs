using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearEffect : MonoBehaviour
{
    public GameObject clearEffect;
    public GameObject gameClearPanel;
    
    public void PlayClearEffect()
    {
        clearEffect.SetActive(true);
        StartCoroutine(ShowGameClearPanel());
    }

    private IEnumerator ShowGameClearPanel()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        gameClearPanel.SetActive(true);
        GameManager.instance.isCleared = true;
    }
}
