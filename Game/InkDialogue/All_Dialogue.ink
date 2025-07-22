VAR yourVariable = true

VAR combatEntered = false

VAR myNumber = 1
/*
	Quick random function for varying choices

	Usage: 

		*	{maybe()} [Ask about apples]
		*	{maybe()} [Ask about oranges]
		*	{maybe()} [Ask about bananas]
		

*/
//-> question
// [] to hide choice when chosen
// + to show a question again when its picked/
// * to NOT show a question again when its picked/
// ~

-> Rival // testing

=== girl1 ===
fdgjlsdfhggflkhflgmjgdfhjmkdf

-> END
=== somebody1 === //MAYBE
//t..tha.thank you NO
-> END // after, *stares at player* 

== PayPhone ===

Hello? Is your refrigerator running?

    * [Yes]
        Well you should go catch it!
        BWAHAHAHAHAHA!!!
        -> END
    * [No]
        Sounds like you need to call a repairman!
        BWAHAHAHAHAHA!!!
        -> END
== girl2 === // i need a baddie for mass apeal

-> END
=== boy1 ===
HI IM A DUDE
-> END


=== silentDialogue === 
    im giving you the silent treatment...
-> END




=== Rival === // generic rival

HELLO IM YOUR RIVAL, PLAYER.
HM
DO YOU WAN'T TO CARD BATTLE?

    * [Why?]
        AHAHAHAH
        -> END
    * [Ok]
        LETS BATTLE!!
        
        ~ combatEntered = true
        ~ myNumber = 5
        -> END
    + [No]
        ...
        ~ combatEntered = false
        ~ myNumber = 3
        -> END







=== function abs(x)
{ x < 0:
      ~ return -1 * x
  - else: 
      ~ return x
}




=== City_Shop_LineWaiter ===
IM WAITING

->END
=== City_Shop ===
    ~ temp pickedBanana = false
    
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