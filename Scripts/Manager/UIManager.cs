using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UIManager : Singleton<UIManager>
{
     private const string Music = "Music";
     private const string Vibrate = "Vibrate";
     public GameObject StartPanel, MarketPanel, PlayPanel;
     public GameObject SettingsPanel, VictoryPanel, DefeatPanel, MoneyBox, MoneyInage;

     [Space(5)]
     [Header("Coins Panel")]
     public TextMeshProUGUI CoinText;
     private int _money;
     private int _totalMoney;

     [HideInInspector]
     public PlayerData playerData;

     [Space(5)]
     [Header("Coins Panel")]
     public TextMeshProUGUI TotalMoneyText;

     [Tooltip("TotalX2moneyText in Ad Button Win Panel")]
     public TextMeshProUGUI TotalX2moneyText;
     public TextMeshProUGUI levelName;
     public GameObject MoneyPrefab;
     public Transform MoneyParent;

     public GameObject QuickPanel;
     public Transform Ok;
     private bool isQuickOpen;
     private float yedekClamp;

     private void Start()
     {

          UpdateGameState(GAMESTATE.START);
          UpdateCoin();
          yedekClamp = clamp;
     }




     public void RequestReward()
     {

     }
     private void OnEnable()
     {
          GameManager.OnGameStateChanged += UpdateGameState;
          LevelManager.OnLevelLoaded += UpdateLevel;
     }


     private void OnDisable()
     {
          GameManager.OnGameStateChanged -= UpdateGameState;
          LevelManager.OnLevelLoaded -= UpdateLevel;
     }

     private void UpdateLevel(bool arg0)
     {
          if (arg0)
          {

          }
     }

     public float yRotate = 0;

     private bool isy;
     public float speed;
     public float clamp;


     private void Update()
     {
          if (isQuickOpen)
          {
               /* yRotate = Mathf.Clamp(yRotate, -clamp, clamp);
                if (isy)
                     yRotate += speed;
                else
                     yRotate -= speed;

                if (yRotate >= clamp)
                     isy = false;
                else if (yRotate <= -clamp)
                     isy = true;*/

               if (yRotate + 52 > clamp)
               {
                    yedekClamp = -clamp;
               }
               if (yRotate - 52 < -clamp)
               {
                    yedekClamp = clamp;
               }

               yRotate = Mathf.Lerp(yRotate, yedekClamp, Time.deltaTime * speed);
               Ok.localRotation = Quaternion.Euler(0, 0, yRotate);


          }
     }

     public void YRotateControl()
     {
          QuickPanel.SetActive(false);
          isQuickOpen = false;
          int currentValue = (int)Mathf.Abs(yRotate);
          if (currentValue >= 0 && currentValue < 18)
               PlayerController.Instance.QuickButtonJump();
          else if (currentValue >= 20 && currentValue < 50)
               PlayerController.Instance.QuickButtonJump();
          else if (currentValue >= 50 && currentValue <= 77)
          {

               PlayerController.Instance.StartGravity();
               StartCoroutine(WaitForSeconds(2f));
          }
     }

     IEnumerator WaitForSeconds(float time)
     {

          yield return new WaitForSeconds(time);
          GameManager.Instance.UpdateGameState(GAMESTATE.DEFEAT);

     }

     public void ShowQuickPanel(bool isShow)
     {
          Debug.Log("ShowQuickPanel");
          isQuickOpen = isShow;
          Ok.transform.Rotate(0, 0, 0);
          QuickPanel.SetActive(isShow);
     }

     private void UpdateGameState(GAMESTATE switchState)
     {
          switch (switchState)
          {
               case GAMESTATE.START:
                    _money = 0;
                    _totalMoney = 0;
                    UpdateUI(StartPanel);
                    isQuickOpen = false;
                    CoinText.text = PlayerPrefs.GetInt("coins").ToString();
                    break;
               case GAMESTATE.PLAY:
                    levelName.SetText(PlayerPrefs.GetInt("currentIndex").ToString());
                    UpdateUI(PlayPanel);
                    BannerController(false);
                    break;
               case GAMESTATE.VICTORY:
                    _money += 150;
                    UpdateUI(VictoryPanel);
                    _totalMoney = _money;
                    CoinsGoToMoneyBox();
                    BannerController(true);
                    CoinText.text = PlayerPrefs.GetInt("coins").ToString();
                    break;
               case GAMESTATE.MARKET:
                    UpdateUI(MarketPanel);
                    BannerController(false);
                    break;
               case GAMESTATE.DEFEAT:
                    //AdmonController.Instance.DefeatIntersitial();
                    UpdateUI(DefeatPanel);
                    BannerController(true);
                    break;
               default:
                    UpdateUI(null);
                    break;

          }

     }

     public void UpdateUI(GameObject obj)
     {
          StartPanel.SetActive(false);
          MarketPanel.SetActive(false);
          PlayPanel.SetActive(false);
          VictoryPanel.SetActive(false);
          DefeatPanel.SetActive(false);
          SettingsPanel.SetActive(false);

          if (obj != null)
               obj.SetActive(true);

     }

     #region Coins
     public void UpdateCoin()
     {
          int total = GetCoin();
          _totalMoney += total;
          PlayerPrefs.SetInt("coins", _totalMoney);
          CoinText.text = _totalMoney.ToString();
     }

     public int GetCoin() => PlayerPrefs.GetInt("coins");
     public void SetMoneyCalculate(int getMoney) => _money += getMoney;

     #endregion
     public void CoinsGoToMoneyBox()
     {
          TotalMoneyText.SetText(_totalMoney.ToString());

          TotalX2moneyText.SetText("Clamp X2 " + _totalMoney * 2);


          UpdateCoin();

          // for (int i = 0; i < 10; i++)
          // {
          //     GameObject obj = Instantiate(CoinsForPointsPrefab, CoinsPointInVictoryPanel.transform);

          //     float xPosition = Random.Range(-40, 10f);
          //     float yPosition = Random.Range(-10, 10f);

          //     obj.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);

          //     obj.transform.DOLocalMove(new Vector2(yPosition, xPosition), 0.1f);


          //     float minDuration = Random.Range(1f, 2f);

          //     obj.transform.
          //         DOMove(MoneyBox.transform.position, minDuration).SetEase(Ease.InCubic).
          //         OnComplete(() =>
          //         {
          //             VibrateController(7);
          //             Destroy(obj);
          //         });
          // }

          // AdmonController.Instance.ShowReward();
     }

     #region Sound Vibrate
     public void SoundController(AudioClip clip)
     {
          if (PlayerPrefs.GetInt(Vibrate) == 1)
               VibrateController(15);
          if (PlayerPrefs.GetInt(Music) == 1)
          {
               // FindObjectOfType<CameraManager>().GunPlaySource(clip);
          }

     }

     private void VibrateController(int val) => Vibrator.Vibrate(val);



     public bool SettingsDataController(SettingsIcon icon)
     {
          if (icon.dataType == dataType.MUSIC)
               return PlayerPrefs.GetInt(Music) == 1 ? true : false;
          else
               return PlayerPrefs.GetInt(Vibrate) == 1 ? true : false;
     }
     #endregion

     public void BannerController(bool isShow)
     {

          // if (isShow)
          //     AdmobManager.Instance.bannerView.Show();
          // else
          //     AdmobManager.Instance.bannerView.Hide();


     }

     public void TransformMoney(Vector2 pos)
     {
          StartCoroutine(InstantceAndTranslate(pos));
     }

     IEnumerator InstantceAndTranslate(Vector3 pos)
     {
          GameObject objx = Instantiate(MoneyPrefab, pos, Quaternion.identity, MoneyParent);

          while (Vector3.Distance(objx.transform.position, MoneyInage.transform.position) > 2f)
          {
               objx.transform.position = Vector3.Lerp(objx.transform.position, MoneyInage.transform.position, 2f * Time.deltaTime);
               yield return null;
          }

          int randomCoins = Random.Range(10, 30);

          SetCoin(randomCoins);


          Destroy(objx);
     }

     private void SetCoin(int v)
     {
          _totalMoney += v;
          CoinText.SetText((PlayerPrefs.GetInt("coins") + v).ToString());
     }
}