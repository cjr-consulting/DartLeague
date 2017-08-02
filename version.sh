#!/bin/bash

export SEMVER_LAST_TAG=$(git describe --abbrev=0 --tags 2>/dev/null)
export SEMVER_RELEASE_LEVEL=$(git log --oneline -1 --pretty=%B | cat | tr -d '\n' | cut -d "[" -f2 | cut -d "]" -f1)

if [ -z "$SEMVER_LAST_TAG" ]; then
    export SEMVER_LAST_TAG="0.0.0"
    echo "No tags defined"
fi

echo "$SEMVER_LAST_TAG"
echo "$SEMVER_RELEASE_LEVEL"
echo "Getting semver-tool"

git clone https://github.com/fsaintjacques/semver-tool /tmp/semver
$(cd /tmp/semver; git checkout tags/2.0.0)
export PATH=$PATH:/tmp/semver/src

export SEMVER_NEXT=$(semver bump patch $SEMVER_LAST_TAG)
