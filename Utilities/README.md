# Loken.Utilities

![Nuget](https://img.shields.io/nuget/v/Loken.Utilities)

Low level utility classes.


## Getting started

Install the package from nuget.org into your .NET 7 or later project using your package manager of choice, or the command line;

```shell
dotnet add package Loken.Utilities
```


## Features

A non-exhaustive list of features by namespace.

### Loken.Utilities.Collections

* MultiMap data structure for one-to-many relationships.

  A `MultiMap` is a `Dictionary<T, ISet<T>>` where each key maps to multiple values of the same type and provides convenience methods for managing multiple values per key.
  
  The `MultiMap` class provides `.Render()` and `.Parse()` methods which can act as serializers and takes some optional settings to control separators.
  
  We use `Convert.ChangeType` and `.ToString()` for conversions.
  
  ```csharp
  // Creating a MultiMap explicitly
  var map = new MultiMap<int>();
  map.Add(1, 11);
  map.Add(1, 12);
  map.Add(11, 111);
  map.Add(11, 112);
  map.Add(2, 21);
  map.Add(21, 212);
  
  // Creating an equivalent MultiMap from a string
  const string input = """
  1:11,12
  11:111,112
  2:21
  21:212
  """;
  
  MultiMap<int> parsedMap = MultiMap.Parse<int>(input);
  
  // Rendering the MultiMap back to a string.
  string output = MultiMap.Render(map);
  //     output = input;
  ```
  
* Interchangeable data structure for queues and stacks
  ```csharp
  ILinear<string> strings = useQueue ? new LinearQueue<string>() : new LinearStack<string>();
  strings.Attach("A", "B");
  var one = strings.Detach();
  var two = strings.Detach();
  // Content of one and two depends on useQueue.
  ```

### Loken.Utilities.ComponentModel

Our `DelimitedStringTypeConverter` allow you to convert a delimited string into an array of strings.

This is useful when you want to convert a string like `"one;other"` into an array of strings `["one", "other"]`.

```csharp
var attribute = new TypeConverterAttribute(typeof(DelimitedStringTypeConverter));
TypeDescriptor.AddAttributes(typeof(ICollection<string>), attribute);

string[] arr = "one;other".Convert<string[]>();
//       arr = ["one", "other"];
```

### Loken.Utilities.IO

* Read environment variables from `.env` and `.env.<EnvironmentName>` files.

  Given an env-file with the following content:
  ```ini
  STUFF=stuffer
  THING=thingy
  ```
  Calling load will add the two environment variables.
  ```csharp
  DotEnv.Load();
  ```
  The file can be in any ancestry folder, and you can optionally specify a different directory than the current directory or even a different file name.


## Feedback & Contribution

If you like what you see so far or would like to suggest changes to improve or extend what the library does, please don't hesitate to leave a comment in an issue or even a PR.

You can run the tests by cloning the repo, restoring packages, compiling and running the tests. There is no magic. There is a visual studio solution if you also like that.

The repository contains projects for other packages as well.
