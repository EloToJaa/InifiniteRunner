using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScript : MonoBehaviour
{
    public float floatingHeight = 0.5f;

    private int count = 0;
    private bool up = true;
    private const int STEPS = 25;

    private void FixedUpdate()
    {
        Vector3 move = up ? new Vector3(0f, floatingHeight / STEPS, 0f) : new Vector3(0f, -floatingHeight / STEPS, 0f);

        transform.position += move;
        count++;

        if(count == STEPS)
        {
            count = 0;
            up = !up;
        }
    }
}
