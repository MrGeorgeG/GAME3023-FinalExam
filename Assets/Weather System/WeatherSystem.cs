using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    [SerializeField] GameObject leafParticle;
    [SerializeField] GameObject rainParticle;
    [SerializeField] GameObject snowParticle;

    [SerializeField] Animator WeatherAnimator;
    [SerializeField] Animator ThunderAnimator;

    [SerializeField] AudioSource music;
    [SerializeField] AudioClip morning;
    [SerializeField] AudioClip night;
    [SerializeField] AudioClip rain;
    [SerializeField] AudioClip snow;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 5f;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = 1f;
        }
    }

    //Morning
    public void StartSunny() 
    {
        //if Rainning, stop
        if (rainParticle.activeSelf)
        {
            if (ThunderAnimator.isActiveAndEnabled) //Check Animator and Stop
            {
                ThunderAnimator.SetBool("Thunder", false);
                Invoke("StopThunderAnimation", 0.5f);
            }

            music.Stop();
            rainParticle.SetActive(false);
        }

        //Play Snow
        int i = Random.Range(0, 3);
        if (i == 2)
        {
            snowParticle.SetActive(true);
            music.clip = snow;
            music.Play();
        }

        music.clip = morning;
        music.Play();
    }

    //Afternoon
    public void StartSunset() 
    {
        music.Stop();

        //Play Rain
        int i = Random.Range(0, 5); 
        if (i <= 3)
        {
            //Stop Snow
            if (snowParticle.activeSelf) 
            {
                snowParticle.SetActive(false);
                music.clip = snow;
                music.Stop();
            }

            if (!rainParticle.activeSelf)  //Check Rain and Play
            {
                rainParticle.SetActive(true);
                music.clip = rain;
                music.Play();
            }
            else if (Random.Range(0, 5) > 2)  // Check Rain work and Play thunder
            {
                if (!IsInvoking("StartThunder")) //Check thunder.
                {
                    Invoke("StartThunder", 2f);
                }
            }
        }

        //Play Snow
        else if(i >= 4) 
        {
            snowParticle.SetActive(true);
            music.clip = snow;
            music.Play();
        }

        //Stop and Play Fall
        else
        {
            if (rainParticle.activeSelf)
            {
                if (ThunderAnimator.isActiveAndEnabled)
                {
                    ThunderAnimator.SetBool("Thunder", false);
                    Invoke("StopThunderAnimation", 0.5f);
                }
                music.Stop();
                rainParticle.SetActive(false);
            }

            if (snowParticle.activeSelf)
            {
                snowParticle.SetActive(false);
                music.clip = snow;
                music.Stop();
            }

            if (!leafParticle.activeSelf)  //Check rain and Play
            {
                leafParticle.SetActive(true);
            }
        }
    }

    // Night
    public void StartOvercast() 
    {
        if (leafParticle.activeSelf)
        {
            leafParticle.SetActive(false);
        }

        int i = Random.Range(0, 5);

        //Play Rain
        if (i <= 3) 
        {
            //Stop Snow
            if (snowParticle.activeSelf) 
            {
                snowParticle.SetActive(false);
                music.clip = snow;
                music.Stop();
            }

            if (!rainParticle.activeSelf)  //Check Rain and Play
            {
                rainParticle.SetActive(true);
                music.clip = rain;
                music.Play();
            }
            else if (Random.Range(0, 5) > 2)  // Check Rain work and Play thunder
            {
                if (!IsInvoking("StartThunder")) //Check thunder.
                {
                    Invoke("StartThunder", 2f);
                }
            }
        }
        else
        {
            //Play Snow
            if (i >= 4)
            {
                if (rainParticle.activeSelf)
                {
                    if (ThunderAnimator.isActiveAndEnabled)
                    {
                        ThunderAnimator.SetBool("Thunder", false);
                        Invoke("StopThunderAnimation", 0.5f);
                    }
                    music.Stop();
                    rainParticle.SetActive(false);
                }

                snowParticle.SetActive(true);
                music.clip = snow;
                music.Play();
            }

            //Stop
            else
            {
                if (rainParticle.activeSelf)
                {
                    if (ThunderAnimator.isActiveAndEnabled)
                    {
                        ThunderAnimator.SetBool("Thunder", false);
                        Invoke("StopThunderAnimation", 0.5f);
                    }
                    music.Stop();
                    rainParticle.SetActive(false);
                }

                //Stop Snow
                if (snowParticle.activeSelf) 
                {
                    snowParticle.SetActive(false);
                    music.clip = snow;
                    music.Stop();
                }
            }
            music.clip = night;
            music.Play();
        }
    }
}
