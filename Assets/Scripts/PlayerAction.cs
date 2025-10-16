using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerAction : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    public List<Slider> interactionSliders = new List<Slider>();
    public List<GameObject> sliderPanels = new List<GameObject>();
    public GameObject cardboardOpen;
    public float currentValue;
    public Image targetImage;

    [Header("SelectButtonsPanel")]
    [SerializeField] Animator XAnim;

    [Header("PlayerMovement")]
    [SerializeField] PlayerMovement playerMovement;

    private bool isInteracting = false;
    private float interactionProgress = 0f;
    private int currentSliderIndex = 0;
    private GameObject currentHitObject = null;
    private bool isInteract = false;

    private void Start()
    {
        foreach (var panel in sliderPanels)
        {
            panel.SetActive(false);
        }
    }

    void Update()
    {

        HandleMouseInput();

        if (isInteracting)
        {
            HandleInteractionProgress();
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 画面の中心の座標を計算
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

            // 画面の中心からRayを飛ばす
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);

            // デバッグ用にRayを描画（オプション）
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit,3f))
            {
                currentHitObject = hit.collider.gameObject;
                HandleObjectInteraction(currentHitObject);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            ResetInteraction();
        }
    }

    void HandleObjectInteraction(GameObject hitObject)
    {
        string tag = hitObject.tag;
        if (tag == "Door" || tag == "Entrance")
        {
            HandleSimpleInteraction(hitObject, tag);
        }
        else if (tag == "Cardboard")
        {
            HandleCardInteraction(hitObject);
        }
    }

    void HandleSimpleInteraction(GameObject hitObject, string tag)
    {
        Red();

        SelectButtonsPanelAnim();

        Animator animator = hitObject.GetComponentInParent<Animator>();
        Debug.Log($"{tag}Open");
        animator.SetBool("openAnim", true);
    }

    void HandleCardInteraction(GameObject hitObject)
    {
        Red();
        Item item = hitObject.GetComponentInParent<Item>();
        Debug.Log($"{item}Get");
        item.GetCardBoard();

        Outline outline = hitObject.GetComponent<Outline>();

        if (!isInteracting)
        {
            sliderPanels[currentSliderIndex].SetActive(true);
            isInteracting = true;
            outline.enabled = true;
        }
    }

    void HandleInteractionProgress()
    {
        interactionProgress += Time.deltaTime * currentValue;
        interactionSliders[currentSliderIndex].value = interactionProgress;

        if (interactionProgress >= 100)
        {
            CompleteInteraction();
        }
    }

    void CompleteInteraction()
    {
        ItemText.onItemText.Invoke();
        sliderPanels[currentSliderIndex].SetActive(false);
        currentSliderIndex = (currentSliderIndex + 1) % interactionSliders.Count;

        SelectButtonsPanelAnim();

        Instantiate(cardboardOpen, currentHitObject.transform.position, Quaternion.identity);
        currentHitObject.SetActive(false);

        Outline outline = currentHitObject.GetComponent<Outline>();
        outline.enabled = false;

        interactionProgress = 0;
        isInteracting = false;
        currentHitObject = null;
    }

    void SelectButtonsPanelAnim()
    {
        XAnim.SetBool("AnimStart", true); // 見える状態
        playerMovement.UpdateCursorLock(false);
        Debug.Log("Test Debug SelectButtonsPanelAnim true");
    }

    void ResetInteraction()
    {
        isInteracting = false;
        if (currentHitObject != null)
        {
            sliderPanels[currentSliderIndex].SetActive(false);
            currentHitObject = null;
        }
        ResetTargetImage();
    }

    void Red()
    {
        targetImage.color = Color.red;
    }

    void ResetTargetImage()
    {
        targetImage.color = Color.white;
    }
}
