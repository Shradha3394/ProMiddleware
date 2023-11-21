using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Collections;
using System.Reflection;
using Nhs.Utility.Common;
using Pro.Web.Api.Library.Helpers;

namespace Pro.Web.Api.Library.NewtonsoftJson
{
    ///https://www.thetopsites.net/article/52528338.shtml
    public class ShouldSerializeContractResolver : DefaultContractResolver
    {
        public static readonly ShouldSerializeContractResolver Instance = new ShouldSerializeContractResolver();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (property.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                property.ShouldSerialize = instance =>
                {
                    IEnumerable enumerable = null;
                    // this value could be in a public field or public property
                    switch (member.MemberType)
                    {
                        case MemberTypes.Property:
                            enumerable = instance.GetType()
                                .GetProperty(member.Name)
                                ?.GetValue(instance, null) as IEnumerable;
                            break;
                        case MemberTypes.Field:
                            enumerable = instance.GetType()
                                .GetField(member.Name)
                                .GetValue(instance) as IEnumerable;
                            break;
                    }

                    return enumerable == null ||
                           enumerable.GetEnumerator().MoveNext();
                    // if the list is null, we defer the decision to NullValueHandling
                };
            }
            else if (property.PropertyType == typeof(string))
            {
                property.ShouldSerialize = instance =>
                {
                    var name = property.UnderlyingName;
                    var value = instance?.GetType().GetProperty(name)?.GetValue(instance);
                    return value.ToType<string>().IsNotNullOrWhiteSpaceOrEmpty();
                };
            }
            return property;

        }
    }
}