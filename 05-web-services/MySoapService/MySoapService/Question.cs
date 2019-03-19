using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MySoapService
{
    // to use a type as return type or parameter type for an operation,
    // it must have [DataContract] attribute.
    [DataContract]
    public class Question
    {
        [DataMember]
        public int QuestionId { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public int Rating { get; set; }

        // without [DataMember] attribute, this property is not serialized/deserialized
        // across the service connection.
        public DateTime DateModified { get; set; }
    }
}
