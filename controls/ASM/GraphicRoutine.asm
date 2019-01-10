;>Section Graphics
;######################################
;########## Graphics Space ############
;######################################

;This space is for routines used for graphics
;if you don't know enough about asm then
;don't edit them.

;>Routine: GraphicRoutine
;>Description: Updates tiles on the oam map
;results will be visible the next frame.
;>RoutineLength: Short
GraphicRoutine:

    JSL GetDrawInfo                     ;Calls GetDrawInfo to get the free slot and the XDisp and YDisp

    STZ !Scratch3                       ;$02 = Free Slot but in 16bits
    STY !Scratch2

<globalflip>
    STZ !Scratch5
    LDA !GlobalFlip,x   
<localflip>    EOR !LocalFlip,x
</localflip>    STA !ScratchF
    ASL
    STA !Scratch4                       ;$04 = Global Flip but in 16bits
    LDA !ScratchF
    CLC
    ROR A
    ROR A 
    ROR A
    STA !ScratchF
</globalflip>
    PHX                                 ;Preserve X
    
    STZ !Scratch7
    LDA !FrameIndex,x
    STA !Scratch6                       ;$06 = Frame Index but in 16bits

    REP #$30                            ;A/X/Y 16bits mode
<globalflip>    LDY !Scratch4                       ;Y = Global Flip
</globalflip>    LDA !Scratch6
    ASL
<globalflip>	CLC
    ADC FramesFlippers,y
</globalflip>    TAX                                 ;X = Frame Index

<samelength1>    LDA FramesLength,x
    CMP #$FFFF
    BNE +
    SEP #$30
    PLX
    RTS
+
</samelength1>    STA !Scratch8

<oneframe1>    LDA FramesEndPosition,x
</oneframe1>    STA !Scratch4                       ;$04 = End Position + A value used to select a frame version that is flipped

<oneframe2>    LDA FramesStartPosition,x           
</oneframe2>    TAX                                 ;X = Start Position
    SEP #$20                            ;A 8bits mode
    LDY !Scratch2                       ;Y = Free Slot
    CPY #$00FD
    BCS .return                         ;Y can't be more than #$00FD
-
<sametile1>    LDA Tiles,x
</sametile1>    STA !TileCode,y                     ;Set the Tile code of the tile Y

<sameprop1>    LDA Properties,x
</sameprop1><globalflip>    EOR !ScratchF
</globalflip>    STA !TileProperty,y                 ;Set the Tile property of the tile Y

    LDA !Scratch0
	CLC
<samexdisp1>	ADC XDisplacements,x
</samexdisp1>	STA !TileXPosition,y                ;Set the Tile x pos of the tile Y

    LDA !Scratch1
	CLC
<sameydisp1>	ADC YDisplacements,x
</sameydisp1>	STA !TileYPosition,y                ;Set the Tile y pos of the tile Y
<samesize1>
    PHY
	REP #$20                                 
    TYA
    LSR
    LSR
    TAY                                 ;Y = Y/4 because size directions are not continuous to map 200 and 300
	SEP #$20
    LDA Sizes,x
    STA !TileSize460,y                  ;Set the Tile size of the tile Y
    PLY
</samesize1>
    INY
    INY
    INY
    INY                                 ;Next OAM Slot
    CPY #$00FD
    BCS .return                         ;Y can't be more than #$00FD

    DEX
    BMI .return
    CPX !Scratch4                       ;if X < start position or is negative then return
    BCS -                               ;else loop

.return
    SEP #$10
    PLX                                 ;Restore X
    
<samesize2>    LDY #$FF                            ;Allows mode of 8 or 16 bits
</samesize2>    LDA !Scratch8                       ;Load the number of tiles used by the frame
    JSL $01B7B3|!rom                  		;This insert the new tiles into the oam, 
                                        ;A = #$00 => only tiles of 8x8, A = #$02 = only tiles of 16x16, A = #$04 = tiles of 8x8 or 16x16
                                        ;if you select A = #$04 then you must put the sizes of the tiles in !TileSize
RTS
;>EndRoutine

;All words that starts with '@' and finish with '.' will be replaced by Dyzen
<samelength2>
;>Table: FramesLengths
;>Description: How many tiles use each frame.
;>ValuesSize: 16
FramesLength:
    dw >fls.
;>EndTable

</samelength2><globalflip>
;>Table: FramesFlippers
;>Description: Values used to add values to FramesStartPosition and FramesEndPosition
;To use a flipped version of the frames.
;>ValuesSize: 16
FramesFlippers:
    dw >ffps.
;>EndTable

</globalflip><oneframe3>
;>Table: FramesStartPosition
;>Description: Indicates the index where starts each frame
;>ValuesSize: 16
FramesStartPosition:
    dw >fsp.
;>EndTable

;>Table: FramesEndPosition
;>Description: Indicates the index where end each frame
;>ValuesSize: 16
FramesEndPosition:
    dw >fep.
;>EndTable

</oneframe3><sametile2>
;>Table: Tiles
;>Description: Tiles codes of each tile of each frame
;>ValuesSize: 8
Tiles:
    db >tiles.
;>EndTable

</sametile2><sameprop2>
;>Table: Properties
;>Description: Properties of each tile of each frame
;>ValuesSize: 8
Properties:
    db >props.
;>EndTable
</sameprop2><samexdisp2>;>Table: XDisplacements
;>Description: X Displacement of each tile of each frame
;>ValuesSize: 8
XDisplacements:
    db >xdisps.
;>EndTable
</samexdisp2><sameydisp2>;>Table: YDisplacements
;>Description: Y Displacement of each tile of each frame
;>ValuesSize: 8
YDisplacements:
    db >ydisps.
;>EndTable
</sameydisp2><samesize3>;>Table: Sizes.
;>Description: size of each tile of each frame
;>ValuesSize: 8
Sizes:
    db >sizes.
;>EndTable

</samesize3>;>End Graphics Section