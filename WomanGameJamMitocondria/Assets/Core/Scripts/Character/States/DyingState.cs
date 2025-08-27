using UnityEngine;

public class DyingState : FSMTemplateState
{
    public DyingState(FSMTemplateMachine fsm) : base(fsm) { }

    public override void Enter()
    {
        if (((Character)_fsm).Sanity != null)
            ((Character)_fsm).Sanity.UpdateMusicFlag = false;

        AudioManager.Instance.StopHeartBeat();
        AudioManager.Instance.StopEarRing();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().Die();
        //AudioManager.Instance.ChangeMusic(MusicPiece.DefinitiveDeath);
        GameManager.Instance.StartReset();
    }
}
