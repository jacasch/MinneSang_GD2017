using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterNpc : Npc{
    public int relevantAct;
    public Quest quest;
    public Phrase encounterBeforeHeadMaster;
    public string targetScene;
    //public Phrase afterDropLine;
    //public Item questDrop;

    public override void Initialize()
    {
        base.Initialize();
        GameObject player = GameObject.Find("player");
        GameObject dancer = GameObject.Find("Umkleide");
        Animator animator = dancer.GetComponent<Animator>();
        if (player.GetComponent<PlayerQuestHandler>().activeAct >= 1)
        {
            animator.SetBool("finished", true);
        }
    }

    public override void NextPhrase()
    {
        //if the player hasnt talked to the headmaster yet, tell him do do so;
        if (player.GetComponent<PlayerQuestHandler>().activeAct == -1)
        {
            if (activePhraseIndex >= 1)
                EndInteraction();
            activePhrase = encounterBeforeHeadMaster;
            activePhraseIndex++;
        }
        //check if we are ready to talk to this master
        else if (relevantAct == player.GetComponent<PlayerQuestHandler>().activeAct)
        {
            
            { // weare ready to talk to this master.
                //if we have not yet gotten the quest, get the quest.
                if (player.GetComponent<PlayerQuestHandler>().activeQuest == "running")
                {
                    //if dialogue is over we will end the dialogue and give the quest
                    if (activePhraseIndex >= quest.dialogue.Length)
                    {
                        player.GetComponent<PlayerQuestHandler>().activeQuest = quest.name;
                        player.GetComponent<PlayerQuestHandler>().questItems = new List<string>();
                        foreach (string name in quest.itemsToCollect)
                        {
                            player.GetComponent<PlayerQuestHandler>().questItems.Add(name);
                        }
                        player.GetComponent<PlayerQuestHandler>().questEnabled = true;
                        EndInteraction();
                    }
                    else
                    { //else we progress in the dialogues
                        activePhrase = quest.dialogue[activePhraseIndex];
                        activePhraseIndex++;
                    }
                }
                else
                { //we are already doing the quest he gave us
                    //if we have all the items do endDialogue
                    if (PlayerHasAllItems())
                    {
                        //if dialogue is over we will end the dialogue and enter the masterylevel
                        if (activePhraseIndex >= quest.endDialogue.Length)
                        {
                            //TODO GO TO NEXT LEVEL
                            player.GetComponent<PlayerQuestHandler>().activeQuest = "mastered";
                            PlayerSpawnHandler psh = player.GetComponent<PlayerSpawnHandler>();
                            psh.targetScene = targetScene;
                            psh.targetSpawn = "masterystart";
                            psh.Respawn();
                            EndInteraction();
                        }
                        else
                        { //else we progress in the dialogues
                            activePhrase = quest.endDialogue[activePhraseIndex];
                            activePhraseIndex++;

                            // Trigger Dance animation
                            GameObject dancer = GameObject.Find("Umkleide");
                            Animator animator = dancer.GetComponent<Animator>();
                            AudioSource sourceChild = dancer.GetComponent<AudioSource>();
                            AudioClip clip = UnityEditor.AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Sounds/Quest/Dance master paper sliding.wav");

                            if (player.GetComponent<PlayerQuestHandler>().activeAct == 0 && activePhrase == quest.endDialogue[3])
                            {
                                animator.SetBool("folding", true);
                                sourceChild.PlayOneShot(clip);
                            }
                        }
                    }
                    //else we are in the quest but havent completed, so drop random questline
                    else
                    {
                        int newIndex;
                        do
                        {
                            newIndex = Random.Range(0, quest.randomLines.Length);
                        } while (newIndex == activePhraseIndex);

                        activePhraseIndex = newIndex;
                        activePhrase = quest.randomLines[activePhraseIndex];
                    }
                }
            }
        }
        else { //else we are not in theh right act so drop a random linie
            int newIndex;
            do
            {
                newIndex = Random.Range(0, randomLines.Length);
            } while (newIndex == activePhraseIndex);

            activePhraseIndex = newIndex;
            activePhrase = randomLines[activePhraseIndex];
        }

        /*
        //if we are in the right quest
        if (relatedQuest == player.GetComponent<PlayerQuestHandler>().activeQuest)
        {
            //check if player already has the item we would give him
            if (!player.GetComponent<PlayerQuestHandler>().HasItem(questDrop.name))
            {
                activePhrase = afterDropLine;
            }
            else
            //check if quest dialogue hes ended
            if (activePhraseIndex >= questDialogue.Length)
            {
                //drop the questdrop
                //DropItem();
                //abort conversation
                EndInteraction();
            }// else we are still talking in the dialogue
            else
            {
                activePhrase = questDialogue[activePhraseIndex];
                activePhraseIndex++;
            }
        }
        //if we are not in the right quest to dialoughe with this npc
        else
        {
            int newIndex;
            do
            {
                newIndex = Random.Range(0, randomLines.Length);
            } while (newIndex == activePhraseIndex);

            activePhraseIndex = newIndex;
            activePhrase = randomLines[activePhraseIndex];
        }*/
    }
    public bool PlayerHasAllItems() {
        bool hasAll = true;
        List<string> playerItems = player.GetComponent<PlayerQuestHandler>().collectedItems;

        foreach (string item in quest.itemsToCollect) {
            if (!playerItems.Contains(item))
                hasAll = false;
        }

        return hasAll;
    }
}
