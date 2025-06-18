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
        Vector2 input = PlayerInputHandler.Instance.GetMovementDelta();

        float moveX = input.x;
        animator.SetFloat(HORIZONTALMOV, moveX, 0.1f, Time.deltaTime);
    }
}
