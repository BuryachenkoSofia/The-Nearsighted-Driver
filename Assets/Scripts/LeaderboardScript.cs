using UnityEngine;
using System.Linq;
using TMPro;
using Dan.Main;
public class LeaderboardScript : MonoBehaviour
{
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject entryPrefab;
    [SerializeField] private TMP_InputField _usernameInputField;
    private string playerName;

    private int Score => Mathf.RoundToInt(PlayerPrefs.GetFloat("maxDistance") * 1000);
    private void Start()
    {
        LoadEntries();
        if (PlayerPrefs.HasKey("name"))
        {
            playerName = PlayerPrefs.GetString("name");
        }
        if (!string.IsNullOrWhiteSpace(playerName))
        {
            _usernameInputField.text = playerName;
        }
    }

    private void LoadEntries()
    {
        Leaderboards.DistanceLeaderboard.GetEntries(entries =>
        {
            foreach (Transform child in contentParent)
            {
                Destroy(child.gameObject);
            }

            var bestEntries = entries
                .GroupBy(e => e.Username)
                .Select(g => g.OrderByDescending(e => e.Score).First())
                .OrderByDescending(e => e.Score);

            foreach (var e in bestEntries)
            {
                GameObject go = Instantiate(entryPrefab, contentParent);
                TMP_Text[] texts = go.GetComponentsInChildren<TMP_Text>();
                if (texts.Length >= 2)
                {
                    texts[0].text = e.Username;
                    texts[1].text = (e.Score / 1000f).ToString("F1") + " km";
                }
            }
        });
    }

    public void UploadEntry()
    {
        if (string.IsNullOrEmpty(_usernameInputField.text))
        {
            return;
        }
        Leaderboards.DistanceLeaderboard.UploadNewEntry(_usernameInputField.text, Score, isSuccessful =>
        {
            if (isSuccessful)
            {
                PlayerPrefs.SetString("name", _usernameInputField.text);
                LoadEntries();
            }
        });
    }
}
