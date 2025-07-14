VAR yourVariable = true

VAR battleEntered = false
//-> question
// [] to hide choice when chosen
// + to show a question again when its picked/
// * to NOT show a question again when its picked/
// ~
=== girl1 ===
fdgjlsdfhggflkhflgmjgdfhjmkdf

-> END
=== boy1 ===
HI IM A DUDE
-> END


=== silentDialogue === 
    im giving you the silent treatment...
-> END




=== NPC_NonStatic ===
DO YOU WAN'T TO CARD BATTLE?
    * yes
        LETS BATTLE!!
        ~ battleEntered = true
        -> END
    + no
        -> NPC_NonStatic
->END
=== NPC_Static ===
I CANT MOVE

->END
=== NPC_Shop ===
    ~ temp pickedBanana = false
    /*
    The shopkeeper brings out a banana and a strawberry in either hand.
    */
    The shopkeeper brings out a banana in one hand and a strawberry in the other.
    They stare at me.
    "pick one"
    + banana
        ~ pickedBanana = true
        -> END
    + strawberry
    
        -> END
/*
=== aaa ===
    {pickedBanana: 
    you pick banana
    - else:
    you pick strawberry
}
*/
->END
=== question === 

    pick yes?
    
    + NO
        -> question 
    * yes
        
        ~ yourVariable = false
        -> test


=== test ===
    {yourVariable: 
        This is written if yourVariable is true
        - else:
        Otherwise this is written
    }
-> END