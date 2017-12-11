using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalNpc : Npc {
    public string relatedQuest;
    public Phrase afterDropLine;
    public Item questDrop;
    private bool itemExists = false;
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
                int newIndex;
                do
                {
                    newIndex = Random.Range(0, randomLines.Length);
                } while (newIndex == activePhraseIndex);

                activePhraseIndex = newIndex;
                activePhrase = randomLines[activePhraseIndex];
            }
        
    }

    private void DropItem()
    {
       if (questDrop.drop != null) {
            GameObject drop = Instantiate(questDrop.drop, transform.position, transform.rotation);
            drop.GetComponent<ItemHandler>().SetName(questDrop.name);
        }
    }

}
