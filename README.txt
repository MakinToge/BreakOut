# BreakNyan
BreakOut - student project

While debuging, use DEBUG_LEVEL_FILES_PATH at line 669 in GamePage.cs

To release, use RELEASE_LEVEL_FILES_PATH and copy LevelScript from BreakoutContent to BreakOut/Bin/x86/Release/Content.

To edit the levels, open the folder "BreakOut/BreakOut/BreakOut/BreakOutContent/LevelScript/", open the ".lvl" file with a text editor.
The first line refers to the percentage to get a particular power up.
Then you can change the values in the grid :
	- 0 = no brick
	- 1 = brick with 1 life
	- 2 = brick with 2 lives
	- 3 = brick with 3 lives
	- 4 = brick with 4 lives
	- 5 = brick with 5 lives
	- 6 = brick with 6 lives
	- 7 = brick with 7 lives