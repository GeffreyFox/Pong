using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    #region Methods
    public IEnumerator Shake(float duration, float shaking)
    {
        var camPos = transform.position;
        float timer = 0f;
        
        while (timer < duration)
        {
            float newX = Random.Range(-1f, 1f) * shaking + camPos.x;
            float newY = Random.Range(-1f, 1f) * shaking + camPos.y;

            transform.position = new Vector3(newX, newY, camPos.z);
            timer += Time.deltaTime;
            yield return 0;
        }
        
        transform.position = camPos;
    }
    
    #endregion
}
