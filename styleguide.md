# Far From Shore - Code Style Guide (WIP)

(c) 2019 Load-Bearing Crabs





## Naming

For functions, the beginning of each word should be written in PascalCase (where the beginning of each word is capitalized). Variables should be camelCased (where the beginning of the first word is lowercase, but others are capitalized).  For example:

```
void GoodFunctionName()
{
    string goodVariableName;
    badFunctionName(BadVariableName);
}
```

Script file names should also be written in PascalCase. (This doesn't apply to other assets, since our team probably won't be creating them.)
```
ThingDoer.cs --good
otherThingDoer.cs --bad
reallycoolthingdoer.cs --HELL FUCKING NO
-----------------------------
boundtothemast.ogg --okay
boat_texture.png --also okay
Guy Person.fbx --still okay
```


# Brackets

Brackets should be written in the standard C# style. For example:

```
//bad
public class Crab : MonoBehaviour {
    public void Start() {
        DoStuff();
    }
}

//good
public class Crab : MonoBehaviour 
{
    public void Start() 
    {
        DoStuff();
    }
}
```

All statements should use brackets, even if they're only one line. For example:
```
//bad
if(player.mutationLevel > 25.0f)
    fear += 50;

//good
if(player.mutationLevel > 25.0f)
{
    fear += 50;
}
    
```

## Indentation
Use four spaces for indentation. (Your IDE or text editor should do this for you, so this rule isn't that important.)

## Comments
**Comment your code liberally.** Since we're a distributed team whose members have many different skill levels, your code may be hard to understand. If you don't think the function of a piece of code is obvious, make sure to comment it.

At the beginning of each file, add some comments that briefly describe what the code does. For example:

```
//Overworld Fish
//Fish that can be caught.
```

## Other
Variables shouldn't be `public`, unless they should be accessible from other scripts. If you need to edit a variable in the inspector, use the `[SerializeField]` modifier instead.

```
public class FishingLine: MonoBehaviour
{
    //bad
    public float ropeWidth = 0.5f;

    //good
    [SerializeField] float ropeWidth = 0.5f;
}

```