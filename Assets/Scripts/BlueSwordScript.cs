using UnityEngine;
using System.Collections;

public class BlueSwordScript : MonoBehaviour
{

    float speed = 1f;

    bool isFightingActionFinish = false;
    bool isEnemyBaseDestroy = false;

    float deltaTime;
    float timer = 7f;
    float timer2 = 21f;
    float timer3 = 24f;
    float timer4 = 48f;

    const float position1 = -3f;
    const float position2 = -0.3f;
    const float position3 = 4f;

    Transform CharacterIdle;
    Transform CharacterAttack;
    Transform CharacterHit;
    Transform CharacterHitRed;
    Transform CharacterDie;

    SpriteRenderer Sr_CharacterIdle;
    SpriteRenderer Sr_CharacterAttack;
    SpriteRenderer Sr_CharacterHit;
    SpriteRenderer Sr_CharacterHitRed;
    SpriteRenderer Sr_CharacterDie;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip audioClip;

    void Start()
    {
        CharacterIdle = this.gameObject.transform;
        CharacterAttack = this.gameObject.transform.GetChild(0);
        CharacterHit = this.gameObject.transform.GetChild(1);
        CharacterHitRed = this.gameObject.transform.GetChild(2);
        CharacterDie = this.gameObject.transform.GetChild(3);

        Sr_CharacterIdle = CharacterIdle.GetComponent<SpriteRenderer>();
        Sr_CharacterAttack = CharacterAttack.GetComponent<SpriteRenderer>();
        Sr_CharacterHit = CharacterHit.GetComponent<SpriteRenderer>();
        Sr_CharacterHitRed = CharacterHitRed.GetComponent<SpriteRenderer>();
        Sr_CharacterDie = CharacterDie.GetComponent<SpriteRenderer>();

        Sr_CharacterIdle.enabled = true;
        Sr_CharacterAttack.enabled = false;
        Sr_CharacterHit.enabled = false;
        Sr_CharacterHitRed.enabled = false;
        Sr_CharacterDie.enabled = false;

        audioSource = CharacterAttack.GetComponent<AudioSource>();
        audioClip = audioSource.clip;

        deltaTime = Time.deltaTime;
    }

    void Update()
    {
        deltaTime += Time.deltaTime;

        if (deltaTime > timer && CharacterIdle.position.x < position1)
        {
            CharacterIdle.Translate(speed * Time.deltaTime, 0, 0f);
        }
        if (deltaTime > timer2 && CharacterIdle.position.x < position2)
        {
            CharacterIdle.Translate(speed * Time.deltaTime, 0, 0f);
        }
        if (deltaTime > timer3 && !isFightingActionFinish)
        {
            StartCoroutine(FightTillMorning());
            isFightingActionFinish = true;
        }
        if (deltaTime > timer4 && CharacterIdle.position.x < position3)
        {
            isFightingActionFinish = true;
            Sr_CharacterIdle.enabled = true;
            Sr_CharacterAttack.enabled = false;
            CharacterIdle.Translate(speed * Time.deltaTime, 0, 0f);
        }
        else if (CharacterIdle.position.x >= position3 && !isEnemyBaseDestroy)
        {
            isEnemyBaseDestroy = true;
            StartCoroutine(HitEnemyBase());
        }

    }

    private IEnumerator FightTillMorning()
    {
        float soundPlayTimer = 0f; // Timer to track sound playing

        while (deltaTime <= 48f)
        {
            // Character switches to attack mode
            Sr_CharacterIdle.enabled = false;
            Sr_CharacterAttack.enabled = true;

            // Play sound only if 1 second has passed
            if (soundPlayTimer >= 1f)
            {
                audioSource.PlayOneShot(audioClip);
                soundPlayTimer = 0f; // Reset timer after playing the sound
                Sr_CharacterIdle.enabled = true;
                Sr_CharacterAttack.enabled = false;
            }

            // Wait for 0.5 seconds, then switch back to idle mode
            yield return new WaitForSeconds(0.5f);

            // Update the sound timer
            soundPlayTimer += 0.5f;
        }

        yield return null;
        isFightingActionFinish = false;
    }

    private IEnumerator HitEnemyBase()
    {
        Sr_CharacterIdle.enabled = false;
        Sr_CharacterAttack.enabled = true;
        audioSource.PlayOneShot(audioClip);
        yield return new WaitForSeconds(0.5f);
        isEnemyBaseDestroy = true;
        Sr_CharacterIdle.enabled = true;
        Sr_CharacterAttack.enabled = false;
    }
}
