using CSharpLearningLab.Models;

namespace CSharpLearningLab.Data;

/// <summary>
/// All lessons for the learning lab. Designed as a 1-2 week intensive for a junior
/// fullstack developer whose only prior language is TypeScript. Every lesson opens
/// with a direct TS → C# analogy so new syntax maps onto something already familiar.
/// </summary>
public static class LessonContent
{
    public static readonly Lesson[] All =
    [
        // ================================================================
        // WEEK 1 - LANGUAGE FUNDAMENTALS
        // ================================================================
        new Lesson(
            Id: "01-hello-world",
            Order: 1,
            Title: "Hello, C#!",
            Category: "Fundamentals",
            TsAnalogy:
                "In TypeScript you'd write `console.log(\"Hello\")`. In C# you write " +
                "`Console.WriteLine(\"Hello\")`. `Console` is a static class in the `System` namespace " +
                "(like importing from a built-in module).",
            Explanation:
                "C# is a statically-typed, compiled language that runs on .NET. The entry point used to " +
                "be a verbose `static void Main(string[] args)` inside a class — but since C# 9, .NET " +
                "supports **top-level statements**, so a tiny program looks almost identical to a TS script.\n\n" +
                "Strings use double quotes ONLY (single quotes are for single `char` values). Every " +
                "statement ends with `;` — not optional like TS.",
            StarterCode:
                "// Your first C# program. Hit \"Run\" to execute it.\n" +
                "Console.WriteLine(\"Hello, C#!\");\n" +
                "\n" +
                "// Try: print your name on the next line",
            SampleSolution:
                "Console.WriteLine(\"Hello, C#!\");\n" +
                "Console.WriteLine(\"My name is Claude\");",
            ExpectedOutput: "Hello, C#!\nMy name is Claude\n",
            KeyPoints:
            [
                "`Console.WriteLine` is the `console.log` of C#",
                "Strings MUST use double quotes; single quotes are for `char` only",
                "Every statement ends with `;`",
                "`System` namespace is auto-imported in this playground (like a global)"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "Which of these is a valid C# string?",
                    Choices: ["'hello'", "\"hello\"", "`hello`", "hello"],
                    CorrectIndex: 1,
                    Explanation: "C# strings use double quotes. Single quotes are reserved for `char` literals like 'a'."
                )
            ]
        ),

        new Lesson(
            Id: "02-variables-types",
            Order: 2,
            Title: "Variables & Types",
            Category: "Fundamentals",
            TsAnalogy:
                "TS: `let name: string = \"Dan\"` → C#: `string name = \"Dan\";`\n" +
                "TS: `const age = 30` → C#: `const int age = 30;` (or more commonly `readonly` fields / `const` for compile-time constants)\n" +
                "TS: `let x` (inferred) → C#: `var x = 5;` (inferred at compile time — still strongly typed)",
            Explanation:
                "C# is **strongly and statically typed**. Every variable has a type that's locked in at compile time. " +
                "You can either write the type explicitly (`int x = 5;`) or use `var` to let the compiler infer it (`var x = 5;`). " +
                "Unlike TS's `any`, `var` is NOT dynamic — the compiler still knows `x` is an `int`.\n\n" +
                "Common primitive types:\n" +
                "• `int` — 32-bit integer (TS `number`)\n" +
                "• `long` — 64-bit integer\n" +
                "• `double` — 64-bit float (TS `number`)\n" +
                "• `decimal` — 128-bit exact decimal (use for money!)\n" +
                "• `bool` — `true`/`false` (NOT `boolean`)\n" +
                "• `string` — UTF-16 string\n" +
                "• `char` — single 16-bit character\n\n" +
                "**Nullability**: By default, reference types are non-nullable in modern C# (like TS `strict` mode). " +
                "Add `?` to allow null: `string? maybeName = null;`",
            StarterCode:
                "int age = 30;\n" +
                "var name = \"Dan\";           // inferred as string\n" +
                "double price = 19.99;\n" +
                "bool isActive = true;\n" +
                "string? nickname = null;    // nullable\n" +
                "\n" +
                "Console.WriteLine($\"{name}, {age}, ${price}, active={isActive}, nick={nickname ?? \"(none)\"}\");",
            SampleSolution:
                "int age = 30;\n" +
                "var name = \"Dan\";\n" +
                "double price = 19.99;\n" +
                "bool isActive = true;\n" +
                "string? nickname = null;\n" +
                "Console.WriteLine($\"{name}, {age}, ${price}, active={isActive}, nick={nickname ?? \"(none)\"}\");",
            ExpectedOutput: "Dan, 30, $19.99, active=True, nick=(none)\n",
            KeyPoints:
            [
                "`var` = type inference, NOT dynamic typing",
                "`$\"...{expr}...\"` is string interpolation (like TS template literals)",
                "`??` is the null-coalescing operator (identical to TS)",
                "Use `decimal` for money — NEVER `double` or `float`",
                "Booleans print as `True`/`False` (capitalized)"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "What type should you use for a price in dollars?",
                    Choices: ["double", "float", "decimal", "int"],
                    CorrectIndex: 2,
                    Explanation: "`decimal` is exact base-10 — no floating-point rounding errors. Always use it for money."
                ),
                new Quiz(
                    Question: "Is `var` the same as TypeScript's `any`?",
                    Choices: ["Yes, fully dynamic", "No, it's compile-time inferred and strongly typed", "Only inside methods", "Only for numbers"],
                    CorrectIndex: 1,
                    Explanation: "`var` is inference. The compiler picks an exact type and locks it in; `any`-like behavior requires `dynamic`."
                )
            ]
        ),

        new Lesson(
            Id: "03-strings",
            Order: 3,
            Title: "Strings & Interpolation",
            Category: "Fundamentals",
            TsAnalogy:
                "TS: `` `Hello ${name}` `` → C#: `$\"Hello {name}\"`\n" +
                "TS: multi-line template literals → C#: raw string literals `\"\"\"...\"\"\"`\n" +
                "TS: `str.length` / `str.toUpperCase()` → C#: `str.Length` / `str.ToUpper()` (PascalCase methods!)",
            Explanation:
                "Strings in C# are immutable, like TS. Key differences:\n\n" +
                "• **Interpolation** uses `$\"{expr}\"` instead of backticks\n" +
                "• **Methods are PascalCase**: `ToUpper()`, `Substring()`, `IndexOf()`, `Replace()`\n" +
                "• **Length is a property**, not a method: `str.Length` (no parens)\n" +
                "• **Raw strings** (C# 11+): triple quotes preserve formatting and let you embed quotes without escaping:\n" +
                "  ```\n" +
                "  var json = \"\"\"{ \"name\": \"Dan\" }\"\"\";\n" +
                "  ```\n" +
                "• **Verbatim strings**: prefix with `@` to disable escape sequences (great for Windows paths):\n" +
                "  `@\"C:\\Users\\Dan\"` — no need for `\\\\`.",
            StarterCode:
                "var first = \"Ada\";\n" +
                "var last = \"Lovelace\";\n" +
                "\n" +
                "// Interpolation\n" +
                "Console.WriteLine($\"Full name: {first} {last}\");\n" +
                "Console.WriteLine($\"Upper: {first.ToUpper()}, length: {first.Length}\");\n" +
                "\n" +
                "// Raw string for JSON-like content\n" +
                "var json = $$\"\"\"\n" +
                "{ \"name\": \"{{first}} {{last}}\" }\n" +
                "\"\"\";\n" +
                "Console.WriteLine(json);",
            SampleSolution: "",
            ExpectedOutput: "Full name: Ada Lovelace\nUpper: ADA, length: 3\n{ \"name\": \"Ada Lovelace\" }\n\n",
            KeyPoints:
            [
                "`$\"...\"` = string interpolation",
                "`@\"...\"` = verbatim (no escape sequences)",
                "`\"\"\"...\"\"\"` = raw string (C# 11+)",
                "`$$\"\"\"...{{expr}}...\"\"\"` = raw string + interpolation (double `$` means `{{ }}` is the interpolation delimiter)",
                "`Length` is a property (no parens); most methods are PascalCase"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "How do you get a string's length in C#?",
                    Choices: ["str.length()", "str.length", "str.Length", "str.size"],
                    CorrectIndex: 2,
                    Explanation: "`Length` — PascalCase, property (no parens)."
                )
            ]
        ),

        new Lesson(
            Id: "04-collections",
            Order: 4,
            Title: "Arrays, Lists & Dictionaries",
            Category: "Fundamentals",
            TsAnalogy:
                "TS: `number[]` or `Array<number>` → C#: `int[]` (fixed-size array) or `List<int>` (resizable, the usual choice)\n" +
                "TS: `Record<string, number>` or `Map<string, number>` → C#: `Dictionary<string, int>`\n" +
                "TS: `new Set<string>()` → C#: `new HashSet<string>()`",
            Explanation:
                "C# has both fixed-size **arrays** (`int[]`) and dynamic **List<T>** — 99% of the time you want `List<T>`. " +
                "`Dictionary<K,V>` is the go-to key/value map. All of these live in `System.Collections.Generic`.\n\n" +
                "**Collection expressions** (C# 12+) let you initialize collections with `[1, 2, 3]` — almost identical to JS:\n" +
                "```csharp\n" +
                "List<int> nums = [1, 2, 3];\n" +
                "int[] arr = [1, 2, 3];\n" +
                "```\n\n" +
                "Common List<T> methods: `Add`, `Remove`, `Contains`, `Count` (property!), `IndexOf`.",
            StarterCode:
                "List<string> langs = [\"TypeScript\", \"C#\", \"Rust\"];\n" +
                "langs.Add(\"Go\");\n" +
                "\n" +
                "Console.WriteLine($\"Count: {langs.Count}\");\n" +
                "foreach (var lang in langs)\n" +
                "{\n" +
                "    Console.WriteLine($\"- {lang}\");\n" +
                "}\n" +
                "\n" +
                "Dictionary<string, int> ages = new()\n" +
                "{\n" +
                "    [\"Dan\"] = 30,\n" +
                "    [\"Ada\"] = 36\n" +
                "};\n" +
                "ages[\"Grace\"] = 85;\n" +
                "\n" +
                "foreach (var (name, age) in ages)\n" +
                "{\n" +
                "    Console.WriteLine($\"{name} is {age}\");\n" +
                "}",
            SampleSolution: "",
            ExpectedOutput:
                "Count: 4\n- TypeScript\n- C#\n- Rust\n- Go\nDan is 30\nAda is 36\nGrace is 85\n",
            KeyPoints:
            [
                "Prefer `List<T>` over `T[]` unless you really need a fixed array",
                "`Count` is a property on List (no parens); `Length` is used for arrays & strings",
                "Collection expressions `[1,2,3]` are the modern init syntax (C# 12+)",
                "`foreach` over a dictionary gives you `(key, value)` tuples — deconstruct them!"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "What's the C# equivalent of TS `Map<string, number>`?",
                    Choices: ["HashMap<string, int>", "Dictionary<string, int>", "Record<string, int>", "List<string, int>"],
                    CorrectIndex: 1,
                    Explanation: "`Dictionary<K,V>` from `System.Collections.Generic` — the workhorse hash map."
                )
            ]
        ),

        new Lesson(
            Id: "05-control-flow",
            Order: 5,
            Title: "Control Flow & Pattern Matching",
            Category: "Fundamentals",
            TsAnalogy:
                "`if`, `else`, `for`, `while` work almost identically to TS. The big upgrade is **switch expressions** " +
                "and **pattern matching** — way more powerful than TS's `switch`.",
            Explanation:
                "Classic C-style `if`/`else`/`for`/`while`/`foreach` — nothing surprising for a TS dev.\n\n" +
                "The *real* upgrade is the **switch expression** (C# 8+). Unlike TS's statement-based switch, " +
                "it returns a value and supports rich patterns:\n\n" +
                "```csharp\n" +
                "var label = status switch\n" +
                "{\n" +
                "    200 => \"OK\",\n" +
                "    >= 400 and < 500 => \"Client error\",\n" +
                "    >= 500 => \"Server error\",\n" +
                "    _ => \"Unknown\"          // _ is the default case\n" +
                "};\n" +
                "```\n\n" +
                "You can match on types, tuples, property values, ranges — it's extremely expressive.",
            StarterCode:
                "int[] codes = [200, 301, 404, 500, 503];\n" +
                "\n" +
                "foreach (var code in codes)\n" +
                "{\n" +
                "    var label = code switch\n" +
                "    {\n" +
                "        200 => \"OK\",\n" +
                "        >= 300 and < 400 => \"Redirect\",\n" +
                "        >= 400 and < 500 => \"Client error\",\n" +
                "        >= 500 => \"Server error\",\n" +
                "        _ => \"Unknown\"\n" +
                "    };\n" +
                "    Console.WriteLine($\"{code}: {label}\");\n" +
                "}",
            SampleSolution: "",
            ExpectedOutput:
                "200: OK\n301: Redirect\n404: Client error\n500: Server error\n503: Server error\n",
            KeyPoints:
            [
                "Switch expressions RETURN a value — they're not statements",
                "`_` is the default arm (like TS `default:`)",
                "You can combine patterns with `and`, `or`, `not`",
                "`foreach (var x in collection)` is the idiomatic iteration loop"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "What does `_` mean in a switch expression?",
                    Choices: ["Private variable", "Discard", "Default case (catch-all)", "Null"],
                    CorrectIndex: 2,
                    Explanation: "`_` is the discard/catch-all pattern — it matches anything not matched by prior arms."
                )
            ]
        ),

        new Lesson(
            Id: "06-methods",
            Order: 6,
            Title: "Methods & Parameters",
            Category: "Fundamentals",
            TsAnalogy:
                "TS: `function add(a: number, b: number): number { return a + b; }`\n" +
                "C#: `int Add(int a, int b) => a + b;`\n\n" +
                "TS arrow functions roughly map to C# **expression-bodied methods** (`=>`).",
            Explanation:
                "Methods in C# are declared inside classes. For top-level code (like this playground), you can " +
                "declare local functions right next to your main statements.\n\n" +
                "Key features:\n" +
                "• **Expression-bodied methods**: `int Square(int x) => x * x;`\n" +
                "• **Default parameter values**: `void Log(string msg, bool verbose = false) { ... }`\n" +
                "• **Named arguments**: `Log(msg: \"hi\", verbose: true)`\n" +
                "• **`params` arrays**: variadic like TS `...args`: `void Print(params string[] items)`\n" +
                "• **`ref` / `out`**: pass by reference (no direct TS equivalent — TS passes by value)\n\n" +
                "Method names are PascalCase by convention.",
            StarterCode:
                "int Add(int a, int b) => a + b;\n" +
                "\n" +
                "string Greet(string name, string greeting = \"Hello\") =>\n" +
                "    $\"{greeting}, {name}!\";\n" +
                "\n" +
                "int Sum(params int[] nums)\n" +
                "{\n" +
                "    int total = 0;\n" +
                "    foreach (var n in nums) total += n;\n" +
                "    return total;\n" +
                "}\n" +
                "\n" +
                "Console.WriteLine(Add(2, 3));\n" +
                "Console.WriteLine(Greet(\"Dan\"));\n" +
                "Console.WriteLine(Greet(\"Dan\", greeting: \"Hi\"));    // named arg\n" +
                "Console.WriteLine(Sum(1, 2, 3, 4, 5));",
            SampleSolution: "",
            ExpectedOutput: "5\nHello, Dan!\nHi, Dan!\n15\n",
            KeyPoints:
            [
                "Expression-bodied methods `=> expr` are the C# equivalent of one-line arrow functions",
                "Default params & named args are first-class features",
                "`params T[]` is C#'s `...rest` / variadic",
                "Method names are PascalCase by convention, not camelCase"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "What's the C# convention for method names?",
                    Choices: ["camelCase", "snake_case", "PascalCase", "kebab-case"],
                    CorrectIndex: 2,
                    Explanation: "Methods, classes, properties, and namespaces are all PascalCase. Only local variables & parameters are camelCase."
                )
            ]
        ),

        new Lesson(
            Id: "07-classes",
            Order: 7,
            Title: "Classes, Properties & Constructors",
            Category: "OOP",
            TsAnalogy:
                "TS and C# classes look nearly identical, with one BIG difference: C# has **real properties** " +
                "(getter/setter pairs generated by the compiler) instead of fields + manual get/set.",
            Explanation:
                "A C# class:\n\n" +
                "```csharp\n" +
                "public class User\n" +
                "{\n" +
                "    public string Name { get; set; }      // auto-property\n" +
                "    public int Age { get; init; }          // settable only in ctor/init\n" +
                "    public string Role { get; private set; } = \"user\";  // public get, private set\n" +
                "\n" +
                "    public User(string name, int age)      // constructor\n" +
                "    {\n" +
                "        Name = name;\n" +
                "        Age = age;\n" +
                "    }\n" +
                "\n" +
                "    public void Promote() => Role = \"admin\";\n" +
                "}\n" +
                "```\n\n" +
                "• `{ get; set; }` — auto-property, generates a hidden backing field\n" +
                "• `{ get; init; }` — can only be set during object initialization (like TS `readonly`)\n" +
                "• `public`, `private`, `internal`, `protected` — access modifiers\n\n" +
                "**Object initializer syntax** is a shortcut: `new User { Name = \"Dan\", Age = 30 }`.",
            StarterCode:
                "public class User\n" +
                "{\n" +
                "    public string Name { get; set; }\n" +
                "    public int Age { get; init; }\n" +
                "    public string Role { get; private set; } = \"user\";\n" +
                "\n" +
                "    public User(string name, int age)\n" +
                "    {\n" +
                "        Name = name;\n" +
                "        Age = age;\n" +
                "    }\n" +
                "\n" +
                "    public void Promote() => Role = \"admin\";\n" +
                "\n" +
                "    public override string ToString() => $\"{Name} ({Age}) - {Role}\";\n" +
                "}\n" +
                "\n" +
                "var dan = new User(\"Dan\", 30);\n" +
                "Console.WriteLine(dan);\n" +
                "dan.Promote();\n" +
                "Console.WriteLine(dan);\n" +
                "\n" +
                "// object initializer (requires a parameterless ctor OR matching init)\n" +
                "var ada = new User(\"Ada\", 36) { Name = \"Ada Lovelace\" };\n" +
                "Console.WriteLine(ada);",
            SampleSolution: "",
            ExpectedOutput:
                "Dan (30) - user\nDan (30) - admin\nAda Lovelace (36) - user\n",
            KeyPoints:
            [
                "`{ get; set; }` creates a real property with a hidden backing field",
                "`init` setters allow one-time assignment during construction (immutable-ish)",
                "`override ToString()` is how you customize an object's string form (like TS `toString()`)",
                "Object initializers `new T { Prop = value }` work after the constructor runs"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "What does `{ get; init; }` mean?",
                    Choices: [
                        "Can be set anywhere",
                        "Read-only forever",
                        "Can only be set during object construction/initialization",
                        "Requires explicit backing field"
                    ],
                    CorrectIndex: 2,
                    Explanation: "`init` setters are called once during construction, then the property is effectively read-only. Great for immutable-style objects."
                )
            ]
        ),

        new Lesson(
            Id: "08-interfaces-inheritance",
            Order: 8,
            Title: "Interfaces & Inheritance",
            Category: "OOP",
            TsAnalogy:
                "TS structural typing: `interface Animal { speak(): string }` → any object with a `speak()` method fits.\n" +
                "C# is **nominal**: a class must EXPLICITLY declare `class Dog : IAnimal` to implement it.\n" +
                "By convention, interface names start with `I` (`IAnimal`, `IUserRepository`).",
            Explanation:
                "**Interfaces** define contracts:\n\n" +
                "```csharp\n" +
                "public interface IAnimal\n" +
                "{\n" +
                "    string Name { get; }\n" +
                "    string Speak();\n" +
                "}\n" +
                "```\n\n" +
                "**Classes** inherit a single base class and any number of interfaces:\n\n" +
                "```csharp\n" +
                "public class Dog : Animal, IAnimal   // base class first, then interfaces\n" +
                "{\n" +
                "    public override string Speak() => \"Woof\";\n" +
                "}\n" +
                "```\n\n" +
                "• `abstract` — class or method that must be implemented by subclasses\n" +
                "• `virtual` — base method that CAN be overridden\n" +
                "• `override` — required keyword when overriding (no silent override like TS)\n" +
                "• `sealed` — prevents further inheritance (like TS `final`)\n\n" +
                "C# has single inheritance for classes, multiple for interfaces.",
            StarterCode:
                "public interface IAnimal\n" +
                "{\n" +
                "    string Name { get; }\n" +
                "    string Speak();\n" +
                "}\n" +
                "\n" +
                "public abstract class Animal : IAnimal\n" +
                "{\n" +
                "    public string Name { get; }\n" +
                "    protected Animal(string name) => Name = name;\n" +
                "    public abstract string Speak();\n" +
                "}\n" +
                "\n" +
                "public class Dog : Animal\n" +
                "{\n" +
                "    public Dog(string name) : base(name) { }\n" +
                "    public override string Speak() => \"Woof\";\n" +
                "}\n" +
                "\n" +
                "public class Cat : Animal\n" +
                "{\n" +
                "    public Cat(string name) : base(name) { }\n" +
                "    public override string Speak() => \"Meow\";\n" +
                "}\n" +
                "\n" +
                "List<IAnimal> zoo = [new Dog(\"Rex\"), new Cat(\"Whiskers\")];\n" +
                "foreach (var animal in zoo)\n" +
                "{\n" +
                "    Console.WriteLine($\"{animal.Name}: {animal.Speak()}\");\n" +
                "}",
            SampleSolution: "",
            ExpectedOutput: "Rex: Woof\nWhiskers: Meow\n",
            KeyPoints:
            [
                "Interfaces are prefixed `I` by convention: `IAnimal`, `IDisposable`",
                "Single class inheritance, multiple interface implementation",
                "`override` is REQUIRED when overriding a `virtual`/`abstract` method",
                "`base(...)` calls the parent constructor (like TS `super(...)`)",
                "C# is nominally typed — you must explicitly declare `: IInterface`"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "Can a C# class inherit from multiple base classes?",
                    Choices: ["Yes, unlimited", "Yes, up to 2", "No, single inheritance only (but multiple interfaces)", "Only abstract classes"],
                    CorrectIndex: 2,
                    Explanation: "Single class inheritance, but you can implement as many interfaces as you want."
                )
            ]
        ),

        new Lesson(
            Id: "09-generics",
            Order: 9,
            Title: "Generics",
            Category: "Intermediate",
            TsAnalogy:
                "TS: `function identity<T>(x: T): T { return x }` → C#: `T Identity<T>(T x) => x;`\n" +
                "Generic syntax and intent are nearly identical. C# adds **constraints**: `where T : IComparable`.",
            Explanation:
                "Generics let you write type-safe, reusable code. You've already seen them: `List<T>`, `Dictionary<K,V>`.\n\n" +
                "You can constrain a generic parameter:\n\n" +
                "```csharp\n" +
                "// T must be a reference type\n" +
                "T FirstOrNull<T>(List<T> items) where T : class => items.FirstOrDefault();\n" +
                "\n" +
                "// T must implement IComparable<T>\n" +
                "T Max<T>(T a, T b) where T : IComparable<T> =>\n" +
                "    a.CompareTo(b) >= 0 ? a : b;\n" +
                "\n" +
                "// T must have a parameterless constructor\n" +
                "T Create<T>() where T : new() => new T();\n" +
                "```\n\n" +
                "Common constraints: `class`, `struct`, `new()`, `IInterface`, `BaseClass`.",
            StarterCode:
                "T Max<T>(T a, T b) where T : IComparable<T> =>\n" +
                "    a.CompareTo(b) >= 0 ? a : b;\n" +
                "\n" +
                "Console.WriteLine(Max(3, 7));\n" +
                "Console.WriteLine(Max(\"apple\", \"banana\"));\n" +
                "Console.WriteLine(Max(1.5, 0.5));",
            SampleSolution: "",
            ExpectedOutput: "7\nbanana\n1.5\n",
            KeyPoints:
            [
                "Generic syntax: `<T>`, `<T, U>` — same as TS",
                "`where T : ...` adds compile-time constraints (TS lacks a direct equivalent)",
                "`List<T>`, `Dictionary<K,V>`, `Task<T>`, `IEnumerable<T>` — you'll see these everywhere"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "What does `where T : class` constrain T to?",
                    Choices: ["Any class named 'class'", "A reference type (not a struct/int/etc.)", "A class with a method `class`", "Generic type parameter"],
                    CorrectIndex: 1,
                    Explanation: "`where T : class` restricts T to reference types. `where T : struct` restricts to value types."
                )
            ]
        ),

        new Lesson(
            Id: "10-linq",
            Order: 10,
            Title: "LINQ — The Superpower",
            Category: "Intermediate",
            TsAnalogy:
                "LINQ is the .NET equivalent of JS array methods (`.map`, `.filter`, `.reduce`) — except it works on " +
                "ANY collection (and even databases via EF Core). The naming is different:\n\n" +
                "• `.map` → `.Select`\n" +
                "• `.filter` → `.Where`\n" +
                "• `.reduce` → `.Aggregate`\n" +
                "• `.find` → `.FirstOrDefault`\n" +
                "• `.some` → `.Any`\n" +
                "• `.every` → `.All`\n" +
                "• `.sort` → `.OrderBy`\n" +
                "• `.flatMap` → `.SelectMany`",
            Explanation:
                "LINQ (**Language Integrated Query**) adds query operators to every `IEnumerable<T>`. It's lazy " +
                "(like a generator) — operators build a pipeline and only execute when you iterate or call a " +
                "**terminal** operator like `ToList()`, `Count()`, `First()`.\n\n" +
                "```csharp\n" +
                "var nums = new[] { 1, 2, 3, 4, 5, 6 };\n" +
                "var evenSquares = nums\n" +
                "    .Where(n => n % 2 == 0)\n" +
                "    .Select(n => n * n)\n" +
                "    .ToList();                 // terminal — forces execution\n" +
                "```\n\n" +
                "**Lambda expressions** (`n => n * n`) are C#'s arrow functions. They're used everywhere.\n\n" +
                "There's also a SQL-like query syntax (`from x in xs where ... select ...`) but the method-chain " +
                "style is more common in modern code.",
            StarterCode:
                "var people = new[]\n" +
                "{\n" +
                "    new { Name = \"Dan\", Age = 30, Role = \"dev\" },\n" +
                "    new { Name = \"Ada\", Age = 36, Role = \"dev\" },\n" +
                "    new { Name = \"Grace\", Age = 85, Role = \"admin\" },\n" +
                "    new { Name = \"Linus\", Age = 54, Role = \"dev\" }\n" +
                "};\n" +
                "\n" +
                "// All devs, ordered by age, just names\n" +
                "var devNames = people\n" +
                "    .Where(p => p.Role == \"dev\")\n" +
                "    .OrderBy(p => p.Age)\n" +
                "    .Select(p => p.Name)\n" +
                "    .ToList();\n" +
                "\n" +
                "Console.WriteLine(string.Join(\", \", devNames));\n" +
                "\n" +
                "// Aggregates\n" +
                "Console.WriteLine($\"Total people: {people.Count()}\");\n" +
                "Console.WriteLine($\"Avg age: {people.Average(p => p.Age):F1}\");\n" +
                "Console.WriteLine($\"Any under 18? {people.Any(p => p.Age < 18)}\");\n" +
                "Console.WriteLine($\"Oldest: {people.MaxBy(p => p.Age)!.Name}\");",
            SampleSolution: "",
            ExpectedOutput:
                "Dan, Ada, Linus\nTotal people: 4\nAvg age: 51.3\nAny under 18? False\nOldest: Grace\n",
            KeyPoints:
            [
                "LINQ is LAZY until you hit a terminal operator (`ToList`, `Count`, `First`, etc.)",
                "Lambdas `x => expr` are the same concept as TS arrow functions",
                "`Where` = filter, `Select` = map, `Aggregate` = reduce",
                "`MinBy`/`MaxBy`/`OrderBy` take a key selector, not a comparator",
                "Anonymous objects: `new { Name = \"Dan\" }` — like TS inline object types"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "Which LINQ method is equivalent to JS Array.filter?",
                    Choices: ["Select", "Where", "Filter", "Find"],
                    CorrectIndex: 1,
                    Explanation: "`Where` filters; `Select` maps. (It's named after SQL's `WHERE` clause.)"
                )
            ]
        ),

        // ================================================================
        // WEEK 2 - .NET PLATFORM
        // ================================================================
        new Lesson(
            Id: "11-async",
            Order: 11,
            Title: "async / await & Task<T>",
            Category: "Async",
            TsAnalogy:
                "TS: `async function fetchUser(): Promise<User>` → C#: `async Task<User> FetchUserAsync()`\n" +
                "`Task<T>` ≈ `Promise<T>`. `Task` (no generic) ≈ `Promise<void>`. The `await` keyword is identical.\n\n" +
                "Convention: async methods are suffixed with `Async` (`GetUserAsync`, `SaveAsync`).",
            Explanation:
                "C# `async`/`await` works almost exactly like TS. A few differences:\n\n" +
                "• `Task<T>` is the return type for an async method that returns `T`\n" +
                "• `Task` (no generic) = `Promise<void>`\n" +
                "• `ValueTask<T>` is a lighter-weight variant for hot paths (rarely needed early on)\n" +
                "• Methods should be suffixed `Async` by convention\n" +
                "• `await Task.WhenAll(...)` ≈ `Promise.all(...)`\n" +
                "• `await Task.Delay(ms)` ≈ `setTimeout` wrapped in a promise\n\n" +
                "**Critical**: never call `.Result` or `.Wait()` on a Task — that's `await`'s evil twin and will deadlock in some contexts. Always `await`.",
            StarterCode:
                "async Task<string> FetchGreetingAsync(string name)\n" +
                "{\n" +
                "    await Task.Delay(100);           // simulate I/O\n" +
                "    return $\"Hello, {name}!\";\n" +
                "}\n" +
                "\n" +
                "var names = new[] { \"Dan\", \"Ada\", \"Grace\" };\n" +
                "\n" +
                "// Run in parallel (like Promise.all)\n" +
                "var tasks = names.Select(FetchGreetingAsync);\n" +
                "var greetings = await Task.WhenAll(tasks);\n" +
                "\n" +
                "foreach (var g in greetings)\n" +
                "{\n" +
                "    Console.WriteLine(g);\n" +
                "}",
            SampleSolution: "",
            ExpectedOutput: "Hello, Dan!\nHello, Ada!\nHello, Grace!\n",
            KeyPoints:
            [
                "`Task<T>` = `Promise<T>`, `Task` = `Promise<void>`",
                "Suffix async methods with `Async` by convention",
                "`Task.WhenAll` = `Promise.all`; `Task.WhenAny` = `Promise.race`",
                "NEVER use `.Result` or `.Wait()` — it's a deadlock footgun; always `await`"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "What's the C# equivalent of TS `Promise<User>`?",
                    Choices: ["Async<User>", "Task<User>", "Future<User>", "Promise<User>"],
                    CorrectIndex: 1,
                    Explanation: "`Task<T>` is .NET's promise type. Async methods return `Task<T>` (or `Task` for void-like)."
                )
            ]
        ),

        new Lesson(
            Id: "12-records",
            Order: 12,
            Title: "Records — Immutable Data Classes",
            Category: "Intermediate",
            TsAnalogy:
                "TS: `type User = { name: string; age: number }` → C#: `record User(string Name, int Age);`\n" +
                "Records give you value-equality, immutability by default, and `with` expressions for non-destructive updates " +
                "(like `{ ...user, age: 31 }` in TS).",
            Explanation:
                "A `record` is a reference type optimized for holding immutable data. In one line you get:\n\n" +
                "• A constructor that takes all parameters\n" +
                "• `init`-only properties for each parameter\n" +
                "• Value-based equality (two records with the same data are `==`)\n" +
                "• A nice `ToString()` implementation\n" +
                "• Support for `with` expressions\n\n" +
                "```csharp\n" +
                "public record User(string Name, int Age);\n" +
                "\n" +
                "var dan = new User(\"Dan\", 30);\n" +
                "var olderDan = dan with { Age = 31 };    // non-destructive update\n" +
                "```\n\n" +
                "Use records for DTOs, API request/response bodies, and value objects. Use classes when you need " +
                "identity (e.g., entities in a database).",
            StarterCode:
                "public record User(string Name, int Age, string Role = \"user\");\n" +
                "\n" +
                "var dan = new User(\"Dan\", 30);\n" +
                "var danBirthday = dan with { Age = 31 };\n" +
                "\n" +
                "Console.WriteLine(dan);\n" +
                "Console.WriteLine(danBirthday);\n" +
                "\n" +
                "// Value equality!\n" +
                "var dan2 = new User(\"Dan\", 30);\n" +
                "Console.WriteLine($\"dan == dan2? {dan == dan2}\");\n" +
                "Console.WriteLine($\"dan == danBirthday? {dan == danBirthday}\");\n" +
                "\n" +
                "// Deconstruction\n" +
                "var (name, age, role) = dan;\n" +
                "Console.WriteLine($\"Deconstructed: {name}, {age}, {role}\");",
            SampleSolution: "",
            ExpectedOutput:
                "User { Name = Dan, Age = 30, Role = user }\nUser { Name = Dan, Age = 31, Role = user }\ndan == dan2? True\ndan == danBirthday? False\nDeconstructed: Dan, 30, user\n",
            KeyPoints:
            [
                "Records give value equality for free (two records with same data are equal)",
                "`with` expressions = spread-and-update without mutation",
                "Records auto-generate a nice `ToString()` for debugging",
                "Use records for DTOs & value objects; use classes for entities with identity"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "What does `dan with { Age = 31 }` do?",
                    Choices: [
                        "Mutates dan.Age to 31",
                        "Creates a new record with Age=31 and all other fields copied from dan",
                        "Deletes dan",
                        "Throws a compile error"
                    ],
                    CorrectIndex: 1,
                    Explanation: "`with` is a non-destructive copy — like `{ ...dan, age: 31 }` in TS. The original `dan` is unchanged."
                )
            ]
        ),

        new Lesson(
            Id: "13-error-handling",
            Order: 13,
            Title: "Exceptions & Error Handling",
            Category: "Intermediate",
            TsAnalogy:
                "TS `try/catch/finally` → C# `try/catch/finally` (nearly identical syntax).\n" +
                "C# has a richer exception hierarchy: `Exception` is the base, with subclasses like " +
                "`ArgumentException`, `InvalidOperationException`, `HttpRequestException`, etc.",
            Explanation:
                "```csharp\n" +
                "try\n" +
                "{\n" +
                "    var result = int.Parse(input);\n" +
                "}\n" +
                "catch (FormatException ex)       // specific exception type\n" +
                "{\n" +
                "    Console.WriteLine($\"Bad format: {ex.Message}\");\n" +
                "}\n" +
                "catch (Exception ex) when (ex.Message.Contains(\"foo\"))   // exception filter\n" +
                "{\n" +
                "    // runs only if the when-clause is true\n" +
                "}\n" +
                "finally\n" +
                "{\n" +
                "    Console.WriteLine(\"Cleanup always runs\");\n" +
                "}\n" +
                "```\n\n" +
                "**Best practices**:\n" +
                "• Catch SPECIFIC exception types, not bare `Exception`\n" +
                "• Use `TryParse` patterns (`int.TryParse(str, out var n)`) instead of catching exceptions for control flow\n" +
                "• Don't swallow exceptions silently\n" +
                "• Throw meaningful exception types: `ArgumentNullException`, `InvalidOperationException`, custom ones",
            StarterCode:
                "string[] inputs = [\"42\", \"abc\", \"-7\", \"\"];\n" +
                "\n" +
                "foreach (var input in inputs)\n" +
                "{\n" +
                "    // The TryParse pattern — no exception for expected failures\n" +
                "    if (int.TryParse(input, out var n))\n" +
                "    {\n" +
                "        Console.WriteLine($\"{input} -> {n}\");\n" +
                "    }\n" +
                "    else\n" +
                "    {\n" +
                "        Console.WriteLine($\"{input} -> invalid\");\n" +
                "    }\n" +
                "}\n" +
                "\n" +
                "// When you DO need to catch an exception:\n" +
                "try\n" +
                "{\n" +
                "    throw new InvalidOperationException(\"Something broke\");\n" +
                "}\n" +
                "catch (InvalidOperationException ex)\n" +
                "{\n" +
                "    Console.WriteLine($\"Caught: {ex.Message}\");\n" +
                "}",
            SampleSolution: "",
            ExpectedOutput:
                "42 -> 42\nabc -> invalid\n-7 -> -7\n -> invalid\nCaught: Something broke\n",
            KeyPoints:
            [
                "Prefer `TryParse`/`TryGetValue`-style APIs over try/catch for expected failures",
                "`out var x` declares a variable inline — super common",
                "Catch specific exception types, not bare `Exception`",
                "`finally` runs regardless of success/failure (cleanup)",
                "Exception filters `catch (Ex ex) when (condition)` are a nice touch"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "What's the idiomatic way to parse an int that might fail?",
                    Choices: [
                        "try { int.Parse(s) } catch { }",
                        "int.TryParse(s, out var n)",
                        "Number(s)",
                        "parseInt(s)"
                    ],
                    CorrectIndex: 1,
                    Explanation: "`TryParse` returns a bool and assigns via `out` — no exception overhead for expected failures."
                )
            ]
        ),

        new Lesson(
            Id: "14-di",
            Order: 14,
            Title: "Dependency Injection",
            Category: ".NET Platform",
            TsAnalogy:
                "If you've used NestJS or Angular, this will feel familiar. .NET has a built-in DI container — " +
                "no need for a third-party library. You register services at startup, and the runtime injects them " +
                "into constructors automatically.",
            Explanation:
                "Register services with one of three lifetimes:\n\n" +
                "• **Singleton** — one instance for the entire app lifetime\n" +
                "• **Scoped** — one instance per HTTP request\n" +
                "• **Transient** — a new instance every time it's resolved\n\n" +
                "```csharp\n" +
                "// In Program.cs\n" +
                "builder.Services.AddSingleton<IClock, SystemClock>();\n" +
                "builder.Services.AddScoped<IUserRepository, UserRepository>();\n" +
                "builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();\n" +
                "```\n\n" +
                "Then in a controller or minimal-API handler, just declare the dependency as a constructor parameter:\n\n" +
                "```csharp\n" +
                "public class UserService(IUserRepository repo, IClock clock)\n" +
                "{\n" +
                "    public async Task<User> CreateAsync(string name) { ... }\n" +
                "}\n" +
                "```\n\n" +
                "(The `ClassName(params)` syntax above is a **primary constructor** — C# 12+.)",
            StarterCode:
                "// This playground runs scripted snippets so we can't wire a full DI container,\n" +
                "// but here's how a primary constructor with injected deps LOOKS:\n" +
                "\n" +
                "public interface IClock { DateTime Now { get; } }\n" +
                "public class SystemClock : IClock { public DateTime Now => DateTime.UtcNow; }\n" +
                "\n" +
                "public class Greeter(IClock clock)\n" +
                "{\n" +
                "    public string Greet(string name) =>\n" +
                "        $\"Hello {name}, the time is {clock.Now:HH:mm:ss}\";\n" +
                "}\n" +
                "\n" +
                "// Manual wiring (what the DI container does automatically)\n" +
                "IClock clock = new SystemClock();\n" +
                "var greeter = new Greeter(clock);\n" +
                "Console.WriteLine(greeter.Greet(\"Dan\"));",
            SampleSolution: "",
            ExpectedOutput: "",   // time-dependent, we'll skip strict checking
            KeyPoints:
            [
                "Three lifetimes: Singleton, Scoped (per-request), Transient (per-resolve)",
                "Register at startup with `builder.Services.Add<Lifetime><Interface, Impl>()`",
                "Constructor injection is the default — just declare the dep as a ctor parameter",
                "Primary constructors (C# 12) let you inline the ctor: `class Foo(IDep d) { ... }`",
                "NEVER new up services manually in production code — rely on the container"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "Which lifetime creates ONE instance per HTTP request?",
                    Choices: ["Singleton", "Scoped", "Transient", "Request"],
                    CorrectIndex: 1,
                    Explanation: "`Scoped` = one instance per scope. In ASP.NET Core, each HTTP request is a scope."
                )
            ]
        ),

        new Lesson(
            Id: "15-minimal-api",
            Order: 15,
            Title: "Minimal APIs — Express for .NET",
            Category: ".NET Platform",
            TsAnalogy:
                "Coming from Express? Minimal APIs are the closest analog:\n\n" +
                "```ts\n" +
                "// Express\n" +
                "app.get('/users/:id', (req, res) => res.json({ id: req.params.id }));\n" +
                "```\n" +
                "```csharp\n" +
                "// Minimal API\n" +
                "app.MapGet(\"/users/{id}\", (int id) => new { Id = id });\n" +
                "```\n" +
                "Routing, binding, and JSON serialization are all built in — no middleware soup.",
            Explanation:
                "Minimal APIs (introduced in .NET 6, polished in .NET 7–10) are the modern way to build HTTP APIs in C#. " +
                "They emphasize low ceremony: a handler is just a lambda or method. Route parameters, query strings, " +
                "JSON bodies, and DI services bind automatically based on their types.\n\n" +
                "```csharp\n" +
                "var builder = WebApplication.CreateBuilder(args);\n" +
                "builder.Services.AddScoped<IUserService, UserService>();\n" +
                "\n" +
                "var app = builder.Build();\n" +
                "\n" +
                "app.MapGet(\"/users/{id:int}\", async (int id, IUserService svc) =>\n" +
                "{\n" +
                "    var user = await svc.GetAsync(id);\n" +
                "    return user is null ? Results.NotFound() : Results.Ok(user);\n" +
                "});\n" +
                "\n" +
                "app.MapPost(\"/users\", async (CreateUserDto dto, IUserService svc) =>\n" +
                "{\n" +
                "    var created = await svc.CreateAsync(dto);\n" +
                "    return Results.Created($\"/users/{created.Id}\", created);\n" +
                "});\n" +
                "\n" +
                "app.Run();\n" +
                "```\n\n" +
                "The binding rules: **route params** come from the URL, **primitives** from query string, **complex types** from the JSON body, **services** from DI.",
            StarterCode:
                "// Minimal APIs need a full WebApplication host which we can't spin up inside\n" +
                "// the scripted playground. But you CAN run your real app alongside this one:\n" +
                "//\n" +
                "//   cd CSharpLearningLab\n" +
                "//   dotnet new webapi -n MyFirstApi --use-minimal-apis\n" +
                "//   cd MyFirstApi && dotnet run\n" +
                "//\n" +
                "// For now, let's simulate the handler signature as a local function:\n" +
                "\n" +
                "string GetUser(int id) => $\"{{ \\\"id\\\": {id}, \\\"name\\\": \\\"user-{id}\\\" }}\";\n" +
                "\n" +
                "Console.WriteLine(\"GET /users/1 ->\");\n" +
                "Console.WriteLine(GetUser(1));\n" +
                "Console.WriteLine();\n" +
                "Console.WriteLine(\"GET /users/42 ->\");\n" +
                "Console.WriteLine(GetUser(42));",
            SampleSolution: "",
            ExpectedOutput:
                "GET /users/1 ->\n{ \"id\": 1, \"name\": \"user-1\" }\n\nGET /users/42 ->\n{ \"id\": 42, \"name\": \"user-42\" }\n",
            KeyPoints:
            [
                "`app.MapGet/Post/Put/Delete` — one line per route",
                "Binding is by type: route params, query strings, JSON body, DI services",
                "`Results.Ok()`, `Results.NotFound()`, `Results.Created()` — typed result helpers",
                "This IS how modern .NET APIs are built — the Controller/Attribute style is older",
                "Try it: `dotnet new webapi -n MyFirstApi && cd MyFirstApi && dotnet run`"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "In a minimal API handler, where does a parameter of a custom class type bind from?",
                    Choices: ["Route", "Query string", "JSON body", "Form data"],
                    CorrectIndex: 2,
                    Explanation: "Complex (non-primitive) types default to binding from the request body as JSON. Primitives default to the query string."
                )
            ]
        ),

        new Lesson(
            Id: "16-ef-core",
            Order: 16,
            Title: "Entity Framework Core (teaser)",
            Category: ".NET Platform",
            TsAnalogy:
                "Coming from Prisma or TypeORM? EF Core fills that slot in .NET. You define C# classes that " +
                "represent your tables, a `DbContext` that ties them together, and you query with LINQ.",
            Explanation:
                "Entity Framework Core is the dominant ORM in .NET. Quick taste:\n\n" +
                "```csharp\n" +
                "// 1. Model class (a 'POCO')\n" +
                "public class Blog\n" +
                "{\n" +
                "    public int Id { get; set; }\n" +
                "    public required string Title { get; set; }\n" +
                "    public List<Post> Posts { get; } = [];\n" +
                "}\n" +
                "\n" +
                "// 2. DbContext\n" +
                "public class BlogDbContext(DbContextOptions<BlogDbContext> opts) : DbContext(opts)\n" +
                "{\n" +
                "    public DbSet<Blog> Blogs => Set<Blog>();\n" +
                "    public DbSet<Post> Posts => Set<Post>();\n" +
                "}\n" +
                "\n" +
                "// 3. Register in Program.cs\n" +
                "builder.Services.AddDbContext<BlogDbContext>(opts =>\n" +
                "    opts.UseSqlite(\"Data Source=blog.db\"));\n" +
                "\n" +
                "// 4. Query with LINQ\n" +
                "var recent = await db.Blogs\n" +
                "    .Include(b => b.Posts)\n" +
                "    .Where(b => b.Posts.Any(p => p.PublishedAt > DateTime.UtcNow.AddDays(-7)))\n" +
                "    .OrderByDescending(b => b.Id)\n" +
                "    .Take(10)\n" +
                "    .ToListAsync();\n" +
                "```\n\n" +
                "Migrations: `dotnet ef migrations add InitialCreate` → `dotnet ef database update`.\n" +
                "We won't run EF inside the scripted playground (it needs a real DB), but this lesson is worth reading through twice — you'll use it in almost every real app.",
            StarterCode:
                "// Simulating what a LINQ-based EF query LOOKS like against in-memory data.\n" +
                "// The actual EF Core query syntax is IDENTICAL — that's the whole point.\n" +
                "\n" +
                "public record Blog(int Id, string Title, DateTime PublishedAt);\n" +
                "\n" +
                "List<Blog> blogs =\n" +
                "[\n" +
                "    new(1, \"Intro to C#\",     DateTime.UtcNow.AddDays(-2)),\n" +
                "    new(2, \"Deep dive LINQ\",  DateTime.UtcNow.AddDays(-10)),\n" +
                "    new(3, \"EF Core tips\",    DateTime.UtcNow.AddDays(-1))\n" +
                "];\n" +
                "\n" +
                "var recent = blogs\n" +
                "    .Where(b => b.PublishedAt > DateTime.UtcNow.AddDays(-7))\n" +
                "    .OrderByDescending(b => b.PublishedAt)\n" +
                "    .Take(10)\n" +
                "    .ToList();\n" +
                "\n" +
                "foreach (var b in recent)\n" +
                "{\n" +
                "    Console.WriteLine($\"{b.Id}: {b.Title}\");\n" +
                "}",
            SampleSolution: "",
            ExpectedOutput: "3: EF Core tips\n1: Intro to C#\n",
            KeyPoints:
            [
                "EF Core queries are LINQ — the same operators you just learned",
                "`DbSet<T>` represents a table; `DbContext` is your unit of work",
                "`Include` is how you eager-load related data (like Prisma `include`)",
                "`ToListAsync`, `FirstOrDefaultAsync`, etc. — always prefer the async variants in real code",
                "Migrations: `dotnet ef migrations add Name` then `dotnet ef database update`"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "What's the EF Core method for eager-loading related entities?",
                    Choices: ["Join", "Include", "With", "Populate"],
                    CorrectIndex: 1,
                    Explanation: "`Include` pulls in related data in the same query — analogous to Prisma's `include`."
                )
            ]
        ),

        new Lesson(
            Id: "17-configuration",
            Order: 17,
            Title: "Configuration & Options",
            Category: ".NET Platform",
            TsAnalogy:
                "Coming from TS, you probably used `dotenv` + `process.env`. .NET has a layered config system " +
                "that reads from `appsettings.json`, `appsettings.{Environment}.json`, environment variables, " +
                "command-line args, user secrets, and more — merged in that order.",
            Explanation:
                "Default sources (last one wins on conflicts):\n" +
                "1. `appsettings.json`\n" +
                "2. `appsettings.Development.json` (or `.Production`, etc.)\n" +
                "3. User Secrets (Development only)\n" +
                "4. Environment variables\n" +
                "5. Command-line args\n\n" +
                "Access via `IConfiguration`, or — better — bind a section to a strongly-typed class:\n\n" +
                "```csharp\n" +
                "// appsettings.json:\n" +
                "// { \"Smtp\": { \"Host\": \"smtp.foo.com\", \"Port\": 587 } }\n" +
                "\n" +
                "public class SmtpOptions { public string Host { get; set; } = \"\"; public int Port { get; set; } }\n" +
                "\n" +
                "builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection(\"Smtp\"));\n" +
                "\n" +
                "// Then inject IOptions<SmtpOptions> anywhere you need it.\n" +
                "public class EmailSender(IOptions<SmtpOptions> opts)\n" +
                "{\n" +
                "    public void Send() => Console.WriteLine($\"Connecting to {opts.Value.Host}:{opts.Value.Port}\");\n" +
                "}\n" +
                "```\n\n" +
                "For secrets in development: `dotnet user-secrets set \"Smtp:Password\" \"hunter2\"`. These are stored " +
                "OUTSIDE your repo in your user profile.",
            StarterCode:
                "// Simulating the Options pattern with a POCO and a manual binding.\n" +
                "\n" +
                "public class SmtpOptions\n" +
                "{\n" +
                "    public string Host { get; set; } = \"\";\n" +
                "    public int Port { get; set; }\n" +
                "}\n" +
                "\n" +
                "// Imagine this came from appsettings.json\n" +
                "var opts = new SmtpOptions { Host = \"smtp.example.com\", Port = 587 };\n" +
                "\n" +
                "Console.WriteLine($\"Would connect to {opts.Host}:{opts.Port}\");",
            SampleSolution: "",
            ExpectedOutput: "Would connect to smtp.example.com:587\n",
            KeyPoints:
            [
                "`appsettings.json` + environment-specific overrides + env vars, merged in order",
                "`IOptions<T>` is the idiomatic way to consume config",
                "Use `dotnet user-secrets` for dev secrets — never commit them",
                "For prod secrets, use Key Vault / AWS Secrets Manager / env vars"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "Where should you store secrets during local development?",
                    Choices: [
                        "appsettings.json",
                        "A .env file in the repo",
                        "User Secrets (dotnet user-secrets)",
                        "Hardcode them and remove before commit"
                    ],
                    CorrectIndex: 2,
                    Explanation: "User Secrets live in your user profile, not the repo. They only load in Development."
                )
            ]
        ),

        new Lesson(
            Id: "18-testing",
            Order: 18,
            Title: "Testing with xUnit",
            Category: ".NET Platform",
            TsAnalogy:
                "Coming from Jest / Vitest? xUnit is the most popular .NET test framework. Key differences:\n\n" +
                "• `[Fact]` = a single test (no parameters)\n" +
                "• `[Theory]` + `[InlineData(...)]` = parameterized test (like `test.each` in Jest)\n" +
                "• Assertions via `Assert.Equal(expected, actual)` — or use FluentAssertions / Shouldly for nicer syntax",
            Explanation:
                "A typical xUnit test file:\n\n" +
                "```csharp\n" +
                "public class CalculatorTests\n" +
                "{\n" +
                "    [Fact]\n" +
                "    public void Add_TwoPositives_ReturnsSum()\n" +
                "    {\n" +
                "        var calc = new Calculator();\n" +
                "        var result = calc.Add(2, 3);\n" +
                "        Assert.Equal(5, result);          // note: (expected, actual)\n" +
                "    }\n" +
                "\n" +
                "    [Theory]\n" +
                "    [InlineData(1, 1, 2)]\n" +
                "    [InlineData(0, 0, 0)]\n" +
                "    [InlineData(-1, 1, 0)]\n" +
                "    public void Add_VariousInputs(int a, int b, int expected)\n" +
                "    {\n" +
                "        var calc = new Calculator();\n" +
                "        Assert.Equal(expected, calc.Add(a, b));\n" +
                "    }\n" +
                "}\n" +
                "```\n\n" +
                "Run tests: `dotnet test`. Setup for integration tests: `WebApplicationFactory<Program>` spins up " +
                "your whole minimal-API app in-memory.\n\n" +
                "**Naming convention** (optional but popular): `MethodName_Scenario_ExpectedResult`.",
            StarterCode:
                "// Simulating a Fact/Theory-style assertion manually.\n" +
                "\n" +
                "static int Add(int a, int b) => a + b;\n" +
                "\n" +
                "void Check(string name, bool condition)\n" +
                "{\n" +
                "    Console.WriteLine(condition ? $\"PASS: {name}\" : $\"FAIL: {name}\");\n" +
                "}\n" +
                "\n" +
                "// Fact\n" +
                "Check(\"2 + 3 = 5\", Add(2, 3) == 5);\n" +
                "\n" +
                "// Theory-style\n" +
                "var cases = new (int a, int b, int expected)[]\n" +
                "{\n" +
                "    (1, 1, 2),\n" +
                "    (0, 0, 0),\n" +
                "    (-1, 1, 0)\n" +
                "};\n" +
                "\n" +
                "foreach (var (a, b, expected) in cases)\n" +
                "{\n" +
                "    Check($\"{a} + {b} = {expected}\", Add(a, b) == expected);\n" +
                "}",
            SampleSolution: "",
            ExpectedOutput:
                "PASS: 2 + 3 = 5\nPASS: 1 + 1 = 2\nPASS: 0 + 0 = 0\nPASS: -1 + 1 = 0\n",
            KeyPoints:
            [
                "`[Fact]` for single tests, `[Theory]` + `[InlineData]` for parameterized",
                "`Assert.Equal(expected, actual)` — **expected comes first**",
                "`dotnet test` runs every test project in the solution",
                "`WebApplicationFactory<Program>` = in-memory integration tests for minimal APIs",
                "Convention: `Method_Scenario_ExpectedResult` (or whatever your team agrees on)"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "In xUnit's `Assert.Equal`, what's the argument order?",
                    Choices: ["(actual, expected)", "(expected, actual)", "Doesn't matter", "(message, actual)"],
                    CorrectIndex: 1,
                    Explanation: "Expected first, actual second. Getting this wrong just produces confusing error messages."
                )
            ]
        ),

        // ================================================================
        // REAL-WORLD .NET — conventions you'll hit in production code
        // ================================================================
        new Lesson(
            Id: "20-modern-csharp",
            Order: 19,
            Title: "Modern C# File Anatomy",
            Category: "Modern C#",
            TsAnalogy:
                "Before you ever read a real .NET codebase, you need to recognize the modern file header. " +
                "The equivalent for a TS dev is: old codebases have `import * as React from \"react\"` at the " +
                "top of every file; modern codebases hoist common imports into a shared ambient module. " +
                "C# did the same thing — `global using` + file-scoped namespaces + implicit usings — and " +
                "every file in a real .NET 6+ codebase looks different from the tutorials.",
            Explanation:
                "A modern .NET 6+ file looks like this:\n\n" +
                "```csharp\n" +
                "// file-scoped namespace — no curly braces, no extra indent\n" +
                "namespace Lobbi.Core.Interfaces;\n" +
                "\n" +
                "/// <summary>XML doc comment — shows up in IntelliSense tooltips.</summary>\n" +
                "public interface IBillingCatalogRepository\n" +
                "{\n" +
                "    Task<ServicePriceRecord?> GetPriceAsync(string serviceId, CancellationToken ct = default);\n" +
                "}\n" +
                "```\n\n" +
                "Three things to notice:\n\n" +
                "1. **File-scoped namespace** (`namespace Foo;`) — C# 10+. Saves a level of indentation.\n" +
                "2. **No `using System;` at the top** — those come from `<ImplicitUsings>enable</ImplicitUsings>` " +
                "in the .csproj, PLUS a file like `GlobalUsings.cs` that declares:\n" +
                "   ```csharp\n" +
                "   global using System;\n" +
                "   global using System.Collections.Generic;\n" +
                "   global using Microsoft.Extensions.Logging;\n" +
                "   ```\n" +
                "   A `global using` is visible from EVERY file in the project — like ambient types in TS.\n\n" +
                "3. **XML doc comments** (`/// <summary>...</summary>`) — these are parsed by the compiler and " +
                "shown in IntelliSense. Use `<see cref=\"Type\"/>` for type links and `<c>code</c>` for inline code.",
            StarterCode:
                "// The playground already has System, System.Linq, etc. globally available,\n" +
                "// which simulates what `<ImplicitUsings>enable</ImplicitUsings>` does in a real .csproj.\n" +
                "\n" +
                "/// <summary>A price record with XML doc comments.</summary>\n" +
                "public sealed record PriceRecord(string ServiceId, decimal Monthly);\n" +
                "\n" +
                "var p = new PriceRecord(\"api-calls\", 29.99m);\n" +
                "Console.WriteLine(p);\n" +
                "Console.WriteLine($\"Monthly: ${p.Monthly:N2}\");",
            SampleSolution: "",
            ExpectedOutput: "PriceRecord { ServiceId = api-calls, Monthly = 29.99 }\nMonthly: $29.99\n",
            KeyPoints:
            [
                "File-scoped namespaces: `namespace Foo;` — no braces, one less indent level",
                "`global using` declares an import once for the whole project — look in `GlobalUsings.cs`",
                "`<ImplicitUsings>enable</ImplicitUsings>` in the .csproj turns on the default set (System, LINQ, etc.)",
                "XML doc comments `/// <summary>` power IntelliSense tooltips — write them on public APIs",
                "`decimal` literals need an `m` suffix: `29.99m`. Without it, it's a `double`"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "What does `global using System.Text.Json;` do?",
                    Choices: [
                        "Imports it only in the current file",
                        "Imports it into every file of the project",
                        "Marks it as a required dependency at runtime",
                        "Nothing — `global` isn't a real keyword"
                    ],
                    CorrectIndex: 1,
                    Explanation: "A `global using` is visible from every file in the compilation. Usually declared in one `GlobalUsings.cs`."
                )
            ]
        ),

        new Lesson(
            Id: "21-required-init",
            Order: 20,
            Title: "`required`, `init`, and Immutable DTOs",
            Category: "Modern C#",
            TsAnalogy:
                "TS: `type Price = { serviceId: string; monthly: number }` — the type system forces you to " +
                "provide both fields at construction. C# needs the `required` keyword (C# 11+) to get the same " +
                "guarantee on properties — otherwise the compiler happily lets you create half-initialized objects. " +
                "You'll see `required` EVERYWHERE in modern .NET codebases; it's how real DTOs are declared.",
            Explanation:
                "Lesson 7 showed you `{ get; set; }` and `{ get; init; }`. The problem with just `init`: the caller " +
                "doesn't HAVE to set it — they'd just get `default` (null for strings, 0 for ints). `required` fixes " +
                "that by making the compiler enforce initialization at construction.\n\n" +
                "```csharp\n" +
                "public sealed record ServicePriceRecord\n" +
                "{\n" +
                "    public Guid Id { get; init; } = Guid.NewGuid();       // has default, optional\n" +
                "    public required string ServiceId { get; init; }       // MUST be set\n" +
                "    public required string Tier { get; init; }            // MUST be set\n" +
                "    public decimal MonthlyPrice { get; init; }\n" +
                "    public DateTimeOffset EffectiveFrom { get; init; } = DateTimeOffset.UtcNow;\n" +
                "    public DateTimeOffset? EffectiveTo { get; init; }     // nullable, optional\n" +
                "}\n" +
                "\n" +
                "// Compiler error if you forget ServiceId or Tier:\n" +
                "var p = new ServicePriceRecord { ServiceId = \"api\", Tier = \"Pro\", MonthlyPrice = 99m };\n" +
                "```\n\n" +
                "Notice the pattern from the Lobbi codebase (`IBillingCatalogRepository.cs`):\n" +
                "• `public sealed record` — reference type, immutable, value equality, can't be subclassed\n" +
                "• `required` on fields that MUST be provided\n" +
                "• Defaults (`= Guid.NewGuid()`, `= DateTimeOffset.UtcNow`) on optional fields\n" +
                "• `DateTimeOffset` (not `DateTime`) — always stores a UTC offset, prevents timezone bugs\n" +
                "• Nullable `DateTimeOffset?` for \"not set yet\"",
            StarterCode:
                "public sealed record ServicePrice\n" +
                "{\n" +
                "    public Guid Id { get; init; } = Guid.NewGuid();\n" +
                "    public required string ServiceId { get; init; }\n" +
                "    public required string Tier { get; init; }\n" +
                "    public decimal MonthlyPrice { get; init; }\n" +
                "    public DateTimeOffset EffectiveFrom { get; init; } = DateTimeOffset.UtcNow;\n" +
                "    public DateTimeOffset? EffectiveTo { get; init; }\n" +
                "}\n" +
                "\n" +
                "var pro = new ServicePrice\n" +
                "{\n" +
                "    ServiceId = \"api-calls\",\n" +
                "    Tier = \"Pro\",\n" +
                "    MonthlyPrice = 99.00m\n" +
                "};\n" +
                "\n" +
                "Console.WriteLine($\"{pro.ServiceId}/{pro.Tier}: ${pro.MonthlyPrice}\");\n" +
                "Console.WriteLine($\"Effective: {pro.EffectiveFrom:yyyy-MM-dd}\");\n" +
                "\n" +
                "// Non-destructive update with `with`\n" +
                "var discounted = pro with { MonthlyPrice = 79.00m };\n" +
                "Console.WriteLine($\"Sale price: ${discounted.MonthlyPrice}\");\n" +
                "\n" +
                "// Try uncommenting this to see the compiler error:\n" +
                "// var broken = new ServicePrice { MonthlyPrice = 50m };",
            SampleSolution: "",
            ExpectedOutput: "",
            KeyPoints:
            [
                "`required` forces the caller to set a property at construction — the C# 11+ way to make DTOs safe",
                "Combine `required string` with nullable `string?` to express \"must set\" vs \"optional\"",
                "`public sealed record` + `init`-only + `required` = the modern immutable DTO recipe",
                "`DateTimeOffset` is almost always better than `DateTime` — it includes the UTC offset",
                "Optional properties get defaults: `= Guid.NewGuid()`, `= DateTimeOffset.UtcNow`, `= []`"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "What happens if you forget to set a `required` property when constructing an object?",
                    Choices: [
                        "It defaults to null/0",
                        "A runtime exception is thrown",
                        "The compiler rejects the code",
                        "Nothing, `required` is just a hint"
                    ],
                    CorrectIndex: 2,
                    Explanation: "`required` is enforced at COMPILE time. The object initializer must include every required member."
                )
            ]
        ),

        new Lesson(
            Id: "22-nullable-refs",
            Order: 21,
            Title: "Nullable Reference Types",
            Category: "Modern C#",
            TsAnalogy:
                "You know TS `strictNullChecks`: `string` can't be null, `string | null` can. C# has the exact " +
                "same feature under a different name: **Nullable Reference Types (NRT)**, toggled by " +
                "`<Nullable>enable</Nullable>` in the .csproj. `string` = non-null; `string?` = nullable. " +
                "The compiler flow-analysis and all the operators (`?.`, `??`, `??=`, `!`) are basically identical.",
            Explanation:
                "With NRT enabled (default in new templates and in this playground), the compiler treats every " +
                "reference type as non-null unless you opt in:\n\n" +
                "```csharp\n" +
                "string name = null;        // ⚠ warning: can't assign null\n" +
                "string? maybeName = null;  // OK, opted in\n" +
                "\n" +
                "int len = maybeName.Length;         // ⚠ warning: possible null dereference\n" +
                "int safe = maybeName?.Length ?? 0;  // OK: ?. and ??\n" +
                "maybeName ??= \"default\";             // assign if null\n" +
                "```\n\n" +
                "**The null-forgiving operator** `!` tells the compiler \"trust me, it's not null here\":\n" +
                "```csharp\n" +
                "var user = await repo.GetAsync(id);\n" +
                "if (user is null) throw new NotFoundException();\n" +
                "Console.WriteLine(user!.Name);   // unnecessary here, flow analysis already knows; use ! sparingly\n" +
                "```\n\n" +
                "**Argument validation**: the modern way is `ArgumentNullException.ThrowIfNull(param)` — " +
                "one line instead of the old `if (param == null) throw new ArgumentNullException(nameof(param));`.\n\n" +
                "**Pattern matching**: `if (x is null)` and `if (x is not null)` are preferred over `== null` " +
                "(they can't be overridden by `operator ==` shenanigans).",
            StarterCode:
                "string FindUser(int id)\n" +
                "{\n" +
                "    ArgumentOutOfRangeException.ThrowIfNegative(id);\n" +
                "    return id == 1 ? \"Dan\" : null!;   // simulating a missing result with !\n" +
                "}\n" +
                "\n" +
                "string? maybe = FindUser(2);\n" +
                "\n" +
                "// Null-safe chain + coalesce\n" +
                "var len = maybe?.Length ?? -1;\n" +
                "Console.WriteLine($\"Length: {len}\");\n" +
                "\n" +
                "// ??= assigns only if null\n" +
                "maybe ??= \"anonymous\";\n" +
                "Console.WriteLine($\"Name: {maybe}\");\n" +
                "\n" +
                "// Pattern: `is null` / `is not null`\n" +
                "object? payload = \"hello\";\n" +
                "if (payload is not null)\n" +
                "{\n" +
                "    Console.WriteLine($\"Got payload: {payload}\");\n" +
                "}\n" +
                "\n" +
                "// ArgumentNullException.ThrowIfNull — one-liner guard\n" +
                "void Print(string text)\n" +
                "{\n" +
                "    ArgumentNullException.ThrowIfNull(text);\n" +
                "    Console.WriteLine(text);\n" +
                "}\n" +
                "Print(\"Hi!\");",
            SampleSolution: "",
            ExpectedOutput: "Length: -1\nName: anonymous\nGot payload: hello\nHi!\n",
            KeyPoints:
            [
                "`string` = non-null; `string?` = nullable. Enable with `<Nullable>enable</Nullable>`",
                "`?.` null-safe access, `??` null-coalesce, `??=` coalesce-assign — all same as TS",
                "Use `is null` / `is not null` instead of `== null` (can't be overridden)",
                "`ArgumentNullException.ThrowIfNull(x)` replaces the old 5-line null check",
                "`!` is the \"trust me\" escape hatch — use it sparingly; it's a smell in most places"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "What does `ArgumentNullException.ThrowIfNull(user)` do?",
                    Choices: [
                        "Prints a warning if user is null",
                        "Throws ArgumentNullException with parameter name \"user\" if user is null",
                        "Returns a default User",
                        "Is the same as `user!`"
                    ],
                    CorrectIndex: 1,
                    Explanation: "It's a one-line guard. The parameter name is captured automatically via [CallerArgumentExpression]."
                )
            ]
        ),

        new Lesson(
            Id: "23-cancellation-tokens",
            Order: 22,
            Title: "CancellationToken — The Async Convention",
            Category: "Modern C#",
            TsAnalogy:
                "In TS, you'd pass an `AbortSignal` to `fetch()` and maybe check it in long-running work. .NET " +
                "formalizes this: **every async method takes a `CancellationToken` as its LAST parameter, " +
                "defaulted to `default`**. If you don't follow this convention, code review will catch you. " +
                "It's the #1 habit to internalize before contributing to a real .NET codebase.",
            Explanation:
                "The convention — look at any interface in the Lobbi codebase:\n\n" +
                "```csharp\n" +
                "public interface IBillingCatalogRepository\n" +
                "{\n" +
                "    Task<ServicePriceRecord?> GetPriceAsync(\n" +
                "        string serviceId,\n" +
                "        string tier,\n" +
                "        CancellationToken ct = default);       // ← ALWAYS last, always `default`\n" +
                "\n" +
                "    Task<IReadOnlyList<ServicePriceRecord>> ListPricesAsync(CancellationToken ct = default);\n" +
                "}\n" +
                "```\n\n" +
                "**You do three things with a token:**\n" +
                "1. **Pass it down** to every inner async call: `await db.SaveChangesAsync(ct);`\n" +
                "2. **Check it** in long loops: `ct.ThrowIfCancellationRequested();`\n" +
                "3. **Hand it to `Task.Delay`**, `HttpClient.GetAsync`, etc. so they bail immediately\n\n" +
                "ASP.NET Core **automatically** binds a `CancellationToken` parameter in endpoint handlers to " +
                "the HTTP request's abort token — if the client hangs up, the token fires and your DB query " +
                "cancels itself. Free correctness, but ONLY if you plumbed it through.\n\n" +
                "**Creating your own token** (e.g., a 5-second timeout):\n" +
                "```csharp\n" +
                "using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));\n" +
                "await DoWorkAsync(cts.Token);   // throws OperationCanceledException after 5s\n" +
                "```\n\n" +
                "Catch `OperationCanceledException` specifically — don't swallow it as a generic error.",
            StarterCode:
                "async Task<int> CountUpAsync(int target, CancellationToken ct = default)\n" +
                "{\n" +
                "    int count = 0;\n" +
                "    for (int i = 0; i < target; i++)\n" +
                "    {\n" +
                "        ct.ThrowIfCancellationRequested();   // bail early if asked\n" +
                "        await Task.Delay(50, ct);             // pass the token DOWN\n" +
                "        count++;\n" +
                "    }\n" +
                "    return count;\n" +
                "}\n" +
                "\n" +
                "// Wrapping the demo in a local async function so `using var` is inside a method scope.\n" +
                "async Task RunDemoAsync()\n" +
                "{\n" +
                "    // Create a 150ms budget, then call — should be cancelled partway through\n" +
                "    using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(150));\n" +
                "\n" +
                "    try\n" +
                "    {\n" +
                "        var result = await CountUpAsync(100, cts.Token);\n" +
                "        Console.WriteLine($\"Finished: {result}\");\n" +
                "    }\n" +
                "    catch (OperationCanceledException)\n" +
                "    {\n" +
                "        Console.WriteLine(\"Cancelled before completing\");\n" +
                "    }\n" +
                "}\n" +
                "\n" +
                "await RunDemoAsync();",
            SampleSolution: "",
            ExpectedOutput: "Cancelled before completing\n",
            KeyPoints:
            [
                "**Every** async method takes `CancellationToken ct = default` as its LAST parameter",
                "Pass the token down to EVERY inner async call — don't drop it",
                "`ct.ThrowIfCancellationRequested()` inside loops so you bail fast",
                "ASP.NET Core auto-binds `CancellationToken` in handlers to the request's abort token",
                "`CancellationTokenSource(TimeSpan)` gives you a timeout token — `using` it so it disposes",
                "Catch `OperationCanceledException` specifically, not bare `Exception`"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "Where does the `CancellationToken` parameter go in an async method signature?",
                    Choices: ["First", "Anywhere", "Last, with `= default`", "It's a class field, not a parameter"],
                    CorrectIndex: 2,
                    Explanation: "Last parameter, defaulted to `default`. This lets callers omit it when they don't care, but always have the option."
                )
            ]
        ),

        new Lesson(
            Id: "24-sealed-readonly",
            Order: 23,
            Title: "`sealed`, `readonly`, and Design Intent",
            Category: "Modern C#",
            TsAnalogy:
                "TS has `readonly` on fields and `Readonly<T>`, but no real equivalent to `sealed`. In C#, " +
                "`sealed` says \"this class cannot be subclassed\" (like TS `final` in some proposals). " +
                "In well-written .NET code you'll see `public sealed class` on nearly every concrete class — " +
                "the opposite of Java's \"everything is extensible\" culture. Lobbi seals aggressively.",
            Explanation:
                "**`sealed`** = no further inheritance allowed.\n" +
                "```csharp\n" +
                "public sealed class AuditTrailService : IAuditTrailService { ... }\n" +
                "public sealed record ServicePriceRecord { ... }\n" +
                "```\n" +
                "Why seal?\n" +
                "• **Intent**: \"this class is complete, don't subclass it\"\n" +
                "• **Performance**: the JIT can devirtualize method calls (virtual dispatch → direct call)\n" +
                "• **Security**: a sealed class can't be subclassed to leak internal state\n" +
                "• **Rule of thumb**: default to sealed, unseal only when you need a base class\n\n" +
                "**`readonly`** on a field means it can only be assigned in the declaration or in a constructor:\n" +
                "```csharp\n" +
                "public sealed class AuditTrailService\n" +
                "{\n" +
                "    private readonly IAuditRepository _repo;     // set once, in ctor\n" +
                "    private readonly ILogger<AuditTrailService> _logger;\n" +
                "\n" +
                "    public AuditTrailService(IAuditRepository repo, ILogger<AuditTrailService> logger)\n" +
                "    {\n" +
                "        _repo = repo;\n" +
                "        _logger = logger;\n" +
                "    }\n" +
                "}\n" +
                "```\n" +
                "**Convention**: private fields are prefixed with `_` and are `readonly` unless they need mutation. " +
                "This is THE pattern for dependencies in every class you'll see.\n\n" +
                "**`readonly struct`** and **`record struct`** are value-type variants — lighter weight for small " +
                "data types. Don't chase them until you profile.\n\n" +
                "**`static readonly`** vs **`const`**:\n" +
                "• `const` — baked into the IL at compile time, must be a primitive or string\n" +
                "• `static readonly` — initialized at runtime, can be any type",
            StarterCode:
                "public interface IClock\n" +
                "{\n" +
                "    DateTimeOffset UtcNow { get; }\n" +
                "}\n" +
                "\n" +
                "public sealed class SystemClock : IClock\n" +
                "{\n" +
                "    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;\n" +
                "}\n" +
                "\n" +
                "public sealed class AuditLog\n" +
                "{\n" +
                "    // Underscore prefix + readonly is the idiomatic dependency-field pattern\n" +
                "    private readonly IClock _clock;\n" +
                "    private readonly List<string> _entries = [];\n" +
                "\n" +
                "    public AuditLog(IClock clock)\n" +
                "    {\n" +
                "        _clock = clock;\n" +
                "    }\n" +
                "\n" +
                "    public void Record(string message)\n" +
                "    {\n" +
                "        _entries.Add($\"[{_clock.UtcNow:HH:mm:ss}] {message}\");\n" +
                "    }\n" +
                "\n" +
                "    public IReadOnlyList<string> Entries => _entries.AsReadOnly();\n" +
                "}\n" +
                "\n" +
                "var log = new AuditLog(new SystemClock());\n" +
                "log.Record(\"user logged in\");\n" +
                "log.Record(\"profile updated\");\n" +
                "\n" +
                "foreach (var entry in log.Entries)\n" +
                "    Console.WriteLine(entry);",
            SampleSolution: "",
            ExpectedOutput: "",
            KeyPoints:
            [
                "Default to `public sealed class` — unseal only when inheritance is explicitly intended",
                "Private dependency fields: `private readonly IRepository _repo;` (underscore prefix)",
                "Expose lists as `IReadOnlyList<T>` via `.AsReadOnly()` to prevent outside mutation",
                "`const` = compile-time constant (strings/primitives only); `static readonly` = runtime constant, any type",
                "JIT perf bonus for sealed classes via devirtualization — not why you seal, but a nice win"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "What does the underscore prefix `_repo` on a field mean in idiomatic C#?",
                    Choices: [
                        "Private field convention (usually paired with `readonly`)",
                        "A reserved keyword",
                        "A public property",
                        "Nothing, it's a typo"
                    ],
                    CorrectIndex: 0,
                    Explanation: "`_camelCase` is the community convention for private fields, and `readonly` + underscore is THE pattern for injected dependencies."
                )
            ]
        ),

        new Lesson(
            Id: "25-controllers",
            Order: 24,
            Title: "Controllers & ActionResults",
            Category: "Web API",
            TsAnalogy:
                "Lesson 15 showed you Minimal APIs, but a lot of real .NET codebases (including Lobbi) still use " +
                "**Controllers** — more structure, class-based, closer to NestJS or Spring. Knowing both styles is " +
                "non-negotiable because you'll see them side-by-side.\n\n" +
                "TS/NestJS: `@Controller(), @Get(), @Post(), @Body(), @Param()` decorators.\n" +
                "C#/MVC: `[ApiController], [Route], [HttpGet], [HttpPost], [FromBody], [FromRoute]` attributes.",
            Explanation:
                "A standard controller:\n\n" +
                "```csharp\n" +
                "[ApiController]\n" +
                "[Route(\"api/[controller]\")]        // => /api/prices\n" +
                "public sealed class PricesController : ControllerBase\n" +
                "{\n" +
                "    private readonly IBillingCatalogRepository _repo;\n" +
                "\n" +
                "    public PricesController(IBillingCatalogRepository repo)\n" +
                "    {\n" +
                "        _repo = repo;\n" +
                "    }\n" +
                "\n" +
                "    [HttpGet(\"{serviceId}/{tier}\")]\n" +
                "    [ProducesResponseType<ServicePriceRecord>(StatusCodes.Status200OK)]\n" +
                "    [ProducesResponseType(StatusCodes.Status404NotFound)]\n" +
                "    public async Task<IActionResult> Get(\n" +
                "        string serviceId,\n" +
                "        string tier,\n" +
                "        CancellationToken ct)\n" +
                "    {\n" +
                "        var price = await _repo.GetPriceAsync(serviceId, tier, ct);\n" +
                "        return price is null ? NotFound() : Ok(price);\n" +
                "    }\n" +
                "\n" +
                "    [HttpPost]\n" +
                "    public async Task<IActionResult> Create(\n" +
                "        [FromBody] ServicePriceRecord price,\n" +
                "        CancellationToken ct)\n" +
                "    {\n" +
                "        await _repo.UpsertPriceAsync(price, ct);\n" +
                "        return CreatedAtAction(nameof(Get),\n" +
                "            new { serviceId = price.ServiceId, tier = price.Tier }, price);\n" +
                "    }\n" +
                "}\n" +
                "```\n\n" +
                "**Key bits:**\n" +
                "• `[ApiController]` — turns on auto-model-validation and conventional 400 responses\n" +
                "• `[Route(\"api/[controller]\")]` — `[controller]` expands to the class name minus `Controller`\n" +
                "• Inherit from `ControllerBase` (not `Controller`) for pure API controllers — no view support\n" +
                "• **Helpers** on `ControllerBase`: `Ok()`, `NotFound()`, `BadRequest()`, `CreatedAtAction()`, `NoContent()`\n" +
                "• `[FromBody]`, `[FromRoute]`, `[FromQuery]` override the default binding source if ambiguous\n" +
                "• **`ProblemDetails`** (RFC 9457) is the standard error response shape — `Problem(\"message\")` returns one\n\n" +
                "**Registration in `Program.cs`**: `builder.Services.AddControllers();` then `app.MapControllers();`.",
            StarterCode:
                "// Simulating a controller's method body without the actual ASP.NET host.\n" +
                "// The PATTERNS here are identical to what you'd write in a real controller.\n" +
                "\n" +
                "public sealed record PriceDto(string ServiceId, string Tier, decimal Monthly);\n" +
                "\n" +
                "public sealed class PriceRepo\n" +
                "{\n" +
                "    private readonly Dictionary<string, PriceDto> _store = new()\n" +
                "    {\n" +
                "        [\"api-calls:Pro\"] = new(\"api-calls\", \"Pro\", 99m)\n" +
                "    };\n" +
                "\n" +
                "    public Task<PriceDto?> GetAsync(string serviceId, string tier, CancellationToken ct = default)\n" +
                "    {\n" +
                "        _store.TryGetValue($\"{serviceId}:{tier}\", out var p);\n" +
                "        return Task.FromResult(p);\n" +
                "    }\n" +
                "}\n" +
                "\n" +
                "// This method body is what would go inside your controller action:\n" +
                "async Task<string> GetPrice(PriceRepo repo, string serviceId, string tier, CancellationToken ct)\n" +
                "{\n" +
                "    var price = await repo.GetAsync(serviceId, tier, ct);\n" +
                "    return price is null\n" +
                "        ? \"404 Not Found\"\n" +
                "        : $\"200 OK: {price}\";\n" +
                "}\n" +
                "\n" +
                "var repo = new PriceRepo();\n" +
                "Console.WriteLine(await GetPrice(repo, \"api-calls\", \"Pro\", default));\n" +
                "Console.WriteLine(await GetPrice(repo, \"api-calls\", \"Enterprise\", default));",
            SampleSolution: "",
            ExpectedOutput:
                "200 OK: PriceDto { ServiceId = api-calls, Tier = Pro, Monthly = 99 }\n404 Not Found\n",
            KeyPoints:
            [
                "`[ApiController]` + `ControllerBase` is the pure-API setup (no razor views)",
                "`[Route(\"api/[controller]\")]` — `[controller]` is the class name minus `Controller`",
                "`Ok()`, `NotFound()`, `BadRequest()`, `CreatedAtAction()` are helpers on `ControllerBase`",
                "Bind sources: `[FromBody]`, `[FromRoute]`, `[FromQuery]`, `[FromHeader]`, `[FromServices]`",
                "Use `ProblemDetails` / `Problem(...)` for error responses — RFC 9457",
                "Register with `builder.Services.AddControllers()` + `app.MapControllers()`"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "Which base class should a pure Web API controller inherit from?",
                    Choices: ["Controller", "ControllerBase", "ApiController", "WebController"],
                    CorrectIndex: 1,
                    Explanation: "`ControllerBase` has all the API helpers but no view support (no Razor). `Controller` adds MVC view rendering you don't need for a JSON API."
                )
            ]
        ),

        new Lesson(
            Id: "26-di-extensions",
            Order: 25,
            Title: "DI Registration Extension Methods",
            Category: ".NET Platform",
            TsAnalogy:
                "In NestJS you have `@Module({ providers: [...] })` bundling a feature's providers. In .NET the " +
                "equivalent convention is an **extension method** on `IServiceCollection` that registers all of a " +
                "module's services in one line: `services.AddLobbiIdentity(configSection);`. This is THE pattern for " +
                "organizing DI in a modular monolith — every service library exports one.",
            Explanation:
                "An **extension method** lets you \"add a method\" to a type you don't own. Syntax: `static` class, " +
                "`static` method, first parameter prefixed with `this`.\n\n" +
                "```csharp\n" +
                "public static class ServiceRegistration\n" +
                "{\n" +
                "    public static IServiceCollection AddLobbiIdentity(\n" +
                "        this IServiceCollection services,                  // ← `this` = extension\n" +
                "        Action<IdentityOptions>? configure = null)\n" +
                "    {\n" +
                "        services.AddOptions<IdentityOptions>()\n" +
                "            .BindConfiguration(IdentityOptions.SectionName)\n" +
                "            .ValidateOnStart();\n" +
                "\n" +
                "        if (configure is not null)\n" +
                "            services.Configure(configure);\n" +
                "\n" +
                "        services.AddHttpContextAccessor();\n" +
                "        services.AddMemoryCache();\n" +
                "\n" +
                "        // TryAdd = register only if not already registered — safe for library code\n" +
                "        services.TryAddScoped<IIdentityService, IdentityService>();\n" +
                "        services.TryAddScoped<IUserStore, GraphUserStore>();\n" +
                "\n" +
                "        return services;         // fluent: allows chaining\n" +
                "    }\n" +
                "}\n" +
                "```\n\n" +
                "Now a consumer just writes:\n" +
                "```csharp\n" +
                "builder.Services\n" +
                "    .AddLobbiIdentity(builder.Configuration.GetSection(\"Identity\"))\n" +
                "    .AddLobbiAuditTrail()\n" +
                "    .AddLobbiBilling();\n" +
                "```\n\n" +
                "**Key conventions:**\n" +
                "• Class named `ServiceRegistration` or `ServiceCollectionExtensions`\n" +
                "• Method named `Add{Module}`\n" +
                "• Returns `IServiceCollection` so you can chain\n" +
                "• Accepts an optional `Action<Options>` or `IConfigurationSection` to configure the module\n" +
                "• Uses `TryAdd*` so callers can override default registrations if they need to",
            StarterCode:
                "public interface IClock { DateTimeOffset UtcNow { get; } }\n" +
                "public sealed class SystemClock : IClock { public DateTimeOffset UtcNow => DateTimeOffset.UtcNow; }\n" +
                "\n" +
                "public sealed class MiniContainer\n" +
                "{\n" +
                "    private readonly Dictionary<Type, object> _map = new();\n" +
                "    public MiniContainer Add<T>(T impl) where T : notnull\n" +
                "    {\n" +
                "        _map[typeof(T)] = impl;\n" +
                "        return this;\n" +
                "    }\n" +
                "    public T Get<T>() => (T)_map[typeof(T)];\n" +
                "}\n" +
                "\n" +
                "// Extension method on MiniContainer — simulating AddLobbiIdentity\n" +
                "public static class ClockModule\n" +
                "{\n" +
                "    public static MiniContainer AddClock(this MiniContainer container)\n" +
                "    {\n" +
                "        return container.Add<IClock>(new SystemClock());\n" +
                "    }\n" +
                "}\n" +
                "\n" +
                "var container = new MiniContainer().AddClock();\n" +
                "var clock = container.Get<IClock>();\n" +
                "Console.WriteLine($\"Now: {clock.UtcNow:yyyy-MM-dd HH:mm:ss}\");",
            SampleSolution: "",
            ExpectedOutput: "",
            KeyPoints:
            [
                "Extension method syntax: `static class` + `static` method + `this T` as first parameter",
                "Every module exports an `Add{Module}(this IServiceCollection services, ...)` method",
                "Return `IServiceCollection` so consumers can chain `.AddX().AddY().AddZ()`",
                "`TryAdd*` variants only register if nothing else already has — polite for library code",
                "This is how you keep `Program.cs` short in a modular monolith — each feature is one line"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "What does the `this` keyword on the first parameter of a static method do?",
                    Choices: [
                        "Nothing, it's illegal",
                        "Makes the parameter optional",
                        "Declares it as an extension method on that type",
                        "Creates a new instance of the type"
                    ],
                    CorrectIndex: 2,
                    Explanation: "`public static T M(this X x, ...)` lets you call `x.M(...)` as if M were an instance method on X — that's what an extension method is."
                )
            ]
        ),

        new Lesson(
            Id: "27-options-pattern",
            Order: 26,
            Title: "IOptions<T> — Binding, Validation & Variants",
            Category: ".NET Platform",
            TsAnalogy:
                "Lesson 17 introduced `IOptions<T>`. Reality is richer: there are **three** options interfaces " +
                "depending on how dynamic your config is, and a full validation pipeline. Coming from TS, imagine " +
                "`process.env` parsed into a validated Zod schema, injected via DI, with hot-reload support. " +
                "That's the Options pattern.",
            Explanation:
                "**The three variants:**\n\n" +
                "| Interface | Lifetime | Reload support | Use when |\n" +
                "|---|---|---|---|\n" +
                "| `IOptions<T>` | Singleton | Never — frozen at startup | 99% of cases |\n" +
                "| `IOptionsSnapshot<T>` | Scoped (per request) | Re-reads per request | You change config per-request |\n" +
                "| `IOptionsMonitor<T>` | Singleton | Live push on change + callback | Long-lived singletons that need updates |\n" +
                "\n" +
                "**Binding + validation** (the Lobbi pattern):\n" +
                "```csharp\n" +
                "// 1. Your options class — plain POCO with `required` where appropriate\n" +
                "public sealed class IdentityOptions\n" +
                "{\n" +
                "    public const string SectionName = \"Identity\";\n" +
                "\n" +
                "    public string TenantId { get; set; } = \"\";\n" +
                "    public string ClientId { get; set; } = \"\";\n" +
                "    public string ClientSecret { get; set; } = \"\";\n" +
                "    public string? ConnectionString { get; set; }\n" +
                "\n" +
                "    public void Validate()\n" +
                "    {\n" +
                "        if (string.IsNullOrWhiteSpace(TenantId))\n" +
                "            throw new InvalidOperationException(\"Identity:TenantId is required\");\n" +
                "    }\n" +
                "}\n" +
                "\n" +
                "// 2. Register with bind + validation + fail-fast at startup\n" +
                "services.AddOptions<IdentityOptions>()\n" +
                "    .BindConfiguration(IdentityOptions.SectionName)   // reads \"Identity\" section\n" +
                "    .Validate(o => { o.Validate(); return true; }, \"Identity options invalid\")\n" +
                "    .ValidateOnStart();                                 // fails at startup, not first use\n" +
                "\n" +
                "// 3. Consume\n" +
                "public sealed class IdentityService(IOptions<IdentityOptions> opts)\n" +
                "{\n" +
                "    private readonly IdentityOptions _opts = opts.Value;\n" +
                "    public string Tenant => _opts.TenantId;\n" +
                "}\n" +
                "```\n\n" +
                "`ValidateOnStart()` is crucial — without it, a misconfigured app starts up fine and only crashes " +
                "when a request happens to touch that service. With it, the app refuses to boot. Pay now or pay later.",
            StarterCode:
                "public sealed class MailOptions\n" +
                "{\n" +
                "    public const string SectionName = \"Mail\";\n" +
                "    public string Host { get; set; } = \"\";\n" +
                "    public int Port { get; set; }\n" +
                "    public string? Username { get; set; }\n" +
                "\n" +
                "    public void Validate()\n" +
                "    {\n" +
                "        if (string.IsNullOrWhiteSpace(Host))\n" +
                "            throw new InvalidOperationException(\"Mail:Host is required\");\n" +
                "        if (Port <= 0)\n" +
                "            throw new InvalidOperationException(\"Mail:Port must be positive\");\n" +
                "    }\n" +
                "}\n" +
                "\n" +
                "// Simulating what AddOptions<T>().Bind(...).Validate(...).ValidateOnStart() does\n" +
                "var opts = new MailOptions { Host = \"smtp.example.com\", Port = 587, Username = \"bot@example.com\" };\n" +
                "opts.Validate();\n" +
                "\n" +
                "Console.WriteLine($\"Host:   {opts.Host}\");\n" +
                "Console.WriteLine($\"Port:   {opts.Port}\");\n" +
                "Console.WriteLine($\"User:   {opts.Username ?? \"(anonymous)\"}\");\n" +
                "\n" +
                "// Try it: a bad config fails fast\n" +
                "try\n" +
                "{\n" +
                "    var bad = new MailOptions { Host = \"\", Port = 0 };\n" +
                "    bad.Validate();\n" +
                "}\n" +
                "catch (InvalidOperationException ex)\n" +
                "{\n" +
                "    Console.WriteLine($\"Startup would fail with: {ex.Message}\");\n" +
                "}",
            SampleSolution: "",
            ExpectedOutput:
                "Host:   smtp.example.com\nPort:   587\nUser:   bot@example.com\nStartup would fail with: Mail:Host is required\n",
            KeyPoints:
            [
                "`IOptions<T>` — singleton, frozen at startup (99% of the time, use this)",
                "`IOptionsSnapshot<T>` — scoped, re-read per request (if you support per-request overrides)",
                "`IOptionsMonitor<T>` — singleton with live updates + change callbacks (long-lived singletons)",
                "`BindConfiguration(\"Section\")` reads appsettings.json automatically",
                "`Validate(...).ValidateOnStart()` — fail-fast on bad config at app boot, NOT first use",
                "Stick a `public const string SectionName = \"...\"` on the options class for DRY binding"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "Which options interface should you inject into a long-lived singleton that needs to react to config changes at runtime?",
                    Choices: ["IOptions<T>", "IOptionsSnapshot<T>", "IOptionsMonitor<T>", "IConfiguration"],
                    CorrectIndex: 2,
                    Explanation: "`IOptionsMonitor<T>` supports live reload + OnChange callbacks; `IOptions<T>` is frozen; `IOptionsSnapshot<T>` can't be injected into singletons at all."
                )
            ]
        ),

        new Lesson(
            Id: "28-logging",
            Order: 27,
            Title: "ILogger<T> & Structured Logging",
            Category: ".NET Platform",
            TsAnalogy:
                "Coming from TS, you probably used `winston`, `pino`, or just `console.log`. .NET has built-in " +
                "structured logging via `ILogger<T>` — injected, scoped per-class, and with a message-template " +
                "format that's better than interpolation. **Critical rule**: don't use string interpolation in log " +
                "messages. Use the message template syntax so log aggregators can index the fields.",
            Explanation:
                "```csharp\n" +
                "public sealed class AuditTrailService(ILogger<AuditTrailService> logger)\n" +
                "{\n" +
                "    private readonly ILogger<AuditTrailService> _logger = logger;\n" +
                "\n" +
                "    public async Task RecordAsync(Guid userId, string action, CancellationToken ct)\n" +
                "    {\n" +
                "        // ✅ RIGHT — message template with named placeholders\n" +
                "        _logger.LogInformation(\n" +
                "            \"User {UserId} performed {Action} at {Timestamp}\",\n" +
                "            userId, action, DateTimeOffset.UtcNow);\n" +
                "\n" +
                "        // ❌ WRONG — string interpolation destroys structure\n" +
                "        _logger.LogInformation($\"User {userId} performed {action}\");\n" +
                "    }\n" +
                "}\n" +
                "```\n\n" +
                "**Why the template matters:** With `\"User {UserId} performed {Action}\"`, Serilog/OpenTelemetry/" +
                "Application Insights store each placeholder as a SEPARATE FIELD. You can query \"all logs where " +
                "Action = 'Delete'\" across billions of records. With interpolation, you get an opaque string blob.\n\n" +
                "**Log levels** (in order of severity):\n" +
                "• `LogTrace` — noise, off in production\n" +
                "• `LogDebug` — dev troubleshooting\n" +
                "• `LogInformation` — normal lifecycle events\n" +
                "• `LogWarning` — unexpected but recoverable\n" +
                "• `LogError` — failed operation\n" +
                "• `LogCritical` — the app is going down\n\n" +
                "**Exceptions**: pass as the first arg: `_logger.LogError(ex, \"Failed to save {UserId}\", userId);`\n\n" +
                "**Scopes** add structured context to every log inside a block:\n" +
                "```csharp\n" +
                "using (_logger.BeginScope(\"TenantId: {TenantId}\", tenantId))\n" +
                "{\n" +
                "    // every log emitted inside here carries TenantId\n" +
                "}\n" +
                "```",
            StarterCode:
                "// Simulating ILogger<T> with a simple interface. The REAL ILogger is injected via DI\n" +
                "// and writes to whatever providers (Console, Serilog, AppInsights) are registered.\n" +
                "\n" +
                "public interface IMiniLogger\n" +
                "{\n" +
                "    void Info(string template, params object[] args);\n" +
                "}\n" +
                "\n" +
                "public sealed class ConsoleMiniLogger : IMiniLogger\n" +
                "{\n" +
                "    public void Info(string template, params object[] args)\n" +
                "    {\n" +
                "        // A real logger records the template AND the args separately so each placeholder\n" +
                "        // becomes a queryable field. Here we just print.\n" +
                "        var formatted = template;\n" +
                "        int i = 0;\n" +
                "        formatted = System.Text.RegularExpressions.Regex.Replace(\n" +
                "            formatted, @\"\\{[^}]+\\}\", _ => args[i++]?.ToString() ?? \"\");\n" +
                "        Console.WriteLine($\"[INFO] {formatted}\");\n" +
                "    }\n" +
                "}\n" +
                "\n" +
                "IMiniLogger logger = new ConsoleMiniLogger();\n" +
                "\n" +
                "// ✅ Message template with named placeholders — this is the idiomatic pattern\n" +
                "logger.Info(\"User {UserId} performed {Action}\", Guid.NewGuid(), \"Login\");\n" +
                "logger.Info(\"Price updated: {ServiceId}/{Tier} -> {Price:C}\", \"api-calls\", \"Pro\", 99m);",
            SampleSolution: "",
            ExpectedOutput: "",
            KeyPoints:
            [
                "Inject `ILogger<ThisClass>` — the generic parameter becomes the log category",
                "**Always** use message templates `{UserId}`, NEVER string interpolation `$\"...{userId}...\"`",
                "Log levels: Trace < Debug < Information < Warning < Error < Critical",
                "Pass exceptions as the FIRST argument: `_logger.LogError(ex, \"failed for {Id}\", id);`",
                "`BeginScope(\"{TenantId}\", tenantId)` adds structured context to every nested log"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "Why use `logger.LogInformation(\"User {UserId} logged in\", id)` instead of `logger.LogInformation($\"User {id} logged in\")`?",
                    Choices: [
                        "Performance — avoids string allocation",
                        "The template preserves structured fields so log aggregators can index/query each placeholder",
                        "It's required by the compiler",
                        "There's no difference"
                    ],
                    CorrectIndex: 1,
                    Explanation: "Both matter (structure is the big one, perf is a bonus), but the structured-field story is why this rule is universal in .NET codebases."
                )
            ]
        ),

        new Lesson(
            Id: "29-disposable",
            Order: 28,
            Title: "IDisposable, IAsyncDisposable & `using`",
            Category: ".NET Platform",
            TsAnalogy:
                "JavaScript has `try/finally` and the new `using` declaration from TC39's Explicit Resource " +
                "Management proposal. C# has had this forever via `IDisposable` + the `using` statement. " +
                "Anything that holds a non-memory resource — file handles, DB connections, HTTP clients, " +
                "timers, streams — implements `IDisposable`. You MUST dispose them.",
            Explanation:
                "The contract: a class that owns unmanaged resources (or wraps one) implements `IDisposable` " +
                "and releases them in `Dispose()`. The `using` statement guarantees `Dispose()` is called even " +
                "if an exception is thrown.\n\n" +
                "**Old style — `using` as a statement with braces:**\n" +
                "```csharp\n" +
                "using (var stream = File.OpenRead(\"data.txt\"))\n" +
                "{\n" +
                "    // stream is disposed when we exit this block\n" +
                "}\n" +
                "```\n\n" +
                "**Modern style — `using` as a declaration (C# 8+):**\n" +
                "```csharp\n" +
                "using var stream = File.OpenRead(\"data.txt\");\n" +
                "// disposed at the end of the ENCLOSING scope (method end usually)\n" +
                "```\n\n" +
                "**`IAsyncDisposable`** — for things that need to flush asynchronously (DB contexts, HTTP response " +
                "bodies). Use `await using`:\n" +
                "```csharp\n" +
                "await using var db = new AppDbContext(options);\n" +
                "var users = await db.Users.ToListAsync(ct);\n" +
                "// DisposeAsync() is awaited at scope exit\n" +
                "```\n\n" +
                "**Implementing `IDisposable`:**\n" +
                "```csharp\n" +
                "public sealed class LeasedResource : IDisposable\n" +
                "{\n" +
                "    private bool _disposed;\n" +
                "\n" +
                "    public void Dispose()\n" +
                "    {\n" +
                "        if (_disposed) return;\n" +
                "        // release resources here\n" +
                "        _disposed = true;\n" +
                "    }\n" +
                "}\n" +
                "```\n\n" +
                "If your class is `sealed`, you don't need the complex `Dispose(bool disposing)` pattern from " +
                "the old docs — just implement `Dispose()` directly. Always **default to sealed**.",
            StarterCode:
                "public sealed class TimerScope : IDisposable\n" +
                "{\n" +
                "    private readonly string _name;\n" +
                "    private readonly System.Diagnostics.Stopwatch _sw;\n" +
                "    private bool _disposed;\n" +
                "\n" +
                "    public TimerScope(string name)\n" +
                "    {\n" +
                "        _name = name;\n" +
                "        _sw = System.Diagnostics.Stopwatch.StartNew();\n" +
                "        Console.WriteLine($\"[START] {_name}\");\n" +
                "    }\n" +
                "\n" +
                "    public void Dispose()\n" +
                "    {\n" +
                "        if (_disposed) return;\n" +
                "        _sw.Stop();\n" +
                "        Console.WriteLine($\"[END]   {_name} took {_sw.ElapsedMilliseconds}ms\");\n" +
                "        _disposed = true;\n" +
                "    }\n" +
                "}\n" +
                "\n" +
                "void DoWork()\n" +
                "{\n" +
                "    using var timer = new TimerScope(\"DoWork\");\n" +
                "    Thread.Sleep(50);\n" +
                "    // timer disposes (prints elapsed) when we exit this method\n" +
                "}\n" +
                "\n" +
                "DoWork();\n" +
                "DoWork();",
            SampleSolution: "",
            ExpectedOutput: "",
            KeyPoints:
            [
                "`IDisposable.Dispose()` releases resources — file handles, DB connections, HTTP clients, streams",
                "`using var x = ...` (C# 8+) is the modern syntax — disposes at end of enclosing scope",
                "`IAsyncDisposable` + `await using` when the cleanup itself is async (DB contexts, network streams)",
                "Sealed classes can implement a simple `Dispose()` without the `Dispose(bool)` legacy pattern",
                "**Rule**: if you didn't create it, you don't dispose it. If you did, you do."
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "What does `using var db = new AppDbContext();` do at the end of the enclosing method?",
                    Choices: [
                        "Nothing",
                        "Calls db.Close()",
                        "Calls db.Dispose() in a finally block",
                        "Forces garbage collection"
                    ],
                    CorrectIndex: 2,
                    Explanation: "The `using` declaration lowers to a try/finally that calls `Dispose()` on scope exit, even if an exception is thrown."
                )
            ]
        ),

        new Lesson(
            Id: "30-hosted-services",
            Order: 29,
            Title: "BackgroundService & HttpClientFactory",
            Category: ".NET Platform",
            TsAnalogy:
                "Two ubiquitous .NET patterns you'll meet immediately:\n\n" +
                "1. **`BackgroundService`** — long-running work (cron jobs, queue consumers, polling). TS equivalent: " +
                "a worker process spawned by BullMQ or a standalone `setInterval` script. In .NET, it's a class that " +
                "runs inside the web host.\n\n" +
                "2. **`HttpClientFactory`** — calling other HTTP APIs. TS equivalent: `fetch` or `axios`. In .NET, " +
                "you **don't** `new HttpClient()` directly — that leaks sockets. You inject `IHttpClientFactory` or " +
                "a typed client.",
            Explanation:
                "**`BackgroundService`** — inherit and override one method:\n\n" +
                "```csharp\n" +
                "public sealed class PriceSyncWorker(IBillingCatalogRepository repo, ILogger<PriceSyncWorker> logger)\n" +
                "    : BackgroundService\n" +
                "{\n" +
                "    protected override async Task ExecuteAsync(CancellationToken stoppingToken)\n" +
                "    {\n" +
                "        while (!stoppingToken.IsCancellationRequested)\n" +
                "        {\n" +
                "            try\n" +
                "            {\n" +
                "                var prices = await repo.ListPricesAsync(stoppingToken);\n" +
                "                logger.LogInformation(\"Synced {Count} prices\", prices.Count);\n" +
                "            }\n" +
                "            catch (Exception ex) when (ex is not OperationCanceledException)\n" +
                "            {\n" +
                "                logger.LogError(ex, \"Sync failed\");\n" +
                "            }\n" +
                "            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);\n" +
                "        }\n" +
                "    }\n" +
                "}\n" +
                "\n" +
                "// Register\n" +
                "builder.Services.AddHostedService<PriceSyncWorker>();\n" +
                "```\n\n" +
                "Key rule: `stoppingToken` fires when the host is shutting down. RESPECT IT or your app won't stop cleanly.\n\n" +
                "**`IHttpClientFactory`** — three usage patterns, from simple to best:\n\n" +
                "```csharp\n" +
                "// 1. Basic - inject IHttpClientFactory and create named clients\n" +
                "builder.Services.AddHttpClient(\"stripe\", c => c.BaseAddress = new Uri(\"https://api.stripe.com\"));\n" +
                "\n" +
                "// 2. Typed client - your own class gets a configured HttpClient injected\n" +
                "builder.Services.AddHttpClient<StripeClient>(c => c.BaseAddress = new Uri(\"https://api.stripe.com\"));\n" +
                "\n" +
                "public sealed class StripeClient(HttpClient http)   // primary constructor\n" +
                "{\n" +
                "    public async Task<Charge?> GetChargeAsync(string id, CancellationToken ct) =>\n" +
                "        await http.GetFromJsonAsync<Charge>($\"/v1/charges/{id}\", ct);\n" +
                "}\n" +
                "```\n\n" +
                "**`IHttpClientFactory` + Polly** (from `Microsoft.Extensions.Http.Resilience`) gives you retry + " +
                "circuit breaker + timeout in one line: `.AddStandardResilienceHandler()`. Never call external APIs " +
                "without it.",
            StarterCode:
                "// Simulating a BackgroundService loop with a short cancellation budget.\n" +
                "\n" +
                "async Task WorkerLoopAsync(CancellationToken stoppingToken)\n" +
                "{\n" +
                "    int tick = 0;\n" +
                "    while (!stoppingToken.IsCancellationRequested)\n" +
                "    {\n" +
                "        tick++;\n" +
                "        Console.WriteLine($\"Tick {tick} at {DateTimeOffset.UtcNow:HH:mm:ss.fff}\");\n" +
                "        try\n" +
                "        {\n" +
                "            await Task.Delay(80, stoppingToken);\n" +
                "        }\n" +
                "        catch (OperationCanceledException)\n" +
                "        {\n" +
                "            Console.WriteLine(\"Stopping token fired — graceful shutdown\");\n" +
                "            return;\n" +
                "        }\n" +
                "    }\n" +
                "}\n" +
                "\n" +
                "// Wrap the demo in a local function so `using var` is inside a method scope.\n" +
                "async Task RunAsync()\n" +
                "{\n" +
                "    // Give the loop a 250ms budget\n" +
                "    using var cts = new CancellationTokenSource(250);\n" +
                "    await WorkerLoopAsync(cts.Token);\n" +
                "    Console.WriteLine(\"Worker exited cleanly\");\n" +
                "}\n" +
                "\n" +
                "await RunAsync();",
            SampleSolution: "",
            ExpectedOutput: "",
            KeyPoints:
            [
                "`BackgroundService` = inherit, override `ExecuteAsync(CancellationToken stoppingToken)`, register with `AddHostedService<T>()`",
                "The `stoppingToken` fires on host shutdown — RESPECT it for graceful shutdown",
                "NEVER `new HttpClient()` in app code — it leaks sockets and causes DNS staleness",
                "Use typed clients: `AddHttpClient<StripeClient>(...)` — HttpClient is injected via primary constructor",
                "Add `.AddStandardResilienceHandler()` (from Microsoft.Extensions.Http.Resilience) for retry + circuit breaker + timeout in one line"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "Why should you inject HttpClient via `AddHttpClient<T>()` instead of `new HttpClient()`?",
                    Choices: [
                        "It's faster",
                        "It pools and reuses underlying handlers, preventing socket exhaustion and DNS staleness",
                        "`new HttpClient()` is a compile error",
                        "No reason, they're equivalent"
                    ],
                    CorrectIndex: 1,
                    Explanation: "`HttpClient` holds sockets. Creating a new one per request leaks sockets under load; caching one forever means DNS never refreshes. `IHttpClientFactory` solves both."
                )
            ]
        ),

        new Lesson(
            Id: "31-nsub-fluentassertions",
            Order: 30,
            Title: "Testing with NSubstitute & FluentAssertions",
            Category: "Testing",
            TsAnalogy:
                "Lesson 18 used plain `Assert.Equal` — fine for basics, but real .NET test suites (including " +
                "Lobbi) use two libraries that make tests much more readable:\n\n" +
                "• **NSubstitute** — the mocking library. TS equivalent: `vi.fn()` / `jest.mock()`.\n" +
                "• **FluentAssertions** — the assertion DSL. TS equivalent: Jest's `expect(x).toBe(y)` chains.",
            Explanation:
                "**NSubstitute basics** — create a substitute for any interface:\n\n" +
                "```csharp\n" +
                "var repo = Substitute.For<IBillingCatalogRepository>();\n" +
                "\n" +
                "// Stub: when called with these args, return this value\n" +
                "repo.GetPriceAsync(\"api-calls\", \"Pro\", Arg.Any<CancellationToken>())\n" +
                "    .Returns(new ServicePriceRecord { ServiceId = \"api-calls\", Tier = \"Pro\", DisplayName=\"API\", MonthlyPrice = 99m });\n" +
                "\n" +
                "// Act\n" +
                "var sut = new PricingService(repo);\n" +
                "var result = await sut.GetMonthlyAsync(\"api-calls\", \"Pro\");\n" +
                "\n" +
                "// Verify a method was called\n" +
                "await repo.Received(1).GetPriceAsync(\"api-calls\", \"Pro\", Arg.Any<CancellationToken>());\n" +
                "```\n\n" +
                "Key NSubstitute bits:\n" +
                "• `Substitute.For<T>()` — creates a fake\n" +
                "• `.Returns(value)` — stubs a return\n" +
                "• `.Received(n)` — asserts it was called n times\n" +
                "• `.DidNotReceive()` — asserts not called\n" +
                "• `Arg.Any<T>()` — any argument matches\n" +
                "• `Arg.Is<T>(x => x.Id > 0)` — predicate match\n\n" +
                "**FluentAssertions** — chainable, reads like English:\n\n" +
                "```csharp\n" +
                "result.Should().Be(99m);\n" +
                "users.Should().HaveCount(3).And.ContainSingle(u => u.Role == \"admin\");\n" +
                "action.Should().Throw<InvalidOperationException>().WithMessage(\"*tenant*\");\n" +
                "price.Should().NotBeNull().And.BeOfType<ServicePriceRecord>();\n" +
                "response.Should().BeEquivalentTo(expected);  // structural equality\n" +
                "```\n\n" +
                "The combo: NSubstitute to mock dependencies, FluentAssertions to verify outcomes. Every class gets " +
                "a `{ClassName}Tests` file, every test follows **AAA** (Arrange / Act / Assert).",
            StarterCode:
                "// Can't load NSubstitute/FluentAssertions inside the scripted playground —\n" +
                "// but we CAN simulate the test structure and assertion style.\n" +
                "\n" +
                "public interface IPriceRepo\n" +
                "{\n" +
                "    Task<decimal?> GetAsync(string id, CancellationToken ct = default);\n" +
                "}\n" +
                "\n" +
                "public sealed class StubPriceRepo : IPriceRepo\n" +
                "{\n" +
                "    private readonly Dictionary<string, decimal> _data;\n" +
                "    public int CallCount { get; private set; }\n" +
                "    public StubPriceRepo(Dictionary<string, decimal> data) => _data = data;\n" +
                "    public Task<decimal?> GetAsync(string id, CancellationToken ct = default)\n" +
                "    {\n" +
                "        CallCount++;\n" +
                "        return Task.FromResult(_data.TryGetValue(id, out var p) ? p : (decimal?)null);\n" +
                "    }\n" +
                "}\n" +
                "\n" +
                "public sealed class PricingService(IPriceRepo repo)\n" +
                "{\n" +
                "    public async Task<decimal> GetOrDefaultAsync(string id, CancellationToken ct = default) =>\n" +
                "        await repo.GetAsync(id, ct) ?? 0m;\n" +
                "}\n" +
                "\n" +
                "// --- TEST: GetOrDefaultAsync_KnownId_ReturnsPrice ---\n" +
                "{\n" +
                "    // Arrange\n" +
                "    var repo = new StubPriceRepo(new() { [\"api\"] = 99m });\n" +
                "    var sut = new PricingService(repo);\n" +
                "\n" +
                "    // Act\n" +
                "    var result = await sut.GetOrDefaultAsync(\"api\");\n" +
                "\n" +
                "    // Assert (FluentAssertions would be: result.Should().Be(99m);)\n" +
                "    Console.WriteLine(result == 99m ? \"PASS: known id\" : $\"FAIL: got {result}\");\n" +
                "    Console.WriteLine(repo.CallCount == 1 ? \"PASS: called once\" : $\"FAIL: called {repo.CallCount}x\");\n" +
                "}\n" +
                "\n" +
                "// --- TEST: GetOrDefaultAsync_UnknownId_ReturnsZero ---\n" +
                "{\n" +
                "    var repo = new StubPriceRepo(new());\n" +
                "    var sut = new PricingService(repo);\n" +
                "    var result = await sut.GetOrDefaultAsync(\"missing\");\n" +
                "    Console.WriteLine(result == 0m ? \"PASS: unknown id\" : $\"FAIL: got {result}\");\n" +
                "}",
            SampleSolution: "",
            ExpectedOutput:
                "PASS: known id\nPASS: called once\nPASS: unknown id\n",
            KeyPoints:
            [
                "`Substitute.For<IFoo>()` — create a fake of any interface, no setup needed",
                "`sub.Method(...).Returns(value)` = stub, `sub.Received(n).Method(...)` = verify",
                "`Arg.Any<T>()` / `Arg.Is<T>(predicate)` — argument matchers",
                "FluentAssertions: `.Should().Be(x)`, `.HaveCount(n)`, `.Throw<T>()`, `.BeEquivalentTo(x)`",
                "AAA pattern: **Arrange** setup + fakes, **Act** call the SUT, **Assert** one logical thing",
                "Name tests `MethodName_Scenario_ExpectedResult` so failures are self-documenting"
            ],
            Quizzes:
            [
                new Quiz(
                    Question: "What does `repo.Received(2).GetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())` assert?",
                    Choices: [
                        "Sets up a stub returning 2",
                        "That `GetAsync` was called exactly twice with any arguments",
                        "That `GetAsync` returned 2 items",
                        "Syntax error"
                    ],
                    CorrectIndex: 1,
                    Explanation: "`Received(n)` is NSubstitute's verification — \"this method should have been called exactly n times with arguments matching these matchers.\""
                )
            ]
        ),

        new Lesson(
            Id: "19-project-challenge",
            Order: 31,
            Title: "Capstone Challenge: Build a TodoApi",
            Category: "Capstone",
            TsAnalogy:
                "Time to leave the playground. This lesson is a roadmap, not runnable code. You'll build a real " +
                "minimal-API todo service with EF Core + SQLite — the .NET version of an Express + Prisma CRUD.",
            Explanation:
                "**Goal**: Build a `TodoApi` with these endpoints:\n\n" +
                "• `GET    /todos`          — list all\n" +
                "• `GET    /todos/{id}`     — get one (404 if missing)\n" +
                "• `POST   /todos`          — create from JSON body\n" +
                "• `PUT    /todos/{id}`     — update\n" +
                "• `DELETE /todos/{id}`     — delete\n\n" +
                "**Steps** (run these in a NEW folder, not inside this playground):\n\n" +
                "```bash\n" +
                "dotnet new webapi -n TodoApi --use-minimal-apis\n" +
                "cd TodoApi\n" +
                "dotnet add package Microsoft.EntityFrameworkCore.Sqlite\n" +
                "dotnet add package Microsoft.EntityFrameworkCore.Design\n" +
                "dotnet tool install --global dotnet-ef            # one-time\n" +
                "\n" +
                "# Write your Todo model, TodoDbContext, and map the endpoints in Program.cs\n" +
                "\n" +
                "dotnet ef migrations add InitialCreate\n" +
                "dotnet ef database update\n" +
                "dotnet run\n" +
                "```\n\n" +
                "**Milestones** (check each off):\n" +
                "1. `Todo` record/class with `Id`, `Title`, `IsDone`, `CreatedAt`\n" +
                "2. `TodoDbContext` with a `DbSet<Todo>`, wired to SQLite via DI\n" +
                "3. Five endpoints mapped in `Program.cs`\n" +
                "4. `Results.NotFound()` for missing IDs; `Results.Created()` for new items\n" +
                "5. An xUnit test project (`dotnet new xunit -n TodoApi.Tests`) with at least one integration test using `WebApplicationFactory<Program>`\n\n" +
                "When you can do this from scratch without looking things up, you're no longer a beginner.",
            StarterCode:
                "// This lesson is a roadmap, not runnable code.\n" +
                "// Open a terminal in a NEW folder outside CSharpLearningLab and run:\n" +
                "//\n" +
                "//   dotnet new webapi -n TodoApi --use-minimal-apis\n" +
                "//\n" +
                "// Then follow the milestones in the explanation panel.\n" +
                "\n" +
                "Console.WriteLine(\"You've got this. Go build it.\");",
            SampleSolution: "",
            ExpectedOutput: "You've got this. Go build it.\n",
            KeyPoints:
            [
                "Real apps combine everything: minimal API + EF Core + DI + async + records + LINQ",
                "Use `dotnet-ef` CLI for migrations",
                "Test with `WebApplicationFactory<Program>` for fast in-memory integration tests",
                "When you can build this from memory, you've internalized the fundamentals"
            ],
            Quizzes: []
        )
    ];
}
