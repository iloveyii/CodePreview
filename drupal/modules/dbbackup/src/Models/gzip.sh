#!/bin/bash
echo "GZIPPING ..."

# If cmd line param for TAR_FILE is provided then overwrite
if [ "$1" ]; then 
    TAR_FILE="${1}.gz"
fi
# Fix encoding
sed -i 's/utf8_0900_ai_ci/utf8_general_ci/g' $1
sed -i 's/utf8mb4_0900_ai_ci/utf8_general_ci/g' $1
# Fix collate
sed -i 's/utf8mb4/utf8/g' $1
sed -i 's/utf8mb3/utf8/g' $1

# GZip db file
tar czvfP $TAR_FILE $1
echo "Zipped with name: "$TAR_FILE