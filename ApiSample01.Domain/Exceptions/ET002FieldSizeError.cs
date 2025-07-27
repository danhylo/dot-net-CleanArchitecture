namespace ApiSample01.Domain.Exceptions;

public class ET002FieldSizeError : BaseException
{
    public string FieldName { get; }
    public int Value { get; }
    public string Type { get; }
    public int MaxSize { get; }


    public ET002FieldSizeError(string fieldName, int value, string type, int maxSize, string application)
        : base($"Field size exceeds expected upper or infer limit: Field [{fieldName}], Value [{value}], Type: [{type}], MaxSize: [{maxSize}]")
    {
        FieldName = fieldName;
        Value = value;
        Type = type;
        MaxSize = maxSize;
        Application = application;
    }
}