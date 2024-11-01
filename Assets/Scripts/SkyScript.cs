using UnityEngine;

public class SkyScript : MonoBehaviour
{
    float deltaTime;

    float timer = 34f;

    bool isAnimationFinish = false;

    [Header("NightSkyAnimation")]
     float halfDuration = 6f; // How long the transition will take

    Color initialColor;
    Color blackColor;

    Transform skyobject;
    SpriteRenderer Sr_Sky;

    void Start()
    {
        deltaTime = Time.deltaTime;

        skyobject = this.gameObject.transform;
        Sr_Sky = skyobject.GetComponent<SpriteRenderer>();

        // Get initial color
        initialColor = Sr_Sky.color;
        blackColor = Color.black;
    }

    void Update()
    {
        deltaTime += Time.deltaTime;

        if (deltaTime > timer && !isAnimationFinish)
        {
            StartCoroutine(NightSkyAnimation());
            isAnimationFinish = true;
        }
    }

    private System.Collections.IEnumerator NightSkyAnimation()
    {
        float elapsedTime = 0f;

        // Fade from the initial color to black
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            Sr_Sky.color = Color.Lerp(initialColor, blackColor, elapsedTime / halfDuration);
            yield return null;  // Wait for the next frame
        }

        // Reset the elapsed time for the second transition
        elapsedTime = 0f;

        // Fade from black back to the initial color
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            Sr_Sky.color = Color.Lerp(blackColor, initialColor, elapsedTime / halfDuration);
            yield return null;  // Wait for the next frame
        }
    }


}
