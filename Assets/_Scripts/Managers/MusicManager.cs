using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] MMF_Player _musicPlayer;

    [SerializeField] AudioClip _goodPlaceIntro;
    [SerializeField] AudioClip _goodPlaceLoop;
    [SerializeField] AudioClip _badPlaceIntro;
    [SerializeField] AudioClip _badPlaceLoop;

    public void StartGoodPlace()
    {
        MMF_MMSoundManagerSound soundFeedback = _musicPlayer.GetFeedbackOfType<MMF_MMSoundManagerSound>();
    }

}
