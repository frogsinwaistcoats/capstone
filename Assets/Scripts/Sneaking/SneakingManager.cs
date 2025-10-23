using UnityEngine;
using UnityEngine.SceneManagement;

public class SneakingManager : MonoBehaviour
{
    public static SneakingManager instance;

    public GameObject instructionScreen;
    public GameObject failScreen;
    public GameObject successScreen;
    
    SneakingPlayerController player;
    SneakingTeacherController teacher;
    public GameObject playerStartPos;
    public GameObject teacherStartPos;

    private void Awake()
    {
        instance = this;
        player = FindFirstObjectByType<SneakingPlayerController>();
        teacher = FindFirstObjectByType<SneakingTeacherController>();
    }

    public void StartGame()
    {
        instructionScreen.SetActive(false);
        player.canMove = true;
    }

    public void Fail()
    {
        failScreen.SetActive(true);
    }

    public void Succeed()
    {
        
        successScreen.SetActive(true);
    }

    public void RestartSneaking()
    {
        player.transform.position = playerStartPos.transform.position;
        teacher.transform.position = teacherStartPos.transform.position;

        failScreen.SetActive(false);
        player.playerTurn = true;
        teacher.isMoving = false;
        player.movePoint.position = playerStartPos.transform.position;
        teacher.path.Clear();
        teacher.currentNode = teacher.startNode;
    }

    public void GoToForest()
    {
        GameManager.instance.GoToForestScene();
    }
}
