using UnityEngine;
using System.Collections;

public class BlueBaseScript : MonoBehaviour
{
    Transform Base;
    Transform BaseHit;
    Transform BaseDestroy;

    SpriteRenderer Sr_Base;
    SpriteRenderer Sr_BaseHit;
    SpriteRenderer Sr_BaseDestroy;

    float finalPosition = 0f;
    float speed = 3f;
    bool isBaseConstFinish = false;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip audioClip;


    float deltaTime;
    float timer = 14f;

    void Start()
    {
        Base = this.gameObject.transform;
        BaseHit = this.gameObject.transform.GetChild(0);
        BaseDestroy = this.gameObject.transform.GetChild(1);

        Sr_Base = Base.GetComponent<SpriteRenderer>();
        Sr_BaseHit = BaseHit.GetComponent<SpriteRenderer>();
        Sr_BaseDestroy = BaseDestroy.GetComponent<SpriteRenderer>();

        Sr_Base.enabled = false;
        Sr_BaseHit.enabled = false;
        Sr_BaseDestroy.enabled = true;

        audioSource = Base.GetComponent<AudioSource>();
        audioClip = audioSource.clip;

        deltaTime = Time.deltaTime;
    }

    void Update()
    {
        deltaTime += Time.deltaTime;

        if (deltaTime > timer)
            if (BaseDestroy.position.y < finalPosition)
            {
                BaseDestroy.Translate(0, speed * Time.deltaTime, 0f);
            }
            else if (!isBaseConstFinish)
            {
                StartCoroutine(AnimateBaseFadeInOut());
                isBaseConstFinish = true;
            }
    }

    private IEnumerator AnimateBaseFadeInOut()
    {
        // Fade out BaseDestroy
        float fadeOutDuration = 4f;
        float fadeOutTime = 0f;

        // Fade in Base
        Sr_Base.enabled = true;
        Sr_Base.color = new Color(Sr_Base.color.r, Sr_Base.color.g, Sr_Base.color.b, 0); // Start transparent
        float fadeInDuration = 3f;
        float fadeInTime = 0f;

        while (fadeOutTime < fadeOutDuration || fadeInTime < fadeInDuration)
        {
            // Fade out BaseDestroy
            if (fadeOutTime < fadeOutDuration)
            {
                fadeOutTime += Time.deltaTime;
                float alpha = 1 - Mathf.Clamp01(fadeOutTime / fadeOutDuration);
                Sr_BaseDestroy.color = new Color(Sr_BaseDestroy.color.r, Sr_BaseDestroy.color.g, Sr_BaseDestroy.color.b, alpha);
            }

            // Fade in Base
            if (fadeInTime < fadeInDuration)
            {
                fadeInTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(fadeInTime / fadeInDuration);
                Sr_Base.color = new Color(Sr_Base.color.r, Sr_Base.color.g, Sr_Base.color.b, alpha);
            }

            yield return null;
        }
        audioSource.PlayOneShot(audioClip);
    }
}
