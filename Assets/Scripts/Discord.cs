// Original script by sventomasek - https://github.com/sventomasek/Discord-Script-for-Unity

using UnityEngine;
using Discord;

public class NewBehaviourScript : MonoBehaviour {
    
    /* -------------------------------- Variables ------------------------------- */
    public long applicationID = 1208931112630485113;
    [Space]
    //public string details = "In a game";
    public string state = "Score: ";
    [Space]
    public string largeImage = "main";
    public string largeText = "Glow Explosions ";

    private long time;

    private static bool instanceExists;
    public Discord.Discord discord;

    /* --------------------------------- Methods -------------------------------- */
    void Awake() {
        // Transition the GameObject between scenes, destroy any duplicates
        if (!instanceExists) {
            instanceExists = true;
            DontDestroyOnLoad(gameObject);
        }
        else if (FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        }
    }

    void Start() {
        // Log in with the Application ID
        discord = new Discord.Discord(applicationID, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);

        time = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();

        UpdateStatus();
    }

    void Update() {
        // Destroy the GameObject if Discord isn't running
        try {
            discord.RunCallbacks();
        }
        catch {
            Destroy(gameObject);
        }
    }

    void LateUpdate() {
        UpdateStatus();
    }

    void UpdateStatus() {
        // Update Status every frame
        try {
            var activityManager = discord.GetActivityManager();
            var activity = new Discord.Activity {
                State = state + GameManager.SCORE,
                Assets = {
                    LargeImage = largeImage,
                    LargeText = largeText
                },
                Timestamps = {
                    Start = time
                }
            };

            activityManager.UpdateActivity(activity, (res) => {
                if (res != Discord.Result.Ok) Debug.LogWarning("Failed connecting to Discord!");
            });
        }
        catch {
            // If updating the status fails, Destroy the GameObject
            Destroy(gameObject);
        }
    }
}
