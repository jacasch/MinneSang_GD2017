using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Npc
{
    public int relevantAct;
    public Quest quest;
    public Phrase encounterBeforeHeadMaster;
    public GameObject chest;

    public string targetScene;

    //public Phrase afterDropLine;
    //public Item questDrop;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void NextPhrase()
    {
        //if the player hasnt talked to the headmaster yet, tell him do do so;
        if (player.GetComponent<PlayerQuestHandler>().activeAct == -1)
        {
            if (activePhraseIndex >= 1)
                //EndInteraction();
            activePhrase = encounterBeforeHeadMaster;
            activePhraseIndex++;
        }
        //check if we are ready to talk to this master
        else if (relevantAct == player.GetComponent<PlayerQuestHandler>().activeAct)
        {

            chest = GameObject.Find("Chest_real");
            Animator animator = chest.GetComponent<Animator>(); 
            { // weare ready to talk to this master.
                //if we have not yet gotten the quest, get the quest.
                if (player.GetComponent<PlayerQuestHandler>().activeQuest == "end")
                {
                    //if dialogue is over we will end the dialogue and give the quest
                    if (activePhraseIndex >= quest.dialogue.Length)
                    {
                        player.GetComponent<PlayerQuestHandler>().activeQuest = quest.name;
                        ending();
                    }
                    else
                    { //else we progress in the dialogues
                        activePhrase = quest.dialogue[activePhraseIndex];
                        activePhraseIndex++;

                        Debug.Log(activePhraseIndex);

                        if (activePhraseIndex == 3)
                        {
                            animator.SetBool("opening", true);
                        }
                    }
                }
                else
                { //we are already doing the quest he gave us
                  //if we have all the items do endDialogue

                    //if dialogue is over we will end the dialogue and enter the masterylevel
                    if (activePhraseIndex >= quest.endDialogue.Length)
                    {
                        //TODO GO TO NEXT LEVEL
                        player.GetComponent<PlayerQuestHandler>().activeQuest = "end";
                        ending();
                    }
                    else
                    { //else we progress in the dialogues
                        activePhrase = quest.endDialogue[activePhraseIndex];
                        activePhraseIndex++;

                        if (activePhraseIndex == 2)
                        {
                            chest = transform.Find("chest").gameObject;
                            animator = chest.GetComponent<Animator>();
                            animator.SetBool("opening", true);
                        }



                    }
                }
            }
        }
    }

    private void ending()
    {
        if (player.GetComponent<PlayerQuestHandler>().letters >= 4)
        {
            Debug.Log("Good Ending", gameObject);
        } else
        {
            Debug.Log("Bad Ending", gameObject);
        }
        EndInteraction();
    }
}