# File-Organizer
An application that lets you quickly move files to specified locations on the fly

Ever just download a bunch of wallpapers only to regret not categorizing them into folders while you were saving them?
...
No? Well, I did. So I started work on this, it's pretty basic but should get the job done.

<h3>Usage</h3>  
When you launch the app, it defaults to your Pictures folder. If you have any thing in there, then great!
You'll see an image and from there your only option is to skip it and go to the next one...
Well don't worry- you can add more commands then that.
Just open up the newly created text file in your Documents folder.
You'll find that it constains this line:<br/>
[Space,,move] - which means the file will be skipped when you press the spacebar.<br/>

Just add a new line in this format:<br/>
  
['Key','Full directory path','Action']<br/>
  
'Key' is a key on your keyboard typed out (i.e. the a key = "A")<br/>
'Full directory path' is a folder path in your system (i.e. "C:\Users\%User%\MyPictures\Nature")<br/>
'Action' can be one of the following: Move, Delete, Copy

Save it. If you entered a proper key, directory, and action you'll now have set up your own keybinding.
