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

using Declarations;
using Declarations.Enums;
using Declarations.Structures;
using Implementation.Utils;
using LibVlcWrapper;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Implementation.Loggers
{
    internal unsafe sealed class LogSubscriber : DisposableBase
    {
        private IntPtr m_instance;
        private LogCallback m_callback;
        private ILogger m_logger;
        private List<SubscriptionData> m_subscribers = new List<SubscriptionData>();

        public LogSubscriber(ILogger logger, IntPtr pInstance)
        {
            m_instance = pInstance;
            m_logger = logger;
            m_callback = OnLogCallback;
            IntPtr hCallback = Marshal.GetFunctionPointerForDelegate(m_callback);
            LibVlcMethods.libvlc_log_set(m_instance, hCallback, IntPtr.Zero);
        }

        private void OnLogCallback(void* data, libvlc_log_level level, void* ctx, char* fmt, char* args)
        {
            try
            {
                int bufSize = NativeMethods._vscprintf(fmt, args);
                char* buffer = stackalloc char[bufSize + 1];
                int len = NativeMethods.vsprintf(buffer, fmt, args);
                string msg = Marshal.PtrToStringAnsi(new IntPtr(buffer), len);

                switch (level)
                {
                    case libvlc_log_level.LIBVLC_DEBUG:
                        m_logger.Debug(msg);
                        break;
                    case libvlc_log_level.LIBVLC_NOTICE:
                        m_logger.Info(msg);
                        break;
                    case libvlc_log_level.LIBVLC_WARNING:
                        m_logger.Warning(msg);
                        break;
                    case libvlc_log_level.LIBVLC_ERROR:
                    default:
                        m_logger.Error(msg);
                        break;
                }

                NotifySubscribers(level, msg);
            }
            catch (Exception ex)
            {
                m_logger.Error("Failed to handle log callback, reason : " + ex.Message);
            }
        }

        private void NotifySubscribers(libvlc_log_level level, string msg)
        {
            LogMessage logMsg = new LogMessage((LogLevel)level, msg);
            lock (m_subscribers)
            {
                foreach (var item in m_subscribers)
                {
                    if (item.Filter == null)
                    {
                        item.Callback(logMsg);
                    }
                    else if (item.Filter(logMsg))
                    {
                        item.Callback(logMsg);
                    }
                }
            }
        }

        public IDisposable Subscribe(Action<LogMessage> callback, Predicate<LogMessage> filter)
        {
            var data = new SubscriptionData() { Callback = callback, Filter = filter };
            lock (m_subscribers)
            {
                m_subscribers.Add(data);
                return new SubscriptionToken(m_subscribers, data);
            }
        }

        protected override void Dispose(bool disposing)
        {
            LibVlcMethods.libvlc_log_unset(m_instance);
                     
            if (disposing)
            {
                m_callback = null;
                m_subscribers = null;
            }
        }

        private class SubscriptionToken : IDisposable
        {
            private readonly List<SubscriptionData> m_subscribers;
            private readonly SubscriptionData m_subscriber;

            public SubscriptionToken(List<SubscriptionData> subscribers, SubscriptionData subscriber)
            {
                m_subscribers = subscribers;
                m_subscriber = subscriber;
            }

            public void Dispose()
            {
                if (m_subscribers == null || m_subscriber == null)
                    return;

                lock (m_subscribers)
                {
                    m_subscribers.Remove(m_subscriber);
                }
            }
        }

        private class SubscriptionData
        {
            public Action<LogMessage> Callback { get; set; }
            public Predicate<LogMessage> Filter { get; set; }
        }
    }
}
