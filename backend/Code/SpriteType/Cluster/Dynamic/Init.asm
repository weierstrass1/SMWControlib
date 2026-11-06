;######################################
;########### Init Routine #############
;######################################
print "INIT ",pc
>@anim.

	LDA #$FF
	STA !LastFrameIndex,x

>@regularInteraction.

	%CheckSlotNormalSprite(>@#tiles., $00)
    ;Here you can write your Init Code
    ;This will be excecuted when the sprite is spawned 
RTL