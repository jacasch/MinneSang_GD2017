using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrot : Npc
{
    public AudioClip flapClip;


    // private string princess = "Princess Freya";
    public Act[] sendingLetters;

    public GameObject parrot;

    //public Phrase afterDropLine;
    //public Item questDrop;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void NextPhrase()
    {

        //if we mastered the previous act, we will enter the new act


        Act whichLetter = sendingLetters[player.GetComponent<PlayerQuestHandler>().letters];

        //if we are just starting an act, the headmaster will initialize this act with a dialogue
        //if dialogue is over we start can spek with the master of that act
        if (player.GetComponent<PlayerQuestHandler>().letterSend == false && player.GetComponent<PlayerQuestHandler>().activeAct >= 1)
        {
            // Zhe fucking Problem >>>
            if (activePhraseIndex >= whichLetter.dialogue.Length)
            {
                // player.GetComponent<PlayerQuestHandler>().activeQuest = "running";
                // Order of letters in different acts
                player.GetComponent<PlayerQuestHandler>().letters++;
                player.GetComponent<PlayerQuestHandler>().letterSend = true;
                //player.GetComponent<PlayerQuestHandler>().optionToSendLetters = false;

                parrot = GameObject.Find("Parrot Parent/parrot");
                Animator animator = parrot.GetComponent<Animator>();
                AudioSource sourceChild = parrot.GetComponent<AudioSource>();
                /*AudioClip clip = UnityEditor.AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Sounds/Quest/Wing Flaps.wav");*/

                if (player.GetComponent<PlayerQuestHandler>().letterSend == true)
                {
                    sourceChild.PlayOneShot(flapClip);
                    animator.SetBool("flying", true);
                } else
                {
                    animator.SetBool("flying", false);
                }
                EndInteraction();
            }
            else
            { //else we progress in the dialogues
                activePhrase = whichLetter.dialogue[activePhraseIndex];
                activePhraseIndex++;
            }
        }
        if (player.GetComponent<PlayerQuestHandler>().letterSend == true || player.GetComponent<PlayerQuestHandler>().activeAct <= 0)
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