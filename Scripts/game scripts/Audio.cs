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
    AudioSource audioMumble1;
    AudioSource audioMumble2;
    AudioSource audioMumble3;
    AudioSource audioMumble4;
    AudioSource audioMumble5;
    AudioSource audioBlop;
    AudioSource audioAlienCreepy1;
    AudioSource audioAlienCreepy2;
    AudioSource audioAlienCreepy3;
    AudioSource audioObjectDropWater;
    AudioSource audioPaperCrumble;
    AudioSource audioNormalDoorOpen;
    AudioSource audioMumblegreat;
    AudioSource audioMumble4short;
    AudioSource audioMumble3short;
    AudioSource audioMumble1short;

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
        audioMumble1 = GameObject.Find("Mumble1").GetComponent<AudioSource>();
        audioMumble2 = GameObject.Find("Mumble2").GetComponent<AudioSource>();
        audioMumble3 = GameObject.Find("Mumble3").GetComponent<AudioSource>();
        audioMumble4 = GameObject.Find("Mumble4").GetComponent<AudioSource>();
        audioMumble5 = GameObject.Find("Mumble5").GetComponent<AudioSource>();
        audioBlop = GameObject.Find("Blop").GetComponent<AudioSource>();
        audioAlienCreepy1 = GameObject.Find("Aliencreepy1").GetComponent<AudioSource>();
        audioAlienCreepy2 = GameObject.Find("Aliencreepy2").GetComponent<AudioSource>();
        audioAlienCreepy3 = GameObject.Find("Aliencreepy3").GetComponent<AudioSource>();
        audioObjectDropWater = GameObject.Find("ObjectDropWater").GetComponent<AudioSource>();
        audioPaperCrumble = GameObject.Find("PaperCrumble").GetComponent<AudioSource>();
        audioNormalDoorOpen = GameObject.Find("NormalDoorOpen").GetComponent<AudioSource>();
        audioMumblegreat = GameObject.Find("Mumblegreat").GetComponent<AudioSource>();
        audioMumble4short = GameObject.Find("Mumble4short").GetComponent<AudioSource>();
        audioMumble1short = GameObject.Find("Mumble1short").GetComponent<AudioSource>();
        audioMumble3short = GameObject.Find("Mumble3short").GetComponent<AudioSource>();


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
        audioMumble1 = GameObject.Find("Mumble1").GetComponent<AudioSource>();
        audioMumble2 = GameObject.Find("Mumble2").GetComponent<AudioSource>();
        audioMumble3 = GameObject.Find("Mumble3").GetComponent<AudioSource>();
        audioMumble4 = GameObject.Find("Mumble4").GetComponent<AudioSource>();
        audioBlop = GameObject.Find("Blop").GetComponent<AudioSource>();
        audioAlienCreepy1 = GameObject.Find("Aliencreepy1").GetComponent<AudioSource>();
        audioAlienCreepy2 = GameObject.Find("Aliencreepy2").GetComponent<AudioSource>();
        audioAlienCreepy3 = GameObject.Find("Aliencreepy3").GetComponent<AudioSource>();
        audioObjectDropWater = GameObject.Find("ObjectDropWater").GetComponent<AudioSource>();
        audioPaperCrumble = GameObject.Find("PaperCrumble").GetComponent<AudioSource>();
        audioNormalDoorOpen = GameObject.Find("NormalDoorOpen").GetComponent<AudioSource>();
        audioMumblegreat = GameObject.Find("Mumblegreat").GetComponent<AudioSource>();
        audioMumble4short = GameObject.Find("Mumble4short").GetComponent<AudioSource>();
        audioMumble5 = GameObject.Find("Mumble5").GetComponent<AudioSource>();
        audioMumble1short = GameObject.Find("Mumble1short").GetComponent<AudioSource>();
        audioMumble3short = GameObject.Find("Mumble3short").GetComponent<AudioSource>();
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

        if (hasPlayed == false)
        {

            audioElectricDoorOpen.Play();
            hasPlayed = true;
        }

    }
    //Plays the audio of the Control room.wav
    public void ControlRoomAudio()
    {
        if (!audioControlRoom.isPlaying)
        {
            audioControlRoom.Play();
        }

        //Mumble audio
    }
    public void Mumble1Audio()
    {
        if (!audioMumble1.isPlaying)
        {
            audioMumble1.Play();
        }

        //Mumble audio
    }
    public void Mumble2Audio()
    {
        if (!audioMumble2.isPlaying)
        {
            audioMumble2.Play();
        }

        //Mumble audio
    }
    public void Mumble3Audio()
    {
        if (!audioMumble3.isPlaying)
        {
            audioMumble3.Play();
        }

        //Mumble audio

    }
    public void Mumble4Audio()
    {
        if (!audioMumble4.isPlaying)
        {
            audioMumble4.Play();
        }


    }
    public void BlopAudio()
    {
        if (!audioBlop.isPlaying)
        {
            audioBlop.Play();
        }


    }
    public void AlienCreepy1Audio()
    {
        if (!audioAlienCreepy1.isPlaying)
        {
            audioAlienCreepy1.Play();
        }


    }
    public void AlienCreepy2Audio()
    {
        if (!audioAlienCreepy2.isPlaying)
        {
            audioAlienCreepy2.Play();
        }


    }
    public void AlienCreepy3Audio()
    {
        if (!audioAlienCreepy3.isPlaying)
        {
            audioAlienCreepy3.Play();
        }


    }
    public void ObjectDropWaterAudio()
    {
        if (!audioObjectDropWater.isPlaying)
        {
            audioObjectDropWater.Play();
        }


    }
    public void PaperCrumbleAudio()
    {
        if (!audioPaperCrumble.isPlaying)
        {
            audioPaperCrumble.Play();
        }


    }
    public void NormalDoorOpenAudio()
    {
        if (!audioNormalDoorOpen.isPlaying)
        {
            audioNormalDoorOpen.Play();

        }
    }
    public void MumblegreatAudio()
    {
        if (!audioMumblegreat.isPlaying)
        {
            audioMumblegreat.Play(88000);

        }
    }
    public void Mumblegreat2Audio()
    {
        if (!audioMumblegreat.isPlaying)
        {
            audioMumblegreat.Play(132000);

        }
    }
    public void Mumblegreat3Audio()
    {
        if (!audioMumblegreat.isPlaying)
        {
            audioMumblegreat.Play();

        }
    }
    public void Mumble4shortAudio()
    {
        if (!audioMumble4short.isPlaying)
        {
            audioMumble4short.Play();

        }
    }
    public void Mumble5Audio()
    {
        if (!audioMumble5.isPlaying)
        {
            audioMumble5.Play();
        }

        //Mumble audio
    }

    public void Mumble1shortAudio()
    {
        if (!audioMumble1short.isPlaying)
        {
            audioMumble1short.Play();
        }

    }

    public void Mumble3shortAudio()
    {
        if (!audioMumble3short.isPlaying)
        {
            audioMumble3short.Play();
        }

    }
}



