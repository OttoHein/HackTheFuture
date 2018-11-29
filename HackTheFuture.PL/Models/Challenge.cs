using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackTheFuture.PL.Models
{
    public class InputValue
    {
        public string Name { get; set; }
        public string Data { get; set; }
    }

    public class Question
    {
        public IList<InputValue> InputValues { get; set; }
    }

    public class Value
    {
        public string Name { get; set; }
        public string Data { get; set; }
    }

    public class Answer
    {
        public string ChallengeId { get; set; }
        public IList<Value> Values { get; set; }
    }

    public class Example
    {
        public Question Question { get; set; }
        public Answer Answer { get; set; }
    }

    public class Challenge
    {
        public string Id { get; set; }
        public string Identifier { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Question Question { get; set; }
        public Example Example { get; set; }
    }

}