using System;
using System.Threading;
using Common;
using Common.Messages;
using Common.Messaging;

namespace EquEngine
{
    internal interface IExecuterCallback
    {
        IPublish DataBus { get; }
        void Start();
        void Stop();
    }

    internal class Executer
    {
        private Timer _signalTimer;
        private readonly Global.Instructions.Signal _signal;
        private readonly IExecuterCallback _callback;
        private SignalGenerator _signalGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Executer"/> class.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <param name="callback">The callback.</param>
        public Executer(Global.Instructions.Signal signal, IExecuterCallback callback)
        {
            _signal = signal;
            _callback = callback;
        }

        public string MethodName { get; set; }

        public int SignalId { get; set; }

        public void Execute()
        {
            switch (_signal.InstructionType)
            {
                case Global.Instructions.InstructionType.Start:
                    _signalTimer = new Timer(Start, null, _signal.Delay == 0 ? 1 :_signal.Delay, int.MaxValue);
                    break;
                case Global.Instructions.InstructionType.End:
                    break;
                case Global.Instructions.InstructionType.Signal:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Start()
        {
            switch (_signal.InstructionType)
            {
                case Global.Instructions.InstructionType.Start:
                    break;
                case Global.Instructions.InstructionType.End:
                    _signalTimer = new Timer(Start, null, _signal.Duration, int.MaxValue);
                    break;
                case Global.Instructions.InstructionType.Signal:
                    _signalGenerator = new SignalGenerator
                    {
                        SignalType = _signal.SignalType,
                        Amplitude = _signal.Amplitude,
                        Frequency = _signal.Frequency
                    };

                    _callback.DataBus.Publish(new Execution.DataPoint { SignalId = SignalId, Color = _signal.Color, MethodName = MethodName, TimeStamp = 0, Value = 0 });
                    _signalTimer = new Timer(Start, null, 0, 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            
        }

        public void Stop()
        {
            if (_signalTimer == null) return;
            _signalGenerator = null;
            _signalTimer.Dispose();
            _signalTimer = null;
        }

        private void Start(object o)
        {
            switch (_signal.InstructionType)
            {
                case Global.Instructions.InstructionType.Start:
                    _callback.Start();
                    Stop();
                    break;
                case Global.Instructions.InstructionType.End:
                    _callback.Stop();
                    Stop();
                    break;
                case Global.Instructions.InstructionType.Signal:
                    float time;
                    float value = _signalGenerator.GetValue(out time);
                    _callback.DataBus.Publish(new Execution.DataPoint { SignalId = SignalId, Color = _signal.Color, MethodName = MethodName, TimeStamp = time, Value = value });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
