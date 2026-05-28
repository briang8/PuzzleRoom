using UnityEngine;

public class BookPuzzle : MonoBehaviour
{
    public static BookPuzzle Instance;

    public int book0TargetStep = 1;
    public int book1TargetStep = 3;

    int book0CurrentStep = 0;
    int book1CurrentStep = 0;
    bool isSolved = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void OnBookRotated(int index, int step)
    {
        if (index == 0) book0CurrentStep = step;
        else if (index == 1) book1CurrentStep = step;

        Debug.Log($"BookPuzzle: book {index} at step {step}");

        CheckState();
    }

    void CheckState()
    {
        bool nowSolved =
            book0CurrentStep == book0TargetStep &&
            book1CurrentStep == book1TargetStep;

        if (nowSolved && !isSolved)
        {
            isSolved = true;
            Debug.Log("Book puzzle solved");
            PuzzleManager.Instance.SetPuzzleSolved(3, true);
        }
        else if (!nowSolved && isSolved)
        {
            isSolved = false;
            Debug.Log("Book puzzle no longer solved");
            PuzzleManager.Instance.SetPuzzleSolved(3, false);
        }
    }

    public void ResetPuzzle()
    {
        isSolved = false;
        book0CurrentStep = 0;
        book1CurrentStep = 0;
        PuzzleManager.Instance.SetPuzzleSolved(3, false);
    }
}