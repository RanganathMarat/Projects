using System;
using System.Text.RegularExpressions;

namespace Common
{
    /// <summary>
    /// Global Variables
    /// </summary>
    public static class Global
    {
        /// <summary>
        /// Database constants
        /// </summary>
        public static class Database
        {
            /// <summary>
            /// The methods database
            /// </summary>
            public const string MethodsDb = @"../Database/Methods/";

            /// <summary>
            /// The results database
            /// </summary>
            public const string ResultsDb = @"../Database/Results/";

            /// <summary>
            /// The methods ext
            /// </summary>
            public const string MethodsExt = ".mtd";

            /// <summary>
            /// The results file
            /// </summary>
            public const string ResultsFile = "Results.res";

            /// <summary>
            /// The sample method
            /// </summary>
            public const string SampleMethod = @"FileName = <name>
# <comment>
Start

End";
            /// <summary>
            /// The file nam tag
            /// </summary>
            public const string FileNamTag = "FileName =";
        }

        /// <summary>
        /// Instructions
        /// </summary>
        public static class Instructions
        {
            /// <summary>
            /// Signal
            /// </summary>
            public class Signal
            {
                /// <summary>
                /// The instruction type
                /// </summary>
                public InstructionType InstructionType;

                /// <summary>
                /// The signal type
                /// </summary>
                public Waveforms SignalType;

                /// <summary>
                /// The amplitude
                /// </summary>
                public int Amplitude;

                /// <summary>
                /// The frequency
                /// </summary>
                public int Frequency;

                /// <summary>
                /// The color
                /// </summary>
                public Colors Color;

                /// <summary>
                /// The delay
                /// </summary>
                public int Delay;

                /// <summary>
                /// The duration
                /// </summary>
                public int Duration;
            }

            /// <summary>
            /// The pattern
            /// </summary>
            public const string Pattern = @"(Sine|Square|Triangle|Sawtooth)\sa:\d\sf:\d\sc:(Red|Blue|Green|Yellow|Black)";

            /// <summary>
            /// The comments tag
            /// </summary>
            public const string CommentsTag = "#";

            /// <summary>
            /// The start
            /// </summary>
            public const string Start = "Start";

            /// <summary>
            /// The end
            /// </summary>
            public const string End = "End";

            /// <summary>
            /// Compiles the instructions.
            /// </summary>
            /// <param name="lines">The lines.</param>
            /// <returns></returns>
            public static string Compile(string[] lines)
            {
                string result = string.Empty;

                for (int i = 0; i < lines.Length; i++)
                {
                    var inst = lines[i].Trim();

                    if (string.IsNullOrEmpty(inst)) continue;

                    if (inst.Contains(Database.FileNamTag)) continue;

                    if (inst.Contains(Start) || lines[i].Contains(End)) continue;

                    if (inst.StartsWith(CommentsTag)) continue;

                    if (!Regex.IsMatch(inst, Pattern))
                    {
                        result += "Error in line: " + i + "\n\r";
                    }
                }

                return result;
            }

            /// <summary>
            /// Parses the specified instruction.
            /// </summary>
            /// <param name="instruction">The instruction.</param>
            /// <returns>Signal</returns>
            public static Signal Pars(string instruction)
            {
                Signal signal;

                try
                {
                    if (string.IsNullOrEmpty(instruction)) return null;

                    var parts = instruction.Trim().Split(' ');

                    var instType = parts[0].Trim();

                    if (Database.FileNamTag.StartsWith(instType) || instType.StartsWith(CommentsTag)) return null;

                    var type = instType.Equals(Start)
                                               ? InstructionType.Start
                                               : instType.Equals(End) ? InstructionType.End : InstructionType.Signal;

                    switch (type)
                    {
                        case InstructionType.Start:
                            int delay = 0;
                            if (parts.Length > 1) int.TryParse(parts[1], out delay);
                            signal = new Signal { InstructionType = type, Delay = delay };
                            break;
                        case InstructionType.End:
                            int duration = 0;
                            if (parts.Length > 1) int.TryParse(parts[1], out duration);
                            signal = new Signal { InstructionType = type, Duration = duration };
                            break;
                        case InstructionType.Signal:
                            var signalType = instType;
                            var amplitude = int.Parse(parts[1].Split(':')[1]);

                            var frequency = int.Parse(parts[2].Split(':')[1]);

                            var color = parts[3].Split(':')[1];

                            signal = new Signal
                                {
                                    InstructionType = type,
                                    SignalType = (Waveforms)Enum.Parse(typeof(Waveforms), signalType),
                                    Amplitude = amplitude,
                                    Frequency = frequency,
                                    Color = (Colors)Enum.Parse(typeof(Colors), color)
                                };
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                catch (Exception)
                {
                    signal = null;
                }

                return signal;
            }

            /// <summary>
            /// Colors
            /// </summary>
            public enum Colors
            {
                /// <summary>
                /// The red
                /// </summary>
                Red,
                /// <summary>
                /// The blue
                /// </summary>
                Blue,
                /// <summary>
                /// The green
                /// </summary>
                Green,
                /// <summary>
                /// The yellow
                /// </summary>
                Yellow,
                /// <summary>
                /// The black
                /// </summary>
                Black
            }

            /// <summary>
            /// InstructionType
            /// </summary>
            public enum InstructionType
            {
                /// <summary>
                /// The start
                /// </summary>
                Start,
                /// <summary>
                /// The end
                /// </summary>
                End,
                /// <summary>
                /// The signal
                /// </summary>
                Signal
            }

            /// <summary>
            /// Waveforms
            /// </summary>
            public enum Waveforms
            {
                /// <summary>
                ///     The sine
                /// </summary>
                Sine,

                /// <summary>
                ///     The square
                /// </summary>
                Square,

                /// <summary>
                ///     The triangle
                /// </summary>
                Triangle,

                /// <summary>
                ///     The sawtooth
                /// </summary>
                Sawtooth
            }
        }
    }
}
