using UnityEngine;
using UnityEngine.Events;

public class DoorController : MonoBehaviour
{
    public bool isOpened;
    public int ID;
    public GameObject doorObj;

    private Animator animator;

    public static UnityEvent OnDoorCloseAnimation = new UnityEvent();

    private void Awake()
    {
        OnDoorCloseAnimation.RemoveAllListeners();

        OnDoorCloseAnimation.AddListener(() =>
        {
            CloseDoorAnimation();
        });
    }

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void OpenDoorAnimation()
    {
        animator.SetBool("openAnim", true);

        doorObj.SetActive(false);
        isOpened = true;
        ShowButtonUI();
    }

    public void CloseDoorAnimation()
    {
        animator.SetBool("openAnim", false);
        doorObj.SetActive(true);
    }

    private void ShowButtonUI()
    {
        UIManager.instance.ShowEventButton();
    }
}
