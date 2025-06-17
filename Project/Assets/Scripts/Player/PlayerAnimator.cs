using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private string HORIZONTALMOV = "horizontalMov";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveX = PlayerInputHandler.Instance.GetMovementDelta();
        animator.SetFloat(HORIZONTALMOV, moveX, 0.1f, Time.deltaTime);
    }
}
