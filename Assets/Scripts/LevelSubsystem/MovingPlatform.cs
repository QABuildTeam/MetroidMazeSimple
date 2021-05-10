using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidMaze
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField]
        private Vector3 point0;
        [SerializeField]
        private float point0Delay;
        [SerializeField]
        private Vector3 point1;
        [SerializeField]
        private float point1Delay;
        [SerializeField]
        private Vector3 speed;

        // Start is called before the first frame update
        void Start()
        {
            transform.localPosition = point0;
            StartCoroutine(Movement());
        }

        private float GetTime(float x0, float x1, float v)
        {
            if (v != 0)
            {
                float t = (x1 - x0) / v;
                if (t > 0)
                {
                    return t;
                }
            }
            return 0;
        }

        private float GetMovementTime(Vector3 p0, Vector3 p1, Vector3 v)
        {
            float t = GetTime(p0.x, p1.x, v.x);
            if (t > 0)
            {
                return t;
            }
            t = GetTime(p0.y, p1.y, v.y);
            if (t > 0)
            {
                return t;
            }
            t = GetTime(p0.z, p1.z, v.z);
            if (t > 0)
            {
                return t;
            }
            return 0;
        }

        public float movementTime = 0;

        private IEnumerator Movement()
        {
            transform.localPosition = point0;
            movementTime = GetMovementTime(point0, point1, speed);
            if (movementTime > 0)
            {
                float currentTime = 0;
                while (true)
                {
                    // wait at point0
                    while (currentTime < point0Delay)
                    {
                        yield return null;
                        currentTime += Time.deltaTime;
                    }
                    currentTime -= point0Delay;
                    Vector3 pos = point0;
                    // move from point0 to point1
                    while (currentTime < movementTime)
                    {
                        yield return null;
                        float deltaTime = Time.deltaTime;
                        pos += speed * deltaTime;
                        transform.localPosition = pos;
                        currentTime += deltaTime;
                    }
                    transform.localPosition = point1;
                    currentTime -= movementTime;
                    // wait at point1
                    while (currentTime < point1Delay)
                    {
                        yield return null;
                        currentTime += Time.deltaTime;
                    }
                    currentTime -= point1Delay;
                    // move from point1 to point0
                    pos = point1;
                    while (currentTime < movementTime)
                    {
                        yield return null;
                        float deltaTime = Time.deltaTime;
                        pos -= speed * deltaTime;
                        transform.localPosition = pos;
                        currentTime += deltaTime;
                    }
                    transform.localPosition = point0;
                    currentTime -= movementTime;
                }
            }
        }
    }
}
