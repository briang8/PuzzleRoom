using TMPro;
using UnityEngine;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    public TextMeshProUGUI progressText;
    public TextMeshProUGUI statusText;

    public int totalPuzzles = 5; // now 5 puzzles

    [Header("Puzzle States")]
    public bool puzzle1Solved; // color pad
    public bool puzzle2Solved; // buttons
    public bool puzzle3Solved; // books
    public bool puzzle4Solved; // lights
    public bool puzzle5Solved; // weight plate

    [Header("Door")]
    public DoorController door; // assign in Inspector

    [Header("Player / Room State")]
    public bool playerHasEnteredRoom = false; // becomes true when door has closed behind player

    int completedPuzzles = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
{
    RecalculateCompleted();
    UpdateUI();

    if (statusText != null)
        statusText.text = "";

    if (door != null)
        door.OpenDoor();
}

    public void SetPuzzleSolved(int index, bool solved)
    {
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
        UpdateDoorAfterPuzzleChange();
    }

    public IEnumerator CloseDoorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!playerHasEnteredRoom) // only once
        {
            SetPlayerEnteredRoom();
        }
    }

    public void SetPlayerEnteredRoom()
    {
        if (playerHasEnteredRoom) return;

        playerHasEnteredRoom = true;
        Debug.Log("PuzzleManager: Player has entered the room (door closing).");

        if (door != null)
            door.CloseDoor();
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

    void UpdateDoorAfterPuzzleChange()
    {
        if (door == null) return;

        // Before entering the room, puzzles do not affect the door.
        if (!playerHasEnteredRoom) return;

        // After entering: only reopen door when all puzzles solved.
        if (completedPuzzles >= totalPuzzles)
        {
            door.OpenDoor();
        }
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
    if (statusText != null)
        statusText.text = "";
}

    }