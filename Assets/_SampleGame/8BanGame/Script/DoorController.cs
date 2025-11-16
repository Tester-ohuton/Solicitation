using UnityEngine;
using UnityEngine.Events;

public class DoorController : MonoBehaviour
{
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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnDoorOpenAnimation.Invoke();
        }
    }

    public void OpenDoorAnimation()
    {
        animator.SetBool("openAnim", true);
        ShowButtonUI();
    }

    public void CloseDoorAnimation()
    {
        animator.SetBool("openAnim", false);
    }

    private void ShowButtonUI()
    {
        UIManager.instance.ShowEventButton();
    }
}
