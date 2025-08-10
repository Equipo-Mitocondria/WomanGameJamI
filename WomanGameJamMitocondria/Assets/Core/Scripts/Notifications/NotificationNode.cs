using UnityEngine;
using UnityEngine.UI;

public class NotificationNode
{
    public int level;
    public int id;
    public Sprite avatar;
    public string speakerName;
    public string speakerEmotion;
    public Speakers speaker;
    public string message;
    public SanityEffect sanityEffect;

    public enum Speakers { Yuko, Client, Monster }

    public NotificationNode(int level, int id, string speakerName, string speakerEmotion, string message, string sanityChange, float sanityAmount)
    {
        this.level = level;
        this.id = id;
        this.speakerName = speakerName;
        this.speakerEmotion = speakerEmotion;
        this.message = message;

        this.speaker = ParseSpeaker(speakerName);
        this.avatar = LoadImage();
        this.sanityEffect = ParseSanityChange(sanityChange, sanityAmount);
    }

    private Sprite LoadImage()
    {
        string avatarPath = "Avatars/avatar_";
        avatarPath += speakerName + "_";
        avatarPath += speakerEmotion;

        Texture2D texture2D = Resources.Load<Texture2D>(avatarPath);
        Rect rec = new Rect(0, 0, texture2D.width, texture2D.height);

        return Sprite.Create(texture2D, rec, new Vector2(0, 0), .01f); ;
    }

    private Speakers ParseSpeaker(string speakerName)
    {
        switch (speakerName)
        {
            case "yuko":
                return Speakers.Yuko;
            case "client":
                return Speakers.Client;
            case "monster":
                return Speakers.Monster;
            default:
                return Speakers.Yuko;
        }
    }

    private SanityEffect ParseSanityChange(string sanityChange, float sanityAmount)
    {
        switch (sanityChange)
        {
            case "substract":
                return new SanityEffect(SanityChange.Substract, sanityAmount);
            case "add":
                return new SanityEffect(SanityChange.Add, sanityAmount);
            case "set":
                return new SanityEffect(SanityChange.Set, sanityAmount);
            default:
                return null;
        }
    }
}
