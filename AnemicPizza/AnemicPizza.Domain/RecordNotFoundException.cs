using System;

namespace AnemicPizza.Core
{
    public class RecordNotFoundException : Exception
    {
        public int RecordId { get; }

        public RecordNotFoundException(int recordId, string typeName) : base($"Record of the type {typeName} of the id {recordId} was not found")
        {
            RecordId = recordId;
        }
    }
}
