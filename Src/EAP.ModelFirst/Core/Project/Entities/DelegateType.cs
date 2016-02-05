using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using EAP.ModelFirst.Core.Project.Parameters;
using System.ComponentModel;
using EAP.ModelFirst.Controls.Editors;
using System.Drawing.Design;

namespace EAP.ModelFirst.Core.Project.Entities
{
	public abstract class DelegateType : TypeBase
	{
		string returnType;
        ArgumentList argumentList;
        MemberInfo memberInfo;

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		protected DelegateType(string name) : base(name)
		{
            argumentList = Language.CreateParameterCollection();
			returnType = DefaultReturnType;
            memberInfo = new MemberInfo(this);
		}

        [Editor(typeof(TypeMemberEditor), typeof(UITypeEditor))]
        public MemberInfo Parameters
        {
            get { return memberInfo; }
        }

		public sealed override EntityType EntityType
		{
			get { return EntityType.Delegate; }
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="value"/> does not fit to the syntax.
		/// </exception>
		public virtual string ReturnType
		{
			get
			{
				return returnType;
			}
			set
			{
                string newReturnType = Language.GetValidTypeName(value);

				if (newReturnType != returnType) {
					returnType = newReturnType;
					Changed();
				}
			}
		}

        [System.ComponentModel.Browsable(false)]
		public IEnumerable<Parameter> Arguments
		{
			get { return argumentList; }
		}

        [System.ComponentModel.Browsable(false)]
		public int ArgumentCount
		{
			get { return argumentList.Count; }
		}

		public sealed override string Signature
		{
			get
			{
                return (Language.GetAccessString(Access, false) + " Delegate");
			}
		}

		public override string Stereotype
		{
			get { return "Delegate"; }
		}

		protected abstract string DefaultReturnType
		{
			get;
		}

		public Parameter GetArgument(int index)
		{
			if (index >= 0 && index < argumentList.Count)
				return argumentList[index];
			else
				return null;
		}

		/// <exception cref="BadSyntaxException">
		/// The name does not fit to the syntax.
		/// </exception>
		/// <exception cref="ReservedNameException">
		/// The parameter name is already exists.
		/// </exception>
		public Parameter AddParameter(string declaration)
		{
			Parameter parameter = argumentList.Add(declaration);

			parameter.Modified += delegate { Changed(); };
			Changed();
			return parameter;
		}

		/// <exception cref="BadSyntaxException">
		/// The name does not fit to the syntax.
		/// </exception>
		/// <exception cref="ReservedNameException">
		/// The parameter name is already exists.
		/// </exception>
		public Parameter ModifyParameter(Parameter parameter, string declaration)
		{
			Parameter modified = argumentList.ModifyParameter(parameter, declaration);

			Changed();
			return modified;
		}

		public void RemoveParameter(Parameter parameter)
		{
			argumentList.Remove(parameter);
			Changed();
		}

		public override bool MoveUpItem(object item)
		{
			if (item is Parameter && MoveUp(argumentList, item))
			{
				Changed();
				return true;
			}
			else
			{
				return false;
			}
		}

		public override bool MoveDownItem(object item)
		{
			if (item is Parameter && MoveDown(argumentList, item))
			{
				Changed();
				return true;
			}
			else
			{
				return false;
			}
		}

		protected override void CopyFrom(TypeBase type)
		{
			base.CopyFrom(type);

			DelegateType delegateType = (DelegateType) type;
			returnType = delegateType.returnType;
			argumentList = delegateType.argumentList.Clone();
		}

		public abstract DelegateType Clone();

		/// <exception cref="ArgumentNullException">
		/// <paramref name="node"/> is null.
		/// </exception>
		protected internal override void Serialize(XmlElement node)
		{
            base.Serialize(node);

			XmlElement returnTypeNode = node.OwnerDocument.CreateElement("ReturnType");
			returnTypeNode.InnerText = ReturnType.ToString();
			node.AppendChild(returnTypeNode);

			foreach (Parameter parameter in argumentList)
			{
				XmlElement paramNode = node.OwnerDocument.CreateElement("Param");
				paramNode.InnerText = parameter.ToString();
				node.AppendChild(paramNode);
			}
		}

		/// <exception cref="BadSyntaxException">
		/// An error occured while deserializing.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// The XML document is corrupt.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="node"/> is null.
		/// </exception>
		protected internal override void Deserialize(XmlElement node)
		{
			RaiseChangedEvent = false;

			XmlElement returnTypeNode = node["ReturnType"];
			if (returnTypeNode != null)
				ReturnType = returnTypeNode.InnerText;

			XmlNodeList nodeList = node.SelectNodes("Param");
            argumentList.Clear();
			foreach (XmlNode parameterNode in nodeList)
				argumentList.Add(parameterNode.InnerText);

            base.Deserialize(node);
			RaiseChangedEvent = true;
		}
	}
}
