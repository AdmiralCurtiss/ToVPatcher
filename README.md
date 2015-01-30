ToVPatcher
==========

Version 1.0

ToVPatcher is a tool to automate the patching process of the Tales of Vesperia (PS3) fan-translation patch.

ToVPatcher requires the .NET Framework 3.5. Please make sure it's installed if you run into issues.


Requirements
============

To run this patched version of Tales of Vesperia, you will need:
* A PS3 capable of running homebrew
* A copy of the Japanese PS3 game disc of Tales of Vesperia
* A Windows PC to run the patching tool


Instructions
============

* Extract ToVPatcher
  
  Extract the entire contents of the ToVPatcher archive to any, ideally empty, folder on your computer.
  
  
* Get your Tales of Vesperia (PS3) game files
  
  This is most easily done by inserting the game disc into a PS3 that can run homebrew, then either copying them to an USB storage medium or transferring them over the local network with an FTP tool.
  
  Keep in mind that we will later run a modified version of Tales of Vesperia using your preferred backup loader, so feel free to prepare an unpatched version on your PS3's HDD in the meantime, so that we can later just replace the files that had to be changed.
  
  
* Copy the files to patch to ToVPatcher's folder
  
  The following files need to be patched:
  - EBOOT.BIN
  - string.svo
  - scenario.dat
  - btl.svo
  - chat.svo
  - UI.svo
  - effect.svo
  - chara.svo
  
  Optionally you can also patch:
  - PARAM.SFO
  - TROPHY.TRP (*SEE NOTE AT THE BOTTOM OF THIS FILE!*)
  
  Copy all of them next to ToVPatcher.exe
  
  
* Acquire ebootMOD
  
  To modify EBOOT.BIN, we need a way to de- and encrypt it. For legal reasons, we cannot provide a tool to do this, so you'll have to find it yourself. Place all files from ebootMOD into the provided empty "ebootmod" folder, so that the executable can be found at ./ebootmod/ebootMOD.exe
  I have tested this with the ebootMOD executable last modified on March 31st, 2011, but other versions may work as well.
  
  
* Run ToVPatcher
  
  Now it's time to actually patch. Run ToVPatcher.exe, and hit the big Patch! button in the window that pops up.
  If you've followed this readme correctly until now, it should automatically find and patch all of the Tales of Vesperia game files you provided.
  This step may take a long time! Due to how Tales of Vesperia stores its files, it's necessary to unpack/decompress, then patch, and then recompress/repack the vast majority of the game files. Please be patient.
  
  Before you proceed to the next step, please make sure all files were patched correctly, as indicated by the green checkmarks next to each file in ToVPatcher.
  
  
* Copy the patched files to your PS3
  
  Once ToVPatcher is done, you can find the patched files in ./new/patched/
  Copy them back to your dumped copy of Tales of Vesperia, overwriting the originals, again via FTP or USB depending on what you prefer.
  
  
* Run Tales of Vesperia!
  
  And that's it! You should now be able to boot a patched, English version of Tales of Vesperia using your preferred backup loader.
  


A Note on TROPHY.TRP
--------------------

TROPHY.TRP is officially signed, so the patched file will not work (and will, in fact, delete all your Tales of Vesperia trophies if you happen to have some!) unless you can find a way to make the console not confirm that TROPHY.TRP is signed correctly. I have not found such a way, but the file is provided anyway in case such a thing becomes possible in the future.
