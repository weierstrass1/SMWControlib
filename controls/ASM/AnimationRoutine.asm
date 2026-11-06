InitWrapperChangeAnimationFromStart:
	PHB
    PHK
    PLB
	STA !AnimationIndex,x
	JSR ChangeAnimationFromStart
	PLB
RTS

ChangeAnimationFromStart:
    LDA #$00
	STA !AnimationPoseIndex,x

	LDA #$01
	STA $8A

	STZ $01
	LDA !AnimationIndex,x
	STA $00					;$00 = Animation index in 16 bits

	STZ $03
	LDA !AnimationPoseIndex,x
	STA $02					;$02 = Animation Pose index in 16 bits

	STZ $05
	STX $04					;$04 = sprite index in 16 bits

	REP #$30						;A7X/Y of 16 bits
	LDX $04					;X = sprite index in 16 bits

	LDA $00
	ASL
	TAY								;Y = 2*Animation index

	LDA !Scratch2
	CLC
	ADC AnimationIndexer,y
	TAY								;Y = Position of the first Pose of the animation + animation Pose index

	BRA SetUpAnimationPose
	
ChangeAnimation:
	ASL
	STA $00

	STY $02|!dp

	TYA
	LDY $00|!dp
	CLC
	ADC AnimationIndexer,y
	TAY								;Y = Position of the first Pose of the animation + animation Pose index

	BRA SetUpAnimationPose

AnimationRoutine:
	LDA $8A
	BEQ +
RTS
+
    LDA !AnimationTimer,x
    BEQ +
	CMP #$FF
	BNE ++
RTS
++
	DEC A
	STA !AnimationTimer,x
	RTS

+
	STZ $01
	LDA !AnimationIndex,x
	STA $00					;$00 = Animation index in 16 bits

	STZ $03
	LDA !AnimationPoseIndex,x
	STA $02					;$02 = Animation Pose index in 16 bits

	STZ $05
	STX $04					;$04 = sprite index in 16 bits

	REP #$30						;A7X/Y of 16 bits
	LDX $04					;X = sprite index in 16 bits

	LDA $00
	ASL
	TAY								;Y = 2*Animation index

	INC $02					;New Animation Pose Index = Animation Pose Index + 1

	LDA $02			        ;if Animation Pose index < Animation Lenght then Animation Pose index++
	CMP AnimationLenght,y			;else go to the Pose where start the loop.
	BCC +							

	LDA AnimationLastTransition,y
	STA $02					;New Animation Pose Index = first Pose of the loop.

+
	LDA $02
	CLC
	ADC AnimationIndexer,y
	TAY								;Y = Position of the first Pose of the animation + animation Pose index

SetUpAnimationPose:
	SEP #$20						;A of 8 bits

	LDA Poses,y
	STA !PoseIndex,x				;New Pose = Poses[New Animation Pose Index]

	LDA Times,y
	STA !AnimationTimer,x			;Time = Times[New Animation Pose Index]

	LDA $02
	STA !AnimationPoseIndex,x

<localflip>	LDA Flips,y
	STA !LocalFlip,x				;Flip = Flips[New Animation Frame Index]

</localflip>	LDA !Scratch2
	STA !AnimationFrameIndex,x

	SEP #$10						;X/Y of 8 bits
	LDX $04					;X = sprite index in 8 bits
RTS

AnimationLenght:
	dw >anl.

AnimationLastTransition:
	dw >alt.

AnimationIndexer:
	dw >ai.

Frames:
	db >af.

Times:
	db >aft.
<localflip>
Flips:
	db >aff.

</localflip>
