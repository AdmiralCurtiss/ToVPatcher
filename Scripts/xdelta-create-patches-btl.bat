mkdir battlefilenames
mkdir battletext
mkdir new\patches
mkdir new\patches\btl

HyoutaTools tovfps4e btl.svo
HyoutaTools tovfps4e btl.svo.ext\BTL_PACK.DAT
HyoutaTools tovfps4e btl.svo.ext\BTL_PACK.DAT.ext\0003 btl.svo.ext\BTL_PACK.DAT.ext\0003.ext -nometa
HyoutaTools tovfps4e new\btl.svo
HyoutaTools tovfps4e new\btl.svo.ext\BTL_PACK.DAT
HyoutaTools tovfps4e new\btl.svo.ext\BTL_PACK.DAT.ext\0003 new\btl.svo.ext\BTL_PACK.DAT.ext\0003.ext -nometa

copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0154 battlefilenames\154
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0156 battlefilenames\156
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0157 battlefilenames\157
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0159 battlefilenames\159
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0160 battlefilenames\160
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0161 battlefilenames\161
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0165 battlefilenames\165
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0166 battlefilenames\166
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0167 battlefilenames\167
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0170 battlefilenames\170
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0172 battlefilenames\172
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0173 battlefilenames\173
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0176 battlefilenames\176
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0177 battlefilenames\177
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0178 battlefilenames\178
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0179 battlefilenames\179
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0180 battlefilenames\180
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0182 battlefilenames\182
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0183 battlefilenames\183
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0184 battlefilenames\184
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0185 battlefilenames\185
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0190 battlefilenames\190
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0191 battlefilenames\191
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0192 battlefilenames\192
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0193 battlefilenames\193
copy btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0196 battlefilenames\196

FOR %%G IN (battlefilenames\*) DO (
HyoutaTools tlzc -d btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0%%~nG btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\%%~nG.ex
HyoutaTools tlzc -d new\btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\0%%~nG new\btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\%%~nG.ex

xdelta -D -9 -S djw -A -B 2000000000 -e -vfs "btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\%%~nG.ex" "new\btl.svo.ext\BTL_PACK.DAT.ext\0003.ext\%%~nG.ex" "new\patches\btl\%%~nG.xdelta3" 
)


rd /S /Q battlefilenames
rd /S /Q battletext
rd /S /Q btl.svo.ext
rd /S /Q new\btl.svo.ext

