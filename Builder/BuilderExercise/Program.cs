using System;
using System.Collections.Generic;

namespace BuilderExercise
{
    public class field
    {
        public string PropertyName;
        public string Type;
    }
    public class Code
    {
        public string CodeClass;
        public List<field> fieldList = new List<field>();

        public override string ToString()
        {
            string Class = $"public class {CodeClass}";
            string Field = "\n{";
            foreach (field field in fieldList)
            {
                Field += $"\n  public {field.Type} {field.PropertyName};";
            }
            Field += "\n}";

            return Class + Field;
        }
    }
    public sealed class CodeBuilder
    {
        // TODO
        public readonly List<Action<Code>> Actions
            = new List<Action<Code>>();

        public CodeBuilder Called(string codeClass)
        {
            Actions.Add(c => { c.CodeClass = codeClass; });
            return this;
        }

        public Code Build()
        {
            var c = new Code();
            Actions.ForEach(a => a(c));
            return c;
        }
    }
    public static class CodeBuilderExtensions
    {
        public static CodeBuilder AddField(this CodeBuilder builder, string type, string property)
        {
            builder.Actions.Add(c =>
            {
                c.fieldList.Add(new field
                {
                    Type = type,
                    PropertyName = property
                });
            });
            return builder;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var cb = new CodeBuilder();
            var code = cb.Called("Person")
                .AddField("string", "Name")
                .AddField("int", "Age")
                .Build();

            Console.WriteLine(code);
        }
    }
}
