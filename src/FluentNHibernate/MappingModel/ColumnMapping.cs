using System;
using System.Linq.Expressions;
using FluentNHibernate.Utils;
using FluentNHibernate.Visitors;

namespace FluentNHibernate.MappingModel;

[Serializable]
public class ColumnMapping(AttributeStore attributes) : MappingBase, IEquatable<ColumnMapping>
{
    readonly AttributeStore attributes = attributes;

    public ColumnMapping()
        : this(new AttributeStore())
    {}

    public ColumnMapping(string defaultColumnName)
        : this()
    {
        Set(x => x.Name, Layer.Defaults, defaultColumnName);
    }

    public override void AcceptVisitor(IMappingModelVisitor visitor)
    {
        visitor.ProcessColumn(this);
    }

    public Member Member { get; set; }

    public string Name => attributes.GetOrDefault<string>();

    public int Length => attributes.GetOrDefault<int>();

    public bool NotNull => attributes.GetOrDefault<bool>();

    public bool Nullable => !NotNull;

    public bool Unique => attributes.GetOrDefault<bool>();

    public string UniqueKey => attributes.GetOrDefault<string>();

    public string SqlType => attributes.GetOrDefault<string>();

    public string Index => attributes.GetOrDefault<string>();

    public string Check => attributes.GetOrDefault<string>();

    public int Precision => attributes.GetOrDefault<int>();

    public int Scale => attributes.GetOrDefault<int>();

    public string Default => attributes.GetOrDefault<string>();

    public ColumnMapping Clone()
    {
        return new ColumnMapping(attributes.Clone());
    }

    public bool Equals(ColumnMapping other)
    {
        return Equals(other.attributes, attributes) && Equals(other.Member, Member);
    }

    public override bool Equals(object obj)
    {
        if (obj.GetType() != typeof(ColumnMapping)) return false;
        return Equals((ColumnMapping)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return ((attributes is not null ? attributes.GetHashCode() : 0) * 397) ^ (Member is not null ? Member.GetHashCode() : 0);
        }
    }

    public void Set<T>(Expression<Func<ColumnMapping, T>> expression, int layer, T value)
    {
        Set(expression.ToMember().Name, layer, value);
    }

    protected override void Set(string attribute, int layer, object value)
    {
        attributes.Set(attribute, layer, value);
    }

    public override bool IsSpecified(string attribute)
    {
        return attributes.IsSpecified(attribute);
    }

    public void MergeAttributes(AttributeStore columnAttributes)
    {
        attributes.Merge(columnAttributes);
    }
}
