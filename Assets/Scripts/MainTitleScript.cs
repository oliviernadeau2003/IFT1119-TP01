using UnityEngine;
using System.Collections;

public class MainTitleScript : MonoBehaviour
{
    Transform MainTitle;
    SpriteRenderer Sr_MainTitle;

    Vector3 finalPosition = new Vector3(0, 2, 0);
    Vector3 finalScale = new Vector3(2, 2, 1);
    float finalRotationZ = 720f;  // Two full rotations (360 * 2)

    float duration = 4f;  // Duration of the entire animation
    bool isAnimFinish = false;

    void Start()
    {
        MainTitle = this.gameObject.transform;
        Sr_MainTitle = MainTitle.GetComponent<SpriteRenderer>();

        MainTitle.localScale = new Vector3(0, 0, 1);  // Start at zero scale

        Sr_MainTitle.enabled = true;
    }

    void Update()
    {
        if (!isAnimFinish)
        {
            StartCoroutine(AnimateMainTitle());
            isAnimFinish = true;
        }
    }

    private IEnumerator AnimateMainTitle()
    {
        Vector3 initialPosition = MainTitle.localPosition;
        Vector3 initialScale = MainTitle.localScale;
        float initialRotationZ = MainTitle.localEulerAngles.z;

        float elapsedTime = 0f;

        // Animate rotation, scale, and position
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Rotate: Lerp from initial to final rotation (2 full rotations)
            float newRotationZ = Mathf.Lerp(initialRotationZ, finalRotationZ, t);
            MainTitle.localEulerAngles = new Vector3(0, 0, newRotationZ);

            // Scale: Lerp from initial to final scale
            MainTitle.localScale = Vector3.Lerp(initialScale, finalScale, t);

            // Move: Lerp from initial to final position
            MainTitle.localPosition = Vector3.Lerp(initialPosition, finalPosition, t);

            yield return null;  // Wait for the next frame
        }

        // Ensure final transformation
        MainTitle.localEulerAngles = new Vector3(0, 0, finalRotationZ);
        MainTitle.localScale = finalScale;
        MainTitle.localPosition = finalPosition;

        // Wait for 5 seconds before disabling the sprite
        yield return new WaitForSeconds(5f);

        // Disable the sprite
        Sr_MainTitle.enabled = false;
    }
}
