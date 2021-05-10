using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PillarRolling
{
    public class BoulderDestroyer : MonoBehaviour
    {
        [SerializeField]
        private string boulderTag = "Boulder";
        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log($"Collided with object \"{other.gameObject.tag}\"");
            if (other.gameObject.CompareTag(boulderTag))
            {
                //Debug.Log($"Starting destruction of {other.gameObject.name}");
                StartCoroutine(DestroyBoulder(other.gameObject));
            }
        }

        [SerializeField]
        private float destroyTime = 0.5f;
        private IEnumerator DestroyBoulder(GameObject boulder)
        {
            MeshRenderer mr = boulder.GetComponent<MeshRenderer>();
            if (mr != null)
            {
                Material[] materials = mr.materials;
                if (materials != null)
                {
                    float currentTime = 0;
                    while (currentTime < destroyTime)
                    {
                        yield return null;
                        currentTime += Time.deltaTime;
                        Material[] newMaterials = new Material[materials.Length];
                        for (int i = 0; i < newMaterials.Length; ++i)
                        {
                            newMaterials[i] = new Material(materials[i]);
                            var c = newMaterials[i].color;
                            c.a = Mathf.Lerp(0, 1, (destroyTime - currentTime) / destroyTime);
                            newMaterials[i].color = c;
                        }
                        if (mr != null)
                        {
                            mr.materials = newMaterials;
                        }
                    }
                }
                else
                {
                    //Debug.Log("No materials");
                }
            }
            else
            {
                //Debug.Log("No mesh renderer");
            }
            Destroy(boulder);
        }
    }
}
