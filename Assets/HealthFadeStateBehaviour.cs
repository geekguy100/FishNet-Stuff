using UnityEngine;

public class HealthFadeStateBehaviour : StateMachineBehaviour
{
    /// <summary>
    /// Destroys the health display after fading out.
    /// </summary>
    /// <param name="animator">The Animator of the health display.</param>
    /// <param name="stateInfo">The associated AnimatorStateInfo.</param>
    /// <param name="layerIndex">The layer index of the animation.</param>
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject);
    }
}
