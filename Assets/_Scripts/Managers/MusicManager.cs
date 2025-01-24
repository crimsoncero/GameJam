using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{

    [SerializeField] AudioClip _goodPlaceMusic;
    [SerializeField] AudioClip _badPlaceMusic;

    private AudioSource _source;

    public void StartGoodPlace()
    {
        _source =  MMSoundManager.Instance.PlaySound(_goodPlaceMusic, MMSoundManager.MMSoundManagerTracks.Music, Vector3.zero);
    }

    public void StartBadPlace()
    {
        if(_source != null)
            _source.Stop();
        MMSoundManager.Instance.PlaySound(_badPlaceMusic, MMSoundManager.MMSoundManagerTracks.Music, Vector3.zero);
    }

}
