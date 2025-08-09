using UnityEngine;
using UnityEngine.UI;

public class NotificationNode
{
    public int level;
    public int id;
    public Image avatar;
    public string speakerName;
    public string speakerEmotion;
    public string message;
    public SanityEffect sanityEffect;

    public NotificationNode(int level, int id, string speakerName, string speakerEmotion, string message, SanityEffect sanityEffect)
    {
        this.level = level;
        this.id = id;
        this.speakerName = speakerName;
        this.speakerEmotion = speakerEmotion;
        this.message = message;
        this.sanityEffect = sanityEffect;

        this.avatar = LoadImage(speakerName, speakerEmotion);
    }

    private Image LoadImage(string speakerName, string speakerEmotion)
    {
        //TO DO
        return null;
    }
}
