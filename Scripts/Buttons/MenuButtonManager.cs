using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButtonManager : MonoBehaviour
{
     [TextArea(3, 5)]
     public string buttonDetails;

     // StartPanel Component Button
     public void TabToStart()
     {
          PlayerController.Instance.TotalStop = false;
          AiController.Instance.TotalStop = false;
          GameManager.Instance.UpdateGameState(GAMESTATE.PLAY);
     }


     // StartPanel Market Icon Component Button
     public void MarketButton()
     {
          GameManager.Instance.UpdateGameState(GAMESTATE.MARKET);
     }

     // StartPanel -> Top Panel -> Settings Icon Component Button
     public void Settings(GameObject obj)
     {
          Time.timeScale = 0;
          obj.SetActive(true);
     }


     // Victory Panel -> Next Level Compoenet Button
     public void LevelLoader()
     {
          // LevelManager.Instance.SceneLoader(PlayerDataController.playerData.currentLevel.ToString());
          GameManager.Instance.UpdateGameState(GAMESTATE.START);
     }

     // Victory Panel
     public void VictoryPanel()
     {
          GameManager.Instance.UpdateGameState(GAMESTATE.VICTORY);
     }

     // Defeat Panel
     public void DefeatPanel()
     {
          GameManager.Instance.UpdateGameState(GAMESTATE.DEFEAT);
     }


     // Start Panel -> Remove Ads
     public void RemoveAds()
     {
          // AdsManager.Instance.RemoveAds();
     }

     public void Winx2()
     {
          // AdmonController.Instance.SetStatus(AdmobStatus.WinX2);
     }

     public void SkipLevel()
     {
          // AdmonController.Instance.SetStatus(AdmobStatus.SkinLevel);
     }

     public void GunBuyAtMarket()
     {
          // AdmonController.Instance.SetStatus(AdmobStatus.Gun);
     }

     public void CharackterBuyAtMarket()
     {
          // AdmonController.Instance.SetStatus(AdmobStatus.Charackter);
     }

     // SettingPanel -> Close Icon Component Button
     public void GoStartPanel(GameObject obj)
     {
          obj.SetActive(false);
          Time.timeScale = 1;
     }

     // SettingPanel -> Close Icon Component Button
     public void StartPanel()
     {
          GameManager.Instance.UpdateGameState(GAMESTATE.START);
          Time.timeScale = 1;
     }




     public void NextLevel()
     {
          GameManager.Instance.UpdateGameState(GAMESTATE.START);
          LevelManager.Instance.NextLevel();
     }

     public void RestartLevel()
     {
          GameManager.Instance.UpdateGameState(GAMESTATE.START);
          LevelManager.Instance.RestartLevel();
     }

}
