using TMPro;
using UnityEngine;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    [Header("UI")]
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI failText;

    [Header("Puzzles")]
    public int totalPuzzles = 5;
    public bool puzzle1Solved; // color pad
    public bool puzzle2Solved; // buttons
    public bool puzzle3Solved; // books
    public bool puzzle4Solved; // lights
    public bool puzzle5Solved; // weight plate

    [Header("Door")]
    public DoorController door;

    [Header("Timer / Failure")]
    public float timeLimit = 100f;

    [Header("Confetti")]
    public ConfettiSpawner confettiSpawner;

    // internals
    float timeRemaining;
    bool gameActive = false;
    bool gameWon = false;

    int completedPuzzles = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        timeRemaining = timeLimit;
        gameActive = true;
        gameWon = false;

        RecalculateCompleted();
        UpdateUI();

        if (statusText != null) statusText.text = "";
        if (winText != null)    winText.gameObject.SetActive(false);
        if (failText != null)   failText.gameObject.SetActive(false);

        // Player starts inside — door starts closed
        if (door != null) door.CloseDoor();
    }

    void Update()
    {
        if (!gameActive || gameWon) return;

        timeRemaining -= Time.deltaTime;

        if (timerText != null)
            timerText.text = $"TIME: {Mathf.CeilToInt(timeRemaining)}s";

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            TriggerFailure();
        }
    }

    public void SetPuzzleSolved(int index, bool solved)
    {
        if (!gameActive || gameWon) return;

        switch (index)
        {
            case 1: puzzle1Solved = solved; break;
            case 2: puzzle2Solved = solved; break;
            case 3: puzzle3Solved = solved; break;
            case 4: puzzle4Solved = solved; break;
            case 5: puzzle5Solved = solved; break;
        }

        RecalculateCompleted();
        UpdateUI();

        if (completedPuzzles >= totalPuzzles)
            TriggerWin();
    }

    void TriggerWin()
    {
        gameWon = true;
        gameActive = false;

        if (door != null) door.OpenDoor();
        if (confettiSpawner != null) confettiSpawner.Play();

        if (winText != null)
        {
            winText.gameObject.SetActive(true);
            winText.text = "YOU WON!";
        }

        if (timerText != null) timerText.gameObject.SetActive(false);

        Debug.Log("PuzzleManager: All puzzles solved — YOU WIN!");
    }

    void TriggerFailure()
    {
        gameActive = false;

        if (failText != null)
        {
            failText.gameObject.SetActive(true);
            failText.text = "TIME'S UP!";
        }

        Debug.Log("PuzzleManager: Time ran out — resetting puzzles.");
        StartCoroutine(ResetAfterDelay(2.5f));
    }

    IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetAllPuzzles();
    }

void ResetAllPuzzles()
{
    puzzle1Solved = false;
    puzzle2Solved = false;
    puzzle3Solved = false;
    puzzle4Solved = false;
    puzzle5Solved = false;

    timeRemaining = timeLimit;
    gameActive = true;
    gameWon = false;

    RecalculateCompleted();
    UpdateUI();

    if (failText != null)   failText.gameObject.SetActive(false);
    if (winText != null)    winText.gameObject.SetActive(false);
    if (timerText != null)  timerText.gameObject.SetActive(true);
    if (statusText != null) statusText.text = "";
    if (door != null)       door.CloseDoor();

    // Reset all puzzles — physical objects + internal state
    FindFirstObjectByType<ColorPad>()?.ResetPuzzle();
    FindFirstObjectByType<WeightPlate>()?.ResetPuzzle();
    FindFirstObjectByType<HeavyObject>()?.ResetPuzzle();
    FindFirstObjectByType<BookPuzzle>()?.ResetPuzzle();
    FindFirstObjectByType<LightPuzzle>()?.ResetPuzzle();
    FindFirstObjectByType<ButtonPuzzle>()?.ResetPuzzle();

    Debug.Log("PuzzleManager: All puzzles reset.");
}

    void RecalculateCompleted()
    {
        completedPuzzles = 0;
        if (puzzle1Solved) completedPuzzles++;
        if (puzzle2Solved) completedPuzzles++;
        if (puzzle3Solved) completedPuzzles++;
        if (puzzle4Solved) completedPuzzles++;
        if (puzzle5Solved) completedPuzzles++;
    }

    void UpdateUI()
    {
        if (progressText != null)
            progressText.text = $"PUZZLE PROGRESS: {completedPuzzles} / {totalPuzzles}";
    }

    public void ShowStatusMessage(string message, float clearAfterSeconds = 0f)
    {
        if (statusText == null) return;
        statusText.text = message;
        if (clearAfterSeconds > 0f)
            StartCoroutine(ClearStatusAfterDelay(clearAfterSeconds));
    }

    IEnumerator ClearStatusAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (statusText != null) statusText.text = "";
    }
}