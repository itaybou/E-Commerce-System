﻿using System;
using System.Timers;

namespace ECommerceSystem.DataAccessLayer.repositories.cache
{
    internal class CacheCleaner
    {
        private Timer _cleanCacheTimer;
        private Action _cleanCacheAction;
        private static volatile bool _isRunning;

        public CacheCleaner(Action cleanCache, int cacheCleanSecondsInterval)
        {
            _isRunning = false;
            _cleanCacheAction = cleanCache;
            _cleanCacheTimer = new Timer(cacheCleanSecondsInterval * 1000);
            _cleanCacheTimer.Elapsed += (sender, e) => CleanCache(sender, e, _cleanCacheAction);
            _cleanCacheTimer.AutoReset = true;
        }

        public void StartCleaner()
        {
            _cleanCacheTimer.Enabled = true;
            _cleanCacheTimer.Start();
        }

        private static void CleanCache(Object source, ElapsedEventArgs e, Action cleanCache)
        {
            if (_isRunning) return;
            _isRunning = true;
            cleanCache();
            _isRunning = false;
        }
    }
}