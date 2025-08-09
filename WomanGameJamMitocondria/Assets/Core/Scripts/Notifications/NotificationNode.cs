using UnityEngine;
using UnityEngine.UI;

public class NotificationNode
{
    public int level;
    public int id;
    public Image avatar;
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

        this.avatar = LoadImage(speakerName, speakerEmotion);
        this.speaker = ParseSpeaker(speakerName);
        this.sanityEffect = ParseSanityChange(sanityChange, sanityAmount);
    }

    private Image LoadImage(string speakerName, string speakerEmotion)
    {
        return Resources.Load<Image>("/Notification Avatars/icon_yuko_avatar_placeholder_01.jpg1");
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
