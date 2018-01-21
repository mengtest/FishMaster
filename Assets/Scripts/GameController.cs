using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public GameObject[] gunGos;
    private int costIndex = 0;
    
    private Text oneShootCostText;
    private Transform bulletHolder;
    private Transform FirePos;
    [SerializeField]
    private GameObject[] bullet1Gos;
    [SerializeField]
    private GameObject[] bullet2Gos;
    [SerializeField]
    private GameObject[] bullet3Gos;
    [SerializeField]
    private GameObject[] bullet4Gos;

    private Text goldText;
    private Text lvText;
    private Text lvNameText;
    private Text smallCountdownText;
    private Text bigCountdownText;
    private Button bigCountdownButton;
    private Button backButton;
    private Button settingButton;
    private Slider expSlider;
    private Color goldTextColor;

    [SerializeField]
    private int lv = 0;
    private int exp = 0;
    [SerializeField]
    private int gold = 500;
    private const int bigCountdown = 240;
    private const int smallCountdown = 60;
    private float bigTimer = bigCountdown;
    private float smallTimer = smallCountdown;

    private int[] oneShootCosts = { 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
    private string[] lvNames = { "新手", "入门", "青铜", "白银", "铂金", "白金", "黄金", "钻石", "大师", "宗师"};

    private void Awake()
    {
        instance = this;
        goldText = GameObject.Find("GoldNumText").GetComponent<Text>();
        lvText = GameObject.Find("LvText").GetComponent<Text>();
        goldTextColor = goldText.color;
        lvNameText = GameObject.Find("LvNameText").GetComponent<Text>();
        smallCountdownText = GameObject.Find("SmallCountdown").GetComponent<Text>();
        bigCountdownText = GameObject.Find("BigCountdown").GetComponent<Text>();
        bigCountdownButton = GameObject.Find("RewarButton").GetComponent<Button>();
        backButton = GameObject.Find("BackButton").GetComponent<Button>();
        settingButton = GameObject.Find("SettingButton").GetComponent<Button>();
        expSlider = GameObject.Find("ExpSlider").GetComponent<Slider>();
        bigCountdownButton.onClick.AddListener(UpdateBigCountDownTime);
        LoadgameData();
    }

    private void LoadgameData()
    {
        lv = PlayerPrefs.GetInt("Lv");
        exp = PlayerPrefs.GetInt("Exp");
        gold = PlayerPrefs.GetInt("Gold");
    }

    public void AddExpAndGold(int newExp,int newGold)
    {
        exp += newExp;
        gold += newGold;
    }
	void Start () {
        oneShootCostText = GameObject.Find("oneShootCostText").GetComponent<Text>();
        bulletHolder = GameObject.Find("bulletHolder").transform;
    }
	
    void Fire()
    {
        GameObject[] useBullets = bullet1Gos;
        int bulletIndex;
        if (Input.GetMouseButtonDown(0)&&EventSystem.current.IsPointerOverGameObject()==false)
        {
            switch (costIndex / 4)
            {
                case 0:useBullets = bullet1Gos;break;
                case 1: useBullets = bullet2Gos; break; 
                case 2: useBullets = bullet3Gos; break;
                case 3:useBullets = bullet4Gos;break;
            }
            
            bulletIndex = (lv / 10 >= 9) ? 9 : lv / 10;
            if (gold >= oneShootCosts[costIndex])
            {
                gold -= oneShootCosts[costIndex];
                GameObject bullet = Instantiate(useBullets[bulletIndex]);
                bullet.transform.SetParent(bulletHolder, false);
                bullet.transform.position = gunGos[costIndex / 4].transform.Find("FirePos").transform.position;
                bullet.transform.rotation = gunGos[costIndex / 4].transform.rotation;
                bullet.AddComponent<EF_AutoMove>().dir = Vector3.up;
                bullet.GetComponent<EF_AutoMove>().speed = bullet.GetComponent<BulletAttr>().speed;
                bullet.GetComponent<BulletAttr>().damage *= oneShootCosts[costIndex];
                gunGos[costIndex / 4].GetComponent<AudioSource>().Play();
            }
            else
            {
                StartCoroutine(GoldNotEnough());
            }
            
            
            
        }
    }
	void Update () {
        ChangeBulletCost();
        Fire();
        UpdateUI();
        PlayerPrefs.SetInt("Lv", lv);
        PlayerPrefs.SetInt("Gold", gold);
        PlayerPrefs.SetInt("Exp", exp);

    }

    void ChangeBulletCost()
    {
        if (Input.GetAxis("Mouse ScrollWheel")<0)
        {
            OnButtonMDown();
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            OnButtonPDown();
        }
    }
    public void OnButtonPDown()
    {
        gunGos[costIndex / 4].SetActive(false);
        costIndex++;
        costIndex = (costIndex>oneShootCosts.Length-1)?0: costIndex;
        gunGos[costIndex / 4].SetActive(true);
        oneShootCostText.text = "$" + oneShootCosts[costIndex];
    }
    public void OnButtonMDown()
    {
        gunGos[costIndex / 4].SetActive(false);
        costIndex--;
        costIndex = (costIndex < 0) ? oneShootCosts.Length - 1 : costIndex;
        gunGos[costIndex / 4].SetActive(true);
        oneShootCostText.text = "$" + oneShootCosts[costIndex];

    }
    void UpdateUI()
    {
        //exp += 10;
        bigTimer -= Time.deltaTime;
        smallTimer -= Time.deltaTime;
        if (smallTimer<=0)
        {
            smallTimer = smallCountdown;
            gold += 50;
        }
        else
        {
           smallCountdownText.text = ((int)smallTimer).ToString();
        }
        if (bigTimer<=0)
        {
            bigCountdownText.text = "";
            bigCountdownButton.transform.GetComponent<CanvasGroup>().alpha = 1;
            bigCountdownButton.interactable = true;
        }
        else
        {
            bigCountdownButton.transform.GetComponent<CanvasGroup>().alpha = 0;
            bigCountdownButton.interactable = false;
            bigCountdownText.text = (int)bigTimer + "S";
        }
        while (exp>=1000+200*lv)
        {
            lv++;
            exp = 0;
        }
        if (lv/10<=9)
        {
            lvNameText.text = lvNames[lv / 10].ToString();
        }
        else
        {
            lvNameText.text = lvNames[9].ToString();
        }
        if (lv>99&&lv<=999)
        {
            lvText.fontSize = 27;
        }
        goldText.text = "$" +  gold;
        lvText.text = lv.ToString(); 
        expSlider.value = (float)exp / (1000 + lv * 200);
    }
    private void UpdateBigCountDownTime()
    {
        bigTimer = bigCountdown;
        gold += 500;
    }
    IEnumerator GoldNotEnough()
    {
        goldText.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        goldText.color = goldTextColor;
    }
}
