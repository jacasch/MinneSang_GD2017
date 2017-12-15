using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalNpc : Npc {
    public string relatedQuest;
    public Phrase afterDropLine;
    public Item questDrop;
    private bool itemExists = false;
    private int newIndex = 0;
    private float itemTimer = 30.0f;
    float currCountdownValue;
    private AudioSource sourceChild;

    //public Phrase[] questDialogue;

    public override void NextPhrase()
    {

            //if we are in the right quest
            if (relatedQuest == player.GetComponent<PlayerQuestHandler>().activeQuest && !itemExists)
        {
                //check if player already has the item we would give him
                if (!player.GetComponent<PlayerQuestHandler>().HasItem(questDrop.name))
                {
                    if (activePhraseIndex > 0)
                    {
                        EndInteraction();
                    }
                    else
                    {
                        activePhrase = afterDropLine;
                        activePhraseIndex++;
                    }
                }
                else
                //check if quest dialogue hes ended
                if (activePhraseIndex >= questDialogue.Length)
                {

                    DropItem();
                    itemExists = true;
                

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
            newIndex += 1;

            if (newIndex >= randomLines.Length)
            {
                newIndex = 0;
            }

            activePhraseIndex = newIndex;
            activePhrase = randomLines[activePhraseIndex];
            EndInteraction();
        }
        
    }

    private void DropItem()
    {
       if (questDrop.drop != null) {
            sourceChild = GetComponent<AudioSource>();
            AudioClip clip = UnityEditor.AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Sounds/Items/Item drop.wav");
            sourceChild.PlayOneShot(clip);
            StartCoroutine(StartCountdown());
        }
    }

    public IEnumerator StartCountdown(float countdownValue = 1)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue >= 0)
        {
            // Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(2.0f);
            currCountdownValue--;
            if (currCountdownValue == 0)
            {
                GameObject drop = Instantiate(questDrop.drop, transform.position, transform.rotation);
                drop.GetComponent<ItemHandler>().SetName(questDrop.name);
            }
        }
    }

}
