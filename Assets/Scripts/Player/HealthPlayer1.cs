using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class HealthPlayer1 : MonoBehaviour
{
    public bool imLife = true;

    public Light2D lightVolumeOpacity;
    float lightIntensity = 0.1f;
    Material material;

    float fade = 1f;


    public float maxLight = 1.5f;
    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }
    void Update()
    {
        if (!imLife)
        {
            fade -= Time.deltaTime;
            if (fade <= 0)
            {
                fade = 0f;
                StartCoroutine(Load());
            }
            material.SetFloat("_Fade", fade);
        }
    }
    public void MinusIntensity(float intensity)
    {

        lightVolumeOpacity.intensity -= intensity;

        if (lightVolumeOpacity.intensity <= 0)
        {
            imLife = false;
        }
    }
    public void PlusIntensity()
    {
        lightVolumeOpacity.intensity += 1;

        if (lightVolumeOpacity.intensity > maxLight)
        {
            lightVolumeOpacity.intensity = maxLight;
        }
    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            MinusIntensity(lightIntensity);
            Destroy(collision.gameObject);
            print("damage");
        }

    }
    
    IEnumerator Load()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //GameManager.Instance.LoadScene();
    }
}