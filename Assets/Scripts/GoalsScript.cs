using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class GoalsScript : MonoBehaviour
{
    public enum GoalStatus
    {
        InProgress, Completed, Claimed
    }

    [System.Serializable]
    public class Goal
    {
        public string text;
        public float reward;
        public Button button;
    }

    private List<Goal> goals = new List<Goal>
    {
        new Goal { text="Collect 10 hearts in a single run", reward=50},               //0
        new Goal { text="Collect 10 coins in a single run", reward=50},                //1
        new Goal { text="Collect 5 gems in a single run", reward=50},                  //2
        new Goal { text="Collect 10 glasses in a single run", reward=50},              //3
        new Goal { text="Drive 1 km", reward=50},                                      //4
        new Goal { text="Drive 2 km", reward=100},                                     //5
        new Goal { text="Drive 3 km", reward=150},                                     //6
        new Goal { text="Drive 4 km", reward=200},                                     //7
        new Goal { text="Drive 5 km", reward=250},                                     //8
        new Goal { text="Earn 100 coins", reward=20},                                  //9
        new Goal { text="Earn 1000 coins", reward=200},                                //10
        new Goal { text="Crash into a truck 10 times in a single run", reward=100},    //11
        new Goal { text="Crash into a let 10 times in a single run", reward=50},       //12
        new Goal { text="Crash into a police car 5 times in a single run", reward=200},//13
        new Goal { text="Buy 1 car", reward=200},                                      //14
        new Goal { text="Buy all cars", reward=1000},                                  //15
    };

    public GameObject goalPrefab, goalsPanel;
    private string goalsStatusStr = "";
    public TMP_Text coinsText;
    public GameObject newGoalsImg;
    public TMP_Text newGoalsText;

    void Start()
    {
        if (!PlayerPrefs.HasKey("goals"))
        {
            goalsStatusStr = "";
            for (int i = 0; i < goals.Count; ++i)
            {
                goalsStatusStr += '0';
            }
            PlayerPrefs.SetString("goals", goalsStatusStr);
        }
        else
        {
            goalsStatusStr = PlayerPrefs.GetString("goals");
        }
        if (SceneManager.GetActiveScene().buildIndex == 1) return;

        for (int i = 0; i < goals.Count; ++i)
        {
            GameObject obj = Instantiate(goalPrefab, goalsPanel.transform);
            obj.transform.localPosition = new Vector3(250f + 450f * i, -341.5f, 0);
            obj.transform.Find("Text").GetComponent<TMP_Text>().text = (goals[i].text) + "\nReward:\n" + goals[i].reward + " coins";
            goals[i].button = obj.transform.Find("GetReward").GetComponent<Button>();

            if (goalsStatusStr[i] == '0')
            {
                goals[i].button.gameObject.transform.Find("Text").GetComponent<TMP_Text>().text = "Get reward";
                goals[i].button.interactable = false;
            }
            else if (goalsStatusStr[i] == '1')
            {
                goals[i].button.gameObject.transform.Find("Text").GetComponent<TMP_Text>().text = "Get reward";
                goals[i].button.interactable = true;
            }
            else if (goalsStatusStr[i] == '2')
            {
                goals[i].button.gameObject.transform.Find("Text").GetComponent<TMP_Text>().text = "Reward received";
                goals[i].button.interactable = false;
            }

            int index = i;
            goals[index].button.onClick.AddListener(() => OnGoalButtonClicked(index));
        }
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) return;

        coinsText.text = "Coins: " + PlayerPrefs.GetFloat("coins");
        if (PlayerPrefs.GetFloat("coins") >= 100f)
        {
            GoalAchieved(9);
        }
        if (PlayerPrefs.GetFloat("coins") >= 1000f)
        {
            GoalAchieved(10);
        }
        if (PlayerPrefs.GetString("cars").Count(c => c == '1') > 1)
        {
            GoalAchieved(14);
        }
        if (PlayerPrefs.GetString("cars") == "11111111")
        {
            GoalAchieved(15);
        }
        if (PlayerPrefs.GetString("goals").Count(c => c == '1') > 0)
        {
            newGoalsImg.SetActive(true);
            newGoalsText.text = PlayerPrefs.GetString("goals").Count(c => c == '1').ToString();
        }
        else
        {
            newGoalsImg.SetActive(false);
        }
    }
    void OnGoalButtonClicked(int index)
    {
        PlayerPrefs.SetFloat("coins", PlayerPrefs.GetFloat("coins") + goals[index].reward);

        string n = PlayerPrefs.GetString("goals");
        char[] chars = PlayerPrefs.GetString("goals").ToCharArray();
        chars[index] = '2';
        n = new string(chars);
        PlayerPrefs.SetString("goals", n);

        goals[index].button.gameObject.transform.Find("Text").GetComponent<TMP_Text>().text = "Reward received";
        goals[index].button.interactable = false;
    }
    public void GoalAchieved(int index)
    {
        if (PlayerPrefs.GetString("goals")[index] == '2')
        {
            return;
        }
        string n = PlayerPrefs.GetString("goals");
        char[] chars = PlayerPrefs.GetString("goals").ToCharArray();
        chars[index] = '1';
        n = new string(chars);
        PlayerPrefs.SetString("goals", n);
        if (SceneManager.GetActiveScene().buildIndex == 1) return;
        goals[index].button.gameObject.transform.Find("Text").GetComponent<TMP_Text>().text = "Get reward";
        goals[index].button.interactable = true;
    }
}