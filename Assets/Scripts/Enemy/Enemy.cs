using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameObject gameObj = collision.gameObject;

        if(collision.gameObject.tag == "Player")
        {
            PlayerLife playerLife = gameObj.GetComponent<PlayerLife>();
            playerLife.Damage(10);
            Debug.Log($"player‚Ìƒ‰ƒCƒt: {playerLife.playerLife}");
        }
    }
}
