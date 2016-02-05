
namespace EAP.ModelFirst.Core.Template
{
    public enum GenerateMode
    {
        /// <summary>
        /// 每个TypeBase单独执行
        /// </summary>
        Single,
        /// <summary>
        /// 如果是Nested，找到NestingRoot单独执行
        /// </summary>
        Nested,
        /// <summary>
        /// 批量执行
        /// </summary>
        Batch,
    }
}
