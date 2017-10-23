using System;
using System.Linq;
using System.Runtime.Serialization;

namespace RocketMC.Utilities
{
    /// <summary>
    /// Type型の拡張機能
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// 文字列から列挙型に変換します。
        /// EnumMember属性を参照します。
        /// </summary>
        /// <param name="type">変換先の列挙型</param>
        /// <param name="name">変換する文字列</param>
        /// <returns>typeで指定した列挙型のメンバ</returns>
        public static object GetEnum(this Type type, string name)
        {
            var enumType = type;
            return
                Enum.GetNames(enumType)
                    .Select(enumName => enumType.GetField(enumName))
                    .Select(
                        field =>
                            ((EnumMemberAttribute[])
                                field.GetCustomAttributes(
                                    typeof(EnumMemberAttribute), true)
                                )
                                .Single())
                    .Any(attribute => attribute.Value == name)
                        ? Convert.ChangeType(Enum.Parse(enumType, name, true), type)
                        : Activator.CreateInstance(type);
        }
    }
}