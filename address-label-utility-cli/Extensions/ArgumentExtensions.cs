using System;
using System.Collections.Generic;
using System.Linq;
using AddressLabelUtilityCli.Arguments;
using AddressLabelUtilityCore.Extensions;

namespace AddressLabelUtilityCli.Extensions
{
    internal static class ArgumentExtensions
    {
        public static int GetArgumentAsInt(this IArgument source)
        {
            if (source.Argument.IsNullOrWhiteSpace())
            {
                return default;
            }

            return Convert.ToInt32(source.Argument);
        }

        public static float GetArgumentAsFloat(this IArgument source)
        {
            if (source.Argument.IsNullOrWhiteSpace())
            {
                return default;
            }

            return (float)Convert.ToDouble(source.Argument);
        }

        public static T GetArgumentAsEnum<T>(this IArgument source)
            where T : struct
        {
            if (Enum.TryParse<T>(source.Argument, true, out var result))
            {
                return result;
            }

            return default;
        }

        public static bool IsDefinedEnumValue<T>(this IArgument source)
            where T : struct
        {
            return Enum.TryParse<T>(source.Argument, true, out var _);
        }

        public static IArgument Concat(this IArgument source, IArgument argument)
        {
            source.Raw += " " + argument.Raw;
            source.Argument = argument.Raw;

            return source;
        }

        public static bool TryConcat(this IArgument source, IArgument argument, out IArgument result)
        {
            result = source;

            if (!source.ShouldHaveArgument || argument.IsOption())
            {
                return false;
            }

            result = result.Concat(argument);
            return true;
        }

        public static bool IsOption(this IArgument source)
        {
            return source.Raw?.StartsWith("-") ?? false;
        }

        public static T Get<T>(this IEnumerable<IArgument> arguments)
            where T : IArgument
        {
            var target = (T)Activator.CreateInstance(typeof(T));

            if (arguments.Contains(target))
            {
                return (T)arguments.Get(target);
            }

            return default;
        }

        public static IArgument Get(this IEnumerable<IArgument> arguments, IArgument target)
        {
            if (arguments.Contains(target))
            {
                return arguments.First(x => x.Equals(target));
            }

            return default;
        }

        public static bool Contains<T>(this IEnumerable<IArgument> arguments)
            where T : IArgument
        {
            var target = (T)Activator.CreateInstance(typeof(T));

            return arguments.Contains(target);
        }

        public static bool Contains(this IEnumerable<IArgument> arguments, IArgument target)
        {
            return arguments.Any(x => x.Equals(target));
        }
    }
}
