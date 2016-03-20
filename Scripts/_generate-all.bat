call "modify-all-files-and-extract-for-website.bat"
call "xdelta-create-patches.bat"
call "create-new-md5s.bat"

rd /S /Q patcher\new\patches
move new\patches patcher\new\patches
