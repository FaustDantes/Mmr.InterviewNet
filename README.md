## Simple MVC applcation for detecting changes in the directory

## Technical details:
- Each file is read by the application and the file hash is computed. This hash is then compared with the previous hash stored in the JSON file to detect changes.
- Changes in the files are stored in the json file. This file is persistent storage.
- Due to time constrains and simplicity solution contains only one project. 

- In the future adjustments we could extract business logic (file detection, validation or storage data into persistent storage) into separate library. 
- Only english language is supported and any other translations are out of scope of the task.
 
## Assumptions and limitations: 
- The proper error handling and input validations are out of scope.
- The each file in the directory and subdirectories is not locked and could be read by application.
- There is enough space on the disk for storing directory changes. 
- The json is on secure location and can be modified only by application.
- This application is public and all hosts are allowed.
- Simple default application console logging is sufficient.
- Basic default design and styles are sufficient.
- The .NET version 8.0 is required.
- Only english language is supdeand.
- No user authentication is needed. 


Author: Michal Marcin  - Brno 2025