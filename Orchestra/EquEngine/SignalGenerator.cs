using System;
using System.Diagnostics;
using Common;

namespace EquEngine
{
    /// <summary>
    /// SignalGenerator
    /// </summary>
    internal class SignalGenerator
    {
        #region [ Properties ... ]

        private Global.Instructions.Waveforms _signalType = Global.Instructions.Waveforms.Sine;

        /// <summary>
        ///     Signal Type.
        /// </summary>
        public Global.Instructions.Waveforms SignalType
        {
            get { return _signalType; }
            set { _signalType = value; }
        }

        private float _frequency = 1f;

        /// <summary>
        ///     Signal Frequency.
        /// </summary>
        public float Frequency
        {
            get { return _frequency; }
            set { _frequency = value; }
        }

        private float _phase;

        /// <summary>
        ///     Signal Phase.
        /// </summary>
        public float Phase
        {
            get { return _phase; }
            set { _phase = value; }
        }

        private float _amplitude = 1f;

        /// <summary>
        ///     Signal Amplitude.
        /// </summary>
        public float Amplitude
        {
            get { return _amplitude; }
            set { _amplitude = value; }
        }

        private float _offset;

        /// <summary>
        ///     Signal Offset.
        /// </summary>
        public float Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        private int _invert = 1; // Yes=-1, No=1

        /// <summary>
        ///     Signal Inverted?
        /// </summary>
        public bool Invert
        {
            get { return _invert == -1; }
            set { _invert = value ? -1 : 1; }
        }

        #endregion  [ Properties ]

        #region [ Private ... ]

        /// <summary>
        ///     Time the signal generator was started
        /// </summary>
        private long _startTime = Stopwatch.GetTimestamp();

        /// <summary>
        ///     Ticks per second on this CPU
        /// </summary>
        private readonly long _ticksPerSecond = Stopwatch.Frequency;

        #endregion  [ Private ]

        #region [ Public ... ]

        /// <summary>
        ///     Initializes a new instance of the <see cref="SignalGenerator" /> class.
        /// </summary>
        /// <param name="initialSignalType">Initial type of the signal.</param>
        public SignalGenerator(Global.Instructions.Waveforms initialSignalType)
        {
            _signalType = initialSignalType;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SignalGenerator" /> class.
        /// </summary>
        public SignalGenerator()
        {
        }

#if DEBUG
        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public float GetValue(float time)
#else
    private float GetValue(float time)
#endif
        {
            float value = 0f;
            float t = _frequency*time + _phase;
            switch (_signalType)
            {
                    // http://en.wikipedia.org/wiki/Waveform
                case Global.Instructions.Waveforms.Sine: // sin( 2 * pi * t )
                    value = (float) Math.Sin(2f*Math.PI*t);
                    break;
                case Global.Instructions.Waveforms.Square: // sign( sin( 2 * pi * t ) )
                    value = Math.Sign(Math.Sin(2f*Math.PI*t));
                    break;
                case Global.Instructions.Waveforms.Triangle:
                    // 2 * abs( t - 2 * floor( t / 2 ) - 1 ) - 1
                    value = 1f - 4f*(float) Math.Abs(Math.Round(t - 0.25f) - (t - 0.25f));
                    break;
                case Global.Instructions.Waveforms.Sawtooth:
                    // 2 * ( t/a - floor( t/a + 1/2 ) )
                    value = 2f*(t - (float) Math.Floor(t + 0.5f));
                    break;
            }

            return (_invert*_amplitude*value + _offset);
        }

        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <returns></returns>
        public float GetValue(out float time)
        {
            time = (float) (Stopwatch.GetTimestamp() - _startTime)
                         /_ticksPerSecond;
            return GetValue(time);
        }

        /// <summary>
        ///     Resets this instance.
        /// </summary>
        public void Reset()
        {
            _startTime = Stopwatch.GetTimestamp();
        }

        #endregion [ Public ]
    }

}