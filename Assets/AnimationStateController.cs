using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        // increases performance
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        bool isrunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        // if player presses w key    
         if (!isWalking && forwardPressed)
         {
            //then set the isWalking Boolen to true
            animator.SetBool(isWalkingHash, true);
         }

         //if player is not pressing w key
         if (isWalking && !forwardPressed)
         {
            //then set the isWalking Boolen to false
            animator.SetBool(isWalkingHash, false);
         }

         //if player is walking and not running and presses left shift
         if (!isrunning && (forwardPressed && runPressed))
         {
            //then se the isRunning Boolen to true
            animator.SetBool(isRunningHash, true);
         }

         //if player is running and stops running or walking
         if (isrunning && (!forwardPressed || !runPressed))
         {
            //then set the isRunning Boolen to false
            animator.SetBool(isRunningHash, false);
         }
    }
}
