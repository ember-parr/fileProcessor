# File Processor


Self contained .NET Core application to compress audio files and assign file specific metadata. 

## Project Goal: 
- take input of folders 
    - each folder contains multiple audio files (.WAV)
    - each folder contains one metadata file (.json)
- create unique file name for each audio file in each folder
- confirm checksum for each audio file
- create individual metadata file for each audio file from Master metadata in parent folder
- compress audio file into ZIP without disrupting audio file integredy

## Tech Used: 
- C# and .NET Core
- File I/O Compression
- JSON Serialization
- Cryptography (MD5 Hashing)