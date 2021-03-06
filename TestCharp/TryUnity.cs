﻿using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Diagnostics;
using System.Collections.Generic;

namespace TestCharp
{
    internal class TryUnity
    {
        internal static void StartTryUnity()
        {
            var container = new UnityContainer().LoadConfiguration();
            var eventTrigger = container.Resolve<IEventTrigger>();
            Console.WriteLine(eventTrigger.GetHashCode());
            var eventTrigger1 = container.Resolve<IEventTrigger>();
            Console.WriteLine(eventTrigger1.GetHashCode());
            var eventListener = container.Resolve<IEventListener>("first");
            Console.WriteLine(eventListener.GetType().Name);
            var secondEventListener = container.Resolve<IEventListener>("second");
            Console.WriteLine(secondEventListener.GetType().Name);
            eventListener.SubscribeToEvent(eventTrigger);
            eventTrigger.OnReactEvent();
        }

        internal static void StartTryUnityConstructorPassing()
        {
            var container = new UnityContainer().LoadConfiguration();
            var thirdEventListener = container.Resolve<IEventListenerExtension>();
            thirdEventListener.DisplayTemp();
        }
        internal static void TryUnityInterceptor()
        {
            var container = new UnityContainer();
            container.AddNewExtension<Interception>();
            container.RegisterType<ILogger, Logger>(
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<PerformanceInterceptionBehaviour>());
            var logger = container.Resolve<ILogger>();
            logger.Log("Simple log message");
            logger.StartTimer("Critical task ", true);
            logger.EndTimer();
        }
    }
    #region BasicUnity

    internal interface IEventTrigger
    {
        event EventHandler React;
        void OnReactEvent();
    }
    internal class EventTrigger : IEventTrigger
    {
        public event EventHandler React;

        public void OnReactEvent()
        {
            React?.Invoke(this, EventArgs.Empty);
        }
    }

    internal interface IEventListener
    {
        void SubscribeToEvent(IEventTrigger eventTrigger);
    }
    internal class EventListener : IEventListener
    {
        public void SubscribeToEvent(IEventTrigger eventTrigger)
        {
            eventTrigger.React += (sender, args) => Console.WriteLine("Triggered");
        }
    }

    internal class SecondEventListener : IEventListener
    {
        public void SubscribeToEvent(IEventTrigger eventTrigger)
        {
            throw new NotImplementedException();
        }
    }

    internal class ThirdEventListener : IEventListenerExtension
    {
        public ThirdEventListener(IEventTrigger eventTrigger)
        {
            if (eventTrigger != null)
            {
                Console.WriteLine("Inside the constructor of ThirdEventListener");
            }
        }
        public void DisplayTemp()
        {
            Console.WriteLine("Inside DisplayTemp");
        }
    }

    internal interface IEventListenerExtension
    {
        void DisplayTemp();
    }

    #endregion
    #region Interceptor

    #region TargetObject
    public interface ILogger
    {
        void Log(string message);
        void StartTimer(string message, bool alsoLog);
        void EndTimer();
    }

    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine("Inside Log() - {0}", message);
        }

        public void StartTimer(string message, bool alsoLog)
        {
            if(alsoLog)
            {
                Console.WriteLine("Inside StartTimer() - {0}", message);
            }
        }

        public void EndTimer()
        {
            ; // Not implemented
        }
    }
    #endregion
    #region InterceptionBehaviours
    public class PerformanceInterceptionBehaviour : IInterceptionBehavior
    {
        private string stopwatchMessage;
        private Stopwatch stopwatch;
        public bool WillExecute
        {
            get
            {
                return true;
            }
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            if(input.MethodBase.Name == "StartTimer")
            {
                stopwatchMessage = input.Arguments["message"].ToString();
                stopwatch = new Stopwatch();
                stopwatch.Start();
            }

            if(input.MethodBase.Name == "EndTimer")
            {
                stopwatch.Stop();
                Console.WriteLine("TIMERLOG : {0} took {1} ms", stopwatchMessage, stopwatch.ElapsedMilliseconds);
                stopwatch.Reset();
                stopwatchMessage = null;
            }
            IMethodReturn result = getNext()(input, getNext);
            return result;
            
        }
    }
    #endregion
    #endregion
}
