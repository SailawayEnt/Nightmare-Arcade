using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script stores every dialogue conversation in a public Dictionary.*/

public class Dialogue : MonoBehaviour
{

    public Dictionary<string, string[]> dialogue = new Dictionary<string, string[]>();

    void Start()
    {
        //Door
        dialogue.Add("LockedDoorA", new string[] {
            "A large door...",
            "Looks like it has a key hole!"
        });


        dialogue.Add("LockedDoorB", new string[] {
            "Key used!"
        });
        
        // Game Cabinet
        dialogue.Add("GameCabinetA", new string[] {
            "This looks like a fun game...",
            "Looks like it takes 1 credit!"
        });
        
        dialogue.Add("GameCabinetAc", new string[] {
            "Inserts coin...",
            "Loading game..."
        });

        //NPC
        dialogue.Add("CharacterA", new string[] {
            "Hey you!...",
            "Yeah you...",
            "You're new here aren't ya?",
            "Here take this machete, it's dangerous to go alone",
            "For free?",
            "Ha no, you'll have to play on of my arkade games...",
            "...er umm... MegaStar to get tickets and redeem those tickets for prizes.",
            "You can find coins outside in random places",
            "No-one appreciates the value of money ever since they raised minimum wage."
        });

        dialogue.Add("CharacterAChoice1", new string[] {
            "",
            "",
            "",
            "Oh, thank you!",
        });

        dialogue.Add("CharacterAChoice2", new string[] {
            "",
            "",
            "",
            "Can I have one of those prizes?"
        });

        dialogue.Add("CharacterB", new string[] {
            "I realize most those machines are broken",
            "I've already tried tinkering around with them with no luck",
            "Scram kid until you have tickets to redeem.",
            "For free?",
            "Ha no, you'll have to play on of my arkade games...",
            "...er umm... MegaStar to get tickets and redeem those tickets for prizes.",
            "You can find coins outside in random places",
            "No-one appreciates the value of money ever since they raised minimum wage."
        });
        dialogue.Add("CharacterBChoice1", new string[] {
            "",
            "",
            "Oh... ok...",
        });

        dialogue.Add("CharacterBChoice2", new string[] {
            "",
            "",
            "Can I have one of those prizes?"
        });
        dialogue.Add("CharacterC", new string[] {
            "Hey! You won a ticket!",
            "I don't have a lot of prizes now-a-days...",
            "Not since those... Things started invading",
            "Here's an energy drink, Try smashing down with that machete I gave you earlier",
            "That should get you around this dark place better."
        });
    }
}
