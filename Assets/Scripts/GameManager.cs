using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        Instance=this;
    }
    public GameObject[] LevelContainer;
    private GameObject CurrentLevel;
    public GameObject LevelSelPan,inGameUI,LastLevel,NextPan,HomePan,SettingPanel,PausePanel;
    
    public Animator CanvasAnim; 
    private int LevelIndex=0;

    public int AttemptCnt=0;

    public GameObject[] Stars;

    //Spawns the level
    public void SpawnLevel(int i)
    {
        GameObject lvl= Instantiate(LevelContainer[i]);

        if(CurrentLevel!=null)
        {
            Destroy(CurrentLevel);
        }   

        CurrentLevel=lvl;
        CurrentLevel.SetActive(false);
        CanvasAnim.SetTrigger("Zoop");
        StartCoroutine(SwitchPanWithDelay(LevelSelPan,CurrentLevel,0.8f));
      
        inGameUI.SetActive(true);
        NextPan.SetActive(false);
        LevelIndex=i;
        AttemptCnt=0;
        foreach(var j in Stars)
        {
            j.SetActive(false);
        }
    }

    public void NextLevel()
    {
        if(LevelIndex>=LevelContainer.Length-1)
        {

           LastLevel.SetActive(true);
        }
        else
        {
            LevelIndex++;
          SpawnLevel(LevelIndex);
        }
    }

    //Resets the current scene 
    public void ResetScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void LevelComplete()
    {
        NextPan.SetActive(true);

        if(AttemptCnt==0)
        {
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(true);
        }else if(AttemptCnt==1)
        {
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
        }
        else
        {
            
            Stars[0].SetActive(true);
        }
        AttemptCnt=0;
    }

    public void RestartLevel()
    {
        SpawnLevel(LevelIndex);
        PausePanel.SetActive(false);
        Time.timeScale=1;
    }

    public void PlayBtn()
    {
        CanvasAnim.SetTrigger("Zoop");
        StartCoroutine(SwitchPanWithDelay(HomePan,LevelSelPan,0.8f));
    }

    IEnumerator SwitchPanWithDelay(GameObject i,GameObject j,float time)
    {
        yield return new WaitForSeconds(time);
        i.SetActive(false);
        j.SetActive(true);
    }

    public void HomeBack()
    {
        CanvasAnim.SetTrigger("Zoop");
        StartCoroutine(SwitchPanWithDelay(LevelSelPan,HomePan,0.8f));
    }


    public void OpenSettingToggle()
    {
        SettingPanel.SetActive(!SettingPanel.activeSelf);
    }

    public void OpenPausePanelToggle()
    {
        PausePanel.SetActive(!PausePanel.activeSelf);
        StartCoroutine(PauseAfterPop());
    }

    IEnumerator PauseAfterPop()
    {
        yield return new WaitForSecondsRealtime(.5f);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        Debug.Log(Time.timeScale);

    }
}
