using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the control of the puzzles currently happening
/// </summary>
public class ColorPuzzle : MonoBehaviour
{
    // EXTERNAL
    [HideInInspector]
    public Screen directionScreen;
    [SerializeField]
    private TimerScreen timerScreen;
    [HideInInspector]
    public List<Screen> colorScreens;

    // GENERIC PRIVATE

    private PuzzleStruct[] puzzlesLoaded;
    private int currentPuzzle;

    private float puzzleTimer;
    private float timeSinceLastCountdown;
    private bool shouldUseTimer;
    private bool isPuzzleOngoing = true;


    // ANSWERS
    private List<CustomColor> colorAnswers = new List<CustomColor>();


    // EVENTS

    [SerializeField]
    private UnityEvent<int> OnTimerCountDown;

    public UnityEvent OnTimerEnd;

    public UnityEvent OnSuccessfulSolve;

    public UnityEvent OnFailedSolve;

    /// <summary>
    /// returns a value that represents how the puzzles were finished.
    /// 
    /// 0 - success
    /// 1 - fail
    /// 2 - time end
    /// 
    /// </summary>
    public UnityEvent<int> OnFinishedPuzzles;

    private void Start()
    {
        // Registering our listeners.
        OnSuccessfulSolve.AddListener(SuccessfulSolve);
        OnFailedSolve.AddListener(FailedSolve);
        OnTimerEnd.AddListener(TimerFailedSolve);
    }

    private void Update()
    {
        // Timer mechanics. Calls OnTimerEnd event when it runs out of time.
        if (shouldUseTimer)
        {
            if (Time.time - timeSinceLastCountdown > 1)
            {
                if (puzzleTimer > 0)
                {
                    puzzleTimer--;
                    timeSinceLastCountdown = Time.time;
                    OnTimerCountDown?.Invoke((int)puzzleTimer);
                }
                if (puzzleTimer <= 0 && isPuzzleOngoing)
                {
                    OnTimerEnd?.Invoke();
                    OnFinishedPuzzles?.Invoke(2);
                    shouldUseTimer = false;
                }
            }
        }
    }

    public void AddAnswer(CustomColor answer)
    {
        if (isPuzzleOngoing == false)
        {
            return;
        }
        if (colorAnswers.Count < puzzlesLoaded[currentPuzzle].colors.Length)
        {

            colorAnswers.Add(answer);
            if (puzzlesLoaded[currentPuzzle].colors.Length == colorAnswers.Count)
            {
                SolveAnswers();
            }
        }
    }


    /// <summary>
    /// Solves the answer, by comparing the colors we have and which direction it is
    /// </summary>
    private void SolveAnswers()
    {
        List<bool> answers = new List<bool>();
        int puzzleLength = puzzlesLoaded[currentPuzzle].colors.Length - 1;
        for (int i = 0; i < puzzlesLoaded[currentPuzzle].colors.Length; i++)
        {
            // if the direction is UP or LEFT
            if (puzzlesLoaded[currentPuzzle].direction == CustomDir.LEFT || puzzlesLoaded[currentPuzzle].direction == CustomDir.UP)
            {
                if (puzzlesLoaded[currentPuzzle].colors[puzzleLength] == colorAnswers[i])
                {
                    answers.Add(true);
                }
                else
                {
                    answers.Add(false);
                }
                puzzleLength--;

                continue;
            }


            // if the direction is DOWN or RIGHT
            if (puzzlesLoaded[currentPuzzle].colors[i] == colorAnswers[i])
            {
                answers.Add(true);
            }
            else
            {
                answers.Add(false);
            }
        }

        // Check if answers are correct or not, and call the appropriate event
        if (answers.Contains(false))
        {
            OnFailedSolve?.Invoke();
        }
        else
        {
            OnSuccessfulSolve?.Invoke();
        }
    }

    /// <summary>
    /// called when the right sequence of colors were used.
    /// </summary>
    private void SuccessfulSolve()
    {
        // stops the puzzle.
        isPuzzleOngoing = false;

        currentPuzzle++;

        if (currentPuzzle < puzzlesLoaded.Length)
        {
            // more puzzles to be done
            StartPuzzle();
        }
        else
        {
            OnFinishedPuzzles?.Invoke(0);
        }
    }

    /// <summary>
    /// Called when timer runs out or the wrong sequence of colors were used.
    /// </summary>
    private void FailedSolve()
    {
        colorAnswers = new List<CustomColor>();
    }

    private void TimerFailedSolve()
    {
        isPuzzleOngoing = false;

        currentPuzzle++;

        if (currentPuzzle < puzzlesLoaded.Length)
        {
            // more puzzles to be done
            StartPuzzle();
            //shouldUseTimer = true;
        }
        else
        {
            OnFinishedPuzzles?.Invoke(0);
        }
    }

    /// <summary>
    /// Loads the data into the "loadedPuzzleStruct" property. This will also reset the puzzle first to avoid any clashing.
    /// </summary>
    /// <param name="data"> what struct do you want to load? </param>
    public void LoadPuzzleData(PuzzleStruct[] data)
    {
        currentPuzzle = 0;
        if (puzzlesLoaded != null)
        {
            ResetPuzzle();
            puzzlesLoaded = data;
        }
        else
        {
            puzzlesLoaded = data;
        }

        StartPuzzle();
    }

    /// <summary>
    /// Starts the puzzle.
    /// </summary>
    public void StartPuzzle()
    {
        // reset the puzzle status to ongoing.
        isPuzzleOngoing = true;

        colorAnswers = new List<CustomColor>();

        puzzleTimer = puzzlesLoaded[currentPuzzle].time;

        // if we need a timer. If the data reads anything 0 and below, it means that no timer is needed.
        if (puzzleTimer > 0)
        {
            shouldUseTimer = true;
            timeSinceLastCountdown = Time.time;
        }

        // set up the timer screen and start it.
        if (puzzleTimer > 0)
        {
            timerScreen.UpdateText((int)puzzleTimer);
        }
        else
        {
            timerScreen.UpdateText(0);

        }

        // set up the direction screen
        directionScreen.SwitchScreen(puzzlesLoaded[currentPuzzle].direction);

        // load of the right materials based on which color is in the puzzle
        for (int i = 0; i < puzzlesLoaded[currentPuzzle].colors.Length; i++)
        {
            colorScreens[i].SwitchScreen(puzzlesLoaded[currentPuzzle].colors[i]);
        }
    }

    /// <summary>
    /// Resets the entirety of the puzzle.
    /// </summary>
    public void ResetPuzzle()
    {
        directionScreen.ResetScreen();
        timerScreen.ResetText();
        colorAnswers = new List<CustomColor>();
        shouldUseTimer = false;
        foreach (var item in colorScreens)
        {
            item.ResetScreen();
        }
    }
}