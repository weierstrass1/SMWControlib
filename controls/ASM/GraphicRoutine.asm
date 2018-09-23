;######################################
;########## Graphics Space ############
;######################################

;This space is for routines used for graphics
;if you don't know enough about asm then
;don't edit them.

;@Routine: AnimationRoutine
;@Description: Decides what will be the next frame.
;@RoutineLength: Short
AnimationRoutine:

    LDA !AnimationTimer,x               ;If timer is not 0, then dont change frame.
    BNE +

    PHX                                 ;Preserve X
    STZ !Scratch0
    LDA !AnimationIndex,x
    STA !Scratch1                       ;$00 = Animation index but 16bits

    STZ !Scratch2
    LDA !AnimationFrameIndex,x
    STA !Scratch3                       ;$02 = Animation Frame Index but 16bits

    STZ !Scratch4
    TXA
    STA !Scratch5                       ;Sprite index but 16bits

    REP #$30                            ;A/X/Y 16bits mode

    LDX !Scratch4

    LDA !Scratch0
    ASL
    TAY                                 ;Fix the index to point to AnimationsIndexer

    LDA AnimationsIndexer,y         
    CLC
    ADC !Scratch2
    TAY                                 ;Y = Index of Frame 0 on the animation + Index of current indexed frame
    SEP #$20                            ;A 8bits mode
    
    LDA NextFrames,y
    STA !AnimationFrameIndex,x          ;Load next index to read on the animation table
    STA !Scratch3                       ;Update the Animation Fram index

    LDA Frames,y
    STA !FrameIndex,x                   ;Load the next frame

    REP #$20                            ;A 16bits mode
    LDA !Scratch0
    ASL 
    TAY                                 ;Fix the index to point to AnimationsIndexer

    LDA AnimationsIndexer,y             
    CLC
    ADC !Scratch2
    TAY                                 ;Y = Index of Frame 0 on the animation + Index of current indexed frame
    SEP #$20                            ;A 8bits mode
    
    LDA Times,y
    STA !AnimationTimer,x               ;Load the number of frames of the next frame

    LDA Flips,y
    STA !LocalFlip,x                    ;Set the flip of the frame

    PLX
+
RTS
;@EndRoutine

;All words that starts with '@' and finish with '.' will be replaced by Dyzen

;@Table: AnimationsIndexer
;@Description: Indicates the index where starts each frame
;@ValuesSize: 16
AnimationsIndexer:
    dw @aind.
;@EndTable

Frames:
    db @frames.

NextFrames:
    db @nframes.

Times:
    db @times.

Flips:
    db @flips.



;@Routine: GraphicRoutine
;@Description: Updates tiles on the oam map
;results will be visible the next frame.
;@RoutineLength: Short
GraphicRoutine:

    JSL GetDrawInfo                     ;Calls GetDrawInfo to get the free slot and the XDisp and YDisp

    STZ !Scratch2                       ;$02 = Free Slot but in 16bits
    STY !Scratch3

    STZ !Scratch4
    LDA !GlobalFlip,x
    EOR !LocalFlip,x
    ASL
    STA !Scratch5                       ;$04 = Global Flip but in 16bits
    
    PHX                                 ;Preserve X
    
    STZ !Scratch6
    LDA !FrameIndex,x
    STA !Scratch7                       ;$06 = Frame Index but in 16bits

    REP #$30                            ;A/X/Y 16bits mode
    LDY !Scratch4                       ;Y = Global Flip
    LDA !Scratch6
    ASL
    TAX                                 ;X = Frame Index

    LDA FramesLength,x
    STA !Scratch8

    LDA FramesEndPosition,x
    CLC
    ADC FramesFlippers,y
    STA !Scratch4                       ;$04 = End Position + A value used to select a frame version that is flipped

    LDA FramesStartPosition,x           
    CLC
    ADC FramesFlippers,y
    TAX                                 ;X = Start Position
    LDY !Scratch2                       ;Y = Free Slot
    SEP #$20                            ;A 8bits mode
-
    LDA Tiles,x
    STA !TileCode,y                     ;Set the Tile code of the tile Y

    LDA Properties,x
    STA !TileProperty,y                 ;Set the Tile property of the tile Y

    LDA !Scratch0
	CLC
	ADC XDisplacements,x
	STA !TileXPosition,y                ;Set the Tile x pos of the tile Y

    LDA !Scratch1
	CLC
	ADC YDisplacements,x
	STA !TileYPosition,y                ;Set the Tile y pos of the tile Y

    PHY                                 
    TYA
    LSR
    LSR
    TAY                                 ;Y = Y/4 because size directions are not continuous to map 200 and 300
    LDA Sizes,x
    STA !TileSizeBuffer,y               ;Set the Tile size of the tile Y
    PLY

    INY
    INY
    INY
    INY                                 ;Next OAM Slot
    CPY #$00FD
    BCS .return                         ;Y can't be more than #$00FD

    DEX
    DEX
    BMI .return
    CPX !Scratch4                       ;if X < start position or is negative then return
    BCS -                               ;else loop

.return
    SEP #$10
    PLX                                 ;Restore X
    
    LDY #$04                            ;Allows mode of 8 and 16 bits
    LDA !Scratch8                       ;Load the number of tiles used by the frame
    JSL OAMFinish                       ;This insert the new tiles into the oam, 
                                        ;A = #$00 => only tiles of 8x8, A = #$02 = only tiles of 16x16, A = #$04 = tiles of 8x8 or 16x16
                                        ;if you select A = #$04 then you must put the sizes of the tiles in !TileSize
RTS
;@EndRoutine

;All words that starts with '@' and finish with '.' will be replaced by Dyzen

;@Table: FramesLengths
;@Description: How many tiles use each frame.
;@ValuesSize: 16
FramesLength:
    dw @fls.
;@EndTable

;@Table: FramesFlippers
;@Description: Values used to add values to FramesStartPosition and FramesEndPosition
;To use a flipped version of the frames.
;@ValuesSize: 16
FramesFlippers:
    dw @ffps.
;@EndTable

;@Table: FramesStartPosition
;@Description: Indicates the index where starts each frame
;@ValuesSize: 16
FramesStartPosition:
    dw @fsp.
;@EndTable

;@Table: FramesEndPosition
;@Description: Indicates the index where end each frame
;@ValuesSize: 16
FramesEndPosition:
    dw @fep.
;@EndTable

;@Table: Tiles
;@Description: Tiles codes of each tile of each frame
;@ValuesSize: 8
Tiles:
    db @tiles.
;@EndTable

;@Table: Properties
;@Description: Properties of each tile of each frame
;@ValuesSize: 8
Properties:
    db @props.
;@EndTable

;@Table: XDisplacements
;@Description: X Displacement of each tile of each frame
;@ValuesSize: 8
XDisplacements:
    db @xdisps.
;@EndTable

;@Table: YDisplacements
;@Description: Y Displacement of each tile of each frame
;@ValuesSize: 8
YDisplacements:
    db @ydisps.
;@EndTable

;@Table: Sizes.
;@Description: size of each tile of each frame
;@ValuesSize: 8
Sizes:
    db @sizes.
;@EndTable



;@Routine: OAMFinish
;@Description: When a graphic routine is done, it set the sizes of the tiles
;Also fix problems with screen boundaries.
;@RoutineLength: Long
;######################################
;######### OAM Finish Routine #########
;######################################
OAMFinish:
    PHB                                 ; Wrapper
    PHK                       
    PLB                       
    JSR OAMFinishWriter    
    PLB                       
    RTL                                 ; Return 

OAMFinishWriter:
    STY !ScratchB                       ;>$0B = tile size
    STA !Scratch8                       ;>$08 = Numb of OAM slot tiles (include 0)

    LDY !SpriteIndexOAM,x               ; Y = Index into sprite OAM 

    LDA !SpriteYHigh,x                  ;\$01 = sprite Y pos high byte
    STA !Scratch1                       ;/
    LDA !SpriteYLow,x                   ;\$00 = sprite Y pos low byte
    STA !Scratch0                       ;/
    SEC                                 ;\$06 = Y position on-screen
    SBC !Layer1Y                        ;|
    STA !Scratch6                       ;/

    LDA !SpriteXHigh,x                  ;\$03 = sprite X pos high byte
    STA !Scratch3                       ;/
    LDA !SpriteXLow,x                   ;\$02 = sprite X pos low byte
    STA !Scratch2                       ;/
    SEC                                 ;\$07 = sprite X pos on-screen
    SBC !Layer1X                        ;|
    STA !Scratch7                       ;/
-
    TYA                                 ;\Transfer RAM_SprOAMIndex into A,
    LSR                                 ;|divide by 4 (divide by 2 twice),
    LSR                                 ;|transfer to X for OAM size ($0420)
    TAX                                 ;/(4 byte -> 1 byte index conversion)
    LDA !ScratchB                             ;
    CMP #$04
    BCC +

    PHY
    TAY
    LDA !TileSizeBuffer,y
    PLY
+
    STA !TileSize460,x                  ;>Set OAM tile size.
++
    LDX #$00                            ;>X index = 0
    LDA !TileXPosition,y                ;>Move tile X pos
    SEC                                 ;\Subtract by X pos by distance between left edge of scrn
    SBC !Scratch7                       ;/and sprite
    BPL +                               ;>If positive (tile not past left edge of screen), don't wrap the tile (skip DEX)
    DEX                                 ;>X = #$FF
+
    CLC                                 ;\Add by x position relative to level
    ADC !Scratch2                       ;/
    STA !Scratch4                       ;>$04 = tile x pos relative to screen?
    TXA                                 ;>Transfer X (#$00 or #$FF) to A
    ADC !Scratch3                       ;>Add by sprite X pos high byte
    STA !Scratch5                       ;>$05 = X pos relative to screen high byte? (ScreenXpos + XPosOnScrn = XPosInLvl)
    JSR OffScreenX                      ;>Offscreen X position check
    BCC +                               ;>If on-screen, skip conversion below
    TYA                                 ;\4 byte -> 1 byte index conversion
    LSR                                 ;|and transfer to X.
    LSR                                 ;|
    TAX                                 ;/
    LDA !TileSize460,x                  ;\Set bit 0 of tile size (#%00000001)
    ORA #$01                            ;|
    STA !TileSize460,x                  ;/
+
    LDX #$00                            ;>X = #$00
    LDA !TileYPosition,y                ;\Tile Y position subtract by Y pos on-screen
    SEC                                 ;|(Tile Y pos on-screen, I think)
    SBC !Scratch6                       ;/
    BPL +                               ;>If positive (not past the top edge of screen), skip
    DEX                                 ;>X = #$FF

+
    CLC                                 ;\Add by sprite Y pos low byte
    ADC !Scratch0                       ;/
    STA !Scratch9                       ;>$09 = tile y pos relative to screen?
    TXA                                 ;>Transfer X (#$00 or #$FF)to A
    ADC !Scratch1                       ;\Add by Y pos high byte
    STA !ScratchA                       ;/
    JSR OffScreenY                      ;>Offscreen Y position check
    BCC +                               ;>If on-screen, skip "clearing" a tile
    LDA #$F0                            ;\If off-screen, hide tile (Y = #$F0, the bottom of screen) to
    STA !TileYPosition,y                ;/not show wrapped gfx. This position is used to detect if that slot is free.
+
    INY                                 ;\Next slot (remember that OAM table, each data is 4 bytes long:
    INY                                 ;|xxxxxxxx yyyyyyyy tttttttt yxppccct)
    INY                                 ;|
    INY                                 ;/
    DEC !Scratch8                             ;>Decrement number of OAM slots (loop counter) by 1
    BPL -                               ;>Loop until Y index is negative (DEC $xx doesn't effect A)
    LDX !SpriteIndex                    ; X = Sprite index 
RTS                                     ; Return 

OffScreenX:
    REP #$20                            ; Accum (16 bit) 
    LDA !Scratch4                       ;\This is a tile off-screen code check, Output:
    SEC                                 ;|carry set if off screen horizontally.
    SBC !Layer1X                        ;|(see CODE_01C9BF for vertical check)
    CMP #$0100                          ;/
    SEP #$20                            ; Accum (8 bit) 
RTS                                     ; Return 

OffScreenY:
    REP #$20                            ; Accum (16 bit) 
    LDA !Scratch9                       ;\Similar to CODE_01B844, but for Y position on-screen. Output:
    PHA                                 ;|Carry is set if off screen vertically.
    CLC                                 ;|
    ADC #$0010                          ;|
    STA !Scratch9                       ;|
    SEC                                 ;|
    SBC !Layer1Y                        ;|
    CMP #$0100                          ;|
    PLA                                 ;|
    STA !Scratch9                       ;/
    SEP #$20                            ; Accum (8 bit) 
RTS                                     ; Return 
;@EndRoutine