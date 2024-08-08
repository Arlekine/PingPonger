using System;
using System.Collections.Generic;

namespace PingPonger.Gameplay
{
    public class SessionServiceLocator : IServiceLocator<ISessionService>
    {
        private Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        public void AddService(IService service)
        {
            var serviceType = service.GetType();
            if (_services.ContainsKey(serviceType))
                throw new ArgumentException("Attempt to add service of existing type");

            _services.Add(serviceType, service);
        }

        public T GetService<T>() where T : ISessionService
        {
            return (T)_services[typeof(T)];
        }
    }
}