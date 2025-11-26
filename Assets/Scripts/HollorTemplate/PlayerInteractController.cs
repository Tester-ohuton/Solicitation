using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInteractController : MonoBehaviour
{
    [TextArea(1, 10)]
    public string textArea;

    public float interactRange = 3f;
    public LayerMask interactableLayer;
    public KeyCode interactKey = KeyCode.E;
    public KeyCode doorKey = KeyCode.Space;
    public string interactTag = "Cardboard";
    public string doorTag = "Door";
    public GameObject gaugeUI;
    public Slider gaugeSlider;
    public float gaugeIncreaseRate = 20f;
    public DialogueManager dialogueManager; // インスペクターで割り当て
    public GameOverEffect gameOverEffect;   // ゲームオーバーエフェクトコンポーネントへの参照

    private bool isInteracting = false;
    private float currentGauge = 0f;
    private RaycastHit currentHit;
    private int cardboardCount = 0;
    private Cardboard2 cardboard2;

    void Start()
    {
        gaugeUI.SetActive(false);

        if (gameOverEffect != null)
        {
            gameOverEffect.EnemyActive(false);
        }
        else
        {
            Debug.LogError("GameOverEffect component not found in the scene.");
        }
    }

    void Update()
    {
        CheckForInteractable();

        if (isInteracting)
        {
            StartCoroutine(UpdateGauge());
        }
        else
        {
            ResetGauge();
        }
    }

    /// <summary>
    /// Check for interactable objects in front of the player
    /// </summary>
    void CheckForInteractable()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, interactableLayer))
        {
            if (hit.collider.CompareTag(interactTag))
            {
                HandleInteraction(hit);
            }
            else if (hit.collider.CompareTag(doorTag))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    DoorController.OnDoorOpenAnimation.Invoke();
                }

                if(Input.GetKeyDown(KeyCode.C))
                {
                    DoorController.OnDoorCloseAnimation.Invoke();
                }
            }
        }
        else
        {
            gaugeUI.SetActive(false);
            isInteracting = false;
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="hit"></param>
    void HandleInteraction(RaycastHit hit)
    {
        currentHit = hit; // Store the current hit for gauge update
        gaugeUI.SetActive(true);

        GameObject obj = currentHit.collider.gameObject;
        
        if(obj != null)
        {
            // TODO:outline
            Outline outline = obj.GetComponent<Outline>();
            outline.enabled = true;
        }
        else
        {
            Debug.LogWarning("Interactable object not found.");
        }

        isInteracting = true;
    }

    /// <summary>
    /// Update the interaction gauge
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateGauge()
    {
        if (Input.GetKey(interactKey))
        {
            currentGauge += gaugeIncreaseRate * Time.deltaTime;
            gaugeSlider.value = currentGauge;

            if (currentGauge >= 100f)
            {
                currentGauge = 100f;
                gaugeUI.SetActive(false);

                GameObject obj = currentHit.collider.gameObject;

                // Play explosion sound at the object's position
                if (obj != null)
                {
                    SoundManager.Instance.PlaySE3D(SESoundData.SE.Explosion, obj.transform.position);

                    Outline outline = obj.GetComponent<Outline>();
                    outline.enabled = false;

                    ItemGenerator itemGenerator = obj.GetComponent<ItemGenerator>();
                    itemGenerator.GenerateItem();

                    // 段ボール撤去直後
                    if (dialogueManager != null)
                    {
                        dialogueManager.currentSituationID++; // 例：状況IDを1つ進める（または任意IDに切り替え）
                        dialogueManager.SetCurrentDialogue(dialogueManager.currentSituationID);
                    }

                    Item.onItemCollected.Invoke();
                    ItemText.onItemText.Invoke();
                    TipsUI.onDestroier.Invoke();

                    // Destroy the cardboard object
                    Destroy(obj);
                    currentHit = new RaycastHit(); // Clear current hit
                }

                cardboardCount++;
                UpdateUI();

                isInteracting = false;
                currentGauge = 0f;
                gaugeSlider.value = currentGauge;

                // Check if all cardboards for the day are opened
                if (cardboardCount >= GameManager.instance.dayCardboardRequirements[GameManager.instance.currentDay])
                {
                    // Proceed to next day or end game logic
                    GameManager.instance.currentDay++;
                    ResetGauge();
                    gaugeUI.SetActive(false);
                    isInteracting = false;

                    //ドアの効果音
                    SoundManager.Instance.PlaySE3D(SESoundData.SE.DoorOpen, transform.position);

                    // Close the door if it's open
                    Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, interactRange, interactableLayer))
                    {
                        if (hit.collider.CompareTag(doorTag))
                        {
                            DoorController.OnDoorOpenAnimation.Invoke();
                        }
                    }

                    yield return new WaitForSeconds(3f);

                    ShowSavePanel();

                    if (GameManager.instance.currentDay == 2)
                    {
                        GameDirector.instance.Date2Game();  // 2Day:cardboard

                        // UI
                        PlayerPrefs.SetInt("SCORE", 2); // SCORE
                        PlayerPrefs.SetInt("BUTTON", 2);
                        PlayerPrefs.SetInt("CLEAR", 0);
                        PlayerPrefs.Save();
                    }

                    if (GameManager.instance.currentDay == 3)
                    {
                        GameDirector.instance.Date3Game(); // 3Day:cardboard

                        // UI
                        PlayerPrefs.SetInt("SCORE", 3); // SCORE
                        PlayerPrefs.SetInt("BUTTON", 3);
                        PlayerPrefs.SetInt("CLEAR", 0);
                        PlayerPrefs.Save();
                    }

                    if (GameManager.instance.currentDay == 4)
                    {
                        GameDirector.instance.Date4Game(); // 4Day:cardboard

                        // UI
                        PlayerPrefs.SetInt("SCORE", 4); // SCORE
                        PlayerPrefs.SetInt("BUTTON", 4);
                        PlayerPrefs.SetInt("CLEAR", 0);
                        PlayerPrefs.Save();
                    }

                    if (GameManager.instance.currentDay == 5)
                    {
                        GameDirector.instance.Date5Game(); // 5Day:cardboard
                        gameOverEffect.EnemyActive(false);

                        // UI
                        PlayerPrefs.SetInt("SCORE", 5); // SCORE
                        PlayerPrefs.SetInt("BUTTON", 5);
                        PlayerPrefs.SetInt("CLEAR", 0);
                        PlayerPrefs.Save();
                    }

                    if (GameManager.instance.currentDay == 6)
                    {
                        GameDirector.instance.Date6Game(); // 6Day:cardboard

                        // UI
                        PlayerPrefs.SetInt("SCORE", 6); // SCORE
                        PlayerPrefs.SetInt("BUTTON", 6);
                        PlayerPrefs.SetInt("CLEAR", 0);
                        PlayerPrefs.Save();
                    }

                    if (GameManager.instance.currentDay == 7)
                    {
                        GameDirector.instance.Date7Game(); // 6Day:cardboard

                        // UI
                        PlayerPrefs.SetInt("SCORE", 7); // SCORE
                        PlayerPrefs.SetInt("BUTTON", 7);
                        PlayerPrefs.SetInt("CLEAR", 1);
                        PlayerPrefs.Save();
                    }

                    if (GameManager.instance.currentDay == 8)
                    {
                        GameManager.instance.SetIsGameOver(true);
                        yield break;
                    }

                    yield return new WaitForSeconds(3f);

                    cardboardCount = 0;
                    UpdateUI();
                }
            }
        }
    }

    void ShowSavePanel()
    {
        SavePanel.instance.Show();
    }

    void ResetGauge()
    {
        currentGauge = 0f;
        gaugeSlider.value = currentGauge;
    }

    public void UpdateUI()
    {
        int totalCardboards = 
            GameManager.instance.dayCardboardRequirements.ContainsKey(GameManager.instance.currentDay)
            ? GameManager.instance.dayCardboardRequirements[GameManager.instance.currentDay]
            : 0;

        UIManager.instance.UpdateDayUI(GameManager.instance.currentDay);
        UIManager.instance.UpdateCardboardCount(cardboardCount, totalCardboards);
    }
}