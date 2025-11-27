using UnityEngine;
using UnityEngine.Events;

public class DoorController : MonoBehaviour
{
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
            // ‰¹‚Ìˆ—‚ğÀ‘•‚·‚éê‡‚Í‚±‚±‚É’Ç‰Á
            // TODO: Play door opening sound effect
            

            OpenDoorAnimation();
        });

        animator = GetComponentInParent<Animator>();
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
