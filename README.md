# homework_alliance
<h2>Implementation</h2>

Firstly, In the separate file I created class where we store the data. I have chosen enum type for 'InboundStrategy' and 'PowerSupply' configId's because they can have small set of possible values. For 'resultStartTime' I decided to use simple string type, because that way it is easier to manipulate the value. I might consider changing it to the DataTime type, if the data was stored in the database.

In the 'Program' class 'Main' function I created a simple while loop with few conditions, to control the sequence of actions of the program. If the 'read' command is written:
files that have '*.txt' ending in the file name are picked. First, we check if a standard config file exists. If not, then we write a message to the console. Created function to read all files and extract information from each line. When the first element of the list is created, we copy that information and use it for the next element as a starting variant.

After all files are processed, we check for errors and display results in the console.

To display results we write 'print' to the console. Values that are not set in the reading phase are displayed as 'Error'. Each layer of the data is displayed to the console. Depending on the data that we're given we can see changes that were made to each layer.

