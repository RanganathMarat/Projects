using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace TestCharp
{
    internal class TryUnity
    {
        internal static void StartTryUnity()
        {
            var container = new UnityContainer().LoadConfiguration();
            var eventTrigger = container.Resolve<IEventTrigger>();
            var eventListener = container.Resolve<IEventListener>();
            eventListener.SubscribeToEvent(eventTrigger);
            eventTrigger.OnReactEvent();
        }

    }

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

}
