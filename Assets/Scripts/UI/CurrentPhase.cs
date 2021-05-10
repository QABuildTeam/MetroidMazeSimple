using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MetroidMaze.UI
{
    public class CurrentPhase : MonoBehaviour
    {
        [SerializeField]
        private Animator characterAnimator;
        [SerializeField]
        private TextMeshProUGUI currentPhaseText;

        private string currentPhase;

        // Update is called once per frame
        void FixedUpdate()
        {
            string newPhase = characterAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            if (newPhase != currentPhase)
            {
                currentPhase = newPhase;
                currentPhaseText.text = currentPhase;
                //Debug.Log($"Current phase: {currentPhase}");
            }
        }
    }
}
