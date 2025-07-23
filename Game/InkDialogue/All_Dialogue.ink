VAR yourVariable = true

VAR combatEntered = false

VAR rivalDefeated = false

VAR myNumber = 1

//TODO REPLACE WASHINGTON and Card battler stuff PLACEHOLDERS

 // testing
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
-> City_Shop_LineWaiter
=== function startBattle()
[ BATTLE START ]
~ combatEntered = true



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
        HAHAHAHAHA!!!
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
HELLO, MY RIVAL
IT'S ME.
HM
DO YOU WAN'T TO CARD BATTLE?
-> Rival_Q1
    
    === Rival_Q1 ===
    * [Who are you?]
        AHAHAHAH
        OH, I'M JUST THE BEST CARD BATTLER IN WASHINGTON.
        ENOUGH, I'M HERE TO BEAT YOU.
        -> Rival_Q1
        
    * [Ok]
        
        ~ startBattle()
        -> END
    + [No]
        ...
        
        
        -> END
    
=== Rival2 ===
HELLO, MY RIVAL.
I'VE MASTERED THE FIVE JUJITSU ARTS.
THIS TIME, I'M SURE TO BEAT YOU!
    * [Ok]
        
        ~ startBattle()
        -> END
    + [No]
        ...
        
        -> END
        
=== Rival_Teacher === 
-> END

=== City_Shop_LineWaiter ===
Oh I'm just waiting in line, but you can skip ahead of me.

+ [But there is nobody in front of you]
    I insist, you go first.
    -> City_Shop_LineWaiter_Q1
+ [Alright, thank you sir]
    ->END
    === City_Shop_LineWaiter_Q1 ===
        
        + [No you go first]
            No, I insist, you go first.
            -> City_Shop_LineWaiter_Q1

        + [Alright, thank you sir]
            ->END
    ->END
//HEY, DID YOU TALK TO THAT WEIRDO POSING OVER THERE?

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



=== function abs(x)
{ x < 0:
      ~ return -1 * x
  - else: 
      ~ return x
}
