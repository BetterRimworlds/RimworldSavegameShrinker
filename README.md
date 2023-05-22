# Rimworld Savegame Shrinker

This mod cleans up unnecessary data from Savegames, leading to much faster play on games after 10+ years.

![screenshot](https://github.com/BetterRimworlds/RimworldSavegameShrinker/assets/1125541/eda49c6b-91e3-416f-9414-f57c4183de97)

It typically reduces file sizes to 10-20 MB, largely dependent on how many pawns and items you have
in your bases.

Backups are made before each shrinking operation, so you can rest secure.

## What's Removed? ##

With the default settings, this mod removes the following data:

### Dead Pawns ###

The full biological and biographical data of every killed pawn remains in the game indefinitely.

That means, if you've had 10 chicken explosions in the last 10 years, you could have 20,000 chickens
in your savegame, making it super slow. Case in point, in the savegame I used to develop this mod,
there were 21,527 chicken corpses in the savegame.

This is the main cause of data savings. It removed over 1 million lines from 20 year savegame.

### Historical Archives ###

#### Letters ####

You know those boxes on the right side of the screen that you click to open and then close? 

Well, Rimworld doesn't actually remove them. It saves them forever. Thousands of these messages can
clog up your savegame after 5 or 10 years, and each time a new event occurs, it causes lag.

This mod removes every closed Letter. There are no known downsides.

#### Messages ####

The white messages that flash for a few seconds on the top left? Yep, Rimworld saves all of those, too,
in the savegame. Over 10-20 years, you can end up with tens of thousands of these mostly-worthless messages
using up more than a megabyte or two of space, especially if you have a farm with lots of animals getting
frostbite, heatstroke, or attacked.

This mod removes every single message that is over 24 hours old. A downside is that these useless messages
won't appear as descriptions of Legendary Art.

#### Archived Dialogs ####

Every time a colonist talks to someone, apparently those are saved in the savegame as well. Who knew?!

If you have lots of colonists, this can add up *fast*. In fact, it's one of the main causes of lag for 
colonies with more than 50 colonists.

This mod removes every dialog recording older than 24 hours.

### Play Log ###

Removes the logs of "chitchatting", "playing games", that sort of stuff.

### Battle Log ###

Removes the play-by-play of the latest battles.

## All Filth ##

This removes all filth from all maps. It can dramatically increase runtime after something like
a huge forest fire. It usually cuts down 100-200 kb with no cost or risk.

### Automatically-accepted Quests ###

You know those quests that get automatically accepted, like Treasure spots, refugee, etc.?
This removes every one of those that you didn't actually go to. If you visit the spot and either
succeed or fail, it keeps the quest in your history.

### Unaccepted Quests ###

**Not removed by default:** This will remove every unaccepted + expired quest from youur history log.

If you accepted a quest and either succeeded or failed, it keeps that quest in your history.

### Tales ###

**Not removed by default:** Tales contain logs of things colonists do, like completing a crafting project,
played a game, killed an animal (not via slaughter), when a pawn is Downed, and lots more.

It's not removed by default, because I couldn't ascertain why RimWorld keeps them around. In my experiments,
I didn't experience any ill effects with this change.

## Mothballed Pawns ##

**Not removed by default:** These pawns aren't actively "alive" but are usually nequired for side-quests
and other game purposes.

Running this will quite likely not save much space (since Rimworld v1.4 adequitely keeps them under
control). This option is kept for backwards-compatibility with RuntimeGC, and because it provides
marginal space savings and speed increases for Rimworld v1.2 and v1.3.


## Change Log

**v1.0.0: 2023-05-21**

* **[2023-05-21 04:47:11 CDT]** All of the initial work. 
