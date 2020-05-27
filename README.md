Repositório para armazenar anotações e exercícios sendo realizados no curso *Data Access in C# and .NET Core* da edX, o qual aborda principalmente o uso do Entity Framework e LINQ

#### Delegates in C#

A delegate is a reference type that defines a method signature with return type and parameters list types.

- Steps:
1. Declare a delegate
2. Set a target method
3. Invoke the delegate

*[access modifier] delegate [return type] [delegate name]([parameters])*

```csharp
//Step 1: declare a delegate
public delegate void MyDelegate(string msg);
    
//Step 2: set a target method
MyDelegate del = new MyDelegate(MethodA);
// or 
MyDelegate del = MethodA; 
// or set lambda expression 
MyDelegate del = (string msg) =>  Console.WriteLine(msg);
 
// target method
static void MethodA(string message)
{
    Console.WriteLine(message);
}
 
//Step 3: invoke the delegate
del.Invoke("Hello World!");
// or 
del("Hello World!");
```

#### LINQ

- LINQ is a set of techologies used to enable the use of queries directly into the C# language.
- Use System.Linq namespace to use LINQ.
- LINQ API includes two main static class Enumerable & Queryable.
- The static Enumerable class includes extension methods for classes that implements the IEnumerable<T> interface.
- IEnumerable<T> type of collections are in-memory collection like List, Dictionary, SortedList, Queue, HashSet, LinkedList.
- The static Queryable class includes extension methods for classes that implements the IQueryable<T> interface.

There are two ways to write a LINQ query: query and method syntax

1. Query syntax

```
var result = from s in strList
            where s.Contains("Tutorials")
            select s;
```

After the from clause, you can use different Standard Query Operators to filter, group, join elements of the collection. There are around 50 Standard Query Operators available in LINQ. In the above figure, we have used "where" operator (aka clause) followed by a condition.
Query Syntax starts with from clause and can be end with select or groupby clause.

```csharp
// string collection 
IList<string> stringList = new List<string>() { 
"C# Tutorials", 
"VB.NET Tutorials", 
"Learn C++", 
"MVC Tutorials" , 
"Java" 
};

// LINQ Query Syntax 
var result = from s in stringList 
where s.Contains("Tutorials") 
select s;
```

2. Method syntax (or fluent syntax)

- As name suggest, Method Syntax is like calling extension method.
- LINQ Method Syntax has this name because it allows series of extension methods call.

	
#### Anonymous methods in C#

- Anonymous method can be defined using the delegate keyword
- Anonymous method must be assigned to a delegate.
- Anonymous method can be passed as a parameter.

```csharp 
public delegate void Print(int value);
class Program
{
    public static void PrintHelperMethod(Print print, int val)
    { 
        val += 10;
        print(val);
    }
 
    static void Main(string[] args)
    {
        PrintHelperMethod(
            delegate(int val) {Console.WriteLine("Anonymous method: {0}", val);}, 
            100);
    }
}
```

#### Anatomy of the Lambda Expression

```csharp
delegate (Student s) { return s.Age > 12 && s.Age < 20; };
 
//Remove delegate and Parameter type and add lambda operator =>
delegate (Student s) { return s.Age > 12 && s.Age < 20 ;};
 
//The next line is valid but we don’t need { }, semicolon and return
//if we just have one statement
(s) => { return s.Age > 12 && s.Age < 20; } ;
 
//So we use like above
       (s) => return s.Age > 12 && s.Age < 20;
 
	//if there’s just one parameter, we can remove ( )
	s => return s.Age > 12 && s.Age < 20;
 
      //an example with more than one parameter
      (s, youngAge) => s.Age >= youngAge;
 
      //an example with more than one parameter and specification of types
      (Student s, int youngAge) => s.Age >= youngage;
 
     //an example with no parameters
     () => Console.WriteLine("Parameter less lambda expression");
 
     //an example with more than one statements
     (s, youngAge) => {
        Console.WriteLine("Lambda expression with multiple statements in the body");
        return s.Age >= youngAge;
     };
```

#### Func delegate 

```csharp
Func<Student, bool> isStudentTeenager = s => s.age > 12 && s.age < 20;
```


#### Action delegate

An Action type delegate is the same as Func delegate except that the Action delegate doesn't return a value. In other words, an Action delegate can be used with a method that has a void return type.

```csharp
static void ConsolePrint(int i)
{
    Console.WriteLine(i);
}
 
static void Main(string[] args)
{
    Action<int> printActionDel = ConsolePrint; // new Action<int>(ConsolePrint)
    printActionDel(10);
}
```

#### Introduction to Entity Framework

##### Sorting

- A query technique provided through the System.Linq namespace
- We can use them off DbSet objects
- For example, sort by one or more fields in ascending or descending order using OderBy and OrderByDescending.

1. OrderBy method

```csharp
using(var context = new SchoolContext()
{
    // Get all Students from the database ordered by Name
    var orderedStudents = context.Students.OrderBy(s => s.First);
     
    //Display all students' names in ascending order
    foreach (var student in orderedStudents) 
    { 
        Console.WriteLine(student.First + " " + student.Last); 
    } 
}
```

2. OrderByDescending method

```
using(var context = new SchoolContext()
{
    //Get all students from the database ordered by Age
    var orderedStudents = context.Students.OrderByDescending(s => s.Age);
    console.WriteLine("All students in the database:"); 
    //Display all students' names in ascending order
    foreach (var student in orderedStudents) 
    { 
        Console.WriteLine(student.First + " " + student.Last); 
    } 
}
```

3. orderby clause

##### LINQ query

```csharp
using(var context = new SchoolContext()
{
    // Get all Students from the database ordered by Name
    var query = from s in context.Students
        orderby s.Last ascending, student.First ascending
        select s; 
 
    //Display all students' names
    foreach (var student in query) 
    { 
        Console.WriteLine(student.Last + " " + student.First); 
    } 
} 
```

#### Paging

- Used to present large amount of data to users 
- Breaks the data into fixed-size blocks
- Two main methods provided for Entity Framework Core with which we can achieve paging:

1. Enumerable.Skip
2. Skip, SkipLast, SkipWhile (System.Linq)
3. Take (System.Linq)

##### Enumerable.Skip and Take

```csharp
using (var context = new SchoolContext()) {
    // Get all Students from the database starting from the 6th student
    var pagedStudents = context.Students.Skip(5);
    // Get all Students using a filter
    var pagedStudents = context.Students.SkipWhile(s => s.Age >= 18);
    // Get the oldest three students
    var pagedStudents = context.Students.OrderByDescending(s => s.Age).Take(3);
 
    console.WriteLine("All students in the database:"); 
    //Display all students' names starting skipping the first five
    foreach (var student in pagedStudents) 
    { 
        Console.WriteLine(student.First + " " + student.Last); 
    } 
}
```
