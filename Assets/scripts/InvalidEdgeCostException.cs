using System;
using System.Runtime.Serialization;

[Serializable]
internal class InvalidEdgeCostException : Exception
{
    public InvalidEdgeCostException()
    {
    }

    public InvalidEdgeCostException(string message) : base(message)
    {
    }

    public InvalidEdgeCostException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InvalidEdgeCostException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}