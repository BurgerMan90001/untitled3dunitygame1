


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
        



=== silentDialogue === 
    silentDialogue...
-> END




=== Someone === 

-> Someone_Q1
    
    === Someone_Q1 ===
    * [Who are you?]
        AHAHAHAH
        
        -> Someone_Q1
        
    * [Ok]
        
        
        -> END
    + [No]
        ...
        
        
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
        


=== function abs(x)
{ x < 0:
      ~ return -1 * x
  - else: 
      ~ return x
}
