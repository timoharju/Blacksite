using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All the audio is in this class
public class Audio
{

    AudioSource audioFootsteps;
    AudioSource audioAmbient;
    AudioSource audioMetalDoor;
    AudioSource audioConcreteWalk;
    AudioSource audioPrisoncellDoor;
    AudioSource audioWaterdrop;
    AudioSource audioBeep;
    AudioSource audioAccessGranted;
    AudioSource audioAccessDenied;
    AudioSource audioInventoryOpen;
    AudioSource audioClick;
    AudioSource audioControlRoom;



    //Constructor
    public Audio(AudioSource audioFootsteps, AudioSource audioAmbient, AudioSource audioMetalDoor, AudioSource audioConcreteWalk, AudioSource audioPrisoncellDoor, AudioSource audioWaterdrop, AudioSource audioBeep, AudioSource audioAccessGranted, AudioSource audioAccessDenied, AudioSource audioInventoryOpen, AudioSource audioClick, AudioSource audioControlRoom)
    {
        this.audioFootsteps = audioFootsteps;
        this.audioAmbient = audioAmbient;
        this.audioMetalDoor = audioMetalDoor;
        this.audioConcreteWalk = audioConcreteWalk;
        this.audioPrisoncellDoor = audioPrisoncellDoor;
        this.audioWaterdrop = audioWaterdrop;
        this.audioBeep = audioBeep;
        this.audioAccessGranted = audioAccessGranted;
        this.audioAccessDenied = audioAccessDenied;
        this.audioInventoryOpen = audioInventoryOpen;
        this.audioClick = audioClick;
        this.audioControlRoom = audioControlRoom;

    }

    public Audio()
    {

        audioFootsteps = GameObject.Find("Footsteps").GetComponent<AudioSource>();
        audioAmbient = GameObject.Find("Ambient").GetComponent<AudioSource>();
        audioMetalDoor = GameObject.Find("MetalDoor").GetComponent<AudioSource>();
        audioConcreteWalk = GameObject.Find("ConcreteWalk").GetComponent<AudioSource>();
        audioPrisoncellDoor = GameObject.Find("PrisoncellDoor").GetComponent<AudioSource>();
        audioWaterdrop = GameObject.Find("Waterdrop").GetComponent<AudioSource>();
        audioBeep = GameObject.Find("Beep").GetComponent<AudioSource>();
        audioAccessGranted = GameObject.Find("AccessGranted").GetComponent<AudioSource>();
        audioAccessDenied = GameObject.Find("AccessDenied").GetComponent<AudioSource>();
        audioInventoryOpen = GameObject.Find("InventoryOpen").GetComponent<AudioSource>();
        audioClick = GameObject.Find("Click").GetComponent<AudioSource>();
        audioControlRoom = GameObject.Find("ControlRoom").GetComponent<AudioSource>();
    }


    //Plays the audio of the Footsteps01.wav
    public void FootstepAudio()
    {
        //.Play(44000) for 0,5 sec delay

        if (!audioFootsteps.isPlaying)
        {
            audioFootsteps.Play();
        }

    }
    //Plays the audio of the Taustamölyä.wav
    public void AmbientAudio()
    {

        audioAmbient.Play();
    }
    //Plays the audio of the Open&Close(Metal Door).mp3
    public void MetalDoorAudio()
    {
        audioMetalDoor.Play();
    }
    //Plays the audio of the Walking on concrete.mp3
    public void ConcreteFootstepAudio()
    {
        audioConcreteWalk.Play();
    }
    //Plays the audio of the PrisonCellDoor.mp3
    public void PrisoncellDoorAudio()
    {
        audioPrisoncellDoor.Play();
    }
    //Plays the audio of the Waterdrop.mp3
    public void WaterdropAudio()
    {
        audioWaterdrop.Play();
    }
    //Plays the audio of the beep.ogg
    public void BeepAudio()
    {
        audioBeep.Play();
    }
    //Plays the audio of the acess granted.ogg
    public void AccessGrantedAudio()
    {
        audioAccessGranted.Play();
    }
    //Plays the audio of the AccessDenied1.wav
    public void AccessDeniedAudio()
    {
        audioAccessDenied.Play();
    }
    //Plays the audio of the InventoryOpen1.wav
    public void InventoryOpenAudio()
    {
        audioInventoryOpen.Play();
    }
    //Plays the audio of the Walking on click.wav
    public void InventoryClickAudio()
    {
        audioClick.Play();
    }
    public void ControlRoomAudio()
    {
        if (!audioControlRoom.isPlaying)
        {
            audioControlRoom.Play();
        }
        
    }

}
