;######################################
;############## Defines ###############
;######################################

!FrameIndex = !SpriteMiscTable1
!AnimationTimer = !SpriteMiscTable7
!AnimationIndex = !SpriteMiscTable2
!AnimationFrameIndex = !SpriteMiscTable3
!LocalFlip = !SpriteMiscTable4
!GlobalFlip = !SpriteMiscTable5
!LastFrameIndex = !SpriteMiscTable6

;######################################
;########### Init Routine #############
;######################################
print "INIT ",pc
	LDA #$00
	STA !GlobalFlip,x
	JSL InitWrapperChangeAnimationFromStart

	LDA #$FF
	STA !LastFrameIndex,x

	%CheckSlotNormalSprite(>ResTiles., $00)
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

;>Routine: SpriteCode
;>Description: This routine excecute the logic of the sprite
;>RoutineLength: Short
Return:
RTS
SpriteCode:
	JSR DynamicRoutine

	LDA DZ_DS_Loc_US_Normal,x
	TAX

	LDA DZ_DS_Loc_IsValid,x
	BNE +
	LDX !SpriteIndex
RTS
+
	LDX !SpriteIndex 
    JSR GraphicRoutine                  ;Calls the graphic routine and updates sprite graphics

    ;Here you can put code that will be excecuted each frame even if the sprite is locked

    LDA !SpriteStatus,x			        
	CMP #$08                            ;if sprite dead return
	BNE Return	

	LDA !LockAnimationFlag				    
	BNE Return			                    ;if locked animation return.

    %SubOffScreen()

    JSR InteractMarioSprite
    ;After this routine, if the sprite interact with mario, Carry is Set.

    ;Here you can write your sprite code routine
    ;This will be excecuted once per frame excepts when 
    ;the animation is locked or when sprite status is not #$08

    JSR AnimationRoutine                ;Calls animation routine and decides the next frame to draw
    
    RTS

;>EndRoutine

;######################################
;######## Sub Routine Space ###########
;######################################

;Here you can write routines or tables

;>Section Dynamic
;>End Dynamic Section

;Don't Delete or write another >Section Graphics or >End Section
;All code between >Section Graphics and >End Graphics Section will be changed by Dyzen : Sprite Maker
;>Section Graphics
;>End Graphics Section

;Don't Delete or write another >Section Animation or >End Section
;All code between >Section Animations and >End Animations Section will be changed by Dyzen : Sprite Maker
;>Section Animations
;>End Animations Section

;Don't Delete or write another >Section Hitbox Interaction or >End Section
;All code between >Section Hitboxes Interaction and >End Hitboxes Interaction Section will be changed by Dyzen : Sprite Maker
;>Section Hitboxes Interaction
;>End Hitboxes Interaction Section