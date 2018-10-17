(**
- title : Basic concepts of Functional Programming
- description : Basic concepts of Functional Programming
- author : Tomasz Heimowski
- theme : night
- transition : default

***

## F# CAMP

### Basic concepts of Functional Programming

    [lang=bash]
    git clone https://github.com/theimowski/fsharp-workshops-basics.git

or download ZIP from [here](https://github.com/theimowski/fsharp-workshops-basics/archive/master.zip), then in **Command Prompt**:

    [lang=bash]
    cd fsharp-workshops-basics
    .\build.cmd KeepRunning

slides are regenerated when the script (.\slides\index.fsx) is **saved**

---

### Agenda

* Intro - workshop format
* Immutable values
* Expressions
* Pattern matching
* Recursion

***

## Workshop format

---

### Problem to solve
#### Evaluate expression written in [ONP](https://pl.wikipedia.org/wiki/Odwrotna_notacja_polska) notation

Conventional notation:

    ((2+7)/3+(14-3)*4)+3

ONP notation:

    2 7 + 3 / 14 3 - 4 * + 3 +

Parse input ==> Evaluate

---

### New Stuff X.X
#### Something new *)
// code
(**

---

### Example X.X
#### Some example *)
let example = "example"
(** #### Value of ``example`` *)
(*** include-value: ``example`` ***)
(**

---

### Exercise X.X
Exercise:

#### --------------- Your code goes below --------------- *)
let exercise = "exercise"
(** #### Value of ``exercise`` *)
(*** include-value: ``exercise`` ***)
(**

---

### Editors + tooling - demo

* Visual Studio with F# + Visual F# Power Tools
* Visual Studio Code + Ionide extension

http://fsharp.org/use/windows/

* Running code in FSI (FSharp Interactive)
* Regenerating the slides
* **DEMO**: running the code (up to current point) in VS and VS Code


---

### Summary/links go at the end of each part

Most links point to [Scott Wlaschin](https://fsharpforfunandprofit.com) blog

***

## Immutable values

---

### New Stuff 1.1
#### Let bindings *)
let value = 5
(** #### Type inference *)
let stringValue = "Hi there"
let integerValue = 100
(** #### Backticks names *)
let ``I can have spaces`` = "I really do"
(**

---

### Example 1.1
#### Processing an immutable value
``System.String`` is an example of immutable type in .NET *)
let helloWorld = "Hello World from F# program!"
let replaced = helloWorld.Replace("o", "u")
let substring = replaced.Substring(0, replaced.IndexOf("#") + 1)
let ``example 1.1`` = substring.ToLower()
(** #### Value of ``example 1.1`` *)
(*** include-value: ``example 1.1`` ***)
(**

---

### Exercise 1.1
Compound interest: compute earnings after 3 years of depositing $1000 on a 10% annual savings account:

#### --------------- Your code goes below --------------- *)
let initial = 1000.0M
let afterFirstYear = 0
// and so on ...
let ``exercise 1.1`` = 0
(** #### Value of ``exercise 1.1`` *)
(*** include-value: ``exercise 1.1`` ***)
(**



---

### New Stuff 1.2
#### Range operator  *)
let range = [28 .. 38]
(*** include-value: range ***)
(** ---
#### Function declaration and application *)
let add x y =
    x + y

let addResult = add 5 6
(*** include-value: addResult ***)
(** ---
#### Pipe operator *)
let pipeResult =
    10
    |> add 15
    |> add 6
(*** include-value: pipeResult ***)
(** ---
#### map function  *)
let lengths = 
    ["F#"; "is"; "the"; "best"]
    |> List.map (fun s -> s.Length)
(*** include-value: lengths ***)
(**
`map` is an equivalent of `Select()` in C# LINQ


---

### Example 1.2
#### LINQ-like list processing
Note: a new immutable value is created in each computation step

[C# version](http://theimowski.com/fsharp-workshops-intro/#/4/3) *)
let isOdd number =
    number % 2 = 1

let ``example 1.2`` = 
    [2 .. 10]
    |> List.filter isOdd
    |> List.map string
    |> String.concat ";"
(** #### Value of ``example 1.2`` *)
(*** include-value: ``example 1.2`` ***)
(**

---

### Pipe operator - think "Extension methods"

C#

    [lang=csharp]
    public static IEnumerable<'T> Where (
        this IEnumberable<'T> sequence, 
        Func<'T, bool> predicate) { ... }

    public static bool IsOdd (int number) { return number % 2 == 1; }

    numbers.Where (IsOdd)

F#

    [lang=fsharp]
    let filter 
        (predicate : 'a -> bool) 
        (sequence : seq<'a>) = ...

    let isOdd number = number % 2 = 1

    numbers |> filter isOdd

---

### Exercise 1.2
Define `isEven` function of type `int -> bool` and sum even numbers from 2 up to 100.
Hint: Use `List.sum` function

    2 + 4 + 6 + ... + 100

#### --------------- Your code goes below --------------- *)
let ``exercise 1.2a`` = 
    let isEven number = number % 2 = 1
    [1..100] 
    |> List.filter isEven
    |> List.sum 
let ``exercise 1.2`` = 0

(** #### Value of ``exercise 1.2`` *)
(*** include-value: ``exercise 1.2`` ***)
(**





---

### Summary: Immutable Values  

* By **default** values in F# are immutable
* "Pipe" operator (`|>`) is a nice syntactic sugar for writing a sequence of expressions
* Immutable values make it easy to write concurrent code (thread safety for free)

---

### Links

* [F# Values](https://msdn.microsoft.com/en-us/visualfsharpdocs/conceptual/values-%5bfsharp%5d) - MSDN
* [Immutability - Making your code predictable](https://fsharpforfunandprofit.com/posts/correctness-immutability/) by Scott Wlaschin

***

## Expressions



---

### New Stuff 2.1
#### In F# everything is an expression *)
// no statements - `Console.WriteLine` returns `Unit` ()
let writeLine = System.Console.WriteLine "Hello"

// in let bindings, `=` associates symbol with value
let writeLineIsUnit : bool = 
    // but anywhere else, `=` means equality test
    writeLine = ()

(*** include-value: ``writeLineIsUnit`` ***)
(**

---

#### If - then - else expression *)
let day =
    if System.DateTime.Now.DayOfWeek = System.DayOfWeek.Thursday then
        "thursday"
    else
        "some other day of week"
(*** include-value: ``day`` ***)
(**

---

#### Binding values in inside scope *)
let day2 =
    let today = System.DateTime.Now.DayOfWeek 
    if today = System.DayOfWeek.Thursday then
        "thursday"
    else
        "some other day of week"
(*** include-value: ``day2`` ***)
(**

---

#### Option type *)
let workDay =
    let today = System.DateTime.Now.DayOfWeek 
    if today <> System.DayOfWeek.Saturday &&
       today <> System.DayOfWeek.Sunday then
        Some today
    else
        None
(*** include-value: ``workDay`` ***)
(**


---

#### Dot notation, Wrapper functions *)
let length (value : string) =
    value.Length

let lengthOfWord = length "Hello word"
(*** include-value: ``lengthOfWord`` ***)
(**

---


### Example 2.1
#### Parsing boolean value *)
let parseBool (value : string) =
    let lowercase = value.ToLowerInvariant()
    if lowercase = "true" then
        Some true
    elif lowercase = "false" then
        Some false
    else
        None

let ``example 2.1`` = parseBool "True"
(** #### Value of ``example 2.1`` *)
(*** include-value: ``example 2.1`` ***)
(**

---

### Exercise 2.1

Implement `parseNumber` function.
You might find following functions useful: 
`ToCharArray()` (String member), `Array.forall`, `System.Char.IsDigit`, `System.Int32.Parse`.

#### --------------- Your code goes below --------------- *)
let parseNumber (value: string) : int option =
    let onlyDigits =
        value.ToCharArray()
        |> Array.forall System.Char.IsDigit
    if onlyDigits  then
        Some (System.Int32.Parse value)
    else
        None
let parseNumber (value: string) : Option<int> =
    None

let ``exercise 2.1`` = parseNumber "42"
(** #### Value of ``exercise 2.1`` *)
(*** include-value: ``exercise 2.1`` ***)
(**





---

### New Stuff 2.2
#### Array literal *)
let intArray = [|2; 4; 5|]
let rangeArray = [|10..15|]
(*** include-value: ``rangeArray`` ***)
(**

---

### Example 2.2
#### No `return` statements. Last expression is the return value *)
let isPalindrome (value : string) =
    let charList = value.ToCharArray()
    let reversed = value.ToCharArray() |> Array.rev
    charList = reversed // this boolean expression returns value

let ``example 2.2`` = isPalindrome "kajak"
(** #### Value of ``example 2.2`` *)
(*** include-value: ``example 2.2`` ***)
(**

---

### Exercise 2.2
Declare `splitBy` function - a wrapper function arround `Split` method from `String` object. 
Hints: Use `Split` method from `String` and `Array.toList` function to convert array to list type.
#### --------------- Your code goes below --------------- *)
let splitBy (separator : char) (str : string) : string list =
    let parts = str.Split(separator)
    Array.toList parts
let splitBy (separator : char) (str : string) : list<string> =
    []

let ``exercise 2.2`` = 
    "1,3,5,8,10" 
    |> splitBy ',' 
(** #### Value of ``exercise 2.2`` *)
(*** include-value: ``exercise 2.2`` ***)
(**

---



### Summary: Expressions

* There are no statements only expressions in F#
* Therefore no need for `return` keywords - last expression is return value
* `Option` type is the preffered way to model missing values (as opposed to `null`)

---

### Links

* [Expressions and Syntax series](https://fsharpforfunandprofit.com/series/expressions-and-syntax.html) by Scott Wlaschin
* [The Option type - And why it is not null or nullable](https://fsharpforfunandprofit.com/posts/the-option-type/) by Scott Wlaschin

***

## Pattern matching




---

### New Stuff 3.1
#### Discriminated Unions - Empty cases (enum style) *)
type Size = 
    | Small
    | Medium
    | Large
(**

---

#### Discriminated Unions - Complex cases *)
type Shape =
    | Square of edge : float
    // `*` in type declarations stands for tuples
    | Rectangle of width : float * height : float
    | Circle of radius : float

type Result = 
    | Success                // no string needed for success state
    | ErrorMessage of string // error message needed 
(**

---

### Example 3.1
#### Modelling with Discriminated Union Types *)
type FruitType =
    | Banana
    | Apple
    | Grapefruit

type Meal = 
    | Fruit of FruitType
    | Sandwich
    | FastFood of string

let ``example 3.1`` = 
    [Sandwich; FastFood "Bar Żuławski"; Fruit Apple]
(** #### Value of ``example 3.1`` *)
(*** include-value: ``example 3.1`` ***)
(**

---

### Exercise 3.1
Define `Operator` and `Symbol` Discriminated Union Types. 

`Symbol` should use `Operator` as field in one case

#### --------------- Your code goes below --------------- *)
// `Int` is used here only so that the code compiles. 
// Remove it and instead define proper Discriminated Union cases:
// Operator might be one of the following: Plus, Minus, Multiply or Divide
type Operator = 
    | Plus
    | Minus
    | Multiply
    | Divide

// Same as above:
// Symbol might be either a NumSymbol (with int) or OpSymbol (with Operator)
type Symbol = 
    | NumSymbol of int
    | OpSymbol of Operator

type Operator = Int

// Same as above:
// Symbol might be either a NumSymbol (with int) or OpSymbol (with Operator)
type Symbol = Int

(**




---

### New Stuff 3.2
#### Pattern matching expression *)
let formatOptionalValue optionalValue =
    match optionalValue with
    | Some value ->
        "Value: " + value
    | None ->
        "No value at all!"

let formattedValues = 
    [Some "nice string"; None]
    |> List.map formatOptionalValue
(*** include-value: formattedValues ***)            
(**

---

### Example 3.2
#### Calculating area of `Shape` with help of pattern matching *)
let area shape =
    match shape with
    | Square edge -> edge ** 2.0
    | Rectangle (width, height) -> width * height
    | Circle radius -> System.Math.PI * (radius ** 2.0)  

let ``example 3.2`` = area (Circle 10.0)
(** #### Value of ``example 3.2`` *)
(*** include-value: ``example 3.2`` ***)
(**

---

### Exercise 3.2
With help of pattern matching, implement `apply` function.

#### --------------- Your code goes below --------------- *)
let apply (operator : Operator) (left : int) (right : int) : int =
    0

// test the function, e.g. `apply Divide 15 4`
let ``exercise 3.2`` = 0
(** #### Value of ``exercise 3.2`` *)
(*** include-value: ``exercise 3.2`` ***)
(**




---

### New Stuff 3.3
#### Pattern matching strings *)
let patternMatchString value =
    match value with
    | "Dog" -> "Animal"
    | "Cat" -> "Animal"
    | x     -> "Something different"

let matchAnimal = patternMatchString "Chair"
(*** include-value: matchAnimal ***)
(**

---

#### Nested `match` expressions *)
let matchFloatingPoint value =
    match value with
    | "1" -> 1.0
    | "2" -> 2.0
    | x   ->
        match x with
        | "1.0" -> 1.0
        | "2.5" -> 2.5
        | y     -> 0.0
(**

---

### Example 3.3
#### Nested `match` - checking if meal is healthy *)
let isHealthy meal =
    match meal with
    | Fruit _ -> true
    | Sandwich _ -> false
    | FastFood restaurant ->
        // we can further use matched `restaurant` symbol
        match restaurant with
        | "Green Way" -> true
        | _ -> false

let ``example 3.3`` = 
    FastFood "Bar Żuławski" 
    |> isHealthy
(** #### Value of ``example 3.3`` *)
(*** include-value: ``example 3.3`` ***)
(**

---

### Exercise 3.3
Implement `parseSymbol` - try parse all operators first, and then in nested `match` expression use `parseNumber` function 

#### --------------- Your code goes below --------------- *)
let parseSymbol (token : string) : Symbol option =
    match token with
    | "+" -> Some (OpSymbol Plus)
    | "-" -> Some (OpSymbol Minus)
    | "*" -> Some (OpSymbol Multiply)
    | "/" -> Some (OpSymbol Divide)
    | _ -> parseNumber token |> Option.map NumSymbol
           //let parsedNumber = parseNumber token
           //match parsedNumber with
           //| None -> None
           //| Some value -> Some (NumSymbol value) 
let parseSymbol (token : string) : Option<Symbol> =
    None

let ``exercise 3.3`` = List.map parseSymbol ["+"; "/"; "12"; "uups"] 
(** #### Value of ``exercise 3.3`` *)
(*** include-value: ``exercise 3.3`` ***)
(**

--- 

### Helper function "sequenceOpts"

if all elements are Some values, return Some of those values
otherwise if there's at least one None, return None *)

let rec sequenceOpts (optionals: 'a option list) : 'a list option =
    match optionals with
    | [] -> 
        Some []
    | None :: _ ->
        None
    | Some h :: t ->
        sequenceOpts t |> Option.map (fun t -> h :: t)


(** ---

### Exercise 3.4
Implement `parseSymbols`. Useful functions: `List.map`, `sequenceOpts` as well as `splitBy` and `parseSymbol` 

#### --------------- Your code goes below --------------- *)
let parseSymbols (expression: string) : Symbol list option =
    expression
    |> splitBy ' ' 
    |> List.map parseSymbol
    |> sequenceOpts
let parseSymbols (expression: string) : Option<list<Symbol>> =
    None

let ``exercise 3.4`` = "1 2 / +" |> parseSymbols
(** #### Value of ``exercise 3.4`` *)
(*** include-value: ``exercise 3.4`` ***)
(**





---

### Summary: Pattern Matching

* Using Discriminated Unions is a neat way to model data 
* Pattern matching is a powerful and elegant mechanism in F# for "branching" code
* F# compiler warns when it finds unhandled cases in pattern matching

---

### Links

* [Discriminated Unions - Adding types together](https://fsharpforfunandprofit.com/posts/discriminated-unions/) by Scott Wlaschin
* [Pattern matching for conciseness](http://fsharpforfunandprofit.com/posts/match-expression/) by Scott Wlaschin
* [Match expressions - The workhorse of F#](http://fsharpforfunandprofit.com/posts/match-expression/) by Scott Wlaschin
* [Exhaustive pattern matching - A powerful technique to ensure correctness](https://fsharpforfunandprofit.com/posts/correctness-exhaustive-pattern-matching/) by Scott Wlaschin

***

## Recursion





---

### New Stuff 4.1
#### Recursive functions *)
let rec countdown counter =
    match counter with
    | 0 -> ""
    | x -> x.ToString() + ";" + countdown (counter - 1)

let counting = countdown 10
(*** include-value: counting ***)
(**

---

#### Tail-recursive functions with accumulator *)
let rec countdownAcc acc counter =
    match counter with
    | 0 -> acc
    | x -> countdownAcc (acc + ";" + x.ToString()) (counter - 1)
    
let countingAcc = countdownAcc "" 10
(*** include-value: countingAcc ***)
(**

---

#### Factorial
*)
let rec factorial x =
    match x with
    | 1 -> 1
    | _ -> (factorial (x-1)) * x

let factorialOf5 = factorial 5
(*** include-value: factorialOf5 ***)
(**
#### Factorial - with accumulator *)
let rec factorialTail acc x =
    match x with 
    | 1 -> acc
    | _ -> factorialTail (x*acc) (x-1)

let factorialOf5Tail = factorialTail 1 5
(*** include-value: factorialOf5Tail ***)
(**

---

#### Pattern matching lists *)
let rec commaSeparated acc list =
    match list with
    | [] -> acc
    | [single] -> acc + "," + single
    | head :: tail -> commaSeparated (acc + "," + head) tail

let csv = commaSeparated "" ["some";"values";"go";"here"]
(*** include-value: csv ***)
(**

---

#### Pattern in pattern *)
let rec formatOptionalInts acc ints =
    match ints with
    | [] -> acc
    | Some 0 :: rest -> formatOptionalInts (acc + " Zero") rest
    | Some x :: rest -> formatOptionalInts (acc + " " + x.ToString()) rest
    | None   :: rest -> formatOptionalInts (acc + " NoValue!") rest

let optionalInts = formatOptionalInts "" [Some 28; Some 0; None]
(*** include-value: optionalInts ***)
(**


---

### Example 4.1
#### Recursive call with "accumulators" *)
let rec partitionEvenOdd even odd numbers =
    match numbers with
    | [] ->
        (even, odd)
    | h :: tail when h % 2 = 0 ->
        partitionEvenOdd (h :: even) odd tail
    | h :: tail -> // guard here would result in compiler complaining on incomplete pattern matching
        partitionEvenOdd even (h :: odd) tail

let ``example 4.1`` = partitionEvenOdd [] [] [1..10]
(** #### Value of ``example 4.1`` *)
(*** include-value: ``example 4.1`` ***)
(**

---

### Homework 4.1
Implement `compute` function ([Wiki](https://pl.wikipedia.org/wiki/Odwrotna_notacja_polska)). Hint: `::` is "right-associative"

#### --------------- Your code goes below --------------- *)
let rec compute (stack : int list) (symbols : Symbol list) : int option =
    None

// test the function, e.g. `compute [] [NumSymbol 4; NumSymbol 2; OpSymbol Multiply]`
let ``exercise 4.1`` : int option = None
(** #### Value of ``exercise 4.1`` *)
(*** include-value: ``exercise 4.1`` ***)
(**


---

### Homework 4.2
Using `parseSymbols` and `compute`, write `onp` function

#### --------------- Your code goes below --------------- *)
let onp (expression : string) : int option = 
    None

let ``exercise 4.2`` = onp "2 7 + 3 / 14 3 - 4 * + 3 +"
(** #### Value of ``exercise 4.2`` *)
(*** include-value: ``exercise 4.2`` ***)
(**

---

### Summary: Recursion

* Recursion is a preferred way of processing in Functional Programming
* F# provides tail recursion feature for preventing Stack Overflows
* Recursive functions combined with pattern matching result in very concise and declarative code

---

### Links

* [Recursion and Tail-recursion in F#](https://cyanbyfuchsia.wordpress.com/2014/02/12/recursion-and-tail-recursion-in-f/) - Karlkim Suwanmongkol
* [F# Tail Calls](https://blogs.msdn.microsoft.com/fsharpteam/2011/07/08/tail-calls-in-f/) - MSDN

***

## Summary

* Immutable values
* Expressions
* Pattern matching
* Recursion

---

## Next week

### Functional Data Structures
### + Something more?

*)
