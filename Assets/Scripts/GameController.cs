using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

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

    [SerializeField]
    private int lv = 0;
    private int exp = 0;
    private int gold = 500;
    private const int bigCountdown = 240;
    private const int smallCountdown = 60;
    private float bigTimer = bigCountdown;
    private float smallTimer = smallCountdown;

    private int[] oneShootCosts = { 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
    private string[] lvNames = { "新手", "入门", "青铜", "白银", "铂金", "白金", "黄金", "钻石", "大师", "宗师"};

    private void Awake()
    {
        goldText = GameObject.Find("GoldNumText").GetComponent<Text>();
        lvText = GameObject.Find("LvText").GetComponent<Text>();
        lvNameText = GameObject.Find("LvNameText").GetComponent<Text>();
        smallCountdownText = GameObject.Find("SmallCountdown").GetComponent<Text>();
        bigCountdownText = GameObject.Find("BigCountdown").GetComponent<Text>();
        bigCountdownButton = GameObject.Find("RewarButton").GetComponent<Button>();
        backButton = GameObject.Find("BackButton").GetComponent<Button>();
        settingButton = GameObject.Find("SettingButton").GetComponent<Button>();
        expSlider = GameObject.Find("LvNameText").GetComponent<Slider>();
    }

	void Start () {
        oneShootCostText = GameObject.Find("oneShootCostText").GetComponent<Text>();
        bulletHolder = GameObject.Find("bulletHolder").transform;
        UpdateUI();
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
            GameObject bullet  = Instantiate(useBullets[bulletIndex]);
            bullet.transform.SetParent(bulletHolder, false);
            bullet.transform.position = gunGos[costIndex / 4].transform.Find("FirePos").transform.position;
            bullet.transform.rotation = gunGos[costIndex / 4].transform.rotation;
            bullet.AddComponent<EF_AutoMove>().dir = Vector3.up;
            bullet.GetComponent<EF_AutoMove>().speed = bullet.GetComponent<BulletAttr>().speed;
            bullet.GetComponent<BulletAttr>().damage *= oneShootCosts[costIndex];
        }
    }
	void Update () {
        ChangeBulletCost();
        Fire();
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
        goldText.text = gold.ToString();
        lvText.text = lv.ToString(); 
        lvNameText.text = lvNames[lv/10].ToString();
        smallCountdownText.text = smallCountdown.ToString();
        bigCountdownText.text = bigCountdown.ToString() ;
    }
}
