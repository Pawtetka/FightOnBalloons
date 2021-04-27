using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private int sceneIndex;

    public void LoadScene()
    {
        StartCoroutine(LoadAsynchronously(PlayerPrefs.GetInt("ActiveLevelIndex")));
    }

    private IEnumerator LoadAsynchronously(int sceneIndex)
    {
        var operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            var progress = Mathf.Clamp01(operation.progress / 0.9f);
            
            slider.value = progress;
            
            yield return null;
        }
    }
}
