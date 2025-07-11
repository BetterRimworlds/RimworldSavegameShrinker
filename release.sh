#!/bin/bash
set -euo pipefail


ORIG_PWD=$PWD
#MOD_VERSION=$(git log | git log --grep='^ *Version' -n1 --format='%s'  | sed -E 's/.*Version ([0-9]+\.[0-9]+\.[0-9]+).*/v\1/')
MOD_VERSION=$(git describe --tags --abbrev=0)
MOD=$(basename $PWD)
MOD_ZIP=${MOD}-"${MOD_VERSION}".zip

cd /rimworld/1.2/Mods
zip -r ${MOD_ZIP} ${MOD}
cp -v ${MOD_ZIP} /tmp
cp -v ${MOD_ZIP} $ORIG_PWD
