using UnityEngine;
using UnityEngine.UI;

public class InstructionManager : MonoBehaviour
{

    public GameObject Focus, FirstTimePlay, Enemy, Heart, PowerUp, Arrow1, Arrow2;

    public Text Instruction;

    bool InstructionChanged;

    //calls the instruction methos for guidance
    void Start()
    {
        PlayerPrefs.SetInt("Location", 0);
        setInstruction();
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0) && InstructionChanged) {
        if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetKey(KeyCode.Mouse0))) && InstructionChanged)
        {
            InstructionChanged = false;
            ChangeInstruction();
        }
    }

    //sets the instruction text
    void setInstruction()
    {
        if (PlayerPrefs.GetInt("Location", 0) == 0)
        {
            Arrow1.SetActive(false);
            Arrow2.SetActive(false);
            FirstTimePlay.transform.position = new Vector3(-3f, -1.5f, 0f);
            Instruction.text = "Tap here to throw\nWeapon with slow speed";
            focusEffect();
        }
        else if (PlayerPrefs.GetInt("Location") == 1)
        {
            FirstTimePlay.transform.position = new Vector3(5f, 1f, 0f);
            Instruction.text = "Tap here to throw\nWeapon with high speed";
        }
        else if (PlayerPrefs.GetInt("Location") == 2)
        {
            FirstTimePlay.transform.position = new Vector3(5f, 0f, 0f);
            Instruction.text = "Tap and hold here\nto throw Weapon with max speed";
        }

        else if (PlayerPrefs.GetInt("Location") == 3)
        {
            Instantiate(Enemy, new Vector3(2.5f, -0.5f, 0f), Enemy.transform.rotation);
            FirstTimePlay.transform.position = new Vector3(1f, -1f, 0f);
            Instruction.text = "Kill the enemy";
            Invoke("checkEnemy", 2f);
            Debug.Log("Kill 1");
        }
        else if (PlayerPrefs.GetInt("Location") == 4)
        {
            Instantiate(Enemy, new Vector3(2.5f, -0.5f, 0f), Enemy.transform.rotation);
            FirstTimePlay.transform.position = new Vector3(1.5f, -0.2f, 0f);
            Instruction.text = "Kill with headShot";
            Invoke("checkEnemy", 2f);
            Debug.Log("Kill 2");
        }
        else if (PlayerPrefs.GetInt("Location") == 5)
        {
            Instantiate(Heart, new Vector3(-0.1f, 0.8f, 0f), Heart.transform.rotation);
            FirstTimePlay.transform.position = new Vector3(0f, 0f, 0f);
            Instruction.text = "Hit heart to increase health";
            GameObject.FindWithTag("NinjaContainer").GetComponent<NinjaHealth>().hitPoint -= 1;
            if (GameObject.Find("ScoreUI").gameObject != null)
            {
                GameObject.Find("ScoreUI").GetComponent<Score>().UpdateHealth();
            }
            Invoke("checkHeart", 1f);
        }
        else if (PlayerPrefs.GetInt("Location") == 6)
        {
            Instantiate(PowerUp, new Vector3(0.9f, 0.8f, 0f), PowerUp.transform.rotation);
            FirstTimePlay.transform.position = new Vector3(1f, 0f, 0f);
            Instruction.text = "Hit shield to protect ninja";
            Invoke("checkPowerUp", 1f);
        }
        else if (PlayerPrefs.GetInt("Location") == 7)
        {
            FirstTimePlay.transform.position = new Vector3(1f, -1f, 0f);
            Instruction.text = "Kill and Headshot are your targets\nto complete the Level";
            Arrow1.SetActive(true);
            Arrow2.SetActive(true);
        }

        else if (PlayerPrefs.GetInt("Location") == 8)
        {
            Arrow1.SetActive(false);
            Arrow2.SetActive(false);
            FirstTimePlay.transform.position = new Vector3(0f, 0f, 0f);
            Instruction.text = "You can buy new Weapons and Helmets\nfrom store.Tap the circle to start the game";
        }
        InstructionChanged = true;
    }
    //resize the focus circle
    void focusEffect()
    {
        iTween.ScaleFrom(Focus, iTween.Hash("scale", new Vector3(0.8f, 0.8f, 0.8f), "time", 1f, "easetype", iTween.EaseType.easeInOutElastic));
        Invoke("focusEffect", 2f);
    }


#if UNITY_EDITOR
    void OnMouseUp()
    {
        ChangeInstruction();
    }
#endif

    void ChangeInstruction()
    {
        if (PlayerPrefs.GetInt("Location") == 0)
        {
            PlayerPrefs.SetInt("Location", PlayerPrefs.GetInt("Location") + 1);
            Invoke("setInstruction", 1f);
        }
        else if (PlayerPrefs.GetInt("Location") == 1)
        {
            PlayerPrefs.SetInt("Location", PlayerPrefs.GetInt("Location") + 1);
            Invoke("setInstruction", 1f);
        }
        else if (PlayerPrefs.GetInt("Location") == 2)
        {
            if (Attack.attackObj.minSpeed < Attack.attackObj.speed)
            {
                PlayerPrefs.SetInt("Location", PlayerPrefs.GetInt("Location") + 1);
                Invoke("setInstruction", 1f);
            }
            else
            {
                InstructionChanged = true;
                iTween.ScaleFrom(Instruction.gameObject, iTween.Hash("scale", new Vector3(0f, 0f, 0f), "time", 1f, "easetype", iTween.EaseType.easeInOutElastic));
            }
        }
        else if (PlayerPrefs.GetInt("Location") == 7)
        {
            PlayerPrefs.SetInt("Location", PlayerPrefs.GetInt("Location") + 1);
            Invoke("setInstruction", 1f);
        }
        else if (PlayerPrefs.GetInt("Location") == 8)
        {
            PlayerPrefs.SetInt("FirstTime", 0);
            FirstTimePlay.SetActive(false);
            Instruction.gameObject.SetActive(false);
            GameObject.Find("GameManager").GetComponent<Manager>().StartGame();
        }
        Invoke("resetInstructionSize", 1.1f);
    }

    void resetInstructionSize()
    {
        Instruction.gameObject.transform.localScale = new Vector3(0.23f, 0.23f, 0.23f);
    }

    //check if enemy is alive
    void checkEnemy()
    {
        if (GameObject.FindWithTag("EnemyContainer") == null)
        {
            PlayerPrefs.SetInt("Location", PlayerPrefs.GetInt("Location") + 1);
            Invoke("setInstruction", 1f);
            return;
        }
        Invoke("checkEnemy", 0f);
    }
    //check if health increase power is available or not
    void checkHeart()
    {
        if (GameObject.FindWithTag("heart") == null)
        {
            PlayerPrefs.SetInt("Location", PlayerPrefs.GetInt("Location") + 1);
            Invoke("setInstruction", 1f);
            return;
        }
        Invoke("checkHeart", 0f);
    }
    //check if shield is available or not
    void checkPowerUp()
    {
        if (GameObject.FindWithTag("PowerUp") == null)
        {
            PlayerPrefs.SetInt("Location", PlayerPrefs.GetInt("Location") + 1);
            Invoke("setInstruction", 1f);
            return;
        }
        Invoke("checkPowerUp", 0f);
    }
}
