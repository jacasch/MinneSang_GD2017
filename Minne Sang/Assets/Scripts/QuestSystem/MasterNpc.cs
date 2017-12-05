using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterNpc : Npc{
    public int relevantAct;
    public Quest quest;
    public Phrase encounterBeforeHeadMaster;

    //public Phrase afterDropLine;
    //public Item questDrop;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void NextPhrase()
    {
        //check if we are ready to talk to this master
        if (relevantAct == player.GetComponent<PlayerQuestHandler>().activeAct)
        {
            //if the player hasnt talked to the headmaster yet, tell him do do so;
            if (player.GetComponent<PlayerQuestHandler>().activeQuest == "started")
            {
                if (activePhraseIndex >= 1)
                    EndInteraction();
                activePhrase = encounterBeforeHeadMaster;
                activePhraseIndex++;
            }
            else
            { // weare ready to talk to this master.
                //if we have not yet gotten the quest, get the quest.
                if (player.GetComponent<PlayerQuestHandler>().activeQuest == "running")
                {
                    //if dialogue is over we will end the dialogue and give the quest
                    if (activePhraseIndex >= quest.dialogue.Length)
                    {
                        player.GetComponent<PlayerQuestHandler>().activeQuest = quest.name;
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
                            EndInteraction();
                        }
                        else
                        { //else we progress in the dialogues
                            activePhrase = quest.endDialogue[activePhraseIndex];
                            activePhraseIndex++;
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
