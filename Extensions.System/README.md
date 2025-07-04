# Loken.Extensions.System

![Nuget](https://img.shields.io/nuget/v/Loken.Extensions.System)

Extension methods for extending functionality that you'd typically find in the `System` namespace of .NET so that it's more convenient to use and generates less boiler plate code.


## Getting started

Install the package from nuget.org into your .NET 7 or later project using your package manager of choice, or the command line;

```shell
dotnet add package Loken.Extensions.System
```


## Features

A non-exhaustive list of features by namespace.

### Loken.System

Some of these provide extra utility and others are convenience wrappers for standard functionality.

> We consider it more convenient to call a method or extension method on a variable we have than passing that variable to a static method.
> ```csharp
> var arr = new[] { "A", "B", "C" };
> var cumbersome = string.Join('-', arr);
> var convenient = arr.Join('-');
> ```

- Split by a params of separators
  ```csharp
  string[] result = "A|B|C:D".SplitBy('|', ':');
  //       result = new[] { "A", "B", "C", "D" };
  ```

- Split by a params of separators to KeyValuePair
  ```csharp
  KeyValuePair<string, string?> kvp1 = "A:1".SplitKvp(':');
  //                            kvp1 = new KeyValuePair("A", "1");
  KeyValuePair<string, string?> kvp2 = "A".SplitKvp(':');
  //                            kvp2 = new KeyValuePair("A", null);
  ```

- Join by separator
  ```csharp
  string result = new[] {"A", "B"}.Join('-');
  //     result = "A-B";
  ```

- Remove affixed characters
  ```csharp
  string result1 = " - Thing".WithoutPrefix(' ', '-');
  //     result1 = "Thing";
  string result2 = "Thing-- ".WithoutSuffix(' ', '-');
  //     result2 = "Thing";
  ```

- Nullable booleans
  ```csharp
  bool isNull = maybe.IsTrueOrNull() && maybe.IsFalseOrNull();
  ```

- Convert.ChangeType
  ```csharp
  int result = "123".ChangeType<int>();
  //  result = 123;
  ```

- Random gaussian. Generated using the Box-Muller transform with pseudo random numbers from `System.Random`.
  ```csharp
  var random = new Random();
  double normal = random.NextGaussian();
  //     normal = random gaussian with a mean of 0 and standard deviation of 1.
  double gaussian = random.NextGaussian(50, 10);
  //     gaussian = random gaussian with a mean of 50 and standard deviation of 10.
  ```

### Loken.System.IO

* Enumerate a directory ancestry
  ```csharp
  IEnumerable<string> result = @"C:\Dev\Thing".DirectoryAncestry();
  string[] resultArr = result.ToArray();
  //       resultArr = new[] { "C:\Dev\Thing", "C:\Dev", "C:\" };
  ```
* Try to find a named file in the directory ancestry
  ```csharp
  bool found = @"C:\Dev\Thing".TryFindAncestryFile("Thingy.dll", out string existingPath);
  // found = true;
  // existingPath = @"C:\Dev\Thingy.dll";
  ```
* Read/write text string to/from a stream directly.

  Similar to how you would do the same using `System.IO.File`.
  ```csharp
  const string TEXT = "This is some text content";
  using var stream = new MemoryStream();

  TEXT.WriteAllText(stream);
  var text = stream.ReadAllText();

  // text = TEXT;
  ```
  Also supports a bunch of optional parameters that you may or may not need.

### Loken.System.Collections

* `.SingleOrDefault()` returns default when there are no elements and throws when there are more than one element, but sometimes you need to get the default in both of those cases, which is what `.SingleOrDefaultMany()` does.
  ```csharp
  string[] none = new string[0];
  string noneItem = none.SingleOrDefaultMany();
  //     noneItem = null;

  string[] single = new[] { "A" };
  string singleItem = single.SingleOrDefaultMany();
  //     singleItem = "A";

  string[] many = new[] { "B", "B" };
  string manyItem = many.SingleOrDefaultMany();
  //     manyItem = null;
  // Does not throw like many.SingleOrDefault();
  // Does not return "B" like many.FirstOrDefault();
  ```
  You can also pass it a defaultVal, if you like a different default than `null`.

* Trimming all of a sequence of strings
  ```csharp
  IEnumerable<string> strings = new[] { " A", "B ", "C C" };
  IEnumerable<string> trimmed = strings.Trim();
  ```
  Has similar methods for `.TrimStart()` and `.TrimEnd()` as well.
  Supports passing another trimming character than the default space character `'   '`.
  It can omit any entries that are trimmed down to the empty string.

* Enuque multiple items
  ```csharp
  var queue = new Queue<string>();
  queue.Enqueue("1", "2", "3");
  IEnumerable<string> strings = new[] { "A", "B", "C" };
  queue.Enqueue(strings);
  ```

* Read/write a dictionary of strings to a string so that it can be passed in a URL.
  ```csharp
  const string source = "ANull~Str:SomeText~Text:Some text";
  IDictionary<string, string> read = source.ReadStrings();
  string written = read.WriteString();
  //     written = source;
  ```
  You can also pass custom separators for separating pairs and entries.

* Read/write a dictionary of values to a string so that it can be passed in a URL.

  This is like the above but additionally attempts to convert the values to bool, int, double, array of values, explicit string (wrapped in single quotes) and Guid, in that order before assuming the value should be a string.
  ```csharp
  const string source = "ANull~Str:SomeText~Text:'Some text'~Num:42~Dbl:3.1415~On:true~Off:false~Id:f43f1ec5-04d9-481d-9c10-511577366b59~List:('hi',42,3.1415,true,false)";
  IDictionary<string, object?> read = source.ReadValues();
  string written = read.WriteValues();
  //     written = source;
  ```
  You can also pass custom separators for separating pairs and entries.

### Loken.System.Collections.ComponentModel

We provide some convenience extension methods for the component model which makes it easier to use the `TypeConverter`s registered in the `TypeDescriptors`.

```csharp
int number = "350".Convert<int>();
//  number = 350;

const string MOMENT = "03/01/1900 12:30:59";
DateTime moment = MOMENT.Convert<DateTime>();
// moment = new DateTime(1900, 3, 1, 12, 30, 59, DateTimeKind.Utc);
```

This also means that you can extend these methods simply by registering your own `TypeConverter`.


## Feedback & Contribution

If you like what you see so far or would like to suggest changes to improve or extend what the library does, please don't hesitate to leave a comment in an issue or even a PR.

You can run the tests by cloning the repo, restoring packages, compiling and running the tests. There is no magic. There is a visual studio solution if you also like that.

The repository contains projects for other packages as well.
