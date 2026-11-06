InteractMarioSprite:

	LDA.b #HitboxTables
	STA $8A
	LDA.b #HitboxTables>>8
	STA $8B
	LDA.b #HitboxTables>>16
	STA $8C

	LDA !LastFlip,x
	TAY                     ;Y = Flip Adder, used to jump to the Pose with the current flip

	STZ $4A
    LDA !LastPoseIndex,x		;A 16 bits Pose index
	STA $49

	%DyzenPlayerNormalSpriteInteraction()
	BCC +

	JSR DefaultAction
+
RTS

HitboxTables:
	dw HitboxAdder
	dw PoseHitboxesIndexer
	dw PoseHitBoxes
	dw HitboxType
	dw HitboxXOffset
	dw HitboxYOffset
	dw HitboxWidth
	dw HitboxHeight
	dw HitboxAction1
	dw HitboxAction2
	dw Actions

HitboxAdder:
    dw >intAdd.

PoseHitboxesIndexer:
    dw >fhbsInd.

PoseHitBoxes:
    db >fhbs.

Hitboxes:
db >hbs.

Actions:
    dw >hbacts.

DefaultAction:
RTS