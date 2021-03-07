using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevelTrigger : MonoBehaviour
{
    public int Level = 0;
    public float TimeDelay = 1.5f;
    public int RequireItems = 0;
    public int Items = 0;
    public AudioSource audios;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() == null)
            return;

        if (RequireItems > Items)
            return;

        StartCoroutine(LoadNextLevel());
    }

    private IEnumerator LoadNextLevel()
    {
        if (audios != null)
            audios.Play();
        yield return new WaitForSeconds(TimeDelay);
        SceneManager.LoadScene(Level);
    }

}
