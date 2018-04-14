using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All the audio and the audio methods are in this class
public class Audio
{
    Player player;
    AudioSource audioFootsteps;
    AudioSource audioAmbient;
    AudioSource audioMetalDoor;
    AudioSource audioConcreteWalk;
    AudioSource audioPrisoncellDoor;
    AudioSource audioWaterdrop;
    AudioSource audioWaterdropLow;
    AudioSource audioBeep;
    AudioSource audioAccessGranted;
    AudioSource audioAccessDenied;
    AudioSource audioInventoryOpen;
    AudioSource audioClick;
    AudioSource audioControlRoom;
    AudioSource audioToiletFlush;
    AudioSource audioElectricDoorOpen;

    //checks if the audio has played
    bool hasPlayed = false;
    bool hasPlayed2 = false;



    public Audio(Player player)
    {
        this.player = player;
        audioFootsteps = GameObject.Find("Footsteps").GetComponent<AudioSource>();
        audioAmbient = GameObject.Find("Ambient").GetComponent<AudioSource>();
        audioMetalDoor = GameObject.Find("MetalDoor").GetComponent<AudioSource>();
        audioConcreteWalk = GameObject.Find("ConcreteWalk").GetComponent<AudioSource>();
        audioPrisoncellDoor = GameObject.Find("PrisoncellDoor").GetComponent<AudioSource>();
        audioWaterdrop = GameObject.Find("Waterdrop").GetComponent<AudioSource>();
        audioWaterdropLow = GameObject.Find("WaterdropLow").GetComponent<AudioSource>();
        audioBeep = GameObject.Find("Beep").GetComponent<AudioSource>();
        audioAccessGranted = GameObject.Find("AccessGranted").GetComponent<AudioSource>();
        audioAccessDenied = GameObject.Find("AccessDenied").GetComponent<AudioSource>();
        audioInventoryOpen = GameObject.Find("InventoryOpen").GetComponent<AudioSource>();
        audioClick = GameObject.Find("Click").GetComponent<AudioSource>();
        audioControlRoom = GameObject.Find("ControlRoom").GetComponent<AudioSource>();
        audioToiletFlush = GameObject.Find("ToiletFlush").GetComponent<AudioSource>();
        audioElectricDoorOpen = GameObject.Find("ElectricDoorOpen").GetComponent<AudioSource>();
    }
    public Audio()
    {

        audioFootsteps = GameObject.Find("Footsteps").GetComponent<AudioSource>();
        audioAmbient = GameObject.Find("Ambient").GetComponent<AudioSource>();
        audioMetalDoor = GameObject.Find("MetalDoor").GetComponent<AudioSource>();
        audioConcreteWalk = GameObject.Find("ConcreteWalk").GetComponent<AudioSource>();
        audioPrisoncellDoor = GameObject.Find("PrisoncellDoor").GetComponent<AudioSource>();
        audioWaterdrop = GameObject.Find("Waterdrop").GetComponent<AudioSource>();
        audioWaterdropLow = GameObject.Find("WaterdropLow").GetComponent<AudioSource>();
        audioBeep = GameObject.Find("Beep").GetComponent<AudioSource>();
        audioAccessGranted = GameObject.Find("AccessGranted").GetComponent<AudioSource>();
        audioAccessDenied = GameObject.Find("AccessDenied").GetComponent<AudioSource>();
        audioInventoryOpen = GameObject.Find("InventoryOpen").GetComponent<AudioSource>();
        audioClick = GameObject.Find("Click").GetComponent<AudioSource>();
        audioControlRoom = GameObject.Find("ControlRoom").GetComponent<AudioSource>();
        audioToiletFlush = GameObject.Find("ToiletFlush").GetComponent<AudioSource>();
        audioElectricDoorOpen = GameObject.Find("ElectricDoorOpen").GetComponent<AudioSource>();
    }

    //stops the audio
    public void stopAudio()
    {
        if (player.Location.RoomName != "controlRoom" && player.Location.RoomName != "securityDoor")
        {
            audioControlRoom.Pause();
        }
        if (player.Location.RoomName != "cellRoom")
        {
            audioWaterdropLow.Pause();
        }
        if (player.Location.RoomName != "start")
        {
            audioWaterdrop.Pause();
        }
       

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
    //Plays the audio of the Black_Site_AmbientMusic.ogg
    public void AmbientAudio()
    {
        if (!audioAmbient.isPlaying)
        {
            audioAmbient.Play();
        }

        
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
    //Plays the audio of the PrisonCellDoor.mp3 and plays it once
    public void PrisoncellDoorAudio()
    {
        if (hasPlayed == false)
        {
            hasPlayed = true;
            audioPrisoncellDoor.Play();
        }

    }
    //Plays the audio of the Waterdrop.mp3
    public void WaterdropAudio()
    {
        if (!audioWaterdrop.isPlaying)
        {
            audioWaterdrop.Play();
        }
        
    }
    //Plays the audio of the Waterdrop.mp3
    public void WaterdropLowAudio()
    {
        if (!audioWaterdropLow.isPlaying)
        {
            audioWaterdropLow.Play();
        }
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
    //Plays the audio of the Toilet flush.wav
    public void ToiletFlushAudio()
    {
        if (!audioToiletFlush.isPlaying)
        {
            audioToiletFlush.Play();
        }
    }
    //Plays the audio of the electric_door_opening.wav and plays it once
    public void ElectricDoorOpenAudio()
    {

        if (!audioElectricDoorOpen.isPlaying)
        {
            
            audioElectricDoorOpen.Play();
        }
     
    }
    //Plays the audio of the Control room.wav
    public void ControlRoomAudio()
    {
        if (!audioControlRoom.isPlaying)
        {
            audioControlRoom.Play();
        }


    }

}
