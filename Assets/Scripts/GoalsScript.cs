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
        public int index;
        public Button button;
    }

    private List<Goal> goals = new List<Goal>
    {
        new Goal { text="Collect 10 hearts in a single run", reward=50, index=7 },                //0
        new Goal { text="Collect 10 coins in a single run", reward=50, index=8 },                 //1
        new Goal { text="Collect 5 gems in a single run", reward=50, index=9 },                   //2
        new Goal { text="Collect 10 glasses in a single run", reward=50, index=10 },              //3
        new Goal { text="Drive 1 km", reward=50, index=0 },                                       //4
        new Goal { text="Drive 2 km", reward=100, index=1 },                                      //5
        new Goal { text="Drive 3 km", reward=150, index=2 },                                      //6
        new Goal { text="Drive 4 km", reward=200, index=3 },                                      //7
        new Goal { text="Drive 5 km", reward=250, index=4 },                                      //8
        new Goal { text="Earn 100 coins", reward=20, index=5 },                                   //9
        new Goal { text="Earn 1000 coins", reward=200, index=6 },                                 //10
        new Goal { text="Crash into a truck 10 times in a single run", reward=100, index=13 },    //11
        new Goal { text="Crash into a let 10 times in a single run", reward=50, index=14 },       //12
        new Goal { text="Crash into a police car 5 times in a single run", reward=200, index=15 },//13
        new Goal { text="Buy 1 car", reward=200, index=16 },                                      //14
        new Goal { text="Buy all cars", reward=1000, index=17 },                                  //15
        new Goal { text="Collect 5 bomb in a single run", reward=100, index=11 },                 //16
        new Goal { text="Collect 5 shield in a single run", reward=100, index=12 },               //17
    };

    private string goalsStatusStr = "";
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private GameObject newGoalsImg;
    [SerializeField] private TMP_Text newGoalsText;
    [SerializeField] private ButtonSound buttonSound;
    [SerializeField] private GameObject goalPrefab, goalsPanel;

    private void Start()
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
        if (SceneManager.GetActiveScene().buildIndex != 0) return;

        List<int> sortedIndexes = new List<int>();
        for (int i = 0; i < goals.Count; i++) sortedIndexes.Add(i);
        sortedIndexes.Sort((a, b) => goals[a].index.CompareTo(goals[b].index));

        for (int sortedPos = 0; sortedPos < sortedIndexes.Count; ++sortedPos)
        {
            int i = sortedIndexes[sortedPos];
            Goal goal = goals[i];

            GameObject obj = Instantiate(goalPrefab, goalsPanel.transform);
            obj.transform.localPosition = new Vector3(250f + 450f * sortedPos, -341.5f, 0);
            obj.transform.Find("Text").GetComponent<TMP_Text>().text = goal.text + "\nReward:\n" + goal.reward + " coins";
            goal.button = obj.transform.Find("GetReward").GetComponent<Button>();

            if (goalsStatusStr[i] == '0')
            {
                goal.button.transform.Find("Text").GetComponent<TMP_Text>().text = "Get reward";
                goal.button.interactable = false;
            }
            else if (goalsStatusStr[i] == '1')
            {
                goal.button.transform.Find("Text").GetComponent<TMP_Text>().text = "Get reward";
                goal.button.interactable = true;
            }
            else if (goalsStatusStr[i] == '2')
            {
                goal.button.transform.Find("Text").GetComponent<TMP_Text>().text = "Reward received";
                goal.button.interactable = false;
            }

            int capturedIndex = i;
            goal.button.onClick.AddListener(() => OnGoalButtonClicked(capturedIndex));
            goal.button.onClick.AddListener(() => buttonSound.PlaySound());
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) return;

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

    private void OnGoalButtonClicked(int index)
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
        if (SceneManager.GetActiveScene().buildIndex != 0) return;
        goals[index].button.gameObject.transform.Find("Text").GetComponent<TMP_Text>().text = "Get reward";
        goals[index].button.interactable = true;
    }
}