;>Section Hitboxes Interaction
;######################################
;######## Interaction Space ###########
;######################################

InteractMarioSprite:
	LDA !SpriteTweaker167A_DPMKSPIS,x
	AND #$20
	BNE ProcessInteract      
	TXA                       
	EOR $13      			
	AND #$01                	
	ORA !SpriteHOffScreenFlag,x 				
	BEQ ProcessInteract       
ReturnNoContact:
	CLC                       
	RTS
ProcessInteract:
	JSR SubHorzPos
	LDA !ScratchF                  
	CLC                       
	ADC #$50                
	CMP #$A0                
	BCS ReturnNoContact       ; No contact, return 
	JSR SubVertPos         
	LDA !ScratchE                   
	CLC                       
	ADC #$60                
	CMP #$C0                
	BCS ReturnNoContact       ; No contact, return 
	LDA $71    ; \ If animation sequence activated... 
	CMP #$01                ;  | 
	BCS ReturnNoContact       ; / ...no contact, return 
	LDA #$00                ; \ Branch if bit 6 of $0D9B set? 
	BIT $0D9B               ;  | 
	BVS +           ; / 
	LDA $13F9 ; \ If Mario and Sprite not on same side of scenery... 
	EOR !SpriteBehindEscenaryFlag,x ;  |
+
	BNE ReturnNoContact2
	JSL $03B664				; MarioClipping
	JSR Interaction

	BCC ReturnNoContact2
	LDA !ScratchE
	CMP #$01
	BNE +
	JSL $00F5B7
+
	SEC
	RTS
ReturnNoContact2:
	CLC
	RTS

	
SubHorzPos:
	LDY #$00                
	LDA $D1                   
	SEC                       
	SBC !SpriteXLow,x       
	STA !ScratchF                   
	LDA $D2                   
	SBC !SpriteXHigh,x     
	BPL +        
	INY                       
+
	RTS
	
SubVertPos:
	LDY #$00                
	LDA $D3                   
	SEC                       
	SBC !SpriteYLow,x       
	STA !ScratchE                  
	LDA $D4                   
	SBC !SpriteYHigh,x     
	BPL +        
	INY                       
+
	RTS

Interaction:
    STZ !ScratchE
    LDA !GlobalFlip,x
    EOR !LocalFlip,x
    TAY                     ;Y = Flip Adder, used to jump to the frame with the current flip

    LDA !FrameIndex,x
    CLC
    ADC HitboxAdder,y
    ASL
    TAY                     ;

    REP #$20
    LDA FrameHitboxesIndexer,y
    REP #$10
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

	LDA !ScratchE
	AND #$01
	BEQ +

	LDA Hitboxes+5,y
	CMP #$FF
	BNE +

	PLY
    INY
	BRA -

+

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

    JSL $03B72B
	BCS ++
	REP #$10
	PLY
	BRA +
++
	REP #$10
	PLY

    LDA Hitboxes+5,y
	CMP #$FF
	BNE ++

	LDA !ScratchE
	ORA #$01
    STA !ScratchE
	PLY
    INY
    JMP -

++

    STA !Scratch4
    STZ !Scratch5
    REP #$20
    LDA !Scratch4
    ASL
    TAY

    LDA Actions,y
    STA !Scratch4
    SEP #$30
	LDX #$00
    JSR ($0004|!dp,x)
    REP #$10
+
    PLY
    INY
    JMP -

HitboxAdder:
    db >intAdd.

FrameHitboxesIndexer:
    dw >fhbsInd.

FrameHitBoxes:
    db >fhbs.

HitboxesStart:
    dw >hbst.

Hitboxes:
    db >hbs.

Actions:
    dw >hbacts.
    
;>End Hitboxes Interaction Section