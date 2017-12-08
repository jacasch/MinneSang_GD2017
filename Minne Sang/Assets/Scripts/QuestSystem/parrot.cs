using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parrot : Npc
{

    public Act[] sendingLetters;
    private int numberLetters = 4;
    private int letters = 0;
    private bool letterSend = false;

    //public Phrase afterDropLine;
    //public Item questDrop;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void NextPhrase()
    {

         //if we mastered the previous act, we will enter the new act
        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "mastered")
        {
            // player.GetComponent<PlayerQuestHandler>().activeAct++;
            //letterSend = false;
        } 

        Act whichLetter = sendingLetters[letters];

        //if we are just starting an act, the headmaster will initialize this act with a dialogue
        //if dialogue is over we start can spek with the master of that act
        if (letterSend == false)
        {
            // Zhe fucking Problem >>>
            if (activePhraseIndex >= whichLetter.dialogue.Length)
            {
                // player.GetComponent<PlayerQuestHandler>().activeQuest = "running";
                // Order of letters in different acts
                letters++;
                letterSend = true;
                EndInteraction();
            }
            else
            { //else we progress in the dialogues
                activePhrase = whichLetter.dialogue[activePhraseIndex];
                activePhraseIndex++;
            }
        }
        if (letterSend == true)
        { //else we just drop a random line and then end the interaction
            int newIndex;
            do
            {
                newIndex = Random.Range(0, randomLines.Length);
            } while (newIndex == activePhraseIndex);

            activePhraseIndex = newIndex;
            activePhrase = randomLines[activePhraseIndex];
        }
    }
}