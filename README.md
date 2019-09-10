[![NuGet](https://img.shields.io/nuget/v/Javi.ExplorerTreeView.svg)](https://www.nuget.org/packages/Javi.ExplorerTreeView/) 

# 

# <img align="center" src="./PackageIcon.png">  Javi.MediaInfo

This package provides a Windows Explorer application navigation like usercontrol for WPF.

![Desktop sample](https://github.com/jacovis/Javi.ExplorerTreeView/blob/master/Demo/samples/desktop.png "desktop")
![This PC sample](https://github.com/jacovis/Javi.ExplorerTreeView/blob/master/Demo/samples/thispc.png "thispc")
![MyComputer sample](https://github.com/jacovis/Javi.ExplorerTreeView/blob/master/Demo/samples/mycomputer.png "mycomputer")
![Videos special folder sample](https://github.com/jacovis/Javi.ExplorerTreeView/blob/master/Demo/samples/videos.png "videos")

- [Features](#features)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Demo application](#demo)
- [License](#license)
- [Acknowledgments](#acknowledgments)

## Features
- provides a Windows Explorer navigation pane alike treeview usercontrol for WPF
- targets .NET Full Framework 4
- supports lazy loading of treeview items
- Dependency property to get the selected folder
- Dependency property to set the root of the treeview
- Dependency property to set if the tree items should be sorted
- Dependency property UnloadItemsOnCollapse to set if the tree items should be unloaded when a branch is collapsed
    
## Getting Started

- Install package using nuget

Install Javi.ExplorerTreeView from NuGet using the Package Manager Console with the following command

    PM> Install-Package Javi.ExplorerTreeView

Alternatively search on [NuGet Javi.ExplorerTreeView](https://www.nuget.org/packages/Javi.ExplorerTreeView)

## Usage

See the [demo application](#demo).

## Demo application

A C# WPF [demo application](https://github.com/jacovis/Javi.ExplorerTreeView/tree/master/Demo) is available which 
shows the usage of the ExplorerTreeView control. Code from this demo should not be used in production code, the code is only to 
demonstrate the basic usage of WPF ExplorerTreeView usercontrol.
    
## License

This project is licensed under the [MIT License](https://github.com/jacovis/Javi.ExplorerTreeView/blob/master/LICENSE.md).

## Acknowledgments

[Dirkster99](https://www.nuget.org/profiles/Dirkster99) for his package [Dirkster.WSF](https://www.nuget.org/packages/Dirkster.WSF/)
providing access to the Windows Shell. 
This package is used to interact with the windows shell in order to obtain all special folders. However, the user of 
this ExplorerTreeView usercontrol does need to known anything or have to interact with Dirkster's package.
