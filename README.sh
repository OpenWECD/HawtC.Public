#!/bin/bash
LANG=$1
if [ "$LANG" == "zh" ]; then
    cat README.md
else
    cat README_EN.md
fi