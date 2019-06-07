# GitSync

GitSync is a .NET application to simplify the use of some Git commands like commit, add, push, pull, and inserting usernames and passwords.

### Technology

GitSync uses some open source projects to work properly:

* [.NET Core] - build and execute .NET applications on Windows, Linux, and MacOS.
* [LibGit2Sharp] - manage and execute native runtimes for Git commands.
* [Newtonsoft.Json] - control .JSON files and objects.

### Requirements

Gitsync requires [.NET Core Runtime] v2.1+ to run. You also have to use one of those Operating Systems:

* Linux (Alpine, Debian, Fedora, Linux-x64, Red Hat, Ubuntu, or derived distributions).
* Windows.
* MacOS X

### Usage

GitSync does not need installation, just download the latest release available [here](https://github.com/RafaelSantosBraz/AutoSyncRepositories-Git/releases) and edit the file *config.json*. You have to put at least your GitHub username and password in the configuration file. For more accurated signature and commit, please add your GitHub e-mail to the configuration file.

After that, you can open your favorite Terminal and run this command to execute the GitSync application.

Executing:
```sh
$ dotnet GitSync.dll some\git\repository\path "commit message here"
```

* To run the *.dll* this way, make sure to insert the path using a valid format, for example, if your path is *C:\Users\user\Desktop\my folder*, you must use " " on it because of the spaces. It is a valid insertion for this case:

Paths with spaces:
```sh
$ dotnet GitSync.dll "C:\Users\user\Desktop\my folder" "commit message here"
```

(optional) Paths with spaces on Linux distributions:
```sh
$ dotnet GitSync.dll /mnt/c/Users/user/Desktop/my\ folder/ "commit message here"
```

* In this case, use \ before the spaces.

### Results

Running GitSync implies the same results as the following Git command lines:

```sh
$ git add -a
$ git commit -m "commit message here"
$ git pull [inserting username and password if needed]
$ git push [inserting username and password]
```

### License

The MIT license (Refer to the [LICENSE.md] file).

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)

   [.NET Core]: <https://github.com/dotnet/core>
   [LibGit2Sharp]: <https://github.com/libgit2/libgit2sharp>
   [Newtonsoft.Json]: <https://github.com/JamesNK/Newtonsoft.Json>
   [.NET Core Runtime]: <https://dotnet.microsoft.com/download/dotnet-core/2.1>
   [LICENSE.md]: <https://github.com/RafaelSantosBraz/AutoSyncRepositories-Git/blob/master/LICENSE>
