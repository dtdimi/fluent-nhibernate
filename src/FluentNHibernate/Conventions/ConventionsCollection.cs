using System;
using System.Collections;
using System.Collections.Generic;
using FluentNHibernate.Utils;

namespace FluentNHibernate.Conventions;

public class ConventionsCollection : IEnumerable<Type>
{
    readonly List<AddedConvention> inner = new List<AddedConvention>();
    readonly List<Type> types = new List<Type>();

    public IEnumerator<Type> GetEnumerator()
    {
        return types.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool Contains(Type type)
    {
        return types.Contains(type);
    }

    public void Add<T>(T instance)
    {
        Add(typeof(T), instance);
    }

    public void Add(Type type, object instance)
    {
        AddedConvention convention;

        if (Contains(type))
        {
            convention = inner.Find(x => x.Type == type);
        }
        else
        {
            convention = new AddedConvention(type);
            types.Add(type);
            inner.Add(convention);
        }

        convention.Instances.Add(instance);
    }

    public IEnumerable<object> this[Type type]
    {
        get { return inner.Find(x => x.Type == type).GetInstances(); }
    }

    class AddedConvention(Type type)
    {
        public IList<object> Instances { get; } = new List<object>();
        public Type Type { get; } = type;

        public IEnumerable<object> GetInstances()
        {
            return new List<object>(Instances);
        }
    }

    public void Merge(ConventionsCollection conventions)
    {
        conventions.inner.ForEach(inner.Add);
        conventions.types.ForEach(types.Add);
    }
}
