Draw:

    LDA #$20
    STA !PropParam
    LDA #$02
    STA !MaxTilePriorityParam
    
	REP #$21
	LDA !LastPoseIndex,x
	AND #$00FF
    ADC #!PoseID<Name>_Frame0
   	!DXDraw
RTS

