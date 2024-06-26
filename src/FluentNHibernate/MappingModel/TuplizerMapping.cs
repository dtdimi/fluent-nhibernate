using System;
using System.Linq.Expressions;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel;

[Serializable]
public class TuplizerMapping(AttributeStore attributes) : MappingBase, IEquatable<TuplizerMapping>
{
    readonly AttributeStore attributes = attributes;

    public TuplizerMapping()
        : this(new AttributeStore())
    {}

    public override void AcceptVisitor(IMappingModelVisitor visitor)
    {
        visitor.ProcessTuplizer(this);
    }

    public override bool IsSpecified(string attribute)
    {
        return attributes.IsSpecified(attribute);
    }

    public TuplizerMode Mode => attributes.GetOrDefault<TuplizerMode>();

    public string EntityName => attributes.GetOrDefault<string>();

    public TypeReference Type => attributes.GetOrDefault<TypeReference>();

    public bool Equals(TuplizerMapping other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(other.attributes, attributes);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != typeof(TuplizerMapping)) return false;
        return Equals((TuplizerMapping)obj);
    }

    public override int GetHashCode()
    {
        return (attributes is not null ? attributes.GetHashCode() : 0);
    }

    public void Set<T>(Expression<Func<TuplizerMapping, T>> expression, int layer, T value)
    {
        Set(expression.ToMember().Name, layer, value);
    }

    protected override void Set(string attribute, int layer, object value)
    {
        attributes.Set(attribute, layer, value);
    }
}
