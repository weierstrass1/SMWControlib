;>Section Dynamic
;######################################
;########## Animation Space ###########
;######################################
ResourceOffset:
    dw >resoff.

ResourceSize:
    dw >ressz.

DynamicRoutine:
    >difrows.
	%EasyNormalSpriteDynamicRoutineFixedGFX("!FrameIndex,x", "!LastFrameIndex,x", !GFX00, "#ResourceOffset", "#ResourceSize", >lns.)
RTS
;>End Dynamic Section