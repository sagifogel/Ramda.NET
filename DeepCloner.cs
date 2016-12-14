using System;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Ramda.NET
{
    internal static class DeepCloner
    {
        private static readonly Type objectType = typeof(object);
        private static readonly Type thisType = typeof(DeepCloner);
        private static readonly Type fieldInfoType = typeof(FieldInfo);
        private static readonly Type objectDictionaryType = typeof(Dictionary<object, object>);
        private static Dictionary<Type, bool> isStructTypeToDeepCopyDictionary = new Dictionary<Type, bool>();
        private static readonly MethodInfo setValueMethod = fieldInfoType.GetMethod("SetValue", new[] { objectType, objectType });
        private static readonly MethodInfo cloneInternal = thisType.GetMethod("CloneInternal", BindingFlags.NonPublic | BindingFlags.Static);
        private static readonly BindingFlags allMembers = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
        private static Dictionary<Type, Func<object, Dictionary<object, object>, object>> compiledCopyFunctionsDictionary = new Dictionary<Type, Func<object, Dictionary<object, object>, object>>();

        internal static T Clone<T>(this T original) {
            return Clone(original, null);
        }

        private static T Clone<T>(this T original, Dictionary<object, object> copiedReferences) {
            return (T)CloneInternal(original, false, copiedReferences ?? new Dictionary<object, object>());
        }

        private static object CloneInternal(object original, bool forceDeepCopy, Dictionary<object, object> copiedReferences) {
            if (original.IsNull()) {
                return null;
            }

            var type = original.GetType();

            if (type.IsDelegate()) {
                return original;
            }

            if (!forceDeepCopy && !IsTypeToDeepCopy(type)) {
                return original;
            }

            object alreadyCopiedObject;

            if (copiedReferences.TryGetValue(original, out alreadyCopiedObject)) {
                return alreadyCopiedObject;
            }

            if (type.Equals(objectType)) {
                return new object();
            }

            var compiledCopyFunction = GetOrCreateCompiledLambdaCopyFunction(type);

            return compiledCopyFunction(original, copiedReferences);
        }

        private static Func<object, Dictionary<object, object>, object> GetOrCreateCompiledLambdaCopyFunction(Type type) {
            return compiledCopyFunctionsDictionary.GetOrAdd(type, () => {
                var uncompiledCopyFunction = CreateCompiledLambdaCopyFunctionForType(type);
                var compiledCopyFunction = uncompiledCopyFunction.Compile();
                var dictionaryCopy = compiledCopyFunctionsDictionary.ToDictionary(pair => pair.Key, pair => pair.Value);

                dictionaryCopy.Add(type, compiledCopyFunction);
                compiledCopyFunctionsDictionary = dictionaryCopy;

                return compiledCopyFunction;
            });
        }

        private static Expression<Func<object, Dictionary<object, object>, object>> CreateCompiledLambdaCopyFunctionForType(Type type) {
            LabelTarget endLabel;
            List<Expression> expressions;
            ParameterExpression inputParameter;
            ParameterExpression outputVariable;
            ParameterExpression boxingVariable;
            List<ParameterExpression> variables;
            ParameterExpression inputDictionary;

            InitializeExpressions(type, out inputParameter, out inputDictionary, out outputVariable, out boxingVariable, out endLabel, out variables, out expressions);
            IfNullThenReturnNullExpression(inputParameter, endLabel, expressions);
            MemberwiseCloneInputToOutputExpression(type, inputParameter, outputVariable, expressions);

            if (IsClassOtherThanString(type)) {
                StoreReferencesIntoDictionaryExpression(inputParameter, inputDictionary, outputVariable, expressions);
            }

            FieldsCopyExpressions(type, inputParameter, inputDictionary, outputVariable, boxingVariable, expressions);

            if (type.IsArray && IsTypeToDeepCopy(type.GetElementType())) {
                CreateArrayCopyLoopExpression(type, inputParameter, inputDictionary, outputVariable, variables, expressions);
            }

            return CombineAllIntoLambdaFunctionExpression(inputParameter, inputDictionary, outputVariable, endLabel, variables, expressions);
        }

        private static void InitializeExpressions(Type type, out ParameterExpression inputParameter, out ParameterExpression inputDictionary, out ParameterExpression outputVariable, out ParameterExpression boxingVariable, out LabelTarget endLabel, out List<ParameterExpression> variables, out List<Expression> expressions) {
            endLabel = Expression.Label();
            expressions = new List<Expression>();
            outputVariable = Expression.Variable(type);
            variables = new List<ParameterExpression>();
            boxingVariable = Expression.Variable(objectType);
            inputParameter = Expression.Parameter(objectType);
            inputDictionary = Expression.Parameter(objectDictionaryType);

            variables.Add(outputVariable);
            variables.Add(boxingVariable);
        }

        private static void IfNullThenReturnNullExpression(ParameterExpression inputParameter, LabelTarget endLabel, List<Expression> expressions) {
            var ifNullThenReturnNullExpression =
                Expression.IfThen(
                    Expression.Equal(
                        inputParameter,
                        Expression.Constant(null, objectType)),
                    Expression.Return(endLabel));

            expressions.Add(ifNullThenReturnNullExpression);
        }

        private static void MemberwiseCloneInputToOutputExpression(Type type, ParameterExpression inputParameter, ParameterExpression outputVariable, List<Expression> expressions) {
            var memberwiseCloneMethod = objectType.GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

            var memberwiseCloneInputExpression =
                Expression.Assign(outputVariable,
                    Expression.Convert(Expression.Call(inputParameter, memberwiseCloneMethod),
                        type));

            expressions.Add(memberwiseCloneInputExpression);
        }

        private static void StoreReferencesIntoDictionaryExpression(ParameterExpression inputParameter, ParameterExpression inputDictionary, ParameterExpression outputVariable, List<Expression> expressions) {
            var storeReferencesExpression =
                Expression.Assign(
                    Expression.Property(inputDictionary,
                        objectDictionaryType.GetProperty("Item"),
                        inputParameter),
                        Expression.Convert(outputVariable, objectType));

            expressions.Add(storeReferencesExpression);
        }

        private static Expression<Func<object, Dictionary<object, object>, object>> CombineAllIntoLambdaFunctionExpression(ParameterExpression inputParameter, ParameterExpression inputDictionary, ParameterExpression outputVariable, LabelTarget endLabel, List<ParameterExpression> variables, List<Expression> expressions) {
            expressions.Add(Expression.Label(endLabel));
            expressions.Add(Expression.Convert(outputVariable, objectType));

            return Expression.Lambda<Func<object, Dictionary<object, object>, object>>(
                        Expression.Block(variables, expressions), inputParameter, inputDictionary);
        }

        private static void CreateArrayCopyLoopExpression(Type type, ParameterExpression inputParameter, ParameterExpression inputDictionary, ParameterExpression outputVariable, List<ParameterExpression> variables, List<Expression> expressions) {
            Expression forExpression;
            var rank = type.GetArrayRank();
            var indices = GenerateIndices(rank);
            var elementType = type.GetElementType();

            forExpression = ArrayFieldToArrayFieldAssignExpression(inputParameter, inputDictionary, outputVariable, elementType, type, indices); ;
            variables.AddRange(indices);

            for (int dimension = 0; dimension < rank; dimension++) {
                forExpression = LoopIntoLoopExpression(inputParameter, indices[dimension], forExpression, dimension);
            }

            expressions.Add(forExpression);
        }

        private static List<ParameterExpression> GenerateIndices(int arrayRank) {
            var indices = new List<ParameterExpression>();

            for (int i = 0; i < arrayRank; i++) {
                var indexVariable = Expression.Variable(typeof(int));

                indices.Add(indexVariable);
            }

            return indices;
        }

        private static BinaryExpression ArrayFieldToArrayFieldAssignExpression(ParameterExpression inputParameter, ParameterExpression inputDictionary, ParameterExpression outputVariable, Type elementType, Type arrayType, List<ParameterExpression> indices) {
            var indexTo = Expression.ArrayAccess(outputVariable, indices);
            var indexFrom = Expression.ArrayIndex(Expression.Convert(inputParameter, arrayType), indices);
            var rightSide =
                Expression.Convert(
                    Expression.Call(
                        cloneInternal,
                        Expression.Convert(indexFrom, objectType),
                        Expression.Constant(true, typeof(bool)),
                        inputDictionary),
                    elementType);

            return Expression.Assign(indexTo, rightSide);
        }

        private static BlockExpression LoopIntoLoopExpression(ParameterExpression inputParameter, ParameterExpression indexVariable, Expression loopToEncapsulate, int dimension) {
            var endLabelForThisLoop = Expression.Label();
            var lengthVariable = Expression.Variable(typeof(int));
            var newLoop =
                Expression.Loop(
                    Expression.Block(
                        new ParameterExpression[0],
                        Expression.IfThen(
                            Expression.GreaterThanOrEqual(indexVariable, lengthVariable),
                            Expression.Break(endLabelForThisLoop)),
                        loopToEncapsulate,
                        Expression.PostIncrementAssign(indexVariable)),
                    endLabelForThisLoop);

            var lengthAssignment = GetLengthForDimensionExpression(lengthVariable, inputParameter, dimension);
            var indexAssignment = Expression.Assign(indexVariable, Expression.Constant(0));

            return Expression.Block(
                new[] { lengthVariable },
                lengthAssignment,
                indexAssignment,
                newLoop);
        }

        private static BinaryExpression GetLengthForDimensionExpression(ParameterExpression lengthVariable, ParameterExpression inputParameter, int i) {
            var getLengthMethod = typeof(Array).GetMethod("GetLength", BindingFlags.Public | BindingFlags.Instance);
            var dimensionConstant = Expression.Constant(i);

            return Expression.Assign(
                    lengthVariable,
                    Expression.Call(
                        Expression.Convert(inputParameter, typeof(Array)),
                        getLengthMethod,
                        new[] { dimensionConstant }));
        }

        private static void FieldsCopyExpressions(Type type, ParameterExpression inputParameter, ParameterExpression inputDictionary, ParameterExpression outputVariable, ParameterExpression boxingVariable, List<Expression> expressions) {
            var fields = GetAllRelevantFields(type);
            var readonlyFields = fields.Where(f => f.IsInitOnly).ToList();
            var writableFields = fields.Where(f => !f.IsInitOnly).ToList();
            bool shouldUseBoxing = readonlyFields.Any();

            if (shouldUseBoxing) {
                expressions.Add(Expression.Assign(boxingVariable, Expression.Convert(outputVariable, objectType)));
            }

            foreach (var field in readonlyFields) {
                ReadonlyFieldCopyExpression(type, field, inputParameter, inputDictionary, boxingVariable, expressions);
            }

            if (shouldUseBoxing) {
                expressions.Add(Expression.Assign(outputVariable, Expression.Convert(boxingVariable, type)));
            }

            foreach (var field in writableFields) {
                WritableFieldCopyExpression(type, field, inputParameter, inputDictionary, outputVariable, expressions);
            }
        }

        private static FieldInfo[] GetAllRelevantFields(Type type, bool forceAllFields = false) {
            var typeCache = type;
            var fieldsList = new List<FieldInfo>();

            while (typeCache != null) {
                var fields = typeCache.GetFields(allMembers).Where(field => forceAllFields || IsTypeToDeepCopy(field.FieldType));

                fieldsList.AddRange(fields);
                typeCache = typeCache.BaseType;
            }

            return fieldsList.ToArray();
        }

        private static FieldInfo[] GetAllFields(Type type) {
            return GetAllRelevantFields(type, forceAllFields: true);
        }

        private static void ReadonlyFieldToNullExpression(FieldInfo field, ParameterExpression boxingVariable, List<Expression> expressions) {
            var fieldToNullExpression =
                    Expression.Call(
                        Expression.Constant(field),
                        setValueMethod,
                        boxingVariable,
                        Expression.Constant(null, field.FieldType));

            expressions.Add(fieldToNullExpression);
        }

        private static void ReadonlyFieldCopyExpression(Type type, FieldInfo field, ParameterExpression inputParameter, ParameterExpression inputDictionary, ParameterExpression boxingVariable, List<Expression> expressions) {
            var fieldFrom = Expression.Field(Expression.Convert(inputParameter, type), field);
            var fieldDeepCopyExpression =
                Expression.Call(
                    Expression.Constant(field, fieldInfoType),
                    setValueMethod,
                    boxingVariable,
                    Expression.Call(
                        cloneInternal,
                        Expression.Convert(fieldFrom, objectType),
                        Expression.Constant(true, typeof(bool)),
                        inputDictionary));

            expressions.Add(fieldDeepCopyExpression);
        }

        private static void WritableFieldToNullExpression(FieldInfo field, ParameterExpression outputVariable, List<Expression> expressions) {
            var fieldTo = Expression.Field(outputVariable, field);
            var fieldToNullExpression =
                Expression.Assign(
                    fieldTo,
                    Expression.Constant(null, field.FieldType));

            expressions.Add(fieldToNullExpression);
        }

        private static void WritableFieldCopyExpression(Type type, FieldInfo field, ParameterExpression inputParameter, ParameterExpression inputDictionary, ParameterExpression outputVariable, List<Expression> expressions) {
            var fieldType = field.FieldType;
            var fieldTo = Expression.Field(outputVariable, field);
            var fieldFrom = Expression.Field(Expression.Convert(inputParameter, type), field);
            var fieldDeepCopyExpression =
                Expression.Assign(
                    fieldTo,
                    Expression.Convert(
                        Expression.Call(
                            cloneInternal,
                            Expression.Convert(fieldFrom, objectType),
                            Expression.Constant(true, typeof(bool)),
                            inputDictionary),
                        fieldType));

            expressions.Add(fieldDeepCopyExpression);
        }

        private static bool IsTypeToDeepCopy(Type type) {
            return IsClassOtherThanString(type) || IsStructWhichNeedsDeepCopy(type);
        }

        private static bool IsClassOtherThanString(Type type) {
            return !type.IsValueType && type != typeof(string);
        }

        private static bool IsStructWhichNeedsDeepCopy(Type type) {
            return isStructTypeToDeepCopyDictionary.GetOrAdd(type, () => {
                var newDictionary = isStructTypeToDeepCopyDictionary.ToDictionary(pair => pair.Key, pair => pair.Value);

                var isStructTypeToDeepCopy = newDictionary[type] = IsStructWhichNeedsDeepCopyInternal(type);
                isStructTypeToDeepCopyDictionary = newDictionary;

                return isStructTypeToDeepCopy;
            });
        }

        private static bool IsStructWhichNeedsDeepCopyInternal(Type type) {
            return IsStructOtherThanBasicValueTypes(type) && HasInItsHierarchyFieldsWithClasses(type);
        }

        private static bool IsStructOtherThanBasicValueTypes(Type type) {
            return type.IsValueType && !type.IsPrimitive && !type.IsEnum && type != typeof(decimal);
        }

        private static bool HasInItsHierarchyFieldsWithClasses(Type type, HashSet<Type> alreadyCheckedTypes = null) {
            var allFields = GetAllFields(type);
            var allFieldTypes = allFields.Select(f => f.FieldType).Distinct().ToList();
            var hasFieldsWithClasses = allFieldTypes.Any(IsClassOtherThanString);

            alreadyCheckedTypes = alreadyCheckedTypes ?? new HashSet<Type>();
            alreadyCheckedTypes.Add(type);

            if (hasFieldsWithClasses) {
                return true;
            }

            var notBasicStructsTypes = allFieldTypes.Where(IsStructOtherThanBasicValueTypes).ToList();
            var typesToCheck = notBasicStructsTypes.Where(t => !alreadyCheckedTypes.Contains(t)).ToList();

            foreach (var typeToCheck in typesToCheck) {
                if (HasInItsHierarchyFieldsWithClasses(typeToCheck, alreadyCheckedTypes)) {
                    return true;
                }
            }

            return false;
        }
    }
}
