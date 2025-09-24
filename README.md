
# Horizon Programming Language

Horizon is an open-source project and high-level programming language.

## Build

> I recommend you to use [Visual Studio IDE](https://visualstudio.microsoft.com/) or [JetBrains Rider IDE](https://www.jetbrains.com/rider/),
> But you can use what you want.

Make sure you install [.NET SDK 9.0](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) or later

> .NET 8.0 work fine, but you need to make some changes
> into `.csproj` files.

After installing the **SDK**, go to the project folder, open terminal and run this:

```bash
# Build the projects
dotnet build
```

You can find the executables in `Horizon/bin/` folder.

## Example

An imaginary example of the syntax

```csharp
package example;

void main() 
{
    var x = 200F;
    float y = x * 2f;
    Console::writeln($"Hello World: {x + y}");
}
```