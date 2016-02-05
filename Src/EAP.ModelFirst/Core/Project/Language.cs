using System;
using System.Reflection;
using System.Xml.Serialization;
using System.Collections.Generic;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Members;
using EAP.ModelFirst.Core.Project.Parameters;

namespace EAP.ModelFirst.Core.Project
{
	public abstract class Language
	{
		public abstract string Name
		{
			get;
		}

		public abstract string AssemblyName
		{
			get;
		}

		[XmlIgnore]
		public abstract Dictionary<AccessModifier, string> ValidAccessModifiers
		{
			get;
		}

		[XmlIgnore]
		public abstract Dictionary<ClassModifier, string> ValidClassModifiers
		{
			get;
		}

		[XmlIgnore]
		public abstract Dictionary<FieldModifier, string> ValidFieldModifiers
		{
			get;
		}

		[XmlIgnore]
		public abstract Dictionary<OperationModifier, string> ValidOperationModifiers
		{
			get;
		}

		public abstract bool SupportsAssemblyImport
		{
			get;
		}

		public abstract bool SupportsInterfaces
		{
			get;
		}

		public abstract bool SupportsStructures
		{
			get;
		}

		public abstract bool SupportsEnums
		{
			get;
		}

		public abstract bool SupportsDelegates
		{
			get;
		}

		public abstract bool SupportsExplicitImplementation
		{
			get;
		}

		public abstract bool ExplicitVirtualMethods
		{
			get;
		}

		public abstract string DefaultFileExtension
		{
			get;
		}

		protected abstract string[] ReservedNames
		{
			get;
		}

		protected abstract string[] TypeKeywords
		{
			get;
		}

		public bool IsForbiddenName(string name)
		{
			return (
				Contains(ReservedNames, name) ||
				Contains(TypeKeywords, name)
			);
		}

		public bool IsForbiddenTypeName(string name)
		{
			return Contains(ReservedNames, name);
		}

		public static Language GetLanguage(string languageName)
        {
            return Uml.UnifiedModelingLanguage.Instance;
            //try
            //{
            //    string languageString = languageName;
            //    if (languageString == "CSharp")
            //        return CSharp.CSharpLanguage.Instance;
            //    else if (languageString == "Java")
            //        return Java.JavaLanguage.Instance;
            //    else if (languageString == "UML")
            //        return Uml.UnifiedModelingLanguage.Instance;

            //    Assembly assembly = Assembly.Load(languageString);

            //    foreach (Type type in assembly.GetTypes())
            //    {
            //        if (type.IsSubclassOf(typeof(Language)))
            //        {
            //            object languageInstance = type.InvokeMember("Instance",
            //                BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty,
            //                null, null, null);
            //            return (languageInstance as Language);
            //        }
            //    }
            //    return null;
            //}
            //catch
            //{
            //    return null;
            //}
		}

		private static bool Contains(string[] values, string value)
		{
			if (values == null)
				return false;

			for (int i = 0; i < values.Length; i++)
				if (values[i] == value)
					return true;

			return false;
		}

		/// <exception cref="ArgumentException">
		/// The language does not support explicit interface implementation.
        /// 語言不支持顯式接口實現
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="operation"/> is null.-or-
		/// <paramref name="newParent"/> is null.
		/// </exception>
		protected internal abstract Operation Implement(Operation operation,
			CompositeType newParent, bool explicitly);

		/// <exception cref="ArgumentException">
		/// <paramref name="operation"/> cannot be overridden.不能被重寫
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="operation"/> is null.
		/// </exception>
		protected internal abstract Operation Override(Operation operation, CompositeType newParent);

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.名稱不符合語法
		/// </exception>
		public virtual string GetValidName(string name, bool isGenericName)
		{
			if (IsForbiddenName(name))
				throw new BadSyntaxException(Strings.ErrorForbiddenName);

			return name;
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.名稱不符合語法
		/// </exception>
		public virtual string GetValidTypeName(string name)
		{
			if (IsForbiddenTypeName(name))
				throw new BadSyntaxException(Strings.ErrorForbiddenTypeName);

			return name;
		}

		public virtual ClassModifier TryParseClassModifier(string value)
		{
			try {
				return (ClassModifier) Enum.Parse(
					typeof(ClassModifier), value, true);
			}
			catch {
				return ClassModifier.None;
			}
		}

		public virtual AccessModifier TryParseAccessModifier(string value)
		{
			try {
				if (string.IsNullOrEmpty(value))
					return AccessModifier.Default;
				else
					return (AccessModifier) Enum.Parse(typeof(AccessModifier), value, true);
			}
			catch {
				return AccessModifier.Default;
			}
		}

		public virtual OperationModifier TryParseOperationModifier(string value)
		{
			try {
				if (string.IsNullOrEmpty(value)) {
					return OperationModifier.None;
				}
				else {
					return (OperationModifier) Enum.Parse(
						typeof(OperationModifier), value, true);
				}
			}
			catch {
				return OperationModifier.None;
			}
		}

		public abstract bool IsValidModifier(FieldModifier modifier);

		public abstract bool IsValidModifier(OperationModifier modifier);

		public abstract bool IsValidModifier(AccessModifier modifier);

		/// <exception cref="BadSyntaxException">
        /// The <paramref name="operation"/> contains invalid modifier combinations.字段包括無效的修飾組合
		/// </exception>
		protected internal abstract void ValidateOperation(Operation operation);

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="field"/> contains invalid modifier combinations.字段包括無效的修飾組合
		/// </exception>
		protected internal abstract void ValidateField(Field field);

		protected internal abstract ClassType CreateClass(string name);

		/// <exception cref="InvalidOperationException">
		/// The language does not support structures.語言不支持結構體
		/// </exception>
        protected internal abstract StructureType CreateStructure(string name);

		/// <exception cref="InvalidOperationException">
		/// The language does not support interfaces.語言不支持接口
		/// </exception>
        protected internal abstract InterfaceType CreateInterface(string name);

		/// <exception cref="InvalidOperationException">
		/// The language does not support enums.語言不支持枚舉
		/// </exception>
        protected internal abstract EnumType CreateEnum(string name);

		/// <exception cref="InvalidOperationException">
		/// The language does not support delegates.語言不技術委託
		/// </exception>
        protected internal abstract DelegateType CreateDelegate(string name);

		protected internal abstract ArgumentList CreateParameterCollection();


		public abstract string GetAccessString(AccessModifier access, bool forCode);

		public abstract string GetFieldModifierString(FieldModifier modifier, bool forCode);

		public abstract string GetOperationModifierString(OperationModifier modifier, bool forCode);

		public abstract string GetClassModifierString(ClassModifier modifier, bool forCode);

		public string GetAccessString(AccessModifier access)
		{
			return GetAccessString(access, false);
		}

		public string GetFieldModifierString(FieldModifier modifier)
		{
			return GetFieldModifierString(modifier, false);
		}

		public string GetOperationModifierString(OperationModifier modifier)
		{
			return GetOperationModifierString(modifier, false);
		}

		public string GetClassModifierString(ClassModifier modifier)
		{
			return GetClassModifierString(modifier, false);
		}

		public override string ToString()
		{
			return Name;
		}
	}
}