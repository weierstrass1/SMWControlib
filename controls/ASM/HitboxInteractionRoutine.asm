;>Section Hitboxes Interaction
;######################################
;######## Interaction Space ###########
;######################################

InteractMarioSprite:
	LDA !SpriteTweaker167A_DPMKSPIS,x
	AND #$20
	BNE ProcessInteract      
	TXA                       
	EOR !TrueFrameCounter      			
	AND #$01                	
	ORA !SpriteHOffScreenFlag,x 				
	BEQ ProcessInteract       
ReturnNoContact:
	CLC                       
	RTS
ProcessInteract:
	JSL SubHorzPos
	LDA !ScratchF                  
	CLC                       
	ADC #$50                
	CMP #$A0                
	BCS ReturnNoContact       ; No contact, return 
	JSL SubVertPos         
	LDA !ScratchE                   
	CLC                       
	ADC #$60                
	CMP #$C0                
	BCS ReturnNoContact       ; No contact, return 
	LDA $71    ; \ If animation sequence activated... 
	CMP #$01                ;  | 
	BCS ReturnNoContact       ; / ...no contact, return 
	LDA #$00                ; \ Branch if bit 6 of $0D9B set? 
	BIT $0D9B|!addr               ;  | 
	BVS +           ; / 
	LDA $13F9|!addr ; \ If Mario and Sprite not on same side of scenery... 
	EOR !SpriteBehindEscenaryFlag,x ;  |
+
	BNE ReturnNoContact2
	JSL $03B664|!rom				; MarioClipping
	JSR Interaction

	BCC ReturnNoContact2
	LDA !ScratchE
	CMP #$01
	BNE +
	JSR DefaultAction
+
	SEC
	RTS
ReturnNoContact2:
	CLC
	RTS

Interaction:
    STZ !ScratchE
<globalflip>	LDA !GlobalFlip,x
<localflip>    EOR !LocalFlip,x
</localflip>    ASL
	TAY                     ;Y = Flip Adder, used to jump to the frame with the current flip

</globalflip>    LDA !FrameIndex,x
	STA !Scratch4
	STZ !Scratch5

    REP #$20
	LDA !Scratch4
	ASL
<globalflip>	CLC
	ADC HitboxAdder,y
</globalflip>	REP #$10
	TAY

    LDA FrameHitboxesIndexer,y
    TAY
    SEP #$20

-
    LDA FrameHitBoxes,y
    CMP #$FF
    BNE +
    LDA !ScratchE
    BNE ++
	SEP #$10
	LDX !SpriteIndex
    CLC
    RTS
++
	SEP #$10
	LDX !SpriteIndex
    SEC
    RTS
+
    STA !Scratch4
    STZ !Scratch5
    PHY

    REP #$20
    LDA !Scratch4
    ASL
    TAY

    LDA HitboxesStart,y
    TAY
    SEP #$20
<onlydefaultaction2>
	LDA !ScratchE
	AND #$01
	BEQ +

	LDA Hitboxes+5,y
	BNE +

	PLY
    INY
	BRA -

</onlydefaultaction2>+

	STZ !ScratchA
    LDA Hitboxes+1,y
    STA !Scratch4           ;$04 = Low X Offset
    BPL +
    LDA #$FF
    STA !ScratchA           ;$0A = High X offset
+

	STZ !ScratchB
    LDA Hitboxes+2,y
    STA !Scratch5           ;$05 = Low Y Offset
    BPL +
    LDA #$FF
    STA !ScratchB           ;$0B = High Y Offset
+

    LDA Hitboxes+3,y
    STA !Scratch6           ;$06 = Width

    LDA Hitboxes+4,y
    STA !Scratch7           ;$07 = Height

	PHY
	SEP #$10
	LDX !SpriteIndex

	LDA !SpriteXHigh,x
	XBA
	LDA !SpriteXLow,x
	REP #$20
	PHA
	SEP #$20

	LDA !ScratchA
	XBA
	LDA !Scratch4
	REP #$20
	CLC
	ADC $01,s
	PHA
	SEP #$20
	PLA 
	STA !Scratch4
	PLA
	STA !ScratchA
	PLA
	PLA

	LDA !SpriteYHigh,x
	XBA
	LDA !SpriteYLow,x
	REP #$20
	PHA
	SEP #$20

	LDA !ScratchB
	XBA
	LDA !Scratch5
	REP #$20
	CLC
	ADC $01,s
	PHA
	SEP #$20
	PLA 
	STA !Scratch5
	PLA
	STA !ScratchB
	PLA
	PLA

    JSL $03B72B|!rom
<onlydefaultaction1>	REP #$10
	BCS ++
	PLY
	BRA +
++
	PLY

    LDA Hitboxes+5,y
	BNE ++

	LDA !ScratchE
	ORA #$01
    STA !ScratchE
	PLY
    INY
    JMP -

++

    STA !ScratchC
    STZ !ScratchD
    REP #$20
    LDA !ScratchC
    ASL
    TAY

    LDA Actions,y
    STA !ScratchC
    SEP #$30
	LDX #$00
    JSR ($000C|!dp,x)
    REP #$10
+
    PLY
    INY
    JMP -

</onlydefaultaction1>
<globalflip>HitboxAdder:
    dw >intAdd.

</globalflip>FrameHitboxesIndexer:
    dw >fhbsInd.

FrameHitBoxes:
    db >fhbs.

HitboxesStart:
    dw >hbst.

Hitboxes:
    db >hbs.
<onlydefaultaction2>
Actions:
    dw >hbacts.
</onlydefaultaction2>
;This routine will be executed when mario interact with a standar hitbox.
;It will be excecuted if $0E is 1 after execute Interaction routine
DefaultAction:
RTS
    
;>End Hitboxes Interaction Section