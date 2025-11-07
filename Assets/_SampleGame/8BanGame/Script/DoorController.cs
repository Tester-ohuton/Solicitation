using UnityEngine;
using UnityEngine.Events;

public class DoorController : MonoBehaviour
{
    public bool isOpened;
    public int ID;

    private Animator animator;

    public static UnityEvent OnDoorOpenAnimation = new UnityEvent();
    public static UnityEvent OnDoorCloseAnimation = new UnityEvent();

    private void Start()
    {
        OnDoorCloseAnimation.RemoveAllListeners();
        OnDoorOpenAnimation.RemoveAllListeners();

        OnDoorCloseAnimation.AddListener(() =>
        {
            CloseDoorAnimation();
        });


        OnDoorOpenAnimation.AddListener(() =>
        {
            OpenDoorAnimation();
        });

        animator = GetComponentInParent<Animator>();
    }

    public void OpenDoorAnimation()
    {
        animator.SetBool("openAnim", true);
        isOpened = true;
        gameObject.SetActive(false);
        ShowButtonUI();
    }

    public void CloseDoorAnimation()
    {
        animator.SetBool("openAnim", false);
        isOpened = false;
        gameObject.SetActive(true);
    }

    private void ShowButtonUI()
    {
        UIManager.instance.ShowEventButton();
    }
}
