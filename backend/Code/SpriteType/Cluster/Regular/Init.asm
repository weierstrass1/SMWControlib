;######################################
;########### Init Routine #############
;######################################
print "INIT ",pc
<animation>
	JSR InitWrapperChangeAnimationFromStart
</animation>
<regularInteraction>
	LDA #$00
	STA !SpritePlayerIsAbove,x
</regularInteraction>
    ;Here you can write your Init Code
    ;This will be excecuted when the sprite is spawned 
RTL