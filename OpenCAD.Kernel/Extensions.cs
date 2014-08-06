using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Maths;

namespace OpenCAD.Kernel
{
    public static class Extensions
    {
        public static IEnumerable<float> ToFloatArray(this Color c)
        {
            yield return ((float) c.R).Map(0, 255, 0, 1);
            yield return ((float) c.G).Map(0, 255, 0, 1);
            yield return ((float) c.B).Map(0, 255, 0, 1);
            yield return ((float) c.A).Map(0, 255, 0, 1);
        }
        public static MemberInfo GetMemberInfo(this Expression expression)
        {
            var lambda = (LambdaExpression)expression;

            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
                memberExpression = (MemberExpression)lambda.Body;

            return memberExpression.Member;
        }
    }
}
