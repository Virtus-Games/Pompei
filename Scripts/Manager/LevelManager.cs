using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
     private const string _level = "Level ";
     private const string _currentLevel = "currentLevel";
     private Scene _lastLoadedScene;
     public static event UnityAction<bool> OnLevelLoaded;


     private void Awake()
     {

          //AdmobManager.Instance.InitiliazedAds();
          LevelLoad();

     }

     private void LevelLoad()
     {

          if (!PlayerPrefs.HasKey("currentLevel"))
          {
               SceneLoader(_level + 1);
               currentLevel = 1;
               currentIndex = 1;
               PlayerPrefs.SetInt(_currentLevel, currentLevel);
               PlayerPrefs.SetInt("currentIndex", currentIndex);
          }
          else
               SceneLoader(_level + 1);
     }

     public void SceneLoader(string name) => ChangeScene(name);

     void ChangeScene(string sceneName)
     {
          StartCoroutine(SceneController(sceneName));
     }

     IEnumerator SceneController(string sceneName)
     {
          OnLevelLoaded?.Invoke(false);

          if (_lastLoadedScene.IsValid())
          {
               SceneManager.UnloadSceneAsync(_lastLoadedScene);
               bool isUnloadScene = false;
               while (!isUnloadScene)
               {
                    isUnloadScene = !_lastLoadedScene.IsValid();
                    yield return new WaitForEndOfFrame();
               }
          }

          SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

          bool isSceneLoaded = false;

          while (!isSceneLoaded)
          {
               _lastLoadedScene = SceneManager.GetSceneByName(sceneName);
               isSceneLoaded = _lastLoadedScene != null && _lastLoadedScene.isLoaded;

               yield return new WaitForEndOfFrame();
          }

          OnLevelLoaded?.Invoke(true);

     }

     [HideInInspector]
     public int currentLevel;
     int currentIndex = 1;

     public void NextLevel()
     {
          currentLevel = PlayerPrefs.GetInt(_currentLevel);
          currentIndex = PlayerPrefs.GetInt("currentIndex");
          // 

          currentIndex++;

          if (currentIndex % 2 == 0)
               currentLevel++;

               // 1 
               // 2 
               // 3 
               // 4  

          if (currentIndex % 2 == 0)
          {
               if (currentLevel >= SceneManager.sceneCountInBuildSettings)
               {
                    currentLevel = 1;
                    currentIndex = 1;
                    PlayerPrefs.SetInt(_currentLevel, currentLevel);
                    PlayerPrefs.SetInt("currentIndex", currentIndex);
                    SceneLoader(_level + currentLevel);
               }
               else
               {
                    SceneLoader(_level + currentLevel);
                    PlayerPrefs.SetInt(_currentLevel, currentLevel);
               }
          }
          else
               SceneLoader(_level + 1);

          PlayerPrefs.SetInt("currentIndex", currentIndex);

     }
     public void RestartLevel()
     {
          LevelLoad();
     }
}
