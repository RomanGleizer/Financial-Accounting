public class Operation
{
    public readonly int Value;
    public readonly OperationType OperationType;

    public Operation(int value, OperationType type)
    {
        Value = value;
        OperationType = type;
    }
}