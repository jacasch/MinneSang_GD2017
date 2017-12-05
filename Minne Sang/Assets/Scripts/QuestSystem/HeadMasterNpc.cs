using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMasterNpc : Npc
{
    public Act[] acts;

    //public Phrase afterDropLine;
    //public Item questDrop;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void NextPhrase()
    {

        //if we mastered the previous act, we will enter the new act
        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "mastered") {
            player.GetComponent<PlayerQuestHandler>().activeAct++;
            player.GetComponent<PlayerQuestHandler>().activeQuest = "start";
        }

        Act currentAct = acts[player.GetComponent<PlayerQuestHandler>().activeAct];

        //if we are just starting an act, the headmaster will initialize this act with a dialogue
        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "start") {
            //if dialogue is over we start can spek with the master of that act
            if (activePhraseIndex >= currentAct.dialogue.Length) {
                player.GetComponent<PlayerQuestHandler>().activeQuest = "running";
                EndInteraction();
            } else { //else we progress in the dialogues
                activePhrase = currentAct.dialogue[activePhraseIndex];
                activePhraseIndex++;
            }
        }
        else { //else we just drop a random line and then end the interaction
            int newIndex;
            do
            {
                newIndex = Random.Range(0, currentAct.randomLines.Length);
            } while (newIndex == activePhraseIndex);

            activePhraseIndex = newIndex;
            activePhrase = currentAct.randomLines[activePhraseIndex];
        }
    }
}

