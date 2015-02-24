ToVPatcher
==========

Version 1.0

ToVPatcher is a tool to automate the patching process of the Tales of Vesperia (PS3) fan-translation patch.

ToVPatcher requires the .NET Framework 3.5. Please make sure it's installed if you run into issues.


DISCLAIMER
==========

Usage of this patch requires a PS3 console that can run unsigned software, also known as homebrew.

We cannot take responsibility for anything you may choose to do or install in the process of acquiring homebrew functionality for your PS3. Please do not try to modify your PS3 unless you are fully aware of and understand the consequences and possible risks.

As of the release of this patch, there is **no known software exploit to run homebrew on or downgrade a console that has been updated past firmware version 3.55**. **Do not attempt to downgrade consoles on higher firmware versions!**

If you are not confident, please use the online script reference instead.


Requirements
============

To run this patched version of Tales of Vesperia, you will need:
* A PS3 capable of running homebrew
* A copy of the Japanese PS3 game disc of Tales of Vesperia
* A Windows PC to run the patching tool


Installed Data
==============

Please note that the Install option on the title screen does not work properly with this patch. Please delete any existing Installed Game Data for Tales of Vesperia, shown as テイルズ オブ ヴェスペリア （インストールデータ） with a size of 3441 MB in the Game Data Utility section of the XMB.

Other installed data such as DLC costumes or skits should be compatible, but will not be translated.

There are no known save file incompatibilities. Saves created with the original Japanese version can be loaded with the patch and vice-versa.


Instructions
============

* Extract ToVPatcher
  
  Extract the entire contents of the ToVPatcher archive to any, ideally empty, folder on your computer.
  
  
* Get your Tales of Vesperia (PS3) game files
  
  This is most easily done by inserting the game disc into a PS3 that can run homebrew, then either copying them to an USB storage medium with a file manager or transferring them over the local network with an FTP tool.
  
  
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
  - PARAM.SFO (Translates the game's name in the XMB.)
  - TROPHY.TRP (*IMPORTANT: WILL NOT WORK AS INTENDED! SEE NOTE AT THE BOTTOM OF THIS FILE!*)
  
  Copy all of them next to ToVPatcher.exe
  
  
* Acquire ebootMOD
  
  To modify EBOOT.BIN, we need a way to de- and encrypt it. For legal reasons, we cannot provide a tool to do this, so you'll have to find it yourself. Place all files from ebootMOD into the provided empty "ebootmod" folder, so that the executable can be found at ./ebootmod/ebootMOD.exe
  I have tested this with the ebootMOD executable last modified on March 31st, 2011, but other versions may work as well.
  
  
* Run ToVPatcher
  
  Now it's time to actually patch. Run ToVPatcher.exe, and hit the big Patch! button in the window that pops up.
  If you've followed this readme correctly until now, it should automatically find and patch all of the Tales of Vesperia game files you provided.
  This step may take a long time! Due to how Tales of Vesperia stores its files, it's necessary to unpack/decompress, then patch, and then recompress/repack the vast majority of the game files. Please be patient.
  
  If you for whatever reason need to interrupt the patching process, just reopen ToVPatcher later and hit the Patch! button again. It will recognize already patched files and just patch the remaining ones.
  
  Before you proceed to the next step, please make sure all files were patched correctly, as indicated by the green checkmarks next to each file in ToVPatcher.
  
  
* Overwrite the original files with the patched ones
  
  Once ToVPatcher is done, you can find the patched files in ./new/patched/
  Copy them back to your dumped copy of Tales of Vesperia, overwriting the originals.
  
  
* Copy the patched game to your PS3
  
  Similarly to what we did before to acquire the game files from the game disc, you can now copy the modified files back to the PS3 via FTP or USB. Place them so that your preferred backup loader can find and recognize them.
  
  
* Run Tales of Vesperia!
  
  And that's it! You should now be able to boot a patched, English version of Tales of Vesperia!


Credits & Thanks
================

This patch uses parts of the official Xbox 360 localization that were left on the Japanese PS3 game disc. Many thanks to Bandai Namco Games and the original localization team for providing a mostly solid localization for the Xbox 360 version of the game, as well as creating the game in the first place.

Other than that, the following people helped with the creation of this patch:

The Project Lead and initiator of the project was **[Admiral H. Curtiss](http://hyouta.com/)**, also known as Pikachu025. He also did some tool programming and made the online translation guide. The game script was translated by **ruta** and an **Anonymous Chinchilla**, who also ended up doing a lot of the editing. They were later joined by **Fedule** to help out with the editing.

Also featuring **[Kajitani-Eizan](http://www.blade2187.com/)** as the Arcane Arteist.

Special Thanks go to **Tempus** and **AerialX** for hosting [the website](http://talesofvesperia.net/) and providing tools from the cancelled Tales of Graces Wii project, **[throughhim413](http://a0t.co/)** for miscellaneous help here and there, and **gbcft** for helping with the tlzc compression some game files use.


This patcher uses [comptoe](https://github.com/soywiz/talestra/tree/master/compto) by [soywiz](http://www.soywiz.com/) for de- and recompressing some files and [xdelta](http://xdelta.org/) to perform the actual file patching. It also requires ebootMOD to de- and encrypt the game executable. The online translation guide was inspired by the [Vesperia Browser](http://apps.lushu.org/vesperia/).


A Note on TROPHY.TRP
--------------------

TROPHY.TRP is officially signed, so the patched file will not work (and will, in fact, delete all your Tales of Vesperia trophies if you happen to have some!) unless you can find a way to make the console not confirm that TROPHY.TRP is signed correctly. I have not found such a way, but the file is provided anyway in case such a thing becomes possible in the future.
