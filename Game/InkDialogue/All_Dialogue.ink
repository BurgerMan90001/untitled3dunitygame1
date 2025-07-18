VAR yourVariable = true

VAR battleEntered = true

VAR myNumber = 5

//-> question
// [] to hide choice when chosen
// + to show a question again when its picked/
// * to NOT show a question again when its picked/
// ~

-> Rival // testing

=== girl1 ===
fdgjlsdfhggflkhflgmjgdfhjmkdf


=== somebody1 === //MAYBE
t..tha.thank you
-> END // after, *stares at player* 

== girl2 === // i need a baddie for mass apeal

-> END
=== boy1 ===
HI IM A DUDE
-> END


=== silentDialogue === 
    im giving you the silent treatment...
-> END




=== Rival === // generic rival
HELLO IM YOUR RIVAL PLAYER.
HM
DO YOU WAN'T TO CARD BATTLE?

    * [yes]
        LETS BATTLE!!
        
        ~ battleEntered = true
        -> END
    + [no]
        ...

        -> END



/*
	Quick random function for varying choices

	Usage: 

		*	{maybe()} [Ask about apples]
		*	{maybe()} [Ask about oranges]
		*	{maybe()} [Ask about bananas]
		

*/



=== function abs(x)
{ x < 0:
      ~ return -1 * x
  - else: 
      ~ return x
}



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