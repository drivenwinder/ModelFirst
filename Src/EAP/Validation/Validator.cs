using System;
using System.Text;
using System.Linq.Expressions;

namespace EAP.Validation
{
    [Serializable]
    public class Validator
    {
        ErrorInfoCollection errors = new ErrorInfoCollection();

        /// <summary>
        /// Checks for a condition and add error if the condition is false.
        /// <para>Return false if has error.</para>
        /// </summary>
        public bool Assert(bool condition, string fieldName, ErrorText error, int rowNum = int.MinValue, string sheetName = "")
        {
            if (!condition)
            {
                AddError(fieldName, error, rowNum, sheetName);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Add error if value equals default(T).
        /// <para>Return false if has error.</para>
        /// </summary>
        public bool Require<T>(T value, string fieldName, int rowNum = int.MinValue, string sheetName = "")
        {
            if (object.Equals(value, default(T)))
            {
                AddError(fieldName, ErrorText.Require, rowNum, sheetName);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Add error if value is null or empty.
        /// <para>Return false if has error.</para>
        /// </summary>
        public bool Require(string value, string fieldName, int rowNum = int.MinValue, string sheetName = "")
        {
            if (string.IsNullOrEmpty(value))
            {
                AddError(fieldName, ErrorText.Require, rowNum, sheetName);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Add error if value is DateTime.MinValue or DateTime.MaxValue.
        /// <para>Return false if has error.</para>
        /// </summary>
        public bool Require(DateTime value, string fieldName, int rowNum = int.MinValue, string sheetName = "")
        {
            if (value == DateTime.MinValue || value == DateTime.MaxValue)
            {
                AddError(fieldName, ErrorText.Require, rowNum, sheetName);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Add error if value's length is greater then lenght.
        /// <para>Return false if has error.</para>
        /// </summary>
        public bool MaxLength(string value, int length, string fieldName, int rowNum = int.MinValue, string sheetName = "")
        {
            if (value.IsNotEmpty() && value.Length > length)
            {
                AddError(fieldName, ErrorText.MaxLength(length, value.Length), rowNum, sheetName);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Add error if value's length is less then lenght.
        /// </summary>
        public bool MinLength(string value, int length, string fieldName, int rowNum = int.MinValue, string sheetName = "")
        {
            if (string.IsNullOrEmpty(value) || value.Length < length)
            {
                AddError(fieldName, ErrorText.MinLength(length, value == null ? 0 : value.Length), rowNum, sheetName);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Add error if value.CompareTo(minValue) &lt; 0.
        /// </summary>
        public bool MinValue<T>(T value, T minValue, string fieldName, int rowNum = int.MinValue, string sheetName = "") where T : IComparable
        {
            if (value != null && value.CompareTo(minValue) >= 0)
                return true;
            AddError(fieldName, ErrorText.MinValue(minValue, value), rowNum, sheetName);
            return false;
        }

        /// <summary>
        /// Add error if value.CompareTo(maxValue) &gt; 0.
        /// </summary>
        public bool MaxValue<T>(T value, T maxValue, string fieldName, int rowNum = int.MinValue, string sheetName = "") where T : IComparable
        {
            if (value != null && value.CompareTo(maxValue) <= 0)
                return true;
            AddError(fieldName, ErrorText.MaxValue(maxValue, value), rowNum, sheetName);
            return false;
        }

        public void AddError(string fieldName, ErrorText error, int rowNum = int.MinValue, string sheetName = "")
        {
            errors.Add(new ErrorInfo(fieldName, error, rowNum, sheetName));
        }

        /// <summary>
        /// Return true if there's no error.
        /// </summary>
        public bool IsValid
        {
            get { return errors.Count == 0; }
        }

        /// <summary>
        /// Error info.
        /// </summary>
        public ErrorInfoCollection ErrorInfos
        {
            get { return errors; }
        }

        public override string ToString()
        {
            if (IsValid)
                return "Valid";
            else
            {
                StringBuilder text = new StringBuilder();
                foreach (ErrorInfo error in errors)
                {
                    if (error.SheetName.IsNotEmpty())
                        text.Append(string.Format("表【{0}】", error.SheetName)).Append(",");
                    if (error.RowNum != int.MinValue)
                        text.Append(string.Format("第{0}行", error.RowNum)).Append(",");
                    foreach (ErrorText errorText in error.Errors)
                    {
                        text.Append(errorText.Text).Append(",");
                    }
                    text.Remove(text.Length - 1, 1);
                    text.Append(";");
                }
                return text.ToString();
            }
        }

        public Contract<TEntity, TResult> Check<TEntity, TResult>(TEntity model,
            Expression<System.Func<TEntity, TResult>> expr, int rowNum = int.MinValue)
        {
            return new Contract<TEntity, TResult>(this, model, expr, rowNum);
        }

        public class Contract<TEntity, TResult>
        {
            bool isValid = true;
            TEntity _entity;
            Validator _validator;
            Expression<Func<TEntity, TResult>> _expr;
            int _rowNum;

            public bool IsValid
            {
                get { return isValid; }
            }

            public Contract(Validator validator, TEntity entity,
                Expression<Func<TEntity, TResult>> expr, int rowNum)
            {
                _entity = entity;
                _validator = validator;
                _expr = expr;
                _rowNum = rowNum;
            }

            public Contract<TEntity, TResult> IsRequired()
            {
                MemberExpression expr = _expr.Body as MemberExpression;
                string fieldName = expr != null ? expr.Member.Name : "";
                isValid = isValid && _validator.Require(
                    _expr.Compile().Invoke(_entity), fieldName, _rowNum);
                return this;
            }

            public Contract<TEntity, TResult> MaxLength(int length)
            {
                MemberExpression expr = _expr.Body as MemberExpression;
                string fieldName = expr != null ? expr.Member.Name : "";
                isValid = isValid && _validator.MaxLength(
                    _expr.Compile().Invoke(_entity).ToSafeString(), length, fieldName, _rowNum);
                return this;
            }

            public Contract<TEntity, TResult> MinLength(int length)
            {
                MemberExpression expr = _expr.Body as MemberExpression;
                string fieldName = expr != null ? expr.Member.Name : "";
                isValid = isValid && _validator.MinLength(
                    _expr.Compile().Invoke(_entity).ToSafeString(), length, fieldName, _rowNum);
                return this;
            }

            public Contract<TEntity, TResult> MaxValue(TResult maxValue)
            {
                if (typeof(TResult).IsAssignableFrom(typeof(IComparable)))
                {
                    MemberExpression expr = _expr.Body as MemberExpression;
                    string fieldName = expr != null ? expr.Member.Name : "";
                    TResult value = _expr.Compile().Invoke(_entity);
                    isValid = isValid && _validator.MaxValue(
                        (IComparable)_expr.Compile().Invoke(_entity), (IComparable)maxValue, fieldName, _rowNum);
                }
                else
                    isValid = false;
                return this;
            }

            public Contract<TEntity, TResult> MinValue(TResult minValue)
            {
                if (typeof(TResult).IsAssignableFrom(typeof(IComparable)))
                {
                    MemberExpression expr = _expr.Body as MemberExpression;
                    string fieldName = expr != null ? expr.Member.Name : "";
                    TResult value = _expr.Compile().Invoke(_entity);
                    isValid = isValid && _validator.MinValue(
                        (IComparable)_expr.Compile().Invoke(_entity), (IComparable)minValue, fieldName, _rowNum);
                }
                else
                    isValid = false;
                return this;
            }

            public Contract<TEntity, TResult> IsUnique(Predicate<TEntity> predicate)
            {
                if (isValid)
                {
                    MemberExpression expr = _expr.Body as MemberExpression;
                    string fieldName = expr != null ? expr.Member.Name : "";
                    TResult value = _expr.Compile().Invoke(_entity);
                    isValid = isValid && _validator.Assert(
                        predicate.Invoke(_entity), fieldName, ErrorText.Exists);
                }
                return this;
            }

            delegate bool Func(TEntity model, bool isNew);
        }
    }
}
