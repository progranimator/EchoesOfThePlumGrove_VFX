using UnityEngine;

namespace ButterflyManager
{
    public class Butterfly_PlayAnim : MonoBehaviour
    {
        public Animator animator; // reference to the Animator component
        public float animationSpeed = 1.0f; // public variable to adjust the speed of the animation

        void Start()
        {
            animator = GetComponent<Animator>(); // get the Animator component on the same game object
            //animator.Play("YourAnimationName"); // play the animation with the specified name
        }

        void Update()
        {
            animator.speed = animationSpeed; // set the speed parameter of the Animator component to the value of the public variable
        }
    }
}