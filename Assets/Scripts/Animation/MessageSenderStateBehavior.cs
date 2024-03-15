using UnityEngine;

public class MessageSenderStateBehavior : StateMachineBehaviour
{
    [SerializeField] private EventTag Message;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SendMessage(Message);
    }
}