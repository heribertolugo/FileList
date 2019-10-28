//    nVLC
//    
//    Author:  Roman Ginzburg
//
//    nVLC is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    nVLC is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.
//     
// ========================================================================

using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace Implementation.Utils
{
    class ProxyBuilder<T> : RealProxy
    {
        private readonly object _target;
        private List<IHandler> _handlers = new List<IHandler>();

        public ProxyBuilder(object target) : base(typeof(T))
        {
            if (!typeof(T).IsInterface)
                throw new InvalidOperationException("Only interface types can be proxied");

            _target = target;
        }

        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = msg as IMethodCallMessage;
            if (methodCall == null)
                return null;

            foreach (var handler in _handlers)
            {
                var attr = Attribute.GetCustomAttribute(methodCall.MethodBase, handler.AttribureType);
                if (attr != null)
                {
                    handler.Handler.DynamicInvoke(attr);
                }
            }
                 
            try
            {
                var result = methodCall.MethodBase.Invoke(_target, methodCall.InArgs);               
                return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
            }
            catch (Exception ex)
            {
                return new ReturnMessage(ex, methodCall);
            }
        }

        public void RegisterAttributeHandler<TAttribute>(Action<TAttribute> handler) where TAttribute : Attribute
        {
            _handlers.Add(new AttributeHandler<TAttribute>(handler));
        }

        private interface IHandler
        {
            Delegate Handler { get; set; }

            Type AttribureType { get; set; }
        }

        private class AttributeHandler<Q> : IHandler where Q : Attribute
        {
            public AttributeHandler(Action<Q> handler)
            {
                AttribureType = typeof(Q);
                Handler = handler;
            }

            public Delegate Handler { get; set; }

            public Type AttribureType { get; set; }
        }
    } 
}
