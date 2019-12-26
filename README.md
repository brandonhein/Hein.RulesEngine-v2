# Hein.RulesEngine-v2
Version 2 of Hein.RulesEngine

#### How does it work?
Like the [first](https://github.com/brandonhein/Hein.RulesEngine) rules engine attempt, we save rule definitions to DynamoDB.  This rules engine solves the complexity rule creation and execution the first one struggled with.  The downside is, it does require some coding/formating knowledge as it takes a Visual Basic Admin Code approach and translates that into CSharp Engine Code using [CodeConverter](https://github.com/icsharpcode/CodeConverter).  Unlike the first rules engine, complexity of this rule creation allows you to create temporary variables to use. Bonus!  You also don't need to worry about order and rule priority, when invoked it'll apply the dynamic rule against the payload model.


Hierarchy
1. Rule Definition  
a. Entities to help wit Easy Documentation and test setup  
b. One Rule per definition (make it as complex as you want)  

### Pros/Cons
| Pros | Cons |
| --- | --- |
| One rule per definition makes it easy to manage | `CSharpScript` eats up so much memory |
| You can make temporary variables that your complex rule can call during runtime | Super Complex for those that don't know code |
