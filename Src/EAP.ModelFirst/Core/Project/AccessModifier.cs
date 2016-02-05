using System;

namespace EAP.ModelFirst.Core.Project
{
	public enum AccessModifier
	{
		/// <summary>
		/// The default access in the context of the element's scope.
        /// �����q�{���X�ݽd��
		/// </summary>
		Default,

		/// <summary>
		/// Access is not restricted.
        /// ���������X��
		/// </summary>
		Public,

		/// <summary>
		/// Access is limited to the current assembly or types 
		/// derived from the containing class.
        /// �X�ݨ������e�{�Ƕ��Ϊ̩ҥ]�t�����M��l��
		/// </summary>
		ProtectedInternal,

		/// <summary>
		/// Access is limited to the current assembly.
        /// �X�ݨ������e�{�Ƕ�
		/// </summary>
		Internal,

		/// <summary>
		/// Access is limited to the containing class or types 
		/// derived from the containing class.
        /// �X�ݨ�����]�t�����M��l��
		/// </summary>
		Protected,

		/// <summary>
		/// Access is limited to the containing type.
        /// �X�ݨ�����]�t����
		/// </summary>
		Private
	}
}
