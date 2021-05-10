using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze
{
    [RequireComponent(typeof(Light))]
    [RequireComponent(typeof(SphereCollider))]
    public class LevelPointLightTrigger : MonoBehaviour
    {
        private Light currentLight;
        private SphereCollider currentCollider;
        [SerializeField]
        private string playerTag = "Player";
        [SerializeField]
        private float minIntensity = 0;
        [SerializeField]
        private float maxIntensity = 1;
        [SerializeField]
        private float intensityFadeDuration = 0.5f;

        private void Awake()
        {
            currentLight = GetComponent<Light>();
            currentCollider = GetComponent<SphereCollider>();
            currentCollider.isTrigger = true;
            currentIntensity = minIntensity;
            SetLightIntensity(currentIntensity);
        }

        private void RestartFader(Collider other, IEnumerator fader)
        {
            if (other.gameObject.CompareTag(playerTag))
            {
                if (fadeLightCoroutine != null)
                {
                    StopCoroutine(fadeLightCoroutine);
                }
                fadeLightCoroutine = StartCoroutine(fader);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            RestartFader(other, FadeOutLight());
        }

        private void OnTriggerExit(Collider other)
        {
            RestartFader(other, FadeInLight());
        }

        private void SetLightIntensity(float intensity)
        {
            currentLight.intensity = intensity;
        }

        private Coroutine fadeLightCoroutine = null;
        private float currentIntensity;
        IEnumerator FadeOutLight()
        {
            yield return FadeLight(maxIntensity, -1);
        }

        IEnumerator FadeInLight()
        {
            yield return FadeLight(minIntensity, 1);
        }

        IEnumerator FadeLight(float endIntensity, int compareValue)
        {
            float deltaIntensity = (maxIntensity - minIntensity) * Time.deltaTime / intensityFadeDuration * (-compareValue);
            while (currentIntensity.CompareTo(endIntensity) == compareValue)
            {
                SetLightIntensity(currentIntensity);
                yield return null;
                currentIntensity += deltaIntensity;
            }
            currentIntensity = endIntensity;
            SetLightIntensity(currentIntensity);
        }
    }
}
