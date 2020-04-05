using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingPlayer : MonoBehaviour
{
    public bool Teleporting;

    public float time;

    public void Teleport()
    {
        Teleporting = true;

        StartCoroutine(WaitAndReset());
    }

    private IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(time);
        Teleporting = false;
    }
}
