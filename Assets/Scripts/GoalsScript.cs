using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public enum GoalId
{
    Drive1Km = 0,
    Drive2Km = 1,
    Drive3Km = 2,
    Drive4Km = 3,
    Drive5Km = 4,
    Earn100Coins = 5,
    Earn1000Coins = 6,
    Collect10Hearts = 7,
    Collect10Coins = 8,
    Collect5Gems = 9,
    Collect10Glasses = 10,
    Bomb5 = 11,
    Shield5 = 12,
    Truck10 = 13,
    Let10 = 14,
    Police5 = 15,
    Buy1Car = 16,
    BuyAllCars = 17,
    Drive10Km = 18,
    Slips5 = 19,
    Slips1 = 20
}

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
        public int id;
        public int order;
        public Button button;
    }

    private List<Goal> goals = new List<Goal>
    {
        new Goal { text="Drive 1 km", reward=50, order=0, id=0 },
        new Goal { text="Drive 2 km", reward=100, order=1, id=1 },
        new Goal { text="Drive 3 km", reward=150, order=2, id=2 },
        new Goal { text="Drive 4 km", reward=200, order=3, id=3 },
        new Goal { text="Drive 5 km", reward=250, order=4, id=4 },
        new Goal { text="Drive 10 km", reward=500, order = 5, id=18},
        new Goal { text="Earn 100 coins", reward=20, order=6, id=5 },
        new Goal { text="Earn 1000 coins", reward=200, order=7, id=6 },
        new Goal { text="Collect 10 hearts in a single run", reward=50, order=8, id=7 },
        new Goal { text="Collect 10 coins in a single run", reward=50, order=9, id=8 },
        new Goal { text="Collect 5 gems in a single run", reward=50, order=10, id=9 },
        new Goal { text="Collect 10 glasses in a single run", reward=50, order=11, id=10 },
        new Goal { text="Collect 5 bomb in a single run", reward=100, order=12, id=11 },
        new Goal { text="Collect 5 shield in a single run", reward=100, order=13, id=12 },
        new Goal { text="Crash into a truck 10 times in a single run", reward=100, order=14, id=13 },
        new Goal { text="Crash into a let 10 times in a single run", reward=50, order=15, id=14 },
        new Goal { text="Crash into a police car 5 times in a single run", reward=200, order=16, id=15 },
        new Goal { text="Survive 1 slip", reward=50, order=17, id=20 },
        new Goal { text="Survive 5 slips", reward=200, order=18, id=19 },
        new Goal { text="Buy 1 car", reward=200, order=19, id=16 },
        new Goal { text="Buy all cars", reward=1000, order=20, id=17 },
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
            int maxId = goals.Max(g => g.id);
            goalsStatusStr = new string('0', maxId + 1);
            PlayerPrefs.SetString("goals", goalsStatusStr);
        }
        else
        {
            goalsStatusStr = PlayerPrefs.GetString("goals");
        }

        int maxIdCheck = goals.Max(g => g.id);
        if (goalsStatusStr.Length <= maxIdCheck)
        {
            goalsStatusStr = goalsStatusStr.PadRight(maxIdCheck + 1, '0');
            PlayerPrefs.SetString("goals", goalsStatusStr);
        }

        if (SceneManager.GetActiveScene().buildIndex != 0) return;

        List<int> sortedIndexes = new List<int>();
        for (int i = 0; i < goals.Count; i++) sortedIndexes.Add(i);
        sortedIndexes.Sort((a, b) => goals[a].order.CompareTo(goals[b].order));

        for (int sortedPos = 0; sortedPos < sortedIndexes.Count; ++sortedPos)
        {
            int i = sortedIndexes[sortedPos];
            Goal goal = goals[i];

            GameObject obj = Instantiate(goalPrefab, goalsPanel.transform);
            obj.transform.localPosition = new Vector3(250f + 450f * sortedPos, -341.5f, 0);
            obj.transform.Find("Text").GetComponent<TMP_Text>().text = goal.text + "\nReward:\n" + goal.reward + " coins";
            goal.button = obj.transform.Find("GetReward").GetComponent<Button>();
            int id = goal.id;

            if (goalsStatusStr[id] == '0')
            {
                goal.button.transform.Find("Text").GetComponent<TMP_Text>().text = "Get reward";
                goal.button.interactable = false;
            }
            else if (goalsStatusStr[id] == '1')
            {
                goal.button.transform.Find("Text").GetComponent<TMP_Text>().text = "Get reward";
                goal.button.interactable = true;
            }
            else if (goalsStatusStr[id] == '2')
            {
                goal.button.transform.Find("Text").GetComponent<TMP_Text>().text = "Reward received";
                goal.button.interactable = false;
            }

            int capturedId = id;
            goal.button.onClick.AddListener(() => OnGoalButtonClicked(capturedId));
            goal.button.onClick.AddListener(() => buttonSound.PlaySound());
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) return;

        coinsText.text = "Coins: " + PlayerPrefs.GetFloat("coins");
        if (PlayerPrefs.GetFloat("coins") >= 100f)
        {
            GoalAchieved((int)GoalId.Earn100Coins);
        }
        if (PlayerPrefs.GetFloat("coins") >= 1000f)
        {
            GoalAchieved((int)GoalId.Earn1000Coins);
        }
        if (PlayerPrefs.GetString("cars").Count(c => c == '1') > 1)
        {
            GoalAchieved((int)GoalId.Buy1Car);
        }
        if (PlayerPrefs.GetString("cars") == "11111111")
        {
            GoalAchieved((int)GoalId.BuyAllCars);
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

    private void OnGoalButtonClicked(int id)
    {
        Goal goal = goals.First(g => g.id == id);

        PlayerPrefs.SetFloat("coins", PlayerPrefs.GetFloat("coins") + goal.reward);

        char[] chars = PlayerPrefs.GetString("goals").ToCharArray();
        chars[id] = '2';
        PlayerPrefs.SetString("goals", new string(chars));

        goal.button.gameObject.transform.Find("Text").GetComponent<TMP_Text>().text = "Reward received";
        goal.button.interactable = false;
    }

    public void GoalAchieved(int id)
    {
        char[] chars = PlayerPrefs.GetString("goals").ToCharArray();
        if (chars[id] == '2') return;
        chars[id] = '1';
        PlayerPrefs.SetString("goals", new string(chars));
        if (SceneManager.GetActiveScene().buildIndex != 0) return;
        Goal goal = goals.First(g => g.id == id);
        goal.button.gameObject.transform.Find("Text").GetComponent<TMP_Text>().text = "Get reward";
        goal.button.interactable = true;
    }
}