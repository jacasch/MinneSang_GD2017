[System.Serializable]
public class Quest {
    public string name;
    public string[] itemsToCollect;
    public Phrase[] dialogue;
    public Phrase[] randomLines;
    public Phrase[] endDialogue;

    Quest(string name, string[] itemsToCollect) {
        this.name = name;
        this.itemsToCollect = itemsToCollect;
    }
}