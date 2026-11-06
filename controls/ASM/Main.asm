
;######################################
;############## Constants ###############
;######################################

;######################################
;############## Defines ###############
;######################################


;######################################
;########### Init Routine #############
;######################################
print "INIT ",pc
	
	LDA.b #HitboxTables>>16
	STA !HitboxTableB,x
	LDA.b #HitboxTables>>8
	STA !HitboxTableH,x
	LDA.b #HitboxTables
	STA !HitboxTableL,x

	LDA.b #!RenderBoxXDistanceOutOfScreen<Name>
	STA !RenderXDistanceOutOfScreen,x
	LDA.b #!RenderBoxYDistanceOutOfScreen<Name>
	STA !RenderYDistanceOutOfScreen,x

	LDA !ExtraBits,x
	AND #$04
	STA !GlobalFlip,x

    LDA #$FF
    STA !Palette,x
    STA !LastPoseIndex,x
    STA !LastPoseHashIndex,x
	STA !LastVersion,x
	STA !PlayerIsAbove,x

    LDA !ExtraByte1,x
	AND #$0F
	STA !Version,x

	LDA #$00
	STA !SpriteActionFlag,x
	STA !LocalFlip,x

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

SpriteCode:
	!RenderBox
	BCS .LongRoutine

.ShortRoutine

	LDA !LockAnimationFlag				    
	BNE ..return			            ;if locked animation return.

	%SubOffScreen()

	JSR AnimationRoutine

..return
RTS
.LongRoutine

	JSR PoseManagement
	BCS +
RTS
+
	JSR Draw

	LDA !LockAnimationFlag				    
	BNE ..return			            ;if locked animation return.

    JSR InteractMarioSprite

	JSR AnimationRoutine
..return
RTS

;######################################
;######## Sub Routine Space ###########
;######################################

;Here you can write routines or tables

;>Section Dynamic
;>End Dynamic Section

;>Section Graphics
;>End Graphics Section

;>Section Animations
;>End Animations Section

;>Section Hitboxes Interaction
;>End Hitboxes Interaction Section