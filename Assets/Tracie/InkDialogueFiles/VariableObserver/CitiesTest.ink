INCLUDE globals.ink 

// -> citiesmain refactored into conditional 
{city_name == " " : -> citiesmain | -> alreadychosen } 

=== citiesmain === 
what is your favorite city? 
+[Berlin]
Berlin, Germany 
-> playerselected (" Berlin is for art walks ")

+[Amsterdam] 
Amsterdam, Netherlands
-> playerselected ("Amsterdam is for waterfront cycling") 

+[Tokyo] 
Tokyo,Japan
-> playerselected ("Japan is for train spotting ")





=== playerselected(choices) ===
~city_name = choices 
You've said {choices} !
-> END

=== alreadychosen ===
You already chose {city_name} ! 
->END

