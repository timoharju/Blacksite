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

    /// <summary>
    /// Stops the audio if player is in specific location
    /// </summary>

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
    /// <summary>
    ///  Plays the audio of the Footsteps01.wav
    ///  Checks if the audio is playing so it doesn't overlap
    /// </summary>

    public void FootstepAudio()
    {
        //.Play(44000) for 0,5 sec delay

        if (!audioFootsteps.isPlaying)
        {
            audioFootsteps.Play();
        }

    }
    /// <summary>
    /// Plays the audio of the Black_Site_AmbientMusic.ogg
    /// Checks if the audio is playing so it doesn't overlap
    /// </summary>

    public void AmbientAudio()
    {
        if (!audioAmbient.isPlaying)
        {
            audioAmbient.Play();
        }


    }

    /// <summary>
    /// Plays the audio of the Open&Close(Metal Door).mp3
    /// Checks if the audio is playing so it doesn't overlap
    /// </summary>
    public void MetalDoorAudio()
    {
        audioMetalDoor.Play();
    }
    /// <summary>
    /// Plays the audio of the Walking on concrete.mp3
    /// Checks if the audio is playing so it doesn't overlap
    /// </summary>

    public void ConcreteFootstepAudio()
    {
        audioConcreteWalk.Play();
    }
    /// <summary>
    ///  Plays the audio of the PrisonCellDoor.mp3 
    ///  Checks if the audio has played so it only plays once
    /// </summary>

    public void PrisoncellDoorAudio()
    {
        if (hasPlayed == false)
        {
            hasPlayed = true;
            audioPrisoncellDoor.Play();
        }

    }
    /// <summary>
    /// Plays the audio of the Waterdrop.mp3
    /// Checks if the audio is playing so it doesn't overlap
    /// </summary>

    public void WaterdropAudio()
    {
        if (!audioWaterdrop.isPlaying)
        {
            audioWaterdrop.Play();
        }

    }
    /// <summary>
    /// Plays the audio of the Waterdrop.mp3
    /// Checks if the audio is playing so it doesn't overlap
    /// </summary>

    public void WaterdropLowAudio()
    {
        if (!audioWaterdropLow.isPlaying)
        {
            audioWaterdropLow.Play();
        }
    }
    /// <summary>
    /// Plays the audio of the beep.ogg
    /// Checks if the audio is playing so it doesn't overlap
    /// </summary>

    public void BeepAudio()
    {
        audioBeep.Play();
    }
    /// <summary>
    /// Plays the audio of the acess granted.ogg
    /// </summary>

    public void AccessGrantedAudio()
    {
        audioAccessGranted.Play();
    }
    /// <summary>
    /// Plays the audio of the AccessDenied1.wav
    /// Checks if the audio is playing so it doesn't overlap
    /// </summary>

    public void AccessDeniedAudio()
    {
        audioAccessDenied.Play();
    }
    /// <summary>
    /// Plays the audio of the InventoryOpen1.wav
    /// </summary>

    public void InventoryOpenAudio()
    {
        audioInventoryOpen.Play();
    }
    /// <summary>
    /// Plays the audio of the click.wav
    /// </summary>

    public void InventoryClickAudio()
    {
        audioClick.Play();
    }
    /// <summary>
    /// //Plays the audio of the Toilet flush.wav
    /// Chekcs if the audio is playing so it doesn't overlap
    /// </summary>

    public void ToiletFlushAudio()
    {
        if (!audioToiletFlush.isPlaying)
        {
            audioToiletFlush.Play();
        }
    }
    /// <summary>
    /// Plays the audio of the electric_door_opening.wav and plays it once
    /// Checks if the audio is playing so it doesn't overlap
    /// </summary>

    public void ElectricDoorOpenAudio()
    {

        if (hasPlayed == false)
        {

            audioElectricDoorOpen.Play();
            hasPlayed = true;
        }

    }
    /// <summary>
    /// Plays the audio of the Control room.wav
    /// Checks if the audio is playing so it doesn't overlap
    /// </summary>

    public void ControlRoomAudio()
    {
        if (!audioControlRoom.isPlaying)
        {
            audioControlRoom.Play();
        }

        /// <summary>
        /// Plays the audio of the Mumble1.wav
        /// Checks if the audio is playing so it doesn't overlap
        /// </summary>
    }
    public void Mumble1Audio()
    {
        if (!audioMumble1.isPlaying)
        {
            audioMumble1.Play();
        }
        /// <summary>
        /// Plays the audio of the Mumble2.wav
        /// Checks if the audio is playing so it doesn't overlap
        /// </summary>
    }
    public void Mumble2Audio()
    {
        if (!audioMumble2.isPlaying)
        {
            audioMumble2.Play();
        }

        /// <summary>
        /// Plays the audio of the Mumble3.wav
        /// Checks if the audio is playing so it doesn't overlap
        /// </summary>
    }
    public void Mumble3Audio()
    {
        if (!audioMumble3.isPlaying)
        {
            audioMumble3.Play();
        }

        /// <summary>
        /// Plays the audio of the Mumble4.wav
        /// Checks if the audio is playing so it doesn't overlap
        /// </summary>

    }
    public void Mumble4Audio()
    {
        if (!audioMumble4.isPlaying)
        {
            audioMumble4.Play();
        }

        /// <summary>
        /// Plays the audio of the Blop.ogg
        /// Checks if the audio is playing so it doesn't overlap
        /// </summary>
    }
    public void BlopAudio()
    {
        if (!audioBlop.isPlaying)
        {
            audioBlop.Play();
        }

        /// <summary>
        /// Plays the audio of the Aliencreepy1.ogg
        /// Checks if the audio is playing so it doesn't overlap
        /// </summary>
    }
    public void AlienCreepy1Audio()
    {
        if (!audioAlienCreepy1.isPlaying)
        {
            audioAlienCreepy1.Play();
        }

        /// <summary>
        /// Plays the audio of the Aliencreepy2.ogg
        /// Checks if the audio is playing so it doesn't overlap
        /// </summary>
    }
    public void AlienCreepy2Audio()
    {
        if (!audioAlienCreepy2.isPlaying)
        {
            audioAlienCreepy2.Play();
        }

        /// <summary>
        /// Plays the audio of the Aliencreepy3.ogg
        /// Checks if the audio is playing so it doesn't overlap
        /// </summary>
    }
    public void AlienCreepy3Audio()
    {
        if (!audioAlienCreepy3.isPlaying)
        {
            audioAlienCreepy3.Play();
        }

        /// <summary>
        /// Plays the audio of the ObjectDropWater.ogg
        /// Checks if the audio is playing so it doesn't overlap
        /// </summary>
    }
    public void ObjectDropWaterAudio()
    {
        if (!audioObjectDropWater.isPlaying)
        {
            audioObjectDropWater.Play();
        }

        /// <summary>
        /// Plays the audio of the PaperCrumble.ogg
        /// Checks if the audio is playing so it doesn't overlap
        /// </summary>
    }
    public void PaperCrumbleAudio()
    {
        if (!audioPaperCrumble.isPlaying)
        {
            audioPaperCrumble.Play();
        }

        /// <summary>
        /// Plays the audio of the NormalDoorOpen.wav
        /// Checks if the audio is playing so it doesn't overlap
        /// </summary>
    }
    public void NormalDoorOpenAudio()
    {
        if (!audioNormalDoorOpen.isPlaying)
        {
            audioNormalDoorOpen.Play();

        }
        /// <summary>
        /// Plays the audio of the Mumblegreat.ogg
        /// Checks if the audio is playing so it doesn't overlap
        /// </summary>
    }
    public void MumblegreatAudio()
    {
        if (!audioMumblegreat.isPlaying)
        {
            audioMumblegreat.Play(88000);

        }
    }
    /// <summary>
    /// Plays the audio of the Mumblegreat.ogg
    /// Checks if the audio is playing so it doesn't overlap
    /// </summary>
    public void Mumblegreat2Audio()
    {
        if (!audioMumblegreat.isPlaying)
        {
            audioMumblegreat.Play(132000);

        }

    }
    /// <summary>
    /// Plays the audio of the Mumblegreat.ogg
    /// Checks if the audio is playing os it doesn't overlap
    /// </summary>
    public void Mumblegreat3Audio()
    {
        if (!audioMumblegreat.isPlaying)
        {
            audioMumblegreat.Play();

        }
    }
    /// <summary>
    /// Plays the audio of the Mumble4short.ogg
    /// Checks if the audio is playing os it doesn't overlap
    /// </summary>
    public void Mumble4shortAudio()
    {
        if (!audioMumble4short.isPlaying)
        {
            audioMumble4short.Play();

        }
    }
    /// <summary>
    /// Plays the audio of the Mumble5.ogg
    /// Checks if the audio is playing os it doesn't overlap
    /// </summary>
    public void Mumble5Audio()
    {
        if (!audioMumble5.isPlaying)
        {
            audioMumble5.Play();
        }

    }
    /// <summary>
    /// Plays the audio of the Mumble1short.ogg
    /// Checks if the audio is playing os it doesn't overlap
    /// </summary>
    public void Mumble1shortAudio()
    {
        if (!audioMumble1short.isPlaying)
        {
            audioMumble1short.Play();
        }

    }
    /// <summary>
    /// Plays the audio of the Mumble3short.ogg
    /// Checks if the audio is playing os it doesn't overlap
    /// </summary>
    public void Mumble3shortAudio()
    {
        if (!audioMumble3short.isPlaying)
        {
            audioMumble3short.Play();
        }

    }
}



