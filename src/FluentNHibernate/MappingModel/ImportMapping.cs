using FluentNHibernate.Visitors;
using System;

namespace FluentNHibernate.MappingModel;

[Serializable]
public class ImportMapping(AttributeStore attributes) : MappingBase, IEquatable<ImportMapping>
{
    readonly AttributeStore attributes = attributes;

    public ImportMapping()
        : this(new AttributeStore())
    {}

    public override void AcceptVisitor(IMappingModelVisitor visitor)
    {
        visitor.ProcessImport(this);
    }

    public string Rename => attributes.GetOrDefault<string>();

    public TypeReference Class => attributes.GetOrDefault<TypeReference>();

    public bool Equals(ImportMapping other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(other.attributes, attributes);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != typeof(ImportMapping)) return false;
        return Equals((ImportMapping)obj);
    }

    public override int GetHashCode()
    {
        return (attributes is not null ? attributes.GetHashCode() : 0);
    }

    public override bool IsSpecified(string attribute)
    {
        return attributes.IsSpecified(attribute);
    }

    protected override void Set(string attribute, int layer, object value)
    {
        attributes.Set(attribute, layer, value);
    }
}
