using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Philips.PmsMR.Protobuf.ProtoGenerator {

    /// <summary>
    /// Simple command line argument parser.
    /// </summary>
    public class ArgumentParser {

        /// <summary>
        /// Information about a single argument.
        /// </summary>
        public abstract class ArgumentTypeInformation
        {
            /// <summary>
            /// Abstract method to search for an argument in command line data.
            /// </summary>
            /// <param name="map"></param>
            /// <param name="args"></param>
            /// <param name="index"></param>
            /// <returns></returns>
            public abstract bool ConsumeArguments(IDictionary<string, string> map, string[] args, ref int index);

            /// <summary>
            /// Command line option name key, without the dashes.
            /// </summary>
            public string OptionName { get; set; }

            /// <summary>
            /// Default value to be used if the option key is not in the command line input.
            /// </summary>
            public string DefaultValue { get; set; }

            /// <summary>
            /// Details about the option.
            /// </summary>
            public string OptionalDescription { get; set; }

            /// <summary>
            /// Descriptive string for synopsis.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                var basicDescription = String.Format(CultureInfo.InvariantCulture, "[--{0}] : default {1}", OptionName,
                                                      DefaultValue);
                if (OptionalDescription == null)
                {
                    return basicDescription;
                }
                return basicDescription + ". " + OptionalDescription;
            }
        }

        /// <summary>
        /// Argument, which presence or absence toggles a stringified flag.
        /// </summary>
        public class FlagTypeInformation : ArgumentTypeInformation
        {
            /// <summary>
            /// Specialized parsing for an on/off argument.
            /// </summary>
            /// <param name="map"></param>
            /// <param name="args"></param>
            /// <param name="index"></param>
            /// <returns></returns>
            public override bool ConsumeArguments(IDictionary<string, string> map, string[] args, ref int index)
            {
                map[OptionName] = StoreValue;
                return true;
            }

            /// <summary>
            /// Value to be stored if the flag argument was present.
            /// </summary>
            public string StoreValue { get; set; }
        }

        /// <summary>
        /// Argument, which presence or absence toggles a boolean flag
        /// </summary>
        public class BooleanFlagTypeInformation : FlagTypeInformation
        {
            /// <summary>
            /// Defailt ctor, setting the default flag value to false.
            /// </summary>
            public BooleanFlagTypeInformation()
            {
                DefaultValue = false.ToString(CultureInfo.InvariantCulture);
            }

            /// <summary>
            /// Value to be stored if the flag argument was present.
            /// </summary>
            public bool BooleanValue 
            { 
                get { return bool.Parse(StoreValue); } 
                set { StoreValue = value.ToString(CultureInfo.InvariantCulture); } 
            }
        }

        /// <summary>
        /// Argument, which value follows the keyword.
        /// </summary>
        public class ValuedTypeInformation : ArgumentTypeInformation
        {
            /// <summary>
            /// Specialized parsing for reading the argument key and value.
            /// </summary>
            /// <param name="map"></param>
            /// <param name="args"></param>
            /// <param name="index"></param>
            /// <returns></returns>
            public override bool ConsumeArguments(IDictionary<string, string> map, string[] args, ref int index)
            {
                if (++index == args.Length) {
                    Console.Error.WriteLine("Last option value was not provided: " + OptionName);
                    return false;
                }

                string optionValue = args[index];
                if (!ValuedValidatorFunc(optionValue)) {
                    Console.Error.WriteLine("Failed to parse option " + OptionName + ", invalid value was " + optionValue);
                    return false;
                }
                map[OptionName] = optionValue;
                return true;
            }

            /// <summary>
            /// Validator for the valued option.
            /// </summary>
            public Func<string, bool> ValuedValidatorFunc { get; set; }
        }

        /// <summary>
        /// Specialized valued type for handling unsigned integers.
        /// </summary>
        public class UnsignedIntValuedTypeInformation : ValuedTypeInformation
        {
            /// <summary>
            /// Initializing ctor.
            /// </summary>
            /// <param name="minValue">Validator minimum allowed value</param>
            /// <param name="maxValue">Validator maximum allowed value</param>
            public UnsignedIntValuedTypeInformation(UInt32 minValue, UInt32 maxValue)
            {
                ValuedValidatorFunc = s =>
                    {
                        UInt32 value;
                        return UInt32.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out value) &&
                               value >= minValue && value <= maxValue;
                    };
            }

            /// <summary>
            /// Default integer value, if not provided on the command line.
            /// </summary>
            public UInt32 DefaultUnsignedIntegerValue {
                get { return UInt32.Parse(DefaultValue); }
                set { DefaultValue = value.ToString(CultureInfo.InvariantCulture); }
            }
        }

        /// <summary>
        /// Specialized valued type for handling floating point values.
        /// </summary>
        public class DoubleValuedTypeInformation : ValuedTypeInformation {
            /// <summary>
            /// Initializing ctor.
            /// </summary>
            /// <param name="minValue">Validator minimum allowed value</param>
            /// <param name="maxValue">Validator maximum allowed value</param>
            public DoubleValuedTypeInformation(double minValue, double maxValue) {
                ValuedValidatorFunc = s => {
                        double value;
                        return double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out value) &&
                               value >= minValue && value <= maxValue;
                    };
            }
        }

        /// <summary>
        /// Parsing of arguments.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="optionalHelpText">If help text is null, the text will be autogenerated</param>
        /// <param name="defaults"></param>
        /// <returns></returns>
        public bool Execute(string[] args, string optionalHelpText, IDictionary<string, ArgumentTypeInformation> defaults)
        {
            var helpText = GenerateSynapsis(optionalHelpText, defaults);
            OptionValueMap = new Dictionary<string, string>();
            for (int index = 0; index < args.Length; ++index)
            {
                var word = args[index];
                if (word.StartsWith("-", StringComparison.InvariantCulture)) {
                    string optionName = word.TrimStart('-');

                    ArgumentTypeInformation typeInfo;
                    if (!defaults.TryGetValue(optionName, out typeInfo)) {
                        Console.Error.WriteLine("Unknown option provided: " + optionName);
                        Console.Error.WriteLine(helpText);
                        return false;
                    }

                    if (!typeInfo.ConsumeArguments(OptionValueMap, args, ref index))
                    {
                        Console.Error.WriteLine(helpText);
                        return false;
                    }
                    Console.Out.WriteLine("Option " + optionName + " set: " + OptionValueMap[optionName]);
                }
            }
            if (OptionValueMap.Count < 1)
            {
                Console.Error.WriteLine(helpText);
                return false;
            }

            foreach (var item in defaults)
            {
                if (!OptionValueMap.ContainsKey(item.Key))
                {
                    OptionValueMap.Add(new KeyValuePair<string, string>(item.Key, item.Value.DefaultValue));
                }
            }

            return true;
        }

        /// <summary>
        /// Parsing results.
        /// </summary>
        public IDictionary<string, string> OptionValueMap { get; private set; }

        private string GenerateSynapsis(string optionalHelpText, IDictionary<string, ArgumentTypeInformation> defaults) {
            string exeName = "<executable>";
            var exeAssembly = System.Reflection.Assembly.GetEntryAssembly();
            if (exeAssembly != null) {
                exeName = System.IO.Path.GetFileName(exeAssembly.Location);
            }
            var builder = new StringBuilder(optionalHelpText ?? String.Format(CultureInfo.InvariantCulture, "Synopsis: {0}", exeName));
            builder.AppendLine();
            foreach (var item in defaults.Values) {
                builder.Append('\t');
                builder.AppendLine(item.ToString());
            }
            return builder.ToString();
        }

    }
}
