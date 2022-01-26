namespace CDR.Schema
{
    public interface ICdrElementDefinition
    {
        string Path { get; }
        string Name { get; }
        string Parselet { get; }
        string ValueType { get; }
    }

}
