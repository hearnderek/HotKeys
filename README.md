# HotKeys
A windows application which is listening for specific hotkeys even when in the background.

## Update [Left Shift] + [Right Shift]
A popup window to write your log of what you are currently doing.

See all of your logs with View in the Menu bar.


## Notez [Left Ctrl] + [Right Ctrl]
Single file text editor with some line highlighting and text collapsing.

### highlighting rules

Markdown Objects:
"[ ]" Yellow. Represets a task

Line endings:
 "-" grey out line
 "-" (on markdown header) collapse section
 "!" Light blue. Represents an idea which you want to stand out.
 "?" Yellow. Represents a question
 ";" Red. Represents a problem/blocker

## Conf
Simple configuration screen to connect to pastebin API, allowing for free cloudstorage of your data.
Note that while the API is usable,

# Why did I make this
I wanted a quick way to log time via a popup box which is summoned via hotkey. 
Instead of installing AutoHotKey, or one of the other windows automation suites I decided to build a simple powershell script -- mkl.ps1 -- to accomplish this. 
There were a few things I wish I was able to customize about the actual popup window, so I built something super simple within WPF. 
The hard part was getting the hotkeys to register.

As time went on I realized there were other things that would be nice to have at my fingertips.
If there is something I could imagine using at work, and on my home computer I will add it to this program.
For example my custom notetaking file and it's syntax highlighting.
I first made that within emacs, and found it extremely useful for writing things down and choosing what jumps out at you. 
