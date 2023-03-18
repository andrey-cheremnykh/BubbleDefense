using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{
    [SerializeField] RectTransform fillImage;

    float y = -14;
    float startX = -14;
    float endX = -490;

    bool isDecreasing = false;

    // Start is called before the first frame update
    void Start()
    {
        //
    }

    public IEnumerator DecreaseBar(float duration)
    {
        float timer = 0;
        isDecreasing = true;
        GetComponentInChildren<Text>().text = "Building Phase";

        while (timer < 1 && isDecreasing == true)
        {
            timer += Time.deltaTime / duration;
            float x = Mathf.Lerp(startX, endX, timer);
            fillImage.offsetMax = new Vector2(x, y);
            yield return null;
        }
    }

    public void DisplayNewWave(int waveCount)
    {
        isDecreasing = false;
        GetComponentInChildren<Text>().text = "Wave " + (waveCount + 1);
        fillImage.offsetMax = new Vector2(startX, y);
    }

    
}
