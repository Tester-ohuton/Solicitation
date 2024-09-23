using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInteractController : MonoBehaviour
{
    [TextArea(1, 10)]
    public string textArea;

    public float interactRange = 3f;
    public LayerMask interactableLayer;
    public KeyCode interactKey = KeyCode.E;
    public string interactTag = "Cardboard";
    public string doorTag = "Door";
    public string entranceTag = "Entrance";
    public GameObject gaugeUI;
    public Slider gaugeSlider;
    public float gaugeIncreaseRate = 20f;

    private bool isInteracting = false;
    private bool isDoorOpen = false;
    private float currentGauge = 0f;
    private RaycastHit currentHit;
    private int cardboardCount = 0;

    private GameOverEffect gameOverEffect;

    void Start()
    {
        gaugeUI.SetActive(false);

        gameOverEffect = GameObject.Find("GameOverEffect").GetComponent<GameOverEffect>();

        if(gameOverEffect != null)
        {

        }
        else
        {

        }

        // Update the UI with the current day and cardboard count
        UpdateUI();
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

        if (Input.GetMouseButtonDown(0))
        {
            isDoorOpen = true;
        }
    }

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

                // �ȉ��̏������Q�[�W��100%�ɂȂ����Ƃ��ɍs������
                // Destroy(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag(doorTag))
            {
                HandleSimpleInteraction(hit);
                isDoorOpen = false;
            }
        }
        else
        {
            gaugeUI.SetActive(false);
            isInteracting = false;
        }
    }

    void HandleInteraction(RaycastHit hit)
    {
        currentHit = hit; // �q�b�g�����I�u�W�F�N�g��ێ�
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

        }

        isInteracting = true;
    }

    void HandleSimpleInteraction(RaycastHit hit)
    {
        DoorController animator = hit.collider.gameObject.GetComponentInChildren<DoorController>();
        if (animator != null)
        {
            Debug.Log("Door ��������܂����B");
            animator.OpenDoorAnimation();
        }
        else
        {
            Debug.LogWarning("Animator ��������܂���ł����B");
        }
    }

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

                // �Q�[�W��100%�ɂȂ����Ƃ���
                if (obj != null)
                {
                    SoundManager.Instance.PlaySE(SESoundData.SE.Comp, obj.transform.position);

                    Outline outline = obj.GetComponent<Outline>();
                    outline.enabled = false;

                    // Simulate changing cardboard state
                    int index = obj.GetComponent<Cardboard>().index;
                    GameManager.instance.cardboardStates[index] = true;

                    ItemGenerator itemGenerator = obj.GetComponent<ItemGenerator>();
                    itemGenerator.GenerateItem();

                    Item item = obj.GetComponent<Item>();
                    item.GetCardBoard();

                    ItemText.OnItemText.Invoke();

                    TipsUI tipsUI = obj.GetComponent<TipsUI>();
                    tipsUI.Destroier();

                    //Destroy(currentHit.collider.gameObject);
                    currentHit = new RaycastHit(); // ���݂̃q�b�g�������Z�b�g
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
                    GameManager.instance.currentDay++; // ���t�X�V
                    
                    // TODO: �h�A�������Ԃɖ߂�
                    DoorController.OnDoorCloseAnimation.Invoke();

                    // �v���C���[�ʒu������
                    GameManager.instance.ResetPlayerPos();

                    yield return new WaitForSeconds(3f);

                    // SavePanel�\��
                    ShowSavePanel();

                    if (GameManager.instance.currentDay == 2)
                    {
                        GameDirector.instance.Date2Game(); // 2����:cardboard

                        // UI
                        //PlayerPrefs��SCORE��2�Ƃ����l������
                        PlayerPrefs.SetInt("BUTTON", 2);
                        //PlayerPrefs���Z�[�u����         
                        PlayerPrefs.Save();
                    }
                    if (GameManager.instance.currentDay == 3)
                    {
                        GameDirector.instance.Date3Game(); // 3����:cardboard

                        // UI
                        //PlayerPrefs��SCORE��3�Ƃ����l������
                        PlayerPrefs.SetInt("BUTTON", 3);
                        //PlayerPrefs���Z�[�u����         
                        PlayerPrefs.Save();
                    }
                    if (GameManager.instance.currentDay == 4)
                    {
                        GameDirector.instance.Date4Game(); // 4����:cardboard

                        gameOverEffect.EnemyActive(true);
                    }

                    if (GameManager.instance.currentDay == 5)
                    {
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

    void UpdateUI()
    {
        int totalCardboards = 
            GameManager.instance.dayCardboardRequirements.ContainsKey(GameManager.instance.currentDay)
            ? GameManager.instance.dayCardboardRequirements[GameManager.instance.currentDay]
            : 0;

        UIManager.instance.UpdateDayUI(GameManager.instance.currentDay);
        UIManager.instance.UpdateCardboardCount(cardboardCount, totalCardboards);
    }
}