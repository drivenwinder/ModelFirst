using System;

namespace EAP.ModelFirst.Core.Project
{
	public enum AccessModifier
	{
		/// <summary>
		/// The default access in the context of the element's scope.
        /// 元素默認的訪問範圍
		/// </summary>
		Default,

		/// <summary>
		/// Access is not restricted.
        /// 不受限的訪問
		/// </summary>
		Public,

		/// <summary>
		/// Access is limited to the current assembly or types 
		/// derived from the containing class.
        /// 訪問受限於當前程序集或者所包含類型和其子類
		/// </summary>
		ProtectedInternal,

		/// <summary>
		/// Access is limited to the current assembly.
        /// 訪問受限於當前程序集
		/// </summary>
		Internal,

		/// <summary>
		/// Access is limited to the containing class or types 
		/// derived from the containing class.
        /// 訪問受限於包含類型和其子類
		/// </summary>
		Protected,

		/// <summary>
		/// Access is limited to the containing type.
        /// 訪問受限於包含類型
		/// </summary>
		Private
	}
}
