#!/bin/bash

unix2dos README.md
rsync -rcv --delete-after README.md SavegameShrinker/About  /rimworld/1.2/Mods/SavegameShrinker/
rsync -rcv --delete-after README.md SavegameShrinker/About  /rimworld/1.3/Mods/SavegameShrinker/
rsync -rcv --delete-after README.md SavegameShrinker/About  /rimworld/1.4/Mods/SavegameShrinker/
rsync -rcv --delete-after README.md SavegameShrinker/About  $HOME/.steam/steam/steamapps/common/RimWorld/Mods/SavegameShrinker/
rsync -rcv /rimworld/1.2/Mods/SavegameShrinker/1.2 $HOME/.steam/steam/steamapps/common/RimWorld/Mods/SavegameShrinker/
rsync -rcv /rimworld/1.3/Mods/SavegameShrinker/1.3 $HOME/.steam/steam/steamapps/common/RimWorld/Mods/SavegameShrinker/
rsync -rcv /rimworld/1.4/Mods/SavegameShrinker/1.4 $HOME/.steam/steam/steamapps/common/RimWorld/Mods/SavegameShrinker/
dos2unix README.md