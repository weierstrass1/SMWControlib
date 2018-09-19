;######################################
;############## Defines ###############
;######################################

!FrameIndex = !SpriteMiscTable1
!AnimationTimer = !SpriteDecTimer1
!AnimationIndex = !SpriteMiscTable2
!AnimationFrameIndex = !SpriteMiscTable3
!LocalFlip = !SpriteMiscTable4
!GlobalFlip = !SpriteMiscTable5

;######################################
;########### Init Routine #############
;######################################
print "INIT ",pc
    ;Here you can write your Init Code
    ;This will be excecuted when the sprite is spawned 
RTL

;######################################
;########## Main Routine ##############
;######################################
print "MAIN ",pc
    PHB
    PHK
    PLB
    JSR SpriteCode
    PLB
RTL

;@Routine: SpriteCode
;@Description: This routine excecute the logic of the sprite
;@RoutineLength: Short
SpriteCode:

    JSR GraphicRoutine                  ;Calls the graphic routine and updates sprite graphics

    ;Here you can put code that will be excecuted each frame even if the sprite is locked

    LDA !SpriteStatus,x			        
	CMP #$08                            ;if sprite dead return
	BEQ +			                
	LDA !LockAnimationFlag				    
	BEQ +			                    ;if locked animation return.

RTS

+
    JSL SubOffScreen

    ;Here you can write you sprite code routine
    ;This will be excecuted once per frame excepts when 
    ;the animation is locked or when sprite status is not #$08

    JSR AnimationRoutine                ;Calls animation routine and decides the next frame to draw

.Return
RTS
;@EndRoutine

;######################################
;######## Sub Routine Space ###########
;######################################

;Here you can write routines or tables