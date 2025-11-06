using UnityEngine;
using UnityEngine.Events;

public class DoorController : MonoBehaviour
{
    public bool isOpened;
    public int ID;

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
        isOpened = true;
        ShowButtonUI();
    }

    public void CloseDoorAnimation()
    {
        animator.SetBool("openAnim", false);
        isOpened = false;
    }

    private void ShowButtonUI()
    {
        UIManager.instance.ShowEventButton();
    }
}
