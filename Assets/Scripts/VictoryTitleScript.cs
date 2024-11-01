using UnityEngine;
using System.Collections;

public class VictoryTitleScript : MonoBehaviour
{
    Transform VictoryTitle;
    SpriteRenderer Sr_VictoryTitle;

    Vector3 finalPosition = new Vector3(0, 2, 0);
    Vector3 finalScale = new Vector3(2, 2, 1);
    float finalRotationZ = 720f;  // Two full rotations (360 * 2)

    float duration = 2f;  // Duration of the entire animation
    bool isAnimFinish = false;
    float deltaTime;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip audioClip;

    float timer = 55f;

    void Start()
    {
        deltaTime = Time.deltaTime;

        VictoryTitle = this.gameObject.transform;
        Sr_VictoryTitle = VictoryTitle.GetComponent<SpriteRenderer>();

        Sr_VictoryTitle.enabled = false;
        VictoryTitle.localScale = new Vector3(0, 0, 1);  // Start at zero scale

        audioSource = VictoryTitle.GetComponent<AudioSource>();
        audioClip = audioSource.clip;
    }

    void Update()
    {
        deltaTime += Time.deltaTime;

        if (deltaTime > timer && !isAnimFinish)
        {
            StartCoroutine(AnimateMainTitle());
            isAnimFinish = true;
        }
    }

    private IEnumerator AnimateMainTitle()
    {
        Sr_VictoryTitle.enabled = true;
        audioSource.PlayOneShot(audioClip);

        Vector3 initialPosition = VictoryTitle.localPosition;
        Vector3 initialScale = VictoryTitle.localScale;
        float initialRotationZ = VictoryTitle.localEulerAngles.z;

        float elapsedTime = 0f;

        // Animate rotation, scale, and position
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Rotate: Lerp from initial to final rotation (2 full rotations)
            float newRotationZ = Mathf.Lerp(initialRotationZ, finalRotationZ, t);
            VictoryTitle.localEulerAngles = new Vector3(0, 0, newRotationZ);

            // Scale: Lerp from initial to final scale
            VictoryTitle.localScale = Vector3.Lerp(initialScale, finalScale, t);

            // Move: Lerp from initial to final position
            VictoryTitle.localPosition = Vector3.Lerp(initialPosition, finalPosition, t);

            yield return null;  // Wait for the next frame
        }

        // Ensure final transformation
        VictoryTitle.localEulerAngles = new Vector3(0, 0, finalRotationZ);
        VictoryTitle.localScale = finalScale;
        VictoryTitle.localPosition = finalPosition;

        // Wait for 5 seconds before disabling the sprite
        yield return null;
    }
}
