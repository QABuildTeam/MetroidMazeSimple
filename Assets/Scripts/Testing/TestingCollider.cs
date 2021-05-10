using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TestingCollider : MonoBehaviour
{
    private float xMove = 0;
    private float yMove = 0;
    [SerializeField]
    private float horizontalFactor = 0.4f;
    [SerializeField]
    private float verticalFactor = 0.4f;


    // Update is called once per frame
    void Update()
    {
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        float fixedDeltaTime = Time.fixedDeltaTime;
        transform.position += Vector3.right * fixedDeltaTime * xMove * horizontalFactor + Vector3.up * fixedDeltaTime * yMove * verticalFactor;
    }

    private int collisionCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        ++collisionCount;
        Debug.Log($"Enter trigger: other: name={other.gameObject.name}, trigger={other.isTrigger}, tag={other.gameObject.tag}, collisionCount={collisionCount}");
    }

    private void OnTriggerExit(Collider other)
    {
        --collisionCount;
        Debug.Log($"Exit trigger: other: name={other.gameObject.name}, trigger={other.isTrigger}, tag={other.gameObject.tag}, collisionCount={collisionCount}");
    }
}
