namespace MrSolutionTable.Methods;

[Flags]
public enum GeneratorBehavior
{
    Default = 0x0,
    View = 0x1,
    DapperContrib = 0x2,
    Comment = 0x4
}